<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svm_pengesahan.ascx.vb" Inherits="kpmkv.svm_pengesahan1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2" style="text-align: center">Sijil Vokasional Malaysia
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="NAMA"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblNama" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="NO. KAD PENGENALAN"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblMykad" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="ANGKA GILIRAN"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblAG" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="INSTITUSI"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblInstitusi" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="KLUSTER"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="KURSUS"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblKursus" runat="server"></asp:Label>
        </td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="BAHASA MELAYU KOLEJ VOKASIONAL 1104"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblBM" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblKompeten" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="PURATA NILAI GRED KUMULATIF AKADEMIK (PNGKA)"></asp:Label>
        </td>
        <td style="width: 100px;">
            <asp:Label ID="lblPNGKA" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="PURATA NILAI GRED KUMULATIF VOKASIONAL (PNGKV)"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPNGKV" runat="server"></asp:Label>
        </td>
    </tr>
</table>

<br />

<div id="tblSetara" runat="server">
    <table class="fbform">
        <tr>
            <td style="width: 550px;">
                <asp:Label ID="Label10" runat="server" Text="Lembaga Peperiksaan memperakukan bahawa calon yang namanya tersebut"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 550px;">
                <asp:Label ID="Label12" runat="server" Text="di atas ini telah dianugerahkan Sijil Vokasional Malaysia yang setara dengan"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 550px;">
                <asp:Label ID="Label13" runat="server" Text="3 kredit Sijil Pelajaran Malaysia."></asp:Label>
            </td>
        </tr>
    </table>
</div>

<br />
<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Untuk tujuan pengesahan Sijil Vokasional Malaysia."></asp:Label>
</div>
