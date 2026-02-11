using AutoMapper;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models
{
    public class MapperProfil : Profile
    {
        public MapperProfil()
        {
            CreateMap<Product, ProductViewModel>();

        }
    }
}