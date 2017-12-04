<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InjectCountryCodeInURL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br />
<asp:HyperLink ID="hphome" NavigateUrl="/" runat="server">Home</asp:HyperLink>
<asp:HyperLink ID="hpabout" NavigateUrl="/About.aspx" runat="server">About</asp:HyperLink>
<asp:HyperLink ID="hpcontact" NavigateUrl="/Contact.aspx" runat="server">Contact</asp:HyperLink>
<br /><br /><br /><br />

    <asp:Button ID="Button1" runat="server" Text="Button" />

<br /><br />

<asp:label id="lbltext" runat="server"><asp:Literal runat="server" Text="<%$Resources:LocalisedText, Welcome %>" /></asp:label>

</asp:Content>


