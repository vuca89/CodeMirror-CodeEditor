using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMirror
{
    public partial class SaveContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["t"];
            if (!string.IsNullOrEmpty(type))
            {
                string id = Request.Form["id"];
                string newName = Request.Form["name"];
                if (string.IsNullOrEmpty(newName))
                {
                    string content = Request.Form["data"];
                    if (!string.IsNullOrEmpty(id))
                    {
                        Response.Write(FileHelper.SaveFile(id, type, content));
                        return;
                    }
                }
                else {
                    Response.Write(FileHelper.RenameFile(id, type, newName));
                    return;
                }
            }

            Response.Write("False");
        }
    }
}