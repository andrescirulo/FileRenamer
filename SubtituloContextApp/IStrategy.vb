Public Interface IStrategy
    Function DeboEjecutar() As Boolean
    Function ObtenerDirectorio() As String
    Sub InitLog()
    Sub CloseLog()
    Sub WriteLog(message As String)
End Interface
