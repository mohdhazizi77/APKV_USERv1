<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="calon.ulang.vokasional.aspx.vb" Inherits="kpmkv.calon_ulang_vokasional" %>
<%@ Register src="commoncontrol/pelajar_ulang_create.ascx" tagname="pelajar_ulang_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pelajar_ulang_create ID="pelajar_ulang_create1" runat="server" />
</asp:Content>
