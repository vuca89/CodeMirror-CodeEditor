<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Files.ascx.cs" Inherits="CodeMirror.Controls_Files" %>

<div class="tree" id="tree">
    <ul>                    
        <li><span title="Expand" class="btn-success"><i class="glyphicon glyphicon-folder-open"></i>Html design</span></li>
        <li><span title="Expand" class="btn-primary"><i class="glyphicon glyphicon-folder-open"></i>Control design</span></li>
        <li id="jsfile"><span title="Expand" class="btn-info"><i class="glyphicon glyphicon-folder-open"></i>Javascript</span>
            <ul><asp:Literal ID="ltrJs" runat="server"></asp:Literal></ul>
        </li>
        <li id="cssfile"><span title="Expand" class="btn-warning"><i class="glyphicon glyphicon-folder-open"></i>Css</span>
            <ul><asp:Literal ID="ltrCss" runat="server"></asp:Literal></ul>
        </li>
    </ul>
</div>