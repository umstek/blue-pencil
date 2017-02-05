Public Class Math

    'auto-expanding variables concept
    Shared Function _ADD(ByVal in0 As Byte(), ByVal in1 As Byte()) As Byte()

        Dim pendingOut As Byte()

        If in0.Length > in1.Length Then

            ReDim pendingOut(in0.Length - 1)
            Dim carryOut As Byte = 0

            For i As Integer = 0 To in1.Length - 1
                Dim int0 As Int16 = in0(in0.Length - i) + in1(in1.Length - i) + carryOut
                If int0 > 255 Then
                    carryOut = 1
                    pendingOut(i) = CByte(int0 - 256)
                Else
                    carryOut = 0
                    pendingOut(i) = CByte(int0)
                End If
            Next

            For i As Integer = in1.Length To in0.Length - 1
                Dim int0 As Int16 = in0(in0.Length - i) + carryOut
                If int0 > 255 Then
                    carryOut = 1
                    pendingOut(i) = CByte(int0 - 256)
                Else
                    carryOut = 0
                    pendingOut(i) = CByte(int0)
                    Exit For
                End If
            Next

            If carryOut > 0 Then
                ReDim Preserve pendingOut(pendingOut.Length)
                pendingOut(pendingOut.Length - 1) = carryOut
                Array.Reverse(pendingOut)
                Return pendingOut
            Else
                Array.Reverse(pendingOut)
                Return pendingOut
            End If

        Else

            ReDim pendingOut(in1.Length - 1)
            Dim carryOut As Byte = 0

            For i As Integer = 0 To in0.Length - 1
                Dim int0 As Int16 = in1(in1.Length - i) + in0(in0.Length - i) + carryOut
                If int0 > 255 Then
                    carryOut = 1
                    pendingOut(i) = CByte(int0 - 256)
                Else
                    carryOut = 0
                    pendingOut(i) = CByte(int0)
                End If
            Next

            For i As Integer = in0.Length To in1.Length - 1
                Dim int0 As Int16 = in1(in1.Length - i) + carryOut
                If int0 > 255 Then
                    carryOut = 1
                    pendingOut(i) = CByte(int0 - 256)
                Else
                    carryOut = 0
                    pendingOut(i) = CByte(int0)
                    Exit For
                End If
            Next

            If carryOut > 0 Then
                ReDim Preserve pendingOut(pendingOut.Length)
                pendingOut(pendingOut.Length - 1) = carryOut
                Array.Reverse(pendingOut)
                Return pendingOut
            Else
                Array.Reverse(pendingOut)
                Return pendingOut
            End If

        End If

    End Function

    Shared Function _SUBSTRACT(ByVal in0 As Byte(), ByVal in1 As Byte()) As Byte()

        'in0-in1
        builtInFunc._TRIM(in0)
        builtInFunc._TRIM(in1)

        If in0.Length > in1.Length Then

            Dim borrow As Integer = 0
            For i As Integer = 0 To in1.Length - 1 Step 1
                If in0(in0.Length - 1 - i) >= (in1(in1.Length - 1 - i) + borrow) Then
                    in0(in0.Length - 1 - i) = CByte(in0(in0.Length - 1 - i) - in1(in1.Length - 1 - i) - borrow)
                    borrow = 0
                Else
                    in0(in0.Length - 1 - i) = CByte(256 + in0(in0.Length - 1 - i) - in1(in1.Length - 1 - i) - borrow)
                    borrow = 1
                End If
            Next
            Return in0

        ElseIf in0.Length < in1.Length Then
            Throw New Exception("Negative values not available")
            Exit Function
        Else

            Dim p As Integer = 0
reCheck:
            If in0(p) < in1(p) Then
                Throw New Exception("Negative values not available")
                Exit Function
            ElseIf in0(p) = in1(p) Then
                If Not p + 1 = in0.Length Then
                    p += 1
                    GoTo reCheck
                Else
                    GoTo subst
                End If
            Else
                GoTo subst
            End If
subst:
            Dim borrow As Integer = 0
            For i As Integer = 0 To in1.Length - 1 Step 1
                If in0(in0.Length - 1 - i) >= (in1(in1.Length - 1 - i) + borrow) Then
                    in0(in0.Length - 1 - i) = CByte(in0(in0.Length - 1 - i) - in1(in1.Length - 1 - i) - borrow)
                    borrow = 0
                Else
                    in0(in0.Length - 1 - i) = CByte(256 + in0(in0.Length - 1 - i) - in1(in1.Length - 1 - i) - borrow)
                    borrow = 1
                End If
            Next
            Return in0

        End If

    End Function

    Shared Function _MULTIPLY(ByVal in0 As Byte(), ByVal in1 As Byte()) As Byte()



    End Function

    Shared Function _DIVIDE(ByVal in0 As Byte(), ByVal in1 As Byte()) As Byte()



    End Function

    Shared Function _POWER(ByVal in0 As Byte(), ByVal in1 As Byte()) As Byte()



    End Function

End Class

'good stalkers are actually followed by the person who's being stalked...
'don't leave a trace