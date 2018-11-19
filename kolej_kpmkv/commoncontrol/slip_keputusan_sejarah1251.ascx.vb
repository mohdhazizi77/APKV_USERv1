Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class slip_keputusan_sejarah12511
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'kolejnama
                strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                Dim strKolejnama As String = oCommon.getFieldValue(strSQL)
                'kolejid
                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                lblKolejID.Text = oCommon.getFieldValue(strSQL)

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kodkursus_list()

                kpmkv_tahun_1_list()
                ddlTahun_1.Text = Now.Year

                kpmkv_tahun_peperiksaan_list()


                'kpmkv_tahun_2_list()
                'ddlTahun_Semasa.Text = Now.Year

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & lblKolejID.Text & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KodKursus"
            ddlKodKursus.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub kpmkv_tahun_1_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun_1.DataSource = ds
            ddlTahun_1.DataTextField = "Tahun"
            ddlTahun_1.DataValueField = "Tahun"
            ddlTahun_1.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahun_peperiksaan_list()

        strSQL = "SELECT DISTINCT IsAKATahun FROM kpmkv_pelajar_Akademik_Ulang ORDER BY IsAKATahun ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunPeperiksaan.DataSource = ds
            ddlTahunPeperiksaan.DataTextField = "IsAKATahun"
            ddlTahunPeperiksaan.DataValueField = "IsAKATahun"
            ddlTahunPeperiksaan.DataBind()

            'ddlTahunPeperiksaan.Items.Insert(0, New ListItem("-Pilih-", "0"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    'Private Sub kpmkv_tahun_2_list()
    '    strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlTahun_Semasa.DataSource = ds
    '        ddlTahun_Semasa.DataTextField = "Tahun"
    '        ddlTahun_Semasa.DataValueField = "Tahun"
    '        ddlTahun_Semasa.DataBind()

    '    Catch ex As Exception

    '    Finally
    '        objConn.Dispose()
    '    End Try

    'End Sub
    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        countStudent()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SlipSejarah1251.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            '1'--start here
            strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
            Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

            'kolejnegeri
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
            Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)


            strSQL = "Select au.Mykad,max(au.nama), max(au.angkaGiliran),max(au.Kodkursus),"
            strSQL += " max(k.namaKursus), max(kl.namakluster),max(au.isAKATahun) "
            strSQL += " From kpmkv_pelajar_akademik_ulang As au"
            strSQL += " Left Join kpmkv_kursus as k on k.kodkursus=au.kodkursus"
            strSQL += " Left Join kpmkv_kluster as kl on k.klusterID=kl.klusterID"
            strSQL += " WHERE au.KolejRecordID ='" & lblKolejID.Text & "'"
            strSQL += " AND isCalon='1'"

            '--tahun
            If Not ddlTahun.Text = "" Then
                strSQL += " AND au.Tahun ='" & ddlTahun.Text & "' AND k.Tahun ='" & ddlTahun.Text & "'"
            End If

            If Not ddlKodKursus.SelectedValue = "" Then
                strSQL += " AND au.KodKursus='" & ddlKodKursus.SelectedValue & "'"
            End If

            If Not ddlTahunPeperiksaan.SelectedValue = "" Then
                strSQL += " AND au.IsAKATahun = '" & ddlTahunPeperiksaan.SelectedValue & "'"
            End If

            '--sesi
            If Not chkSesi.Text = "" Then
                strSQL += " AND au.Sesi ='" & chkSesi.Text & "'"
            End If
            strSQL += "GROUP BY au.mykad"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim strkey As String = ds.Tables(0).Rows(i).Item(0).ToString

                Dim strmykad As String = ds.Tables(0).Rows(i).Item(0).ToString
                Dim strname As String = ds.Tables(0).Rows(i).Item(1).ToString
                Dim strag As String = ds.Tables(0).Rows(i).Item(2).ToString
                Dim strkodKursus As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim strprogram As String = ds.Tables(0).Rows(i).Item(4).ToString
                Dim strbidang As String = ds.Tables(0).Rows(i).Item(5).ToString
                Dim strAkaTahun As String = ds.Tables(0).Rows(i).Item(6).ToString
                ''getting data end

                Dim table As New PdfPTable(3)
                table.WidthPercentage = 100
                table.SetWidths({42, 16, 42})
                table.DefaultCell.Border = 0


                myDocument.Add(table)

                Dim myPara001 As New Paragraph("LEMBAGA PEPERIKSAAN", FontFactory.GetFont("Arial", 10, Font.BOLD))
                myPara001.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara001)

                Dim myPara01 As New Paragraph("KEMENTERIAN PENDIDIKAN MALAYSIA", FontFactory.GetFont("Arial", 10, Font.BOLD))
                myPara01.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara01)

                myDocument.Add(imgSpacing)
                Dim myPara02 As New Paragraph("SLIP KEPUTUSAN SEJARAH 1251", FontFactory.GetFont("Tw Cen Mt", 12, Font.NORMAL))
                myPara02.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara02)

                Dim myPara03 As New Paragraph("TAHUN " & strAkaTahun, FontFactory.GetFont("Tw Cen Mt", 12, Font.NORMAL))
                myPara03.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara03)

                myDocument.Add(imgSpacing)

                ''PROFILE STARTS HERE

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                table = New PdfPTable(2)

                table.WidthPercentage = 100
                table.SetWidths({30, 70})

                Dim cell = New PdfPCell()
                Dim cetak = Environment.NewLine & "NAMA"
                cetak += Environment.NewLine & "NO.KAD PENGENALAN"
                cetak += Environment.NewLine & "ANGKA GILIRAN"
                cetak += Environment.NewLine & "INSTITUSI"
                cetak += Environment.NewLine & "NAMA BIDANG"
                cetak += Environment.NewLine & "PROGRAM"
                cetak += Environment.NewLine & ""

                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = Environment.NewLine & ": " & strname
                cetak += Environment.NewLine & ": " & strmykad
                cetak += Environment.NewLine & ": " & strag
                cetak += Environment.NewLine & ": " & strKolejnama
                cetak += Environment.NewLine & ": " & strbidang
                cetak += Environment.NewLine & ": " & strprogram & " (" & strkodKursus & ")"
                cetak += Environment.NewLine & " "

                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)
                Debug.WriteLine(cetak)

                myDocument.Add(table)

                ''profile ends here
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({30, 42, 18, 10})

                cell = New PdfPCell()
                cetak = "KOD"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "MATA PELAJARAN"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "GRED"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

                strSQL = "select Kompetensi from kpmkv_pelajar_akademik_ulang where Mykad = '" & strkey & "'"
                strSQL += " AND Tahun='" & ddlTahun.SelectedValue & "'"
                strSQL += " AND Sesi='" & chkSesi.SelectedValue & "'"
                strSQL += " AND isAKATahun='" & strAkaTahun & "'"
                Dim strgred As String = oCommon.getFieldValue(strSQL)
                If strgred = "" Then
                    strgred = ""
                End If


                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({30, 42, 18, 10})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += "1251"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "SEJARAH"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += strgred
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                Debug.WriteLine(cetak)
                myDocument.Add(table)


                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(New Paragraph("TARIKH: " & ddlHari.Text & "/" & ddlBulan.Text & "/" & ddlTahun_1.Text & "                                                                                                                                                      PENGARAH PEPERIKSAAN", FontFactory.GetFont("Arial", 8, Font.BOLD)))
                'Dim myPengarah As New Paragraph("" & strKolejnama, FontFactory.GetFont("Arial", 8, Font.BOLD))
                'myPengarah.Alignment = Element.ALIGN_RIGHT
                'myDocument.Add(myPengarah)

                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                Dim myslip As New Paragraph("Slip ini adalah cetakan komputer, tandatangan tidak diperlukan", FontFactory.GetFont("Arial", 8, Font.ITALIC))
                myslip.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myslip)
                myDocument.NewPage()
                ''--content end


                myDocument.NewPage()


            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        countStudent()

    End Sub

    Private Sub countStudent()
        strSQL = "Select count(distinct au.mykad)"
        strSQL += " From kpmkv_pelajar_akademik_ulang As au"
        strSQL += " Left Join kpmkv_kursus as k on k.kodkursus=au.kodkursus"
        strSQL += " Left Join kpmkv_kluster as kl on k.klusterID=kl.klusterID"
        strSQL += " WHERE au.KolejRecordID ='" & lblKolejID.Text & "'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strSQL += " AND au.Tahun ='" & ddlTahun.Text & "'"
        End If

        If Not ddlKodKursus.SelectedValue = "" Then
            strSQL += " AND au.KodKursus='" & ddlKodKursus.SelectedValue & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strSQL += " AND au.Sesi ='" & chkSesi.Text & "'"
        End If


        Dim total As String = oCommon.getFieldValue(strSQL)
        If total = "" Then
            total = "Tiada Rekod Ditemui"
        End If

        lblMsg.Text = "Jumlah pelajar : " & total
    End Sub
End Class