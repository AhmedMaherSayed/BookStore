using AutoMapper;
using BookStore.API.DTOs;
using BookStore.Core.Models;

namespace BookStore.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, AddCustomerDto>()
                .ReverseMap();
        }
    }
}
