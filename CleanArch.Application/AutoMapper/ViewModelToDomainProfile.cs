using Application.Cqrs.Elevator.Commands.PostCallElevatorCommand;
using Application.Cqrs.Elevator.Commands.PostCallElevatorFromFloorCommand;
using AutoMapper;
using Domain.Models.Elevator;

namespace CleanArch.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<PostCallElevatorCommand, ElevatorCallStep>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ElevatorId, opt => opt.MapFrom(x => x.Id));

            CreateMap<PostCallElevatorFromFloorCommand, ElevatorCallStep>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ElevatorId, opt => opt.MapFrom(x => x.Id));
        }
    }
}

