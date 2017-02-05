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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lstFiles = New System.Windows.Forms.ListBox()
        Me.cmsFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblFiles = New System.Windows.Forms.Label()
        Me.btnSPause = New System.Windows.Forms.Button()
        Me.btnShred = New System.Windows.Forms.Button()
        Me.btnSStop = New System.Windows.Forms.Button()
        Me.prgSUni = New System.Windows.Forms.ProgressBar()
        Me.prgSAll = New System.Windows.Forms.ProgressBar()
        Me.lblprgShred = New System.Windows.Forms.Label()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tpFile = New System.Windows.Forms.TabPage()
        Me.tpFreespace = New System.Windows.Forms.TabPage()
        Me.prgWAll = New System.Windows.Forms.ProgressBar()
        Me.prgWUni = New System.Windows.Forms.ProgressBar()
        Me.lblprgWipe = New System.Windows.Forms.Label()
        Me.btnWipe = New System.Windows.Forms.Button()
        Me.btnWPause = New System.Windows.Forms.Button()
        Me.btnWStop = New System.Windows.Forms.Button()
        Me.lblFreespace = New System.Windows.Forms.Label()
        Me.lstDrives = New System.Windows.Forms.CheckedListBox()
        Me.cmbAlgo = New System.Windows.Forms.ComboBox()
        Me.lblAlgo = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.btnAlgo = New System.Windows.Forms.Button()
        Me.btnMoreAlgo = New System.Windows.Forms.Button()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.grpMT = New System.Windows.Forms.GroupBox()
        Me.numThreads = New System.Windows.Forms.NumericUpDown()
        Me.lblThreads = New System.Windows.Forms.Label()
        Me.tt0 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmsFiles.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tpFile.SuspendLayout()
        Me.tpFreespace.SuspendLayout()
        Me.grpMT.SuspendLayout()
        CType(Me.numThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstFiles
        '
        Me.lstFiles.AllowDrop = True
        Me.lstFiles.ContextMenuStrip = Me.cmsFiles
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.ItemHeight = 17
        Me.lstFiles.Location = New System.Drawing.Point(6, 29)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFiles.Size = New System.Drawing.Size(750, 157)
        Me.lstFiles.TabIndex = 1
        '
        'cmsFiles
        '
        Me.cmsFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRemove, Me.mnuClear})
        Me.cmsFiles.Name = "cmsFiles"
        Me.cmsFiles.Size = New System.Drawing.Size(118, 48)
        '
        'mnuRemove
        '
        Me.mnuRemove.Name = "mnuRemove"
        Me.mnuRemove.Size = New System.Drawing.Size(117, 22)
        Me.mnuRemove.Text = "Remove"
        '
        'mnuClear
        '
        Me.mnuClear.Name = "mnuClear"
        Me.mnuClear.Size = New System.Drawing.Size(117, 22)
        Me.mnuClear.Text = "Clear"
        '
        'lblFiles
        '
        Me.lblFiles.Location = New System.Drawing.Point(6, 3)
        Me.lblFiles.Name = "lblFiles"
        Me.lblFiles.Size = New System.Drawing.Size(750, 23)
        Me.lblFiles.TabIndex = 0
        Me.lblFiles.Text = "Drag files here to shred."
        '
        'btnSPause
        '
        Me.btnSPause.Enabled = False
        Me.btnSPause.Location = New System.Drawing.Point(600, 214)
        Me.btnSPause.Name = "btnSPause"
        Me.btnSPause.Size = New System.Drawing.Size(75, 29)
        Me.btnSPause.TabIndex = 6
        Me.btnSPause.Text = "Pause"
        Me.btnSPause.UseVisualStyleBackColor = True
        '
        'btnShred
        '
        Me.btnShred.Location = New System.Drawing.Point(6, 214)
        Me.btnShred.Name = "btnShred"
        Me.btnShred.Size = New System.Drawing.Size(75, 29)
        Me.btnShred.TabIndex = 3
        Me.btnShred.Text = "Shred"
        Me.btnShred.UseVisualStyleBackColor = True
        '
        'btnSStop
        '
        Me.btnSStop.Enabled = False
        Me.btnSStop.Location = New System.Drawing.Point(681, 214)
        Me.btnSStop.Name = "btnSStop"
        Me.btnSStop.Size = New System.Drawing.Size(75, 29)
        Me.btnSStop.TabIndex = 7
        Me.btnSStop.Text = "Stop"
        Me.btnSStop.UseVisualStyleBackColor = True
        '
        'prgSUni
        '
        Me.prgSUni.Location = New System.Drawing.Point(87, 219)
        Me.prgSUni.Name = "prgSUni"
        Me.prgSUni.Size = New System.Drawing.Size(507, 10)
        Me.prgSUni.TabIndex = 4
        '
        'prgSAll
        '
        Me.prgSAll.Location = New System.Drawing.Point(87, 229)
        Me.prgSAll.Name = "prgSAll"
        Me.prgSAll.Size = New System.Drawing.Size(507, 10)
        Me.prgSAll.TabIndex = 5
        '
        'lblprgShred
        '
        Me.lblprgShred.Location = New System.Drawing.Point(3, 194)
        Me.lblprgShred.Name = "lblprgShred"
        Me.lblprgShred.Size = New System.Drawing.Size(750, 17)
        Me.lblprgShred.TabIndex = 2
        Me.lblprgShred.Text = "Wiping file 0 of n"
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tpFile)
        Me.tabMain.Controls.Add(Me.tpFreespace)
        Me.tabMain.Location = New System.Drawing.Point(12, 131)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(770, 279)
        Me.tabMain.TabIndex = 7
        '
        'tpFile
        '
        Me.tpFile.Controls.Add(Me.lblFiles)
        Me.tpFile.Controls.Add(Me.prgSAll)
        Me.tpFile.Controls.Add(Me.lstFiles)
        Me.tpFile.Controls.Add(Me.prgSUni)
        Me.tpFile.Controls.Add(Me.lblprgShred)
        Me.tpFile.Controls.Add(Me.btnShred)
        Me.tpFile.Controls.Add(Me.btnSPause)
        Me.tpFile.Controls.Add(Me.btnSStop)
        Me.tpFile.Location = New System.Drawing.Point(4, 26)
        Me.tpFile.Name = "tpFile"
        Me.tpFile.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFile.Size = New System.Drawing.Size(762, 249)
        Me.tpFile.TabIndex = 0
        Me.tpFile.Text = "Files"
        Me.tpFile.UseVisualStyleBackColor = True
        '
        'tpFreespace
        '
        Me.tpFreespace.Controls.Add(Me.prgWAll)
        Me.tpFreespace.Controls.Add(Me.prgWUni)
        Me.tpFreespace.Controls.Add(Me.lblprgWipe)
        Me.tpFreespace.Controls.Add(Me.btnWipe)
        Me.tpFreespace.Controls.Add(Me.btnWPause)
        Me.tpFreespace.Controls.Add(Me.btnWStop)
        Me.tpFreespace.Controls.Add(Me.lblFreespace)
        Me.tpFreespace.Controls.Add(Me.lstDrives)
        Me.tpFreespace.Location = New System.Drawing.Point(4, 26)
        Me.tpFreespace.Name = "tpFreespace"
        Me.tpFreespace.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFreespace.Size = New System.Drawing.Size(762, 249)
        Me.tpFreespace.TabIndex = 1
        Me.tpFreespace.Text = "Freespace"
        Me.tpFreespace.UseVisualStyleBackColor = True
        '
        'prgWAll
        '
        Me.prgWAll.Location = New System.Drawing.Point(87, 229)
        Me.prgWAll.Name = "prgWAll"
        Me.prgWAll.Size = New System.Drawing.Size(507, 10)
        Me.prgWAll.TabIndex = 5
        '
        'prgWUni
        '
        Me.prgWUni.Location = New System.Drawing.Point(87, 219)
        Me.prgWUni.Name = "prgWUni"
        Me.prgWUni.Size = New System.Drawing.Size(507, 10)
        Me.prgWUni.TabIndex = 4
        '
        'lblprgWipe
        '
        Me.lblprgWipe.Location = New System.Drawing.Point(3, 194)
        Me.lblprgWipe.Name = "lblprgWipe"
        Me.lblprgWipe.Size = New System.Drawing.Size(753, 17)
        Me.lblprgWipe.TabIndex = 2
        Me.lblprgWipe.Text = "Wiping freespace"
        '
        'btnWipe
        '
        Me.btnWipe.Location = New System.Drawing.Point(6, 214)
        Me.btnWipe.Name = "btnWipe"
        Me.btnWipe.Size = New System.Drawing.Size(75, 29)
        Me.btnWipe.TabIndex = 3
        Me.btnWipe.Text = "Wipe"
        Me.btnWipe.UseVisualStyleBackColor = True
        '
        'btnWPause
        '
        Me.btnWPause.Enabled = False
        Me.btnWPause.Location = New System.Drawing.Point(600, 214)
        Me.btnWPause.Name = "btnWPause"
        Me.btnWPause.Size = New System.Drawing.Size(75, 29)
        Me.btnWPause.TabIndex = 6
        Me.btnWPause.Text = "Pause"
        Me.btnWPause.UseVisualStyleBackColor = True
        '
        'btnWStop
        '
        Me.btnWStop.Enabled = False
        Me.btnWStop.Location = New System.Drawing.Point(681, 214)
        Me.btnWStop.Name = "btnWStop"
        Me.btnWStop.Size = New System.Drawing.Size(75, 29)
        Me.btnWStop.TabIndex = 7
        Me.btnWStop.Text = "Stop"
        Me.btnWStop.UseVisualStyleBackColor = True
        '
        'lblFreespace
        '
        Me.lblFreespace.Location = New System.Drawing.Point(6, 3)
        Me.lblFreespace.Name = "lblFreespace"
        Me.lblFreespace.Size = New System.Drawing.Size(750, 23)
        Me.lblFreespace.TabIndex = 0
        Me.lblFreespace.Text = "Select drives to wipe freespace."
        '
        'lstDrives
        '
        Me.lstDrives.CheckOnClick = True
        Me.lstDrives.FormattingEnabled = True
        Me.lstDrives.Location = New System.Drawing.Point(6, 29)
        Me.lstDrives.Name = "lstDrives"
        Me.lstDrives.Size = New System.Drawing.Size(750, 164)
        Me.lstDrives.TabIndex = 1
        '
        'cmbAlgo
        '
        Me.cmbAlgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlgo.FormattingEnabled = True
        Me.cmbAlgo.Location = New System.Drawing.Point(191, 12)
        Me.cmbAlgo.Name = "cmbAlgo"
        Me.cmbAlgo.Size = New System.Drawing.Size(283, 25)
        Me.cmbAlgo.TabIndex = 1
        '
        'lblAlgo
        '
        Me.lblAlgo.Location = New System.Drawing.Point(12, 12)
        Me.lblAlgo.Name = "lblAlgo"
        Me.lblAlgo.Size = New System.Drawing.Size(173, 25)
        Me.lblAlgo.TabIndex = 0
        Me.lblAlgo.Text = "Select shredder algorithm"
        '
        'lblDesc
        '
        Me.lblDesc.Location = New System.Drawing.Point(12, 40)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(522, 25)
        Me.lblDesc.TabIndex = 3
        Me.lblDesc.Text = "Author:"
        '
        'btnAlgo
        '
        Me.btnAlgo.Location = New System.Drawing.Point(474, 12)
        Me.btnAlgo.Name = "btnAlgo"
        Me.btnAlgo.Size = New System.Drawing.Size(60, 25)
        Me.btnAlgo.TabIndex = 2
        Me.btnAlgo.Text = "Edit"
        Me.btnAlgo.UseVisualStyleBackColor = True
        '
        'btnMoreAlgo
        '
        Me.btnMoreAlgo.Location = New System.Drawing.Point(540, 12)
        Me.btnMoreAlgo.Name = "btnMoreAlgo"
        Me.btnMoreAlgo.Size = New System.Drawing.Size(242, 25)
        Me.btnMoreAlgo.TabIndex = 5
        Me.btnMoreAlgo.Text = "More tasks on Algorithms..."
        Me.btnMoreAlgo.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDesc.Location = New System.Drawing.Point(12, 68)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDesc.Size = New System.Drawing.Size(522, 57)
        Me.txtDesc.TabIndex = 4
        '
        'grpMT
        '
        Me.grpMT.Controls.Add(Me.numThreads)
        Me.grpMT.Controls.Add(Me.lblThreads)
        Me.grpMT.Enabled = False
        Me.grpMT.Location = New System.Drawing.Point(540, 43)
        Me.grpMT.Name = "grpMT"
        Me.grpMT.Size = New System.Drawing.Size(242, 82)
        Me.grpMT.TabIndex = 6
        Me.grpMT.TabStop = False
        Me.grpMT.Text = "Multithreading options"
        '
        'numThreads
        '
        Me.numThreads.Location = New System.Drawing.Point(189, 21)
        Me.numThreads.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.numThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numThreads.Name = "numThreads"
        Me.numThreads.Size = New System.Drawing.Size(47, 25)
        Me.numThreads.TabIndex = 1
        Me.numThreads.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblThreads
        '
        Me.lblThreads.Location = New System.Drawing.Point(6, 23)
        Me.lblThreads.Name = "lblThreads"
        Me.lblThreads.Size = New System.Drawing.Size(177, 23)
        Me.lblThreads.TabIndex = 0
        Me.lblThreads.Text = "Number of threads at-a-time"
        '
        'tt0
        '
        Me.tt0.BackColor = System.Drawing.Color.AliceBlue
        Me.tt0.ForeColor = System.Drawing.Color.Black
        Me.tt0.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 422)
        Me.Controls.Add(Me.grpMT)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblAlgo)
        Me.Controls.Add(Me.cmbAlgo)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.btnMoreAlgo)
        Me.Controls.Add(Me.btnAlgo)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "bluepencil scriptable shredder (alpha)"
        Me.cmsFiles.ResumeLayout(False)
        Me.tabMain.ResumeLayout(False)
        Me.tpFile.ResumeLayout(False)
        Me.tpFreespace.ResumeLayout(False)
        Me.grpMT.ResumeLayout(False)
        CType(Me.numThreads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents lblFiles As System.Windows.Forms.Label
    Friend WithEvents btnSPause As System.Windows.Forms.Button
    Friend WithEvents btnShred As System.Windows.Forms.Button
    Friend WithEvents btnSStop As System.Windows.Forms.Button
    Friend WithEvents prgSUni As System.Windows.Forms.ProgressBar
    Friend WithEvents prgSAll As System.Windows.Forms.ProgressBar
    Friend WithEvents lblprgShred As System.Windows.Forms.Label
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tpFile As System.Windows.Forms.TabPage
    Friend WithEvents tpFreespace As System.Windows.Forms.TabPage
    Friend WithEvents lblFreespace As System.Windows.Forms.Label
    Friend WithEvents lstDrives As System.Windows.Forms.CheckedListBox
    Friend WithEvents prgWAll As System.Windows.Forms.ProgressBar
    Friend WithEvents prgWUni As System.Windows.Forms.ProgressBar
    Friend WithEvents lblprgWipe As System.Windows.Forms.Label
    Friend WithEvents btnWipe As System.Windows.Forms.Button
    Friend WithEvents btnWPause As System.Windows.Forms.Button
    Friend WithEvents btnWStop As System.Windows.Forms.Button
    Friend WithEvents cmbAlgo As System.Windows.Forms.ComboBox
    Friend WithEvents lblAlgo As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents btnAlgo As System.Windows.Forms.Button
    Friend WithEvents btnMoreAlgo As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents grpMT As System.Windows.Forms.GroupBox
    Friend WithEvents lblThreads As System.Windows.Forms.Label
    Friend WithEvents numThreads As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmsFiles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tt0 As System.Windows.Forms.ToolTip

End Class
