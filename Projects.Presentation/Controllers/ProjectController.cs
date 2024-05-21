using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Projects.Logic.Projects.Commands.CreateProject;
using Projects.Logic.Projects.Commands.DeleteProject;
using Projects.Logic.Projects.Commands.UpdateProject;
using Projects.Logic.Projects.Queries.FilterProjects;
using Projects.Logic.Projects.Queries.GetProject;
using Projects.Logic.Projects.Queries.GetProjectList;
using Projects.Logic.Projects.Queries.SortProjects;
using Projects.Presentation.Models.Projects;

namespace Projects.Presentation.Controllers;

public class ProjectController(IMediator mediator, IMemoryCache cache, 
    ILogger<ProjectController> logger) : Controller
{
    // mediator - is used to send requests to their handlers
    // cache - is used to avoid making unnecessary queries to the database
    //         (for example, when we just want to sort the collection or
    //          for storing data from different steps of the wizard)

    [HttpGet]
    public async Task<IActionResult> Projects(ProjectsViewModel projectsVm)
    {
        try
        {
            // If ajax request and data already in cache
            if (Request.Headers.XRequestedWith == "XMLHttpRequest" &&
                cache.TryGetValue<ProjectListVm>("ProjectsData", out var projectsVmCache) && 
                projectsVmCache != null)
            {
                var filterQuery = new FilterProjectsQuery
                {
                    ProjectListVm = projectsVmCache,
                    Filters =
                    [
                        projectsVm.PriorityFilter,
                        projectsVm.DateStartFilter,
                        projectsVm.DateEndFilter
                    ]
                };
                
                var filteredVm = await mediator.Send(filterQuery);
                
                var sortQuery = new SortProjectsQuery
                {
                    ProjectListVm = filteredVm,
                    Column = projectsVm.SortBy,
                    Order = projectsVm.SortOrder
                };
                
                projectsVm.ProjectsList = await mediator.Send(sortQuery);
                return PartialView("_ProjectTable", projectsVm);
            }
            
            var query = new GetProjectListQuery();
            var listVm = await mediator.Send(query);
            projectsVm.ProjectsList = listVm;
            cache.Set("ProjectsData", listVm);
            
            return View(projectsVm);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving the list of projects", exception: ex);
            return NoContent();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ProjectDetails(Guid id)
    {
        try
        {
            var query = new GetProjectQuery { Id = id };
            var employee = await mediator.Send(query);

            return View(employee);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving project information", exception: ex);
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult CreateProject() => View();

    [HttpPost]
    public IActionResult CreateProjectStep1(CreateProjectStep1Dto dto)
    { 
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateProjectStep1", dto);
        }

        cache.Set("Step1Data", dto);
        return PartialView("_CreateProjectStep2");
    }

    [HttpPost]
    public IActionResult CreateProjectStep2(CreateProjectStep2Dto dto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateProjectStep2", dto);
        }

        cache.Set("Step2Data", dto);
        return PartialView("_CreateProjectStep3");
    }
    
    [HttpPost]
    public IActionResult CreateProjectStep3(CreateProjectStep3Dto dto)
    {
        cache.Set("Step3Data", dto);
        return PartialView("_CreateProjectStep4");
    }

    [HttpPost]
    public IActionResult CreateProjectStep4(CreateProjectStep4Dto dto)
    {
        cache.Set("Step4Data", dto);
        return PartialView("_CreateProjectStep5");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProject(CreateProjectStep5Dto dto)
    {
        try
        {
            var step1Data = cache.Get<CreateProjectStep1Dto>("Step1Data")!;
            var step2Data = cache.Get<CreateProjectStep2Dto>("Step2Data")!;
            var step3Data = cache.Get<CreateProjectStep3Dto>("Step3Data")!;
            var step4Data = cache.Get<CreateProjectStep4Dto>("Step4Data")!;

            var command = new CreateProjectCommand
            {
                ProjectName = step1Data.ProjectName,
                Priority = step1Data.Priority,
                DateStart = step1Data.DateStart,
                DateEnd = step1Data.DateEnd,
                CompanyCustomer = step2Data.CompanyCustomer,
                CompanyExecutor = step2Data.CompanyExecutor,
                ManagerId = step3Data.ManagerId,
                ExecutorsId = step4Data.ExecutorsId?.Split(',').Select(Guid.Parse)
            };

            Guid newProjectId = await mediator.Send(command);
            
            if (dto.Files != null && dto.Files.Any())
            {
                Directory.CreateDirectory("uploads");
                
                foreach (var file in dto.Files)
                {
                    if (file.Length == 0) continue;

                    Directory.CreateDirectory($"uploads/{newProjectId}");

                    await using var stream = new FileStream($"uploads/{newProjectId}/{file.FileName}", FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            
            cache.Remove("Step1Data");
            cache.Remove("Step2Data");
            cache.Remove("Step3Data");
            cache.Remove("Step4Data");
            
            return RedirectToAction(nameof(ProjectDetails), new { id = newProjectId });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while creating a new project", exception: ex);
            return NoContent();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        try
        {
            var command = new DeleteProjectCommand { Id = id };
            await mediator.Send(command);

            return RedirectToAction(nameof(Projects));
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while deleting a project", exception: ex);
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProject(Guid id)
    {
        try
        {
            var query = new GetProjectQuery { Id = id };
            var project = await mediator.Send(query);

            // Create a string with all id of executors to easily edit them through a hidden input field on the form
            string executorsId = string.Join(',', project.Executors is { Count: > 0 }
                ? project.Executors?.Select(e => e.Id)
                : string.Empty);

            return View(new UpdateProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                CompanyCustomer = project.CompanyCustomer,
                CompanyExecutor = project.CompanyExecutor,
                Priority = project.Priority,
                DateStart = project.DateStart,
                DateEnd = project.DateEnd,
                ManagerId = project.ManagerId,
                Manager = project.Manager,
                Executors = project.Executors,
                ExecutorsId = executorsId
            });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving project information", exception: ex);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var command = new UpdateProjectCommand
            {
                Id = dto.Id,
                ProjectName = dto.ProjectName,
                Priority = dto.Priority,
                DateStart = dto.DateStart,
                DateEnd = dto.DateEnd,
                ManagerId = dto.ManagerId,
                CompanyCustomer = dto.CompanyCustomer,
                CompanyExecutor = dto.CompanyExecutor,
                ExecutorsId = dto.ExecutorsId?.Split(',').Select(Guid.Parse)
            };

            await mediator.Send(command);

            return RedirectToAction(nameof(ProjectDetails), new { id = dto.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving project information", exception: ex);
            return NoContent();
        }
    }
}