Public Class RenombrarPreview
    Public Property PathCompleto As String
    Public Property NombreActual As String
    Public Property NombreNuevo As String

    Public Sub New(PathCompleto As String, Actual As String, Nuevo As String)
        Me.PathCompleto = PathCompleto
        NombreActual = Actual
        NombreNuevo = Nuevo
    End Sub
End Class
