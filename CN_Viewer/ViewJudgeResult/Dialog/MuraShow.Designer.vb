<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MuraShow
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
        Me.picMuraShow = New System.Windows.Forms.PictureBox()
        Me.labPanelMinX = New System.Windows.Forms.Label()
        Me.labPanelMaxX = New System.Windows.Forms.Label()
        Me.labPanelMinY = New System.Windows.Forms.Label()
        Me.labPanelMaxY = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.dgvMuraShow = New System.Windows.Forms.DataGridView()
        Me.Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DfType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Data = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pattern = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCanel = New System.Windows.Forms.Button()
        CType(Me.picMuraShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMuraShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picMuraShow
        '
        Me.picMuraShow.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.picMuraShow.Location = New System.Drawing.Point(37, 27)
        Me.picMuraShow.Name = "picMuraShow"
        Me.picMuraShow.Size = New System.Drawing.Size(750, 490)
        Me.picMuraShow.TabIndex = 0
        Me.picMuraShow.TabStop = False
        '
        'labPanelMinX
        '
        Me.labPanelMinX.AutoSize = True
        Me.labPanelMinX.Location = New System.Drawing.Point(35, 9)
        Me.labPanelMinX.Name = "labPanelMinX"
        Me.labPanelMinX.Size = New System.Drawing.Size(11, 12)
        Me.labPanelMinX.TabIndex = 2
        Me.labPanelMinX.Text = "0"
        '
        'labPanelMaxX
        '
        Me.labPanelMaxX.AutoSize = True
        Me.labPanelMaxX.Location = New System.Drawing.Point(758, 9)
        Me.labPanelMaxX.Name = "labPanelMaxX"
        Me.labPanelMaxX.Size = New System.Drawing.Size(29, 12)
        Me.labPanelMaxX.TabIndex = 2
        Me.labPanelMaxX.Text = "5760"
        '
        'labPanelMinY
        '
        Me.labPanelMinY.AutoSize = True
        Me.labPanelMinY.Location = New System.Drawing.Point(2, 27)
        Me.labPanelMinY.Name = "labPanelMinY"
        Me.labPanelMinY.Size = New System.Drawing.Size(11, 12)
        Me.labPanelMinY.TabIndex = 2
        Me.labPanelMinY.Text = "0"
        '
        'labPanelMaxY
        '
        Me.labPanelMaxY.AutoSize = True
        Me.labPanelMaxY.Location = New System.Drawing.Point(2, 506)
        Me.labPanelMaxY.Name = "labPanelMaxY"
        Me.labPanelMaxY.Size = New System.Drawing.Size(29, 12)
        Me.labPanelMaxY.TabIndex = 2
        Me.labPanelMaxY.Text = "1080"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1114, 524)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 43)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dgvMuraShow
        '
        Me.dgvMuraShow.AllowUserToAddRows = False
        Me.dgvMuraShow.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvMuraShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMuraShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Check, Me.DfType, Me.Data, Me.Gate, Me.MinX, Me.MinY, Me.MaxX, Me.MaxY, Me.Pattern})
        Me.dgvMuraShow.Location = New System.Drawing.Point(797, 27)
        Me.dgvMuraShow.Name = "dgvMuraShow"
        Me.dgvMuraShow.RowHeadersVisible = False
        Me.dgvMuraShow.RowTemplate.Height = 24
        Me.dgvMuraShow.Size = New System.Drawing.Size(483, 491)
        Me.dgvMuraShow.TabIndex = 4
        '
        'Check
        '
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        Me.Check.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Check.TrueValue = "True"
        Me.Check.Width = 40
        '
        'DfType
        '
        Me.DfType.HeaderText = "Defect Type"
        Me.DfType.Name = "DfType"
        Me.DfType.ReadOnly = True
        '
        'Data
        '
        Me.Data.HeaderText = "Data"
        Me.Data.Name = "Data"
        Me.Data.ReadOnly = True
        Me.Data.Width = 40
        '
        'Gate
        '
        Me.Gate.HeaderText = "Gate"
        Me.Gate.Name = "Gate"
        Me.Gate.ReadOnly = True
        Me.Gate.Width = 40
        '
        'MinX
        '
        Me.MinX.HeaderText = "MinX"
        Me.MinX.Name = "MinX"
        Me.MinX.ReadOnly = True
        Me.MinX.Width = 40
        '
        'MinY
        '
        Me.MinY.HeaderText = "MinY"
        Me.MinY.Name = "MinY"
        Me.MinY.ReadOnly = True
        Me.MinY.Width = 40
        '
        'MaxX
        '
        Me.MaxX.HeaderText = "MaxX"
        Me.MaxX.Name = "MaxX"
        Me.MaxX.ReadOnly = True
        Me.MaxX.Width = 40
        '
        'MaxY
        '
        Me.MaxY.HeaderText = "MaxY"
        Me.MaxY.Name = "MaxY"
        Me.MaxY.ReadOnly = True
        Me.MaxY.Width = 40
        '
        'Pattern
        '
        Me.Pattern.HeaderText = "Pattern"
        Me.Pattern.Name = "Pattern"
        Me.Pattern.ReadOnly = True
        '
        'btnCanel
        '
        Me.btnCanel.Location = New System.Drawing.Point(1200, 524)
        Me.btnCanel.Name = "btnCanel"
        Me.btnCanel.Size = New System.Drawing.Size(80, 43)
        Me.btnCanel.TabIndex = 3
        Me.btnCanel.Text = "Cancel"
        Me.btnCanel.UseVisualStyleBackColor = True
        '
        'MuraShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 572)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgvMuraShow)
        Me.Controls.Add(Me.btnCanel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.labPanelMaxX)
        Me.Controls.Add(Me.labPanelMaxY)
        Me.Controls.Add(Me.labPanelMinY)
        Me.Controls.Add(Me.labPanelMinX)
        Me.Controls.Add(Me.picMuraShow)
        Me.Name = "MuraShow"
        Me.Text = "MuraShow"
        CType(Me.picMuraShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMuraShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picMuraShow As System.Windows.Forms.PictureBox
    Friend WithEvents labPanelMinX As System.Windows.Forms.Label
    Friend WithEvents labPanelMaxX As System.Windows.Forms.Label
    Friend WithEvents labPanelMinY As System.Windows.Forms.Label
    Friend WithEvents labPanelMaxY As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents dgvMuraShow As System.Windows.Forms.DataGridView
    Friend WithEvents Check As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DfType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pattern As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCanel As System.Windows.Forms.Button
End Class
