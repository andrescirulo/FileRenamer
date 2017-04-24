Public Class TodoAMinusculas
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.TodoAMinusculas_Nombre

    Public Overrides Function ToString() As String
        Return Operaciones.TodoAMinusculas_ToString
    End Function

    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Hidden
        form.lbl_op2.Visibility = Visibility.Hidden
        form.txt_dest_reemp.Visibility = Visibility.Hidden
        form.txt_src_reemp.Visibility = Visibility.Hidden
    End Sub

    Public Overrides Sub Armar(ByRef form As RenombrarPage)
    End Sub

    Public Overrides Function Operar(path As String) As String
        Return path.ToLower
    End Function

    Public Overrides Function GetDescripcion() As String
        Return "Convierte todo el nombre del archivo a minúsculas"
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New TodoAMinusculas
    End Function
End Class
