Public Class Video
    Inherits Archivo

    Public Subtitulo As Subtitulo

    Public Overrides Function ToString() As String
        If (Subtitulo Is Nothing) Then
            Return Nombre & "." & Extension
        Else
            Return Nombre & "." & Extension & " --> " & Subtitulo.Nombre & "." & Subtitulo.Extension
        End If

    End Function
End Class
