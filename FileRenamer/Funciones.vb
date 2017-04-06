Module Funciones
    Public Function NombreCorto(ByVal path As String) As String
        Return path.Substring(path.LastIndexOf("\") + 1)
    End Function

    Public Function GetDirectorio(ByVal path As String) As String
        Return path.Substring(0, path.LastIndexOf("\") + 1)
    End Function
End Module
