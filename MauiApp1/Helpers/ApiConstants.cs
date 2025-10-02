using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Helpers
{
    public class ApiConstants
    {
        public const bool isDev = true;
        public const string BaseUrl = isDev ? "http://10.0.2.2:5000" : "https://jakeposapi.onrender.com" ;


    }
}
