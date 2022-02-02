using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAPI.DTOs;
using DotNetAPI.Models;
using DotNetAPI.Core;

namespace DotNetAPI.Repositories
{
    public interface IAlertRepository
    {
        Task<Result<TroubleAlert>> CreateAlert(AlertDto newAlert);
        Task<Result<List<TroubleAlert>>> GetAlerts();
    }
}