<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tstxtControlStatus = New System.Windows.Forms.TextBox()
        Me.btnCallSubView = New System.Windows.Forms.Button()
        Me.tmrViewStatus = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'tstxtControlStatus
        '
        Me.tstxtControlStatus.BackColor = System.Drawing.Color.Yellow
        Me.tstxtControlStatus.Enabled = False
        Me.tstxtControlStatus.Location = New System.Drawing.Point(74, 1)
        Me.tstxtControlStatus.Name = "tstxtControlStatus"
        Me.tstxtControlStatus.Size = New System.Drawing.Size(120, 22)
        Me.tstxtControlStatus.TabIndex = 5
        Me.tstxtControlStatus.Text = "Controller DisConnect"
        '
        'btnCallSubView
        '
        Me.btnCallSubView.Location = New System.Drawing.Point(0, 0)
        Me.btnCallSubView.Name = "btnCallSubView"
        Me.btnCallSubView.Size = New System.Drawing.Size(75, 25)
        Me.btnCallSubView.TabIndex = 4
        Me.btnCallSubView.Text = "CallSubView"
        Me.btnCallSubView.UseVisualStyleBackColor = True
        '
        'tmrViewStatus
        '
        Me.tmrViewStatus.Enabled = True
        Me.tmrViewStatus.Interval = 1
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(414, 582)
        Me.Controls.Add(Me.tstxtControlStatus)
        Me.Controls.Add(Me.btnCallSubView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "AOI View"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tstxtControlStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnCallSubView As System.Windows.Forms.Button
    Friend WithEvents tmrViewStatus As System.Windows.Forms.Timer
End Class
