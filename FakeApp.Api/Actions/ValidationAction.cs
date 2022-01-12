using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace FakeApp.Api.Actions
{
    /// <summary>
    /// Класс, обрабатывающий ошибки валидации данных при запросах post и put
    /// </summary>
    public class ValidationAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var errorsList = new List<string>();

            if (context.ModelState.IsValid) return;
            
            foreach (var value in context.ModelState.Values)
            {
                errorsList.AddRange(value.Errors.Select(error => error.ErrorMessage));
            }
            
            context.Result = new BadRequestObjectResult(new {errors = errorsList});
        }
    }
}