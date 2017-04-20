Imports System.IO
Imports FileRenamer

Module MainModule
    Sub Main()
        Dim log As New StreamWriter("D:\Log.txt")
        If My.Application.CommandLineArgs.Count = 1 Then
            Try

                Dim srcFolder As String = My.Application.CommandLineArgs(0)
                log.WriteLine(srcFolder)
                Dim videos As List(Of Video) = SubtitulosManager.AnalizarSubtitulos(srcFolder)
                log.WriteLine(videos.Count & " videos")
                SubtitulosManager.RenombrarSubtitulos(videos)
            Catch ex As Exception
                log.WriteLine("ERROR - " & ex.StackTrace.ToString)
            End Try
        Else
            log.WriteLine("SIN CARPETA")
        End If
        log.Close()
    End Sub
End Module
