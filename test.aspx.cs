using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMirror
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<FolderItem> lst = FileHelper.BuildListFile(FileHelper.folderJSName);
            if (lst != null && lst.Count > 0)
                Response.Write(JsonConvert.SerializeObject(lst));
        }
    }
}