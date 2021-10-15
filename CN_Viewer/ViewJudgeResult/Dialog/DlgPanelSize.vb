Imports System.Windows.Forms

Public Class DlgPanelSize
    Private m_ConfigFileName As String
    Private m_Config As ClsConfig

    Public Sub New(ByVal configFile As String)
        InitializeComponent()
        Me.m_ConfigFileName = configFile
        Me.LoadConfig()
        Me.nudData.Value = Me.m_Config.PanelW
        Me.nudGate.Value = Me.m_Config.PanelH
        Me.nudWindowWidth.Value = Me.m_Config.WinWidth
        Me.nudWindowHeight.Value = Me.m_Config.WinHeight
        Me.cbxRotate.Checked = Me.m_Config.Rotate
        Me.cbxShowMuraDlg.Checked = Me.m_Config.bShowMuraDlg
        Me.cbxMuraCcdNo.Checked = Me.m_Config.bMuraCcdNo
        Me.cbxFuncCcdNo.Checked = Me.m_Config.bFuncCcdNo
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
        Me.m_Config.PanelW = Me.nudData.Value
        Me.m_Config.PanelH = Me.nudGate.Value
        Me.m_Config.PanelBMW = Me.nudBMData.Value
        Me.m_Config.PanelBMH = Me.nudBMGate.Value
        Me.m_Config.WinWidth = Me.nudWindowWidth.Value
        Me.m_Config.WinHeight = Me.nudWindowHeight.Value
        Me.m_Config.Rotate = Me.cbxRotate.Checked
        Me.m_Config.bShowMuraDlg = Me.cbxShowMuraDlg.Checked
        Me.m_Config.bMuraCcdNo = Me.cbxMuraCcdNo.Checked
        Me.m_Config.bFuncCcdNo = Me.cbxFuncCcdNo.Checked
        Me.SaveConfig()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
