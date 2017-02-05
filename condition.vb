Public Class condition

    Shared Function _EQUAL(ByVal in0 As Byte(), ByVal in1 As Byte()) As Boolean

        If in0.LongLength <> in1.LongLength Then
            Return False
        Else
            For i As Integer = 0 To in0.Length - 1 Step 1
                If in0(i) <> in1(i) Then
                    Return False
                    Exit Function
                End If
            Next i
            Return True
        End If

    End Function

    Shared Function _GREATERTHAN(ByVal in0 As Byte(), ByVal in1 As Byte()) As Boolean

        builtInFunc._TRIM(in0)
        builtInFunc._TRIM(in1)

        If in0.LongLength > in1.LongLength Then
            Return True
            Exit Function

        ElseIf in0.LongLength < in1.LongLength Then
            Return False
            Exit Function

        Else
            Dim p As Integer = 0
reCheck:
            If in0(p) > in1(p) Then
                Return True
                Exit Function
            ElseIf in0(p) < in1(p) Then
                Return False
                Exit Function
            Else
                If Not p + 1 = in0.LongLength Then
                    p += 1
                    GoTo reCheck
                Else
                    GoTo subst
                End If

            End If

subst:      Return False
            Exit Function

        End If

    End Function

    Shared Function _LESSERTHAN(ByVal in0 As Byte(), ByVal in1 As Byte()) As Boolean

        builtInFunc._TRIM(in0)
        builtInFunc._TRIM(in1)

        If in0.LongLength < in1.LongLength Then
            Return True
            Exit Function

        ElseIf in0.LongLength > in1.LongLength Then
            Return False
            Exit Function

        Else
            Dim p As Integer = 0
reCheck:
            If in0(p) < in1(p) Then
                Return True
                Exit Function
            ElseIf in0(p) > in1(p) Then
                Return False
                Exit Function
            Else
                If Not p + 1 = in0.LongLength Then
                    p += 1
                    GoTo reCheck
                Else
                    GoTo subst
                End If

            End If

subst:      Return False
            Exit Function

        End If

    End Function

End Class
