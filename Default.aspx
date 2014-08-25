<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"
     EnableViewState="false" Inherits="CodeMirror._Default" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="cpHead" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="cpMain" runat="server">
    Welcome!
    <br />
    <span>Type editor demo</span>
    <br/>
    <a class="btn btn-info btn-sm" target="_blank" href="JavascriptCode.aspx">JavascriptCode.aspx</a>
    <a class="btn btn-warning btn-sm" target="_blank" href="CssCode.aspx">CssCode.aspx</a>
    <br /><br />
    <span>Mix editor demo</span>
    <br/>
    <a class="btn btn-success btn-sm" target="_blank" href="HtmlCode.aspx">HtmlCode.aspx</a>
    <a class="btn btn-danger btn-sm" target="_blank" href="MixCode.aspx">MixCode.aspx</a>
</asp:Content>

<asp:Content ContentPlaceHolderID="cpBottom" runat="server">

</asp:Content>