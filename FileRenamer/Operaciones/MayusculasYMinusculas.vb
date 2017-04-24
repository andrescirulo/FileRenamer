Public Class MayusculasYMinusculas
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.MayusculasYMinusculas_Nombre

    Public Overrides Function ToString() As String
        Return Operaciones.MayusculasYMinusculas_ToString
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
        Dim caracteres() As String = {" ", "."}
        Dim index As Integer = path.LastIndexOf(".")
        Dim texto As String = ""
        Dim extension As String = ""
        If index > 0 Then
            texto = path.Substring(0, index)
            extension = path.Substring(index)
        Else
            texto = path
        End If

        For Each caracter In caracteres
            Dim tmpTexto As String = ""
            Dim palabras() As String = texto.Split(caracter)
            For Each pal In palabras
                tmpTexto = tmpTexto & pal.Substring(0, 1).ToUpper & pal.Substring(1).ToLower & caracter
            Next
            tmpTexto = tmpTexto.Substring(0, tmpTexto.Length - 1)
            texto = tmpTexto
        Next
        Return texto & extension
    End Function

    Public Overrides Function GetDescripcion() As String
        Return "Convierte el texto a mayúsculas y minúsculas separando por palabra"
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New MayusculasYMinusculas
    End Function
End Class
