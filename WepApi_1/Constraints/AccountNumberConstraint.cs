using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WepApi_1.Constraints
{
    public class AccountNumberConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                var accountNumber = value as string;

                return IsValidAccount(accountNumber);
            }

            return false;
        }


        public static bool IsValidAccount(string sAccount)
        {
            return (!String.IsNullOrEmpty(sAccount) &&
                sAccount.StartsWith("1234") &&
                sAccount.Length > 5);
        }
    }
}