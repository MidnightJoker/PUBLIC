Imports System.Threading
Imports System.IO
Imports AUO.SubSystemControl

Public Class Form1


    Private WithEvents m_SystemDispatcher As CSubSystemDispatcher   'SubSystem Control Server
    Private m_Request As CRequest
    Private f As New ArrayList
    Private m_CommandRun As Boolean
    Private m_ChipCommandInfo As String() '¬ö¿ý¦U­ÓChip©R¥O¬ö¿ý
    Private m_ClsConfig As ClsConfig
    Private m_blnRunCmd As Boolean = False
    Private m_RunCmdThread As System.Threading.Thread = Nothing
    Public Shared frmShowDefect As FrmShowDefect

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If File.Exists(Application.StartupPath & "\" & "ViewJudgerResultConfig.xml") Then
            Me.m_ClsConfig = ClsConfig.ReadXML(Application.StartupPath & "\" & "ViewJudgerResultConfig.xml")
        Else
            Me.m_ClsConfig = New ClsConfig
        End If

        Dim args() As String
        args = Environment.GetCommandLineArgs()
        Me.Text = Me.m_ClsConfig.MainViewName & " [Version]: " & System.Windows.Forms.Application.ProductVersion

        Me.m_SystemDispatcher = New CSubSystemDispatcher

        If args.Length > 1 Then
            Me.m_SystemDispatcher.CreateListener(args(1), CInt(args(2)))
            Me.m_SystemDispatcher.StartListen()
        Else
            Me.m_SystemDispatcher.CreateListener("127.0.0.1", 7878)
            Me.m_SystemDispatcher.StartListen()
        End If

        Me.m_blnRunCmd = False

        frmShowDefect = New FrmShowDefect
        frmShowDefect.Size = New Point(Me.m_ClsConfig.DefectFrmW, Me.m_ClsConfig.DefectFrmH)
        frmShowDefect.Show()
        frmShowDefect.Hide()
    End Sub

    Private Sub m_SystemDispatcher_RemoteConnectComing() Handles m_SystemDispatcher.RemoteConnectComing

        Me.UI_Lamp(eSystem.CtrlConnect)
    End Sub

    Private Sub m_SystemDispatcher_RemoteDisconnect() Handles m_SystemDispatcher.RemoteDisconnect
        Try

            Me.UI_Lamp(eSystem.CtrlDisConnect)
            Me.m_SystemDispatcher.Disconnect()

        Catch ex As System.Threading.ThreadAbortException
            '---don't care---
        Catch ex As Exception
            '---don't care---
        End Try
    End Sub

    Private Sub m_SystemDispatcher_ReceiveOccurError(ByVal ErrMessage As String) Handles m_SystemDispatcher.ReceiveOccurError
        Try

            'MessageBox.Show(ErrMessage)

        Catch ex As System.Threading.ThreadAbortException
            '---don't care---
        Catch ex As Exception
            '---don't care---
        End Try
    End Sub

    Private Sub m_SystemDispatcher_RemoteControlEventHandler(ByVal Request As CRequest) Handles m_SystemDispatcher.RemoteControl

        If Me.m_blnRunCmd Then
            Throw New Exception("Command is Running!")
            Exit Sub
        End If

        Me.m_Request = Request
        Me.m_blnRunCmd = True

        Try
            Me.m_RunCmdThread = New System.Threading.Thread(AddressOf Me.RunCommand)
            Me.m_RunCmdThread.SetApartmentState(ApartmentState.STA)
            Me.m_RunCmdThread.Start()

            Me.m_RunCmdThread.Join()
            Me.m_RunCmdThread = Nothing
            Me.m_blnRunCmd = False
            Me.m_SystemDispatcher.ReturnResponse(eResponseResult.OK, "", "", "", "", "", "", "", "", "")

        Catch ex As System.Threading.ThreadAbortException
            '---don't care---
        Catch ex As Exception
            Me.m_blnRunCmd = False
            'Throw New Exception(ex.Message)
        End Try

        'Try
        '    If Me.m_CommandRun = False Then
        '        Me.m_Request = Request
        '        Me.m_CommandRun = True
        '    Else
        '        'Throw New Exception("Command is Running!")
        '    End If

        '    Me.m_SystemDispatcher.ReturnResponse(eResponseResult.OK, "", "", "", "", "", "", "", "", "")

        'Catch ex As System.Threading.ThreadAbortException
        '    '---don't care---
        'Catch ex As Exception
        '    Me.m_SystemDispatcher.ReturnResponse(eResponseResult.ERR, ex.Message, "", "", "", "", "", "", "", "")
        'End Try
    End Sub

    'Public Sub ShowDefect(ByVal path As String, ByVal Location_x As Integer, ByVal Location_Y As Integer, ByVal winW As Single, ByVal winH As Single)
    '    frmShowDefect.LoadImage(path, Location_x, Location_Y, winW, winH)
    'End Sub

    'Public Sub NoDefectImage()
    '    frmShowDefect.NoImage()
    'End Sub

    'Public Sub ShowDefectFrm(ByVal Location_x As Integer, ByVal Location_y As Integer)
    '    frmShowDefect.Location = New Point(Location_x, Location_y)
    '    frmShowDefect.Show()
    'End Sub

    'Public Sub HideDefectFrm()
    '    frmShowDefect.Hide()
    'End Sub

    'Public Sub DefectFrmReSize(ByVal x As Integer, ByVal y As Integer)
    '    frmShowDefect.Size = New Point(x, y)
    'End Sub

    Private Sub RunCommand()
        Try
            Select Case Me.m_Request.Command

                Case "SET_VIEW"
                    SET_VIEW(Me.m_Request.Param1)

                Case "CLEAR_VIEW"
                    CLEAR_VIEW()

                Case "CLEAR_SINGALVIEW"
                    CLEAR_SINGALVIEW(Me.m_Request.Param1)

                Case "SET_VIEW_FORMAT"
                    SET_VIEW_FORMAT(Me.m_Request.Param1)

                Case "SET_CHIP_INFO"
                    SET_CHIP_INFO(Me.m_Request.Param1, Me.m_Request.Param2)

                Case "COPY_CHIP_INFO"
                    COPY_CHIP_INFO(Me.m_Request.Param1, Me.m_Request.Param2)

            End Select

        Catch ex As Exception
            Me.m_CommandRun = False
            'Throw New Exception(ex.Message)
        End Try

    End Sub

    Delegate Sub UI_SET_VIEWCallback(ByVal chipcnt As Integer)
    Private Sub SET_VIEW(ByVal chipcnt As Integer)

        If Me.InvokeRequired Then
            Me.Invoke(New UI_SET_VIEWCallback(AddressOf Me.SET_VIEW), New Object() {chipcnt})
        Else
            Dim i As Integer
            Dim offsetx, offsety As Integer

            ReDim Me.m_ChipCommandInfo(chipcnt)

            For i = 0 To chipcnt - 1
                Dim c As New FrmMain
                f.Add(c)
            Next

            Try
                offsetx = 0
                offsety = Me.btnCallSubView.Height

                For i = 0 To chipcnt - 1

                    'Dim f As New FrmMain
                    'offsety = Me.ToolStrip1.Height
                    CType(f(i), FrmMain).TopLevel = False
                    CType(f(i), FrmMain).Parent = Me
                    CType(f(i), FrmMain).Location = New Point(offsetx, offsety)
                    CType(f(i), FrmMain).AutoScrollPosition = New Point(0, 0)
                    CType(f(i), FrmMain).Show()

                    offsetx = offsetx + f(i).Width / 2

                    'Me.TopMost = True
                    'Me.TopMost = False
                    If i = 3 Then offsetx = 0
                    If i >= 3 Then offsety = FrmMain.Size.Height + Me.btnCallSubView.Height
                Next
                If chipcnt <= 4 Then
                    Me.AutoSize = True
                Else
                    Me.AutoScroll = True
                End If

                Me.Location = New Point(0, 0)
            Catch ex As Exception
                'Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Delegate Sub CLEAR_VIEWCallback()
    Private Sub CLEAR_VIEW()
        If Me.InvokeRequired Then
            Me.Invoke(New CLEAR_VIEWCallback(AddressOf Me.CLEAR_VIEW), New Object() {})
        Else
            Dim i As Integer
            For i = 0 To f.Count - 1
                CType(f(i), FrmMain).Close()
            Next
            f.Clear()
        End If
    End Sub

    Delegate Sub CLEAR_SINGALVIEWCallback(ByVal ChipViewNum As Integer)
    Private Sub CLEAR_SINGALVIEW(ByVal ChipViewNum As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New CLEAR_SINGALVIEWCallback(AddressOf Me.CLEAR_SINGALVIEW), New Object() {ChipViewNum})
        Else
            'CType(f(ChipViewNum), FrmMain).Close()
            f(ChipViewNum).Clear()
        End If
    End Sub

    Delegate Sub SET_VIEW_FORMATCallback(ByVal StrViewFormat As String)
    Private Sub SET_VIEW_FORMAT(ByVal StrViewFormat As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SET_VIEW_FORMATCallback(AddressOf Me.SET_VIEW_FORMAT), New Object() {StrViewFormat})
        Else
            Dim i As Integer = 0
            Dim ViewFormat() As String = StrViewFormat.Trim(";").Split(";")

            For i = 0 To f.Count - 1
                CType(f(i), FrmMain).Set_View_Foramt(ViewFormat(0), ViewFormat(1), ViewFormat(2), ViewFormat(3), ViewFormat(4), ViewFormat(5))
            Next

            Me.Width = 421
            Me.Height = 612
        End If

    End Sub

    Delegate Sub SET_CHIP_INFOCallback(ByVal ChipNo As Integer, ByVal StrChipInfo As String)
    Private Sub SET_CHIP_INFO(ByVal ChipNo As Integer, ByVal StrChipInfo As String)

        If Me.InvokeRequired Then
            Me.Invoke(New SET_CHIP_INFOCallback(AddressOf Me.SET_CHIP_INFO), New Object() {ChipNo, StrChipInfo})
        Else
            Me.m_ChipCommandInfo(ChipNo) = StrChipInfo

            'StrChipInfo = FactoryNo;PanelID;ResultPathName;MQRank;CSTRank;AGSRank;MainDefect;MainDefect;LogJudgeResultMsg;MainDefectData;MainDefectGate;Data;Gate;CCDMode
            Dim ChipInfo() As String = StrChipInfo.Trim(";").Split(";")
            Try
                CType(f(ChipNo), FrmMain).Set_Chip_Info(ChipInfo(0), ChipInfo(1), ChipInfo(2), ChipInfo(3), ChipInfo(4), ChipInfo(5), ChipInfo(6), ChipInfo(7), ChipInfo(8), ChipInfo(9), ChipInfo(10), ChipInfo(11), ChipInfo(12), ChipInfo(13))
            Catch ex As Exception
                'Throw New Exception(ex.Message)
            End Try
        End If

    End Sub


    Delegate Sub COPY_CHIP_INFOCallback(ByVal ScrNo As String, ByVal DstNo As String)
    Private Sub COPY_CHIP_INFO(ByVal ScrNo As String, ByVal DstNo As String)
        If Me.InvokeRequired Then
            Me.Invoke(New COPY_CHIP_INFOCallback(AddressOf Me.COPY_CHIP_INFO), New Object() {ScrNo, DstNo})
        Else
            Dim i As Integer = 0
            Dim strScrNo() As String = ScrNo.Trim("@").Split("@")
            Dim strDstNo() As String = DstNo.Trim("@").Split("@")
            Dim ChipInfo() As String
            Dim ForwardStrChipInfo As String
            Try
                For i = 0 To strScrNo.Length - 1
                    ChipInfo = Me.m_ChipCommandInfo(CInt(strScrNo(i))).Trim(";").Split(";")
                    ForwardStrChipInfo = Me.m_ChipCommandInfo(CInt(strScrNo(i)))
                    Me.m_ChipCommandInfo(CInt(strScrNo(i)) + 1) = ForwardStrChipInfo
                    CType(f(CInt(strDstNo(i))), FrmMain).Set_Chip_Info(ChipInfo(0), ChipInfo(1), ChipInfo(2), ChipInfo(3), ChipInfo(4), ChipInfo(5), ChipInfo(6), ChipInfo(7), ChipInfo(8), ChipInfo(9), ChipInfo(10), ChipInfo(11), ChipInfo(12))
                Next
            Catch ex As Exception
                ''Throw New Exception(ex.Message)
            End Try
        End If
    End Sub


    Delegate Sub UI_Lamp_DataCallback(ByVal msg As eSystem)
    Private Sub UI_Lamp(ByVal msg As eSystem)

        If Me.tstxtControlStatus.InvokeRequired Then
            Me.Invoke(New UI_Lamp_DataCallback(AddressOf Me.UI_Lamp), New Object() {msg})
        Else
            Select Case msg
                Case eSystem.CtrlDisConnect
                    Me.tstxtControlStatus.Text = "Controller DisConnect"
                    Me.tstxtControlStatus.BackColor = Color.Yellow

                Case eSystem.CtrlConnect
                    Me.tstxtControlStatus.Text = "Controller Connected"
                    Me.tstxtControlStatus.BackColor = Color.Lime
            End Select
            Me.Update()
        End If


    End Sub

    Public Enum eSystem As Byte
        'Judger
        Init = 0
        Run
        Alarm

        'Mq
        MqConnect
        MqDisConnect
        McmqConnect
        McmqDisConnect
        'Controller
        CtrlConnect
        CtrlDisConnect
    End Enum

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.m_SystemDispatcher IsNot Nothing Then
            Me.m_SystemDispatcher.Disconnect()
            Me.m_SystemDispatcher.StopListen()
        End If
    End Sub

    Private Sub btnCallSubView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCallSubView.Click
        Dim f As New FrmMain()
        f.TopLevel = False
        f.Parent = Me
        f.Location = New Point(0, Me.btnCallSubView.Height)
        f.Show()
    End Sub

    Private Sub Form1_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
#If DEBUG Then
        MessageBox.Show("Key up")
#End If
    End Sub
End Class