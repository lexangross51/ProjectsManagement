using System.Linq.Expressions;

namespace Projects.Logic.Projects.Queries.FilterProjects.Filters;

public interface IFilterSpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}