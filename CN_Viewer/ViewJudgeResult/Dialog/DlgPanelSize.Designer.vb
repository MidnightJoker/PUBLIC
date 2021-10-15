<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgPanelSize
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DlgPanelSize))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpAA = New System.Windows.Forms.GroupBox()
        Me.nudGate = New System.Windows.Forms.NumericUpDown()
        Me.nudData = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpBM = New System.Windows.Forms.GroupBox()
        Me.nudBMGate = New System.Windows.Forms.NumericUpDown()
        Me.nudBMData = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.gbWindowSize = New System.Windows.Forms.GroupBox()
        Me.cbxFuncCcdNo = New System.Windows.Forms.CheckBox()
        Me.cbxMuraCcdNo = New System.Windows.Forms.CheckBox()
        Me.cbxShowMuraDlg = New System.Windows.Forms.CheckBox()
        Me.cbxRotate = New System.Windows.Forms.CheckBox()
        Me.nudWindowHeight = New System.Windows.Forms.NumericUpDown()
        Me.nudWindowWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAA.SuspendLayout()
        CType(Me.nudGate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBM.SuspendLayout()
        CType(Me.nudBMGate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudBMData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbWindowSize.SuspendLayout()
        CType(Me.nudWindowHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWindowWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnSave, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(151, 265)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 26)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(76, 2)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 22)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(9, 10)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(134, 114)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'grpAA
        '
        Me.grpAA.Controls.Add(Me.nudGate)
        Me.grpAA.Controls.Add(Me.nudData)
        Me.grpAA.Controls.Add(Me.Label2)
        Me.grpAA.Controls.Add(Me.Label1)
        Me.grpAA.Location = New System.Drawing.Point(155, 12)
        Me.grpAA.Name = "grpAA"
        Me.grpAA.Size = New System.Drawing.Size(128, 78)
        Me.grpAA.TabIndex = 8
        Me.grpAA.TabStop = False
        Me.grpAA.Text = "AA (Sub-pixel)"
        '
        'nudGate
        '
        Me.nudGate.Location = New System.Drawing.Point(46, 49)
        Me.nudGate.Margin = New System.Windows.Forms.Padding(2)
        Me.nudGate.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nudGate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudGate.Name = "nudGate"
        Me.nudGate.Size = New System.Drawing.Size(50, 22)
        Me.nudGate.TabIndex = 12
        Me.nudGate.Value = New Decimal(New Integer() {1080, 0, 0, 0})
        '
        'nudData
        '
        Me.nudData.Location = New System.Drawing.Point(47, 18)
        Me.nudData.Margin = New System.Windows.Forms.Padding(2)
        Me.nudData.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nudData.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudData.Name = "nudData"
        Me.nudData.Size = New System.Drawing.Size(50, 22)
        Me.nudData.TabIndex = 11
        Me.nudData.Value = New Decimal(New Integer() {5760, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 52)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Gate ="
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Data ="
        '
        'grpBM
        '
        Me.grpBM.Controls.Add(Me.nudBMGate)
        Me.grpBM.Controls.Add(Me.nudBMData)
        Me.grpBM.Controls.Add(Me.Label7)
        Me.grpBM.Controls.Add(Me.Label8)
        Me.grpBM.Location = New System.Drawing.Point(155, 92)
        Me.grpBM.Name = "grpBM"
        Me.grpBM.Size = New System.Drawing.Size(128, 78)
        Me.grpBM.TabIndex = 14
        Me.grpBM.TabStop = False
        Me.grpBM.Text = "BM (Sub-pixel)"
        '
        'nudBMGate
        '
        Me.nudBMGate.Location = New System.Drawing.Point(46, 48)
        Me.nudBMGate.Margin = New System.Windows.Forms.Padding(2)
        Me.nudBMGate.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nudBMGate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudBMGate.Name = "nudBMGate"
        Me.nudBMGate.Size = New System.Drawing.Size(50, 22)
        Me.nudBMGate.TabIndex = 12
        Me.nudBMGate.Value = New Decimal(New Integer() {1200, 0, 0, 0})
        '
        'nudBMData
        '
        Me.nudBMData.Location = New System.Drawing.Point(46, 20)
        Me.nudBMData.Margin = New System.Windows.Forms.Padding(2)
        Me.nudBMData.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nudBMData.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudBMData.Name = "nudBMData"
        Me.nudBMData.Size = New System.Drawing.Size(50, 22)
        Me.nudBMData.TabIndex = 11
        Me.nudBMData.Value = New Decimal(New Integer() {6000, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 51)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 12)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Gate ="
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 23)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 12)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Data ="
        '
        'gbWindowSize
        '
        Me.gbWindowSize.Controls.Add(Me.cbxFuncCcdNo)
        Me.gbWindowSize.Controls.Add(Me.cbxMuraCcdNo)
        Me.gbWindowSize.Controls.Add(Me.cbxShowMuraDlg)
        Me.gbWindowSize.Controls.Add(Me.cbxRotate)
        Me.gbWindowSize.Controls.Add(Me.nudWindowHeight)
        Me.gbWindowSize.Controls.Add(Me.nudWindowWidth)
        Me.gbWindowSize.Controls.Add(Me.Label9)
        Me.gbWindowSize.Location = New System.Drawing.Point(9, 129)
        Me.gbWindowSize.Name = "gbWindowSize"
        Me.gbWindowSize.Size = New System.Drawing.Size(134, 164)
        Me.gbWindowSize.TabIndex = 15
        Me.gbWindowSize.TabStop = False
        Me.gbWindowSize.Text = "BootConfig"
        '
        'cbxFuncCcdNo
        '
        Me.cbxFuncCcdNo.AutoSize = True
        Me.cbxFuncCcdNo.Location = New System.Drawing.Point(10, 140)
        Me.cbxFuncCcdNo.Name = "cbxFuncCcdNo"
        Me.cbxFuncCcdNo.Size = New System.Drawing.Size(93, 16)
        Me.cbxFuncCcdNo.TabIndex = 6
        Me.cbxFuncCcdNo.Text = "Func CCD NO"
        Me.cbxFuncCcdNo.UseVisualStyleBackColor = True
        '
        'cbxMuraCcdNo
        '
        Me.cbxMuraCcdNo.AutoSize = True
        Me.cbxMuraCcdNo.Location = New System.Drawing.Point(10, 118)
        Me.cbxMuraCcdNo.Name = "cbxMuraCcdNo"
        Me.cbxMuraCcdNo.Size = New System.Drawing.Size(95, 16)
        Me.cbxMuraCcdNo.TabIndex = 5
        Me.cbxMuraCcdNo.Text = "Mura CCD NO"
        Me.cbxMuraCcdNo.UseVisualStyleBackColor = True
        '
        'cbxShowMuraDlg
        '
        Me.cbxShowMuraDlg.AutoSize = True
        Me.cbxShowMuraDlg.Location = New System.Drawing.Point(10, 96)
        Me.cbxShowMuraDlg.Name = "cbxShowMuraDlg"
        Me.cbxShowMuraDlg.Size = New System.Drawing.Size(98, 16)
        Me.cbxShowMuraDlg.TabIndex = 4
        Me.cbxShowMuraDlg.Text = "Show Mura Dlg"
        Me.cbxShowMuraDlg.UseVisualStyleBackColor = True
        '
        'cbxRotate
        '
        Me.cbxRotate.AutoSize = True
        Me.cbxRotate.Location = New System.Drawing.Point(10, 74)
        Me.cbxRotate.Name = "cbxRotate"
        Me.cbxRotate.Size = New System.Drawing.Size(54, 16)
        Me.cbxRotate.TabIndex = 3
        Me.cbxRotate.Text = "Rotate"
        Me.cbxRotate.UseVisualStyleBackColor = True
        '
        'nudWindowHeight
        '
        Me.nudWindowHeight.Location = New System.Drawing.Point(75, 45)
        Me.nudWindowHeight.Maximum = New Decimal(New Integer() {2160, 0, 0, 0})
        Me.nudWindowHeight.Minimum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudWindowHeight.Name = "nudWindowHeight"
        Me.nudWindowHeight.Size = New System.Drawing.Size(50, 22)
        Me.nudWindowHeight.TabIndex = 2
        Me.nudWindowHeight.Value = New Decimal(New Integer() {770, 0, 0, 0})
        '
        'nudWindowWidth
        '
        Me.nudWindowWidth.Location = New System.Drawing.Point(75, 18)
        Me.nudWindowWidth.Maximum = New Decimal(New Integer() {3840, 0, 0, 0})
        Me.nudWindowWidth.Minimum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudWindowWidth.Name = "nudWindowWidth"
        Me.nudWindowWidth.Size = New System.Drawing.Size(50, 22)
        Me.nudWindowWidth.TabIndex = 1
        Me.nudWindowWidth.Value = New Decimal(New Integer() {1030, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 36)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "WinWidth :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "WinHeight :"
        '
        'DlgPanelSize
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(303, 298)
        Me.Controls.Add(Me.gbWindowSize)
        Me.Controls.Add(Me.grpBM)
        Me.Controls.Add(Me.grpAA)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DlgPanelSize"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Panel Size"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAA.ResumeLayout(False)
        Me.grpAA.PerformLayout()
        CType(Me.nudGate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBM.ResumeLayout(False)
        Me.grpBM.PerformLayout()
        CType(Me.nudBMGate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudBMData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbWindowSize.ResumeLayout(False)
        Me.gbWindowSize.PerformLayout()
        CType(Me.nudWindowHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWindowWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grpAA As System.Windows.Forms.GroupBox
    Friend WithEvents nudGate As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudData As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpBM As System.Windows.Forms.GroupBox
    Friend WithEvents nudBMGate As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudBMData As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents gbWindowSize As GroupBox
    Friend WithEvents nudWindowHeight As NumericUpDown
    Friend WithEvents nudWindowWidth As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents cbxFuncCcdNo As CheckBox
    Friend WithEvents cbxMuraCcdNo As CheckBox
    Friend WithEvents cbxShowMuraDlg As CheckBox
    Friend WithEvents cbxRotate As CheckBox
End Class
