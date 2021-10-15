Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Public Class FrmMain

    Private m_AllFiles() As String
    Private m_aryResult As New ClsJRdataArray

    Private m_ViewerLog As ClsLogRecorder

    Private m_PanelID As String
    Private m_FilePath As String
    Private m_AGSRank As String
    Private m_CSTRank As String
    Private m_MainDefect As String
    Private m_PanelW As Integer
    Private m_PanelH As Integer
    Private m_PanelBMW As Integer
    Private m_PanelBMH As Integer
    Private m_EnablePanelBMArea As Boolean
    Private m_ToolTipMsg As ToolTip

    Private m_MaskIDAGSRank As String
    Private m_MaskIDCSTRank As String
    Private m_MaskIDMainDefect As String


    Private m_Config As ClsConfig
    Private m_ConfigFileName As String
    Private m_NeedSave As Boolean = False
    Private m_CCDModeType As String = "1"

    Private Const CONFIG_FILE_NAME As String = "ViewJudgerResultConfig.xml"

    Private m_FirstTimeCreateDefectList = False

    Private Const IdxType As Integer = 0
    Private Const IdxArea As Integer = 3
    Private Const IdxData As Integer = 4
    Private Const IdxGate As Integer = 5
    Private Const IdxPType As Integer = 6
    Private Const IdxHistory As Integer = 11   
    Private Const IdxCCDNo As Integer = 12
    Private Const IdxImageFilePath As Integer = 14
    Private Const IdxFileName As Integer = 15

    Private Const IdxMuraType As Integer = 0
    Private Const IdxMuraData As Integer = 1
    Private Const IdxMuraGate As Integer = 2
    Private Const IdxMuraArea As Integer = 3
    Private Const IdxMuraJND As Integer = 4
    Private Const IdxMuraRank As Integer = 5
    Private Const IdxMuraCenterX As Integer = 6
    Private Const IdxMuraCenterY As Integer = 7
    Private Const IdxMinX As Integer = 8
    Private Const IdxMinY As Integer = 9
    Private Const IdxMaxX As Integer = 10
    Private Const IdxMaxY As Integer = 11
    Private Const IdxScore As Integer = 12
    Private Const IdxMuraPattern As Integer = 13
    'Private Const IdxMuraWidth As Integer = 14
    Private Const IdxMuraName As Integer = 14
    Private Const IdxMuraChipID As Integer = 15
    Private Const IdxMuraCCDNo As Integer = 16
    Private Const IdxMuraImageFilePath As Integer = 18
    Private Const IdxMuraFileName As Integer = 19
    Private Const IdxMuraBlock As Integer = 19
    Private Const IdxMuraCheck As Integer = 20
    Private Const ClickRange As Integer = 3 'Data/Gate axis range=+- 3 subpixel
    Private m_ViewResultInfo As String = ""

    Private sMeasureResult As List(Of String) = New List(Of String)
    Private sMeasureParam As String()
    Private M_DlgShowDefectFrm As List(Of FrmShowDefect) = New List(Of FrmShowDefect)
    Private F_DlgShowDefectFrm As List(Of FrmShowDefect) = New List(Of FrmShowDefect)
    Private _M_DefectFrmPositionList As List(Of Point) = New List(Of Point)
    Private _F_DefectFrmPositionList As List(Of Point) = New List(Of Point)
    Private _tmpPoint As New Point
    Private MeasureAVG As List(Of String) = New List(Of String)
    Private Measure5U As List(Of String) = New List(Of String)
    Private Measure13U As List(Of String) = New List(Of String)

#Region "Defect Count"
    Private intGSDPcount As Integer
    Private intDP1count As Integer
    Private intDP2count As Integer
    Private intDP3count As Integer
    Private intDPxcount As Integer
    Private intDPncount As Integer

    Private intGSBPcount As Integer
    Private intBP1count As Integer
    Private intBP2count As Integer
    Private intBP3count As Integer
    Private intBPxcount As Integer
    Private intBPncount As Integer

    Private intBPDP2count As Integer
    Private intBPDP3count As Integer
    Private intBPDPxcount As Integer
    Private intBPDPncount As Integer

    Private intXLcount As Integer
    Private intVLcount As Integer
    Private intHLcount As Integer
    Private intVOLcount As Integer
    Private intHOLcount As Integer

    Private intFrameGluecount As Integer
    Private intSmallBubblecount As Integer
    Private intMiddleBubblecount As Integer
    Private intLargeBubblecount As Integer
    Private intSGradeBubblecount As Integer

    Private intMURAcount As Integer
    Private intCPcount As Integer
    Private intSBPcount As Integer
    Private intOmitBPcount As Integer
    Private MuraJudgePartition As New ClsMuraJudgePart

#End Region

#Region "Pen define"
    Private penBLDP As New Pen(Color.White)
    Private penDP1 As New Pen(Color.White)
    Private penDP2 As New Pen(Color.White)
    Private penDP3 As New Pen(Color.White)
    Private penDPx As New Pen(Color.White)
    Private penDPn As New Pen(Color.White)

    Private penGSBP As New Pen(Color.White)
    Private penBP1 As New Pen(Color.White)
    Private penBP2 As New Pen(Color.White)
    Private penBP3 As New Pen(Color.White)
    Private penBPx As New Pen(Color.White)
    Private penBPn As New Pen(Color.White)

    Private penBPDP2 As New Pen(Color.White)
    Private penBPDP3 As New Pen(Color.White)
    Private penBPDPx As New Pen(Color.White)
    Private penBPDPn As New Pen(Color.White)

    Private penXL As New Pen(Color.White)
    Private penVL As New Pen(Color.White)
    Private penHL As New Pen(Color.White)
    Private penVOL As New Pen(Color.White)
    Private penHOL As New Pen(Color.White)

    Private penFG As New Pen(Color.Blue)
    Private penBB As New Pen(Color.Blue)

    Private penMURA As New Pen(Color.White)
    Private penCP As New Pen(Color.White)
    Private penSBP As New Pen(Color.White)
    Private penOMITBP As New Pen(Color.White)

    Private penMark As New Pen(Color.White)
