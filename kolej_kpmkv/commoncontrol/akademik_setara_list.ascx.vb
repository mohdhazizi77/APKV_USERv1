Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class akademik_setara_list
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim IntTakwim As Integer = 0

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

                '------exist takwim
                strSQL = "SELECT * FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Senarai Calon Pengesahan Akademik' AND Aktif='1'"
                If oCommon.isExist(strSQL) = True Then

                    'count data takwim
                    'Get the data from database into datatable
                    Dim cmd As New SqlCommand("SELECT TakwimID FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Senarai Calon Pengesahan Akademik' AND Aktif='1'")
                    Dim dt As DataTable = GetData(cmd)

                    For i As Integer = 0 To dt.Rows.Count - 1
                        IntTakwim = dt.Rows(i)("TakwimID")

                        strSQL = "SELECT TarikhMula,TarikhAkhir FROM kpmkv_takwim WHERE TakwimID='" & IntTakwim & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_user_login As Array
                        ar_user_login = strRet.Split("|")
                        Dim strMula As String = ar_user_login(0)
                        Dim strAkhir As String = ar_user_login(1)

                        Dim strdateNow As Date = Date.Now
                        Dim startDate = DateTime.ParseExact(strMula, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        Dim endDate = DateTime.ParseExact(strAkhir, "dd-MM-yyyy", CultureInfo.InvariantCulture)

                        Dim ts As New TimeSpan
                        ts = endDate.Subtract(strdateNow)
                        Dim dayDiff = ts.Days

                        If strMula IsNot Nothing Then
                            If strAkhir IsNot Nothing And dayDiff <> 1 Then

                                Year()
                                'checkinbox
                                strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "' ORDER BY Kohort ASC"
                                strRet = oCommon.getFieldValue(strSQL)


                                If strRet = 1 Then
                                    chkSesi.Text = "0"
                                    chkSesi.Items(0).Enabled = False
                                    chkSesi.Items(1).Enabled = False
                                    kpmkv_tahun_list()
                                    ddlKohort.Text = "0"
                                Else
                                    chkSesi.Items(0).Enabled = False
                                    chkSesi.Items(1).Enabled = False
                                    chkSesi.Text = "2"
                                    kpmkv_tahun_list()
                                    ddlKohort.Text = "0"
                                End If

                                'kpmkv_tahun_list()
                                kpmkv_kursus_list()
                                ddlKodkursus.Text = "0"

                                kpmkv_MP_list()
                                ddlMataPelajaran.Text = "0"


                                btnSearch.Enabled = True
                            End If
                        Else
                            btnSearch.Enabled = False
                            lblMsg.Text = "Senarai Calon Pengesahan Akademik telah ditutup!"
                        End If
                    Next
                Else
                    btnSearch.Enabled = False
                    lblMsg.Text = "Senarai Calon Pengesahan Akademik telah ditutup!"
                End If
                RepoveDuplicate(ddlTahunSemasa)
            End If


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Shared Function RepoveDuplicate(ByVal ddl As DropDownList) As DropDownList
        For Row As Int16 = 0 To ddl.Items.Count - 2
            For RowAgain As Int16 = ddl.Items.Count - 1 To Row + 1 Step -1
                If ddl.Items(Row).ToString = ddl.Items(RowAgain).ToString Then
                    ddl.Items.RemoveAt(RowAgain)
                End If
            Next
        Next
        Return ddl
    End Function
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT DISTINCT Tahun FROM kpmkv_pelajar_Akademik_Ulang WHERE IsAKATahun='" & ddlTahunSemasa.Text & "' AND IsAKASesi='" & chkSesi.Text & "' AND KolejRecordID='" & lblKolejID.Text & "' ORDER BY Tahun ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKohort.DataSource = ds
            ddlKohort.DataTextField = "Tahun"
            ddlKohort.DataValueField = "Tahun"
            ddlKohort.DataBind()

            '--ALL
            ddlKohort.Items.Insert(0, New ListItem("-Pilih-", "0"))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub Year()
        strSQL = "SELECT Kohort FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
        strRet = oCommon.getFieldValue(strSQL)
        Try
            If Not ddlTahunSemasa.Text = strRet Then
                ddlTahunSemasa.Items.Add(strRet)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub kpmkv_MP_list()
        strSQL = "SELECT DISTINCT MataPelajaran FROM kpmkv_pelajar_Akademik_Ulang WHERE IsAKATahun='" & ddlTahunSemasa.Text & "' AND IsAKASesi='" & chkSesi.Text & "'"
        strSQL += " AND Tahun='" & ddlKohort.Text & "' AND Sesi='" & chkSesiKohort.Text & "' AND KolejRecordID='" & lblKolejID.Text & "' ORDER BY MataPelajaran ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMataPelajaran.DataSource = ds
            ddlMataPelajaran.DataTextField = "MataPelajaran"
            ddlMataPelajaran.DataValueField = "MataPelajaran"
            ddlMataPelajaran.DataBind()
            '--ALL
            ddlMataPelajaran.Items.Insert(0, New ListItem("-Pilih-", "0"))
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kursus_list()
        strSQL = "SELECT DISTINCT KodKursus FROM kpmkv_pelajar_Akademik_Ulang WHERE IsAKATahun='" & ddlTahunSemasa.Text & "' AND  IsAKASesi='" & chkSesi.Text & "'"
        strSQL += " AND Tahun='" & ddlKohort.Text & "' AND Sesi='" & chkSesiKohort.Text & "' AND KolejRecordID='" & lblKolejID.Text & "' ORDER BY KodKursus ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodkursus.DataSource = ds
            ddlKodkursus.DataTextField = "KodKursus"
            ddlKodkursus.DataValueField = "KodKursus"
            ddlKodkursus.DataBind()

            '--ALL
            ddlKodkursus.Items.Insert(0, New ListItem("-Pilih-", "0"))
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblMsgResult.Text = ""
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Nama ASC"

        '--not deleted
        tmpSQL = "SELECT PelajarAKAID, PelajarID, KolejRecordID, Nama, AngkaGiliran, MYKAD, Tahun, Sesi, KodKursus, MataPelajaran, IsCalon, IsAKASesi, IsAKATahun "
        tmpSQL += " FROM kpmkv_pelajar_Akademik_Ulang"
        strWhere = " WHERE IsDeleted='N' AND IsCalon='1' AND KolejRecordID='" & lblKolejID.Text & "'"
        strWhere += " AND IsAKATahun ='" & ddlTahunSemasa.Text & "' AND IsAKASesi ='" & chkSesi.Text & "'"

        '--kohort
        If Not ddlKohort.Text = "0" Then
            strWhere += " AND Tahun ='" & ddlKohort.Text & "'"
        End If

        '--sesi
        If Not chkSesiKohort.Text = "" Then
            strWhere += " AND Sesi ='" & chkSesiKohort.Text & "'"
        End If

        '--mp
        If Not ddlMataPelajaran.Text = "0" Then
            strWhere += " AND MataPelajaran ='" & ddlMataPelajaran.Text & "'"
        End If

        '--ddlkursus
        If Not ddlKodkursus.Text = "0" Then
            strWhere += " AND KodKursus='" & ddlKodkursus.Text & "'"
        End If
        '--txtNama
        If Not txtNama.Text.Length = "0" Then
            strWhere += " AND Mykad='" & oCommon.FixSingleQuotes(txtNama.Text) & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        ' Response.Redirect("pelajar.view.aspx?PelajarID=" & strKeyID)

    End Sub

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kursus_list()
        ddlKodKursus.Text = "0"

        kpmkv_MP_list()
        ddlMataPelajaran.Text = "0"
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        lblMsgResult.Text = ""
        Dim IntPelajarAKAID As Integer = datRespondent.DataKeys(e.RowIndex).Values("PelajarAKAID")
        Try
            If Not IntPelajarAKAID = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_Akademik_Ulang SET IsCalon='0' WHERE PelajarAKAID='" & IntPelajarAKAID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Calon berjaya dipadamkan Pengesahan"
                Else
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Calon tidak berjaya dipadamkan Pengesahan"
                End If
            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub btnLink_Click(sender As Object, e As EventArgs) Handles btnLink.Click
        Response.Redirect("admin_akademik_setara.aspx")
    End Sub

    Private Sub chkSesiKohort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesiKohort.SelectedIndexChanged
        kpmkv_kursus_list()
        ddlKodkursus.Text = "0"

        kpmkv_MP_list()
        ddlMataPelajaran.Text = "0"
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=AkademikSetara.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

End Class