using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Filters
{
    public class ThemeActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("theme"))
            {
                context.HttpContext.Response.Cookies.Append("theme", "2");
            }
        }
    }
}
