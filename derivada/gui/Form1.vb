Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim funcion, derivada As String
        funcion = txtFuncion.Text
        derivada = derivar(funcion)
        txtDerivada.Text = derivada
        btnGraficar.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFuncion.Select()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnGraficar.Click
        Me.Chart1.Series.Clear()
        Me.Chart1.Series.Add("f(x)")
        Me.Chart1.Series.Add("f'(x)")
        Me.Chart1.Series(0).ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Me.Chart1.Series(0).BorderWidth = 2
        Me.Chart1.Series(1).ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Me.Chart1.Series(1).BorderWidth = 2

        Dim puntosX As New ArrayList
        Dim puntosYFuncion As New ArrayList
        Dim puntosYDerivada As New ArrayList
        Dim x As Single = -30
        Dim nroPuntos As Short = 1000
        For i = 0 To nroPuntos
            Dim puntoYFuncion As Single = stringAFuncion(txtFuncion.Text, x)
            Dim puntoYDerivada As Single = stringAFuncion(txtDerivada.Text, x)
            puntosYFuncion.Add(puntoYFuncion)
            puntosYDerivada.Add(puntoYDerivada)
            puntosX.Add(x)
            x += 0.1
        Next

        For i = 0 To puntosX.Count - 1
            Me.Chart1.Series(0).Points.AddXY(puntosX(i), puntosYFuncion(i))
            Me.Chart1.Series(1).Points.AddXY(puntosX(i), puntosYDerivada(i))
        Next

    End Sub
End Class