#End Region

    Private Sub FrmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.m_NeedSave Then Me.SaveConfig()
    End Sub

    Private Sub tsmOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOpen.Click
        Dim dlg As OpenFileDialog
        Dim fsReader As StreamReader
        Dim strfsText As String
        Dim strfsLine As String()
        Dim strViewResultInfo As String() = Nothing


        dlg = New OpenFileDialog

        dlg.Filter = "JudgeResult(*.txt)|*.txt|(*.*)|*.*"
        dlg.FilterIndex = 1
        dlg.FileName = ""
        dlg.Title = "Open JudgeResult File"
        dlg.InitialDirectory = Me.m_Config.InitialFolderPath & DateTime.Today.Year & DateTime.Today.Month & DateTime.Today.Day
        dlg.Multiselect = True
        dlg.RestoreDirectory = True

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            Me.m_AllFiles = dlg.FileNames
            Me.m_PanelID = Me.m_AllFiles(0).Substring(Me.m_AllFiles(0).LastIndexOf("\") + 1, Me.m_AllFiles(0).LastIndexOf(".txt") - Me.m_AllFiles(0).LastIndexOf("\") - 1)
            Me.m_FilePath = Me.m_AllFiles(0).Substring(0, Me.m_AllFiles(0).LastIndexOf("\") + 1)

            fsReader = New StreamReader(Me.m_AllFiles(0))
            strfsText = fsReader.ReadToEnd
            strfsLine = strfsText.Split(vbCrLf)

            For i As Integer = 0 To strfsLine.Length - 1
                If strfsLine(i).Trim.ToUpper = "VIEWRESULTINFO" Then
                    strViewResultInfo = strfsLine(i + 1).Trim.Split(";")
                    strViewResultInfo(2) = Me.m_AllFiles(0)
                    Me.m_ViewResultInfo = strViewResultInfo(0) & ";" & strViewResultInfo(1) & ";" & Me.m_AllFiles(0) & ";" & strViewResultInfo(3) & ";" & strViewResultInfo(4) & ";" & _
                                        strViewResultInfo(5) & ";" & strViewResultInfo(6) & ";" & strViewResultInfo(7) & ";" & strViewResultInfo(8) & ";" & strViewResultInfo(9) & ";" & _
                                        strViewResultInfo(10) & ";" & strViewResultInfo(11) & ";" & strViewResultInfo(12)
                    Me.m_ViewResultInfo = strfsLine(i + 1).Trim
                    Exit For
                End If
            Next
            fsReader.Close()
            Me.m_PanelBMW = strViewResultInfo(10)
            Me.m_PanelBMH = strViewResultInfo(11)
            Me.Set_Chip_Info(strViewResultInfo(0), strViewResultInfo(1), strViewResultInfo(2), strViewResultInfo(3), strViewResultInfo(4), strViewResultInfo(5), strViewResultInfo(6), strViewResultInfo(7), strViewResultInfo(8), strViewResultInfo(9), strViewResultInfo(10), strViewResultInfo(11), strViewResultInfo(12))
            Me.SaveConfig()
        End If
    End Sub

    Private Sub Run()
        Dim i As Integer
        Dim strFileText As String = ""
        Dim fileReader As StreamReader
        Dim objJRdata As ClsJRdata

        Me.m_aryResult.Clear()
        Me.teeView.Nodes.Clear()
        Me.CountClear()
        Me.rtbJudgeResultFile.Text = ""
        Me.picDfImg.Image = Nothing

        Try
            For i = 0 To Me.m_AllFiles.Length - 1
                If File.Exists(Me.m_AllFiles(i)) Then

                    fileReader = New StreamReader(Me.m_AllFiles(i))
                    objJRdata = New ClsJRdata
                    objJRdata.FileName = Me.m_AllFiles(i)
                    objJRdata.Text = fileReader.ReadToEnd
                    fileReader.Close()

                    Me.teeView.Nodes.Add(Path.GetFileName(objJRdata.FileName))
                    Me.objJudgeResultFile(objJRdata, teeView.Nodes(i))

                    Me.m_aryResult.Add(objJRdata)
                End If
            Next

            Me.CountUpdate()
            Me.teeView.ExpandAll()
            Me.view_Paint()

        Catch ex As Exception
            'MessageBox.Show(Format(Now) + vbTab + Me.m_PanelID + vbCrLf + ex.StackTrace, "ViewJudgeResult Error")
        End Try
    End Sub

    '程式開啟時秀MainDefect
   
    Delegate Sub UI_MainDefectShowCallback(ByVal MainX As Integer, ByVal MainY As Integer)
    Private Sub MainDefectShow(ByVal MainX As Integer, ByVal MainY As Integer)

        If Me.picDfImg.InvokeRequired Then
            Me.Invoke(New UI_MainDefectShowCallback(AddressOf Me.MainDefectShow), New Object() {MainX, MainY})
        Else
            Dim intRangeX, intRangeY As Integer
            Dim i, j As Integer
            Dim f, m, n, x, y As Integer
            Dim strTmp As String = ""
            Dim p As New ClsFuncDf
            Dim mp As New ClsMuraDf
            Dim Dfx As Integer = 0
            Dim Dfy As Integer = 0
            Dim DfstrTmp As String = ""

            Try
                For j = 0 To Me.teeView.GetNodeCount(False) - 1
                    For i = 0 To Me.teeView.Nodes(j).GetNodeCount(False) - 1

                        strTmp = Me.teeView.Nodes(j).Nodes(i).Text
                        f = strTmp.IndexOf(",")
                        If f <> -1 Then
                            m = strTmp.IndexOf("(")
                            n = strTmp.IndexOf(")")

                            x = CInt(strTmp.Substring(m + 1, f - m - 1))
                            y = CInt(strTmp.Substring(f + 1, n - f - 1))
                        End If

                        'Point
                        If Math.Abs(x - MainX) <= intRangeX And Math.Abs(y - MainY) <= intRangeY Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'V-Line
                        ElseIf Math.Abs(x - MainX) <= intRangeX And y = -1 Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'H-Line
                        ElseIf x = -1 And Math.Abs(y - MainY) <= intRangeY Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'X-Short
                        ElseIf (strTmp.ToUpper.IndexOf("X_SHORT") <> -1) And (Math.Abs(x - MainX) <= intRangeX Or Math.Abs(y - MainY) <= intRangeY) Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                        End If

                    Next i
                Next j

                Me.view_Paint(Dfx, Dfy) '標出mark線

                'show defect image
                Dim DefectImagePath As String = ""
                For j = 0 To Me.m_aryResult.Count - 1

                    If DfstrTmp.IndexOf("Mura") <> -1 Then
                        For i = 0 To Me.m_aryResult.Index(0).MuraCount - 1
                            mp = Me.m_aryResult.Index(0).GetMuraDf(i)
                            If mp.Data = Dfx And mp.Gate = Dfy Then
                                DefectImagePath = mp.ImageFilePath + mp.FileName
                            End If
                        Next

                    Else
                        For i = 0 To Me.m_aryResult.Index(0).FuncCount - 1
                            p = Me.m_aryResult.Index(0).GetFuncDf(i)
                            If p.Data = Dfx And p.Gate = Dfy Then
                                DefectImagePath = p.ImageFilePath + p.FileName
                            End If
                        Next

                    End If
                Next

            Catch ex As Exception
                'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
            End Try
        End If
    End Sub

    '---讀取defect檔案---
    Private Sub objJudgeResultFile(ByRef objJRdata As ClsJRdata, ByVal teeNode As TreeNode)
        Dim i, j As Integer
        Dim strLine() As String
        Dim strElement() As String
        Dim func As ClsFuncDf
        Dim mura As ClsMuraDf
        Dim iFilePathIndex As Integer = 0
        Dim iFileNameIndex As Integer = 0

        strLine = objJRdata.Text.Split(vbCrLf)

        '---Search Func Title------------------------------
        For i = 0 To strLine.Length - 1
            strElement = strLine(i).Split(vbTab)
            If strElement(0).Trim.ToUpper = "TYPE" Then Exit For
        Next i

        If strElement IsNot Nothing Then
            For index As Integer = 0 To strElement.Length - 1
                If strElement(index).IndexOf("ImageFilePath") <> -1 Then iFilePathIndex = index
                If strElement(index).IndexOf("FileName") <> -1 Then iFileNameIndex = index
            Next
        End If

        If i >= strLine.Length - 1 Then Exit Sub 'No Type element on line-text

        '---start read Func defect-------------------------
        j = i + 1
        For i = j To strLine.Length - 1

            strElement = strLine(i).Split(vbTab)
            If strElement(0).Trim = "" Then Exit For

            func = New ClsFuncDf

            If strElement(IdxData).ToUpper.Trim = "NULL" Then
                func.Data = -1
            Else
                func.Data = CInt(strElement(IdxData))
            End If

            If strElement(IdxGate).ToUpper.Trim = "NULL" Then
                func.Gate = -1
            Else
                func.Gate = CInt(strElement(IdxGate))
            End If

            If strElement.Length > IdxArea Then
                If strElement(IdxGate).ToUpper.Trim = "NULL" Then
                    func.Area = 0
                Else
                    func.Area = CInt(strElement(IdxArea))
                End If
            End If

            If strElement.Length > IdxHistory Then
                If strElement(IdxHistory).ToUpper.Trim = "NULL" Then
                    func.History = ""
                Else
                    func.History = strElement(IdxHistory)
                End If
            End If

            If strElement.Length > IdxCCDNo Then
                If strElement(IdxCCDNo).ToUpper.Trim = "NULL" Then
                    func.CCDNo = ""
                Else
                    func.CCDNo = strElement(IdxCCDNo)
                End If
            End If

            If strElement.Length > IdxImageFilePath Then
                If strElement(IdxCCDNo).ToUpper.Trim = "NULL" Then
                    func.ImageFilePath = ""
                Else
                    'func.ImageFilePath = strElement(IdxImageFilePath)
                    func.ImageFilePath = strElement(iFilePathIndex)
                End If
            End If

            If strElement.Length > IdxFileName Then
                If strElement(IdxCCDNo).ToUpper.Trim = "NULL" Then
                    func.FileName = ""
                Else
                    'func.FileName = strElement(IdxFileName)
                    func.FileName = strElement(iFileNameIndex)
                End If
            End If

            If strElement(7).ToUpper.Trim = "NULL" Then
                func.Pattern = -1
            Else
                func.Pattern = CStr(strElement(7))
            End If

            Select Case strElement(IdxType).ToUpper.Trim.Replace("-", "_").Replace(" ", "_")

                Case "FRAME_GLUE"
                    func.Type = defectType.FRAME_GLUE
                    Me.intFrameGluecount += 1

                Case "SMALL_BUBBLE"
                    func.Type = defectType.SMALL_BUBBLE
                    Me.intSmallBubblecount += 1

                Case "MIDDLE_BUBBLE"
                    func.Type = defectType.MIDDLE_BUBBLE
                    Me.intMiddleBubblecount += 1

                Case "LARGE_BUBBLE"
                    func.Type = defectType.LARGE_BUBBLE
                    Me.intLargeBubblecount += 1

                Case "S_GRADE_BUBBLE"
                    func.Type = defectType.S_GRADE_BUBBLE
                    Me.intSGradeBubblecount += 1

                Case "X_SHORT"
                    func.Type = defectType.X_SHORT
                    Me.intXLcount += 1

                Case "H_LINE"
                    func.Type = defectType.H_LINE
                    Me.intHLcount += 1

                Case "V_LINE"
                    func.Type = defectType.V_LINE
                    Me.intVLcount += 1

                Case "H_OPEN"
                    func.Type = defectType.H_OPEN
                    Me.intHOLcount += 1

                Case "V_OPEN"
                    func.Type = defectType.V_OPEN
                    Me.intVOLcount += 1

                Case "POINT_COUNT"
                    func.Type = defectType.POINT_COUNT
                    'not thing

                Case "GROUP_SMALL_BP"
                    func.Type = defectType.GROUP_SMALL_BP
                    Me.intGSBPcount += 1

                Case "BP", "WEAK_BP"
                    func.Type = defectType.BP
                    Me.intBP1count += 1

                Case "BP_PAIR"
                    func.Type = defectType.BP_PAIR
                    Me.intBP2count += 1

                Case "3BP_ADJ"
                    func.Type = defectType.BP_ADJ
                    Me.intBP3count += 1

                Case "BP_CLUSTER"
                    func.Type = defectType.BP_CLUSTER
                    Me.intBPxcount += 1

                Case "BP_NEAR"
                    func.Type = defectType.BP_NEAR
                    Me.intBPncount += 1

                Case "BACK_LIGHT_DP"
                    func.Type = defectType.BACK_LIGHT_DP
                    Me.intGSDPcount += 1

                Case "DP"
                    func.Type = defectType.DP
                    Me.intDP1count += 1

                Case "DP_PAIR"
                    func.Type = defectType.DP_PAIR
                    Me.intDP2count += 1

                Case "3DP_ADJ"
                    func.Type = defectType.DP_ADJ
                    Me.intDP3count += 1

                Case "DP_CLUSTER"
                    func.Type = defectType.DP_CLUSTER
                    Me.intDPxcount += 1

                Case "DP_NEAR"
                    func.Type = defectType.DP_NEAR
                    Me.intDPncount += 1

                Case "BPDP_PAIR"
                    func.Type = defectType.BPDP_PAIR
                    Me.intBPDP2count += 1

                Case "3BPDP_ADJ"
                    func.Type = defectType.BPDP_ADJ
                    Me.intBPDP3count += 1

                Case "BPDP_CLUSTER"
                    func.Type = defectType.BPDP_CLUSTER
                    Me.intBPDPxcount += 1

                Case "BPDP_NEAR"
                    func.Type = defectType.BPDP_NEAR
                    Me.intBPDPncount += 1

                Case "CELL_PARTICLE"
                    func.Type = defectType.CP
                    Me.intCPcount += 1

                Case "SMALL_BP"
                    func.Type = defectType.SBP
                    Me.intSBPcount += 1

                Case "MURA", "AROUND_GAP_MURA", "V_BAND_MURA", "H_BAND_MURA" 'old string
                    func.Type = defectType.MURA
                    'not thing

                Case "BLACK_MURA", "BLACK_SPOT", "BLACK_AGM", "BLACK_V_BAND", "BLACK_H_BAND"
                    func.Type = defectType.MURA
                    'not thing

                Case "WHITE_MURA", "WHITE_SPOT", "WHITE_AGM", "WHITE_V_BAND", "WHITE_H_BAND"
                    func.Type = defectType.MURA
                    'not thing

                Case "H_BLOCK"
                    func.Type = defectType.H_BLOCK
                    'Me.intHBcount += 1

                Case "V_BLOCK"
                    func.Type = defectType.V_BLOCK
                    'Me.intVBcount += 1

                Case "OMIT_BP"
                    func.Type = defectType.OMIT_BP
                    Me.intOmitBPcount += 1

                Case "LUMINANCE"
                    func.Type = defectType.LUMINANCE

                Case Else
                    Dim tmp As String() = strElement(IdxType).Split("(")
                    If tmp(0).ToUpper.IndexOf("DF_IN_PATTERN") <> -1 Then
                        func.Type = defectType.DF_IN_PATTERN
                    Else
                        func.Type = defectType.NULL
                        teeNode.Nodes.Add("UnDefine Type:" & strElement(IdxType).Trim)
                    End If
            End Select

            If Not (func.Type = defectType.NULL Or func.Type = defectType.POINT_COUNT Or func.Type = defectType.MURA) Then
                teeNode.Nodes.Add(strElement(IdxType).Trim & "(" & func.Data & "," & func.Gate & ")")
                objJRdata.AddFunc(func)
                teeNode.Nodes.Item(teeNode.Nodes.Count - 1).ToolTipText = "(" & func.Data & "," & func.Gate & ") " & " " & func.ImageFilePath + func.FileName
            End If
        Next

        iFilePathIndex = 0
        iFileNameIndex = 0

        '---Search Mura Title------------------------------
        j = i + 1
        For i = j To strLine.Length - 1
            strElement = strLine(i).Split(vbTab)
            If strElement(0).Trim.ToUpper = "MURA TYPE" Then Exit For
        Next

        If strElement IsNot Nothing Then
            For index As Integer = 0 To strElement.Length - 1
                If strElement(index).IndexOf("ImageFilePath") <> -1 Then iFilePathIndex = index
                If strElement(index).IndexOf("FileName") <> -1 Then iFileNameIndex = index
            Next
        End If

        If i >= strLine.Length - 1 Then Exit Sub 'No Type element on line-text

        '---start read Mura defect-------------------------
        j = i + 1
        For i = j To strLine.Length - 1

            strElement = strLine(i).Split(vbTab)
            If strElement(0).Trim = "" Then Exit For

            mura = New ClsMuraDf
            mura.MuraType = strElement(IdxMuraType).Trim(" ")
            mura.Data = CInt(strElement(IdxMuraData))
            mura.Gate = CInt(strElement(IdxMuraGate))
            mura.JND = CSng(strElement(IdxMuraJND))
            mura.Rank = strElement(IdxMuraRank).Trim(" ")
            mura.X = CInt(strElement(IdxMuraCenterX))
            mura.Y = CInt(strElement(IdxMuraCenterY))
            mura.MinX = CInt(strElement(IdxMinX))
            mura.MinY = CInt(strElement(IdxMinY))
            mura.MaxX = CInt(strElement(IdxMaxX))
            mura.MaxY = CInt(strElement(IdxMaxY))
            mura.MuraName = strElement(IdxMuraName)
            mura.Pattern = strElement(IdxMuraPattern)
            'mura.Width = strElement(IdxMuraWidth)

            If strElement.Length > IdxMuraCCDNo Then
                If strElement(IdxMuraCCDNo).ToUpper.Trim = "NULL" Then
                    mura.CCDNo = ""
                Else
                    mura.CCDNo = CStr(strElement(IdxMuraCCDNo))
                End If
            End If

            If strElement.Length > IdxMuraImageFilePath Then
                If strElement(IdxMuraImageFilePath).ToUpper.Trim = "NULL" Then
                    mura.ImageFilePath = ""
                Else
                    'mura.ImageFilePath = strElement(IdxMuraImageFilePath)
                    mura.ImageFilePath = strElement(iFilePathIndex)
                End If
            End If

            If strElement.Length > IdxMuraFileName Then
                If strElement(IdxMuraFileName).ToUpper.Trim = "NULL" Then
                    mura.FileName = ""
                Else
                    'mura.FileName = strElement(IdxMuraFileName)
                    mura.FileName = strElement(iFileNameIndex)
                End If
            End If

            'If strElement.Length > IdxMuraFileName + 1 Then
            '    If strElement(IdxMuraFileName + 1).ToUpper.Trim = "NULL" Then
            '        mura.FileName = ""
            '    Else
            '        mura.FileName = strElement(IdxMuraFileName + 1)
            '    End If
            'End If
            If strElement.Length > IdxMuraCheck Then
                If strElement(IdxMuraCheck).ToUpper.Trim = "" Then
                    mura.CheckMura = "True"
                Else
                    Select Case strElement(IdxMuraCheck)
                        Case "1"
                            MuraJudgePartition.Partition1 = True
                            mura.CheckMura = "True"
                            mura.Block = 1
                        Case "2"
                            MuraJudgePartition.Partition2 = True
                            mura.CheckMura = "True"
                            mura.Block = 2
                        Case "3"
                            MuraJudgePartition.Partition3 = True
                            mura.CheckMura = "True"
                            mura.Block = 3
                        Case "4"
                            MuraJudgePartition.Partition4 = True
                            mura.CheckMura = "True"
                            mura.Block = 4
                        Case "5"
                            MuraJudgePartition.Partition5 = True
                            mura.CheckMura = "True"
                            mura.Block = 5
                        Case "6"
                            MuraJudgePartition.Partition6 = True
                            mura.CheckMura = "True"
                            mura.Block = 6
                        Case "7"
                            MuraJudgePartition.Partition7 = True
                            mura.CheckMura = "True"
                            mura.Block = 7
                        Case "8"
                            MuraJudgePartition.Partition8 = True
                            mura.CheckMura = "True"
                            mura.Block = 8
                        Case "9"
                            MuraJudgePartition.Partition9 = True
                            mura.CheckMura = "True"
                            mura.Block = 9
                        Case Else
                            mura.CheckMura = strElement(IdxMuraCheck)
                    End Select
                End If
            End If

            Me.intMURAcount += 1

            teeNode.Nodes.Add("Mura" & "(" & mura.Data & "," & mura.Gate & ")" & " = " & CheckDefectName(mura.MuraType.Replace(vbLf, "")) & "[" & mura.Pattern & "]")
            objJRdata.AddMura(mura)
            teeNode.Nodes.Item(teeNode.Nodes.Count - 1).ToolTipText = "(" & mura.Data & "," & mura.Gate & ")" & "; " & CheckDefectName(mura.MuraType.Replace(vbLf, "")) & "; " & mura.MaxX & "," & mura.MaxY & "," & mura.MinX & "," & mura.MinY & "; " & mura.ImageFilePath & mura.FileName
        Next
    End Sub

    Private Sub GetBlockInfomation(ByVal MuraBlock As Integer, ByRef Data As Integer, ByRef Gate As Integer, ByRef BlockWidth As Integer, ByRef BlockHeight As Integer)
        If MuraJudgePartition.TotalPartition = 1 Then
            BlockWidth = m_PanelW - 10
            BlockHeight = m_PanelH - 5
            Data = 0
            Gate = 0
        ElseIf MuraJudgePartition.TotalPartition = 2 Then
            BlockWidth = m_PanelW / 2 - 10
            BlockHeight = m_PanelH
            Select Case MuraBlock
                Case "1"
                    Data = 0
                    Gate = 0
                Case "2"
                    Data = m_PanelW / 2
                    Gate = 0
            End Select
        ElseIf MuraJudgePartition.TotalPartition = 4 Then
            BlockWidth = m_PanelW / 2 - 10
            BlockHeight = m_PanelH / 2 - 5
            Select Case MuraBlock
                Case "1"
                    Data = 0
                    Gate = 0
                Case "2"
                    Data = m_PanelW / 2
                    Gate = 0
                Case "3"
                    Data = m_PanelW / 2
                    Gate = m_PanelH / 2
                Case "4"
                    Data = 0
                    Gate = m_PanelH / 2
            End Select
        ElseIf MuraJudgePartition.TotalPartition = 6 Then
            BlockWidth = m_PanelW / 3 - 10
            BlockHeight = m_PanelH / 2 - 5
            Select Case MuraBlock
                Case "1"
                    Data = 0
                    Gate = 0
                Case "2"
                    Data = m_PanelW / 3
                    Gate = 0
                Case "3"
                    Data = m_PanelW / 3 * 2
                    Gate = 0
                Case "4"
                    Data = m_PanelW / 3 * 2
                    Gate = m_PanelH / 2
                Case "5"
                    Data = m_PanelW / 3
                    Gate = m_PanelH / 2
                Case "6"
                    Data = 0
                    Gate = m_PanelH / 2
            End Select
        ElseIf MuraJudgePartition.TotalPartition = 9 Then
            BlockWidth = m_PanelW / 3 - 10
            BlockHeight = m_PanelH / 3 - 5
            Select Case MuraBlock
                Case "1"
                    Data = 0
                    Gate = 0
                Case "2"
                    Data = m_PanelW / 3
                    Gate = 0
                Case "3"
                    Data = m_PanelW / 3 * 2
                    Gate = 0
                Case "4"
                    Data = m_PanelW / 3 * 2
                    Gate = m_PanelH / 3
                Case "5"
                    Data = m_PanelW / 3
                    Gate = m_PanelH / 3
                Case "6"
                    Data = 0
                    Gate = m_PanelH / 3
                Case "7"
                    Data = 0
                    Gate = m_PanelH / 3 * 2
                Case "8"
                    Data = m_PanelW / 3
                    Gate = m_PanelH / 3 * 2
                Case "9"
                    Data = m_PanelW / 3 * 2
                    Gate = m_PanelH / 3 * 2
            End Select
        End If
    End Sub
    Private Function CheckRotateData(ByVal d As Integer) As Integer
        Dim i As Integer

        i = d
        If Me.chkRotate.Checked And d > 0 Then
            If Me.m_EnablePanelBMArea Then
                i = Me.m_PanelBMW - d
            Else
                i = Me.m_PanelW - d
            End If
        End If
        Return i
    End Function

    Private Function CheckRotateGate(ByVal d As Integer) As Integer
        Dim i As Integer

        i = d
        If Me.chkRotate.Checked And d > 0 Then
            If Me.m_EnablePanelBMArea Then
                i = Me.m_PanelBMH - d
            Else
                i = Me.m_PanelH - d
            End If
        End If
        Return i

    End Function

    Private Sub view_DefectWinShow(Optional ByVal intMarkX As Integer = 0, Optional ByVal intMarkY As Integer = 0) 'CCDMODE Select
        Dim mousepos As New Point(intMarkX, intMarkY)
        For Index As Integer = 0 To _M_DefectFrmPositionList.Count - 1
            If _M_DefectFrmPositionList(Index) = mousepos Then
                M_DlgShowDefectFrm(Index).Show()
            Else
                M_DlgShowDefectFrm(Index).Hide()
            End If
        Next

        For Index As Integer = 0 To _F_DefectFrmPositionList.Count - 1
            If _F_DefectFrmPositionList(Index) = mousepos Then
                F_DlgShowDefectFrm(Index).Show()
            Else
                F_DlgShowDefectFrm(Index).Hide()
            End If
        Next

    End Sub
    '---繪defect---
    Private Sub view_Paint(Optional ByVal intMarkX As Integer = 0, Optional ByVal intMarkY As Integer = 0) 'CCDMODE Select

        '9CCD排列方式                   '4CCD排列方式                   '3CCD排列方式                   '2CCD排列方式                          '1CCD排列方式
        '''''''''''''''''''''''''       '''''''''''''''''''''''''       '''''''''''''''''''''''''       '''''''''''''''''''''''''              '''''''''''''''''''''''''
        '   1   '   2   '   3   '       '     1     '     3     '       '       '       '       '       '           '           '              '                       '
        '''''''''''''''''''''''''       '           '           '       '       '       '       '       '           '           '              '                       '
        '   4   '   5   '   6   '       '''''''''''''''''''''''''       '   1   '   2   '   3   '       '      1    '     2     '              '           1           '
        '''''''''''''''''''''''''       '     2     '     4     '       '       '       '       '       '           '           '              '                       '
        '   7   '   8   '   9   '       '           '           '       '       '       '       '       '           '           '              '                       '
        '''''''''''''''''''''''''       '''''''''''''''''''''''''       '''''''''''''''''''''''''       '''''''''''''''''''''''''              '''''''''''''''''''''''''
        If Me.m_Config IsNot Nothing Then Me.ViewerLog("Start Paint" & m_CCDModeType & "CCD Mode Type")
        If Me.m_CCDModeType = "1" Then
            Me.rdo1CCD.Checked = True
            Me.chkCCD1.Checked = True
            Me.chkCCD2.Checked = False
            Me.chkCCD3.Checked = False
            Me.chkCCD4.Checked = False
            Me.chkCCD5.Checked = False
            Me.chkCCD6.Checked = False
            Me.chkCCD7.Checked = False
            Me.chkCCD8.Checked = False
            Me.chkCCD9.Checked = False
            Me.view_Paint_1CCDMODE(intMarkX, intMarkY)


        ElseIf Me.m_CCDModeType = "2" Then
            Me.rdo2CCD.Checked = True
            Me.chkCCD1.Checked = True
            Me.chkCCD2.Checked = True
            Me.chkCCD3.Checked = False
            Me.chkCCD4.Checked = False
            Me.chkCCD5.Checked = False
            Me.chkCCD6.Checked = False
            Me.chkCCD7.Checked = False
            Me.chkCCD8.Checked = False
            Me.chkCCD9.Checked = False
            Me.view_Paint_2CCDMODE(intMarkX, intMarkY)

        ElseIf Me.m_CCDModeType = "3" Then
            Me.rdo3CCD.Checked = True
            Me.chkCCD1.Checked = True
            Me.chkCCD2.Checked = True
            Me.chkCCD3.Checked = True
            Me.chkCCD4.Checked = False
            Me.chkCCD5.Checked = False
            Me.chkCCD6.Checked = False
            Me.chkCCD7.Checked = False
            Me.chkCCD8.Checked = False
            Me.chkCCD9.Checked = False
            Me.view_Paint_3CCDMODE(intMarkX, intMarkY)

        ElseIf Me.m_CCDModeType = "4" Then
            Me.rdo4CCD.Checked = True
            Me.chkCCD1.Checked = True
            Me.chkCCD2.Checked = True
            Me.chkCCD3.Checked = True
            Me.chkCCD4.Checked = True
            Me.chkCCD5.Checked = False
            Me.chkCCD6.Checked = False
            Me.chkCCD7.Checked = False
            Me.chkCCD8.Checked = False
            Me.chkCCD9.Checked = False
            If Me.m_EnablePanelBMArea Then
                Me.view_Paint_BM_4CCDMODE(intMarkX, intMarkY)
            Else
                Me.view_Paint_4CCDMODE(intMarkX, intMarkY)
            End If

        ElseIf Me.m_CCDModeType = "9" Then
            Me.rdo9CCD.Checked = True
            Me.chkCCD1.Checked = True
            Me.chkCCD2.Checked = True
            Me.chkCCD3.Checked = True
            Me.chkCCD4.Checked = True
            Me.chkCCD5.Checked = True
            Me.chkCCD6.Checked = True
            Me.chkCCD7.Checked = True
            Me.chkCCD8.Checked = True
            Me.chkCCD9.Checked = True
            Me.view_Paint_9CCDMODE(intMarkX, intMarkY)
        End If

    End Sub

    Private Function CheckDefectWinPosition(ByVal PositionX As Integer, ByVal PositionY As Integer,
                                            ByVal PicboxW As Single, ByVal PicboxH As Single,
                                            ByVal PositionList As List(Of Point), ByVal WinSizeW As Integer, ByVal WinSizeH As Integer)
        Dim SmallWindow_LT As Point
        Dim SmallWindow_RB As Point
        SmallWindow_LT = New Point(PositionX, PositionY)
        SmallWindow_RB = New Point(PositionX + WinSizeW, PositionY + WinSizeH)

        If SmallWindow_LT.X < 0 Then
            SmallWindow_LT.X = 0
            SmallWindow_RB.X = WinSizeW
        End If

        If SmallWindow_LT.Y < 0 Then
            SmallWindow_LT.Y = 0
            SmallWindow_RB.Y = WinSizeH
        End If




        If Not Me.m_Config.bUseDefectDynamicShowWin Then
            If SmallWindow_RB.X > PicboxW Then
                SmallWindow_LT.X = PicboxW - WinSizeW
                SmallWindow_RB.X = PicboxW
            End If

            If SmallWindow_RB.Y > PicboxH Then
                SmallWindow_LT.Y = PicboxH - WinSizeH
                SmallWindow_RB.Y = PicboxH
            End If
            If PositionList.Count > 0 Then
                For positionListIndex As Integer = 0 To PositionList.Count - 1

                    If PositionList(positionListIndex) = SmallWindow_LT Then
                        SmallWindow_LT.X += WinSizeW / 2
                        SmallWindow_LT.Y += WinSizeH / 2
                        SmallWindow_RB.X += WinSizeW / 2
                        SmallWindow_RB.Y += WinSizeH / 2

                        If SmallWindow_RB.X > PicboxW Then
                            SmallWindow_LT.X -= WinSizeW / 2
                            SmallWindow_RB.X -= WinSizeH / 2
                        End If

                        If SmallWindow_RB.Y > PicboxH Then
                            SmallWindow_LT.Y -= WinSizeW / 2
                            SmallWindow_RB.Y -= WinSizeH / 2
                        End If
                    End If
                Next
            End If
        Else
            If SmallWindow_RB.X > PicboxW Then
                SmallWindow_LT.X = PositionX - WinSizeW
                SmallWindow_RB.X = PositionX
            End If

            If SmallWindow_RB.Y > PicboxH Then
                SmallWindow_LT.Y = PositionY - WinSizeH
                SmallWindow_RB.Y = PositionY
            End If
        End If


        Return SmallWindow_LT
    End Function
    Private Function CheckDefectWinPosition(ByVal PositionX As Integer, ByVal PositionY As Integer,
                                            ByVal PicboxW As Single, ByVal PicboxH As Single,
                                            ByVal WinSizeW As Integer, ByVal WinSizeH As Integer)
        Dim SmallWindow_LT As Point
        Dim SmallWindow_RB As Point
        SmallWindow_LT = New Point(PositionX, PositionY)
        SmallWindow_RB = New Point(PositionX + WinSizeW, PositionY + WinSizeH)

        If SmallWindow_RB.X > PicboxW Then
            SmallWindow_LT.X = PositionX - WinSizeW
            SmallWindow_RB.X = PositionX
        End If

        If SmallWindow_RB.Y > PicboxH Then
            SmallWindow_LT.Y = PositionY - WinSizeH
            SmallWindow_RB.Y = PositionY
        End If

        Return SmallWindow_LT
    End Function
    Private Function CheckDefectTextPosition(ByVal PositionX As Integer, ByVal PositionY As Integer,
                                            ByVal PicboxW As Single, ByVal PicboxH As Single,
                                            ByVal PositionList As List(Of Point))
        Dim SmallWindow_LT As Point
        Dim SmallWindow_RB As Point
        SmallWindow_LT = New Point(PositionX, PositionY)
        SmallWindow_RB = New Point(PositionX + 100, PositionY + 46)

        If SmallWindow_RB.X > PicboxW Then
            SmallWindow_LT.X = PicboxW - 100
            SmallWindow_RB.X = PicboxW
        End If

        If SmallWindow_RB.Y > PicboxH Then
            SmallWindow_LT.Y = PicboxH - 46
            SmallWindow_RB.Y = PicboxH
        End If

        If PositionList.Count > 0 Then

            For positionListIndex As Integer = 0 To PositionList.Count - 1

                If PositionList(positionListIndex) = SmallWindow_LT Then
                    SmallWindow_LT.X += 100 / 2
                    SmallWindow_LT.Y += 46 / 2
                    SmallWindow_RB.X += 100 / 2
                    SmallWindow_RB.Y += 46 / 2

                    If SmallWindow_RB.X > PicboxW Then
                        SmallWindow_LT.X -= 100 / 2
                        SmallWindow_RB.X -= 46 / 2
                    End If

                    If SmallWindow_RB.Y > PicboxH Then
                        SmallWindow_LT.Y -= 100 / 2
                        SmallWindow_RB.Y -= 46 / 2
                    End If
                End If
            Next
        End If

        Return SmallWindow_LT
    End Function
    Private Function CheckDefectTextPosition(ByVal PositionX As Integer, ByVal PositionY As Integer,
                                            ByVal PicboxW As Single, ByVal PicboxH As Single)
        Dim SmallWindow_LT As Point
        Dim SmallWindow_RB As Point
        SmallWindow_LT = New Point(PositionX, PositionY)
        SmallWindow_RB = New Point(PositionX + 100, PositionY + 46)

        If SmallWindow_RB.X > PicboxW Then
            SmallWindow_LT.X = PositionX - 100
            SmallWindow_RB.X = PositionX
        End If

        If SmallWindow_RB.Y > PicboxH Then
            SmallWindow_LT.Y = PositionY - 46
            SmallWindow_RB.Y = PositionY
        End If

        Return SmallWindow_LT
    End Function
    Private Function CheckDefectName(ByVal EnDefectName As String)
        Dim CNDefectName As String = ""

        Select Case (EnDefectName)
            Case "BP"
                CNDefectName = "亮点"
            Case "DP"
                CNDefectName = "暗点"
            Case "Line"
                CNDefectName = "线缺陷"
            Case "Black-Spot"
                CNDefectName = "黑团"
            Case "White-Spot"
                CNDefectName = "白团"
            Case "V_BAND_MURA"
                CNDefectName = "垂直Mura"
            Case "H_BAND_MURA"
                CNDefectName = "水平Mura"
            Case "GSBP"
                CNDefectName = "红蓝班"
        End Select
        Return CNDefectName
    End Function
    Private Sub view_Paint_1CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)

        If Me.m_Config Is Nothing Then Exit Sub
        t1 = Environment.TickCount

        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
            W = bm_source.Width
            H = bm_source.Height
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)
        Dim sx, sy As Single
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim BlockWidth, BlockHeight As Integer
        Dim data, gate As Integer
        Dim window_Loacate_X, window_Loacate_Y As Integer
        Dim mousePosition As New Point(intMarkX, intMarkY)

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked

        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        If Me.m_PanelW = 0 Then Me.m_PanelW = 1
        If Me.m_PanelH = 0 Then Me.m_PanelH = 1
        sx = W / CSng(Me.m_PanelW)
        sy = H / CSng(Me.m_PanelH)

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)

            If mura.CCDNo = "" Then mura.CCDNo = "0"

            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If mura.Data > 0 AndAlso mura.Gate = -1 Then 'V-Band
                    If rbLeftView.Checked = True AndAlso mura.Pattern.IndexOf("LEFT") = -1 Then
                        Continue For
                    ElseIf rbNormalView.Checked = True AndAlso mura.Pattern.IndexOf("CENTER") = -1 Then
                        Continue For
                    ElseIf rbRightView.Checked = True AndAlso mura.Pattern.IndexOf("RIGHT") = -1 Then
                        Continue For
                    Else
                        Me.penMURA.Width = 1.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawLine(Pens.Red, CInt(mura.Data * sx), 0, CInt(mura.Data * sx), CInt(H - 1.0))
                    End If

                ElseIf mura.Data = -1 AndAlso mura.Gate > 0 Then 'H-Band
                    If rbLeftView.Checked = True AndAlso mura.Pattern.IndexOf("LEFT") = -1 Then
                        Continue For
                    ElseIf rbNormalView.Checked = True AndAlso mura.Pattern.IndexOf("CENTER") = -1 Then
                        Continue For
                    ElseIf rbRightView.Checked = True AndAlso mura.Pattern.IndexOf("RIGHT") = -1 Then
                        Continue For
                    Else
                        Me.penMURA.Width = 1.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawLine(Pens.Red, 0, CInt(mura.Gate * sy), CInt(W - 1.0), CInt(mura.Gate * sy))
                    End If

                ElseIf rbNormalView.Checked = True Then
                    If rbMuraPartition.Checked AndAlso mura.Block > 0 Then
                        Select Case mura.Block
                            Case "1"
                                MuraJudgePartition.Partition1 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition1_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition1_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "2"
                                MuraJudgePartition.Partition2 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition2_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition2_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "3"
                                MuraJudgePartition.Partition3 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition3_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition3_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "4"
                                MuraJudgePartition.Partition4 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition4_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition4_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "5"
                                MuraJudgePartition.Partition5 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition5_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition5_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "6"
                                MuraJudgePartition.Partition6 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition6_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition6_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "7"
                                MuraJudgePartition.Partition7 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition7_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition7_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "8"
                                MuraJudgePartition.Partition8 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition8_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition8_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                            Case "9"
                                MuraJudgePartition.Partition9 = True
                                Dim tmpStr As String() = mura.Pattern.Split("/")
                                For c As Integer = 0 To tmpStr.Length - 1
                                    If MuraJudgePartition.Partition9_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                        MuraJudgePartition.Partition9_Pattern += "/" + tmpStr(c)
                                    End If
                                Next
                        End Select

                    Else
                        data = Me.CheckRotateData(mura.Data)
                        gate = Me.CheckRotateGate(mura.Gate)

                        If Me.chkOverlap.Checked Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If

                        muraSizeW = (mura.MaxX - mura.MinX) * sx
                        muraSizeH = (mura.MaxY - mura.MinY) * sy

                        Me.penMURA.Width = 2.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Me.penMURA.Width = 1.0F
                        gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)

                    End If

                End If

                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)

                If (func.CCDNo = "") Then func.CCDNo = 0

                data = Me.CheckRotateData(func.Data)
                gate = Me.CheckRotateGate(func.Gate)

                If Me.chkOverlap.Checked Then

                    If data = -1 And blnRotate Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelW * sx - 12, gate * sy)
                    ElseIf gate = -1 And blnRotate Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelH * sy - 16)
                    Else
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                    End If
                End If

                Select Case (func.Type)
                    Case defectType.BACK_LIGHT_DP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.DP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.DP_PAIR
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.DP_ADJ
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.DP_CLUSTER
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.DP_NEAR
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.GROUP_SMALL_BP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BP_PAIR
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BP_ADJ
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BP_CLUSTER
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BP_NEAR
                        gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BPDP_PAIR
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BPDP_ADJ
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BPDP_CLUSTER
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.BPDP_NEAR
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                        If blnRotate Then
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            End If
                        Else
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penHL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            End If
                        End If

                    Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                        If blnRotate Then
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                            End If
                        Else
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penVL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            End If
                        End If

                    Case defectType.H_OPEN
                        If blnRotate Then
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penHOL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            End If
                        Else
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penHOL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            End If
                        End If

                    Case defectType.V_OPEN
                        If blnRotate Then
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                            End If
                        Else
                            If (func.CCDNo.IndexOf("1") <> -1) Then
                                If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                                gr.DrawLine(Me.penVOL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            End If
                        End If

                    Case defectType.X_SHORT '整塊面板貫穿的兩條直線疊在一起
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawLine(Me.penXL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                        gr.DrawLine(Me.penXL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))

                    Case defectType.CP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.SBP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Case defectType.OMIT_BP
                        If Me.m_Config.bFuncCcdNo Then gr.DrawString(CStr(func.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                End Select

                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If

            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '--------draw dark area--------
        If rbNormalView.Checked = True Then
            If MuraJudgePartition.Partition1 Then
                GetBlockInfomation(1, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition1_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition2 Then
                GetBlockInfomation(2, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition2_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition3 Then
                GetBlockInfomation(3, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition3_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition4 Then
                GetBlockInfomation(4, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition4_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition5 Then
                GetBlockInfomation(5, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition5_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition6 Then
                GetBlockInfomation(6, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition6_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition7 Then
                GetBlockInfomation(7, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition7_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition8 Then
                GetBlockInfomation(8, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition8_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition9 Then
                GetBlockInfomation(9, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition9_Pattern, Me.Font, Brushes.Red, data * sx + 10, gate * sy + 10)
            End If

        ElseIf rbLeftView.Checked = True Then
            Dim BackgroundBrush As New SolidBrush(Color.Black)
            Dim p_Array() As Point = {New Point(0, 0), New Point(Me.picView.Size.Width, 0), New Point(Me.picView.Size.Width, Me.picView.Size.Height), _
                                      New Point(0, Me.picView.Size.Height), New Point(Me.picView.Size.Width * 0.9, Me.picView.Size.Height * 0.8), New Point(Me.picView.Size.Width * 0.9, Me.picView.Size.Height * 0.2)}
            gr.FillPolygon(BackgroundBrush, p_Array)

        ElseIf rbRightView.Checked = True Then
            Dim BackgroundBrush As New SolidBrush(Color.Black)
            Dim p_Array() As Point = {New Point(0, 0), New Point(Me.picView.Size.Width, 0), New Point(Me.picView.Size.Width * 0.1, Me.picView.Size.Height * 0.2), _
                                      New Point(Me.picView.Size.Width * 0.1, Me.picView.Size.Height * 0.8), New Point(Me.picView.Size.Width, Me.picView.Size.Height), New Point(0, Me.picView.Size.Height)}
            gr.FillPolygon(BackgroundBrush, p_Array)

        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Dark Area" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = Me.CheckRotateData(intMarkX)
            gate = Me.CheckRotateGate(intMarkY)

            If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
        Me.m_FirstTimeCreateDefectList = False
    End Sub

    Private Sub view_Paint_2CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        'Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim backgroundImg As New Bitmap(CInt(W), CInt(H))
        'Dim gr As Graphics = Graphics.FromImage(bm)
        Dim window_Loacate_X, window_Loacate_Y As Integer
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)

        If Me.m_Config Is Nothing Then Exit Sub

        t1 = Environment.TickCount
        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
            W = bm_source.Width
            H = bm_source.Height
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)

        Dim bggr As Graphics = Graphics.FromImage(backgroundImg)

        Dim sx, sy As Single
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim data, gate As Integer
        Dim BlockWidth, BlockHeight As Integer

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked
        Dim blnCCD2 As Boolean = Me.chkCCD2.Checked
        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        sx = W / CSng(Me.m_PanelW)
        sy = H / CSng(Me.m_PanelH)

        Dim JndL As Double = 0.0
        Dim JndC As Double = 0.0
        Dim JndR As Double = 0.0
        Dim RankL As String = ""
        Dim RankC As String = ""
        Dim RankR As String = ""

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)

            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If rbMuraPartition.Checked AndAlso mura.Block > 0 AndAlso rbNormalView.Checked = True Then
                    Select Case mura.Block
                        Case "1"
                            MuraJudgePartition.Partition1 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition1_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition1_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "2"
                            MuraJudgePartition.Partition2 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition2_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition2_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "3"
                            MuraJudgePartition.Partition3 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition3_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition3_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "4"
                            MuraJudgePartition.Partition4 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition4_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition4_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "5"
                            MuraJudgePartition.Partition5 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition5_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition5_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "6"
                            MuraJudgePartition.Partition6 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition6_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition6_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "7"
                            MuraJudgePartition.Partition7 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition7_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition7_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "8"
                            MuraJudgePartition.Partition8 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition8_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition8_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                        Case "9"
                            MuraJudgePartition.Partition9 = True
                            Dim tmpStr As String() = mura.Pattern.Split("/")
                            For c As Integer = 0 To tmpStr.Length - 1
                                If MuraJudgePartition.Partition9_Pattern.IndexOf(tmpStr(c)) = -1 Then
                                    MuraJudgePartition.Partition9_Pattern += "/" + tmpStr(c)
                                End If
                            Next
                    End Select

                ElseIf mura.Data = -1 AndAlso mura.Gate > 0 Then 'H-Band
                    If rbLeftView.Checked = True AndAlso mura.Pattern.IndexOf("LEFT") <> -1 Then
                        '左視角檢到的Band
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndL Then
                            JndL = mura.JND
                            RankL = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, 0, CInt(mura.Gate * sy), CInt(W * 0.9), CInt(mura.Gate * sy * 0.6 + H * 0.2))
                        End If
                        'txtAoiJudgerResult.Text = "LEFT_JND" + mura.JND.ToString
                    ElseIf rbNormalView.Checked = True AndAlso mura.Pattern.IndexOf("CENTER") <> -1 Then
                        '正視角檢到的Band
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndC Then
                            JndC = mura.JND
                            RankC = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, 0, CInt(mura.Gate * sy), CInt(W - 1.0), CInt(mura.Gate * sy))
                        End If
                        'txtAoiJudgerResult.Text = "CENTER_JND" + mura.JND.ToString
                    ElseIf rbRightView.Checked = True AndAlso mura.Pattern.IndexOf("RIGHT") <> -1 Then
                        '右視角檢到的Band
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndR Then
                            JndR = mura.JND
                            RankR = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, CInt(W * 0.1), CInt(mura.Gate * sy * 0.6 + H * 0.2), CInt(W - 1.0), CInt(mura.Gate * sy))
                        End If
                        'txtAoiJudgerResult.Text = "RIGHT_JND" + mura.JND.ToString
                    Else
                        Continue For
                    End If

                ElseIf mura.Data > 0 AndAlso mura.Gate = -1 Then 'V-Band
                    If rbLeftView.Checked = True AndAlso mura.Pattern.IndexOf("LEFT") <> -1 Then
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndL Then
                            JndL = mura.JND
                            RankL = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, CInt(mura.Data * sx), 0, CInt(mura.Data * sx), CInt(H - 1.0))
                        End If

                    ElseIf rbNormalView.Checked = True AndAlso mura.Pattern.IndexOf("CENTER") <> -1 Then
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndC Then
                            JndC = mura.JND
                            RankC = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, CInt(mura.Data * sx), 0, CInt(mura.Data * sx), CInt(H - 1.0))
                        End If

                    ElseIf rbRightView.Checked = True AndAlso mura.Pattern.IndexOf("RIGHT") <> -1 Then
                        Me.penMURA.Width = 3.0F
                        If Me.m_Config.bMuraCcdNo Then gr.DrawString(CStr(mura.CCDNo), Me.Font, Brushes.White, CInt(data * sx), CInt(gate * sy))
                        If mura.JND > JndR Then
                            JndR = mura.JND
                            RankR = mura.Rank
                        End If
                        If mura.Rank = "P" OrElse mura.Rank = "N" OrElse mura.Rank = "S" Then
                            gr.DrawLine(Pens.LightGreen, CInt(mura.Data * sx), 0, CInt(mura.Data * sx), CInt(H - 1.0))
                        End If

                    Else
                        Continue For
                    End If

                ElseIf rbNormalView.Checked = True Then
                    If (mura.CCDNo = "") Or (blnCCD1 And mura.CCDNo.IndexOf("1") <> -1) Or (blnCCD2 And mura.CCDNo.IndexOf("2") <> -1) Then
                        data = Me.CheckRotateData(mura.Data)
                        gate = Me.CheckRotateGate(mura.Gate)

                        If Me.chkOverlap.Checked Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If

                        muraSizeW = (mura.MaxX - mura.MinX) * sx
                        muraSizeH = (mura.MaxY - mura.MinY) * sy

                        Me.penMURA.Width = 2.0F
                        gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Me.penMURA.Width = 1.0F
                        gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                    End If
                End If

                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If

            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)

                If (func.CCDNo = "") Or _
                    (blnCCD1 And func.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And func.CCDNo.IndexOf("2") <> -1) Then

                    data = Me.CheckRotateData(func.Data)
                    gate = Me.CheckRotateGate(func.Gate)

                    If Me.chkOverlap.Checked Then
                        If data = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelW * sx - 12, gate * sy)
                        ElseIf gate = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelH * sy - 16)
                        Else
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If
                    End If

                    Select Case (func.Type)
                        Case defectType.BACK_LIGHT_DP
                            gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP
                            gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_PAIR
                            gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_ADJ
                            gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_CLUSTER
                            gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_NEAR
                            gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.GROUP_SMALL_BP
                            gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP
                            gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_PAIR
                            gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_ADJ
                            gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_CLUSTER
                            gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_NEAR
                            gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_PAIR
                            gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_ADJ
                            gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_CLUSTER
                            gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_NEAR
                            gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 2.0) - 1.0), CInt(gate * sy))
                                End If
                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 2.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                End If

                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.H_OPEN
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 2.0) - 1.0), CInt(gate * sy))
                                End If
                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 2.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_OPEN
                            If blnRotate Then
                                If blnRotate Then
                                    If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                        gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                    End If
                                Else
                                    If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                        gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                    End If
                                End If
                            End If


                        Case defectType.X_SHORT
                            gr.DrawLine(Me.penXL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            gr.DrawLine(Me.penXL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            'gr.DrawEllipse(Me.penXL, CInt(data * sx) - 3, CInt(gate * sy) - 3, 6, 6)

                        Case defectType.CP
                            gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SBP
                            gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.OMIT_BP
                            gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                    End Select

                End If

                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '--------draw dark area--------
        If rbNormalView.Checked = True Then

            If JndC > 0 Then
                If RankC = "N" OrElse RankC = "S" Then
                    txtAoiJudgerResult.ForeColor = Color.Blue
                    txtAoiJudgerResult.BackColor = Color.Red
                Else
                    txtAoiJudgerResult.ForeColor = Color.Red
                    txtAoiJudgerResult.BackColor = Color.Lime
                End If
                txtAoiJudgerResult.Text = "JND = " + JndC.ToString("#.##")
            End If

            If MuraJudgePartition.Partition1 Then
                GetBlockInfomation(1, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition1_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition2 Then
                GetBlockInfomation(2, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition2_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition3 Then
                GetBlockInfomation(3, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition3_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition4 Then
                GetBlockInfomation(4, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition4_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition5 Then
                GetBlockInfomation(5, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition5_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition6 Then
                GetBlockInfomation(6, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition6_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition7 Then
                GetBlockInfomation(7, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition7_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition8 Then
                GetBlockInfomation(8, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition8_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If
            If MuraJudgePartition.Partition9 Then
                GetBlockInfomation(9, data, gate, BlockWidth, BlockHeight)
                gr.DrawRectangle(Pens.Red, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                bggr.FillRectangle(Brushes.HotPink, CInt(data * sx), CInt(gate * sy), BlockWidth * sx, BlockHeight * sy)
                gr.DrawString(MuraJudgePartition.Partition9_Pattern, Me.Font, Brushes.White, data * sx + 10, gate * sy + 10)
            End If

        ElseIf rbLeftView.Checked = True Then

            If JndL > 0 Then
                If RankL = "N" OrElse RankL = "S" Then
                    txtAoiJudgerResult.ForeColor = Color.Blue
                    txtAoiJudgerResult.BackColor = Color.Red
                Else
                    txtAoiJudgerResult.ForeColor = Color.Red
                    txtAoiJudgerResult.BackColor = Color.Lime
                End If
                txtAoiJudgerResult.Text = "JND = " & JndL.ToString("#.##")
            End If

            Dim BackgroundBrush As New SolidBrush(Color.Black)
            Dim p_Array() As Point = {New Point(0, 0), New Point(Me.picView.Size.Width, 0), New Point(Me.picView.Size.Width, Me.picView.Size.Height), _
                                      New Point(0, Me.picView.Size.Height), New Point(Me.picView.Size.Width * 0.9, Me.picView.Size.Height * 0.8), New Point(Me.picView.Size.Width * 0.9, Me.picView.Size.Height * 0.2)}
            gr.FillPolygon(BackgroundBrush, p_Array)
            'bggr.FillPolygon(BackgroundBrush, p_Array)
        ElseIf rbRightView.Checked = True Then

            If JndR > 0 Then
                If RankR = "N" OrElse RankR = "S" Then
                    txtAoiJudgerResult.ForeColor = Color.Blue
                    txtAoiJudgerResult.BackColor = Color.Red
                Else
                    txtAoiJudgerResult.ForeColor = Color.Red
                    txtAoiJudgerResult.BackColor = Color.Lime
                End If
                txtAoiJudgerResult.Text = "JND = " & JndR.ToString("#.##")
            End If

            Dim BackgroundBrush As New SolidBrush(Color.Black)
            Dim p_Array() As Point = {New Point(0, 0), New Point(Me.picView.Size.Width, 0), New Point(Me.picView.Size.Width * 0.1, Me.picView.Size.Height * 0.2), _
                                      New Point(Me.picView.Size.Width * 0.1, Me.picView.Size.Height * 0.8), New Point(Me.picView.Size.Width, Me.picView.Size.Height), New Point(0, Me.picView.Size.Height)}
            gr.FillPolygon(BackgroundBrush, p_Array)
            'bggr.FillPolygon(BackgroundBrush, p_Array)
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Dark Area" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = Me.CheckRotateData(intMarkX)
            gate = Me.CheckRotateGate(intMarkY)

            If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.BackgroundImage = backgroundImg
        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
    End Sub

    Private Sub view_Paint_3CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)
        Dim window_Loacate_X, window_Loacate_Y As Integer
        If Me.m_Config Is Nothing Then Exit Sub

        t1 = Environment.TickCount
        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
            W = bm_source.Width
            H = bm_source.Height
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)

        'Dim bm As New Bitmap(CInt(W), CInt(H))
        'Dim gr As Graphics = Graphics.FromImage(bm)

        Dim sx, sy As Single
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim data, gate As Integer

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked
        Dim blnCCD2 As Boolean = Me.chkCCD2.Checked
        Dim blnCCD3 As Boolean = Me.chkCCD3.Checked
        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        sx = W / CSng(Me.m_PanelW)
        sy = H / CSng(Me.m_PanelH)

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)


            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If (mura.CCDNo = "") Or _
                    (blnCCD1 And mura.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And mura.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And mura.CCDNo.IndexOf("3") <> -1) Then

                    data = Me.CheckRotateData(mura.Data)
                    gate = Me.CheckRotateGate(mura.Gate)

                    If Me.chkOverlap.Checked Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                    End If
                    '3CCD因為CCD轉90度，所以將MIN與MAX對換
                    muraSizeW = (mura.MaxY - mura.MinY) * sx
                    muraSizeH = (mura.MaxX - mura.MinX) * sy

                    Me.penMURA.Width = 2.0F
                    gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Me.penMURA.Width = 1.0F
                    gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)

                If (func.CCDNo = "") Or _
                    (blnCCD1 And func.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And func.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And func.CCDNo.IndexOf("3") <> -1) Then

                    data = Me.CheckRotateData(func.Data)
                    gate = Me.CheckRotateGate(func.Gate)

                    If Me.chkOverlap.Checked Then

                        If data = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelW * sx - 12, gate * sy)

                        ElseIf gate = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelH * sy - 16)

                        Else
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If
                    End If

                    Select Case (func.Type)
                        Case defectType.BACK_LIGHT_DP
                            gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP
                            gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_PAIR
                            gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_ADJ
                            gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_CLUSTER
                            gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_NEAR
                            gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.GROUP_SMALL_BP
                            gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP
                            gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_PAIR
                            gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_ADJ
                            gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_CLUSTER
                            gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_NEAR
                            gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_PAIR
                            gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_ADJ
                            gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_CLUSTER
                            gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_NEAR
                            gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                End If

                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.H_OPEN
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_OPEN
                            If blnRotate Then
                                If blnRotate Then
                                    If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                        gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                    End If
                                Else
                                    If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                        gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H - 1.0))
                                    End If
                                End If
                            End If


                        Case defectType.X_SHORT
                            gr.DrawLine(Me.penXL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            gr.DrawLine(Me.penXL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            'gr.DrawEllipse(Me.penXL, CInt(data * sx) - 3, CInt(gate * sy) - 3, 6, 6)

                        Case defectType.CP
                            gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SBP
                            gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.OMIT_BP
                            gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                    End Select

                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = Me.CheckRotateData(intMarkX)
            gate = Me.CheckRotateGate(intMarkY)

            If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
    End Sub
    Private Sub view_Paint_BM_4CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)
        Dim window_Loacate_X, window_Loacate_Y As Integer
        If Me.m_Config Is Nothing Then Exit Sub

        t1 = Environment.TickCount
        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)

        Dim sx, sy As Single

        Dim offsetx, offsety As Integer
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim data, gate As Integer

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked
        Dim blnCCD2 As Boolean = Me.chkCCD2.Checked
        Dim blnCCD3 As Boolean = Me.chkCCD3.Checked
        Dim blnCCD4 As Boolean = Me.chkCCD4.Checked
        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelBMW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelBMH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelBMW)
            Me.labPanelH.Text = CStr(Me.m_PanelBMH)
        End If


        sx = W / CSng(Me.m_PanelBMW)
        sy = H / CSng(Me.m_PanelBMH)


        offsetx = (Me.m_PanelBMW - Me.m_PanelW) / 2
        offsety = (Me.m_PanelBMH - Me.m_PanelH) / 2

        gr.FillRectangle(Brushes.Silver, CSng(offsetx * sx), CSng(offsety * sy), CSng(Me.m_PanelW * sx), CSng(Me.m_PanelH * sy))

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)


            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If (mura.CCDNo = "") Or _
                    (blnCCD1 And mura.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And mura.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And mura.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And mura.CCDNo.IndexOf("4") <> -1) Then


                    data = mura.Data + offsetx
                    gate = mura.Gate + offsety

                    data = Me.CheckRotateData(data)
                    gate = Me.CheckRotateGate(gate)

                    If Me.chkOverlap.Checked Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                    End If

                    muraSizeW = (mura.MaxX - mura.MinX) * sx
                    muraSizeH = (mura.MaxY - mura.MinY) * sy

                    Me.penMURA.Width = 2.0F
                    gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Me.penMURA.Width = 1.0F
                    gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)


                If (func.CCDNo = "") Or _
                    (blnCCD1 And func.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And func.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And func.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And func.CCDNo.IndexOf("4") <> -1) Then


                    data = func.Data + offsetx
                    gate = func.Gate + offsety

                    data = Me.CheckRotateData(data)
                    gate = Me.CheckRotateGate(gate)

                    If Me.chkOverlap.Checked Then

                        If data = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelBMW * sx - 12, gate * sy)

                        ElseIf gate = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelBMH * sy - 16)

                        Else
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If
                    End If


                    Select Case (func.Type)

                        Case defectType.FRAME_GLUE
                            gr.DrawRectangle(Me.penFG, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SMALL_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.MIDDLE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.LARGE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.S_GRADE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.BACK_LIGHT_DP
                            gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP
                            gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_PAIR
                            gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy + offsety), 2, 2)

                        Case defectType.DP_ADJ
                            gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_CLUSTER
                            gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_NEAR
                            gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.GROUP_SMALL_BP
                            gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP
                            gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_PAIR
                            gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_ADJ
                            gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_CLUSTER
                            gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_NEAR
                            gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_PAIR
                            gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_ADJ
                            gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_CLUSTER
                            gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_NEAR
                            gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - offsetx * sx - 1.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, offsetx * sx, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, offsetx * sx, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - offsetx * sx - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - offsety * sy - 1.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), offsety * sy, CInt(data * sx), CInt(H / 2.0))
                                End If
                            Else

                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), offsety * sy, CInt(data * sx), CInt(H / 2.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - offsety * sy - 1.0))
                                End If
                            End If

                        Case defectType.H_OPEN
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - offsetx * sx - 1.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, offsetx * sx, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, offsetx * sx, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - offsetx * sx - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_OPEN
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - offsety * sy - 1.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), offsety * sy, CInt(data * sx), CInt(H / 2.0))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), offsety * sy, CInt(data * sx), CInt(H / 2.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - offsety * sy - 1.0))
                                End If
                            End If

                        Case defectType.X_SHORT
                            gr.DrawLine(Me.penXL, CInt(data * sx), offsety * sy, CInt(data * sx), CInt(H - offsety * sy - 1.0))
                            gr.DrawLine(Me.penXL, offsetx * sx, CInt(gate * sy), CInt(W - offsetx * sx - 1.0), CInt(gate * sy))

                        Case defectType.CP
                            gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SBP
                            gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.OMIT_BP
                            gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                    End Select

                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            'data = Me.CheckRotateData(intMarkX)
            'gate = Me.CheckRotateGate(intMarkY)

            'If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            'If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))

            data = intMarkX + offsetx
            gate = intMarkY + offsety

            gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
    End Sub

    Private Sub view_Paint_4CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)
        Dim window_Loacate_X, window_Loacate_Y As Integer
        If Me.m_Config Is Nothing Then Exit Sub

        t1 = Environment.TickCount
        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
            W = bm_source.Width
            H = bm_source.Height
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)

        Dim sx, sy As Single
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim data, gate As Integer

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked
        Dim blnCCD2 As Boolean = Me.chkCCD2.Checked
        Dim blnCCD3 As Boolean = Me.chkCCD3.Checked
        Dim blnCCD4 As Boolean = Me.chkCCD4.Checked
        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        sx = W / CSng(Me.m_PanelW)
        sy = H / CSng(Me.m_PanelH)

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)


            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If (mura.CCDNo = "") Or _
                    (blnCCD1 And mura.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And mura.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And mura.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And mura.CCDNo.IndexOf("4") <> -1) Then

                    data = Me.CheckRotateData(mura.Data)
                    gate = Me.CheckRotateGate(mura.Gate)

                    If Me.chkOverlap.Checked Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                    End If

                    muraSizeW = (mura.MaxX - mura.MinX) * sx
                    muraSizeH = (mura.MaxY - mura.MinY) * sy

                    Me.penMURA.Width = 2.0F
                    gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Me.penMURA.Width = 1.0F
                    gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)

                If (func.CCDNo = "") Or _
                    (blnCCD1 And func.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And func.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And func.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And func.CCDNo.IndexOf("4") <> -1) Then

                    data = Me.CheckRotateData(func.Data)
                    gate = Me.CheckRotateGate(func.Gate)

                    If Me.chkOverlap.Checked Then

                        If data = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelW * sx - 12, gate * sy)

                        ElseIf gate = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelH * sy - 16)

                        Else
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If
                    End If

                    Select Case (func.Type)

                        Case defectType.FRAME_GLUE
                            gr.DrawRectangle(Me.penFG, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SMALL_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.MIDDLE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.LARGE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.S_GRADE_BUBBLE
                            Dim radius As Integer = CInt(Math.Sqrt(func.Area / Math.PI)) * sx
                            gr.DrawEllipse(Me.penBB, CInt(data * sx - radius * sx), CInt(gate * sy - radius * sx), radius * sx * 2, radius * sy * 2)

                        Case defectType.BACK_LIGHT_DP
                            gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP
                            gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_PAIR
                            gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_ADJ
                            gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_CLUSTER
                            gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_NEAR
                            gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.GROUP_SMALL_BP
                            gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP
                            gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_PAIR
                            gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_ADJ
                            gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_CLUSTER
                            gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_NEAR
                            gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_PAIR
                            gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_ADJ
                            gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_CLUSTER
                            gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_NEAR
                            gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, 0, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, 0, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - 1.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), 0, CInt(data * sx), CInt(H / 2.0))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), 0, CInt(data * sx), CInt(H / 2.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.H_OPEN
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, 0, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, 0, CInt(gate * sy), CInt(W / 2.0), CInt(gate * sy))

                                ElseIf (func.CCDNo.IndexOf("3") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 2.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_OPEN
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - 1.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), 0, CInt(data * sx), CInt(H / 2.0))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), 0, CInt(data * sx), CInt(H / 2.0))

                                ElseIf (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 2.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.X_SHORT
                            gr.DrawLine(Me.penXL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            gr.DrawLine(Me.penXL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            'gr.DrawEllipse(Me.penXL, CInt(data * sx) - 3, CInt(gate * sy) - 3, 6, 6)

                        Case defectType.CP
                            gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SBP
                            gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.OMIT_BP
                            gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                    End Select

                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = Me.CheckRotateData(intMarkX)
            gate = Me.CheckRotateGate(intMarkY)

            If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
    End Sub

    Private Sub view_Paint_9CCDMODE(ByVal intMarkX As Integer, ByVal intMarkY As Integer)
        Dim t1, t2 As Integer


        Dim W As Single = CSng(Me.picView.Size.Width)
        Dim H As Single = CSng(Me.picView.Size.Height)
        Dim bm As New Bitmap(CInt(W), CInt(H))
        Dim bm_source As Bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)
        Dim window_Loacate_X, window_Loacate_Y As Integer
        If Me.m_Config Is Nothing Then Exit Sub

        t1 = Environment.TickCount
        Dim f As FileInfo = New FileInfo("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
        'Dim f As FileInfo = New FileInfo("C:\Users\BingTsai\Desktop\Clipboard.bmp")
        If f.Exists Then
            bm_source = New Bitmap("D:\AOI_Data\GrabImage\IP1\IP1\" + Format(Now, "yyyyMMdd") + Me.m_PanelID + "_" + Me.m_Config.LoadImagePattern + ".bmp")
            'bm_source = New Bitmap("C:\Users\BingTsai\Desktop\Clipboard.bmp")
            gr.DrawImage(bm_source, 0, 0, bm_source.Width, bm_source.Height)
            W = bm_source.Width
            H = bm_source.Height
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Load Background Image" & t2 - t1)

        Dim sx, sy As Single
        Dim i, j As Integer
        Dim func As New ClsFuncDf
        Dim mura As New ClsMuraDf
        Dim JRdataList As New ClsJRdata
        Dim muraSizeH, muraSizeW As Single
        Dim data, gate As Integer

        Dim blnCCD1 As Boolean = Me.chkCCD1.Checked
        Dim blnCCD2 As Boolean = Me.chkCCD2.Checked
        Dim blnCCD3 As Boolean = Me.chkCCD3.Checked
        Dim blnCCD4 As Boolean = Me.chkCCD4.Checked
        Dim blnCCD5 As Boolean = Me.chkCCD5.Checked
        Dim blnCCD6 As Boolean = Me.chkCCD6.Checked
        Dim blnCCD7 As Boolean = Me.chkCCD7.Checked
        Dim blnCCD8 As Boolean = Me.chkCCD8.Checked
        Dim blnCCD9 As Boolean = Me.chkCCD9.Checked
        Dim blnRotate As Boolean = Me.chkRotate.Checked

        If Me.m_Config.bUse9Part Then
            Dim pen As New Pen(Color.DarkGreen)
            pen.Width = 1.0F
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3), CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3))
            gr.DrawLine(pen, 0, CInt(Me.picView.Size.Height / 3) * 2, CInt(Me.picView.Size.Width), CInt(Me.picView.Size.Height / 3) * 2)
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3), 0, CInt(Me.picView.Size.Width / 3), CInt(Me.picView.Size.Height))
            gr.DrawLine(pen, CInt(Me.picView.Size.Width / 3) * 2, 0, CInt(Me.picView.Size.Width / 3) * 2, CInt(Me.picView.Size.Height))
        End If

        If blnRotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        sx = W / CSng(Me.m_PanelW)
        sy = H / CSng(Me.m_PanelH)

        t1 = Environment.TickCount
        For j = 0 To Me.m_aryResult.Count - 1
            JRdataList = Me.m_aryResult.Index(j)


            '---draw Mura defect---
            For i = 0 To JRdataList.MuraCount - 1
                mura = JRdataList.GetMuraDf(i)

                If (mura.CCDNo = "") Or _
                    (blnCCD1 And mura.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And mura.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And mura.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And mura.CCDNo.IndexOf("4") <> -1) Or _
                    (blnCCD5 And mura.CCDNo.IndexOf("5") <> -1) Or _
                    (blnCCD6 And mura.CCDNo.IndexOf("6") <> -1) Or _
                    (blnCCD7 And mura.CCDNo.IndexOf("7") <> -1) Or _
                    (blnCCD8 And mura.CCDNo.IndexOf("8") <> -1) Or _
                    (blnCCD9 And mura.CCDNo.IndexOf("9") <> -1) Then

                    data = Me.CheckRotateData(mura.Data)
                    gate = Me.CheckRotateGate(mura.Gate)

                    If Me.chkOverlap.Checked Then
                        gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                    End If

                    muraSizeW = (mura.MaxX - mura.MinX) * sx
                    muraSizeH = (mura.MaxY - mura.MinY) * sy

                    Me.penMURA.Width = 2.0F
                    gr.DrawRectangle(Me.penMURA, CInt(data * sx), CInt(gate * sy), 2, 2)

                    Me.penMURA.Width = 1.0F
                    gr.DrawEllipse(Me.penMURA, CInt(data * sx - muraSizeW / 2.0), CInt(gate * sy - muraSizeH / 2.0), muraSizeW, muraSizeH)
                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    M_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    '_DefectFrmPositionList.Add(CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height, _DefectFrmPositionList))
                    _M_DefectFrmPositionList.Add(_tmpPoint)
                    M_DlgShowDefectFrm(i).Location = _M_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        M_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        M_DlgShowDefectFrm(i).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        M_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             M_DlgShowDefectFrm(i).Size.Width, M_DlgShowDefectFrm(i).Size.Height)

                    Else
                        M_DlgShowDefectFrm(i).NoImage(CheckDefectName(mura.MuraType.Replace(vbLf, "")), mura.Pattern)
                        _M_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            M_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i

            '---draw Func defect---
            For i = 0 To JRdataList.FuncCount - 1
                func = JRdataList.GetFuncDf(i)

                If (func.CCDNo = "") Or _
                    (blnCCD1 And func.CCDNo.IndexOf("1") <> -1) Or _
                    (blnCCD2 And func.CCDNo.IndexOf("2") <> -1) Or _
                    (blnCCD3 And func.CCDNo.IndexOf("3") <> -1) Or _
                    (blnCCD4 And func.CCDNo.IndexOf("4") <> -1) Or _
                    (blnCCD5 And func.CCDNo.IndexOf("5") <> -1) Or _
                    (blnCCD6 And func.CCDNo.IndexOf("6") <> -1) Or _
                    (blnCCD7 And func.CCDNo.IndexOf("7") <> -1) Or _
                    (blnCCD8 And func.CCDNo.IndexOf("8") <> -1) Or _
                    (blnCCD9 And func.CCDNo.IndexOf("9") <> -1) Then

                    data = Me.CheckRotateData(func.Data)
                    gate = Me.CheckRotateGate(func.Gate)

                    If Me.chkOverlap.Checked Then

                        If data = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, Me.m_PanelW * sx - 12, gate * sy)

                        ElseIf gate = -1 And blnRotate Then
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, Me.m_PanelH * sy - 16)

                        Else
                            gr.DrawString(CStr(Me.PointCountAtTree(data, gate)), Me.Font, Brushes.White, data * sx, gate * sy)
                        End If
                    End If

                    Select Case (func.Type)
                        Case defectType.BACK_LIGHT_DP
                            gr.DrawRectangle(Me.penBLDP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP
                            gr.DrawRectangle(Me.penDP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_PAIR
                            gr.DrawRectangle(Me.penDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_ADJ
                            gr.DrawRectangle(Me.penDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_CLUSTER
                            gr.DrawRectangle(Me.penDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.DP_NEAR
                            gr.DrawRectangle(Me.penDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.GROUP_SMALL_BP
                            gr.DrawRectangle(Me.penGSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP
                            gr.DrawRectangle(Me.penBP1, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_PAIR
                            gr.DrawRectangle(Me.penBP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_ADJ
                            gr.DrawRectangle(Me.penBP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_CLUSTER
                            gr.DrawRectangle(Me.penBPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BP_NEAR
                            gr.DrawRectangle(Me.penBPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_PAIR
                            gr.DrawRectangle(Me.penBPDP2, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_ADJ
                            gr.DrawRectangle(Me.penBPDP3, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_CLUSTER
                            gr.DrawRectangle(Me.penBPDPx, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.BPDP_NEAR
                            gr.DrawRectangle(Me.penBPDPn, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.H_LINE, defectType.H_BLOCK 'H_BLOCK暫時用penHL的顏色
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("5") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("8") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If

                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("8") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("5") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_LINE, defectType.V_BLOCK 'V_BLOCK暫時用penVL的顏色
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("7") <> -1) Or (func.CCDNo.IndexOf("8") <> -1) Or (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Or (func.CCDNo.IndexOf("5") <> -1) Or (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 3.0), CInt(data * sx), CInt(2 * H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(2 * H / 3.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Or (func.CCDNo.IndexOf("5") <> -1) Or (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(H / 3.0), CInt(data * sx), CInt(2 * H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Or (func.CCDNo.IndexOf("8") <> -1) Or (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penVL, CInt(data * sx), CInt(2 * H / 3.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.H_OPEN
                            If blnRotate Then   '有翻轉
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("5") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("8") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If

                            Else    '無翻轉
                                If (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("8") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("5") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(2 * W / 3.0), CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("2") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(W / 3.0), CInt(gate * sy), CInt(2 * W / 3.0), CInt(gate * sy))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Then
                                    gr.DrawLine(Me.penHOL, CInt(0), CInt(gate * sy), CInt((W / 3.0) - 1.0), CInt(gate * sy))
                                End If
                            End If

                        Case defectType.V_OPEN
                            If blnRotate Then
                                If (func.CCDNo.IndexOf("7") <> -1) Or (func.CCDNo.IndexOf("8") <> -1) Or (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Or (func.CCDNo.IndexOf("5") <> -1) Or (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 3.0), CInt(data * sx), CInt(2 * H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(2 * H / 3.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            Else
                                If (func.CCDNo.IndexOf("1") <> -1) Or (func.CCDNo.IndexOf("2") <> -1) Or (func.CCDNo.IndexOf("3") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(0), CInt(data * sx), CInt(H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("4") <> -1) Or (func.CCDNo.IndexOf("5") <> -1) Or (func.CCDNo.IndexOf("6") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(H / 3.0), CInt(data * sx), CInt(2 * H / 3.0))
                                End If
                                If (func.CCDNo.IndexOf("7") <> -1) Or (func.CCDNo.IndexOf("8") <> -1) Or (func.CCDNo.IndexOf("9") <> -1) Then
                                    gr.DrawLine(Me.penVOL, CInt(data * sx), CInt(2 * H / 3.0), CInt(data * sx), CInt(H - 1.0))
                                End If
                            End If

                        Case defectType.X_SHORT
                            gr.DrawLine(Me.penXL, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
                            gr.DrawLine(Me.penXL, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
                            'gr.DrawEllipse(Me.penXL, CInt(data * sx) - 3, CInt(gate * sy) - 3, 6, 6)

                        Case defectType.CP
                            gr.DrawRectangle(Me.penCP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.SBP
                            gr.DrawRectangle(Me.penSBP, CInt(data * sx), CInt(gate * sy), 2, 2)

                        Case defectType.OMIT_BP
                            gr.DrawRectangle(Me.penOMITBP, CInt(data * sx), CInt(gate * sy), 2, 2)
                    End Select

                End If

                'If Not Me.m_Config.bUseDefectDynamicShowWin AndAlso JRdataList.MuraCount > DlgShowDefectFrm.Count Then
                '    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                '    DlgShowDefectFrm.Add(ShowDefect)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Show()
                '    'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Showfrm((Form1.DesktopLocation.X + Me.Location.X + TabControl_ShowDefect.Location.X + 25) + data / sx + 25, (Form1.DesktopLocation.Y + Me.Location.Y + TabControl_ShowDefect.Location.Y + 120) + gate / sy)
                '    DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).Movefrm(Form1.DesktopLocation.X + 5 + Me.Location.X + 10 + TabControl_ShowDefect.Location.X + 5 + picView.Location.X + data * sx + 5, Form1.DesktopLocation.Y + 30 + Me.Location.Y + 30 + TabControl_ShowDefect.Location.Y + 28 + picView.Location.Y + gate * sy + 5)
                '    If File.Exists(mura.ImageFilePath + mura.FileName) Then
                '        DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).LoadImage(mura.ImageFilePath + mura.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                '    Else
                '        'DlgShowDefectFrm(DlgShowDefectFrm.Count - 1).NoImage()
                '    End If
                'End If
                window_Loacate_X = CInt(data * sx)
                window_Loacate_Y = CInt(gate * sy)
                _tmpPoint.X = window_Loacate_X
                _tmpPoint.Y = window_Loacate_Y
                If m_FirstTimeCreateDefectList Then
                    Dim ShowDefect As FrmShowDefect = New FrmShowDefect
                    ShowDefect.TopMost = False
                    ShowDefect.TopLevel = False
                    F_DlgShowDefectFrm.Add(ShowDefect)

                    Me.picView.Controls.Add(ShowDefect)

                    _F_DefectFrmPositionList.Add(_tmpPoint)
                    F_DlgShowDefectFrm(i).Location = _F_DefectFrmPositionList(i)
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                    End If
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(i).Show()
                    End If
                Else
                    If Me.m_Config.USE_defectWindow Then
                        F_DlgShowDefectFrm(i).Size = New Point(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        F_DlgShowDefectFrm(i).LoadImage(func.ImageFilePath + func.FileName, data, gate, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH,
CheckDefectName(func.Type.ToString().Replace(vbLf, "")), func.Pattern)
                        F_DlgShowDefectFrm(i).Location = CheckDefectWinPosition(window_Loacate_X, window_Loacate_Y, picView.Width, picView.Height,
                                                                             F_DlgShowDefectFrm(i).Size.Width, F_DlgShowDefectFrm(i).Size.Height)

                    Else
                        F_DlgShowDefectFrm(i).NoImage(func.Type.ToString(), func.Pattern)
                        _F_DefectFrmPositionList(i) = _tmpPoint
                        If Not Me.m_Config.bUseDefectDynamicShowWin Then
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        Else
                            F_DlgShowDefectFrm(i).Location = CheckDefectTextPosition(window_Loacate_X, window_Loacate_Y,
                                                                      picView.Width, picView.Height)
                        End If

                    End If

                End If
            Next i
        Next j
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Func Mura Defect" & t2 - t1)

        t1 = Environment.TickCount
        '---draw Mark-line---
        If Not (intMarkX = 0 And intMarkY = 0) Then
            data = Me.CheckRotateData(intMarkX)
            gate = Me.CheckRotateGate(intMarkY)

            If intMarkX > 0 Then gr.DrawLine(Me.penMark, CInt(data * sx), 0, CInt(data * sx), CInt(H - 1.0))
            If intMarkY > 0 Then gr.DrawLine(Me.penMark, 0, CInt(gate * sy), CInt(W - 1.0), CInt(gate * sy))
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("Draw Mark-line" & t2 - t1)

        Me.picView.Image = bm
        Me.picView.Refresh()
        gr.Dispose()
    End Sub

    Public Sub ViewerLog(ByVal logMsg As String)
        Me.m_ViewerLog.Open(Me.m_Config.LogPath, "LogViewer")
        Me.m_ViewerLog.WriteLogTimeMs(logMsg)
        Me.m_ViewerLog.Close()
    End Sub

    'Public Sub ShowDefectImage(ByVal path As String, ByVal data As Integer, ByVal gate As Integer, ByVal winW As Single, ByVal winH As Single)
    '    Form1.ShowDefect(path, data, gate, winW, winH)
    'End Sub

    'Public Sub NoDefectImage()
    '    Form1.NoDefectImage()
    'End Sub

    'Public Sub ShowDefectFrm(ByVal Location_x As Integer, ByVal Location_y As Integer)
    '    Form1.ShowDefectFrm(Location_x, Location_y)
    'End Sub

    'Public Sub HideDefectFrm()
    '    Form1.HideDefectFrm()
    'End Sub

    'Public Sub DefectFrmReSize(ByVal x As Integer, ByVal y As Integer)
    '    Form1.DefectFrmReSize(x, y)
    'End Sub

    '---點選Tree後秀Mark線---
    Private Sub teeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles teeView.AfterSelect
        If Not Me.m_Config.bUseDefectDynamicShowWin Then Exit Sub

        Dim i, j, k, x, y As Integer
        Dim strTmp As String = ""
        Dim p As New ClsFuncDf
        Dim mp As New ClsMuraDf

        Dim Dfx, Dfy As Integer
        Dim DfstrTmp As String = ""
        Dim sx, sy As Single


        sx = CSng(Me.m_PanelBMW / Me.picView.Size.Width)
        sy = CSng(Me.m_PanelBMH / Me.picView.Size.Height)

        If Me.m_EnablePanelBMArea Then
            Try
                strTmp = e.Node.Text
                j = e.Node.Text.IndexOf(",")

                If j <> -1 Then
                    i = e.Node.Text.IndexOf("(")
                    k = e.Node.Text.IndexOf(")")

                    x = CInt(e.Node.Text.Substring(i + 1, j - i - 1))
                    y = CInt(e.Node.Text.Substring(j + 1, k - j - 1))
                    DfstrTmp = strTmp
                    Dfx = x
                    Dfy = y
                    Me.rtbJudgeResultFile.Text = ""

                    If x > Me.m_PanelBMW Or y > Me.m_PanelBMH Then
                        Me.rtbJudgeResultFile.Text = "Warning: Outside of Boundary!!!"

                    End If
                    Me.view_Paint(x, y) '標出mark線
                Else
                    For j = 0 To Me.m_aryResult.Count - 1
                        If strTmp = Path.GetFileName(Me.m_aryResult.Index(j).FileName) Then
                            Me.rtbJudgeResultFile.Text = Me.m_aryResult.Index(j).FileName & vbCrLf & Me.m_aryResult.Index(j).Text
                        End If
                    Next j

                    Me.view_Paint()
                End If
            Catch ex As Exception
                'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
            End Try
        Else
            Try
                strTmp = e.Node.Text
                j = e.Node.Text.IndexOf(",")

                If j <> -1 Then
                    i = e.Node.Text.IndexOf("(")
                    k = e.Node.Text.IndexOf(")")

                    x = CInt(e.Node.Text.Substring(i + 1, j - i - 1))
                    y = CInt(e.Node.Text.Substring(j + 1, k - j - 1))
                    DfstrTmp = strTmp
                    Dfx = x
                    Dfy = y

                    Me.rtbJudgeResultFile.Text = ""

                    If x > Me.m_PanelW Or y > Me.m_PanelH Then
                        Me.rtbJudgeResultFile.Text = "Warning: Outside of Boundary!!!"

                    End If
                    Me.view_Paint(x, y) '標出mark線

                    'show defect image
                    Dim DefectImagePath As String = ""
                    For j = 0 To Me.m_aryResult.Count - 1

                        If strTmp.IndexOf("Mura") <> -1 Then
                            For i = 0 To Me.m_aryResult.Index(0).MuraCount - 1
                                mp = Me.m_aryResult.Index(0).GetMuraDf(i)
                                If mp.Data = x And mp.Gate = y Then
                                    DefectImagePath = mp.ImageFilePath + mp.FileName
                                End If
                            Next

                        Else
                            For i = 0 To Me.m_aryResult.Index(0).FuncCount - 1
                                p = Me.m_aryResult.Index(0).GetFuncDf(i)
                                If p.Data = x And p.Gate = y Then
                                    DefectImagePath = p.ImageFilePath + p.FileName
                                End If
                            Next

                        End If
                    Next

                Else
                    For j = 0 To Me.m_aryResult.Count - 1
                        If strTmp = Path.GetFileName(Me.m_aryResult.Index(j).FileName) Then
                            Me.rtbJudgeResultFile.Text = Me.m_aryResult.Index(j).FileName & vbCrLf & Me.m_aryResult.Index(j).Text
                        End If
                    Next j

                    Me.view_Paint()
                End If
                If Me.m_Config.bUseDefectDynamicShowWin Then

                    Me.view_DefectWinShow(Dfx / sx, Dfy / sy)
                End If
            Catch ex As Exception
                'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
            End Try
        End If


    End Sub

    Private Sub teeView_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.HideDefectFrm()
    End Sub

    '---找出defect座標重疊的數量---
    Private Function PointCountAtTree(ByVal intx As Integer, ByVal inty As Integer) As Integer
        Dim W As Single = CSng(picView.Size.Width)
        Dim H As Single = CSng(picView.Size.Height)
        Dim sx, sy As Single
        Dim intRangeX, intRangeY, offsetx, offsety As Integer
        Dim i, j, count As Integer
        Dim f, m, n, x, y As Integer
        Dim strTmp As String
        If Me.m_EnablePanelBMArea Then
            Try
                sx = CSng(Me.m_PanelBMW) / W
                sy = CSng(Me.m_PanelBMH) / H

                intx = Me.CheckRotateData(intx)
                inty = Me.CheckRotateGate(inty)

                offsetx = (Me.m_PanelBMW - Me.m_PanelW) / 2
                offsety = (Me.m_PanelBMH - Me.m_PanelH) / 2

                intRangeX = CInt(ClickRange * sx) 'X axis range=+- 3 subpixel
                intRangeY = CInt(ClickRange * sy) 'Y axis range=+- 3 subpixel

                count = 0
                For j = 0 To Me.teeView.GetNodeCount(False) - 1
                    For i = 0 To Me.teeView.Nodes(j).GetNodeCount(False) - 1

                        strTmp = Me.teeView.Nodes(j).Nodes(i).Text
                        f = strTmp.IndexOf(",")
                        If f <> -1 Then
                            m = strTmp.IndexOf("(")
                            n = strTmp.IndexOf(")")

                            x = CInt(strTmp.Substring(m + 1, f - m - 1))
                            y = CInt(strTmp.Substring(f + 1, n - f - 1))
                        End If

                        If Math.Abs(x + offsetx - intx) <= intRangeX And Math.Abs(y + offsety - inty) <= intRangeY Then
                            count += 1
                        End If
                    Next i
                Next j

                Return count
            Catch ex As Exception
                'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
            End Try
        Else
            Try
                sx = CSng(Me.m_PanelW) / W
                sy = CSng(Me.m_PanelH) / H

                intx = Me.CheckRotateData(intx)
                inty = Me.CheckRotateGate(inty)

                intRangeX = CInt(ClickRange * sx) 'X axis range=+- 3 subpixel
                intRangeY = CInt(ClickRange * sy) 'Y axis range=+- 3 subpixel

                count = 0
                For j = 0 To Me.teeView.GetNodeCount(False) - 1
                    For i = 0 To Me.teeView.Nodes(j).GetNodeCount(False) - 1

                        strTmp = Me.teeView.Nodes(j).Nodes(i).Text
                        f = strTmp.IndexOf(",")
                        If f <> -1 Then
                            m = strTmp.IndexOf("(")
                            n = strTmp.IndexOf(")")

                            x = CInt(strTmp.Substring(m + 1, f - m - 1))
                            y = CInt(strTmp.Substring(f + 1, n - f - 1))
                        End If

                        If Math.Abs(x - intx) <= intRangeX And Math.Abs(y - inty) <= intRangeY Then
                            count += 1
                        End If
                    Next i
                Next j

                Return count
            Catch ex As Exception
                'MessageBox.Show(ex.StackTrace, "ViewJudgeResult Error")
            End Try
        End If

    End Function

    '---顯示mouse在panel上移動的座標---
    Private Sub picView_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picView.MouseMove
        If Not Me.m_Config.bUseDefectDynamicShowWin Then Exit Sub

        Dim sx, sy As Single
        Dim intx, inty, offsetx, offsety As Integer
        Dim intRangeX, intRangeY As Integer
        Dim i, j As Integer
        Dim f, m, n, x, y As Integer
        Dim strTmp As String = ""
        Dim p As New ClsFuncDf
        Dim mp As New ClsMuraDf
        Dim Dfx As Integer = 0
        Dim Dfy As Integer = 0
        Dim DfstrTmp As String = ""
        Dim ary As New ArrayList
        If Me.m_EnablePanelBMArea Then
            Try
                '畫布每格等於幾個SubPiexl
                sx = CSng(Me.m_PanelBMW / Me.picView.Size.Width)
                sy = CSng(Me.m_PanelBMH / Me.picView.Size.Height)

                'defect的data & gate
                intx = CInt(e.X * sx)
                inty = CInt(e.Y * sy)

                intx = Me.CheckRotateData(intx)
                inty = Me.CheckRotateGate(inty)

                offsetx = (Me.m_PanelBMW - Me.m_PanelW) / 2
                offsety = (Me.m_PanelBMH - Me.m_PanelH) / 2

                intRangeX = CInt(ClickRange * sx) 'X axis range=+- 3 subpixel
                intRangeY = CInt(ClickRange * sy) 'Y axis range=+- 3 subpixel

                Me.rtbJudgeResultFile.Text = ""

                For j = 0 To Me.teeView.GetNodeCount(False) - 1
                    For i = 0 To Me.teeView.Nodes(j).GetNodeCount(False) - 1

                        strTmp = Me.teeView.Nodes(j).Nodes(i).Text
                        f = strTmp.IndexOf(",")
                        If f <> -1 Then
                            m = strTmp.IndexOf("(")
                            n = strTmp.IndexOf(")")

                            x = CInt(strTmp.Substring(m + 1, f - m - 1))
                            y = CInt(strTmp.Substring(f + 1, n - f - 1))
                        End If

                        'Point
                        If Math.Abs(x + offsetx - intx) <= intRangeX And Math.Abs(y + offsety - inty) <= intRangeY Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y

                            'V-Line
                        ElseIf (strTmp.ToUpper.IndexOf("V_LINE") <> -1 Or strTmp.ToUpper.IndexOf("V_OPEN") <> -1 Or strTmp.ToUpper.IndexOf("V_BLOCK") <> -1) And (Math.Abs(x + offsetx - intx) <= intRangeX And y = -1) Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y

                            'H-Line
                        ElseIf (strTmp.ToUpper.IndexOf("H_LINE") <> -1 Or strTmp.ToUpper.IndexOf("H_OPEN") <> -1 Or strTmp.ToUpper.IndexOf("H_BLOCK") <> -1) And (x = -1 And Math.Abs(y + offsety - inty) <= intRangeY) Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y

                            'X-Short
                        ElseIf (strTmp.ToUpper.IndexOf("X_SHORT") <> -1) And (Math.Abs(x + offsetx - intx) <= intRangeX Or Math.Abs(y + offsety - inty) <= intRangeY) Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                        End If

                    Next i
                Next j
            Catch ex As Exception
            End Try
        Else
            Try
                sx = CSng(Me.m_PanelW / Me.picView.Size.Width)
                sy = CSng(Me.m_PanelH / Me.picView.Size.Height)

                intx = CInt(e.X * sx)
                inty = CInt(e.Y * sy)

                intx = Me.CheckRotateData(intx)
                inty = Me.CheckRotateGate(inty)

                intRangeX = CInt(ClickRange * sx) 'X axis range=+- 3 subpixel
                intRangeY = CInt(ClickRange * sy) 'Y axis range=+- 3 subpixel

                Me.rtbJudgeResultFile.Text = ""

                For j = 0 To Me.teeView.GetNodeCount(False) - 1
                    For i = 0 To Me.teeView.Nodes(j).GetNodeCount(False) - 1

                        strTmp = Me.teeView.Nodes(j).Nodes(i).Text
                        f = strTmp.IndexOf(",")
                        If f <> -1 Then
                            m = strTmp.IndexOf("(")
                            n = strTmp.IndexOf(")")

                            x = CInt(strTmp.Substring(m + 1, f - m - 1))
                            y = CInt(strTmp.Substring(f + 1, n - f - 1))
                        End If

                        'Point
                        If Math.Abs(x - intx) <= intRangeX And Math.Abs(y - inty) <= intRangeY Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'V-Line
                        ElseIf Math.Abs(x - intx) <= intRangeX And y = -1 Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'H-Line
                        ElseIf x = -1 And Math.Abs(y - inty) <= intRangeY Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                            'X-Short
                        ElseIf (strTmp.ToUpper.IndexOf("X_SHORT") <> -1) And (Math.Abs(x - intx) <= intRangeX Or Math.Abs(y - inty) <= intRangeY) Then
                            Me.rtbJudgeResultFile.Text &= Me.teeView.Nodes(j).Nodes(i).Text & vbCrLf
                            DfstrTmp = strTmp
                            Dfx = x
                            Dfy = y
                        End If

                    Next i
                Next j

                Me.view_Paint(Dfx, Dfy) '標出mark線
                Me.view_DefectWinShow(Dfx / sx, Dfy / sy)
                'show defect image
                Dim DefectImagePath As String = ""
                If DfstrTmp <> "" AndAlso Me.m_Config.bUseDefectDynamicShowWin Then
                    For j = 0 To Me.m_aryResult.Count - 1

                        If DfstrTmp.IndexOf("Mura") <> -1 Then
                            For i = 0 To Me.m_aryResult.Index(0).MuraCount - 1
                                mp = Me.m_aryResult.Index(0).GetMuraDf(i)
                                If mp.Data = Dfx And mp.Gate = Dfy Then
                                    DefectImagePath = mp.ImageFilePath + mp.FileName
                                End If
                            Next

                        Else
                            For i = 0 To Me.m_aryResult.Index(0).FuncCount - 1
                                p = Me.m_aryResult.Index(0).GetFuncDf(i)
                                If p.Data = Dfx And p.Gate = Dfy Then
                                    DefectImagePath = p.ImageFilePath + p.FileName
                                End If
                            Next

                        End If
                    Next
                    'If File.Exists(DefectImagePath) Then
                    '    Me.ShowDefectImage(DefectImagePath, Dfx, Dfy, Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
                    'Else
                    '    Me.NoDefectImage()
                    'End If
                    '25 -> 
                    'Me.ShowDefectFrm(Dfx / sx, Dfy / sy)
                    Refresh()
                Else
                    'Me.HideDefectFrm()
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    '---選擇要顯示defect的CCD No---
    Private Sub chkCCD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCCD1.CheckedChanged, chkCCD2.CheckedChanged, chkCCD3.CheckedChanged, chkCCD4.CheckedChanged, chkCCD5.CheckedChanged, chkCCD6.CheckedChanged, chkCCD7.CheckedChanged, chkCCD8.CheckedChanged, chkCCD9.CheckedChanged
        Me.m_NeedSave = True
        Me.view_Paint()
    End Sub

    '---顯示defect的重疊數量---
    Private Sub chkOverlap_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverlap.CheckedChanged
        Me.m_NeedSave = True
        Me.view_Paint()
    End Sub

    '---選擇Panel轉180度---
    Private Sub chkRotate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRotate.CheckedChanged
        Me.m_NeedSave = True
        Me.view_Paint()
    End Sub

#Region "About UI"
    Private Sub tsmExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmExit.Click
        Me.Close()
    End Sub

    Private Sub tsmPanelSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPanelSize.Click
        Dim dlg As DlgPanelSize = New DlgPanelSize(Me.m_ConfigFileName)

        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.LoadConfig()
            Me.view_Paint()
        End If
    End Sub

    Private Sub tsmDefectColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDefectColor.Click
        Dim dlg As DlgDefectColor = New DlgDefectColor(Me.m_ConfigFileName)

        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.LoadConfig()
            Me.view_Paint()
        End If
    End Sub

    Private Sub tsmDefectView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDefectView.Click
        Dim dlg As DlgSetDefectView = New DlgSetDefectView(Me.m_ConfigFileName)

        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.LoadConfig()
            Me.view_Paint()
            'Me.DefectFrmReSize(Me.m_Config.DefectFrmW, Me.m_Config.DefectFrmH)
            If M_DlgShowDefectFrm.Count > 0 Then
                For count As Integer = 0 To M_DlgShowDefectFrm.Count - 1
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        M_DlgShowDefectFrm(count).Show()
                    Else
                        M_DlgShowDefectFrm(count).Hide()
                    End If
                Next
            End If
            If F_DlgShowDefectFrm.Count > 0 Then
                For count As Integer = 0 To F_DlgShowDefectFrm.Count - 1
                    If Not Me.m_Config.bUseDefectDynamicShowWin Then
                        F_DlgShowDefectFrm(count).Show()
                    Else
                        F_DlgShowDefectFrm(count).Hide()
                    End If
                Next
            End If

        End If
    End Sub

    Private Sub CountUpdate()
        Me.txtBLDP.Text = CStr(intGSDPcount)
        Me.txtDP1.Text = CStr(intDP1count)
        Me.txtDP2.Text = CStr(intDP2count / 2)
        Me.txtDP3.Text = CStr(intDP3count / 3)
        Me.txtDPx.Text = CStr(intDPxcount)
        Me.txtDPn.Text = CStr(intDPncount / 2)
        Me.txtOmitBP.Text = CStr(intOmitBPcount)

        Me.txtGSBP.Text = CStr(intGSBPcount)
        Me.txtBP1.Text = CStr(intBP1count)
        Me.txtBP2.Text = CStr(intBP2count / 2)
        Me.txtBP3.Text = CStr(intBP3count / 3)
        Me.txtBPx.Text = CStr(intBPxcount)
        Me.txtBPn.Text = CStr(intBPncount / 2)

        Me.txtBPDP2.Text = CStr(intBPDP2count / 2)
        Me.txtBPDP3.Text = CStr(intBPDP3count / 3)
        Me.txtBPDPx.Text = CStr(intBPDPxcount)
        Me.txtBPDPn.Text = CStr(intBPDPncount / 2)

        Me.txtXL.Text = CStr(intXLcount / 2)
        Me.txtVL.Text = CStr(intVLcount)
        Me.txtHL.Text = CStr(intHLcount)
        Me.txtVOL.Text = CStr(intVOLcount)
        Me.txtHOL.Text = CStr(intHOLcount)

        Me.txtFG.Text = CStr(intFrameGluecount)
        Me.txtSBB.Text = CStr(intSmallBubblecount)
        Me.txtMBB.Text = CStr(intMiddleBubblecount)
        Me.txtLBB.Text = CStr(intLargeBubblecount)
        Me.txtSGBB.Text = CStr(intSGradeBubblecount)

        Me.txtMURA.Text = CStr(intMURAcount)
        Me.txtCP.Text = CStr(intCPcount)
        Me.txtSBP.Text = CStr(intSBPcount)
    End Sub

    Private Sub CountClear()
        Me.intGSDPcount = 0
        Me.intDP1count = 0
        Me.intDP2count = 0
        Me.intDP3count = 0
        Me.intDPxcount = 0
        Me.intDPncount = 0
        Me.intOmitBPcount = 0

        Me.intGSBPcount = 0
        Me.intBP1count = 0
        Me.intBP2count = 0
        Me.intBP3count = 0
        Me.intBPxcount = 0
        Me.intBPncount = 0

        Me.intBPDP2count = 0
        Me.intBPDP3count = 0
        Me.intBPDPxcount = 0
        Me.intBPDPncount = 0

        Me.intXLcount = 0
        Me.intVLcount = 0
        Me.intHLcount = 0
        Me.intVOLcount = 0
        Me.intHOLcount = 0

        Me.intFrameGluecount = 0
        Me.intSmallBubblecount = 0
        Me.intMiddleBubblecount = 0
        Me.intLargeBubblecount = 0
        Me.intSGradeBubblecount = 0

        Me.intMURAcount = 0
        Me.intCPcount = 0
        Me.intSBPcount = 0

    End Sub

    Private Sub ColorLoad()
        Me.labBLDP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBLDP)
        Me.labDP1.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP1)
        Me.labDP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP2)
        Me.labDP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP3)
        Me.labDPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDPx)
        Me.labDPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDPn)
        Me.labOmitBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorOmitBP)

        Me.labGSBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorGSBP)
        Me.labBP1.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP1)
        Me.labBP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP2)
        Me.labBP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP3)
        Me.labBPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPx)
        Me.labBPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPn)

        Me.labBPDP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP2)
        Me.labBPDP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP3)
        Me.labBPDPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPx)
        Me.labBPDPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPn)

        Me.labXL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorXL)
        Me.labVL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorVL)
        Me.labHL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorHL)
        Me.labVOL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorVOL)
        Me.labHOL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorHOL)

        Me.labMURA.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorMURA)
        Me.labCP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorCP)
        Me.labSBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorSBP)
        '
        Me.penBLDP.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBLDP)
        Me.penDP1.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorDP1)
        Me.penDP2.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorDP2)
        Me.penDP3.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorDP3)
        Me.penDPx.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorDPx)
        Me.penDPn.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorDPn)

        Me.penOMITBP.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorOmitBP)
        Me.penGSBP.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorGSBP)
        Me.penBP1.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBP1)
        Me.penBP2.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBP2)
        Me.penBP3.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBP3)
        Me.penBPx.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPx)
        Me.penBPn.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPn)

        Me.penBPDP2.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP2)
        Me.penBPDP3.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP3)
        Me.penBPDPx.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPx)
        Me.penBPDPn.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPn)

        Me.penXL.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorXL)
        Me.penVL.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorVL)
        Me.penHL.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorHL)
        Me.penVOL.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorVOL)
        Me.penHOL.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorHOL)

        Me.penFG.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorFRAMEGLUE)
        Me.penBB.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorBUBBLE)

        Me.penMURA.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorMURA)
        Me.penCP.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorCP)
        Me.penSBP.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorSBP)

        Me.penMark.Color = System.Drawing.Color.FromArgb(Me.m_Config.colorMark)
        '
        Me.penBLDP.Width = 2.0F
        Me.penDP1.Width = 2.0F
        Me.penDP2.Width = 2.0F
        Me.penDP3.Width = 2.0F
        Me.penDPx.Width = 2.0F
        Me.penDPn.Width = 2.0F

        Me.penOMITBP.Width = 2.0F
        Me.penGSBP.Width = 2.0F
        Me.penBP1.Width = 2.0F
        Me.penBP2.Width = 2.0F
        Me.penBP3.Width = 2.0F
        Me.penBPx.Width = 2.0F
        Me.penBPn.Width = 2.0F

        Me.penBPDP2.Width = 2.0F
        Me.penBPDP3.Width = 2.0F
        Me.penBPDPx.Width = 2.0F
        Me.penBPDPn.Width = 2.0F

        Me.penMURA.Width = 1.0F
        Me.penCP.Width = 2.0F
        Me.penSBP.Width = 2.0F

        Me.penMark.Width = 1.0F
    End Sub
