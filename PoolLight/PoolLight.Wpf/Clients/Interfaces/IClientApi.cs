﻿using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientApi
    {
        Task<bool> AllumerAsync();

        Task<bool> EteindreAsync();
    }
}