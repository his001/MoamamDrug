using System;
using System.Web;
using System.Web.UI;
using System.IO;

using Moamam.Lib;
using Moamam.Data.WebControls;


/// <summary>
/// Error
/// </summary>
public partial class Site_Help_Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.StatusCode = 404;
        Response.TrySkipIisCustomErrors = true;

        if (!IsPostBack)
        {

        }        
    }

    protected void Page_PreInit()
    {

    }
}