using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ILoginApiService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
