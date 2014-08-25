﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false"
    CodeFile="CssCode.aspx.cs" Inherits="CodeMirror.CssCode" MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/Files.ascx" TagPrefix="uc1" TagName="Files" %>
<%@ Register Src="~/Controls/Buttons.ascx" TagPrefix="uc1" TagName="Buttons" %>

<asp:Content ContentPlaceHolderID="cpHead" runat="server">
    <link href="css/code-mirror/codemirror.css" rel="stylesheet" />
    <link href="js/addon-codemirror/fold/foldgutter.css" rel="stylesheet" />
    <link href="js/addon-codemirror/hint/show-hint.css" rel="stylesheet" />
    <link href="css/code-mirror/blackboard.css" rel="stylesheet" />
    <link href="js/addon-codemirror/display/fullscreen.css" rel="stylesheet" />
    <link href="js/addon-codemirror/lint/lint.css" rel="stylesheet" />
    <link href="js/addon-codemirror/dialog/dialog.css" rel="stylesheet" />

    <script src="js/libs/codemirror.js"></script>
    <script src="js/addon-codemirror/xml.js"></script>
    <script src="js/addon-codemirror/css.js"></script>
    <script src="js/addon-codemirror/selection/active-line.js"></script>
    <script src="js/addon-codemirror/fold/foldcode.js"></script>
    <script src="js/addon-codemirror/fold/foldgutter.js"></script>
    <script src="js/addon-codemirror/fold/brace-fold.js"></script>
    <script src="js/addon-codemirror/fold/comment-fold.js"></script>
    <script src="js/addon-codemirror/edit/closebrackets.js"></script>
    <script src="js/addon-codemirror/edit/matchbrackets.js"></script>
    <script src="js/addon-codemirror/hint/show-hint.js"></script>
    <script src="js/addon-codemirror/hint/css-hint.js"></script>
    <script src="js/addon-codemirror/display/fullscreen.js"></script>

    <script src="js/addon-codemirror/lint/csslint.js"></script>
    <script src="js/addon-codemirror/lint/css-lint.js"></script>
    <script src="js/addon-codemirror/lint/lint.js"></script>

    <script src="js/addon-codemirror/dialog/dialog.js"></script>
    <script src="js/addon-codemirror/search/search.js"></script>
    <script src="js/addon-codemirror/search/searchcursor.js"></script>
    <script src="js/addon-codemirror/search/match-highlighter.js"></script>
    <style type="text/css">
     
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="fileHolder" runat="server">
    <uc1:Files runat="server" ID="Files" />
</asp:Content>

<asp:Content ContentPlaceHolderID="buttonHolder" runat="server">
    <uc1:Buttons runat="server" ID="Buttons" />
</asp:Content>

<asp:Content ContentPlaceHolderID="cpMain" runat="server">
    <textarea id="code" runat="server" name="code"></textarea>    
    <script src="js/file/css.js"></script>
</asp:Content>
