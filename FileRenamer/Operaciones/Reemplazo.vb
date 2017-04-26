Public Class Reemplazo
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.Reemplazo_Nombre
    Public origen As String
    Public destino As String

    Public Overrides Function ToString() As String
        Dim txt As String = Operaciones.Reemplazo_ToString
        txt = txt.Replace("{1}", origen)
        txt = txt.Replace("{2}", destino)
        Return txt
    End Function

    Public Overrides Sub Armar(ByRef form As RenombrarPage)
        origen = form.txt_src_reemp.Text
        destino = form.txt_dest_reemp.Text
    End Sub

    Public Overrides Function Operar(ByVal path As String) As String
        Return path.Replace(origen, destino)
    End Function


    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Visible
        form.lbl_op1.Content = Language.renombrar_reemplazar
        form.lbl_op2.Content = Language.renombrar_por
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Visible
    End Sub

    Public Overrides Function GetDescripcion() As String
        Return Operaciones.Reemplazo_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New Reemplazo
    End Function
End Class