#End Region

#Region "About Config"
    Private Sub LoadConfig()

        Me.m_ConfigFileName = Application.StartupPath & "\" & CONFIG_FILE_NAME

        If File.Exists(Me.m_ConfigFileName) Then
            Me.m_Config = ClsConfig.ReadXML(Me.m_ConfigFileName)
        Else
            Me.m_Config = New ClsConfig
            Me.m_NeedSave = True
        End If

        Me.m_PanelW = Me.m_Config.PanelW
        Me.m_PanelH = Me.m_Config.PanelH
        Me.m_PanelBMW = Me.m_Config.PanelBMW
        Me.m_PanelBMH = Me.m_Config.PanelBMH
        Me.chkRotate.Checked = Me.m_Config.Rotate

        If Me.m_Config.Rotate Then
            Me.labPanelMinW.Text = CStr(Me.m_PanelW)
            Me.labPanelMinH.Text = CStr(Me.m_PanelH)
            Me.labPanelW.Text = "1"
            Me.labPanelH.Text = "1"
        Else
            Me.labPanelMinW.Text = "1"
            Me.labPanelMinH.Text = "1"
            Me.labPanelW.Text = CStr(Me.m_PanelW)
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        Me.ColorLoad()
    End Sub

    Private Sub SaveConfig()
        Try
            Me.m_Config.PanelW = Me.m_PanelW
            Me.m_Config.PanelH = Me.m_PanelH
            Me.m_Config.PanelBMW = Me.m_PanelBMW
            Me.m_Config.PanelBMH = Me.m_PanelBMH
            Me.m_Config.Rotate = Me.chkRotate.Checked

            ClsConfig.WriteXML(Me.m_Config, Me.m_ConfigFileName)

        Catch ex As Exception
            'don't care
        End Try
    End Sub
