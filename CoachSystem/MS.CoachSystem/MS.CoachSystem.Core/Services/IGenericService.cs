namespace MS.CoachSystem.Core.Services;

public interface IGenericService<T, TDto> where T : class where TDto : class
{
    Task<TDto> GetByIdAsync(int id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> AddAsync(TDto entity);
    Task<TDto> UpdateAsync(TDto entity, int id);
    Task<TDto> DeleteAsync(int id);

    Task<T> GetMainEntityByIdAsync(int id);

    Task<IEnumerable<T>> GetAllMainEntitiesAsync();
}
