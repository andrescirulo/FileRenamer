﻿Public Class SoloEntreCaracteres
    Inherits Operacion

    Public Overrides Property Nombre As String = Operaciones.SoloEntreCaracteres_Nombre

    Public inicio As String
    Public fin As String

    Public Overrides Function ToString() As String
        Dim txt As String = Operaciones.SoloEntreCaracteres_ToString
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
        posInicio = InStr(pathTemp, inicio) + inicio.Length
        posFinal = InStr(posInicio, pathTemp, fin)

        While posInicio > 0
            If (posInicio < posFinal) Then
                pathTemp = pathTemp.Substring(posInicio - 1, (posFinal - posInicio))
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
        Return Operaciones.SoloEntreCaracteres_Descripcion
    End Function

    Public Overrides Function GetNewInstance() As Operacion
        Return New SoloEntreCaracteres
    End Function
End Class
