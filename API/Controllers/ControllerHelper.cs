using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http.ModelBinding;

namespace API.Controllers
{
    public static class ControllerHelper
    {
        public static string GetModelStateErrorMessages(ModelStateDictionary modelState)
        {
            return string.Join(" | ", modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
        }

        public static string GetModelStateErrorExceptions(ModelStateDictionary modelState)
        {
            return string.Join(" | ", modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.Exception));
        }

        public static bool IsValidEmail(string emailAddress)
        {
            var emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                             + "@"
                             + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            return Regex.IsMatch(emailAddress, emailRegex);
        }
    }
}