using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Enum;
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

    public async Task<GenericResponse<List<UserTask>>> GetUserTaskByStudentIdAsync(UserTaskRequestDto userTaskRequestDto)
    {
        var userTasks = await repository.Where(x =>
            x.CoachID == userTaskRequestDto.CoachID && x.StudentID == userTaskRequestDto.StudentID).ToListAsync();

        return GenericResponse<List<UserTask>>.Success(userTasks, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<UserTaskDto>> MarkTaskAsCompleted(UserTaskDto userTask)
    {
        if (userTask.IsCompleted == false)
        {
            userTask.Status = Status.Tamamlandı.ToString();
            userTask.CompletedDate = DateTime.Now;
            userTask.IsCompleted = true;
            userTask.IsWorking = false;
        }
        else
        {
            userTask.Status = Status.Tamamlanmadı.ToString();
            userTask.CompletedDate = null;
            userTask.IsCompleted = false;
        }
        return GenericResponse<UserTaskDto>.Success(userTask, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<UserTaskDto>> MarkTaskAsWorking(UserTaskDto userTask)
    {
        if (userTask.IsWorking == false)
        {
            userTask.Status = Status.Çalışılıyor.ToString();
            userTask.IsWorking = true;
            userTask.CompletedDate = null;

            userTask.IsCompleted = false;
            userTask.CompletedDate = null;
        }
        else
        {
            userTask.Status = Status.Tamamlanmadı.ToString();
            userTask.IsWorking = false;
        }
        return GenericResponse<UserTaskDto>.Success(userTask, HttpStatusCode.OK);
    }
}