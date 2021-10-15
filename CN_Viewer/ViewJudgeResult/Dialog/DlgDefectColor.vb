Imports System.Windows.Forms

Public Class DlgDefectColor
    Private m_ConfigFileName As String
    Private m_Config As ClsConfig

    Public Sub New(ByVal configFile As String)

        InitializeComponent()

        Me.m_ConfigFileName = configFile
        Me.LoadConfig()
        Me.ColorLoad()
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
        Me.ColorUpdate()
        Me.SaveConfig()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ColorLoad()
        Me.txtGSDP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBLDP)
        Me.txtDP1.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP1)
        Me.txtDP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP2)
        Me.txtDP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDP3)
        Me.txtDPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDPx)
        Me.txtDPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorDPn)

        Me.txtOmitBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorOmitBP)
        Me.txtGSBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorGSBP)
        Me.txtBP1.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP1)
        Me.txtBP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP2)
        Me.txtBP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBP3)
        Me.txtBPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPx)
        Me.txtBPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPn)

        Me.txtBPDP2.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP2)
        Me.txtBPDP3.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDP3)
        Me.txtBPDPx.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPx)
        Me.txtBPDPn.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBPDPn)

        Me.txtXL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorXL)
        Me.txtVL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorVL)
        Me.txtHL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorHL)
        Me.txtVOL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorVOL)
        Me.txtHOL.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorHOL)

        Me.txtFG.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorFRAMEGLUE)
        Me.txtBB.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorBUBBLE)

        Me.txtMURA.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorMURA)
        Me.txtCP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorCP)
        Me.txtSBP.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorSBP)

        Me.txtMark.BackColor = System.Drawing.Color.FromArgb(Me.m_Config.colorMark)
    End Sub

    Private Sub ColorUpdate()
        Me.m_Config.colorBLDP = Me.txtGSDP.BackColor.ToArgb
        Me.m_Config.colorDP1 = Me.txtDP1.BackColor.ToArgb
        Me.m_Config.colorDP2 = Me.txtDP2.BackColor.ToArgb
        Me.m_Config.colorDP3 = Me.txtDP3.BackColor.ToArgb
        Me.m_Config.colorDPx = Me.txtDPx.BackColor.ToArgb
        Me.m_Config.colorDPn = Me.txtDPn.BackColor.ToArgb

        Me.m_Config.colorOmitBP = Me.txtOmitBP.BackColor.ToArgb
        Me.m_Config.colorGSBP = Me.txtGSBP.BackColor.ToArgb
        Me.m_Config.colorBP1 = Me.txtBP1.BackColor.ToArgb
        Me.m_Config.colorBP2 = Me.txtBP2.BackColor.ToArgb
        Me.m_Config.colorBP3 = Me.txtBP3.BackColor.ToArgb
        Me.m_Config.colorBPx = Me.txtBPx.BackColor.ToArgb
        Me.m_Config.colorBPn = Me.txtBPn.BackColor.ToArgb

        Me.m_Config.colorBPDP2 = Me.txtBPDP2.BackColor.ToArgb
        Me.m_Config.colorBPDP3 = Me.txtBPDP3.BackColor.ToArgb
        Me.m_Config.colorBPDPx = Me.txtBPDPx.BackColor.ToArgb
        Me.m_Config.colorBPDPn = Me.txtBPDPn.BackColor.ToArgb

        Me.m_Config.colorXL = Me.txtXL.BackColor.ToArgb
        Me.m_Config.colorVL = Me.txtVL.BackColor.ToArgb
        Me.m_Config.colorHL = Me.txtHL.BackColor.ToArgb
        Me.m_Config.colorVOL = Me.txtVOL.BackColor.ToArgb
        Me.m_Config.colorHOL = Me.txtHOL.BackColor.ToArgb

        Me.m_Config.colorMURA = Me.txtMURA.BackColor.ToArgb
        Me.m_Config.colorCP = Me.txtCP.BackColor.ToArgb
        Me.m_Config.colorSBP = Me.txtSBP.BackColor.ToArgb

        Me.m_Config.colorFRAMEGLUE = Me.txtFG.BackColor.ToArgb
        Me.m_Config.colorBUBBLE = Me.txtBB.BackColor.ToArgb

        Me.m_Config.colorMark = Me.txtMark.BackColor.ToArgb
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        Me.txtDP1.BackColor = Color.Lime
        Me.txtDP2.BackColor = Color.Lime
        Me.txtDP3.BackColor = Color.Lime
        Me.txtDPx.BackColor = Color.Lime
        Me.txtDPn.BackColor = Color.Lime

        Me.txtOmitBP.BackColor = Color.IndianRed
        Me.txtBP1.BackColor = Color.Red
        Me.txtBP2.BackColor = Color.Red
        Me.txtBP3.BackColor = Color.Red
        Me.txtBPx.BackColor = Color.Red
        Me.txtBPn.BackColor = Color.Red

        Me.txtBPDP2.BackColor = Color.Yellow
        Me.txtBPDP3.BackColor = Color.Yellow
        Me.txtBPDPx.BackColor = Color.Yellow
        Me.txtBPDPn.BackColor = Color.Yellow

        Me.txtXL.BackColor = Color.White
        Me.txtVL.BackColor = Color.White
        Me.txtHL.BackColor = Color.White
        Me.txtVOL.BackColor = Color.White
        Me.txtHOL.BackColor = Color.White

        Me.txtFG.BackColor = Color.Blue
        Me.txtBB.BackColor = Color.Blue

        Me.txtMURA.BackColor = Color.Cyan
        Me.txtCP.BackColor = Color.Orange
        Me.txtSBP.BackColor = Color.Fuchsia

        Me.txtMark.BackColor = Color.Cyan
    End Sub

    Private Sub txtDP1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDP1.Click, txtGSDP.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtDP1.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtDP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDP2.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtDP2.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtDP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDP3.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtDP3.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtDPx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDPx.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtDPx.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtDPn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDPn.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtDPn.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBP1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBP1.Click, txtGSBP.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBP1.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBP2.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBP2.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBP3.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBP3.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPx.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPx.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPn.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPn.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPDP2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPDP2.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPDP2.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPDP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPDP3.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPDP3.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPDPx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPDPx.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPDPx.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBPDPn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPDPn.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBPDPn.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtXL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtXL.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtXL.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtVL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVL.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtVL.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtHL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHL.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtHL.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtVOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVOL.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtVOL.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtHOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHOL.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtHOL.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtMURA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMURA.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtMURA.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCP.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtCP.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtSBP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSBP.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtSBP.BackColor = Me.DlgColor.Color
        End If
    End Sub
    
    Private Sub txtMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMark.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtMark.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtOmitBP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOmitBP.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtOmitBP.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtFG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFG.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtFG.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub txtBB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBB.Click
        If Me.DlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtBB.BackColor = Me.DlgColor.Color
        End If
    End Sub

    Private Sub DlgDefectColor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
