<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="markah.akademik.ulang.bm.aspx.vb" Inherits="kpmkv.markah_akademik_ulang_bm" %>
<%@ Register src="commoncontrol/markah_ulang_akademik_bm.ascx" tagname="markah_ulang_akademik_bm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:markah_ulang_akademik_bm ID="markah_ulang_akademik_bm" runat="server" />
</asp:Content>
