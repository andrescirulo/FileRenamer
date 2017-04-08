Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports FileRenamer

Public Class SubtitulosPage
    Dim EXTENSIONES_SUBTITULOS() As String = {"*.srt", "*.sub"}
    Dim EXTENSIONES_VIDEOS() As String = {"*.avi", "*.mkv", "*.mp4"}


    Dim oFileD As OpenFileDialog
    Dim oFolderD As FolderBrowserDialog

    Dim listaVideos As List(Of Archivo)
    Dim listaSubtitulos As List(Of Archivo)

    Private Sub SubtitulosPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        oFileD = New OpenFileDialog
        oFileD.Multiselect = True
        oFolderD = New FolderBrowserDialog

        Dim carpetaInicial As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        carpetaInicial = carpetaInicial.Replace("Mis Documentos", "Descargas")
        carpetaInicial = carpetaInicial.Replace("My Documents", "Descargas")

        If Not My.Computer.FileSystem.DirectoryExists(carpetaInicial) Then
            carpetaInicial = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        End If
        oFolderD.SelectedPath = carpetaInicial

    End Sub


    Private Sub btn_carpeta_Click(sender As Object, e As RoutedEventArgs) Handles btn_carpeta.Click
        If (oFolderD.ShowDialog() = Forms.DialogResult.OK) Then
            lbl_carpeta.Content = oFolderD.SelectedPath

            If Not (lbl_carpeta.Content.EndsWith("\")) Then
                lbl_carpeta.Content = lbl_carpeta.Content & "\"
            End If

            listaSubtitulos = New List(Of Archivo)
            Dim listaTmp As New List(Of String)
            listaTmp.AddRange(My.Computer.FileSystem.GetFiles(lbl_carpeta.Content, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_SUBTITULOS))
            listaSubtitulos.AddRange(ArmarArchivos(listaTmp))

            listaVideos = New List(Of Archivo)
            listaTmp = New List(Of String)
            listaTmp.AddRange(My.Computer.FileSystem.GetFiles(lbl_carpeta.Content, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_VIDEOS))
            listaVideos.AddRange(ArmarArchivos(listaTmp))


            For Each video In listaVideos
                Dim subtitulo As Archivo = BuscarMejorMatch(video, listaSubtitulos)
                If (Not subtitulo Is Nothing) Then
                    Console.Out.WriteLine(video.Nombre, subtitulo.Nombre)
                End If
            Next

        End If
    End Sub

    Private Function BuscarMejorMatch(video As Archivo, listaSubtitulos As List(Of Archivo)) As Archivo

        Dim capituloRegEx As New Regex("s(?:\d){1,2}e(?:\d){1,2}")
        Dim nombre As String = video.NombreFiltrado
        Dim match As Match = capituloRegEx.Match(nombre)
        'SI ES UN CAPITULO DE UNA SERIE
        If (Not match Is Nothing) Then
            Dim nombrePrincipal As String = nombre.Substring(0, match.Index).Trim
            For Each subt In listaSubtitulos
                Dim nom = subt.NombreFiltrado
                match = capituloRegEx.Match(nom)
                If (Not match Is Nothing) Then
                    Dim nombreSub As String = nom.Substring(0, match.Index).Trim
                    If (nombreSub = nombrePrincipal) Then
                        Return subt
                    End If
                End If
            Next
        Else

        End If
        Return Nothing
    End Function

    Private Function ArmarArchivos(files As List(Of String)) As IEnumerable(Of Archivo)
        Dim lista As New List(Of Archivo)
        For Each file In files
            Dim arch As New Archivo
            arch.Path = file
            arch.Nombre = NombreCorto(file)
            arch.Nombre = arch.Nombre.Substring(0, arch.Nombre.LastIndexOf("."))
            arch.Extension = NombreCorto(file)
            arch.Extension = arch.Extension.Substring(arch.Extension.LastIndexOf(".") + 1)
            arch.NombreFiltrado = AplicarFiltrosNombre(arch.Nombre)
            lista.Add(arch)
        Next
        Return lista
    End Function

    Private Function AplicarFiltrosNombre(nombre As String) As String
        Dim nomTmp As String = nombre.ToLower
        nomTmp = nomTmp.Replace(".", " ")
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

    Private Function QuitarNoClaves(parte As String) As String
        Dim palabrasNoClave() As String = {"hdtv", "webrip", "720p", "1080p", "-"}
        For Each pal In palabrasNoClave
            parte = parte.Replace(pal, "")
        Next
        Return parte
    End Function

    Private Function FormatearEpisodios(parte As String) As String
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
            End If
        End If
        Return parte
    End Function

End Class
