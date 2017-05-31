Public Class Funcion
    Private funcion As String

    Public Sub New(fx As String)
        funcion = fx
        formatear()

    End Sub

    Private Sub formatear()
        ' 3x^-2+10x^(1/2)-5
        Dim salida As String = ""
        Dim j As Byte = 0
        For i = 0 To funcion.Length - 1
            If (funcion(i) = "-" Or funcion(i) = "+") And i > 0 Then
                If funcion(i - 1) <> "^" Then
                    salida += funcion.Substring(j, i - j) & " "
                    j = i
                End If
            End If
        Next
        ' ultimo termino
        salida += funcion.Substring(j, funcion.Length - j)
        Me.funcion = salida

    End Sub

    Public Function derivar() As String
        Dim salida As String = ""
        Dim terminos As String()
        Dim dx As String
        terminos = funcion.Split()
        For Each termino In terminos
            Dim t As New Termino(termino)
            dx = t.derivar()
            salida += dx
        Next
        If Not salida.Trim().Length > 0 Then
            Return "0"
        End If
        ' para darle formato a la salida unicamente
        If salida(1) = "+" Then
            Return salida.Substring(3)
        ElseIf salida(1) = "-" Then
            Return salida(1) & salida.Substring(3)
        ElseIf salida(0) = " " Then
            Return salida.Substring(1)
        End If
        Return salida
    End Function

    Public Function evaluar(x As Single) As Single
        Dim fx As String = Me.funcion
        Dim i As Integer = 0
        While i < fx.Length
            If fx(i) = "x" Then
                fx = fx.Insert(i, "*")
                i = i + 1
            End If
            i += 1
        End While
        fx = fx.Replace("x", "(" & x.ToString & ")")
        fx = fx.Replace(",", ".")
        Dim sc As New MSScriptControl.ScriptControl
        sc.Language = "VBScript"
        Return Convert.ToSingle(sc.Eval(fx))
    End Function

End Class
