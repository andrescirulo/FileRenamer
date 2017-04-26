Public Class AgregarAlFinalDelNombre
    Inherits Operacion

    Public texto As String

    Public Overrides Property Nombre As String = Operaciones.AgregarAlFinalDelNombre_Nombre

    Public Overrides Function ToString() As String
        Dim txt = Operaciones.AgregarAlFinalDelNombre_ToString
        txt = txt.Replace("{1}", texto)
        Return txt
    End Function
    Public Overrides Sub Armar(ByRef form As RenombrarPage)
        texto = form.txt_src_reemp.Text
    End Sub

    Public Overrides Function Operar(ByVal path As String) As String
        Dim index As Integer = path.LastIndexOf(".")
        If index > 0 Then
            Dim nombre As String = path.Substring(0, index)
            Dim extension As String = path.Substring(index)
            Return (nombre & texto & extension)
        Else
            Return (path & texto)
        End If
    End Function

    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Hidden
        form.lbl_op1.Content = Language.renombrar_agregar
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Hidden
    End Sub


    Public Overrides Function GetDescripcion() As String
        Return Operaciones.AgregarAlFinalDelNombre_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New AgregarAlFinalDelNombre
    End Function
End Class
