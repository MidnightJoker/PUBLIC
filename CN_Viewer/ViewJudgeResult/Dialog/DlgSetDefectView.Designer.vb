<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgSetDefectView
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ckb_9Part = New System.Windows.Forms.CheckBox()
        Me.gbWindowSize = New System.Windows.Forms.GroupBox()
        Me.nudWindowHeight = New System.Windows.Forms.NumericUpDown()
        Me.nudWindowWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_LoadImagePattern = New System.Windows.Forms.TextBox()
        Me.ckb_UseDefectShowWin = New System.Windows.Forms.CheckBox()
        Me.txt_LogPath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Rdb_defectName = New System.Windows.Forms.RadioButton()
        Me.Rdb_defectWindow = New System.Windows.Forms.RadioButton()
        Me.gbWindowSize.SuspendLayout()
        CType(Me.nudWindowHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWindowWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ckb_9Part
        '
        Me.ckb_9Part.AutoSize = True
        Me.ckb_9Part.Location = New System.Drawing.Point(12, 12)
        Me.ckb_9Part.Name = "ckb_9Part"
        Me.ckb_9Part.Size = New System.Drawing.Size(84, 16)
        Me.ckb_9Part.TabIndex = 81
        Me.ckb_9Part.Text = "九宮格顯示"
        Me.ckb_9Part.UseVisualStyleBackColor = True
        '
        'gbWindowSize
        '
        Me.gbWindowSize.Controls.Add(Me.nudWindowHeight)
        Me.gbWindowSize.Controls.Add(Me.nudWindowWidth)
        Me.gbWindowSize.Controls.Add(Me.Label9)
        Me.gbWindowSize.Location = New System.Drawing.Point(5, 87)
        Me.gbWindowSize.Name = "gbWindowSize"
        Me.gbWindowSize.Size = New System.Drawing.Size(134, 74)
        Me.gbWindowSize.TabIndex = 82
        Me.gbWindowSize.TabStop = False
        Me.gbWindowSize.Text = "DefectWindowSize"
        '
        'nudWindowHeight
        '
        Me.nudWindowHeight.Location = New System.Drawing.Point(75, 45)
        Me.nudWindowHeight.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudWindowHeight.Name = "nudWindowHeight"
        Me.nudWindowHeight.Size = New System.Drawing.Size(50, 22)
        Me.nudWindowHeight.TabIndex = 2
        Me.nudWindowHeight.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'nudWindowWidth
        '
        Me.nudWindowWidth.Location = New System.Drawing.Point(75, 18)
        Me.nudWindowWidth.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudWindowWidth.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudWindowWidth.Name = "nudWindowWidth"
        Me.nudWindowWidth.Size = New System.Drawing.Size(50, 22)
        Me.nudWindowWidth.TabIndex = 1
        Me.nudWindowWidth.Value = New Decimal(New Integer() {300, 0, 0, 0})
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnSave, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(154, 209)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 26)
        Me.TableLayoutPanel1.TabIndex = 83
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 12)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "Load Image Pattern :"
        '
        'txt_LoadImagePattern
        '
        Me.txt_LoadImagePattern.Location = New System.Drawing.Point(120, 32)
        Me.txt_LoadImagePattern.Name = "txt_LoadImagePattern"
        Me.txt_LoadImagePattern.Size = New System.Drawing.Size(100, 22)
        Me.txt_LoadImagePattern.TabIndex = 85
        '
        'ckb_UseDefectShowWin
        '
        Me.ckb_UseDefectShowWin.AutoSize = True
        Me.ckb_UseDefectShowWin.Location = New System.Drawing.Point(102, 12)
        Me.ckb_UseDefectShowWin.Name = "ckb_UseDefectShowWin"
        Me.ckb_UseDefectShowWin.Size = New System.Drawing.Size(102, 16)
        Me.ckb_UseDefectShowWin.TabIndex = 86
        Me.ckb_UseDefectShowWin.Text = "Defect動態顯示"
        Me.ckb_UseDefectShowWin.UseVisualStyleBackColor = True
        '
        'txt_LogPath
        '
        Me.txt_LogPath.Location = New System.Drawing.Point(71, 57)
        Me.txt_LogPath.Name = "txt_LogPath"
        Me.txt_LogPath.Size = New System.Drawing.Size(229, 22)
        Me.txt_LogPath.TabIndex = 88
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 87
        Me.Label2.Text = "Log Path :"
        '
        'Rdb_defectName
        '
        Me.Rdb_defectName.AutoSize = True
        Me.Rdb_defectName.Checked = True
        Me.Rdb_defectName.Location = New System.Drawing.Point(166, 87)
        Me.Rdb_defectName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Rdb_defectName.Name = "Rdb_defectName"
        Me.Rdb_defectName.Size = New System.Drawing.Size(77, 16)
        Me.Rdb_defectName.TabIndex = 89
        Me.Rdb_defectName.TabStop = True
        Me.Rdb_defectName.Text = "Defect名稱"
        Me.Rdb_defectName.UseVisualStyleBackColor = True
        '
        'Rdb_defectWindow
        '
        Me.Rdb_defectWindow.AutoSize = True
        Me.Rdb_defectWindow.Location = New System.Drawing.Point(166, 111)
        Me.Rdb_defectWindow.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Rdb_defectWindow.Name = "Rdb_defectWindow"
        Me.Rdb_defectWindow.Size = New System.Drawing.Size(71, 16)
        Me.Rdb_defectWindow.TabIndex = 89
        Me.Rdb_defectWindow.Text = "小圖視窗"
        Me.Rdb_defectWindow.UseVisualStyleBackColor = True
        '
        'DlgSetDefectView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 240)
        Me.Controls.Add(Me.Rdb_defectWindow)
        Me.Controls.Add(Me.Rdb_defectName)
        Me.Controls.Add(Me.txt_LogPath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ckb_UseDefectShowWin)
        Me.Controls.Add(Me.txt_LoadImagePattern)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.gbWindowSize)
        Me.Controls.Add(Me.ckb_9Part)
        Me.Name = "DlgSetDefectView"
        Me.Text = "DlgSetDefectView"
        Me.gbWindowSize.ResumeLayout(False)
        Me.gbWindowSize.PerformLayout()
        CType(Me.nudWindowHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWindowWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ckb_9Part As System.Windows.Forms.CheckBox
    Friend WithEvents gbWindowSize As System.Windows.Forms.GroupBox
    Friend WithEvents nudWindowHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudWindowWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_LoadImagePattern As System.Windows.Forms.TextBox
    Friend WithEvents ckb_UseDefectShowWin As System.Windows.Forms.CheckBox
    Friend WithEvents txt_LogPath As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Rdb_defectName As RadioButton
    Friend WithEvents Rdb_defectWindow As RadioButton
End Class
