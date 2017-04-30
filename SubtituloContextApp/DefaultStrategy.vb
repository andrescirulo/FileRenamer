Imports SubtituloContextApp

Public Class DefaultStrategy
    Implements IStrategy

    Public Sub InitLog() Implements IStrategy.InitLog
    End Sub

    Public Sub CloseLog() Implements IStrategy.CloseLog
    End Sub

    Public Sub WriteLog(message As String) Implements IStrategy.WriteLog
    End Sub

    Public Function DeboEjecutar() As Boolean Implements IStrategy.DeboEjecutar
        Return (My.Application.CommandLineArgs.Count = 1)
    End Function

    Public Function ObtenerDirectorio() As String Implements IStrategy.ObtenerDirectorio
        Return My.Application.CommandLineArgs(0)
    End Function
End Class
