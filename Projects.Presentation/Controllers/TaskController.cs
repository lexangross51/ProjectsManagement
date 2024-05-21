using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Projects.Logic.Tasks.Commands.CreateTask;
using Projects.Logic.Tasks.Commands.DeleteTask;
using Projects.Logic.Tasks.Commands.UpdateTask;
using Projects.Logic.Tasks.Queries.FilterTasks;
using Projects.Logic.Tasks.Queries.GetTask;
using Projects.Logic.Tasks.Queries.GetTaskList;
using Projects.Logic.Tasks.Queries.SortTasks;
using Projects.Presentation.Models.Tasks;

namespace Projects.Presentation.Controllers;

public class TaskController(IMediator mediator, ILogger<TaskController> logger, 
    IMemoryCache cache) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Tasks(TasksViewModel tasksVm)
    {
        try
        {
            // If ajax request and data already in cache
            if (Request.Headers.XRequestedWith == "XMLHttpRequest" &&
                cache.TryGetValue<TaskListVm>("TasksData", out var tasksVmCache) &&
                tasksVmCache != null)
            {
                var filterQuery = new FilterTasksQuery
                {
                    TaskListVm = tasksVmCache,
                    Filters =
                    [
                        tasksVm.PriorityFilter,
                        tasksVm.StatusFilter
                    ]
                };

                var filteredListVm = await mediator.Send(filterQuery);

                var sortQuery = new SortTasksQuery
                {
                    TaskListVm = filteredListVm,
                    Column = tasksVm.SortBy,
                    Order = tasksVm.SortOrder
                };

                tasksVm.TaskList = await mediator.Send(sortQuery);

                if (tasksVm.ProjectId.HasValue)
                {
                    tasksVm.TaskList.Tasks = tasksVm.TaskList.Tasks
                        .Where(t => t.ProjectId == tasksVm.ProjectId)
                        .ToList();
                }

                return tasksVm.ProjectId.HasValue
                    ? PartialView("_TaskTableWithDelete", tasksVm)
                    : PartialView("_TaskTable", tasksVm);
            }

            var query = new GetTaskListQuery();
            var listVm = await mediator.Send(query);
            tasksVm.TaskList = listVm;
            cache.Set("TasksData", listVm);

            return View(tasksVm);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving the list of tasks", exception: ex);
            return NoContent();
        }
    }

    [HttpGet]
    public async Task<IActionResult> TasksForProject(Guid projectId)
    {
        try
        {
            var tasksVm = new TasksViewModel
            {
                ProjectId = projectId, 
                ShowDeleteAndAddButtons = true
            };
            var query = new GetTaskListQuery();
            var listVm = await mediator.Send(query);
            listVm.Tasks = listVm.Tasks.Where(t => t.ProjectId == projectId).ToList();
            tasksVm.TaskList = listVm;
            cache.Set("TasksData", listVm);

            return View("Tasks", tasksVm);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving the list of tasks", exception: ex);
            return NoContent();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> TaskDetails(Guid id)
    {
        try
        {
            var query = new GetTaskQuery { Id = id };
            var task = await mediator.Send(query);

            return View(task);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving project information", exception: ex);
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult CreateTask(Guid projectId) => View(new CreateTaskDto { ProjectId = projectId });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTask(CreateTaskDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var command = new CreateTaskCommand
            {
                TaskName = dto.TaskName,
                Priority = dto.Priority,
                AuthorId = dto.AuthorId,
                Description = dto.Description,
                ExecutorId = dto.ExecutorId,
                ProjectId = dto.ProjectId
            };

            _ = await mediator.Send(command);

            return RedirectToAction(nameof(TasksForProject), new {projectId = dto.ProjectId});
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while creating a new task", exception: ex);
            return NoContent();
        }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTask(Guid id)
    {
        try
        {
            var query = new GetTaskQuery { Id = id };
            var task = await mediator.Send(query);

            return View(new UpdateTaskDto
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Priority = task.Priority,
                Status = task.Status,
                Description = task.Description,
                ExecutorId = task.ExecutorId,
                Executor = task.Executor
            });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving task information", exception: ex);
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateTask(UpdateTaskDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var command = new UpdateTaskCommand
            {
                Id = dto.Id,
                TaskName = dto.TaskName,
                Priority = dto.Priority,
                Status = dto.Status,
                Description = dto.Description,
                ExecutorId = dto.ExecutorId
            };

            await mediator.Send(command);

            return RedirectToAction(nameof(TaskDetails), new { id = dto.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while updating task information", exception: ex);
            return NoContent();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            // Store project id
            var res = await mediator.Send(new GetTaskQuery {Id = id});
            Guid projectId = res.ProjectId;
            
            var command = new DeleteTaskCommand { Id = id };
            await mediator.Send(command);

            return RedirectToAction(nameof(TasksForProject), new { projectId });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while deleting a task", exception: ex);
            return NotFound();
        }
    }

}