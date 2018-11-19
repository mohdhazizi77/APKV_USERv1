Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class svm_pengesahan1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PelajarID As String = Request.QueryString("id")

        If Not PelajarID = Nothing Then

            Dim decryptedID As String = Decrypt(HttpUtility.UrlDecode(PelajarID))

            strSQL = "  SELECT 
                    kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, 
                    kpmkv_kolej.Nama, kpmkv_kolej.Negeri,
                    kpmkv_kursus.NamaKursus, kpmkv_kursus.KodKursus,
                    kpmkv_kluster.NamaKluster,
                    kpmkv_pelajar_markah.GredBMSetara,
                    kpmkv_SVM.PNGKA, kpmkv_SVM.PNGKV, kpmkv_SVM.GredBMSetara, kpmkv_SVM.LayakSVM
                    FROM kpmkv_pelajar
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_kursus On kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID
                    LEFT JOIN kpmkv_kluster ON kpmkv_kluster.KlusterID = kpmkv_kursus.KlusterID 
                    LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID
                    LEFT JOIN kpmkv_SVM ON kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID
                    WHERE
                    kpmkv_pelajar.PelajarID = '" & decryptedID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim strNama As String = ds.Tables(0).Rows(i).Item(0).ToString
                Dim strMykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                Dim strAG As String = ds.Tables(0).Rows(i).Item(2).ToString
                Dim strInstitusi As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim strNegeri As String = ds.Tables(0).Rows(i).Item(4).ToString
                Dim strNamaKursus As String = ds.Tables(0).Rows(i).Item(5).ToString
                Dim strKodKursus As String = ds.Tables(0).Rows(i).Item(6).ToString
                Dim strKluster As String = ds.Tables(0).Rows(i).Item(7).ToString
                Dim strGredBMSetara As String = ds.Tables(0).Rows(i).Item(8).ToString
                Dim strPNGKA As String = ds.Tables(0).Rows(i).Item(9).ToString
                Dim strPNGKV As String = ds.Tables(0).Rows(i).Item(10).ToString
                Dim strGredBMSVM As String = ds.Tables(0).Rows(i).Item(11).ToString
                Dim strLayakSVM As String = ds.Tables(0).Rows(i).Item(12).ToString

                Dim strStatus As String = ""
                If strLayakSVM = "1" Then
                    If strGredBMSVM = "C" Or strGredBMSVM = "C+" Or strGredBMSVM = "B-" Or strGredBMSVM = "B" Or strGredBMSVM = "B+" Or strGredBMSVM = "A-" Or strGredBMSVM = "A" Or strGredBMSVM = "A+" Then
                        strStatus = "SETARA"
                    Else
                        strStatus = "TAKSETARA"
                    End If
                End If

                lblNama.Text = strNama
                lblMykad.Text = strMykad
                lblAG.Text = strAG
                lblInstitusi.Text = strInstitusi & ", " & strNegeri
                lblKluster.Text = strKluster
                lblKursus.Text = strNamaKursus & " (" & strKodKursus & ")"
                lblBM.Text = "GRED " & strGredBMSetara
                lblKompeten.Text = "KOMPETEN SEMUA MODUL " & strKluster
                lblPNGKA.Text = strPNGKA
                lblPNGKV.Text = strPNGKV

                If strStatus = "SETARA" Then
                    tblSetara.Visible = True
                Else
                    tblSetara.Visible = False
                End If

            Next

        End If

    End Sub

    Private Function Decrypt(qrCipher As String) As String

        Dim encryptionKey As String = "MAKV2SPBNI99212"
        qrCipher = qrCipher.Replace(" ", "+")

        Dim qrCipherBytes As Byte() = Convert.FromBase64String(qrCipher)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(encryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(qrCipherBytes, 0, qrCipherBytes.Length)
                    cs.Close()
                End Using
                qrCipher = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return qrCipher

    End Function

End Class