using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GMMT.Models
{
    public class TokenAuthentication : ActionFilterAttribute
    {
        //Your Properties in Action Filter
        public string Token { get; set; }

        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    try
        //    {

        //        var args = actionContext.ActionArguments;
        //        Guid clientToken;
        //        UserDetails details = null;
        //        string argsEmail = args["emailId"].ToString();

        //        if (args["clientToken"]!=null)
        //        {
        //            clientToken = (Guid)args["clientToken"];
        //            // string argsToken = args["Token"].ToString();

        //            using (var context = new TurfDbContext())
        //            {
        //                // Query to extract user info 
        //                details = context.UserDetails
        //                               .Where(b => b.EmailId == argsEmail && b.Token.ToString() == clientToken.ToString())
        //                               .FirstOrDefault();
        //                if (details != null)
        //                {
        //                    return;
        //                }
        //            }
        //        }

        //        Guid token;
        //        token = Guid.NewGuid();
        //        actionContext.ActionArguments["clientToken"] = token;

        //        using (var context = new TurfDbContext())
        //        {
        //            details = context.UserDetails.Where(b => b.EmailId == argsEmail).FirstOrDefault();
        //        }
        //        if (details != null)
        //        {
        //            details.Token = token;
        //        }

        //        //******** code to update database ********
        //        using (var context = new TurfDbContext())
        //        {
        //            context.Entry(details).State = System.Data.Entity.EntityState.Modified;
        //            context.SaveChanges();
        //        }

        //        //base.OnActionExecuting(actionContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }

}