using System.Net;
using AutoMapper;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class CoachStudentService : GenericService<CoachStudent, CoachStudentDto>, ICoachStudentService
{
    private readonly IGenericRepository<CoachStudent> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICoachStudentRepository coachStudentRepository;
    public CoachStudentService(IGenericRepository<CoachStudent> _repository, IMapper _mapper, IUnitOfWork _unitOfWork, ICoachStudentRepository coachStudentRepository) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
        this.coachStudentRepository = coachStudentRepository;
    }


    public  async Task<GenericResponse<List<string>>> GetStudentIdsByCoachIdAsync(string coachId)
    {
        var response = await coachStudentRepository.GetStudentIdsByCoachIdAsync(coachId);
        return GenericResponse<List<string>>.Success(response, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<string>> GetEntityIdByCoachIdAndStudentId(string coachId, string studentId)
    {
        var response =  repository.Where(x => x.CoachId == coachId && x.StudentId == studentId);
        var entity = response.First();

        if (entity is null)
        {
            return GenericResponse<string>.Fail("Entity not found", HttpStatusCode.NotFound, true);
        }

        return GenericResponse<string>.Success(entity.Id.ToString(), HttpStatusCode.OK);
    }

    public async  Task<GenericResponse<List<string>>> GetCoachIdByStudentIdAsync(string studentId)
    {
        var entity = coachStudentRepository.Where(x => x.StudentId == studentId);

        if (entity is null)
        {
            return GenericResponse<List<string>>.Fail("Entity not found", HttpStatusCode.NotFound, true);
        }
        var response = entity.Select(x => x.CoachId).ToList();
        return GenericResponse<List<string>>.Success(response, HttpStatusCode.OK);
    }
}
