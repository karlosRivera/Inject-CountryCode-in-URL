<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InjectCountryCodeInURL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<br /><br />
<asp:HyperLink ID="hphome" NavigateUrl="~/" runat="server">Home</asp:HyperLink>
<asp:HyperLink ID="hpabout" NavigateUrl="~/About.aspx" runat="server">About</asp:HyperLink>
<asp:HyperLink ID="hpcontact" NavigateUrl="~/Contact.aspx" runat="server">Contact</asp:HyperLink>
<br /><br /><br /><br />

    <asp:Button ID="Button1" runat="server" Text="Button" />


    <script>

        $(document).ready(function () {
            $("[id$=Button1]").click(function () {
                var url = 'http://localhost:53741/gb/default.aspx';
                var segments = url.split('/');
                var first = segments[1];
                var second = segments[2];
                var countrycode = segments[3];
                alert(first);
                alert(second);
                alert(countrycode);

                $('a').each(function (index) {
                    alert($(this).href());
                });

                return false;
            });
        });
    </script>
</asp:Content>


