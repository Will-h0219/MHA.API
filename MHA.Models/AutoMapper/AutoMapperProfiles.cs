using AutoMapper;
using MHA.Models.DataTransferObjects;
using MHA.Models.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<NewUserDTO, User>().ReverseMap();
            CreateMap<NewCharacterDTO, Character>().ReverseMap();
            CreateMap<SearchParametersDTO, SearchParameters>().ForMember(dest => dest.Skips, y => y.MapFrom(x => x.Page))
                                                              .ForMember(x => x.UserId, opt => opt.Ignore())
                                                              .ForMember(x => x.CharacterId, opt => opt.Ignore());
            CreateMap<CharacterDTO, Character>().ForMember(dest => dest.Id, y => y.MapFrom(x => ObjectId.Parse(x.Id)));
            CreateMap<Character, CharacterDTO>().ForMember(dest => dest.Id, y => y.MapFrom(x => x.Id.ToString()));
        }
    }
}
