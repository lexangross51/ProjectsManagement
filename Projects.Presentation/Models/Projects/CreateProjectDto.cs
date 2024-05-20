using System.ComponentModel.DataAnnotations;

namespace Projects.Presentation.Models.Projects;

public class CreateProjectStep1Dto
{
    [MaxLength(256)]
    public string ProjectName { get; init; } = String.Empty;
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }

    [Range(0, 10)] 
    public uint Priority { get; init; }
}

public class CreateProjectStep2Dto
{
    [MaxLength(256)] public string CompanyCustomer { get; init; } = string.Empty;

    [MaxLength(256)]
    public string CompanyExecutor { get; init; } = string.Empty;
}

public class CreateProjectStep3Dto
{
    public Guid? ManagerId { get; init; }
}

public class CreateProjectStep4Dto
{
    public string? ExecutorsId { get; init; }
}

public class CreateProjectStep5Dto
{
    public IList<IFormFile>? Files { get; set; }
}