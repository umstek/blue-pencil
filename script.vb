Public Class script

    Public bps As New XDocument
    Public fileversion As String
    Public algoversion As String
    Public algoname As String

    Public autorename As Boolean = False
    Public blocksize As Integer

    Public paused As Boolean = False
    Public currentBlock As Integer = 0

    Public stopped As Boolean = False

    Public current_fz As Long = 0

    Public lstfil As List(Of KeyValuePair(Of String, Long))

    Public wipe As Boolean = False

    Public Sub New(ByVal path As String)

        bps = XDocument.Parse(System.IO.File.ReadAllText(path))
        fileversion = bps.<bluepencil>.@fileversion
        algoversion = bps.<bluepencil>.@algversion
        algoname = bps.<bluepencil>.@algorithm

        If bps.<bluepencil>.<file>.@autorename = "true" Then
            autorename = True
        End If

        Select Case bps.<bluepencil>.<file>.@blocksize
            Case "$solid"
                blocksize = -1
            Case "$default"
                blocksize = 0
            Case Else
                blocksize = CInt(bps.<bluepencil>.<file>.@blocksize)
        End Select

    End Sub

    Friend WithEvents shredder As New System.ComponentModel.BackgroundWorker

    Private Sub shredder_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles shredder.DoWork

        shredder.WorkerReportsProgress = True
        shredder.WorkerSupportsCancellation = True

        Dim file As String = CStr(e.Argument)


        codeExec.selectedFile = file
        Select Case blocksize
            Case -1
                codeExec.blockSize = My.Computer.FileSystem.GetFileInfo(file).Length
                blocksize = My.Computer.FileSystem.GetFileInfo(file).Length
            Case 0
                codeExec.blockSize = 4096
                blocksize = 4096
            Case Else
                codeExec.blockSize = blocksize
        End Select
        current_fz = My.Computer.FileSystem.GetFileInfo(file).Length

        Dim blocks As Integer = (My.Computer.FileSystem.GetFileInfo(file).Length \ blocksize) + 1

        If Not currentBlock = 0 Then
            codeExec.execute_function("event_resume")
        End If

        For i As Integer = currentBlock To blocks - 1

            codeExec._setCurrentBlock()

            If stopped Then
                codeExec.execute_function("event_stop")
                shredder.CancelAsync()
                Exit Sub
            End If

            If paused Then
                currentBlock = i
                codeExec.execute_function("event_pause")
                shredder.CancelAsync()
                Exit Sub
            Else
                codeExec.selectedBlock = i
                codeExec.execute_function("main")
            End If

            If Not CInt(blocks / 100) = 0 Then
                If ((i + 1) Mod CInt(blocks / 100)) = 0 Then
                    shredder.ReportProgress((i + 1) * 100 / blocks)
                End If
            End If

        Next

    End Sub

    Private Sub shredder_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles shredder.ProgressChanged
        ''
        frmMain.prgSUni.Value = e.ProgressPercentage
        ''
    End Sub

    Private Sub shredder_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles shredder.RunWorkerCompleted

        If Not paused Then
            codeExec.execute_function("event_finish")
            frmMain.prgSUni.Value = 0
            currentBlock = 0
            frmMain.currentShredFile += 1
            frmMain.shred_nextfile(False)
        End If

    End Sub


    Friend WithEvents wiper As New System.ComponentModel.BackgroundWorker

    Private Sub wiper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles wiper.DoWork
        wipe = True

        wiper.WorkerReportsProgress = True
        wiper.WorkerSupportsCancellation = True

        If Not CType(e.Argument, List(Of String)).Count = 0 Then

            Dim lstdrv As New List(Of String)(CType(e.Argument, List(Of String)))
            lstfil = New List(Of KeyValuePair(Of String, Long))

            For i As Integer = 0 To lstdrv.Count - 1
                Dim driv As New System.IO.DriveInfo(lstdrv(i))

                If Not driv.IsReady Then
                    Resume Next
                End If

                Dim totalrq As Long = 0

                While driv.TotalFreeSpace > totalrq
reNew:              Dim fname As String = driv.RootDirectory.FullName & System.IO.Path.GetRandomFileName
                    If System.IO.File.Exists(fname) Then
                        GoTo reNew
                    End If
                    lstfil.Add(New KeyValuePair(Of String, Long)(fname, If(frmMain.wiping_filesize < driv.TotalFreeSpace - totalrq, frmMain.wiping_filesize, driv.TotalFreeSpace - totalrq)))
                    totalrq += If(frmMain.wiping_filesize < driv.TotalFreeSpace, frmMain.wiping_filesize, driv.TotalFreeSpace)
                End While

            Next

        End If

        For j As Integer = 0 To lstfil.Count - 1

            Dim fileNsz As KeyValuePair(Of String, Long) = lstfil(j)
            current_fz = fileNsz.Value

            codeExec.selectedFile = fileNsz.Key
            Select Case blocksize
                Case -1
                    codeExec.blockSize = fileNsz.Value
                    blocksize = fileNsz.Value
                Case 0
                    codeExec.blockSize = 4096
                    blocksize = 4096
                Case Else
                    codeExec.blockSize = blocksize
            End Select

            Dim blocks As Integer = (fileNsz.Value \ blocksize) + 1

            If Not currentBlock = 0 Then
                codeExec.execute_function("event_resume")
            End If

            For i As Integer = currentBlock To blocks - 1
                Dim b(blocksize - 1) As Byte
                codeExec.currentBlock = b

                If stopped Then
                    codeExec.execute_function("event_stop")
                    wiper.CancelAsync()
                    Exit Sub
                End If

                If paused Then
                    currentBlock = i
                    codeExec.execute_function("event_pause")
                    wiper.CancelAsync()
                    Exit Sub
                Else
                    codeExec.selectedBlock = i
                    codeExec.execute_function("main")
                End If

                wiper.ReportProgress((i + 1) * 100 / blocks, 0)
            Next i

            wiper.ReportProgress((j + 1) * 100 / lstfil.Count, 1)
        Next j

    End Sub

    Private Sub wiper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles wiper.ProgressChanged

        If CType(e.UserState, Integer) = 0 Then
            frmMain.prgWUni.Value = e.ProgressPercentage
        Else
            frmMain.prgWAll.Value = e.ProgressPercentage
        End If

    End Sub

    Private Sub wiper_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles wiper.RunWorkerCompleted

        If Not paused Then
            codeExec.execute_function("event_finish")
            frmMain.prgWUni.Value = 0
            frmMain.prgWAll.Value = 0
            currentBlock = 0
        End If

    End Sub

End Class
