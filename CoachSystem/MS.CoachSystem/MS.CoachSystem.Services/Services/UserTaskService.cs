using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class UserTaskService : GenericService<UserTask, UserTaskDto>, IUserTaskService
{
    private readonly IGenericRepository<UserTask> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public UserTaskService(IGenericRepository<UserTask> _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }

    public async Task<GenericResponse<List<UserTaskDto>>> GetUserTaskByStudentIdAsync(UserTaskRequestDto userTaskRequestDto)
    {
        var userTasks = await repository.Where(x =>
            x.CoachID == userTaskRequestDto.CoachID && x.StudentID == userTaskRequestDto.StudentID).ToListAsync();

        var userTaskDtos = mapper.Map<List<UserTaskDto>>(userTasks);

        return GenericResponse<List<UserTaskDto>>.Success(userTaskDtos, HttpStatusCode.OK);
    }
}