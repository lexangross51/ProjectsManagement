using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Logic.Employees.Commands.CreateEmployee;
using Projects.Logic.Employees.Commands.DeleteEmployee;
using Projects.Logic.Employees.Commands.UpdateEmployee;
using Projects.Logic.Employees.Queries.GetEmployee;
using Projects.Logic.Employees.Queries.GetEmployeeList;
using Projects.Presentation.Models.Employees;

namespace Projects.Presentation.Controllers;

public class EmployeeController(IMediator mediator, ILogger<EmployeeController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Employees()
    {
        try
        {
            var query = new GetEmployeeListQuery();
            var listVm = await mediator.Send(query);

            return View(listVm);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving the list of employees", exception: ex);
            return NoContent();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> EmployeeDetails(Guid id)
    {
        try
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await mediator.Send(query);

            return View(employee);
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while receiving employee information", exception: ex);
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult CreateEmployee() => View();

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var command = new CreateEmployeeCommand
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Mail = dto.Mail
            };
            
            Guid newEmployeeId = await mediator.Send(command);
            return RedirectToAction(nameof(EmployeeDetails), new { id = newEmployeeId });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while adding a new employee", exception: ex);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateEmployee(Guid id)
    {
        try
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await mediator.Send(query);

            return View(new UpdateEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Mail = employee.Mail
            });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while updating an employee", exception: ex);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        
        try
        {
            var command = new UpdateEmployeeCommand
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Mail = dto.Mail
            };

            await mediator.Send(command);
            return RedirectToAction(nameof(EmployeeDetails), new { id = dto.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while updating an employee", exception: ex);
            throw;
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        try
        {
            var command = new DeleteEmployeeCommand { Id = id };
            await mediator.Send(command);

            return RedirectToAction(nameof(Employees));
        }
        catch (Exception ex)
        {
            logger.LogError(message: "Error while deleting an employee", exception: ex);
            return NotFound();
        }
    }
}