#End Region


#Region "CCD MODE Buttont觸發事件"
    Private Sub rdo1CCD_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rdo1CCD.MouseDown

        Me.rdo1CCD.Checked = True
        Me.m_CCDModeType = "1"
        '強制關掉chkCCD2~chkCCD9，因為不會用到
        If Me.chkCCD1.Checked = False Then Me.chkCCD1.Checked = True
        If Me.chkCCD2.Checked = True Then Me.chkCCD2.Checked = False
        If Me.chkCCD3.Checked = True Then Me.chkCCD3.Checked = False
        If Me.chkCCD4.Checked = True Then Me.chkCCD4.Checked = False
        If Me.chkCCD5.Checked = True Then Me.chkCCD5.Checked = False
        If Me.chkCCD6.Checked = True Then Me.chkCCD6.Checked = False
        If Me.chkCCD7.Checked = True Then Me.chkCCD7.Checked = False
        If Me.chkCCD8.Checked = True Then Me.chkCCD8.Checked = False
        If Me.chkCCD9.Checked = True Then Me.chkCCD9.Checked = False
    End Sub

    Private Sub rdo2CCD_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rdo2CCD.MouseDown

        Me.rdo2CCD.Checked = True
        Me.m_CCDModeType = "2"
        '強制關掉chkCCD3~chkCCD9，因為不會用到
        If Me.chkCCD1.Checked = False Then Me.chkCCD1.Checked = True
        If Me.chkCCD2.Checked = False Then Me.chkCCD2.Checked = True
        If Me.chkCCD3.Checked = True Then Me.chkCCD3.Checked = False
        If Me.chkCCD4.Checked = True Then Me.chkCCD4.Checked = False
        If Me.chkCCD5.Checked = True Then Me.chkCCD5.Checked = False
        If Me.chkCCD6.Checked = True Then Me.chkCCD6.Checked = False
        If Me.chkCCD7.Checked = True Then Me.chkCCD7.Checked = False
        If Me.chkCCD8.Checked = True Then Me.chkCCD8.Checked = False
        If Me.chkCCD9.Checked = True Then Me.chkCCD9.Checked = False
    End Sub


    Private Sub rdo3CCD_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rdo3CCD.MouseDown

        Me.rdo3CCD.Checked = True
        Me.m_CCDModeType = "3"
        '強制關掉chkCCD4~chkCCD9，因為不會用到
        If Me.chkCCD1.Checked = False Then Me.chkCCD1.Checked = True
        If Me.chkCCD2.Checked = False Then Me.chkCCD2.Checked = True
        If Me.chkCCD3.Checked = False Then Me.chkCCD3.Checked = True
        If Me.chkCCD4.Checked = True Then Me.chkCCD4.Checked = False
        If Me.chkCCD5.Checked = True Then Me.chkCCD5.Checked = False
        If Me.chkCCD6.Checked = True Then Me.chkCCD6.Checked = False
        If Me.chkCCD7.Checked = True Then Me.chkCCD7.Checked = False
        If Me.chkCCD8.Checked = True Then Me.chkCCD8.Checked = False
        If Me.chkCCD9.Checked = True Then Me.chkCCD9.Checked = False
    End Sub

    Private Sub rdo4CCD_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rdo4CCD.MouseDown

        Me.rdo4CCD.Checked = True
        Me.m_CCDModeType = "4"
        '強制關掉chkCCD5~chkCCD9，因為不會用到
        If Me.chkCCD1.Checked = False Then Me.chkCCD1.Checked = True
        If Me.chkCCD2.Checked = False Then Me.chkCCD2.Checked = True
        If Me.chkCCD3.Checked = False Then Me.chkCCD3.Checked = True
        If Me.chkCCD4.Checked = False Then Me.chkCCD4.Checked = True
        If Me.chkCCD5.Checked = True Then Me.chkCCD5.Checked = False
        If Me.chkCCD6.Checked = True Then Me.chkCCD6.Checked = False
        If Me.chkCCD7.Checked = True Then Me.chkCCD7.Checked = False
        If Me.chkCCD8.Checked = True Then Me.chkCCD8.Checked = False
        If Me.chkCCD9.Checked = True Then Me.chkCCD9.Checked = False
    End Sub

    Private Sub rdo9CCD_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rdo9CCD.MouseDown

        Me.rdo9CCD.Checked = True
        Me.m_CCDModeType = "9"
        If Me.chkCCD1.Checked = False Then Me.chkCCD1.Checked = True
        If Me.chkCCD2.Checked = False Then Me.chkCCD2.Checked = True
        If Me.chkCCD3.Checked = False Then Me.chkCCD3.Checked = True
        If Me.chkCCD4.Checked = False Then Me.chkCCD4.Checked = True
        If Me.chkCCD5.Checked = False Then Me.chkCCD5.Checked = True
        If Me.chkCCD6.Checked = False Then Me.chkCCD6.Checked = True
        If Me.chkCCD7.Checked = False Then Me.chkCCD7.Checked = True
        If Me.chkCCD8.Checked = False Then Me.chkCCD8.Checked = True
        If Me.chkCCD9.Checked = False Then Me.chkCCD9.Checked = True
    End Sub
