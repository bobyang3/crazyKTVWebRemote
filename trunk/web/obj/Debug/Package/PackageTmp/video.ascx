﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="video.ascx.cs" Inherits="web.video" %>
<link href="css/layout.css" rel="stylesheet" />
    <br />
    
<asp:Button ID="Pause" runat="server" Text="Pause" CssClass="button1" OnClick="Pause_Click" meta:resourcekey="PauseResource1" /> 
<asp:Button ID="Play" runat="server" Text="Play" CssClass="button1" meta:resourcekey="PlayResource1" /> 
<asp:Button ID="Cut" runat="server" Text="Cut" CssClass="button1" OnClick="Cut_Click" meta:resourcekey="CutResource1" /> 
<asp:Button ID="Repeat" runat="server" Text="Repeat" CssClass="button1" OnClick="Repeat_Click" meta:resourcekey="RepeatResource1" /> 
<asp:Button ID="Restart" runat="server" Text="Restart" CssClass="button1" OnClick="Restart_Click" meta:resourcekey="RestartResource1" /> 
<asp:Button ID="FastFoward" runat="server" Text="Fast Foward"  CssClass="button1" meta:resourcekey="FastFowardResource1" /> 
<asp:Button ID="FastBackward" runat="server" Text="Fast Backward" CssClass="button1" meta:resourcekey="FastBackwardResource1" /> 
<asp:Button ID="Mute" runat="server" Text="Mute" CssClass="button1" OnClick="Mute_Click" meta:resourcekey="MuteResource1" /> 
<asp:Button ID="Channel" runat="server" Text="Channel" CssClass="button1" OnClick="Channel_Click" meta:resourcekey="ChannelResource1" /> 
<asp:Button ID="FixedChannel" runat="server" Text="Fixed Channel" CssClass="button1" OnClick="FixedChannel_Click" meta:resourcekey="FixedChannelResource1" /> 
 <br />