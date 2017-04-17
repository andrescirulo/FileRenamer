Imports System.Text.RegularExpressions

Public Class SubtitulosManager

    Shared EXTENSIONES_SUBTITULOS() As String = {"*.srt", "*.sub"}
    Shared EXTENSIONES_VIDEOS() As String = {"*.avi", "*.mkv", "*.mp4"}

    Private Shared listaSubtitulos As List(Of Subtitulo)
    Private Shared listaVideos As List(Of Video)


    Public Shared Function AnalizarSubtitulos(srcFolder As String) As List(Of Video)
        Dim listaFinal As New List(Of Video)

        listaSubtitulos = New List(Of Subtitulo)
        Dim listaTmp As New List(Of String)
        listaTmp.AddRange(My.Computer.FileSystem.GetFiles(srcFolder, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_SUBTITULOS))
        listaSubtitulos.AddRange(ArmarSubtitulos(listaTmp))

        listaVideos = New List(Of Video)
        listaTmp = New List(Of String)
        listaTmp.AddRange(My.Computer.FileSystem.GetFiles(srcFolder, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_VIDEOS))
        listaVideos.AddRange(ArmarVideos(listaTmp))

        For Each video In listaVideos
            Dim subtitulo As Archivo = BuscarMejorMatch(video, listaSubtitulos)
            video.Subtitulo = subtitulo
            If Not (video.Subtitulo Is Nothing) Then
                listaFinal.Add(video)
                listaSubtitulos.Remove(video.Subtitulo)
            End If
        Next
        Return listaFinal
    End Function


    Private Shared Function BuscarMejorMatch(video As Archivo, listaSubtitulos As List(Of Subtitulo)) As Archivo

        Dim capituloRegEx As New Regex("s(?:\d){1,2}e(?:\d){1,2}")
        Dim nombre As String = video.NombreFiltrado
        Dim match As Match = capituloRegEx.Match(nombre)
        'SI ES UN CAPITULO DE UNA SERIE
        If (match.Success) Then

            Dim nombrePrincipal As String = nombre.Substring(0, match.Index).Trim
            For Each subt In listaSubtitulos
                If (CompararCapitulo(video, subt)) Then
                    Return subt
                End If
            Next
        Else
            For Each subt In listaSubtitulos
                If (CompararOtro(video, subt)) Then
                    Return subt
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Shared Function CompararCapitulo(video As Archivo, subtitulo As Archivo) As Boolean
        Dim capituloRegEx As New Regex("s(?:\d){1,2}e(?:\d){1,2}")
        Dim matchVideo As Match = capituloRegEx.Match(video.NombreFiltrado)
        Dim matchSubtitulo As Match = capituloRegEx.Match(subtitulo.NombreFiltrado)

        If (Not matchSubtitulo Is Nothing) Then
            Dim nombreVideo As String = video.NombreFiltrado.Substring(0, matchVideo.Index).Trim
            Dim capituloVideo As String = matchVideo.Value
            Dim nombreSubtitulo As String = subtitulo.NombreFiltrado.Substring(0, matchSubtitulo.Index).Trim
            Dim capituloSubtitulo As String = matchSubtitulo.Value

            If (capituloVideo = capituloSubtitulo And EsSimilar(nombreVideo, nombreSubtitulo)) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Shared Function EsSimilar(nombreVideo As String, nombreSubtitulo As String) As Boolean
        Dim palsVideo() As String = nombreVideo.Split(" ")
        Dim palsSubt() As String = nombreSubtitulo.Split(" ")
        Dim coincidencia As Integer = 0
        For Each palV In palsVideo
            For Each palS In palsSubt
                If (palV = palS) Then
                    coincidencia += 1
                End If
            Next
        Next
        If ((coincidencia * 1.0) / palsVideo.Count) > 0.65 Then
            Return True
        End If
        Return False
    End Function

    Private Shared Function CompararOtro(video As Archivo, subtitulo As Archivo) As Boolean
        Return EsSimilar(video.NombreFiltrado, subtitulo.NombreFiltrado)
    End Function

    Private Shared Function ArmarSubtitulos(files As List(Of String)) As IEnumerable(Of Subtitulo)
        Dim lista As New List(Of Subtitulo)

        For Each file In files
            Dim arch As New Subtitulo
            ArmarArchivo(file, arch)
            lista.Add(arch)
        Next
        Return lista
    End Function

    Private Shared Function ArmarVideos(files As List(Of String)) As IEnumerable(Of Video)
        Dim lista As New List(Of Video)

        For Each file In files
            Dim arch As New Video
            ArmarArchivo(file, arch)
            lista.Add(arch)
        Next
        Return lista
    End Function

    Private Shared Sub ArmarArchivo(file As String, ByRef arch As Archivo)
        arch.Path = file
        arch.Nombre = NombreCorto(file)
        arch.Nombre = arch.Nombre.Substring(0, arch.Nombre.LastIndexOf("."))
        arch.Extension = NombreCorto(file)
        arch.Extension = arch.Extension.Substring(arch.Extension.LastIndexOf(".") + 1)
        arch.NombreFiltrado = AplicarFiltrosNombre(arch.Nombre)
    End Sub

    Private Shared Function AplicarFiltrosNombre(nombre As String) As String
        Dim nomTmp As String = nombre.ToLower
        nomTmp = nomTmp.Replace(".", " ").Replace("_", " ")
        Dim partes() As String = nomTmp.Split(" ")

        Dim nomFinal As String = ""
        For Each parte In partes
            parte = QuitarNoClaves(parte)
            parte = FormatearEpisodios(parte)

            'EJEMPLO PARA OBTENER LOS RIPEADORES
            'x264-[\p{Ll}\p{Nd}]+

            If (parte.Trim <> "") Then
                nomFinal = nomFinal & parte & " "
            End If
        Next
        nomFinal = nomFinal.Trim
        Return nomFinal
    End Function

    Private Shared Function QuitarNoClaves(parte As String) As String
        Dim palabrasNoClave() As String = {"hdtv", "web", "webrip", "720p", "1080p", "-"}
        For Each pal In palabrasNoClave
            parte = parte.Replace(pal, "")
        Next
        Return parte
    End Function

    Private Shared Function FormatearEpisodios(parte As String) As String
        Dim regEx As New Regex("(?:\d){1,2}x(?:\d){1,2}")
        Dim final As String = ""
        If regEx.IsMatch(parte) Then
            Dim elems() As String = parte.Split("x")
            final = "s" & elems(0).PadLeft(2, "0") & "e" & elems(1).PadLeft(2, "0")
            Return final
        Else
            regEx = New Regex("s(?:\d){1,2}e(?:\d){1,2}")
            If regEx.IsMatch(parte) Then
                Dim elems() As String = parte.Replace("s", "").Split("e")
                final = "s" & elems(0).PadLeft(2, "0") & "e" & elems(1).PadLeft(2, "0")
                Return final
            Else
                regEx = New Regex("s(?:\d){1,2}xe(?:\d){1,2}")
                If regEx.IsMatch(parte) Then
                    Dim elems() As String = parte.Replace("s", "").Replace("xe", "e").Split("e")
                    final = "s" & elems(0).PadLeft(2, "0") & "e" & elems(1).PadLeft(2, "0")
                    Return final
                End If
            End If
        End If
        Return parte
    End Function

    Public Shared Sub RenombrarSubtitulos(ByRef listaVideos As List(Of Video))
        Dim elem As Video
        For Each elem In listaVideos
            Dim pathActual As String = elem.Subtitulo.Path
            Dim pathNuevo As String = pathActual.Replace(elem.Subtitulo.Nombre, elem.Nombre)
            If pathActual <> pathNuevo Then
                My.Computer.FileSystem.RenameFile(pathActual, NombreCorto(pathNuevo))
            End If
        Next
    End Sub
End Class
