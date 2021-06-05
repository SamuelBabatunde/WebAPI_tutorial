using System.Web.Http.Routing.Constraints;

namespace WepApi_1.Constraints
{
    public class Base64Constraint : RegexRouteConstraint
    {
        public Base64Constraint() : base("^([A-Za-z0-9+/\\-_])*={0,3}$")
        {

        }
    }
}