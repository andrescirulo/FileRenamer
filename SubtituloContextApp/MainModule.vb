Imports FileRenamer

Module MainModule
    Sub Main()
        Dim Strategy As IStrategy = New DefaultStrategy
        Strategy.InitLog()

        If Strategy.DeboEjecutar Then
            Try
                Dim srcFolder As String = Strategy.ObtenerDirectorio
                Strategy.WriteLog(srcFolder)
                Dim videos As List(Of Video) = SubtitulosManager.AnalizarSubtitulos(srcFolder)
                Strategy.WriteLog(videos.Count & " videos")
                SubtitulosManager.RenombrarSubtitulos(videos)
            Catch ex As Exception
                Strategy.WriteLog("ERROR - " & ex.StackTrace.ToString)
            End Try
        Else
            Strategy.WriteLog("SIN CARPETA")
        End If
        Strategy.CloseLog()
    End Sub
End Module
