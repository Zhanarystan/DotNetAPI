using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAPI.DTOs;
using DotNetAPI.Models;
using DotNetAPI.Core;

namespace DotNetAPI.Repositories
{
    public interface IChatRepository
    {
        Task<Result<ICollection<ChatDto>>> GetChats();
        Task<Result<ChatDto>> GetChat(string id);
        Task<Result<ChatDto>> CreateChat(string destinationEmail);
    }
}