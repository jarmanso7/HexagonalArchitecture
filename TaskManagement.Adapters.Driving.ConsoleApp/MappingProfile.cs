using AutoMapper;
using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driving.DTOs;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskItemDto>();
        }
    }
}
