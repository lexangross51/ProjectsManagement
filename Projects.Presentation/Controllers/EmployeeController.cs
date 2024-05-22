using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projects.DataAccess.Database;
using Projects.Logic.Employees.Commands.CreateEmployee;
using Projects.Logic.Employees.Commands.DeleteEmployee;
using Projects.Logic.Employees.Commands.UpdateEmployee;
using Projects.Logic.Employees.Queries.GetEmployee;
using Projects.Logic.Employees.Queries.GetEmployeeList;
using Projects.Presentation.Models.Employees;

namespace Projects.Presentation.Controllers;

[Authorize(Roles = "Admin,Director")]
public class EmployeeController(IMediator mediator, ILogger<EmployeeController> logger,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) : Controller
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
            var existingUser = await userManager.FindByEmailAsync(dto.Mail);
            
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "A user with this email already exists");
                return View(dto);
            }
            
            var appUser = new ApplicationUser
            {
                UserName = dto.Mail,
                Email = dto.Mail
            };

            var result = await userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Ensure the role exists
            if (!await roleManager.RoleExistsAsync(dto.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(dto.Role));
            }

            // Assign the appUser to the role
            await userManager.AddToRoleAsync(appUser, dto.Role);
            
            var command = new CreateEmployeeCommand
            {
                Id = Guid.Parse(appUser.Id),
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
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);

            if (roles.Any())
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }

            var res = await userManager.DeleteAsync(user);

            if (!res.Succeeded)
            {
                throw new Exception("Error while deleting an employee");
            }
            
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

    [HttpGet]
    [Authorize(Roles = "Admin,Director,Manager")]
    public async Task<IActionResult> EmployeesJson(string term, string role)
    { 
        var query = new GetEmployeeListQuery();
        var listVm = await mediator.Send(query);

        // Get all employees in role
        var employees = new List<EmployeeLookupDto>();

        foreach (var e in listVm.Employees)
        {
            var user = await userManager.FindByIdAsync(e.Id.ToString());

            if (user == null) continue;

            if (await userManager.IsInRoleAsync(user, role))
            {
                employees.Add(e);
            }
        }
        
        if (string.IsNullOrEmpty(term))
        {
            return Json(employees
                .Select(e => new { id = e.Id, value = e.FullName }));
        }
        
        return Json(employees
            .Where(e => e.FullName.Contains(term, StringComparison.OrdinalIgnoreCase))
            .Select(e => new { id = e.Id, value = e.FullName }));
    }
}