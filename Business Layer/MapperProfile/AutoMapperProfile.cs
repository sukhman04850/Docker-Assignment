using AutoMapper;
using Microsoft.Data.SqlClient;
using Shared_Layer.DTO;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IncomingExpenseGroupDTO, ExpenseGroup>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<ExpenseGroup, ExpenseGroupDTO>()
                 .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<IncomingUser,Users>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<IncomingExpense,Expenses>()
                .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=> Guid.NewGuid()))
                .ForMember(dest=>dest.Date, opt=>opt.MapFrom(src=>DateTime.Now));



        }
    }
}
