using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BCNPortal.Utility
{
    public class ParameterBasedOnFormNameAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _actionParameterName;

        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            _name = name;
            _actionParameterName = actionParameterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.HttpContext.Request.Form[_name];
            filterContext.ActionArguments[_actionParameterName] = !string.IsNullOrEmpty(formValue);
        }
    }
}

