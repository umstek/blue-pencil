Public Class frmMain

    Dim drvLst As New List(Of String)
    Dim algLst As New List(Of String)

    Dim dirLst As New List(Of String)

    Public wiping_filesize As Long = 256 * 1024 * 1024

    Dim global_pause As Boolean = False
    Dim global_stop As Boolean = False
    Public currentShredFile As Integer = 0

    Sub disableForShred()
        cmbAlgo.Enabled = False
        btnAlgo.Enabled = False
        lstFiles.Enabled = False
        btnShred.Enabled = False
        lstDrives.Enabled = False
        btnWipe.Enabled = False
        btnWPause.Enabled = False
        btnWStop.Enabled = False
        btnSPause.Enabled = True
        btnSStop.Enabled = True
    End Sub

    Sub disableForWipe()
        cmbAlgo.Enabled = False
        btnAlgo.Enabled = False
        lstFiles.Enabled = False
        btnShred.Enabled = False
        lstDrives.Enabled = False
        btnWipe.Enabled = False
        btnSPause.Enabled = False
        btnSStop.Enabled = False
        btnWPause.Enabled = True
        btnWStop.Enabled = True
    End Sub

    Sub enableForSoW()
        cmbAlgo.Enabled = True
        btnAlgo.Enabled = True
        lstFiles.Enabled = True
        btnShred.Enabled = True
        lstDrives.Enabled = True
        btnWipe.Enabled = True
        btnWPause.Enabled = False
        btnWStop.Enabled = False
        btnSPause.Enabled = False
        btnSStop.Enabled = False
    End Sub

    Private Sub btnShred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShred.Click

        If Not cmbAlgo.SelectedItem = Nothing Then

            global_stop = False
            codeExec.script_load(algLst(cmbAlgo.SelectedIndex))
            shred_nextfile(False)
            codeExec.scriptZ.stopped = False

            disableForShred()
        End If

    End Sub

    Private Sub btnSPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPause.Click

        codeExec.scriptZ.paused = Not codeExec.scriptZ.paused

        If codeExec.scriptZ.paused Then
            btnSPause.Text = "Resume"
            global_pause = True
        Else
            btnSPause.Text = "Pause"
            global_pause = False
            shred_nextfile(True)
        End If

    End Sub

    Private Sub btnSStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSStop.Click
        codeExec.scriptZ.stopped = True
        global_stop = True
        enableForSoW()
    End Sub

    Private Sub lstFiles_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragDrop
        On Error Resume Next

        Dim files As String() = e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop)

        For i As Integer = 0 To files.Length - 1

            If System.IO.File.Exists(files(i)) Then

                If Not lstFiles.Items.Contains(files(i)) Then
                    lstFiles.Items.Add(files(i))
                End If

            ElseIf System.IO.Directory.Exists(files(i)) Then
                Dim fileList As New List(Of String)

                If Not dirLst.Contains(files(i)) Then
                    dirLst.Add(files(i))
                End If

                _recursiveFileGet(New System.IO.DirectoryInfo(files(i)), fileList)

                For j As Integer = 0 To fileList.Count - 1

                    If Not lstFiles.Items.Contains(fileList(j)) Then
                        lstFiles.Items.Add(fileList(j))
                    End If

                Next

            Else
                'error - not a file, not a folder?
            End If

        Next

    End Sub

    Sub _recursiveFileGet(ByVal folder As System.IO.DirectoryInfo, ByRef fileList As List(Of String))
        On Error Resume Next

        For Each f As System.IO.FileInfo In folder.GetFiles
            fileList.Add(f.FullName)
        Next

        For Each dirN As System.IO.DirectoryInfo In folder.GetDirectories
            _recursiveFileGet(dirN, fileList)
        Next

    End Sub

    Private Sub lstFiles_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragEnter

        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop, True), e.AllowedEffect And DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub lstFiles_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragOver

        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop, True), e.AllowedEffect And DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        listAlgos()
        listDrives()

    End Sub

    Private Sub mnuRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemove.Click

        If Not lstFiles.SelectedItems.Count = 0 Then

            Dim arrf(lstFiles.SelectedItems.Count - 1) As String
            lstFiles.SelectedItems.CopyTo(arrf, 0)

            For i As Integer = 0 To arrf.Length - 1
                _recursiveFolderUnlist(My.Computer.FileSystem.GetFileInfo(arrf(i)).Directory)
                lstFiles.Items.Remove(arrf(i))
            Next

        End If

    End Sub

    Private Sub mnuClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClear.Click
        lstFiles.Items.Clear()
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        If Not lstFiles.SelectedItems.Count = 0 Then
            tt0.Show(lstFiles.SelectedItems(0), lblprgShred)
        End If

    End Sub

    Sub listDrives()

        drvLst.Clear()
        For Each drive As System.IO.DriveInfo In System.IO.DriveInfo.GetDrives

            If drive.IsReady Then
                drvLst.Add(drive.Name)
                lstDrives.Items.Add(drive.VolumeLabel & " " & drive.Name & " (" & drive.DriveType.ToString & " " & drive.DriveFormat & ")")
            End If

        Next

    End Sub

    Sub listAlgos()

        algLst.Clear()
        Dim folder As New System.IO.DirectoryInfo(Application.StartupPath + "\Algo\")

        For Each f As System.IO.FileInfo In folder.GetFiles
            If f.Extension = ".bps" Then
                algLst.Add(f.FullName)
                cmbAlgo.Items.Add(f.Name.Replace(f.Extension, ""))
            End If
        Next

    End Sub

    Private Sub btnAlgo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlgo.Click

        If Not cmbAlgo.SelectedItem = Nothing Then
            Process.Start("notepad", algLst(cmbAlgo.SelectedIndex))
        End If

    End Sub

    Private Sub cmbAlgo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAlgo.SelectedIndexChanged

        If Not cmbAlgo.SelectedItem = Nothing Then
            codeExec.script_load(algLst(cmbAlgo.SelectedIndex))
            txtDesc.Text = codeExec.scriptZ.bps.<bluepencil>.<info>.Value.Replace("\n", vbCrLf).Trim
            lblDesc.Text = codeExec.scriptZ.bps.<bluepencil>.@algorithm + " by: " + codeExec.scriptZ.bps.<bluepencil>.<info>.@author + "; passes: " + codeExec.scriptZ.bps.<bluepencil>.<info>.@passes
        End If

    End Sub

    Private Sub btnMoreAlgo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreAlgo.Click
        MsgBox("Oops. Not implemented yet.")
    End Sub

    Private Sub btnWipe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWipe.Click

        If Not cmbAlgo.SelectedItem = Nothing Then
            codeExec.script_load(algLst(cmbAlgo.SelectedIndex))

            codeExec.scriptZ.bps.<bluepencil>.<file>.<event_pause>.Descendants.Remove()
            codeExec.scriptZ.bps.<bluepencil>.<file>.Descendants("event_pause").FirstOrDefault.Add(codeExec.scriptZ.bps.<bluepencil>.<freespace>.<event_pause>.Descendants)
            codeExec.scriptZ.bps.<bluepencil>.<file>.<event_resume>.Descendants.Remove()
            codeExec.scriptZ.bps.<bluepencil>.<file>.Descendants("event_resume").FirstOrDefault.Add(codeExec.scriptZ.bps.<bluepencil>.<freespace>.<event_resume>.Descendants)
            codeExec.scriptZ.bps.<bluepencil>.<file>.<event_stop>.Descendants.Remove()
            codeExec.scriptZ.bps.<bluepencil>.<file>.Descendants("event_stop").FirstOrDefault.Add(codeExec.scriptZ.bps.<bluepencil>.<freespace>.<event_stop>.Descendants)
            codeExec.scriptZ.bps.<bluepencil>.<file>.<event_finish>.Descendants.Remove()
            codeExec.scriptZ.bps.<bluepencil>.<file>.Descendants("event_finish").FirstOrDefault.Add(codeExec.scriptZ.bps.<bluepencil>.<freespace>.<event_finish>.Descendants)

            If Not codeExec.scriptZ.bps.<bluepencil>.<freespace>.@filesize = "$fdefault" Then
                wiping_filesize = CInt(codeExec.scriptZ.bps.<bluepencil>.<freespace>.@filesize)
            End If

            Dim tempDriveList As New List(Of String)
            For i As Integer = 0 To drvLst.Count - 1
                If lstDrives.CheckedIndices.Contains(i) Then
                    tempDriveList.Add(drvLst(i))
                End If
            Next

            codeExec.scriptZ.wiper.RunWorkerAsync(tempDriveList)
            codeExec.scriptZ.stopped = False

            disableForWipe()
        End If

    End Sub

    Sub shred_nextfile(ByVal _resume As Boolean)

        If global_stop Then
            prgSAll.Value = 0
            Exit Sub
        End If

        If Not global_pause Then

            If currentShredFile < lstFiles.Items.Count Then
                If Not _resume Then
                    codeExec.script_load(algLst(cmbAlgo.SelectedIndex))
                End If

                lblprgShred.Text = "Wiping file: " & lstFiles.Items(currentShredFile) & "; File " & (currentShredFile + 1).ToString & " of " & lstFiles.Items.Count

                codeExec.scriptZ.shredder.RunWorkerAsync(lstFiles.Items(currentShredFile))
                shread_getready(lstFiles.Items(currentShredFile))
                prgSAll.Value = CInt(((currentShredFile + 1) * 100) / lstFiles.Items.Count)
            Else
                prgSAll.Value = 0
                lstFiles.Items.Clear()
                currentShredFile = 0
                lblprgShred.Text = ""

                For i As Integer = 0 To dirLst.Count - 1
                    Dim dir As New System.IO.DirectoryInfo(dirLst(i))
                    _recursiveDirAttrNorm(dir)
                    If dir.Exists Then

                        If codeExec.scriptZ.autorename Then

                            Dim dateRnda As New Random
                            Dim yr As Integer = dateRnda.Next(1900, 2099)
                            Dim mnth As Integer = dateRnda.Next(1, 13)
                            Dim dt As Integer = dateRnda.Next(1, Date.DaysInMonth(yr, mnth) + 1)
                            Dim dateZ As New Date(yr, mnth, dt, dateRnda.Next(0, 24), dateRnda.Next(0, 60), dateRnda.Next(0, 60))
                            dir.CreationTime = dateZ

reNew:                      Dim newName As String = System.IO.Path.GetRandomFileName()

                            If My.Computer.FileSystem.DirectoryExists(System.IO.Path.GetDirectoryName(dir.FullName) + "\" + newName) Then
                                GoTo reNew
                            Else
                                My.Computer.FileSystem.RenameDirectory(dir.FullName, newName)
                            End If
                            My.Computer.FileSystem.DeleteDirectory(System.IO.Path.GetDirectoryName(dir.FullName) + "\" + newName, FileIO.DeleteDirectoryOption.DeleteAllContents)

                        End If

                    End If
                Next
                dirLst.Clear()
                enableForSoW()

            End If

        End If

    End Sub

    Sub _recursiveDirAttrNorm(ByRef folder As System.IO.DirectoryInfo)
        folder.Attributes = IO.FileAttributes.Normal

        For Each directory As System.IO.DirectoryInfo In folder.GetDirectories
            _recursiveDirAttrNorm(directory)
        Next

    End Sub

    Sub _recursiveFolderUnlist(ByRef folder As System.IO.DirectoryInfo)

        If dirLst.Contains(folder.FullName) Then
            dirLst.Remove(folder.FullName)
        Else
            _recursiveFolderUnlist(folder.Parent)
        End If

    End Sub

    Sub shread_getready(ByVal file As String)
        Dim f As New System.IO.FileInfo(file)
        f.Attributes = IO.FileAttributes.Normal
    End Sub

    Private Sub btnWPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWPause.Click
        codeExec.scriptZ.paused = Not codeExec.scriptZ.paused

        If codeExec.scriptZ.paused Then
            btnWPause.Text = "Resume"
            global_pause = True
        Else
            btnWPause.Text = "Pause"
            global_pause = False
            codeExec.scriptZ.wiper.RunWorkerAsync(New List(Of String))
        End If

    End Sub

    Private Sub btnWStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWStop.Click
        codeExec.scriptZ.stopped = True
        enableForSoW()
    End Sub

End Class
