Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization

Public Class markah_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim IntTakwim As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                'kolejnama
                strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                Dim strKolejnama As String = oCommon.getFieldValue(strSQL)
                'kolejid
                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                lblKolejID.Text = oCommon.getFieldValue(strSQL)

                '------exist takwim
                strSQL = "SELECT * FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pentaksiran Berterusan Vokasional' AND Aktif='1'"
                If oCommon.isExist(strSQL) = True Then

                    'count data takwim
                    'Get the data from database into datatable
                    Dim cmd As New SqlCommand("SELECT TakwimID FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pentaksiran Berterusan Vokasional' AND Aktif='1'")
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
                                kpmkv_tahun_list()
                                kpmkv_semester_list()

                                'checkinbox
                                strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
                                strRet = oCommon.getFieldValue(strSQL)

                                If strRet = 1 Then
                                    chkSesi.Items(0).Enabled = True
                                    'chkSesi.Items(1).Enabled = False
                                Else
                                    'chkSesi.Items(0).Enabled = False
                                    chkSesi.Items(1).Enabled = True
                                End If
                                btnExport.Enabled = True
                                btnUpdate.Enabled = True
                            End If
                        Else
                            btnExport.Enabled = False
                            btnUpdate.Enabled = False
                            lblMsg.Text = "Pentaksiran Berterusan Vokasional telah ditutup!"
                        End If
                    Next
                Else
                    btnExport.Enabled = False
                    btnUpdate.Enabled = False
                    lblMsg.Text = "Pentaksiran Berterusan Vokasional telah ditutup!"
                End If
                RepoveDuplicate(ddlTahun)
                RepoveDuplicate(ddlSemester)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub hiddencolumn()
        strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
        strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
        strSQL += " AND kpmkv_modul.KursusID='" & ddlKodKursus.SelectedValue & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Select Case strRet
            Case "2"
                datRespondent.Columns.Item("9").Visible = False
                datRespondent.Columns.Item("10").Visible = False
                datRespondent.Columns.Item("11").Visible = False
                datRespondent.Columns.Item("12").Visible = False
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "3"
                datRespondent.Columns.Item("11").Visible = False
                datRespondent.Columns.Item("12").Visible = False
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "4"
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "5"
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "6"
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "7"
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False
        End Select


    End Sub
    Private Sub kpmkv_tahun_list()
         strSQL = "SELECT Kohort FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
        strRet = oCommon.getFieldValue(strSQL)
        Try
            If Not ddlTahun.Text = strRet Then
                ddlTahun.Items.Add(strRet)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Semester ASC"
        strRet = oCommon.getFieldValue(strSQL)
        Try
            If Not ddlSemester.Text = strRet Then
                ddlSemester.Items.Add(strRet)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & lblKolejID.Text & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID"
        strSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & lblKolejID.Text & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "NamaKelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            '--ALL
            ddlKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
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
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.PelajarID,  kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_pelajar_markah.B_Amali1, kpmkv_pelajar_markah.B_Amali2, kpmkv_pelajar_markah.B_Amali3, kpmkv_pelajar_markah.B_Amali4,"
        tmpSQL += " kpmkv_pelajar_markah.B_Amali5, kpmkv_pelajar_markah.B_Amali6, kpmkv_pelajar_markah.B_Amali7, kpmkv_pelajar_markah.B_Amali8,"
        tmpSQL += " kpmkv_pelajar_markah.B_Teori1, kpmkv_pelajar_markah.B_Teori2, kpmkv_pelajar_markah.B_Teori3, kpmkv_pelajar_markah.B_Teori4,"
        tmpSQL += " kpmkv_pelajar_markah.B_Teori5, kpmkv_pelajar_markah.B_Teori6, kpmkv_pelajar_markah.B_Teori7, kpmkv_pelajar_markah.B_Teori8"
        tmpSQL += " FROM  kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & lblKolejID.Text & "' and kpmkv_pelajar.IsDeleted='N' and kpmkv_pelajar.StatusID='2'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--jantina
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

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
        Response.AddHeader("content-disposition", "attachment;filename=KOKO_File.csv")
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

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        lblMsg.Text = ""
        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
            Exit Sub
        End If

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strAmali1 As TextBox = datRespondent.Rows(i).FindControl("txtAmali1")
            Dim strAmali2 As TextBox = datRespondent.Rows(i).FindControl("txtAmali2")
            Dim strAmali3 As TextBox = datRespondent.Rows(i).FindControl("txtAmali3")
            Dim strAmali4 As TextBox = datRespondent.Rows(i).FindControl("txtAmali4")
            Dim strAmali5 As TextBox = datRespondent.Rows(i).FindControl("txtAmali5")
            Dim strAmali6 As TextBox = datRespondent.Rows(i).FindControl("txtAmali6")
            Dim strAmali7 As TextBox = datRespondent.Rows(i).FindControl("txtAmali7")
            Dim strAmali8 As TextBox = datRespondent.Rows(i).FindControl("txtAmali8")

            Dim strTeori1 As TextBox = datRespondent.Rows(i).FindControl("txtTeori1")
            Dim strTeori2 As TextBox = datRespondent.Rows(i).FindControl("txtTeori2")
            Dim strTeori3 As TextBox = datRespondent.Rows(i).FindControl("txtTeori3")
            Dim strTeori4 As TextBox = datRespondent.Rows(i).FindControl("txtTeori4")
            Dim strTeori5 As TextBox = datRespondent.Rows(i).FindControl("txtTeori5")
            Dim strTeori6 As TextBox = datRespondent.Rows(i).FindControl("txtTeori6")
            Dim strTeori7 As TextBox = datRespondent.Rows(i).FindControl("txtTeori7")
            Dim strTeori8 As TextBox = datRespondent.Rows(i).FindControl("txtTeori8")

            'assign value to integer
            Dim Amali1 As String = strAmali1.Text
            Dim Amali2 As String = strAmali2.Text
            Dim Amali3 As String = strAmali3.Text
            Dim Amali4 As String = strAmali4.Text
            Dim Amali5 As String = strAmali5.Text
            Dim Amali6 As String = strAmali6.Text
            Dim Amali7 As String = strAmali7.Text
            Dim Amali8 As String = strAmali8.Text
            Dim Teori1 As String = strTeori1.Text
            Dim Teori2 As String = strTeori2.Text
            Dim Teori3 As String = strTeori3.Text
            Dim Teori4 As String = strTeori4.Text
            Dim Teori5 As String = strTeori5.Text
            Dim Teori6 As String = strTeori6.Text
            Dim Teori7 As String = strTeori7.Text
            Dim Teori8 As String = strTeori8.Text

           
            strSQL = "UPDATE kpmkv_pelajar_markah SET B_Amali1='" & Amali1 & "',B_Teori1='" & Teori1 & "', B_Amali2='" & Amali2 & "', B_Teori2='" & Teori2 & "',"
            strSQL += " B_Amali3='" & Amali3 & "',B_Teori3='" & Teori3 & "', B_Amali4='" & Amali4 & "', B_Teori4='" & Teori4 & "',"
            strSQL += " B_Amali5='" & Amali5 & "', B_Teori5='" & Teori5 & "', B_Amali6='" & Amali6 & "', B_Teori6='" & Teori6 & "',"
            strSQL += " B_Amali7='" & Amali7 & "', B_Teori7='" & Teori7 & "',B_Amali8='" & Amali8 & "',B_Teori8='" & Teori8 & "'"
            strSQL += " WHERE KolejRecordID='" & lblKolejID.Text & "'"
            strSQL += " AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                divMsgResult.Attributes("class") = "error"
                lblMsgResult.Text = "Tidak Berjaya mengemaskini markah Pentaksiran Berterusan Vokasional"
            End If
        Next

       
        divMsgResult.Attributes("class") = "info"
        lblMsgResult.Text = "Berjaya mengemaskini markah Pentaksiran Berterusan Vokasional"
        strRet = BindData((datRespondent))
    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strAmali1 As TextBox = CType(row.FindControl("txtAmali1"), TextBox)
            Dim strAmali2 As TextBox = CType(row.FindControl("txtAmali2"), TextBox)
            Dim strAmali3 As TextBox = CType(row.FindControl("txtAmali3"), TextBox)
            Dim strAmali4 As TextBox = CType(row.FindControl("txtAmali4"), TextBox)
            Dim strAmali5 As TextBox = CType(row.FindControl("txtAmali5"), TextBox)
            Dim strAmali6 As TextBox = CType(row.FindControl("txtAmali6"), TextBox)
            Dim strAmali7 As TextBox = CType(row.FindControl("txtAmali7"), TextBox)
            Dim strAmali8 As TextBox = CType(row.FindControl("txtAmali8"), TextBox)
            Dim strTeori1 As TextBox = CType(row.FindControl("txtTeori1"), TextBox)
            Dim strTeori2 As TextBox = CType(row.FindControl("txtTeori2"), TextBox)
            Dim strTeori3 As TextBox = CType(row.FindControl("txtTeori3"), TextBox)
            Dim strTeori4 As TextBox = CType(row.FindControl("txtTeori4"), TextBox)
            Dim strTeori5 As TextBox = CType(row.FindControl("txtTeori5"), TextBox)
            Dim strTeori6 As TextBox = CType(row.FindControl("txtTeori6"), TextBox)
            Dim strTeori7 As TextBox = CType(row.FindControl("txtTeori7"), TextBox)
            Dim strTeori8 As TextBox = CType(row.FindControl("txtTeori8"), TextBox)

            '--validate NUMBER and less than 100
            '--amali1
            If Not strAmali1.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali1.Text) = False Then
                    Return False
                End If
                If CInt(strAmali1.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali1.Text = "0"
            End If

            '--amali2
            If Not strAmali2.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali2.Text) = False Then
                    Return False
                End If
                If CInt(strAmali2.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali2.Text = "0"
            End If

            '--amali3
            If Not strAmali3.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali3.Text) = False Then
                    Return False
                End If
                If CInt(strAmali3.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali3.Text = "0"
            End If

            '--amali4
            If Not strAmali4.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali4.Text) = False Then
                    Return False
                End If
                If CInt(strAmali4.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali4.Text = "0"
            End If

            '--amali5
            If Not strAmali5.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali5.Text) = False Then
                    Return False
                End If
                If CInt(strAmali5.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali5.Text = "0"
            End If

            '--amali6
            If Not strAmali6.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali6.Text) = False Then
                    Return False
                End If
                If CInt(strAmali6.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali6.Text = "0"
            End If

            '--amali7
            If Not strAmali7.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali7.Text) = False Then
                    Return False
                End If
                If CInt(strAmali7.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali7.Text = "0"
            End If

            '--amali8
            If Not strAmali8.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali8.Text) = False Then
                    Return False
                End If
                If CInt(strAmali8.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali8.Text = "0"
            End If

            'teori1
            If Not strTeori1.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori1.Text) = False Then
                    Return False
                End If
                If CInt(strTeori1.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori1.Text = "0"
            End If

            '--teori2
            If Not strTeori2.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori2.Text) = False Then
                    Return False
                End If
                If CInt(strTeori2.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori2.Text = "0"
            End If

            '--teori3
            If Not strTeori3.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori3.Text) = False Then
                    Return False
                End If
                If CInt(strTeori3.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori3.Text = "0"
            End If

            '--teori4
            If Not strTeori4.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori4.Text) = False Then
                    Return False
                End If
                If CInt(strTeori4.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori4.Text = "0"
            End If


            'teori5
            If Not strTeori5.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori5.Text) = False Then
                    Return False
                End If
                If CInt(strTeori5.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori5.Text = "0"
            End If

            '--teori6
            If Not strTeori6.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori6.Text) = False Then
                    Return False
                End If
                If CInt(strTeori6.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori6.Text = "0"
            End If

            '--teori7
            If Not strTeori7.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori7.Text) = False Then
                    Return False
                End If
                If CInt(strTeori7.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori7.Text = "0"
            End If

            '--teori8
            If Not strTeori8.Text.Length = 0 Then
                If oCommon.IsCurrency(strTeori8.Text) = False Then
                    Return False
                End If
                If CInt(strTeori8.Text) > 100 Then
                    Return False
                End If
            Else
                strTeori8.Text = "0"
            End If
        Next

        Return True
    End Function
    Protected Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""

        datRespondent.Columns.Item("5").Visible = True
        datRespondent.Columns.Item("6").Visible = True
        datRespondent.Columns.Item("7").Visible = True
        datRespondent.Columns.Item("8").Visible = True
        datRespondent.Columns.Item("9").Visible = True
        datRespondent.Columns.Item("10").Visible = True
        datRespondent.Columns.Item("11").Visible = True
        datRespondent.Columns.Item("12").Visible = True
        datRespondent.Columns.Item("13").Visible = True
        datRespondent.Columns.Item("14").Visible = True
        datRespondent.Columns.Item("15").Visible = True
        datRespondent.Columns.Item("16").Visible = True
        datRespondent.Columns.Item("17").Visible = True
        datRespondent.Columns.Item("18").Visible = True
        datRespondent.Columns.Item("19").Visible = True
        datRespondent.Columns.Item("20").Visible = True
        strRet = BindData(datRespondent)
        hiddencolumn()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
        ddlKelas.Text = "0"

    End Sub

End Class