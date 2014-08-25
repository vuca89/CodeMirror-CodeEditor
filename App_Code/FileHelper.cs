using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for FileHelper
/// </summary>
public class FileHelper
{
    public FileHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    static Dictionary<string, List<string>> FolderLevel;
    public const string folderJSName = "js";
    public const string folderCSSName = "css";

    #region string

    public static string BuildFile(string folderName)
    {
        string physicalPath = HttpContext.Current.Server.MapPath("~/" + folderName);

        #region build

        string[] allFileInFolder = Directory.GetFiles(physicalPath, "*", SearchOption.AllDirectories);
        if (allFileInFolder != null && allFileInFolder.Length > 0)
        {
            FolderLevel = new Dictionary<string, List<string>>();
            string key = string.Empty;
            int maxLevel = 0;
            foreach (string item in allFileInFolder)
            {
                key = folderName;

                string[] arr = item.Replace(physicalPath + "\\", "").Split('\\');
                if (arr.Length > 0)
                {
                    if (arr.Length > maxLevel)
                        maxLevel = arr.Length;
                    for (int i = 0, j = arr.Length; i < j; i++)
                    {
                        //last
                        if (i == j - 1) { }
                        else
                            key += "/" + arr[i];
                    }
                }

                if (FolderLevel.ContainsKey(key))
                {
                    if (FolderLevel[key] == null)
                        FolderLevel[key] = new List<string>();
                    FolderLevel[key].Add(arr[arr.Length - 1]);
                }
                else
                    FolderLevel.Add(key, new List<string>() { arr[arr.Length - 1] });
            }

            if (FolderLevel.Count > 0)
                return BuildFolderLevel(folderName, folderName);
        }
        #endregion

        return string.Empty;
    }

    static string BuildFolderLevel(string key, string folderName)
    {
        StringBuilder sbReturn = new StringBuilder();
        if (!string.IsNullOrEmpty(key))
        {
            if (key.ToLower() != folderName.ToLower())
            {
                sbReturn.Append("<li style='display: none;' datapath='" + key.Substring(folderName.Length) +
                           "'><span><i class='glyphicon glyphicon-folder-open'></i>" + Path.GetFileName(key) + "</span>");
            }
            bool hasChild = false;
            //find folder
            var childFolder = FolderLevel.Where(x => (x.Key.StartsWith(key) &&
                x.Key.Split('/').Length == (key.Split('/').Length + 1)));
            if (childFolder != null && childFolder.Count() > 0)
            {
                if (key.ToLower() != folderName.ToLower())
                    sbReturn.Append("<ul>");

                hasChild = true;
                foreach (var item in childFolder)
                {
                    sbReturn.Append(BuildFolderLevel(item.Key, folderName));
                }
            }

            List<string> allFile = FolderLevel[key];
            if (allFile != null && allFile.Count > 0)
            {
                if (hasChild == false && (key.ToLower() != folderName.ToLower()))
                {
                    hasChild = true;
                    sbReturn.Append("<ul>");
                }
                foreach (string item in allFile)
                {
                    sbReturn.Append("<li style='display: none;'><a class='item' href='javascript:void(0);'>" + Path.GetFileName(item) + "</a></li>");
                }
            }

            if (hasChild && (key.ToLower() != folderName.ToLower()))
                sbReturn.Append("</ul>");

            if (key.ToLower() != folderName.ToLower())
                sbReturn.Append("</li>");
        }

        return sbReturn.ToString();
    }

    #endregion

    #region for recruit list

    public static List<FolderItem> BuildListFile(string folderName)
    {
        string physicalPath = HttpContext.Current.Server.MapPath("~/" + folderName);

        #region build

        string[] allFileInFolder = Directory.GetFiles(physicalPath, "*", SearchOption.AllDirectories);
        if (allFileInFolder != null && allFileInFolder.Length > 0)
        {
            FolderLevel = new Dictionary<string, List<string>>();
            string key = string.Empty;
            int maxLevel = 0;
            foreach (string item in allFileInFolder)
            {
                key = folderName;

                string[] arr = item.Replace(physicalPath + "\\", "").Split('\\');
                if (arr.Length > 0)
                {
                    if (arr.Length > maxLevel)
                        maxLevel = arr.Length;
                    for (int i = 0, j = arr.Length; i < j; i++)
                    {
                        //last
                        if (i == j - 1) { }
                        else
                            key += "/" + arr[i];
                    }
                }

                if (FolderLevel.ContainsKey(key))
                {
                    if (FolderLevel[key] == null)
                        FolderLevel[key] = new List<string>();
                    FolderLevel[key].Add(arr[arr.Length - 1]);
                }
                else
                    FolderLevel.Add(key, new List<string>() { arr[arr.Length - 1] });
            }

            if (FolderLevel.Count > 0)
                return BuildListFolderItem(folderName, folderName);
        }
        #endregion

        return null;
    }

