<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="slip_keputusan_sejarah1251.ascx.vb" Inherits="kpmkv.slip_keputusan_sejarah12511" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Slip Keputusan Sejarah 1251</td>
    </tr>
    <tr>
        <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">Tahun Peperiksaan:</td>
        <td>
            <asp:DropDownList ID="ddlTahunPeperiksaan" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">Sesi Pengambilan:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="349px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Tetapan Tarikh</td>
    </tr>
    <tr style="text-align: center;">
           <td style="width: 10%;">Hari:
             <asp:DropDownList ID="ddlHari" runat="server" AutoPostBack="false" >
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
                 </asp:DropDownList>
                 </td>
        <td style="width: 10%;">Bulan:
            <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="false" >
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                </td>
        <td style="width: 10%;">Tahun:
         <asp:DropDownList ID="ddlTahun_1" runat="server" AutoPostBack="false" Width="53px" Height="17px">
         </asp:DropDownList>
        </td>
    </tr>

    <tr>
         <td style="text-align: center;" colspan="3"><asp:Button ID="btnPrint" runat="server" Text="Cetak Slip Keputusan" CssClass="fbbutton"/>&nbsp;<asp:HyperLink ID="hyPDF" runat="server" Target="_blank"
                    Visible="false">Klik disini untuk muat turun.</asp:HyperLink> </td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>