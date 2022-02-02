using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    public class StatusSeeder 
    {
        
        public static async Task SeedStatuses(DataContext context)
        {
            var statuses = await context.Statuses.ToListAsync();
            if(statuses.Count == 0)
            {
                var statusList = new List<Status>()
                {
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Label = "New"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Label = "In Process"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Label = "Successfully finished"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Label = "Failed"
                    }
                };

                foreach(var s in statuses)
                {
                    context.Statuses.Add(s);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}