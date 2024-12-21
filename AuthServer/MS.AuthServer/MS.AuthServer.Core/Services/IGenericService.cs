using System.Linq.Expressions;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.Core.Services;

public interface IGenericService<T, TDto> where T : class where TDto : class
{
    Task<GenericResponse<TDto>> GetByIdAsync(int id);

    Task<GenericResponse<IEnumerable<TDto>>> GetAllAsync();

    GenericResponse<IEnumerable<TDto>> Where(Expression<Func<T, bool>> predicate);

    Task<GenericResponse<TDto>> AddAsync(TDto entity);

    Task<GenericResponse<NoDataDto>> Remove(int id);

    Task<GenericResponse<NoDataDto>> Update(TDto entity, int id);
}
