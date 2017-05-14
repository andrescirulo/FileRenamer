Imports System.Globalization

<ValueConversion(GetType(String), GetType(Boolean))>
Public Class HeaderToImageConverter
    Implements IValueConverter

    Public Shared Instance As New HeaderToImageConverter()

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim valor As String = value
        Try
            If EnEspeciales(valor) Then
                Dim Uri As New Uri("pack://application:,,,/Iconos/" & GetIconoEspecial(valor))
                Dim source As New BitmapImage(Uri)
                Return source
            ElseIf (valor.Contains("\")) Then
                Dim Uri As New Uri("pack://application:,,,/Iconos/diskdrive.png")
                Dim source As New BitmapImage(Uri)
                Return source
            Else
                Dim Uri As New Uri("pack://application:,,,/Iconos/folder.png")
                Dim source As New BitmapImage(Uri)
                Return source
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetIconoEspecial(valor As String) As String
        Select Case valor
            Case Carpetas.Descargas
                Return "descargas.png"
            Case Carpetas.Documentos
                Return "documentos.png"
            Case Carpetas.Equipo
                Return "equipo.png"
            Case Carpetas.Escritorio
                Return "escritorio.png"
            Case Carpetas.Imagenes
                Return "imagenes.png"
            Case Carpetas.Musica
                Return "musica.png"
            Case Carpetas.Videos
                Return "videos.png"
        End Select
        Return Nothing
    End Function

    Private Function EnEspeciales(valor As String) As Boolean
        Return valor = Carpetas.Descargas Or
            valor = Carpetas.Documentos Or
            valor = Carpetas.Equipo Or
            valor = Carpetas.Escritorio Or
            valor = Carpetas.Imagenes Or
            valor = Carpetas.Musica Or
            valor = Carpetas.Videos
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotSupportedException("Cannot convert back")
    End Function
End Class

