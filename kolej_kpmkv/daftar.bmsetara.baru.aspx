﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="daftar.bmsetara.baru.aspx.vb" Inherits="kpmkv.daftar_bmsetara_baru" %>
<%@ Register src="commoncontrol/bmsetara_calon_baru_daftar.ascx" tagname="bmsetara_calon_baru_daftar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:bmsetara_calon_baru_daftar ID="bmsetara_calon_baru_daftar1" runat="server" />
</asp:Content>
