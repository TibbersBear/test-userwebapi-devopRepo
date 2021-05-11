using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_dotnet_core.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestTechAwsLogin.Models;
using TestTechAwsLogin.Service;

namespace aspnet_core_dotnet_core.Controllers
{
    public static class ControllerHelper
    {
        public static IActionResult HandleServerError(this ControllerBase controller, Exception exception)
        {
            return controller.Problem(statusCode: 500, detail: exception.Message);
        }
        public static ActionResult<T> HandleServerError<T>(this ControllerBase controller, Exception exception)
        {
            return controller.Problem(statusCode: 500, detail: exception.Message);
        }

        public static ActionResult<T> HandleServiceResponse<T>(this ControllerBase controller, T value)
        {
            try
            {
                if(value == null)
                    return controller.NotFound();

                //return controller.Ok(value);
                return value;
            }
            catch (Exception e)
            {
                return controller.HandleServerError<T>(e);
            }
        }
    }
}
