using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace FakeApp.Api.Auth.Actions
{
    /// <summary>
    /// Класс, обрабатывающий ошибки валидации данных при запросах post и put
    /// </summary>
    public class ValidationAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var errorsList = new List<string>();
            
            if (!context.ModelState.IsValid) 
            {
                foreach (var value in context.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorsList.Add(error.ErrorMessage);
                    }
                    
                }
                context.Result = new BadRequestObjectResult(new {errors = errorsList});
            }
        }
    }
}