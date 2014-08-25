using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeMirror
{
    public partial class Controls_Files : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrCss.Text = FileHelper.AllCss;
            ltrJs.Text = FileHelper.AllJs;
        }
    }
}