    public static List<FolderItem> BuildListFolderItem(string key, string folderName)
    {
        List<FolderItem> lstReturn = new List<FolderItem>();
        FolderItem fi = null;
        if (!string.IsNullOrEmpty(key))
        {
            if (key.ToLower() != folderName.ToLower())
            {
                fi = new FolderItem();
                fi.path = key;
            }
            bool hasChild = false;
            //find folder
            var childFolder = FolderLevel.Where(x => (x.Key.StartsWith(key) &&
                x.Key.Split('/').Length == (key.Split('/').Length + 1)));
            if (childFolder != null && childFolder.Count() > 0)
            {
                if (key.ToLower() != folderName.ToLower())
                {
                    if (fi != null)
                        fi.children = new List<FolderItem>();
                }

                hasChild = true;
                foreach (var item in childFolder)
                {
                    FolderItem fichild = new FolderItem();
                    fichild.path = item.Key;
                    fichild.children = new List<FolderItem>();
                    fichild.children.AddRange(BuildListFolderItem(item.Key, folderName));
                    lstReturn.Add(fichild);
                }
            }

            List<string> allFile = FolderLevel[key];
            if (allFile != null && allFile.Count > 0)
            {
                foreach (string item in allFile)
                    lstReturn.Add(new FolderItem() { path = Path.GetFileName(item) });
            }
        }

        return lstReturn;
    }

    #endregion

    public static string AllCss
    {
        get
        {
            if (HttpContext.Current.Session["CodeMirror-AllCss"] != null)
                return HttpContext.Current.Session["CodeMirror-AllCss"] as string;
            else
            {
                string allCss = FileHelper.BuildFile(FileHelper.folderCSSName);
                HttpContext.Current.Session["CodeMirror-AllCss"] = allCss;
                return allCss;
            }
        }
        set
        {
            HttpContext.Current.Session["CodeMirror-AllCss"] = value;
        }
    }

    public static string AllJs
    {
        get
        {
            if (HttpContext.Current.Session["CodeMirror-AllJs"] != null)
                return HttpContext.Current.Session["CodeMirror-AllJs"] as string;
            else
            {
                string allJs = FileHelper.BuildFile(FileHelper.folderJSName);
                HttpContext.Current.Session["CodeMirror-AllJs"] = allJs;
                return allJs;
            }
        }
        set
        {
            HttpContext.Current.Session["CodeMirror-AllJs"] = value;
        }
    }

    public static bool SaveFile(string id, string type, string content)
    {
        string fileGet = string.Empty;
        if (!id.StartsWith("/"))
            id = "/" + id;
        if (type.ToLower().StartsWith("js"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderJSName + id);
        else if (type.ToLower().StartsWith("css"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderCSSName + id);
        if (string.IsNullOrEmpty(fileGet))
            return false;

        try
        {
            if (File.Exists(fileGet))
            {
                // create a writer and open the file
                TextWriter tw = new StreamWriter(fileGet, false, System.Text.Encoding.UTF8);
                // write a line of text to the file
                tw.Write(HttpUtility.HtmlDecode(content));
                // close the stream
                tw.Close();

            }
            else
            {
                using (FileStream f = new FileStream(fileGet, FileMode.Create, FileAccess.Write))
                using (StreamWriter s = new StreamWriter(f))
                    s.Write(HttpUtility.HtmlDecode(content));

                if (type.ToLower().StartsWith("js"))
                    HttpContext.Current.Session.Remove("CodeMirror-AllJs");
                else if (type.ToLower().StartsWith("css"))
                    HttpContext.Current.Session.Remove("CodeMirror-AllCss");
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool RenameFile(string id, string type, string newName)
    {
        string fileGet = string.Empty;
        if (!id.StartsWith("/"))
            id = "/" + id;
        if (type.ToLower().StartsWith("js"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderJSName + id);
        else if (type.ToLower().StartsWith("css"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderCSSName + id);
        if (string.IsNullOrEmpty(fileGet))
            return false;

        try
        {
            if (File.Exists(fileGet))
            {
                newName = newName.Replace("/", string.Empty).Replace("\\", string.Empty);

                newName = Path.GetFileNameWithoutExtension(newName);
                if (!newName.StartsWith("/"))
                    newName = "/" + newName;
                if (type.ToLower().StartsWith("js")) {
                    if (!newName.EndsWith(".js"))
                        newName = newName + ".js";
                }
                else if (type.ToLower().StartsWith("css"))
                {
                    if (!newName.EndsWith(".css"))
                        newName = newName + ".css";
                }
                File.Move(fileGet, Path.GetDirectoryName(fileGet) + newName);

                if (type.ToLower().StartsWith("js"))
                    HttpContext.Current.Session.Remove("CodeMirror-AllJs");
                else if (type.ToLower().StartsWith("css"))
                    HttpContext.Current.Session.Remove("CodeMirror-AllCss");
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool DeleteFile(string id, string type)
    {
        string fileGet = string.Empty;
        if (!id.StartsWith("/"))
            id = "/" + id;
        if (type.ToLower().StartsWith("js"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderJSName + id);
        else if (type.ToLower().StartsWith("css"))
            fileGet = HttpContext.Current.Server.MapPath("~/" + FileHelper.folderCSSName + id);
        if (string.IsNullOrEmpty(fileGet))
            return false;

        try
        {
            if (File.Exists(fileGet))
                File.Delete(fileGet);

            if (type.ToLower().StartsWith("js"))
                HttpContext.Current.Session.Remove("CodeMirror-AllJs");
            else if (type.ToLower().StartsWith("css"))
                HttpContext.Current.Session.Remove("CodeMirror-AllCss");

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

public class FolderItem
{
    public string path { get; set; }
    public List<FolderItem> children { get; set; }
}