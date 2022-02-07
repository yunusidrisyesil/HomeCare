using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Extensions
{
    public static class AppExtensions
    {
        public static string ToFullErrorString(this ModelStateDictionary modelState)
        {

            var message = new List<string>();
            foreach(var entry in modelState.Values)
            {
                foreach (var error in entry.Errors)
                    message.Add(error.ErrorMessage);
            }


            return string.Join(" ", message);
        }
    }
}
