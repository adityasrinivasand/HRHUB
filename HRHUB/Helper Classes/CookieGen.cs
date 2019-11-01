using HRHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRHUB.Helper_Classes
{
    public class CookieGen
    {
        public static HttpCookie GenerateCookie(UserInfo u)
        {
            int timeOut = u.isAdmin==true ? 20 : 1;
            TicketAuth ticket = new TicketAuth();
            HttpCookie cookie = ticket.Encrypt(u.UserName.ToString(), timeOut);
            return cookie;
        }
    }
}