<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_update.ascx.vb"
    Inherits="kpmkv.pelajar_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian Dan Penyelengaraan >> Calon >> Kemaskini Maklumat Calon</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Calon.</td>
    </tr>
    <tr><td  colspan="2"></td></tr>
     <tr>
         <td style="width: 20%;">Peringkat Pengajian:</td>
        <td><asp:Label ID="lblPengajian" runat="server"></asp:Label></td>
    </tr>
     <tr>
         <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
         <td style="width: 20%;">Kohort:</td>
         <td><asp:Label ID="lblTahun" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td style="width: 20%;">Sesi Pengambilan:</td>
         <td>
            <asp:Label ID="lblSesi" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td style="width: 20%;">Nama Bidang:</td>
         <td>
            <asp:Label ID="lblKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td style="width: 20%;">Kod Program:</td>
         <td>
            <asp:Label ID="lblKodKursus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
          <td style="width: 20%;">Nama Program:</td>
         <td>
            <asp:Label ID="lblNamaKursus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td style="width: 20%;">Nama Kelas:</td>
         <td>
            <asp:Label ID="lblNamaKelas" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td></td>
    </tr>
     <tr>
          <td style="width: 20%;">Nama Calon: </td>
         <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
             <asp:Label ID="lblNamaPelajar" runat="server" Visible ="false" ></asp:Label>
        </td>
    </tr>
    <tr>
          <td style="width: 20%;">Mykad:</td>
         <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
              <asp:Label ID="lblMykad" runat="server" Visible ="false" ></asp:Label>
        </td>
    </tr>
     <tr>
          <td style="width: 20%;">AngkaGiliran:</td>
         <td>
            <asp:Label ID="lblAngkaGiliran" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td style="width: 20%;">Jantina:</td>
         <td><asp:CheckBoxList ID="chkJantina" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>LELAKI</asp:ListItem>
             <asp:ListItem>PEREMPUAN</asp:ListItem>
             </asp:CheckBoxList>   
    </tr>
    <tr>
          <td style="width: 20%;">Kaum:</td>
         <td>
            <asp:DropDownList ID="ddlKaum" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td style="width: 20%;">Agama:</td>
         <td><asp:CheckBoxList ID="chkAgama" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>ISLAM</asp:ListItem>
             <asp:ListItem>LAIN-LAIN</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr>
         <td style="width: 20%;">Emel:</td>
         <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
          <td style="width: 20%;">Status Calon:</td>
         <td><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
</td>
    </tr>
    <tr>
          <td style="width: 20%;">Jenis Calon:</td>
         <td><asp:DropDownList ID="ddlJenisCalon" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
</td>
    </tr>

    <tr>
          <td style="width: 20%;">Catatan:</td>
         <td>
            <asp:Textbox ID="txtCatatan" runat="server" Width="460px" Height="117px" TextMode="MultiLine"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp; <asp:Button ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;

        </td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>


