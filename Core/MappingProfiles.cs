using System;
using System.Linq;
using AutoMapper;
using DotNetAPI.DTOs;
using DotNetAPI.Interfaces;
using DotNetAPI.Models;

namespace DotNetAPI.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<AppUser, MemberDto>();

            CreateMap<Chat, ChatDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.Members))
                .ForMember(d => d.Destination, o => o.MapFrom(s =>
                    s.Members.Where(m => !m.UserName.Equals(currentUsername))
                    .Select(m => new MemberDto { 
                                Email = m.Email, 
                                Username = m.UserName,
                                Image = m.Image,
                                DisplayName = m.DisplayName, 
                            }).FirstOrDefault()));
        }
    }
}