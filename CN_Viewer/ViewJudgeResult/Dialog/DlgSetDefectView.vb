Public Class DlgSetDefectView
    Private m_ConfigFileName As String
    Private m_Config As ClsConfig

    Public Sub New(ByVal configFile As String)
        InitializeComponent()
        Me.m_ConfigFileName = configFile
        Me.LoadConfig()
        Me.nudWindowWidth.Value = Me.m_Config.DefectFrmW
        Me.nudWindowHeight.Value = Me.m_Config.DefectFrmH
        Me.ckb_9Part.Checked = Me.m_Config.bUse9Part
        Me.ckb_UseDefectShowWin.Checked = Me.m_Config.bUseDefectDynamicShowWin
        Me.txt_LoadImagePattern.Text = Me.m_Config.LoadImagePattern
        Me.txt_LogPath.Text = Me.m_Config.LogPath
        Me.Rdb_defectName.Checked = Me.m_Config.USE_defectName
        Me.Rdb_defectWindow.Checked = Me.m_Config.USE_defectWindow
    End Sub

    Private Sub LoadConfig()

        If System.IO.File.Exists(Me.m_ConfigFileName) Then
            Me.m_Config = ClsConfig.ReadXML(Me.m_ConfigFileName)
        Else
            Me.m_Config = New ClsConfig
        End If
    End Sub

    Private Sub SaveConfig()
        Try
            ClsConfig.WriteXML(Me.m_Config, Me.m_ConfigFileName)
        Catch ex As Exception
            'don't care
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.m_Config.DefectFrmW = Me.nudWindowWidth.Value
        Me.m_Config.DefectFrmH = Me.nudWindowHeight.Value
        Me.m_Config.bUse9Part = Me.ckb_9Part.Checked
        Me.m_Config.bUseDefectDynamicShowWin = Me.ckb_UseDefectShowWin.Checked
        Me.m_Config.LoadImagePattern = Me.txt_LoadImagePattern.Text
        Me.m_Config.LogPath = Me.txt_LogPath.Text
        Me.m_Config.DefectFrmW = Me.nudWindowWidth.Value
        Me.m_Config.DefectFrmH = Me.nudWindowHeight.Value
        Me.m_Config.USE_defectName = Me.Rdb_defectName.Checked
        Me.m_Config.USE_defectWindow = Me.Rdb_defectWindow.Checked
        Me.SaveConfig()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txt_LoadImagePattern_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_LoadImagePattern.TextChanged
        Me.m_Config.LoadImagePattern = Me.txt_LoadImagePattern.Text
    End Sub

    Private Sub txt_LogPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_LogPath.TextChanged
        Me.m_Config.LogPath = Me.txt_LogPath.Text
    End Sub


End Class