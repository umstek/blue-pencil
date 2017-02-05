Public Class Logic

    Shared Sub _NOT(ByRef input As Byte())

        For i As Integer = 0 To input.Length - 1 Step 1
            input(i) = Not input(i)
        Next i

    End Sub

    Shared Function _AND(ByVal input0 As Byte(), ByVal input1 As Byte()) As Byte()

        If input0.Length > input1.Length Then

            Dim out(input0.Length - 1) As Byte

            For i As Integer = 0 To input1.Length - 1 Step 1
                out(i) = input0(i) And input1(i)
            Next i
            For i As Integer = input1.Length To input0.Length - 1 Step 1
                out(i) = 0
            Next i

            Return out
        Else

            Dim out(input1.Length - 1) As Byte

            For i As Integer = 0 To input0.Length - 1 Step 1
                out(i) = input1(i) And input0(i)
            Next i
            For i As Integer = input0.Length To input1.Length - 1 Step 1
                out(i) = 0
            Next i

            Return out
        End If

    End Function

    Shared Function _OR(ByVal input0 As Byte(), ByVal input1 As Byte()) As Byte()

        If input0.Length > input1.Length Then

            Dim out(input0.Length - 1) As Byte

            For i As Integer = 0 To input1.Length - 1 Step 1
                out(i) = input0(i) Or input1(i)
            Next i
            For i As Integer = input1.Length To input0.Length - 1 Step 1
                out(i) = input0(i)
            Next i

            Return out
        Else
            Dim out(input1.Length - 1) As Byte

            For i As Integer = 0 To input0.Length - 1 Step 1
                out(i) = input1(i) Or input0(i)
            Next i
            For i As Integer = input0.Length To input1.Length - 1 Step 1
                out(i) = input1(i)
            Next i

            Return out
        End If

    End Function

    Shared Function _XOR(ByVal input0 As Byte(), ByVal input1 As Byte()) As Byte()

        If input0.Length > input1.Length Then

            Dim out(input0.Length - 1) As Byte

            For i As Integer = 0 To input1.Length - 1 Step 1
                out(i) = input0(i) Xor input1(i)
            Next i
            For i As Integer = input1.Length To input0.Length - 1 Step 1
                out(i) = input0(i)
            Next i

            Return out
        Else

            Dim out(input1.Length - 1) As Byte

            For i As Integer = 0 To input0.Length - 1 Step 1
                out(i) = input1(i) Xor input0(i)
            Next i
            For i As Integer = input0.Length To input1.Length - 1 Step 1
                out(i) = input1(i)
            Next i

            Return out
        End If

    End Function

End Class
