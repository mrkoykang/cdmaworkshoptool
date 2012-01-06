<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cwTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cwTool))
        Me.RemovePlanTextButton = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.OpenFD = New System.Windows.Forms.OpenFileDialog()
        Me.searchForSpcButton = New System.Windows.Forms.Button()
        Me.ResultsListBox = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ConvertMeidHexToDec = New System.Windows.Forms.Button()
        Me.MeidConvertInBox = New System.Windows.Forms.TextBox()
        Me.SaveFD = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'RemovePlanTextButton
        '
        Me.RemovePlanTextButton.ForeColor = System.Drawing.Color.DarkGreen
        Me.RemovePlanTextButton.Location = New System.Drawing.Point(12, 71)
        Me.RemovePlanTextButton.Name = "RemovePlanTextButton"
        Me.RemovePlanTextButton.Size = New System.Drawing.Size(275, 23)
        Me.RemovePlanTextButton.TabIndex = 2
        Me.RemovePlanTextButton.Text = "Remove Text From NV Item Read (Ascii Hex remains)"
        Me.RemovePlanTextButton.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(212, 287)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(74, 20)
        Me.TextBox2.TabIndex = 5
        Me.TextBox2.Text = "¿kernelpanic?"
        '
        'OpenFD
        '
        Me.OpenFD.FileName = "OpenFileDialog1"
        '
        'searchForSpcButton
        '
        Me.searchForSpcButton.ForeColor = System.Drawing.Color.DarkGreen
        Me.searchForSpcButton.Location = New System.Drawing.Point(12, 103)
        Me.searchForSpcButton.Name = "searchForSpcButton"
        Me.searchForSpcButton.Size = New System.Drawing.Size(275, 23)
        Me.searchForSpcButton.TabIndex = 3
        Me.searchForSpcButton.Text = "Search for SPC "
        Me.searchForSpcButton.UseVisualStyleBackColor = True
        '
        'ResultsListBox
        '
        Me.ResultsListBox.FormattingEnabled = True
        Me.ResultsListBox.Location = New System.Drawing.Point(12, 135)
        Me.ResultsListBox.Name = "ResultsListBox"
        Me.ResultsListBox.Size = New System.Drawing.Size(275, 147)
        Me.ResultsListBox.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label1.Location = New System.Drawing.Point(11, 290)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "ChromableedStudios.com"
        '
        'ConvertMeidHexToDec
        '
        Me.ConvertMeidHexToDec.BackColor = System.Drawing.Color.White
        Me.ConvertMeidHexToDec.ForeColor = System.Drawing.Color.DarkGreen
        Me.ConvertMeidHexToDec.Location = New System.Drawing.Point(12, 10)
        Me.ConvertMeidHexToDec.Name = "ConvertMeidHexToDec"
        Me.ConvertMeidHexToDec.Size = New System.Drawing.Size(275, 23)
        Me.ConvertMeidHexToDec.TabIndex = 0
        Me.ConvertMeidHexToDec.Text = "Convert MEID/ESN"
        Me.ConvertMeidHexToDec.UseVisualStyleBackColor = False
        '
        'MeidConvertInBox
        '
        Me.MeidConvertInBox.Location = New System.Drawing.Point(12, 42)
        Me.MeidConvertInBox.Name = "MeidConvertInBox"
        Me.MeidConvertInBox.Size = New System.Drawing.Size(275, 20)
        Me.MeidConvertInBox.TabIndex = 1
        '
        'cwTool
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(298, 319)
        Me.Controls.Add(Me.MeidConvertInBox)
        Me.Controls.Add(Me.ConvertMeidHexToDec)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ResultsListBox)
        Me.Controls.Add(Me.searchForSpcButton)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.RemovePlanTextButton)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "cwTool"
        Me.Text = "cT.5e"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RemovePlanTextButton As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents OpenFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents searchForSpcButton As System.Windows.Forms.Button
    Friend WithEvents ResultsListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ConvertMeidHexToDec As System.Windows.Forms.Button
    Friend WithEvents MeidConvertInBox As System.Windows.Forms.TextBox
    Friend WithEvents SaveFD As System.Windows.Forms.SaveFileDialog

End Class
