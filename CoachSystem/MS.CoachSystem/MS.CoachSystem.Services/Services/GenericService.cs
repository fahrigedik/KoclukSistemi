using AutoMapper;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;

namespace MS.CoachSystem.Service.Services;
public class GenericService<T, TDto> : IGenericService<T, TDto> where T : class where TDto : class
{
    private readonly IGenericRepository<T> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public GenericService(IGenericRepository<T> _repository, IMapper _mapper, IUnitOfWork _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity is null)
        {
            throw new FileNotFoundException("Entity is not found");
        }

        return mapper.Map<TDto>(entity);
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();

        if (entities is null)
        {
            throw new FileNotFoundException("Entities is not found");
        }

        return mapper.Map<List<TDto>>(entities);
    }

    public async Task<TDto> AddAsync(TDto entity)
    {
        var newEntity = await repository.AddAsync(mapper.Map<T>(entity));

        if (newEntity is null)
        {
            throw new FileNotFoundException("entity oluşturulurken hata.");
        }
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<TDto>(newEntity);
    }

    public async Task<TDto> UpdateAsync(TDto entity, int id)
    {
        var entityToUpdate = await repository.GetByIdAsync(id);
        if (entityToUpdate is null)
        {
            throw new FileNotFoundException("Entity is not found");
        }
        var updatedEntity = mapper.Map(entity, entityToUpdate);
        var result = await repository.UpdateAsync(updatedEntity);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<TDto>(updatedEntity);
    }

    public async Task<TDto> DeleteAsync(int id)
    {
        var entityToDelete = await repository.GetByIdAsync(id);
        if (entityToDelete is null)
        {
            throw new FileNotFoundException("Entity is not found");
        }
        repository.Delete(entityToDelete);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<TDto>(entityToDelete);
    }

    public async Task<T> GetMainEntityByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity is null)
        {
            throw new FileNotFoundException("Entity is not found");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllMainEntitiesAsync()
    {
        var entities = await repository.GetAllAsync();

        if (entities is null)
        {
            throw new FileNotFoundException("Entities is not found");
        }

        return entities;
    }
}
