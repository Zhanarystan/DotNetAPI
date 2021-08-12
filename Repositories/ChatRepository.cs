using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetAPI.Core;
using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Interfaces;
using DotNetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public ChatRepository(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _context = context;
        }

        public async Task<Result<ICollection<ChatDto>>> GetChats()
        {
            
            var currentUser = await _context.Users.Where(u => u.UserName == _userAccessor.GetUsername()).FirstOrDefaultAsync();
            var chats = await _context.Chats.Where(c => c.Members.Contains(currentUser))
                .ProjectTo<ChatDto>(_mapper.ConfigurationProvider, new { currentUsername = currentUser.UserName }).ToListAsync();
            
            return Result<ICollection<ChatDto>>.Success(chats);
        }
        public async Task<Result<ChatDto>> GetChat(string id)
        {
            var chat = await _context.Chats.Include(c => c.Members)
                .ProjectTo<ChatDto>(_mapper.ConfigurationProvider,new{currentUsername = _userAccessor.GetUsername()})
                .SingleOrDefaultAsync(c => c.Id == new Guid(id));

            if(chat == null)
            {
                return Result<ChatDto>.Success(null);
            }
            
            return Result<ChatDto>.Success(chat);
        }
        public async Task<Result<ChatDto>> CreateChat(string destinationEmail)
        {
            var destinationUser = await _context.Users.Where(u => u.NormalizedEmail == destinationEmail.ToUpper())
                .FirstOrDefaultAsync();
                
            var currentUser = await _context.Users.Where(u => u.UserName == _userAccessor.GetUsername())
                .FirstOrDefaultAsync();

            if (destinationUser == null)
            {
                return Result<ChatDto>.Failure("Failed to find user");
            }

            var chat = new Chat
            {
                Id = Guid.NewGuid(),
            };

            chat.Members.Add(currentUser);
            chat.Members.Add(destinationUser);
            _context.Chats.Add(chat);

            var success = await _context.SaveChangesAsync() > 0;

            if(success) return Result<ChatDto>.Success(_mapper.Map<ChatDto>(chat));

            return Result<ChatDto>.Failure("Failed to create chat");
        }
    }
}