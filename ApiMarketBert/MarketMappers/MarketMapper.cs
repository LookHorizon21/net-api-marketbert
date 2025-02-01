using ApiMarketBert.Models;
using ApiMarketBert.Models.Dtos;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiMarketBert.MarketMapper
{
    public class MarketMapper : Profile
    {
        public MarketMapper()
        {
            CreateMap<CategoriaMd, CategoriaDto>().ReverseMap();
            CreateMap<CategoriaMd, CrearCategoriaDto>().ReverseMap();

            CreateMap<ProductoMd, ProductoDto>().ReverseMap();
            CreateMap<ProductoMd, CrearProductoDto>().ReverseMap();
        }
    }
}
