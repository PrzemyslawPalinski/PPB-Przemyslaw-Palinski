using System.Linq;
using AutoMapper;
using SimpleShop.API.Dtos;
using SimpleShop.API.Models;

namespace SimpleShop.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<ArticleForCreationDto, Article>();
            CreateMap<Purchase, PurchaseForDisplay>();
        }
    }
}