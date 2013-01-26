﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tune.ascx.cs" Inherits="web.tune" %>

<link href="css/layout.css" rel="stylesheet" />

<br/>
<asp:Button ID="tuneUp" runat="server" Text="Tune Up" CssClass="button1" OnClick="tuneUp_Click" meta:resourcekey="tuneUpResource1" /> 
<asp:Button ID="tuneDown" runat="server" Text="Tune Down" CssClass="button1" OnClick="tuneDown_Click" meta:resourcekey="tuneDownResource1" /> 
<asp:Button ID="tuneReset" runat="server" Text="Tune Reset" CssClass="button1" Visible="False" meta:resourcekey="tuneResetResource1" /> 