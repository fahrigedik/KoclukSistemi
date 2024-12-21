using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Repositories;
using MS.AuthServer.Core.Services;
using MS.AuthServer.Core.UnitOfWork;

namespace MS.AuthServer.Service.Services;

public class GenericService<T, TDto> : IGenericService<T, TDto> where T : class where TDto : class
{

    public readonly IMapper _mapper;
    public readonly IGenericRepository<T> _repository;
    public readonly IUnitOfWork _unitOfWork;

    public GenericService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<T> genericRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = genericRepository;
    }

    public async Task<GenericResponse<TDto>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
        {
            return GenericResponse<TDto>.Fail("Entity is not found", HttpStatusCode.NotFound, true);
        }

        var entityDto = _mapper.Map<TDto>(entity);

        return GenericResponse<TDto>.Success(entityDto, HttpStatusCode.OK);
    }
    public async Task<GenericResponse<IEnumerable<TDto>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        if (entities is null)
        {
            return GenericResponse<IEnumerable<TDto>>.Fail("Entities is not found", HttpStatusCode.NotFound, true);
        }

        var entityDtos = _mapper.Map<IEnumerable<TDto>>(entities);

        return GenericResponse<IEnumerable<TDto>>.Success(entityDtos, HttpStatusCode.OK);
    }
    public GenericResponse<IEnumerable<TDto>> Where(Expression<Func<T, bool>> predicate)
    {
        var entities = _repository.Where(predicate).AsEnumerable();

        if (entities is null)
        {
            return GenericResponse<IEnumerable<TDto>>.Fail("entities is not found", HttpStatusCode.NotFound, true);
        }

        var entityDtos = _mapper.Map<IEnumerable<TDto>>(entities);

        return GenericResponse<IEnumerable<TDto>>.Success(entityDtos, HttpStatusCode.OK);
    }
    public async Task<GenericResponse<TDto>> AddAsync(TDto entity)
    {
        var newEntity = _mapper.Map<T>(entity);
        await _repository.AddAsync(newEntity);
        await _unitOfWork.SaveChangesAsync();

        var newDto = _mapper.Map<TDto>(newEntity);
        return GenericResponse<TDto>.Success(newDto, HttpStatusCode.OK);
    }
    public async Task<GenericResponse<NoDataDto>> Remove(int id)
    {
        var isExistEntity = await _repository.GetByIdAsync(id);

        if (isExistEntity is null)
        {
            return GenericResponse<NoDataDto>.Fail("Entity is not found", HttpStatusCode.NotFound, true);
        }

        _repository.Remove(isExistEntity);
        await _unitOfWork.SaveChangesAsync();

        return GenericResponse<NoDataDto>.Success(HttpStatusCode.OK);
    }
    public async Task<GenericResponse<NoDataDto>> Update(TDto entity, int id)
    {
        var isExistEntity = _repository.GetByIdAsync(id);

        if (isExistEntity is null)
        {
            return GenericResponse<NoDataDto>.Fail("Entity is not found", HttpStatusCode.NotFound, true);
        }

        var updateEntity = _mapper.Map<T>(entity);
        _repository.Update(updateEntity);
        _unitOfWork.SaveChangesAsync();

        return GenericResponse<NoDataDto>.Success(HttpStatusCode.OK);
    }
}
