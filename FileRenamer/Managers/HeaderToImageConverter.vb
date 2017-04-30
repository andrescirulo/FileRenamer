Imports System.Globalization

<ValueConversion(GetType(String), GetType(Boolean))>
Public Class HeaderToImageConverter
    Implements IValueConverter

    Public Shared Instance As New HeaderToImageConverter()

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim valor As String = value
        Try
            If (valor.Contains("\")) Then
                Dim Uri As New Uri("pack://application:,,,/diskdrive.png")
                Dim source As New BitmapImage(Uri)
                Return source
            Else
                Dim Uri As New Uri("pack://application:,,,/folder.png")
                Dim source As New BitmapImage(Uri)
                Return source
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotSupportedException("Cannot convert back")
    End Function
End Class

