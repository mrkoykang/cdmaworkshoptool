<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NvEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NvEditor))
        Me.LoadNvEditorBtn = New System.Windows.Forms.Button()
        Me.nvEditorItemListTxtbox = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FilterButton = New System.Windows.Forms.Button()
        Me.XMLtoNVILBtn = New System.Windows.Forms.Button()
        Me.ScriptingMode = New System.Windows.Forms.Button()
        Me.SaveScriptBtn = New System.Windows.Forms.Button()
        Me.DeleteRowsBtn = New System.Windows.Forms.Button()
        Me.NvWriteModeBtn = New System.Windows.Forms.Button()
        Me.NvWriteBtn = New System.Windows.Forms.Button()
        Me.FilterCombo = New System.Windows.Forms.ComboBox()
        Me.CheckAll = New System.Windows.Forms.Button()
        Me.UpdateNumbersBtn = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LoadNvEditorBtn
        '
        Me.LoadNvEditorBtn.Location = New System.Drawing.Point(12, 12)
        Me.LoadNvEditorBtn.Name = "LoadNvEditorBtn"
        Me.LoadNvEditorBtn.Size = New System.Drawing.Size(219, 23)
        Me.LoadNvEditorBtn.TabIndex = 0
        Me.LoadNvEditorBtn.Text = "Read NV Item # < 1 2 5-10 40-50 ... > : "
        Me.LoadNvEditorBtn.UseVisualStyleBackColor = True
        '
        'nvEditorItemListTxtbox
        '
        Me.nvEditorItemListTxtbox.Location = New System.Drawing.Point(249, 15)
        Me.nvEditorItemListTxtbox.Name = "nvEditorItemListTxtbox"
        Me.nvEditorItemListTxtbox.Size = New System.Drawing.Size(505, 20)
        Me.nvEditorItemListTxtbox.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 90)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(973, 378)
        Me.DataGridView1.TabIndex = 2
        '
        'FilterButton
        '
        Me.FilterButton.Location = New System.Drawing.Point(14, 52)
        Me.FilterButton.Name = "FilterButton"
        Me.FilterButton.Size = New System.Drawing.Size(100, 23)
        Me.FilterButton.TabIndex = 3
        Me.FilterButton.Text = "Clear Bad Items"
        Me.FilterButton.UseVisualStyleBackColor = True
        '
        'XMLtoNVILBtn
        '
        Me.XMLtoNVILBtn.Location = New System.Drawing.Point(768, 13)
        Me.XMLtoNVILBtn.Name = "XMLtoNVILBtn"
        Me.XMLtoNVILBtn.Size = New System.Drawing.Size(100, 23)
        Me.XMLtoNVILBtn.TabIndex = 4
        Me.XMLtoNVILBtn.Text = "CF XML to NVIL"
        Me.XMLtoNVILBtn.UseVisualStyleBackColor = True
        Me.XMLtoNVILBtn.Visible = False
        '
        'ScriptingMode
        '
        Me.ScriptingMode.BackColor = System.Drawing.SystemColors.Control
        Me.ScriptingMode.Location = New System.Drawing.Point(540, 52)
        Me.ScriptingMode.Name = "ScriptingMode"
        Me.ScriptingMode.Size = New System.Drawing.Size(100, 23)
        Me.ScriptingMode.TabIndex = 5
        Me.ScriptingMode.Text = "Scripting Mode"
        Me.ScriptingMode.UseVisualStyleBackColor = False
        Me.ScriptingMode.Visible = False
        '
        'SaveScriptBtn
        '
        Me.SaveScriptBtn.Location = New System.Drawing.Point(654, 52)
        Me.SaveScriptBtn.Name = "SaveScriptBtn"
        Me.SaveScriptBtn.Size = New System.Drawing.Size(100, 23)
        Me.SaveScriptBtn.TabIndex = 6
        Me.SaveScriptBtn.Text = "Save"
        Me.SaveScriptBtn.UseVisualStyleBackColor = True
        Me.SaveScriptBtn.Visible = False
        '
        'DeleteRowsBtn
        '
        Me.DeleteRowsBtn.Location = New System.Drawing.Point(312, 52)
        Me.DeleteRowsBtn.Name = "DeleteRowsBtn"
        Me.DeleteRowsBtn.Size = New System.Drawing.Size(100, 23)
        Me.DeleteRowsBtn.TabIndex = 7
        Me.DeleteRowsBtn.Text = "Delete Seleted Rows"
        Me.DeleteRowsBtn.UseVisualStyleBackColor = True
        '
        'NvWriteModeBtn
        '
        Me.NvWriteModeBtn.Location = New System.Drawing.Point(768, 52)
        Me.NvWriteModeBtn.Name = "NvWriteModeBtn"
        Me.NvWriteModeBtn.Size = New System.Drawing.Size(100, 23)
        Me.NvWriteModeBtn.TabIndex = 8
        Me.NvWriteModeBtn.Text = "NV Write Mode"
        Me.NvWriteModeBtn.UseVisualStyleBackColor = True
        '
        'NvWriteBtn
        '
        Me.NvWriteBtn.Location = New System.Drawing.Point(882, 52)
        Me.NvWriteBtn.Name = "NvWriteBtn"
        Me.NvWriteBtn.Size = New System.Drawing.Size(100, 23)
        Me.NvWriteBtn.TabIndex = 9
        Me.NvWriteBtn.Text = "NV Write"
        Me.NvWriteBtn.UseVisualStyleBackColor = True
        '
        'FilterCombo
        '
        Me.FilterCombo.FormattingEnabled = True
        Me.FilterCombo.Items.AddRange(New Object() {"Bad Items(Leave Access Denied)", "All Bad Items"})
        Me.FilterCombo.Location = New System.Drawing.Point(128, 54)
        Me.FilterCombo.Name = "FilterCombo"
        Me.FilterCombo.Size = New System.Drawing.Size(170, 21)
        Me.FilterCombo.TabIndex = 10
        '
        'CheckAll
        '
        Me.CheckAll.Location = New System.Drawing.Point(426, 52)
        Me.CheckAll.Name = "CheckAll"
        Me.CheckAll.Size = New System.Drawing.Size(100, 23)
        Me.CheckAll.TabIndex = 11
        Me.CheckAll.Text = "Check All"
        Me.CheckAll.UseVisualStyleBackColor = True
        '
        'UpdateNumbersBtn
        '
        Me.UpdateNumbersBtn.Location = New System.Drawing.Point(784, 13)
        Me.UpdateNumbersBtn.Name = "UpdateNumbersBtn"
        Me.UpdateNumbersBtn.Size = New System.Drawing.Size(198, 23)
        Me.UpdateNumbersBtn.TabIndex = 12
        Me.UpdateNumbersBtn.Text = "Update #'s Based on Datagrid"
        Me.UpdateNumbersBtn.UseVisualStyleBackColor = True
        '
        'NvEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 480)
        Me.Controls.Add(Me.UpdateNumbersBtn)
        Me.Controls.Add(Me.CheckAll)
        Me.Controls.Add(Me.FilterCombo)
        Me.Controls.Add(Me.NvWriteBtn)
        Me.Controls.Add(Me.NvWriteModeBtn)
        Me.Controls.Add(Me.DeleteRowsBtn)
        Me.Controls.Add(Me.SaveScriptBtn)
        Me.Controls.Add(Me.ScriptingMode)
        Me.Controls.Add(Me.XMLtoNVILBtn)
        Me.Controls.Add(Me.FilterButton)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.nvEditorItemListTxtbox)
        Me.Controls.Add(Me.LoadNvEditorBtn)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NvEditor"
        Me.Text = "NvEditor"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LoadNvEditorBtn As System.Windows.Forms.Button
    Friend WithEvents nvEditorItemListTxtbox As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FilterButton As System.Windows.Forms.Button
    Friend WithEvents XMLtoNVILBtn As System.Windows.Forms.Button
    Friend WithEvents ScriptingMode As System.Windows.Forms.Button
    Friend WithEvents SaveScriptBtn As System.Windows.Forms.Button
    Friend WithEvents DeleteRowsBtn As System.Windows.Forms.Button
    Friend WithEvents NvWriteModeBtn As System.Windows.Forms.Button
    Friend WithEvents NvWriteBtn As System.Windows.Forms.Button
    Friend WithEvents FilterCombo As System.Windows.Forms.ComboBox
    Friend WithEvents CheckAll As System.Windows.Forms.Button
    Friend WithEvents UpdateNumbersBtn As System.Windows.Forms.Button
End Class
