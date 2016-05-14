using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public static class MvcExtensions
    {
        public static Dictionary<string, string[]> ToJson(this ModelStateDictionary modelState)
        {
            var jsonModel = new Dictionary<string, string[]>();

            foreach (var field in modelState.Keys)
            {
                if (!modelState.IsValidField(field))
                {
                    jsonModel.Add(field, new string[modelState[field].Errors.Count]);
                    var i = 0;

                    foreach (var error in modelState[field].Errors.Select(e => e.ErrorMessage))
                    {
                        jsonModel[field][i++] = error;
                    }
                }
            }

            return jsonModel;
        }
    }
}