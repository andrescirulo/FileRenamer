﻿Public Class EliminarEntreCaracteres
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.EliminarEntreCaracteres_Nombre
    Public inicio As String
    Public fin As String

    Public Overrides Function ToString() As String
        Dim txt As String = Operaciones.EliminarEntreCaracteres_ToString
        txt = txt.Replace("{1}", inicio)
        txt = txt.Replace("{2}", fin)
        Return txt
    End Function

    Public Overrides Sub AcomodarFormulario(ByRef form As RenombrarPage)
        form.lbl_op1.Visibility = Visibility.Visible
        form.lbl_op2.Visibility = Visibility.Visible
        form.lbl_op1.Content = Operaciones.Operacion_Entre
        form.lbl_op2.Content = Operaciones.Operacion_Y
        form.txt_src_reemp.Visibility = Visibility.Visible
        form.txt_dest_reemp.Visibility = Visibility.Visible
    End Sub

    Public Overrides Sub Armar(ByRef form As RenombrarPage)
        inicio = form.txt_src_reemp.Text
        fin = form.txt_dest_reemp.Text
    End Sub

    Public Overrides Function Operar(path As String) As String
        Dim pathTemp As String = path

        Dim posInicio As Integer
        Dim posFinal As Integer
        posInicio = InStr(pathTemp, inicio)
        posFinal = InStr(pathTemp, fin)
        Dim stringRes As String
        While posInicio > 0 And posFinal > 0
            If (posInicio < posFinal) Then
                stringRes = pathTemp.Substring(posInicio - 1, (posFinal - posInicio) + fin.Length)
                pathTemp = pathTemp.Replace(stringRes, "")
            End If

            posInicio = InStr(pathTemp, inicio)
            posFinal = InStr(pathTemp, fin)
        End While

        Dim index As Integer = pathTemp.LastIndexOf(".")
        If index > 0 Then
            Dim nombre As String = pathTemp.Substring(0, index)
            Dim extension As String = pathTemp.Substring(index)
            pathTemp = nombre.Trim & extension
        Else
            pathTemp = pathTemp.Trim
        End If
        Return pathTemp
    End Function

    Public Overrides Function GetDescripcion() As String
        Return Operaciones.EliminarEntreCaracteres_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New EliminarEntreCaracteres
    End Function
End Class
