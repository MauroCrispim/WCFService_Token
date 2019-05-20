using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SecuredTokenWebservice
/// </summary>
public class SecuredTokenWebservice : System.Web.Services.Protocols.SoapHeader
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AuthenticationToken { get; set; }

    public bool IsUserCredentialsValid(string UserName, string Password)
    {
        //database connection to validate the User
        if (UserName == "admin" && Password == "admin")
            return true;
        else
            return false;
    }

    public bool IsUserCredentialsValid(SecuredTokenWebservice SoapHeader)
    {
        if (SoapHeader == null)
            return false;

        // check the token exists in Cache
        if (!string.IsNullOrEmpty(SoapHeader.AuthenticationToken))
            return (HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

        return false;
    }
}