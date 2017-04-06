Public Class SoloEntreCaracteres
    Inherits Operacion

    Public Overrides Property Nombre As String = "Solo entre caracteres (sin incluir la extension)"

    Public inicio As String
    Public fin As String

    Public Overrides Function ToString() As String
        Return "Solo texto entre """ & inicio & """ y """ & fin & """"
    End Function

    Public Overrides Sub acomodarFormulario(ByRef form As MainWindow)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Visible
        form.lbl_op1.Content = "Entre"
        form.lbl_op2.Content = "y"
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Visible
    End Sub

    Public Overrides Sub armar(ByRef form As MainWindow)
        inicio = form.txt_src_reemp.Text
        fin = form.txt_dest_reemp.Text
    End Sub

    Public Overrides Function operar(path As String) As String
        Dim pathTemp As String = path

        Dim posInicio As Integer
        Dim posFinal As Integer
        posInicio = InStr(pathTemp, inicio) + inicio.Length
        posFinal = InStr(posInicio, pathTemp, fin)

        'Dim stringRes As String
        While posInicio > 0
            If (posInicio < posFinal) Then
                pathTemp = pathTemp.Substring(posInicio - 1, (posFinal - posInicio))
                'pathTemp = pathTemp.Replace(stringRes, "")
            End If

            posInicio = InStr(pathTemp, inicio)
            If posInicio > 0 Then
                posFinal = InStr(posInicio, pathTemp, fin)
            End If
        End While

        Dim index As Integer = path.LastIndexOf(".")
        If index > 0 Then
            Dim nombre As String = pathTemp
            Dim extension As String = path.Substring(index)
            pathTemp = nombre.Trim & extension
        Else
            pathTemp = pathTemp.Trim
        End If
        Return pathTemp
    End Function

    Public Overrides Function GetDescripcion() As String
        Return ""
    End Function
End Class
