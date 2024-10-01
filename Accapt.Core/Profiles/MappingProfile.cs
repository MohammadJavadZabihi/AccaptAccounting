using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductUpdateDTO>();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<UserDTO, Users>();
            CreateMap<Users, UserDTO>();
            CreateMap<Users, UserUpdateDTO>();
            CreateMap<UserUpdateDTO, Users>();
        }
    }
}
