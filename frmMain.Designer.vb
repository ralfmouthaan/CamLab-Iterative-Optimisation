<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblNoModes = New System.Windows.Forms.Label()
        Me.cmbNoModes = New System.Windows.Forms.ComboBox()
        Me.ssStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ssLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cmdIterativeOptimisation = New System.Windows.Forms.Button()
        Me.cmdCaptureSingleImage = New System.Windows.Forms.Button()
        Me.ssStatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblNoModes
        '
        Me.lblNoModes.AutoSize = True
        Me.lblNoModes.Location = New System.Drawing.Point(57, 35)
        Me.lblNoModes.Name = "lblNoModes"
        Me.lblNoModes.Size = New System.Drawing.Size(59, 13)
        Me.lblNoModes.TabIndex = 4
        Me.lblNoModes.Text = "No Modes:"
        '
        'cmbNoModes
        '
        Me.cmbNoModes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNoModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoModes.FormattingEnabled = True
        Me.cmbNoModes.Location = New System.Drawing.Point(122, 32)
        Me.cmbNoModes.Name = "cmbNoModes"
        Me.cmbNoModes.Size = New System.Drawing.Size(212, 21)
        Me.cmbNoModes.TabIndex = 5
        '
        'ssStatusStrip
        '
        Me.ssStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ssLabel})
        Me.ssStatusStrip.Location = New System.Drawing.Point(0, 197)
        Me.ssStatusStrip.Name = "ssStatusStrip"
        Me.ssStatusStrip.Size = New System.Drawing.Size(358, 22)
        Me.ssStatusStrip.TabIndex = 9
        Me.ssStatusStrip.Text = "StatusStrip1"
        '
        'ssLabel
        '
        Me.ssLabel.Name = "ssLabel"
        Me.ssLabel.Size = New System.Drawing.Size(45, 17)
        Me.ssLabel.Text = "ssLabel"
        '
        'cmdIterativeOptimisation
        '
        Me.cmdIterativeOptimisation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdIterativeOptimisation.Location = New System.Drawing.Point(29, 127)
        Me.cmdIterativeOptimisation.Name = "cmdIterativeOptimisation"
        Me.cmdIterativeOptimisation.Size = New System.Drawing.Size(305, 54)
        Me.cmdIterativeOptimisation.TabIndex = 10
        Me.cmdIterativeOptimisation.Text = "Iterative Optimisation Routine"
        Me.cmdIterativeOptimisation.UseVisualStyleBackColor = True
        '
        'cmdCaptureSingleImage
        '
        Me.cmdCaptureSingleImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCaptureSingleImage.Location = New System.Drawing.Point(29, 67)
        Me.cmdCaptureSingleImage.Name = "cmdCaptureSingleImage"
        Me.cmdCaptureSingleImage.Size = New System.Drawing.Size(305, 54)
        Me.cmdCaptureSingleImage.TabIndex = 11
        Me.cmdCaptureSingleImage.Text = "Capture Single Image"
        Me.cmdCaptureSingleImage.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 219)
        Me.Controls.Add(Me.cmdCaptureSingleImage)
        Me.Controls.Add(Me.cmdIterativeOptimisation)
        Me.Controls.Add(Me.ssStatusStrip)
        Me.Controls.Add(Me.cmbNoModes)
        Me.Controls.Add(Me.lblNoModes)
        Me.Name = "frmMain"
        Me.Text = "RPM Iterative Optimisation"
        Me.ssStatusStrip.ResumeLayout(False)
        Me.ssStatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNoModes As Label
    Friend WithEvents cmbNoModes As ComboBox
    Friend WithEvents ssStatusStrip As StatusStrip
    Friend WithEvents ssLabel As ToolStripStatusLabel
    Friend WithEvents cmdIterativeOptimisation As Button
    Friend WithEvents cmdCaptureSingleImage As Button
End Class
