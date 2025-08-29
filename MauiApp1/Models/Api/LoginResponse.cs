using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models.Api
{
    public class LoginResponse
    {
        public int id {  get; set; }
        public string username { get; set; }
        public string accessToken { get; set; }
        public int expiresIn { get; set; }
    }
}
