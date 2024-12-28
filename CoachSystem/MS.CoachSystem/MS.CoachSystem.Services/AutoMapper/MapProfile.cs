using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CoachingResource, CoachingResourceDto>().ReverseMap();
            CreateMap<CoachStudent, CoachStudentDto>().ReverseMap();
            CreateMap<CoachingSession, CoachingSessionDto>().ReverseMap();
            CreateMap<Goal, GoalDto>().ReverseMap();
            CreateMap<GoalType, GoalTypeDto>().ReverseMap();
            CreateMap<ResourceTag, ResourceTagDto>().ReverseMap();
            CreateMap<ResourceToTag, ResourceToTagDto>().ReverseMap();
            CreateMap<ResourceType, ResourceTypeDto>().ReverseMap();
            CreateMap<UserTask, UserTaskDto>().ReverseMap();
        }

    }
}
