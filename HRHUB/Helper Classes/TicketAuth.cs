using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HRHUB.Helper_Classes
{
    public class TicketAuth
    {
        public HttpCookie Encrypt(string id, int timeout)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "userName", DateTime.Now, DateTime.Now.AddMinutes(timeout), false, id, FormsAuthentication.FormsCookiePath);
            HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            return c;
        }

        public int Decrypt()
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            int key = Convert.ToInt32(ticket.Name);
            return key;
        }
    }
}