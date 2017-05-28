Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim funcion, derivada As String
        funcion = txtFuncion.Text
        derivada = derivar(funcion)
        txtDerivada.Text = derivada
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFuncion.Select()
    End Sub
End Class
