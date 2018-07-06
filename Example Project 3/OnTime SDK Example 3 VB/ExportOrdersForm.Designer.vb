<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportOrdersForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportOrdersForm))
        Me.LoadingLabel = New System.Windows.Forms.Label()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'LoadingLabel
        '
        Me.LoadingLabel.AutoSize = True
        Me.LoadingLabel.Location = New System.Drawing.Point(9, 13)
        Me.LoadingLabel.Name = "LoadingLabel"
        Me.LoadingLabel.Size = New System.Drawing.Size(54, 13)
        Me.LoadingLabel.TabIndex = 0
        Me.LoadingLabel.Text = "Loading..."
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(550, 23)
        Me.ProgressBar.TabIndex = 1
        '
        'BackgroundWorker
        '
        Me.BackgroundWorker.WorkerReportsProgress = True
        Me.BackgroundWorker.WorkerSupportsCancellation = True
        '
        'frmProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 62)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.LoadingLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmProgressBar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export to CSV"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LoadingLabel As System.Windows.Forms.Label
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker As ComponentModel.BackgroundWorker
End Class
