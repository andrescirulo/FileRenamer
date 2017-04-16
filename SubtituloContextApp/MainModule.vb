Imports System.IO
Imports FileRenamer

Module MainModule
    Sub Main()
        Dim log As New StreamWriter("D:\Log.txt")
        If My.Application.CommandLineArgs.Count = 1 Then
            Dim srcFolder As String = My.Application.CommandLineArgs(0)
            log.WriteLine(srcFolder)
            Dim videos As List(Of Video) = SubtitulosManager.AnalizarSubtitulos(srcFolder)
            log.WriteLine(videos.Count & " videos")
            SubtitulosManager.RenombrarSubtitulos(videos)
        Else
            log.WriteLine("SIN CARPETA")
        End If
        log.Close()
    End Sub
End Module
