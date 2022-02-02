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
    public class AlertRepository : IAlertRepository
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public AlertRepository(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _context = context;
        }
        public async Task<Result<TroubleAlert>> CreateAlert(AlertDto newAlert)
        {
            var user = await _context.Users.Where(u => u.UserName == newAlert.Username).FirstOrDefaultAsync();
            var alert = new TroubleAlert
            {
                Id = Guid.NewGuid(),
                Longitude = newAlert.Longitude,
                Latitude = newAlert.Latitude,
                User = user
            };

            _context.Alerts.Add(alert);

            var success = await _context.SaveChangesAsync() > 0;

            if(success) return Result<TroubleAlert>.Success(alert);

            return Result<TroubleAlert>.Failure("Failed to create alert");
        }

        public async Task<Result<List<TroubleAlert>>> GetAlerts()
        {
            var alerts = await _context.Alerts.ToListAsync();
            bool success = alerts.Count > 0;
            
            if(success) return Result<List<TroubleAlert>>.Success(alerts);
            
            return Result<List<TroubleAlert>>.Failure("Alerts doesn't found");
        }
    }
}