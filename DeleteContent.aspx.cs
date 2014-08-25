using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMirror
{
    public partial class DeleteContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["t"];
            if (!string.IsNullOrEmpty(type))
            {
                string id = Request.Form["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    Response.Write(FileHelper.DeleteFile(id, type));
                    return;
                }
            }
            Response.Write("False");
        }
    }
}