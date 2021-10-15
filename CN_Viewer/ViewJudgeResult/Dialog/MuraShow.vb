Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Public Class MuraShow
    Public m_PanelID As String
    Public m_FilePath As String
    Public m_PanelSizeX As Integer
    Public m_PanelSizeY As Integer
    Public m_aryMuraList As New ArrayList
    Public m_TemparyMuraList As New ArrayList
    Public m_MuraPen As New System.Drawing.Pen(Color.Red)
    Public m_penMark As New System.Drawing.Pen(Color.Yellow)
    Private Const ClickRange As Integer = 3 'Data/Gate axis range=+- 3 subpixel
    Private m_ToolTipMsg As ToolTip
    'Private m_aryCheckMuraPt As ArrayList
    Private m_ViewResultInfo As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a(3) As String
        a(0) = "False"
        a(1) = "DF"
        a(2) = "1"
        a(3) = "2"
        Me.dgvMuraShow.Rows.Add(a)

    End Sub

    Public Sub New(ByVal PanelID As String, ByVal FilePath As String, ByVal PanelSizeX As Integer, ByVal PanelSizeY As Integer, ByRef aryMuraList As ArrayList, ByVal ViewResultInfo As String)

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.m_PanelID = PanelID
        Me.m_FilePath = FilePath
        Me.m_PanelSizeX = PanelSizeX
        Me.m_PanelSizeY = PanelSizeY
        Me.m_aryMuraList = aryMuraList
        Me.m_ViewResultInfo = ViewResultInfo
        Me.m_ToolTipMsg = New ToolTip
       
        'Canel恢復用
        For i As Integer = 0 To aryMuraList.Count - 1
            Dim TempclsMuraDdf As New ClsMuraDf
            TempclsMuraDdf.MuraType = aryMuraList.Item(i).MuraType
            TempclsMuraDdf.Data = aryMuraList.Item(i).Data
            TempclsMuraDdf.Gate = aryMuraList.Item(i).Gate
            TempclsMuraDdf.Area = aryMuraList.Item(i).Area
            TempclsMuraDdf.JND = aryMuraList.Item(i).JND
            TempclsMuraDdf.Rank = aryMuraList.Item(i).Rank
            TempclsMuraDdf.X = aryMuraList.Item(i).X
            TempclsMuraDdf.Y = aryMuraList.Item(i).Y
            TempclsMuraDdf.MinX = aryMuraList.Item(i).MinX
            TempclsMuraDdf.MinY = aryMuraList.Item(i).MinY
            TempclsMuraDdf.MaxX = aryMuraList.Item(i).MaxX
            TempclsMuraDdf.MaxY = aryMuraList.Item(i).MaxY
            TempclsMuraDdf.Score = aryMuraList.Item(i).Score
            TempclsMuraDdf.Pattern = aryMuraList.Item(i).Pattern
            TempclsMuraDdf.Width = aryMuraList.Item(i).Width
            TempclsMuraDdf.ChipID = aryMuraList.Item(i).ChipID
            TempclsMuraDdf.CCDNo = aryMuraList.Item(i).CCDNo
            TempclsMuraDdf.MuraName = aryMuraList.Item(i).MuraName
            TempclsMuraDdf.ImageFilePath = aryMuraList.Item(i).ImageFilePath
            TempclsMuraDdf.FileName = aryMuraList.Item(i).FileName
            TempclsMuraDdf.CheckMura = aryMuraList.Item(i).CheckMura
            Me.m_TemparyMuraList.Add(TempclsMuraDdf)
        Next
    End Sub

    Private Sub dgvMuraShow_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMuraShow.CellValueChanged
        If dgvMuraShow.Columns(e.ColumnIndex).Name = "Check" Then
            Dim x = e.ColumnIndex
            Dim y = e.RowIndex
            Me.m_aryMuraList.Item(y).CheckMura = Me.dgvMuraShow.Rows(y).Cells(0).Value
            Dim data, gate As Integer
            data = Me.m_aryMuraList.Item(y).Data
            gate = Me.m_aryMuraList.Item(y).Gate
            Me.view_Paint_1CCDMODE(data, gate)
        End If
    End Sub
    Private Sub dgvMuraShow_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvMuraShow.CurrentCellDirtyStateChanged
        If dgvMuraShow.IsCurrentCellDirty Then
            dgvMuraShow.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer = 0
        Dim sw As StreamWriter
        Dim a As System.Windows.Forms.DialogResult
        Try
            a = MessageBox.Show("確認是否儲存" & vbCrLf & "(按下確定後, 原本的檔案將會被覆蓋, 是否確定寫入?)", "Over-Write?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If a = System.Windows.Forms.DialogResult.OK Then
                sw = File.CreateText(Me.m_FilePath + "\" + Me.m_PanelID + "_DMResult.txt")

                sw.WriteLine()
                sw.WriteLine("VIEWRESULTINFO")
                sw.WriteLine(Me.m_ViewResultInfo)

                'Func Format
                sw.WriteLine()
                sw.WriteLine("Type".PadRight(20) & vbTab & "BlobX" & vbTab & "BlobY" & vbTab & "Area" & vbTab & "Data" & vbTab & "Gate" & vbTab & "PType" & vbTab & "Pattern" & vbTab & "Rank" & vbTab & "Cst." & vbTab & "ChipID" & vbTab & "History" & vbTab & "CCDNo" & vbTab & "GrayMean" & vbTab & "ImageFilePath" & vbTab & "FileName")

                sw.WriteLine()
                sw.WriteLine("Mura Type".PadRight(20) & vbTab & "Data" & vbTab & "Gate" & vbTab & "Area" & vbTab & "JND" & vbTab & "Rank" & vbTab & "CenterX" & vbTab & "CenterY" & vbTab & "MinX" & vbTab & "MinY" & vbTab & "MaxX" & vbTab & "MaxY" & vbTab & "Score" & vbTab & "Pattern" & vbTab & "Width" & vbTab & "MuraName" & vbTab & "ChipID" & vbTab & "CCDNo" & vbTab & "ImageFilePath" & vbTab & "FileName" & vbTab & "CheckMura")

                For i = 0 To Me.dgvMuraShow.Rows.Count - 1
                    Dim str(20) As String
                    str(0) = Me.dgvMuraShow.Rows(i).Cells(1).Value.ToString.Trim
                    str(1) = Me.dgvMuraShow.Rows(i).Cells(2).Value.ToString.Trim
                    str(2) = Me.dgvMuraShow.Rows(i).Cells(3).Value.ToString.Trim
                    str(3) = "0"
                    str(4) = "0"
                    str(5) = "NULL"
                    str(6) = "0"
                    str(7) = "0"
                    str(8) = Me.dgvMuraShow.Rows(i).Cells(4).Value.ToString.Trim
                    str(9) = Me.dgvMuraShow.Rows(i).Cells(5).Value.ToString.Trim
                    str(10) = Me.dgvMuraShow.Rows(i).Cells(6).Value.ToString.Trim
                    str(11) = Me.dgvMuraShow.Rows(i).Cells(7).Value.ToString.Trim
                    str(12) = "0"
                    str(13) = Me.dgvMuraShow.Rows(i).Cells(8).Value.ToString.Trim
                    str(14) = "0" 'Mura Width
                    str(15) = "NULL"
                    str(16) = "NULL"
                    str(17) = "NULL"
                    str(18) = "NULL"
                    str(19) = "NULL"
                    str(20) = Me.dgvMuraShow.Rows(i).Cells(0).Value.ToString.Trim
                    sw.WriteLine(str(0).PadRight(20) & vbTab & str(1) & vbTab & str(2) & vbTab & str(3) & vbTab & str(4) & vbTab & str(5) & vbTab & str(6) & vbTab & str(7) & vbTab & str(8) & vbTab & str(9) & vbTab & str(10) & vbTab & str(11) & vbTab & str(12) & vbTab & str(13) & vbTab & str(14) & vbTab & str(15) & vbTab & str(16) & vbTab & str(17) & vbTab & str(18) & vbTab & str(19) & vbTab & str(20))
                Next
                sw.Close()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Save File Fail..!")
        End Try

    End Sub

    Private Sub view_Paint_1CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim W As Single = CSng(Me.picMuraShow.Size.Width)
        Dim H As Single = CSng(Me.picMuraShow.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim gr As Graphics = Graphics.FromImage(bm)

        Dim sx, sy As Single
        Dim i As Integer
        Dim data, gate As Integer


        Me.labPanelMinX.Text = "   1"
        Me.labPanelMaxX.Text = CStr(Me.m_PanelSizeX)
        Me.labPanelMinY.Text = "   1"
        Me.labPanelMaxY.Text = CStr(Me.m_PanelSizeY)


        sx = W / CSng(Me.m_PanelSizeX)
        sy = H / CSng(Me.m_PanelSizeY)

        For i = 0 To Me.m_aryMuraList.Count - 1

            data = Me.m_aryMuraList.Item(i).Data
            gate = Me.m_aryMuraList.Item(i).Gate

            If data = -1 Or gate = -1 Then
                If gate = -1 Then 'V-Line
                    Me.m_MuraPen.Width = 1.0F
                    If Me.m_aryMuraList.Item(i).CheckMura Then
                        gr.DrawLine(Pens.Red, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                    Else
                        gr.DrawLine(Pens.Blue, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                    End If

                Else 'H-Line
                    Me.m_MuraPen.Width = 1.0F
                    If Me.m_aryMuraList.Item(i).CheckMura Then
                        gr.DrawLine(Pens.Red, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                    Else
                        gr.DrawLine(Pens.Blue, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                    End If

                End If
            Else
                'Me.m_MuraPen.Width = 1.0F
                'gr.DrawEllipse(Pens.Red, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                Me.m_MuraPen.Width = 2.0F
                If Me.m_aryMuraList.Item(i).CheckMura Then
                    gr.DrawRectangle(Me.m_MuraPen, CInt(data * sx), CInt(gate * sy), 2, 2)
                Else
                    gr.DrawRectangle(Pens.Blue, CInt(data * sx), CInt(gate * sy), 2, 2)
                End If
            End If
        Next
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = intMarkX
            gate = intMarkY

            If intMarkX > 0 And intMarkY > 0 Then gr.DrawEllipse(Me.m_penMark, CInt(data * sx) - 5, CInt(gate * sy) - 5, 10, 10)
            If intMarkX > 0 And intMarkY = -1 Then gr.DrawLine(Me.m_penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkX = -1 And intMarkY > 0 Then gr.DrawLine(Me.m_penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))

        End If

        Me.picMuraShow.Image = bm
        Me.picMuraShow.Refresh()
        gr.Dispose()
    End Sub

    Private Sub LaadToMuraList()
        Dim sMuraList(10) As String
        Dim mura As New ClsMuraDf
        Dim i As Integer
        If Me.m_aryMuraList.Count > 0 Then
            For i = 0 To Me.m_aryMuraList.Count - 1
                mura = Me.m_aryMuraList.Item(i)
                sMuraList(0) = mura.CheckMura
                sMuraList(1) = mura.MuraType
                sMuraList(2) = mura.Data
                sMuraList(3) = mura.Gate
                sMuraList(4) = mura.MinX
                sMuraList(5) = mura.MinY
                sMuraList(6) = mura.MaxX
                sMuraList(7) = mura.MaxY
                sMuraList(8) = mura.Pattern
                sMuraList(9) = mura.Width
                Me.dgvMuraShow.Rows.Add(sMuraList)

                'Dim TempstuCheck As New clsCheck
                'TempstuCheck.Check = sMuraList(0)
                'TempstuCheck.MuraType = sMuraList(1)
                'TempstuCheck.Data = sMuraList(2)
                'TempstuCheck.Gate = sMuraList(3)
                'TempstuCheck.MinX = sMuraList(4)
                'TempstuCheck.MinY = sMuraList(5)
                'TempstuCheck.MaxX = sMuraList(6)
                'TempstuCheck.MaxY = sMuraList(7)
                'TempstuCheck.Pattern = sMuraList(8)

                'Me.m_aryCheckMuraPt.Add(TempstuCheck)
            Next
        End If

    End Sub
    Private Sub MuraShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LaadToMuraList()
        Me.view_Paint_1CCDMODE(0, 0)
    End Sub

    Private Sub picMuraShow_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMuraShow.MouseDown
        Dim sx, sy As Single
        Dim intx, inty As Integer
        Dim intRangeX, intRangeY As Integer
        Dim i As Integer
        Dim x, y As Integer
        Dim strTmp As String = ""
        Dim mp As New ClsMuraDf
        Dim Dfx As Integer = 0
        Dim Dfy As Integer = 0
        Dim DfstrTmp As String = ""

        For i = 0 To Me.dgvMuraShow.Rows.Count - 1
            Me.dgvMuraShow.Rows(i).DefaultCellStyle.BackColor = Color.White
        Next
     
        Try
            sx = CSng(Me.m_PanelSizeX / Me.picMuraShow.Size.Width)
            sy = CSng(Me.m_PanelSizeY / Me.picMuraShow.Size.Height)

            intx = CInt(e.X * sx)
            inty = CInt(e.Y * sy)

            intRangeX = CInt(ClickRange * sx) 'X axis range=+- 3 subpixel
            intRangeY = CInt(ClickRange * sy) 'Y axis range=+- 3 subpixel

         

            For i = 0 To Me.dgvMuraShow.Rows.Count - 1

                
                x = CInt(Me.dgvMuraShow.Rows(i).Cells(2).Value)
                y = CInt(Me.dgvMuraShow.Rows(i).Cells(3).Value)
                
                'Point
                If Math.Abs(x - intx) <= intRangeX And Math.Abs(y - inty) <= intRangeY Then
                    Me.dgvMuraShow.Rows(i).DefaultCellStyle.BackColor = Color.Lime
                    DfstrTmp = strTmp
                    Dfx = x
                    Dfy = y
                    'V-Band
                ElseIf Math.Abs(x - intx) <= intRangeX And y = -1 Then
                    Me.dgvMuraShow.Rows(i).DefaultCellStyle.BackColor = Color.Lime
                    DfstrTmp = strTmp
                    Dfx = x
                    Dfy = y
                    'H-Band
                ElseIf x = -1 And Math.Abs(y - inty) <= intRangeY Then
                    Me.dgvMuraShow.Rows(i).DefaultCellStyle.BackColor = Color.Lime
                    DfstrTmp = strTmp
                    Dfx = x
                    Dfy = y
                End If

            Next i


            Me.view_Paint_1CCDMODE(Dfx, Dfy) '標出mark線

        Catch ex As Exception
            'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
        End Try

    End Sub

    Private Sub dgvMuraShow_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMuraShow.CellClick
        Dim data, gate As Integer
        Dim i As Integer

        For i = 0 To Me.dgvMuraShow.Rows.Count - 1
            Me.dgvMuraShow.Rows(i).DefaultCellStyle.BackColor = Color.White
        Next

        data = Me.dgvMuraShow.Rows(e.RowIndex).Cells(2).Value
        gate = Me.dgvMuraShow.Rows(e.RowIndex).Cells(3).Value
        Me.view_Paint_1CCDMODE(data, gate)
        Me.dgvMuraShow.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Lime
    End Sub

    Private Sub picMuraShow_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMuraShow.MouseMove
        Dim sx, sy As Single
        Dim intx, inty As Integer
       
        sx = CSng(Me.m_PanelSizeX / Me.picMuraShow.Size.Width)
        sy = CSng(Me.m_PanelSizeY / Me.picMuraShow.Size.Height)

        intx = CInt(e.X * sx)
        inty = CInt(e.Y * sy)

        Me.m_ToolTipMsg.SetToolTip(Me.picMuraShow, CStr(intx) & "," & CStr(inty))

    End Sub

    Private Sub btnCanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanel.Click
        Me.m_aryMuraList.Clear()
        Me.m_aryMuraList.AddRange(Me.m_TemparyMuraList)
        Me.Close()
    End Sub

End Class

Class clsCheck
    Public Check As Boolean
   Public MuraType As String
    Public Data As Integer
    Public Gate As Integer
    Public MinX As Integer
    Public MinY As Integer
    Public MaxX As Integer
    Public MaxY As Integer
    Public Pattern As String
End Class
