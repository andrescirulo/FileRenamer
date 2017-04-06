Public Class AgregarAlFinalDelNombre
    Inherits Operacion

    Public texto As String

    Public Overrides Property Nombre As String = "Agregar al final del nombre"

    Public Overrides Function ToString() As String
        Return "Agregar """ & texto & """ al final del nombre"
    End Function
    Public Overrides Sub armar(ByRef form As MainWindow)
        texto = form.txt_src_reemp.Text
    End Sub

    Public Overrides Function operar(ByVal path As String) As String
        Dim index As Integer = path.LastIndexOf(".")
        If index > 0 Then
            Dim nombre As String = path.Substring(0, index)
            Dim extension As String = path.Substring(index)
            Return (nombre & texto & extension)
        Else
            Return (path & texto)
        End If
    End Function

    Public Overrides Sub acomodarFormulario(ByRef form As MainWindow)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Hidden
        form.lbl_op1.Content = "Agregar"
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Hidden
    End Sub


    Public Overrides Function GetDescripcion() As String
        Return ""
    End Function
End Class
