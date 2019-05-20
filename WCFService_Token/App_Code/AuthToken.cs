using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AuthToken
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AuthToken : System.Web.Services.WebService {

    public SecuredTokenWebservice SoapHeader;

    [WebMethod]
    [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
    public string AuthenticationUser()
    {
        if (SoapHeader == null)
            return "Please provide Username and Password";
        if (string.IsNullOrEmpty(SoapHeader.UserName) || string.IsNullOrEmpty(SoapHeader.Password))
            return "Please provide Username and Password";

        //Check is User credentials Valid
        if (!SoapHeader.IsUserCredentialsValid(SoapHeader.UserName,SoapHeader.Password))
            return "Invalid Username or Password";

        // Create and store the AuthenticatedToken before returning it
        string token = Guid.NewGuid().ToString();
        HttpRuntime.Cache.Add(
            token,
            SoapHeader.UserName,
            null,
            System.Web.Caching.Cache.NoAbsoluteExpiration,
            TimeSpan.FromMinutes(30),
            System.Web.Caching.CacheItemPriority.NotRemovable,
            null
            );

        return token;
    }

    [WebMethod]
    [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
    public string HelloWorld() 
    {
        if (SoapHeader == null)
            return "Please call AuthenticationMethod() first.";

        if(!SoapHeader.IsUserCredentialsValid(SoapHeader))
            return "Please call AuthenticationMethod() first.";

        return "Hello " + HttpRuntime.Cache[SoapHeader.AuthenticationToken];
    }
    
}
