using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WepApi_1.Models.CustomModelBinder
{
    public class Base64ModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(key);
            if (value != null)
            {
                var s = value.AttemptedValue;
                if (s != null)
                {
                    try
                    {
                        //support "modified base64" where + was replaced with -, and / replaced _
                        //since this is a URL parameter. Caller can URL-encoded as an alternative
                        s = s.Replace('-', '+').Replace('_', '/');

                        int mod4 = s.Length % 4;
                        if (mod4 > 0)
                        {
                            s += new string('=', 4 - mod4);
                        }

                        var array = Convert.FromBase64String(s);

                        bindingContext.Model = array;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
                return true;

            }

            return false;
        }
    }
}