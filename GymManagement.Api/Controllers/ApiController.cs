﻿using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata.Ecma335;

namespace GymManagment.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if(errors.Count is 0)
            {
                return Problem();
            }

            if(errors.All(error => error.Type == ErrorType.Validation)) //if all are validarion erros
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        protected IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, detail: error.Description);
        }

        protected IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictonary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictonary.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelStateDictonary);
        }
    }
}
