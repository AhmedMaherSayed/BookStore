using AutoMapper;
using BookStore.API.DTOs.BooksDtos;
using BookStore.API.DTOs.CustomerDtos;
using BookStore.API.DTOs.OrderDto;
using BookStore.Core.Models;

namespace BookStore.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, AddCustomerDto>()
                .ReverseMap();

            CreateMap<Customer, EditCustomerDto>()
                .ReverseMap();

            CreateMap<Customer, DisplayCustomerDto>()
                .ReverseMap();

            CreateMap<Customer, ChangePasswordDto>()
                .ForMember(dist => dist.NewPassword, o => o.MapFrom(src => src.PasswordHash))
                .ReverseMap();


            CreateMap<Book, BookDto>()
                .ForMember(dist => dist.AuthorName, o => o.MapFrom(src => src.Author.FullName))
                .ForMember(dist => dist.CatalogName, o => o.MapFrom(src => src.Catalog.Name))
                .ReverseMap();

            CreateMap<Book, UpdateBookDto>().ReverseMap();

            CreateMap<Book, AddBookDto>().ReverseMap();

        }
    }
}
