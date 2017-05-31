Public Class Termino

    Private termino As String
    Private esPositiva As Boolean
    Private coeficiente, potencia As Single

    Public Sub New(termino As String)
        Me.termino = termino
        Me.esPositiva = True
        separar()
    End Sub


    Private Function fraccionADecimal(fraccion As String) As Single
        Dim inicio As Byte = 0
        Dim primer, segundo As Single
        primer = fraccion.Substring(0, fraccion.IndexOf("/"))
        segundo = fraccion.Substring(fraccion.IndexOf("/") + 1)
        Return primer / segundo
    End Function


    Sub separar()
        Dim finCoeficiente As Short = 0
        Dim inicio As Short
        If termino(0) = "-" Then
            esPositiva = False
            inicio = 1
        ElseIf termino(0) = "+" Then
            esPositiva = True
            inicio = 1
        Else
            inicio = 0
        End If
        If termino = "x" Then
            coeficiente = 1
        Else
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
            Dim c As String
            If finCoeficiente <= 0 Then
                c = termino.Substring(inicio, 1)
            ElseIf inicio = 0 Then
                c = termino.Substring(inicio, finCoeficiente + 1)
            Else
                c = termino.Substring(inicio, finCoeficiente)

            End If
            If c = "x" Or c.Length = 0 Then
                coeficiente = 1
            Else
                coeficiente = c
            End If
        End If
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



    Public Function derivar() As String
        Dim salida As String = ""
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

End Class
