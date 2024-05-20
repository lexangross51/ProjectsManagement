using System.Linq.Expressions;

namespace Projects.Logic.Common.DataFiltration;

public interface IFilterSpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}