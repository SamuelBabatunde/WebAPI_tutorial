using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WepApi_1.Models.CustomModelBinder
{
    public class StringArrayWildCardBinder : IModelBinder
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
                        //parse the elements on the forward slash
                        var array = s.Split('/');
                        bindingContext.Model = array;
                    }
                    catch (Exception ex)
                    {
                        //binding failed
                        return false;
                    }
                }
                return true;

            }
            //binding failed
            return false;
        }
    }
}