Public Class codeExec

    'Public variables As New Collections.Generic.SortedDictionary(Of String, Byte())
    Public Shared vars As New List(Of Dictionary(Of String, Byte()))

    Public Shared selectedFile As String
    Public Shared selectedBlock As Integer 'zero based
    Public Shared blockSize As Integer
    Public Shared scriptZ As script

    Public Shared currentBlock As Byte()

    Shared Sub script_load(ByVal path As String)
        scriptZ = New script(path)
    End Sub

    Shared Sub variable_define(ByVal name As String, ByVal length As Integer)
        Dim l As Integer = Integer.Parse(length)
        Dim bA(l - 1) As Byte

        vars(vars.Count - 1).Add(name, bA)
    End Sub

    Shared Sub variable_define(ByVal name As String, ByVal content As String, ByVal ParamArray parameters() As KeyValuePair(Of String, Byte()))

        If parameters.Length = 0 Then

            If Not (content(0) = "$" Or content(0) = "#" Or content(0) = "!") Then
                Dim sA As String() = content.Split(" ")
                Dim bA(sA.Length - 1) As Byte

                For i As Integer = 0 To bA.Length - 1
                    bA(i) = Byte.Parse(sA(i))
                Next i

                vars(vars.Count - 1).Add(name, bA)
            Else

                vars(vars.Count - 1).Add(name, logic_expression_simplify(content))
            End If

        Else

            vars(vars.Count - 1).Add(name, execute_function(Mid(content, 2, content.Length - 1), parameters))
        End If

    End Sub

    Shared Sub variable_set(ByVal name As String, ByVal content As String, ByVal ParamArray parameters() As KeyValuePair(Of String, Byte()))

        If parameters.Length = 0 Then

            If content.Contains(" ") Then
                Dim sA As String() = content.Split(" ")
                Dim bA(sA.Length - 1) As Byte

                For i As Integer = 0 To bA.Length - 1
                    bA(i) = Byte.Parse(sA(i))
                Next i

                vars(vars.Count - 1)(name) = bA
            Else

                vars(vars.Count - 1)(name) = logic_expression_simplify(content)
            End If

        Else

            vars(vars.Count - 1)(name) = execute_function(Mid(content, 2, content.Length - 1), parameters)
        End If

    End Sub

    Shared Sub variable_dispose(ByVal name As String)

        vars(vars.Count - 1).Remove(name)
    End Sub

    Shared Function logic_expression_simplify(ByVal expression As String) As Byte()

        If expression.Contains("&") Then

            Dim bA1(-1) As Byte
            Dim bA2(-1) As Byte
            Dim sAmpersand As String() = expression.Split("&")

            Select Case sAmpersand(0)(0)
                Case Is = "#"
                    bA1 = execute_function(Mid(sAmpersand(0), 2, sAmpersand(0).Length - 1))

                Case Is = "$"
                    If Mid(sAmpersand(0), 2, sAmpersand(0).Length - 1) = "default" Then
                        bA1 = _IntToBytes(blockSize)
                    ElseIf Mid(sAmpersand(0), 2, sAmpersand(0).Length - 1) = "block" Then
                        bA1 = currentBlock
                    Else
                        bA1 = vars.Item(vars.Count - 1).Item(Mid(sAmpersand(0), 2, sAmpersand(0).Length - 1))
                    End If

                Case Is = "!"

                    Select Case expression.Split("&")(0)(1)
                        Case Is = "#"
                            bA1 = execute_function(Mid(sAmpersand(0), 3, sAmpersand(0).Length - 2))
                            Logic._NOT(bA1)

                        Case Is = "$"
                            If Mid(sAmpersand(0), 3, sAmpersand(0).Length - 2) = "default" Then
                                bA1 = _IntToBytes(blockSize)
                                Logic._NOT(bA1)
                            ElseIf Mid(sAmpersand(0), 3, sAmpersand(0).Length - 2) = "block" Then
                                bA1 = currentBlock
                                Logic._NOT(bA1)
                            Else
                                bA1 = vars.Item(vars.Count - 1).Item(Mid(sAmpersand(0), 3, sAmpersand(0).Length - 2))
                                Logic._NOT(bA1)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Select Case sAmpersand(1)(0)
                Case Is = "#"
                    bA2 = execute_function(Mid(sAmpersand(1), 2, sAmpersand(1).Length - 1))

                Case Is = "$"
                    If Mid(sAmpersand(1), 2, sAmpersand(1).Length - 1) = "default" Then
                        bA2 = _IntToBytes(blockSize)
                    ElseIf Mid(sAmpersand(1), 2, sAmpersand(1).Length - 1) = "block" Then
                        bA2 = currentBlock
                    Else
                        bA2 = vars.Item(vars.Count - 1).Item(Mid(sAmpersand(1), 2, sAmpersand(1).Length - 1))
                    End If

                Case Is = "!"

                    Select Case sAmpersand(1)(1)
                        Case Is = "#"
                            bA2 = execute_function(Mid(sAmpersand(1), 3, sAmpersand(1).Length - 2))
                            Logic._NOT(bA2)

                        Case Is = "$"
                            If Mid(sAmpersand(1), 3, sAmpersand(1).Length - 2) = "default" Then
                                bA2 = _IntToBytes(blockSize)
                                Logic._NOT(bA2)
                            ElseIf Mid(sAmpersand(1), 3, sAmpersand(1).Length - 2) = "block" Then
                                bA2 = currentBlock
                                Logic._NOT(bA2)
                            Else
                                bA2 = vars.Item(vars.Count - 1).Item(Mid(sAmpersand(1), 3, sAmpersand(1).Length - 2))
                                Logic._NOT(bA2)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Return Logic._AND(bA1, bA2)

        ElseIf expression.Contains("|") Then
            Dim bA1(-1) As Byte
            Dim bA2(-1) As Byte
            Dim sVBar As String() = expression.Split("|")

            Select Case sVBar(0)(0)
                Case Is = "#"
                    bA1 = execute_function(Mid(sVBar(0), 2, sVBar(0).Length - 1))

                Case Is = "$"
                    If Mid(sVBar(0), 2, sVBar(0).Length - 1) = "default" Then
                        bA1 = _IntToBytes(blockSize)
                    ElseIf Mid(sVBar(0), 2, sVBar(0).Length - 1) = "block" Then
                        bA1 = currentBlock
                    Else
                        bA1 = vars.Item(vars.Count - 1).Item(Mid(sVBar(0), 2, sVBar(0).Length - 1))
                    End If

                Case Is = "!"

                    Select Case sVBar(0)(1)
                        Case Is = "#"
                            bA1 = execute_function(Mid(sVBar(0), 3, sVBar(0).Length - 2))
                            Logic._NOT(bA1)

                        Case Is = "$"
                            If Mid(sVBar(0), 3, sVBar(0).Length - 2) = "default" Then
                                bA1 = _IntToBytes(blockSize)
                                Logic._NOT(bA1)
                            ElseIf Mid(sVBar(0), 3, sVBar(0).Length - 2) = "block" Then
                                bA1 = currentBlock
                                Logic._NOT(bA1)
                            Else
                                bA1 = vars.Item(vars.Count - 1).Item(Mid(sVBar(0), 3, sVBar(0).Length - 2))
                                Logic._NOT(bA1)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Select Case sVBar(1)(0)
                Case Is = "#"
                    bA2 = execute_function(Mid(sVBar(1), 2, sVBar(1).Length - 1))

                Case Is = "$"
                    If Mid(sVBar(1), 2, sVBar(1).Length - 1) = "default" Then
                        bA2 = _IntToBytes(blockSize)
                    ElseIf Mid(sVBar(1), 2, sVBar(1).Length - 1) = "block" Then
                        bA2 = currentBlock
                    Else
                        bA2 = vars.Item(vars.Count - 1).Item(Mid(sVBar(1), 2, sVBar(1).Length - 1))
                    End If

                Case Is = "!"

                    Select Case sVBar(1)(1)
                        Case Is = "#"
                            bA2 = execute_function(Mid(sVBar(1), 3, sVBar(1).Length - 2))
                            Logic._NOT(bA2)

                        Case Is = "$"
                            If Mid(sVBar(1), 3, sVBar(1).Length - 2) = "default" Then
                                bA2 = _IntToBytes(blockSize)
                                Logic._NOT(bA2)
                            ElseIf Mid(sVBar(1), 3, sVBar(1).Length - 2) = "block" Then
                                bA2 = currentBlock
                                Logic._NOT(bA2)
                            Else
                                bA2 = vars.Item(vars.Count - 1).Item(Mid(sVBar(1), 3, sVBar(1).Length - 2))
                                Logic._NOT(bA2)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Return Logic._OR(bA1, bA2)

        ElseIf expression.Contains("~") Then
            Dim bA1(-1) As Byte
            Dim bA2(-1) As Byte
            Dim sTilde As String() = expression.Split("~")

            Select Case sTilde(0)(0)
                Case Is = "#"
                    bA1 = execute_function(Mid(sTilde(0), 2, sTilde(0).Length - 1))

                Case Is = "$"
                    If Mid(sTilde(0), 2, sTilde(0).Length - 1) = "default" Then
                        bA1 = _IntToBytes(blockSize)
                    ElseIf Mid(sTilde(0), 2, sTilde(0).Length - 1) = "block" Then
                        bA1 = currentBlock
                    Else
                        bA1 = vars.Item(vars.Count - 1).Item(Mid(sTilde(0), 2, sTilde(0).Length - 1))
                    End If

                Case Is = "!"

                    Select Case sTilde(0)(1)
                        Case Is = "#"
                            bA1 = execute_function(Mid(sTilde(0), 3, sTilde(0).Length - 2))
                            Logic._NOT(bA1)

                        Case Is = "$"
                            If Mid(sTilde(0), 3, sTilde(0).Length - 2) = "default" Then
                                bA1 = _IntToBytes(blockSize)
                                Logic._NOT(bA1)
                            ElseIf Mid(sTilde(0), 3, sTilde(0).Length - 2) = "block" Then
                                bA1 = currentBlock
                                Logic._NOT(bA1)
                            Else
                                bA1 = vars.Item(vars.Count - 1).Item(Mid(sTilde(0), 3, sTilde(0).Length - 2))
                                Logic._NOT(bA1)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Select Case sTilde(1)(0)
                Case Is = "#"
                    bA2 = execute_function(Mid(sTilde(0), 2, sTilde(0).Length - 1))

                Case Is = "$"
                    If Mid(sTilde(1), 2, sTilde(1).Length - 1) = "default" Then
                        bA2 = _IntToBytes(blockSize)
                    ElseIf Mid(sTilde(1), 2, sTilde(1).Length - 1) = "block" Then
                        bA2 = currentBlock
                    Else
                        bA2 = vars.Item(vars.Count - 1).Item(Mid(sTilde(0), 2, sTilde(0).Length - 1))
                    End If

                Case Is = "!"

                    Select Case sTilde(1)(1)
                        Case Is = "#"
                            bA2 = execute_function(Mid(sTilde(0), 3, sTilde(0).Length - 2))
                            Logic._NOT(bA2)

                        Case Is = "$"
                            If Mid(sTilde(1), 3, sTilde(1).Length - 2) = "default" Then
                                bA2 = _IntToBytes(blockSize)
                                Logic._NOT(bA2)
                            ElseIf Mid(sTilde(1), 3, sTilde(1).Length - 2) = "block" Then
                                bA2 = currentBlock
                                Logic._NOT(bA2)
                            Else
                                bA2 = vars.Item(vars.Count - 1).Item(Mid(sTilde(0), 3, sTilde(0).Length - 2))
                                Logic._NOT(bA2)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Return Logic._XOR(bA1, bA2)

        Else

            Dim bA0(-1) As Byte

            Select Case expression(0)
                Case Is = "#"
                    bA0 = execute_function(Mid(expression, 2, expression.Length - 1))

                Case Is = "$"
                    If Mid(expression, 2, expression.Length - 1) = "default" Then
                        bA0 = _IntToBytes(blockSize)
                    ElseIf Mid(expression, 2, expression.Length - 1) = "block" Then
                        bA0 = currentBlock
                    Else
                        bA0 = vars.Item(vars.Count - 1).Item(Mid(expression, 2, expression.Length - 1))
                    End If

                Case Is = "!"

                    Select Case expression(1)
                        Case Is = "#"
                            bA0 = execute_function(Mid(expression, 3, expression.Length - 2))
                            Logic._NOT(bA0)

                        Case Is = "$"
                            If Mid(expression, 3, expression.Length - 2) = "default" Then
                                bA0 = _IntToBytes(blockSize)
                                Logic._NOT(bA0)
                            ElseIf Mid(expression, 3, expression.Length - 2) = "block" Then
                                bA0 = currentBlock
                                Logic._NOT(bA0)
                            Else
                                bA0 = vars.Item(vars.Count - 1).Item(Mid(expression, 3, expression.Length - 2))
                                Logic._NOT(bA0)
                            End If

                        Case Else
                            'error
                    End Select

                Case Else
                    'error
            End Select

            Return bA0

        End If

    End Function

    Shared Sub execute_application(ByVal path As String, ByVal args As String)
        Process.Start(path, args)
    End Sub

    Shared Function execute_function(ByVal name As String, ByVal ParamArray parameters() As KeyValuePair(Of String, Byte())) As Byte()

        Dim funcNameParam As New Dictionary(Of String, Byte())
        funcNameParam.Add(name, Nothing)

        For i As Integer = 0 To parameters.Length - 1
            funcNameParam.Add(parameters.ElementAt(i).Key, parameters.ElementAt(i).Value)
        Next
        vars.Add(funcNameParam)

        'execute lines on function and return back to the main program =====================================================================================================

        Select Case name
            Case "rand"
                Dim l As Long = _BytesToInt(parameters(0).Value)
                vars(vars.Count - 1)(name) = builtInFunc._RAND(l)

            Case "trim"
                builtInFunc._TRIM(parameters(0).Value)
                vars(vars.Count - 1)(name) = parameters(0).Value

            Case "part"
                vars(vars.Count - 1)(name) = builtInFunc._PART(parameters(0).Value, _BytesToInt(parameters(1).Value), _BytesToInt(parameters(2).Value))

            Case "lengthof"
                vars(vars.Count - 1)(name) = builtInFunc._LENGTHOF(parameters(0).Value)

            Case Else

                Dim xn As XName = name

                Dim count As Integer = scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.Count

                For i As Integer = 0 To count - 1

                    'MsgBox(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Name.ToString)

                    Select Case scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Name.ToString
                        Case "condition"
                            Dim bA1 As Byte() = logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@select.ToString)
                            Dim succeededOnce As Boolean = False

                            For j As Integer = 0 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.Count - 1

                                Select Case scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Name
                                    Case "greaterthan"

                                        If condition._GREATERTHAN(bA1, _
                                                                  logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value.ToString)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then
                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "lesserthan"

                                        If condition._LESSERTHAN(bA1, logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@applicatione, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "equal"

                                        If condition._EQUAL(bA1, logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "notgreaterthan"

                                        If Not condition._GREATERTHAN(bA1, logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "notlesserthan"

                                        If Not condition._LESSERTHAN(bA1, logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "notequal"

                                        If Not condition._EQUAL(bA1, logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@value)) Then
                                            succeededOnce = True

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                    For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                        Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                       logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                        kvpC.Add(kvp)
                                                    Next

                                                    execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        Else

                                        End If

                                        If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).@break = "true" Then
                                            Exit For
                                        End If

                                    Case "else"

                                        If Not succeededOnce Then

                                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Name = "execute" Then

                                                If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application <> "" Then

                                                    execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@application, _
                                                                        scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@args)

                                                ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function <> "" Then

                                                    If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count > 1 Then

                                                        Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                                        For k As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.Count - 1 Step 1
                                                            Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Name.ToString, _
                                                                                                           logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).Attributes.ElementAt(k).Value))
                                                            kvpC.Add(kvp)
                                                        Next

                                                        execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function), kvpC.ToArray)
                                                    Else

                                                        execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Elements.ElementAt(j).Elements.ElementAt(0).@function))
                                                    End If

                                                Else
                                                    'error
                                                End If

                                            Else
                                                'error
                                            End If

                                        End If

                                End Select

                            Next j

                            ''===================================================
                        Case "define"

                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content = "" Then
                                variable_define(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name, _
                                                CInt(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@length))

                            Else
                                Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                For j As Integer = 2 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.Count - 1 Step 1
                                    Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Name.ToString, _
                                                                                   logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Value))
                                    kvpC.Add(kvp)
                                Next

                                variable_define(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name, _
                                                scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content, kvpC.ToArray)
                            End If

                        Case "set"
                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content = "" Then
                                variable_set(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name, _
                                                CInt(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@length))

                            Else
                                Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                For j As Integer = 2 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.Count - 1 Step 1
                                    Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Name.ToString, _
                                                                                   logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Value))
                                    kvpC.Add(kvp)
                                Next

                                variable_set(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name, _
                                                scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content, kvpC.ToArray)
                            End If

                        Case "dispose"
                            variable_dispose(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name)

                            ''==============================================
                        Case "display"
                            out_display(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content, _
                                        CInt(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@timeout))

                        Case "write"

                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content(0) = "#" Then

                                Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                For j As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.Count - 1 Step 1
                                    Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Name.ToString, _
                                                                                   logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Value))
                                    kvpC.Add(kvp)
                                Next

                                out_write(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content, kvpC.ToArray)
                            Else

                                out_write(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@content)
                            End If
                            ''=================================================
                        Case "execute"
                            Dim exe_application As XName = "application"
                            Dim exe_function As XName = "function"

                            If scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@application <> "" Then
                                Dim exe_args As XName = "args"
                                execute_application(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@application, _
                                                    scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@args)

                            ElseIf scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@function <> "" Then

                                Dim function_name As XName = "name"
                                Dim kvpC As New List(Of KeyValuePair(Of String, Byte()))

                                For j As Integer = 1 To scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.Count - 1 Step 1
                                    Dim kvp As New KeyValuePair(Of String, Byte())(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Name.ToString, _
                                                                                   logic_expression_simplify(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).Attributes.ElementAt(j).Value))
                                    kvpC.Add(kvp)
                                Next

                                execute_function(_getEx1stChar(scriptZ.bps.<bluepencil>.<file>.Elements(xn).Elements.ElementAt(i).@name), kvpC.ToArray)
                            Else
                                'error
                            End If

                            ''=================================================
                        Case "terminate"
                            command_terminate()

                        Case "pause"
                            scriptZ.paused = True

                        Case "resume"
                            scriptZ.paused = False

                        Case "delete"
                            command_delete()

                        Case "exit"
                            Dim bA1 As Byte() = vars(vars.Count - 1)(name)
                            vars.RemoveAt(vars.Count - 1)
                            Return bA1
                            Exit Function

                            ''================================================
                        Case Else

                    End Select

                Next i

        End Select

        'execute lines on function and return back to the main program =======================================================================================================

        Dim bA0 As Byte() = vars(vars.Count - 1)(name)
        vars.RemoveAt(vars.Count - 1)
        Return bA0
    End Function

    Shared Sub out_write(ByVal content As String, ByVal ParamArray parameters() As KeyValuePair(Of String, Byte()))

        Dim toWrite(-1) As Byte

        Dim stream0 As System.IO.Stream = System.IO.File.OpenWrite(selectedFile)
        stream0.Seek(selectedBlock * blockSize, IO.SeekOrigin.Begin)

        Select Case content(0)
            Case Is = "#"
                toWrite = execute_function(Mid(content, 2, content.Length - 1), parameters)

            Case Is = "$"
                toWrite = vars(vars.Count - 1)(Mid(content, 2, content.Length - 1))

            Case Else
                'error
        End Select

        If blockSize > toWrite.Length Then

            For i As Integer = 0 To blockSize - 1 Step toWrite.Length
                If i + toWrite.Length < blockSize Then
                    stream0.Write(toWrite, 0, toWrite.Length)
                Else
                    stream0.Write(toWrite, 0, blockSize Mod toWrite.Length)
                End If
            Next

        Else

            stream0.Write(toWrite, 0, blockSize)
        End If

        stream0.Close()

    End Sub

    Shared Sub out_write(ByVal content As String)

        On Error Resume Next

        Dim toWrite(-1) As Byte

        Dim stream0 As System.IO.Stream = System.IO.File.OpenWrite(selectedFile)
        stream0.SetLength(scriptZ.current_fz)
        stream0.Seek(selectedBlock * blockSize, IO.SeekOrigin.Begin)

        toWrite = logic_expression_simplify(content)

        If blockSize > toWrite.Length Then

            For i As Integer = 0 To blockSize - 1 Step toWrite.Length
                If i + toWrite.Length < blockSize Then
                    stream0.Write(toWrite, 0, toWrite.Length)
                Else
                    stream0.Write(toWrite, 0, blockSize Mod toWrite.Length)
                End If
            Next

        Else

            stream0.Write(toWrite, 0, blockSize)
        End If

        stream0.Close()

    End Sub

    Shared Sub out_display(ByVal content As String, ByVal timeout As Integer)

        frmMsg.txtMsg.Text = content
        If Not timeout = 0 Then
            frmMsg.tmrDisp.Enabled = True
            frmMsg.tmrDisp.Interval = timeout
        Else
            frmMsg.tmrDisp.Enabled = False
        End If

        frmMsg.ShowDialog()
    End Sub

    Shared Sub command_delete()

        If codeExec.scriptZ.wipe Then

            For i As Integer = 0 To scriptZ.lstfil.Count - 1
                If My.Computer.FileSystem.FileExists(scriptZ.lstfil(i).Key) Then
                    My.Computer.FileSystem.DeleteFile(scriptZ.lstfil(i).Key)
                End If
            Next

        Else

            If scriptZ.autorename Then

                Dim fileI As New System.IO.FileInfo(selectedFile)

                Dim dateRnda As New Random
                Dim yr As Integer = dateRnda.Next(1900, 2099)
                Dim mnth As Integer = dateRnda.Next(1, 13)
                Dim dt As Integer = dateRnda.Next(1, Date.DaysInMonth(yr, mnth) + 1)
                Dim dateZ As New Date(yr, mnth, dt, dateRnda.Next(0, 24), dateRnda.Next(0, 60), dateRnda.Next(0, 60))
                fileI.CreationTime = dateZ

