using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMirror
{
    public partial class GetContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["t"];
            if (!string.IsNullOrEmpty(type))
            {
                string id = Request.Form["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    //javascript
                    if (id.ToLower().Contains(".min."))
                        return;
                    //css
                    if (id.ToLower().EndsWith(".eot")||
                        id.ToLower().EndsWith(".ttf")||
                        id.ToLower().EndsWith(".woff"))
                        return;

                    string fileGet = string.Empty;
                    if (!id.StartsWith("/"))
                        id = "/" + id;                    
                    
                    if (type.ToLower().StartsWith("js"))
                        fileGet = Server.MapPath("~/" + FileHelper.folderJSName + id);
                    else if (type.ToLower().StartsWith("css"))
                        fileGet = Server.MapPath("~/" + FileHelper.folderCSSName + id);
                    if (string.IsNullOrEmpty(fileGet))
                        return;

                    if (File.Exists(fileGet))
                    {
                        try
                        {
                            //Response.Write(Regex.Replace(File.ReadAllText(fileGet), @"\t|\n|\r", ""));
                            Response.Write(File.ReadAllText(fileGet));
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}