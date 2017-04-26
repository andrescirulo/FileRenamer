Public Class AgregarPadding
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.AgregarPadding_Nombre
    Dim caracter As String
    Dim cantidad As Integer

    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Visible
        form.lbl_op1.Content = Operaciones.AgregarPadding_Caracter
        form.lbl_op2.Content = Operaciones.AgregarPadding_Cantidad
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Visible
    End Sub

    Public Overrides Sub Armar(ByRef form As RenombrarPage)
        caracter = form.txt_src_reemp.Text
        cantidad = form.txt_dest_reemp.Text
    End Sub

    Public Overrides Function Operar(path As String) As String
        Dim index As Integer = path.LastIndexOf(".")
        Dim res As String = ""
        If index > 0 Then
            Dim nombre As String = path.Substring(0, index)
            Dim extension As String = path.Substring(index + 1)
            While nombre.Length < cantidad
                nombre = caracter & nombre
            End While
            res = nombre & "." & extension
        Else
            res = path
            While res.Length < cantidad
                res = caracter & res
            End While
        End If
        Return res

    End Function

    Public Overrides Function ToString() As String
        Dim txt As String = Operaciones.AgregarAlPrincipio_ToString
        txt = txt.Replace("{1}", caracter)
        txt = txt.Replace("{2}", cantidad)
        Return txt
    End Function

    Public Overrides Function GetDescripcion() As String
        Return Operaciones.AgregarPadding_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New AgregarPadding
    End Function
End Class
