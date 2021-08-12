using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Controllers
{
    public class MembersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MembersController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(string queryString)
        {
            if(queryString != null)
            {
                return Ok(await  _context.Users.Where(m => m.DisplayName.Contains(queryString))
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .ToListAsync());
            }

            return Ok(await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync());
        }
   }
}