Module Module1

    Private Function fraccionADecimal(fraccion As String) As Single
        ' 15/2
        Dim inicio As Byte = 0
        Dim primer, segundo As Single
        primer = fraccion.Substring(0, fraccion.IndexOf("/"))
        segundo = fraccion.Substring(fraccion.IndexOf("/") + 1)
        Return primer / segundo
    End Function

    Private Function reglaDeLaCadena(fx As String, gx As String) As String
        Dim salida, dgx As String
        salida = ""
        Dim coeficiente, potencia As Single
        Dim esPositiva As Boolean
        separar(gx, coeficiente, potencia, esPositiva)
        dgx = derivarTermino(coeficiente, potencia, esPositiva)
        If dgx(1) = "+" Then
            dgx = dgx.Substring(3)
        End If
        If fx = "cos" Or fx = "+cos" Then
            salida += "-sen(" & gx & ") * " & dgx
        ElseIf fx = "-cos" Then
            salida += "sen(" & gx & ") * " & dgx
        ElseIf fx = "sen" Or fx = "+sen" Then
            salida += "cos(" & gx & ") * " & dgx
        ElseIf fx = "-sen" Then
            salida += "-cos(" & gx & ") * " & dgx
        End If
        Return salida
    End Function

    Sub subTermino(termino As String, ByRef fx As String, ByRef gx As String)
        ' cos(2x)
        Dim j As Integer = termino.IndexOf("(")
        fx = termino.Substring(0, j)
        Dim i As Integer = termino.IndexOf(")", j + 1)
        gx = termino.Substring(j + 1, i - j - 1)

    End Sub

    Sub separar(termino As String, ByRef coeficiente As Single, ByRef potencia As Single, ByRef esPositiva As Boolean)
        Dim finCoeficiente As Short = 0
        Dim inicio As Short
        esPositiva = True
        If termino(0) = "-" Then
            esPositiva = False
            inicio = 1
        ElseIf termino(0) = "+" Then
            esPositiva = True
            inicio = 1
        Else
            inicio = 0
        End If
        For i = inicio To termino.Length - 1
            If Not Char.IsNumber(termino(i)) Then
                finCoeficiente = i - 1
                Exit For
            End If
        Next
        ' 2x => 0
        ' +20x^2 => 1
        ' x^2 => -1
        ' x^(1/2)
        Select Case finCoeficiente
            Case 0
                coeficiente = termino.Substring(inicio, 1)
            Case >= 1
                coeficiente = termino.Substring(inicio, finCoeficiente)
            Case -1
                coeficiente = 1
        End Select
        If termino.IndexOf("^") >= 0 And termino.IndexOf("(") < 0 Then
            potencia = termino.Substring(termino.IndexOf("^") + 1)
        ElseIf termino.IndexOf("x") >= 0 And termino.IndexOf("^") < 0 Then
            potencia = 1
        ElseIf termino.IndexOf("(") >= 0 Then
            Dim fraccion As String
            inicio = termino.IndexOf("(") + 1
            fraccion = termino.Substring(inicio, (termino.Length - inicio) - 1)
            potencia = fraccionADecimal(fraccion)
        Else
            potencia = 0
        End If
    End Sub


    Private Function derivarTermino(coeficiente As Single, potencia As Single, esPositiva As Boolean) As String
        Dim salida As String = ""
        Dim termino As Single
        If Not esPositiva Then
            If potencia < 0 Then
                salida += " + "
            Else
                salida += " - "
            End If
        ElseIf potencia >= 0 Then
            salida += " + "
        Else
            salida += " - "
        End If
        termino = Math.Abs(coeficiente * potencia)
        Select Case potencia
            Case 1
                Return salida & termino
            Case 2
                Return salida & termino & "x"
            Case 0
                Return ""
            Case Else
                If termino <> 1 Then
                    Return salida & termino & "x^" & (potencia - 1)
                Else
                    Return salida & "x^" & (potencia - 1)
                End If
        End Select
    End Function


    Private Function derivar(funcion As String) As String
        Dim salida As String = ""
        Dim terminos As String()
        Dim dx As String
        Dim esPositiva As Boolean
        Dim potencia, coeficiente As Single
        funcion = formatear(funcion)
        terminos = funcion.Split()
        For Each termino In terminos
            If termino.Contains("s") Then
                Dim fx, gx As String
                fx = ""
                gx = ""
                subTermino(termino, fx, gx)
                salida += reglaDeLaCadena(fx, gx) & " "
            Else
                separar(termino, coeficiente, potencia, esPositiva)
                dx = derivarTermino(coeficiente, potencia, esPositiva)
                salida += dx
            End If
        Next
        If Not salida.Trim().Length > 0 Then
            Return "0"
        End If
        If salida(1) = "+" Then
            Return salida.Substring(3)
        End If
        Return salida
    End Function


    Private Function formatear(funcion As String) As String
        ' (2x^2+2)/5
        Dim salida As String = ""
        Dim comienzaParentesis As Boolean = False
        Dim j As Byte = 0
        If funcion.StartsWith("(") Then
            comienzaParentesis = True
        End If
        For i = 0 To funcion.Length - 1
            If funcion.ElementAt(i) = "-" Or funcion(i) = "+" Or funcion(i) = ")" Then
                If Not (funcion(i - 1) = "^" Or funcion(i - 1) = "(") And Not comienzaParentesis Then
                    salida += funcion.Substring(j, i - j + 1) & " "
                    j = i
                ElseIf funcion(i) = ")" And comienzaParentesis Then
                    salida += funcion.Substring(j, i - j + 1) & " "
                    j = i + 1
                End If
            End If
        Next
        salida += funcion.Substring(j, funcion.Length - j)
        Return salida
    End Function
    Sub Main()
	' derivar es la funcion principal
        'Console.WriteLine(formatear("2x^2+2"))
        'Console.WriteLine(formatear("2x^2+2x"))
        'Console.WriteLine(formatear("cos(2x)^2+3x"))
        'Console.WriteLine(formatear("2x"))
        'Console.WriteLine(derivarTermino("10", "2", True))
        'Console.WriteLine(fraccionADecimal("1/2"))
        'Console.WriteLine(fraccionADecimal("10/2"))
        'Console.WriteLine("Derivadas")
        'Console.WriteLine(derivar("2x"))
        'Console.WriteLine(derivar("2x^4"))
        'Console.WriteLine(derivar("10"))
        'Console.WriteLine(derivar("3x^5+10x+50x^3"))
        'Console.WriteLine(derivar("x^(1/2)"))
        'Dim fx, gx As String
        'fx = ""
        'gx = ""
        'subTermino("cos(2x)", fx, gx)
        'Console.WriteLine(fx & " " & gx)
        'Console.WriteLine(derivar("cos(2x)+5"))
        'Console.WriteLine(derivar("(2x^2+2)/5"))
        Console.WriteLine(formatear("cos(2x)^3+35x"))
        Console.ReadLine()
    End Sub

End Module
