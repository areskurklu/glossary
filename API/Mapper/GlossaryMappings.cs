using API.Models;
using API.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    public class GlossaryMappings :Profile
    {
        public GlossaryMappings()
        {
            CreateMap<Glossary, GlossaryDto>().ReverseMap();
        }
    }
}
