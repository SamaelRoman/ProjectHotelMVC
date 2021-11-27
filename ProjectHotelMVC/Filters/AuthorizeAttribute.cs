using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string[] Roles;
        public AuthorizeAttribute(params string[] Roles)
        {
            if (Roles == null)
            {
                this.Roles = null;
            }
            else
            {
                this.Roles = Roles;
            }
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            EmployeeViewModel employee = context.HttpContext.Items["Employee"] as EmployeeViewModel;
            if (employee == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (Roles != null)
                {
                    if (!Roles.Contains(employee.Role.RoleName))
                    {
                        context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                    }
                }
            }
        }
    }
}
