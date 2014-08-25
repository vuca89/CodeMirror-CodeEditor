using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeMirror
{
    public partial class JavascriptCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.Page.Header.Title = "Javascript editor - Code mirror";
        }
    }
}