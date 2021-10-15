Imports System.IO
Public Class FrmShowDefect

    Public Sub LoadImage(ByVal path As String, ByVal x As Integer, ByVal y As Integer, ByVal winW As Single, ByVal winH As Single)

        Me.picDfImg.Image = Nothing
        Dim W As Single = CSng(winW)
        Dim H As Single = CSng(winH)
        Dim bm As Bitmap
        Dim bm_defect As Bitmap
        Dim gr As Graphics

        If File.Exists(path) Then
            Me.picDfImg.Visible = True
            bm = New Bitmap(CInt(W), CInt(H))
            bm_defect = New Bitmap(path)
            gr = Graphics.FromImage(bm)
            gr.DrawImage(bm_defect, New Rectangle(0, 0, winW, winH), New Rectangle(0, 0, bm_defect.Width, bm_defect.Height), GraphicsUnit.Pixel)
            Me.picDfImg.Image = bm
            Me.labDfImgMsg.Text = "(" & x.ToString & ", " & y.ToString & ")"
            Me.labDfImgMsg.BackColor = Color.DarkGreen
            gr.Dispose()
        Else
            Me.picDfImg.Visible = False
            Me.labDfImgMsg.Text = "檔案不存在或路徑錯誤!"
            Me.labDfImgMsg.BackColor = Color.Red
            Me.picDfImg.Image = Nothing
            Me.picDfImg.BackColor = Color.Black
        End If

        Me.Refresh()
    End Sub
    Public Sub LoadImage(ByVal path As String, ByVal x As Integer, ByVal y As Integer, ByVal winW As Single, ByVal winH As Single, ByVal DefectName As String, ByVal PatternName As String)

        Me.picDfImg.Image = Nothing
        Dim W As Single = CSng(winW)
        Dim H As Single = CSng(winH)
        Dim bm As Bitmap
        Dim bm_defect As Bitmap
        Dim gr As Graphics

        If File.Exists(path) Then
            Me.picDfImg.Visible = True
            bm = New Bitmap(CInt(W), CInt(H))
            bm_defect = New Bitmap(path)
            gr = Graphics.FromImage(bm)
            gr.DrawImage(bm_defect, New Rectangle(0, 0, winW, winH), New Rectangle(0, 0, bm_defect.Width, bm_defect.Height), GraphicsUnit.Pixel)
            Me.picDfImg.Image = bm
            'Me.labDfImgMsg.Text = "(" & x.ToString & ", " & y.ToString & ")"
            Me.labDfImgMsg.Text = DefectName & vbLf & PatternName
            Me.labDfImgMsg.BackColor = Color.DarkGreen
            gr.Dispose()
        Else
            Me.picDfImg.Visible = False
            Me.labDfImgMsg.Text = "檔案不存在或路徑錯誤!"
            Me.labDfImgMsg.BackColor = Color.Red
            Me.picDfImg.Image = Nothing
            Me.picDfImg.BackColor = Color.Black
        End If

        Me.Refresh()
    End Sub

    Public Sub NoImage(ByVal DefectName As String, ByVal PatternName As String)
        Me.labDfImgMsg.Text = DefectName & vbLf & PatternName
        Me.Panel1.Visible = False
        Me.Size = New Point(100, 46)
    End Sub

    Public Sub Movefrm(ByVal x As Integer, ByVal y As Integer)
        'Me.Show()
        Me.Location = New Point(x, y)
    End Sub

    Private Sub FrmShowDefect_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
    Private Sub ToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picDfImg.Click, labDfImgMsg.Click, MyBase.Click
        Me.BringToFront()
    End Sub
End Class