Public Class AgregarAlPrincipio
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.AgregarAlPrincipio_Nombre
    Public texto As String

    Public Overrides Function ToString() As String
        Dim txt As String = Operaciones.AgregarAlPrincipio_ToString
        txt = txt.Replace("{1}", texto)
        Return txt
    End Function
    Public Overrides Sub Armar(ByRef form As RenombrarPage)
        texto = form.txt_src_reemp.Text
    End Sub

    Public Overrides Function Operar(ByVal path As String) As String
        Return (texto & path)
    End Function

    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Hidden
        form.lbl_op1.Content = Language.renombrar_agregar
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Hidden
    End Sub

    Public Overrides Function GetDescripcion() As String
        Return Operaciones.AgregarAlPrincipio_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New AgregarAlPrincipio
    End Function
End Class
