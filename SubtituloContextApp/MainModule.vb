Imports FileRenamer

Module MainModule
    Sub Main()
        Dim Strategy As IStrategy = New DefaultStrategy
        'Dim Strategy As IStrategy = New DefaultLogStrategy
        'Dim Strategy As IStrategy = New DebugStrategy
        Strategy.InitLog()

        If Strategy.DeboEjecutar Then
            Try
                Dim srcFolder As String = CorregirDirectorio(Strategy.ObtenerDirectorio)
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

    Private Function CorregirDirectorio(path As String) As String
        Dim dest As String = path
        dest = dest.Replace("""", "")
        If Not dest.EndsWith("\") Then
            dest &= "\"
        End If
        Return dest
    End Function
End Module
