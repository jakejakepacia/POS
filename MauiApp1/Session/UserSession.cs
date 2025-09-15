using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Session
{
    public class UserSession
    {
        public int? UserId { get; private set; }

        public void SetStoreId(int userId)
        {
            UserId = userId;
        }

        public void Clear()
        {
            UserId = null;
        }

        public bool IsLoggedIn => UserId.HasValue;
    }
}