reNewShred:     Dim newName As String = System.IO.Path.GetRandomFileName()

                If My.Computer.FileSystem.FileExists(System.IO.Path.GetDirectoryName(selectedFile) + "\" + newName) Then
                    GoTo reNewShred
                Else
                    My.Computer.FileSystem.RenameFile(selectedFile, newName)
                End If
                My.Computer.FileSystem.DeleteFile(System.IO.Path.GetDirectoryName(selectedFile) + "\" + newName)

            Else

                My.Computer.FileSystem.DeleteFile(selectedFile)
            End If

        End If

        command_terminate()
    End Sub

    Shared Sub command_terminate()
        scriptZ.stopped = True
    End Sub


    Shared Function _IntToBytes(ByVal number As Long) As Byte()

        Dim ret(7) As Byte

        For i As Integer = 7 To 0 Step -1
            ret(i) = CByte(number Mod 256)
            number \= 256
        Next i

        Return ret
    End Function

    Shared Function _BytesToInt(ByVal number As Byte()) As Long

        Dim ret As Long = 0

        For i As Integer = number.Length - 1 To 0 Step -1
            ret += number(i) * (256 ^ (number.Length - 1 - i))
        Next i

        Return ret
    End Function

    Shared Sub _setCurrentBlock() 'As Byte()
        Dim bufferx(blockSize - 1) As Byte

        Dim stream0 As System.IO.Stream = System.IO.File.OpenRead(selectedFile)

        stream0.Read(bufferx, 0, blockSize)
        currentBlock = bufferx ''
        stream0.Close()
        'Return bufferx
    End Sub

    Shared Function _getEx1stChar(ByVal input As String) As String

        Return Mid(input, 2, input.Length - 1)
    End Function

End Class
