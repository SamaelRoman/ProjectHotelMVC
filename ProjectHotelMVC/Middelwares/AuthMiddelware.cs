using Microsoft.AspNetCore.Http;
using ProjectHotelMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Middelwares
{
    public class AuthMiddelware
    {
        public RequestDelegate Next { get; set; }
        public AuthMiddelware(RequestDelegate Next)
        {
            this.Next = Next;
        }
        public async Task Invoke(HttpContext context, IAuthService authService){

            var JWT = context.Session.GetString("ValidationJWT");
            if(JWT != null)
            {
                var employee = await authService.IsAuthenticated(JWT);
                if(employee != null){
                    context.Items["Employee"] = employee;
                }
            }

            await Next.Invoke(context);
        }
    }
}
