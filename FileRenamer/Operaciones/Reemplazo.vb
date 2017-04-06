Public Class Reemplazo
    Inherits Operacion

    Public Overrides Property Nombre As String = "Reemplazar (inclusive en la extension)"
    Public origen As String
    Public destino As String

    Public Overrides Function ToString() As String
        Return "Reemplazar """ & origen & """" & " por """ & destino & """"
    End Function

    Public Overrides Sub armar(ByRef form As MainWindow)
        origen = form.txt_src_reemp.Text
        destino = form.txt_dest_reemp.Text
    End Sub

    Public Overrides Function operar(ByVal path As String) As String
        Return path.Replace(origen, destino)
    End Function


    Public Overrides Sub acomodarFormulario(ByRef form As MainWindow)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Visible
        form.lbl_op1.Content = "Reemplazar"
        form.lbl_op2.Content = "por"
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Visible
    End Sub

    Public Overrides Function GetDescripcion() As String
        Return ""
    End Function
End Class
