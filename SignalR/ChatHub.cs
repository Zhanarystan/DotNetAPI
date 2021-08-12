using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Interfaces;
using DotNetAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.SignalR
{
    public class ChatHub : Hub
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public ChatHub(DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;
        }

        public async Task SendMessage(MessageDto messageDto)
        {
            var chat = await _context.Chats.FindAsync(new Guid(messageDto.ChatId));
            var user = await _context.Users.Where(u => u.UserName.Equals(messageDto.AuthorUsername)).SingleOrDefaultAsync();
            
            var message = new Message
            {
                Author = user,
                Chat = chat,
                Text = messageDto.Text
            };
            
            chat.Messages.Add(message);

            await _context.SaveChangesAsync();

            var response = new {
                id = message.Id,
                text = message.Text,
                time = message.Time,
                author = message.Author,
                chatId = message.Chat.Id
            };

            await Clients.Group(messageDto.ChatId).SendAsync("ReceiveMessage", response);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var chatId = httpContext.Request.Query["chatId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            var chat = await _context.Chats.Include(c => c.Messages).ThenInclude(m => m.Author).SingleOrDefaultAsync(c => c.Id == new Guid(chatId));
            //var result = await _context.Messages.Where(m => m.Chat.Id == new Guid(chatId)).Include(m => m.Author).ToListAsync();
            // var result = chat.Messages.AsQueryable().Select(message => new {
            //     id = message.Id,
            //     text = message.Text,
            //     time = message.Time,
            //     author = message.Author,
            //     chatId = message.Chat.Id
            // });
            await Clients.Caller.SendAsync("LoadMessages", chat.Messages);
        }
    }
}