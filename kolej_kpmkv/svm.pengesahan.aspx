<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="svm.pengesahan.aspx.vb" Inherits="kpmkv.svm_pengesahan" %>

<%@ Register Src="~/commoncontrol/svm_pengesahan.ascx" TagPrefix="uc1" TagName="svm_pengesahan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svm_pengesahan runat="server" id="svm_pengesahan" />
</asp:Content>