#End Region

    Private Sub chkBMMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBMMode.CheckedChanged
        Me.m_NeedSave = True
        Me.m_EnablePanelBMArea = Me.chkBMMode.Checked
        Me.view_Paint()
    End Sub

    Private Sub ckbOpMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbOpMode.CheckedChanged
        Me.txtAoiJudgerResult.Visible = Me.ckbOpMode.Checked
    End Sub

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

        Try
            Me.LoadConfig()
            Me.Text = Me.m_Config.SubViewName & " [Version]: " & Application.ProductVersion

            If Me.m_ViewerLog Is Nothing Then
                Me.m_ViewerLog = New ClsLogRecorder
                Try
                    Me.m_ViewerLog.Open(Me.m_Config.LogPath, "LogViewer")
                    Me.m_ViewerLog.Close()
                Catch ex As Exception
                    Me.m_ViewerLog = Nothing
                    Throw New Exception("Open Judger Log fail!")
                End Try
            End If

            Me.penMark.DashPattern = New Single() {4.0F, 4.0F, 4.0F, 4.0F}
            Me.m_ToolTipMsg = New ToolTip
            Me.teeView.Nodes.Add("JudgeResult File")
            Me.UI_PanelStatus("", "")
            Me.Size = New Size(Me.m_Config.WinWidth / 2, Me.m_Config.WinHeight)
            Me.SplitContainer_OKNGShow.Panel1Collapsed = False
            Me.SplitContainer_OKNGShow.Panel2Collapsed = True
            Me.SplitContainer_OKNGTitle.Panel1Collapsed = False
            Me.SplitContainer_OKNGTitle.Panel2Collapsed = True
            Me.TabControl_ShowDefect.TabPages.Remove(Me.TabPage_Luminance)

        Catch ex As Exception
            Me.Close()
            End
        End Try

    End Sub

    Public Sub Set_View_Foramt(ByVal FactoryNo As String, ByVal ViewHorizontal As Boolean, ByVal Data As Integer, ByVal Gate As Integer, ByVal CCDMode As Integer, ByVal ShowViewerCcdNoType As String)
        Dim t1, t2 As Integer
        Dim h, w As Integer
        Me.ViewerLog("Start" & "SET_VIEW_FORMAT")
        t1 = Environment.TickCount
        '初使狀態，Data代表短邊
        If ViewHorizontal Then
            'Data代表長邊
            w = Me.picView.Width
            h = Me.picView.Height
            Me.picView.Height = w
            Me.picView.Width = h
            Me.labPanelW.Location = New Point(Me.picView.Location.X + Me.picView.Width - Me.labPanelW.Size.Width, Me.labPanelW.Location.Y)
            Me.labPanelH.Location = New Point(Me.labPanelH.Location.X, Me.picView.Location.Y + Me.picView.Height - labPanelH.Size.Height)
            Me.Size = New Size(Me.m_Config.WinWidth / 2, Me.m_Config.WinHeight)
            'Me.SplitContainer1.Location = New Point(Me.picView.Location.X, Me.labPanelW.Location.Y + Me.picView.Height + 30)
            'w = Me.SplitContainer1.Width
            'h = Me.SplitContainer1.Height
            'Me.SplitContainer1.Height = w
            'Me.SplitContainer1.Width = h
            'Me.SplitContainer1.Orientation = Orientation.Vertical
            'Me.SplitContainer1.SplitterDistance = Me.SplitContainer1.Width / 2

        End If


        If Me.m_PanelW <> Data Then
            Me.m_NeedSave = True
            Me.m_Config.PanelW = Data
            Me.m_PanelW = Me.m_Config.PanelW
            Me.labPanelW.Text = CStr(Me.m_PanelW)
        End If

        If Me.m_PanelH <> Gate Then
            Me.m_NeedSave = True
            Me.m_Config.PanelH = Gate
            Me.m_PanelH = Me.m_Config.PanelH
            Me.labPanelH.Text = CStr(Me.m_PanelH)
        End If

        Me.m_CCDModeType = CCDMode

        If ShowViewerCcdNoType = "OnlyFunc" Then
            Me.m_Config.bFuncCcdNo = True
            Me.m_Config.bMuraCcdNo = False
        ElseIf ShowViewerCcdNoType = "OnlyMura" Then
            Me.m_Config.bFuncCcdNo = False
            Me.m_Config.bMuraCcdNo = True
        ElseIf ShowViewerCcdNoType = "Both" Then
            Me.m_Config.bFuncCcdNo = True
            Me.m_Config.bMuraCcdNo = True
        ElseIf ShowViewerCcdNoType = "None" Then
            Me.m_Config.bFuncCcdNo = False
            Me.m_Config.bMuraCcdNo = False
        Else
            Me.m_Config.bFuncCcdNo = False
            Me.m_Config.bMuraCcdNo = False
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("SET_VIEW_FORMAT" & t2 - t1)
    End Sub

    Public Sub Set_Chip_Info(ByVal FactoryNo As String, ByVal PanelID As String, ByVal ResultPathName As String, ByVal MQRank As String, ByVal CSTRank As String, _
                             ByVal AGSRank As String, ByVal MainDefect As String, ByVal LogJudgeResultMsg As String, ByVal MainDefectData As String, ByVal MainDefectgate As String, _
                             ByVal Data As Integer, ByVal Gate As Integer, ByVal sCheckPanel As String, Optional ByVal CameraPathType As String = "SINGLE")
        Dim t1, t2 As Integer
        Dim tMeasure1, tMeasure2 As Integer
        Me.ViewerLog("Start" & "SET_CHIP_INFO")
        t1 = Environment.TickCount


        For index As Integer = 0 To M_DlgShowDefectFrm.Count - 1
            M_DlgShowDefectFrm(index).Close()
            M_DlgShowDefectFrm(index).Dispose()
        Next
        M_DlgShowDefectFrm.Clear()
        _M_DefectFrmPositionList.Clear()
        For index As Integer = 0 To F_DlgShowDefectFrm.Count - 1
            F_DlgShowDefectFrm(index).Close()
            F_DlgShowDefectFrm(index).Dispose()
        Next
        F_DlgShowDefectFrm.Clear()
        _F_DefectFrmPositionList.Clear()
        m_FirstTimeCreateDefectList = True

        If ResultPathName <> "" Then
            MeasureAVG.Clear()
            Measure5U.Clear()
            Measure13U.Clear()
            Me.ViewerLog("Start LUMINANCE_Measure")
            tMeasure1 = Environment.TickCount
            Dim sPath As String = ResultPathName
            sPath = sPath.Substring(0, sPath.Length - 4) + "_Measure.txt"
            If System.IO.File.Exists(sPath) Then
                If Not Me.lb_MeasureAve.Visible Then
                    Me.teeView.Size = New Point(Me.teeView.Size.Width, Me.teeView.Size.Height - 10)
                    Me.rtbJudgeResultFile.Size = New Point(Me.rtbJudgeResultFile.Size.Width, Me.rtbJudgeResultFile.Size.Height - 10)
                    Me.rtbJudgeResultFile.Location = New Point(Me.rtbJudgeResultFile.Location.X, Me.rtbJudgeResultFile.Location.Y + 10)
                    Me.lb_MeasureAve.Visible = True
                    Me.lb_Measure5U.Visible = True
                    Me.lb_Measure13U.Visible = True
                    Me.txt_MeasureAvg.Visible = True
                    Me.txt_Measure5U.Visible = True
                    Me.txt_Measure13U.Visible = True
                End If
                sMeasureResult.Clear()
                Dim sw As StreamReader
                sw = File.OpenText(sPath)
                Dim sLine As String

                While True
                    sLine = sw.ReadLine()
                    If sLine Is Nothing Then
                        Exit While
                    End If
                    If sLine <> "" Then
                        Me.sMeasureResult.Add(sLine)
                    End If
                End While

                Dim sModelName As String = Me.sMeasureResult(0)
                Dim tmpParam As String = Me.sMeasureResult(1)
                Me.sMeasureResult.RemoveRange(0, 2)

                sMeasureParam = tmpParam.Split(",")
                Me.lb_MeasureParam.Text = "平均值上限 : " + sMeasureParam(0) + vbCrLf + "平均值下限 : " + sMeasureParam(1) + vbCrLf +
                                          "5點均勻度上限 : " + sMeasureParam(2) + vbCrLf + "5點均勻度下限 : " + sMeasureParam(3) + vbCrLf +
                                          "13點均勻度上限 : " + sMeasureParam(4) + vbCrLf + "13點均勻度下限 : " + sMeasureParam(5) + vbCrLf +
                                          "Row : " + sMeasureParam(6) + vbCrLf + "Column : " + sMeasureParam(7)

                cbo_MeasurePatternName.Items.Clear()

                For i As Integer = 0 To Me.sMeasureResult.Count - 1
                    Dim sTmp() As String
                    sTmp = Me.sMeasureResult(i).Split(",")
                    cbo_MeasurePatternName.Items.Add(sTmp(0))
                    MeasureAVG.Add(sTmp(26))
                    Measure5U.Add(sTmp(27))
                    Measure13U.Add(sTmp(28))
                    If i = 0 Then
                        If sTmp(29) = "NG" Then
                            lb_MeasureAve.BackColor = Color.Red
                            lb_MeasureAve.ForeColor = Color.Yellow
                            'cbo_MeasureAvg.BackColor = Color.Red
                        Else
                            lb_MeasureAve.BackColor = Color.Green
                            'cbo_MeasureAvg.BackColor = Color.Green
                        End If
                        If sTmp(30) = "NG" Then
                            lb_Measure5U.BackColor = Color.Red
                            'cbo_MeasureEve5.BackColor = Color.Red
                        Else
                            lb_Measure5U.BackColor = Color.Green
                            'cbo_MeasureEve5.BackColor = Color.Green
                        End If
                        If sTmp(31) = "NG" Then
                            lb_Measure13U.BackColor = Color.Red
                            'cbo_MeasureEve13.BackColor = Color.Red
                        Else
                            lb_Measure13U.BackColor = Color.Green
                            'cbo_MeasureEve13.BackColor = Color.Green
                        End If
                        If sTmp(32) = "NG" Then
                            Me.txtLuminanceResult.Text = "NG"
                            Me.txtLuminanceResult.ForeColor = Color.Yellow
                            Me.txtLuminanceResult.BackColor = Color.Red
                        Else
                            Me.txtLuminanceResult.Text = "OK"
                            Me.txtLuminanceResult.ForeColor = Color.Black
                            Me.txtLuminanceResult.BackColor = Color.Lime
                        End If
                        Me.SplitContainer_OKNGShow.Panel1Collapsed = False
                        Me.SplitContainer_OKNGShow.Panel2Collapsed = False
                        Me.SplitContainer_OKNGTitle.Panel1Collapsed = False
                        Me.SplitContainer_OKNGTitle.Panel2Collapsed = False
                        If Me.TabControl_ShowDefect.TabPages.Contains(Me.TabPage_Luminance) = False Then
                            Me.TabControl_ShowDefect.TabPages.Add(Me.TabPage_Luminance)
                        End If
                    End If

                Next
                cbo_MeasurePatternName.SelectedIndex = -1
                If cbo_MeasurePatternName.Items.Count <> 0 Then
                    cbo_MeasurePatternName.SelectedIndex = 0
                End If
                If MeasureAVG.Count <> 0 Then
                    txt_MeasureAvg.Text = MeasureAVG(0)
                End If
                If Measure5U.Count <> 0 Then
                    txt_Measure5U.Text = Measure5U(0)
                End If
                If Measure13U.Count <> 0 Then
                    txt_Measure13U.Text = Measure13U(0)
                End If
                sw.Close()
                tMeasure2 = Environment.TickCount
                Me.ViewerLog("LUMINANCE_Measure" & tMeasure2 - tMeasure1)
                'cbo_MeasurePatternName.SelectedIndex = 0
                'cbo_MeasureAvg.SelectedIndex = 0
                'cbo_MeasureEve5.SelectedIndex = 0
                'cbo_MeasureEve13.SelectedIndex = 0
                'Dim sTmp1() As String
                'sTmp1 = sMeasureResult(cbo_MeasurePatternName.SelectedIndex).Split(",")
                'For i As Integer = 0 To sMeasureParam(6) - 1
                '    dgv_MeasureResult.Rows.Add(New Object() {sTmp1(i * sMeasureParam(7) + 1), sTmp1(i * sMeasureParam(7) + 2), sTmp1(i * sMeasureParam(7) + 3), sTmp1(i * sMeasureParam(7) + 4), sTmp1(i * sMeasureParam(7) + 5)})
                'Next
            Else
                Me.txtPanelID.Text = ""
                Me.txtAgsRank.Text = ""
                Me.txtCstRank.Text = ""
                Me.txtMainDefect.Text = ""
                Me.txtPanelID.Enabled = False
                Me.txtAgsRank.Enabled = False
                Me.txtCstRank.Enabled = False
                Me.txtMainDefect.Enabled = False
                Me.SplitContainer_OKNGShow.Panel1Collapsed = False
                Me.SplitContainer_OKNGShow.Panel2Collapsed = True
                Me.SplitContainer_OKNGTitle.Panel1Collapsed = False
                Me.SplitContainer_OKNGTitle.Panel2Collapsed = True
                If Me.TabControl_ShowDefect.TabPages.Contains(Me.TabPage_Luminance) Then
                    Me.TabControl_ShowDefect.TabPages.Remove(Me.TabPage_Luminance)
                End If
                If Me.lb_MeasureAve.Visible AndAlso Me.teeView.Size.Height + Me.teeView.Location.Y + 20 < Me.rtbJudgeResultFile.Location.Y Then
                    Me.teeView.Size = New Point(Me.teeView.Size.Width, Me.teeView.Size.Height + 10)
                    Me.rtbJudgeResultFile.Size = New Point(Me.rtbJudgeResultFile.Size.Width, Me.rtbJudgeResultFile.Size.Height + 10)
                    Me.rtbJudgeResultFile.Location = New Point(Me.rtbJudgeResultFile.Location.X, Me.rtbJudgeResultFile.Location.Y - 10)
                    Me.lb_MeasureAve.Visible = False
                    Me.lb_Measure5U.Visible = False
                    Me.lb_Measure13U.Visible = False
                    Me.txt_MeasureAvg.Visible = False
                    Me.txt_Measure5U.Visible = False
                    Me.txt_Measure13U.Visible = False
                    If Me.TabControl_ShowDefect.TabPages.Contains(Me.TabPage_Luminance) Then
                        Me.TabControl_ShowDefect.TabPages.Remove(Me.TabPage_Luminance)
                    End If
                End If
            End If
            If System.IO.File.Exists(ResultPathName) = True Then
                Me.teeView.Nodes.Add("JudgeResult File")

                ReDim m_AllFiles(0)
                Me.m_AllFiles(0) = ResultPathName
                Me.m_FilePath = ResultPathName.Substring(0, ResultPathName.LastIndexOf("\") + 1)
                Me.m_PanelID = PanelID
                Me.txtPanelID.Enabled = True
                Me.txtPanelID.Text = PanelID

                Me.m_CSTRank = CSTRank
                Me.txtCstRank.Enabled = True
                Me.txtCstRank.Text = CSTRank

                'Me.picDfImg.Visible = False

                Me.m_AGSRank = AGSRank
                Me.txtAgsRank.Enabled = True
                Me.txtAgsRank.Text = AGSRank

                Me.m_MainDefect = MainDefect
                Me.txtMainDefect.Enabled = True
                Me.txtMainDefect.Text = MainDefect

                If Me.m_PanelW <> Data Then
                    Me.m_NeedSave = True
                    Me.m_Config.PanelW = Data
                    Me.m_PanelW = Me.m_Config.PanelW
                    Me.labPanelW.Text = CStr(Me.m_PanelW)
                End If

                If Me.m_PanelH <> Gate Then
                    Me.m_NeedSave = True
                    Me.m_Config.PanelH = Gate
                    Me.m_PanelH = Me.m_Config.PanelH
                    Me.labPanelH.Text = CStr(Me.m_PanelH)
                End If

                If FactoryNo = "S16" Then
                    Me.UI_PanelStatus(LogJudgeResultMsg, sCheckPanel)
                Else
                    If CSTRank = "G" Then
                        If sCheckPanel = "TRUE" Then
                            txtAoiJudgerResult.Text = "OK(抽檢)"
                            txtAoiJudgerResult.ForeColor = Color.Red
                            txtAoiJudgerResult.BackColor = Color.Lime
                        Else
                            txtAoiJudgerResult.Text = "OK"
                            txtAoiJudgerResult.BackColor = Color.Lime
                        End If
                    Else
                        If sCheckPanel = "TRUE" Then
                            If MainDefect = "OTHER_GLASS_DEFECT" OrElse MainDefect = "OTHER_ALIGN_DEFECT" Then
                                txtAoiJudgerResult.Text = "画异"
                                txtAoiJudgerResult.ForeColor = Color.Blue
                                txtAoiJudgerResult.BackColor = Color.Red
                            Else
                                txtAoiJudgerResult.Text = "NG(抽檢)"
                                txtAoiJudgerResult.ForeColor = Color.Blue
                                txtAoiJudgerResult.BackColor = Color.Red
                            End If

                        Else
                            If MainDefect = "OTHER_GLASS_DEFECT" OrElse MainDefect = "OTHER_ALIGN_DEFECT" Then
                                txtAoiJudgerResult.Text = "画异"
                                txtAoiJudgerResult.BackColor = Color.Red
                            Else
                                txtAoiJudgerResult.Text = "NG"
                                txtAoiJudgerResult.BackColor = Color.Red
                            End If

                        End If
                    End If
                End If

                If AGSRank = "Z" OrElse AGSRank = "P" Then
                    txtAoiJudgerAGSResult.Text = AGSRank
                    txtAoiJudgerAGSResult.ForeColor = Color.Red
                    txtAoiJudgerAGSResult.BackColor = Color.Lime
                Else
                    txtAoiJudgerAGSResult.Text = "N"
                    txtAoiJudgerAGSResult.BackColor = Color.Red
                End If

                MuraJudgePartition.Clear()
                Select Case CameraPathType
                    Case "SINGLE"
                        MuraJudgePartition.TotalPartition = 1
                    Case "TWO"
                        MuraJudgePartition.TotalPartition = 2
                    Case "FOUR"
                        MuraJudgePartition.TotalPartition = 4
                    Case "SIX"
                        MuraJudgePartition.TotalPartition = 6
                    Case "NINE"
                        MuraJudgePartition.TotalPartition = 9
                End Select

                Me.Run()
                Me.MainDefectShow(MainDefectData, MainDefectgate)

                If Me.m_Config.bShowMuraDlg Then
                    Me.btnMuraShow.Enabled = True
                    Me.m_ViewResultInfo = FactoryNo & ";" & PanelID & ";" & ResultPathName & ";" & MQRank & ";" & CSTRank & ";" & AGSRank & ";" & MainDefect & ";" & LogJudgeResultMsg & ";" & MainDefectData & ";" & MainDefectgate & ";" & Data & ";" & Gate & ";" & sCheckPanel
                    Dim dlg As New MuraShow(Me.m_PanelID, Me.m_FilePath, Me.m_PanelW, Me.m_PanelH, Me.m_aryResult.Index(0).GetMuraAry, Me.m_ViewResultInfo) '只秀第一片
                    dlg.Show()
                End If

            Else 'file doesn't exist

                'Me.Close()     '20200122 Mark 為什麼要自己關掉??
                'End            '20200122 Mark 為什麼要自己關掉??
            End If
        Else  '檢測中
            Me.txtPanelID.Text = ""
            Me.txtAgsRank.Text = ""
            Me.txtCstRank.Text = ""
            Me.txtMainDefect.Text = ""
            Me.txtPanelID.Enabled = False
            Me.txtAgsRank.Enabled = False
            Me.txtCstRank.Enabled = False
            Me.txtMainDefect.Enabled = False
            Me.SplitContainer_OKNGShow.Panel1Collapsed = False
            Me.SplitContainer_OKNGShow.Panel2Collapsed = True
            Me.SplitContainer_OKNGTitle.Panel1Collapsed = False
            Me.SplitContainer_OKNGTitle.Panel2Collapsed = True
            If Me.TabControl_ShowDefect.TabPages.Contains(Me.TabPage_Luminance) Then
                Me.TabControl_ShowDefect.TabPages.Remove(Me.TabPage_Luminance)
            End If
            Me.UI_PanelStatus("檢測中", "")
        End If
        t2 = Environment.TickCount
        Me.ViewerLog("SET_CHIP_INFO" & t2 - t1)
    End Sub


    Delegate Sub UI_PanelStatusCallback(ByVal msg As String, ByVal sCheckPanel As String)
    Private Sub UI_PanelStatus(ByVal msg As String, ByVal sCheckPanel As String)
        If msg = "" Then
            txtAoiJudgerAGSResult.Text = msg
            txtAoiJudgerAGSResult.BackColor = Color.White
        ElseIf msg = "檢測中" Then
            txtAoiJudgerAGSResult.Text = msg
            txtAoiJudgerAGSResult.BackColor = Color.DarkOrange
        End If
        If Me.txtAoiJudgerResult.InvokeRequired Then
            Me.Invoke(New UI_PanelStatusCallback(AddressOf Me.UI_PanelStatus), New Object() {msg, sCheckPanel})
        Else
            If msg = "良品" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "良品(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Lime
                Else
                    txtAoiJudgerResult.Text = "良品"
                    txtAoiJudgerResult.BackColor = Color.Lime
                End If

            ElseIf msg = "不良品" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "不良品(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Magenta
                Else
                    txtAoiJudgerResult.Text = "不良品"
                    txtAoiJudgerResult.BackColor = Color.Magenta
                End If

            ElseIf msg = "點不良" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "點不良(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Red
                Else
                    txtAoiJudgerResult.Text = "點不良"
                    txtAoiJudgerResult.BackColor = Color.Red
                End If

            ElseIf msg = "覆判" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "覆判(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Yellow
                Else
                    txtAoiJudgerResult.Text = "覆判"
                    txtAoiJudgerResult.BackColor = Color.Yellow
                End If

            ElseIf msg = "畫異" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "畫異(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Pink

                Else
                    txtAoiJudgerResult.Text = "畫異"
                    txtAoiJudgerResult.BackColor = Color.Pink
                End If

            ElseIf msg = "MURA" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "MURA(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.Blue
                Else
                    txtAoiJudgerResult.Text = "MURA"
                    txtAoiJudgerResult.BackColor = Color.Blue
                End If

            ElseIf msg = "線不良" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "線不良(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                Else
                    txtAoiJudgerResult.Text = "線不良"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                End If

            ElseIf msg = "水平線缺陷" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "水平線缺陷(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                Else
                    txtAoiJudgerResult.Text = "水平線缺陷"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                End If

            ElseIf msg = "垂直線缺陷" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "垂直線缺陷(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                Else
                    txtAoiJudgerResult.Text = "垂直線缺陷"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                End If

            ElseIf msg = "水平線開路" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "水平線開路(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                Else
                    txtAoiJudgerResult.Text = "水平線開路"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                End If

            ElseIf msg = "垂直線開路" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "垂直線開路(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                Else
                    txtAoiJudgerResult.Text = "垂直線開路"
                    txtAoiJudgerResult.BackColor = Color.DeepPink
                End If

            ElseIf msg = "檢測中" Then
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = "檢測中(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.DarkOrange
                Else
                    txtAoiJudgerResult.Text = "檢測中"
                    txtAoiJudgerResult.BackColor = Color.DarkOrange
                End If

            Else
                If sCheckPanel = "TRUE" Then
                    txtAoiJudgerResult.Text = msg & Environment.NewLine & "(抽檢)"
                    txtAoiJudgerResult.BackColor = Color.White
                Else
                    txtAoiJudgerResult.Text = msg
                    txtAoiJudgerResult.BackColor = Color.White
                End If
            End If
        End If
    End Sub

    Private Sub rtbJudgeResultFile_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtbJudgeResultFile.MouseMove
        Dim PositionCurosorIsIn As Integer = rtbJudgeResultFile.GetCharIndexFromPosition(e.Location)
        Dim LineCurosorIsIn As Integer = rtbJudgeResultFile.GetLineFromCharIndex(PositionCurosorIsIn)
        If LineCurosorIsIn <= rtbJudgeResultFile.Lines.Length - 1 Then

            Dim str As String = rtbJudgeResultFile.Lines(LineCurosorIsIn).Replace(vbTab, " ")

            Me.m_ToolTipMsg.SetToolTip(rtbJudgeResultFile, str)
        End If
    End Sub

    Private Sub OpenNotePadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenNotePadToolStripMenuItem.Click
        Dim dlg As OpenFileDialog
        Dim i As Integer
        Dim SelFileNames As String()

        dlg = New OpenFileDialog

        dlg.Filter = "JudgeResult(*.txt)|*.txt|(*.*)|*.*"
        dlg.FilterIndex = 1
        dlg.FileName = ""
        dlg.Title = "Open JudgeResult File"
        dlg.InitialDirectory = Me.m_Config.InitialFolderPath & DateTime.Today.Year & DateTime.Today.Month & DateTime.Today.Day
        dlg.Multiselect = True
        dlg.RestoreDirectory = True

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            SelFileNames = dlg.FileNames
            For i = 0 To SelFileNames.Length - 1
                Shell("notepad.exe " & SelFileNames(i), vbNormalFocus)
            Next
        End If
    End Sub

    Private Sub btnMuraShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMuraShow.Click
        If Me.m_ViewResultInfo <> "" Then
            Dim dlg As New MuraShow(Me.m_PanelID, Me.m_FilePath, Me.m_PanelW, Me.m_PanelH, Me.m_aryResult.Index(0).MuraDfList, Me.m_ViewResultInfo) '只秀第一片
            dlg.Show()
        Else
            MessageBox.Show("請先OpenFile!")
        End If
    End Sub

    Private Sub FrmMain_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged

        If MyBase.Size.Width >= 500 AndAlso MyBase.Size.Height >= 500 Then
            '--------------------------------------Defect.txt---------------------------------------
            teeView.Size = New System.Drawing.Size(MyBase.Size.Width - 420, Me.teeView.Size.Height)
            rtbJudgeResultFile.Size = New System.Drawing.Size(MyBase.Size.Width - 420, Me.rtbJudgeResultFile.Size.Height)
            '--------------------------------------Defect Map--------------------------------------
            TabControl_ShowDefect.Size = New System.Drawing.Size(MyBase.Size.Width, MyBase.Size.Height)
            grpDefectMapping.Size = New System.Drawing.Size(MyBase.Size.Width - 25, MyBase.Size.Height - 330)
            picView.Size = New System.Drawing.Size(MyBase.Size.Width - 56, MyBase.Size.Height - 370)
            labPanelW.Location = New Point(MyBase.Size.Width - 67, 8)
            labPanelH.Location = New Point(4, MyBase.Size.Height - 350)
            '--------------------------------------------------------------------------------------
            gbSettingTool.Location = New Point(MyBase.Size.Width, 30)
            Me.view_Paint()
            '--------------------------------------------------------------------------------------
        End If
    End Sub

#Region "宮格/視角設定"
    Private Sub rbMuraPartition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMuraPartition.CheckedChanged
        If rbMuraPartition.Checked = True Then
            Me.rbDetailInfo.Checked = False
            Me.view_Paint()
        End If
    End Sub

    Private Sub rbDetailInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDetailInfo.CheckedChanged
        If rbDetailInfo.Checked = True Then
            Me.rbMuraPartition.Checked = False
            Me.view_Paint()
        End If
    End Sub

    Private Sub rbNormalView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbNormalView.CheckedChanged
        If rbNormalView.Checked = True Then
            rbLeftView.Checked = False
            rbRightView.Checked = False
            Me.view_Paint()
            'Me.picView.BackColor = System.Drawing.SystemColors.ControlDarkDark
        End If
    End Sub

    Private Sub rbLeftView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbLeftView.CheckedChanged
        If rbLeftView.Checked = True Then
            rbNormalView.Checked = False
            rbRightView.Checked = False
            Me.view_Paint()
            'Me.picView.BackColor = System.Drawing.SystemColors.Control
        End If
    End Sub

    Private Sub rbRightView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRightView.CheckedChanged
        If rbRightView.Checked = True Then
            rbNormalView.Checked = False
            rbLeftView.Checked = False
            Me.view_Paint()
            'Me.picView.BackColor = System.Drawing.SystemColors.Control
        End If
    End Sub

    Private Sub FrmMain_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub FrmMain_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Me.m_AllFiles = files
        Me.m_PanelID = Me.m_AllFiles(0).Substring(Me.m_AllFiles(0).LastIndexOf("\") + 1, Me.m_AllFiles(0).LastIndexOf(".txt") - Me.m_AllFiles(0).LastIndexOf("\") - 1)
        Me.m_FilePath = Me.m_AllFiles(0).Substring(0, Me.m_AllFiles(0).LastIndexOf("\") + 1)

        Dim fsReader As StreamReader = New StreamReader(Me.m_AllFiles(0))
        Dim strfsText As String = fsReader.ReadToEnd
        Dim strfsLine() As String = strfsText.Split(vbCrLf)

        Dim strViewResultInfo As String() = Nothing
        For i As Integer = 0 To strfsLine.Length - 1
            If strfsLine(i).Trim.ToUpper = "VIEWRESULTINFO" Then
                strViewResultInfo = strfsLine(i + 1).Trim.Split(";")
                strViewResultInfo(2) = Me.m_AllFiles(0)
                Me.m_ViewResultInfo = strViewResultInfo(0) & ";" & strViewResultInfo(1) & ";" & Me.m_AllFiles(0) & ";" & strViewResultInfo(3) & ";" & strViewResultInfo(4) & ";" &
                                        strViewResultInfo(5) & ";" & strViewResultInfo(6) & ";" & strViewResultInfo(7) & ";" & strViewResultInfo(8) & ";" & strViewResultInfo(9) & ";" &
                                        strViewResultInfo(10) & ";" & strViewResultInfo(11) & ";" & strViewResultInfo(12)
                Me.m_ViewResultInfo = strfsLine(i + 1).Trim
                Exit For
            End If
        Next
        fsReader.Close()

        Me.Set_Chip_Info(strViewResultInfo(0), strViewResultInfo(1), strViewResultInfo(2), strViewResultInfo(3), strViewResultInfo(4), strViewResultInfo(5), strViewResultInfo(6), strViewResultInfo(7), strViewResultInfo(8), strViewResultInfo(9), strViewResultInfo(10), strViewResultInfo(11), strViewResultInfo(12))

    End Sub

#End Region

    Private Sub cbo_MeasurePatternName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_MeasurePatternName.SelectedIndexChanged
        'For i As Integer = dgv_MeasureResult.Rows.Count - 2 To 0 Step -1
        '    dgv_MeasureResult.Rows.RemoveAt(i)
        'Next
        'For i As Integer = 0 To dgv_MeasureResult.Rows.Count Step +1
        '    Console.WriteLine(dgv_MeasureResult.Rows(i))
        'Next

        If cbo_MeasurePatternName.SelectedIndex <> -1 Then
            dgv_MeasureResult.Rows.Clear()


            txt_MeasureAvg.Text = MeasureAVG(cbo_MeasurePatternName.SelectedIndex)
            txt_Measure5U.Text = MeasureAVG(cbo_MeasurePatternName.SelectedIndex)
            txt_Measure13U.Text = MeasureAVG(cbo_MeasurePatternName.SelectedIndex)
            Dim sTmp1() As String
            sTmp1 = sMeasureResult(cbo_MeasurePatternName.SelectedIndex).Split(",")
            For i As Integer = 1 To 25
                If sTmp1(i) = 0 Then sTmp1(i) = ""
            Next
            For i As Integer = 0 To sMeasureParam(6) - 1
                dgv_MeasureResult.Rows.Add(New Object() {sTmp1(i * sMeasureParam(7) + 1), sTmp1(i * sMeasureParam(7) + 2), sTmp1(i * sMeasureParam(7) + 3), sTmp1(i * sMeasureParam(7) + 4), sTmp1(i * sMeasureParam(7) + 5)})
            Next
        End If


    End Sub

    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.LoadConfig()
            Me.Text = Me.m_Config.SubViewName & " [Version]: " & Application.ProductVersion

            Me.penMark.DashPattern = New Single() {4.0F, 4.0F, 4.0F, 4.0F}
            Me.m_ToolTipMsg = New ToolTip
            Me.teeView.Nodes.Add("JudgeResult File")
            Me.UI_PanelStatus("", "")
            Me.Size = New Size(Me.m_Config.WinWidth / 2, Me.m_Config.WinHeight)
            Me.SplitContainer_OKNGShow.Panel1Collapsed = False
            Me.SplitContainer_OKNGShow.Panel2Collapsed = True
            Me.SplitContainer_OKNGTitle.Panel1Collapsed = False
            Me.SplitContainer_OKNGTitle.Panel2Collapsed = True
            Me.TabControl_ShowDefect.TabPages.Remove(Me.TabPage_Luminance)

            If Me.m_ViewerLog Is Nothing Then
                Me.m_ViewerLog = New ClsLogRecorder
                Try
                    Me.m_ViewerLog.Open(Me.m_Config.LogPath, "LogViewer")
                    Me.m_ViewerLog.Close()
                Catch ex As Exception
                    Me.m_ViewerLog = Nothing
                    Throw New Exception("Open Judger Log fail!")
                End Try
            End If
        Catch ex As Exception
            Me.Close()
            End
        End Try
    End Sub
End Class

Public Enum eMuraType As Byte

    MURA
    BLACK_MURA
    WHITE_MURA
    BLACK_SPOT
    WHITE_SPOT
    AROUND_GAP_MURA_BLACK
    AROUND_GAP_MURA_WHITE
    H_BAND_MURA
    V_BAND_MURA
    UNKNOW_MURA

End Enum

Public Enum PanelStatus As Byte
    NULL = 0
    PASS = 1
    NG = 2
    REVIEW = 3
    LINE = 4
    FUNC = 5
    MURA = 6
    CHECK = 10
End Enum

Public Enum defectType As Byte
    NULL = 0
    FRAME_GLUE
    SMALL_BUBBLE
    MIDDLE_BUBBLE
    LARGE_BUBBLE
    S_GRADE_BUBBLE
    X_SHORT
    H_LINE
    V_LINE
    H_OPEN
    V_OPEN
    POINT_COUNT
    GROUP_SMALL_BP
    BP
    BP_PAIR
    BP_ADJ
    BP_CLUSTER
    BP_NEAR
    BACK_LIGHT_DP
    DP
    DP_PAIR
    DP_ADJ
    DP_CLUSTER
    DP_NEAR
    BPDP_PAIR
    BPDP_ADJ
    BPDP_CLUSTER
    BPDP_NEAR
    MURA
    CP
    SBP
    H_BLOCK
    V_BLOCK
    OMIT_BP
    DF_IN_PATTERN
    LUMINANCE
End Enum