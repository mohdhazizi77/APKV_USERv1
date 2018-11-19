<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_akademik_setara_pengesahan.aspx.vb" Inherits="kpmkv.admin_akademik_setara_pengesahan" %>
<%@ Register src="commoncontrol/akademik_setara_list.ascx" tagname="akademik_setara_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:akademik_setara_list ID="akademik_setara_list1" runat="server" />
</asp:Content>
