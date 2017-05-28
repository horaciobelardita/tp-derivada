<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFuncion = New System.Windows.Forms.TextBox()
        Me.txtDerivada = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "f(x) = "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(49, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "f'(x) = "
        '
        'txtFuncion
        '
        Me.txtFuncion.Location = New System.Drawing.Point(93, 30)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.Size = New System.Drawing.Size(292, 20)
        Me.txtFuncion.TabIndex = 1
        '
        'txtDerivada
        '
        Me.txtDerivada.Location = New System.Drawing.Point(93, 56)
        Me.txtDerivada.Name = "txtDerivada"
        Me.txtDerivada.ReadOnly = True
        Me.txtDerivada.Size = New System.Drawing.Size(292, 20)
        Me.txtDerivada.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(193, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Derivar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 137)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtDerivada)
        Me.Controls.Add(Me.txtFuncion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Calculo Derivada"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFuncion As TextBox
    Friend WithEvents txtDerivada As TextBox
    Friend WithEvents Button1 As Button
End Class
