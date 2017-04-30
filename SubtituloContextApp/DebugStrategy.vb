Imports System.IO

Public Class DebugStrategy
    Implements IStrategy

    Dim log As StreamWriter

    Public Sub InitLog() Implements IStrategy.InitLog
        log = New StreamWriter("D:\Log.txt")
    End Sub

    Public Sub CloseLog() Implements IStrategy.CloseLog
        log.Close()
    End Sub

    Public Sub WriteLog(message As String) Implements IStrategy.WriteLog
        Dim linea As String
        linea = Now.ToString("dd-MM-yyyy HH:mm:ss")
        linea = linea & " - " & message
        Try
            log.WriteLine(linea)
        Catch ex As Exception
        End Try
    End Sub

    Public Function DeboEjecutar() As Boolean Implements IStrategy.DeboEjecutar
        Return True
    End Function

    Public Function ObtenerDirectorio() As String Implements IStrategy.ObtenerDirectorio
        Return "G:\"
    End Function
End Class
