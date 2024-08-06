using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stories.API.ExceptionHandling;
using static Stories.API.ExceptionHandling.ValidationResponse;
using Stories.Domain.Validation;

namespace Stories.API
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                foreach (var parameter in descriptor.MethodInfo.GetParameters())
                {
                    object args = null;
                    if (context.ActionArguments.ContainsKey(parameter.Name))
                    {
                        args = context.ActionArguments[parameter.Name];
                    }

                    ValidateAttributes(parameter, args, context.ModelState);
                }
            }
           
            var errorList = new List<string>();
            context.ModelState.ToList().ForEach(v =>
            {
                v.Value!.Errors.ToList().ForEach(e =>
                {
                    errorList.Add(e.ErrorMessage);
                });
            });

            var validationMessages = new List<Message>();
            
            errorList.ForEach(errorItem =>
            {
                var msg = new Message(new Domain.Validation.ValidationMessage(ValidationCategory.Invalid,
                                            "Invalid Model", errorItem, null));
                validationMessages.Add(msg);
            });
            
            var validationResponse = new ValidationResponse(validationMessages,"");
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(validationResponse);
            }
        }

        private void ValidateAttributes(ParameterInfo parameter, object args, ModelStateDictionary modelState)
        {
            foreach (var attributeData in parameter.CustomAttributes)
            {
                var attributeInstance = parameter.GetCustomAttribute(attributeData.AttributeType);

                if (attributeInstance is ValidationAttribute validationAttribute)
                {
                    var isValid = validationAttribute.IsValid(args);
                    if (!isValid)
                    {
                        modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
                    }
                }
            }
        }
    }
}

