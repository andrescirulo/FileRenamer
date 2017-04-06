Public Class AgregarAlPrincipio
    Inherits Operacion

    Public Overrides Property Nombre As String = "Agregar al principio del nombre"
    Public texto As String

    Public Overrides Function ToString() As String
        Return "Agregar """ & texto & """ al principio del nombre"
    End Function
    Public Overrides Sub armar(ByRef form As MainWindow)
        texto = form.txt_src_reemp.Text
    End Sub

    Public Overrides Function operar(ByVal path As String) As String
        Return (texto & path)
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
