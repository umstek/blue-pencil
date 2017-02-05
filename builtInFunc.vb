Public Class builtInFunc

    Shared Function _RAND(ByVal length As Integer) As Byte()

        Dim pseudoRNG As System.Security.Cryptography.RandomNumberGenerator = New System.Security.Cryptography.RNGCryptoServiceProvider
        Dim toOut(length - 1) As Byte
        pseudoRNG.GetBytes(toOut)

        Return toOut
    End Function

    Shared Sub _TRIM(ByRef content As Byte())

        Array.Reverse(content)
        For i As Integer = content.Length - 1 To 0 Step -1
            If Not content(i) = 0 Then
                ReDim Preserve content(i)
                Exit For
            End If
        Next i
        Array.Reverse(content)

    End Sub

    Shared Function _PART(ByVal data As Byte(), ByVal start As Integer, ByVal count As Integer) As Byte()

        Dim out(count - 1) As Byte
        Array.ConstrainedCopy(data, start, out, 0, count)

        Return out
    End Function

    Shared Function _LENGTHOF(ByVal data() As Byte) As Byte()

        Dim ret(7) As Byte

        Dim a As Long = data.LongLength
        For i As Integer = 7 To 0 Step -1
            ret(i) = CByte(a Mod 256)
            a \= 256
        Next i

        Return ret
    End Function

End Class
