<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmShowDefect
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.picDfImg = New System.Windows.Forms.PictureBox()
        Me.labDfImgMsg = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.picDfImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picDfImg
        '
        Me.picDfImg.BackColor = System.Drawing.SystemColors.Control
        Me.picDfImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picDfImg.Location = New System.Drawing.Point(0, 0)
        Me.picDfImg.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.picDfImg.Name = "picDfImg"
        Me.picDfImg.Size = New System.Drawing.Size(297, 186)
        Me.picDfImg.TabIndex = 0
        Me.picDfImg.TabStop = False
        '
        'labDfImgMsg
        '
        Me.labDfImgMsg.BackColor = System.Drawing.Color.Red
        Me.labDfImgMsg.Dock = System.Windows.Forms.DockStyle.Top
        Me.labDfImgMsg.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.labDfImgMsg.Location = New System.Drawing.Point(0, 0)
        Me.labDfImgMsg.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labDfImgMsg.Name = "labDfImgMsg"
        Me.labDfImgMsg.Size = New System.Drawing.Size(297, 58)
        Me.labDfImgMsg.TabIndex = 2
        Me.labDfImgMsg.Text = "檔案不存在或路徑錯誤!"
        Me.labDfImgMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.picDfImg)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 58)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 186)
        Me.Panel1.TabIndex = 3
        '
        'FrmShowDefect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(297, 244)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.labDfImgMsg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmShowDefect"
        Me.Text = "FrmShowDefectvb"
        CType(Me.picDfImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picDfImg As System.Windows.Forms.PictureBox
    Friend WithEvents labDfImgMsg As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
End Class
