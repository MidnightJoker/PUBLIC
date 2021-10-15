Imports System.IO
Imports System.Xml.Serialization

Public Class ClsConfig
    Public PanelW As Integer
    Public PanelH As Integer
    Public PanelBMW As Integer
    Public PanelBMH As Integer
    Public WinWidth As Integer
    Public WinHeight As Integer
    Public EnablePanelBMArea As Boolean
    Public Rotate As Boolean

    Public colorCP As Integer
    Public colorSBP As Integer

    Public colorGSBP As Integer
    Public colorBP1 As Integer
    Public colorBP2 As Integer
    Public colorBP3 As Integer
    Public colorBPx As Integer
    Public colorBPn As Integer
    Public colorOmitBP As Integer

    Public colorBLDP As Integer
    Public colorDP1 As Integer
    Public colorDP2 As Integer
    Public colorDP3 As Integer
    Public colorDPx As Integer
    Public colorDPn As Integer

    Public colorBPDP2 As Integer
    Public colorBPDP3 As Integer
    Public colorBPDPx As Integer
    Public colorBPDPn As Integer

    Public colorXL As Integer
    Public colorVL As Integer
    Public colorHL As Integer
    Public colorVOL As Integer
    Public colorHOL As Integer

    Public colorBUBBLE As Integer
    Public colorFRAMEGLUE As Integer

    Public colorMURA As Integer
    Public colorMark As Integer

    Public InitialFolderPath As String
    Public MainViewName As String
    Public SubViewName As String
    Public bShowMuraDlg As Boolean
    Public bMuraCcdNo As Boolean
    Public bFuncCcdNo As Boolean
    Public bUse9Part As Boolean
    Public bUseDefectDynamicShowWin As Boolean
    Public DefectFrmW As Integer
    Public DefectFrmH As Integer
    Public LoadImagePattern As String
    Public LogPath As String

    Public USE_defectName As Boolean
    Public USE_defectWindow As Boolean

    Public Sub New()

        Me.PanelW = 5760
        Me.PanelH = 1080
        Me.PanelBMW = 6000
        Me.PanelBMH = 1200
        Me.Rotate = False
        Me.WinWidth = 1000
        Me.WinHeight = 700

        Me.colorCP = Color.Orange.ToArgb
        Me.colorSBP = Color.Fuchsia.ToArgb

        Me.colorGSBP = Color.Red.ToArgb
        Me.colorBP1 = Color.Red.ToArgb
        Me.colorBP2 = Color.Red.ToArgb
        Me.colorBP3 = Color.Red.ToArgb
        Me.colorBPx = Color.Red.ToArgb
        Me.colorBPn = Color.Red.ToArgb
        Me.colorOmitBP = Color.IndianRed.ToArgb


        Me.colorBLDP = Color.Lime.ToArgb
        Me.colorDP1 = Color.Lime.ToArgb
        Me.colorDP2 = Color.Lime.ToArgb
        Me.colorDP3 = Color.Lime.ToArgb
        Me.colorDPx = Color.Lime.ToArgb
        Me.colorDPn = Color.Lime.ToArgb

        Me.colorBPDP2 = Color.Yellow.ToArgb
        Me.colorBPDP3 = Color.Yellow.ToArgb
        Me.colorBPDPx = Color.Yellow.ToArgb
        Me.colorBPDPn = Color.Yellow.ToArgb

        Me.colorXL = Color.White.ToArgb
        Me.colorVL = Color.White.ToArgb
        Me.colorHL = Color.White.ToArgb
        Me.colorVOL = Color.White.ToArgb
        Me.colorHOL = Color.White.ToArgb

        Me.colorBUBBLE = Color.Blue.ToArgb
        Me.colorFRAMEGLUE = Color.Blue.ToArgb

        Me.colorMURA = Color.Cyan.ToArgb
        Me.colorMark = Color.Cyan.ToArgb

        Me.InitialFolderPath = "D:\AOI_Data\JudgeResult\"
        Me.MainViewName = "AOI View"
        Me.SubViewName = "View JudgeResult"
        Me.bShowMuraDlg = False
        Me.bMuraCcdNo = False
        Me.bFuncCcdNo = False
        Me.bUse9Part = False
        Me.bUseDefectDynamicShowWin = True
        Me.DefectFrmW = 300
        Me.DefectFrmH = 300
        Me.LoadImagePattern = ""
        Me.LogPath = "D:\AOI_Data\Log\Viewer"
        Me.USE_defectName = True
        Me.USE_defectWindow = False
    End Sub

    Public Shared Sub WriteXML(ByVal cls As ClsConfig, ByVal fileName As String)
        Dim serializer As XmlSerializer
        Dim writer As StreamWriter

        serializer = New XmlSerializer(GetType(ClsConfig))
        writer = New StreamWriter(fileName)
        serializer.Serialize(writer, cls)
        writer.Close()
    End Sub

    Public Shared Function ReadXML(ByVal fileName As String) As ClsConfig
        Dim serializer As XmlSerializer
        Dim fs As FileStream
        Dim cls As ClsConfig

        serializer = New XmlSerializer(GetType(ClsConfig))
        fs = New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)
        cls = CType(serializer.Deserialize(fs), ClsConfig)
        fs.Close()
        Return cls
    End Function

End Class
