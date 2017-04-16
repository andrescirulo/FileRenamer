Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports FileRenamer
Imports Microsoft.Win32

Public Class SubtitulosPage

    Dim oFileD As Forms.OpenFileDialog
    Dim oFolderD As FolderBrowserDialog

    Dim listaVideos As List(Of Video)
    Dim listaSubtitulos As List(Of Subtitulo)

    Private Sub SubtitulosPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        oFileD = New Forms.OpenFileDialog
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

            'listaSubtitulos = New List(Of Subtitulo)
            'Dim listaTmp As New List(Of String)
            'listaTmp.AddRange(My.Computer.FileSystem.GetFiles(lbl_carpeta.Content, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_SUBTITULOS))
            'listaSubtitulos.AddRange(ArmarSubtitulos(listaTmp))

            'listaVideos = New List(Of Video)
            'listaTmp = New List(Of String)
            'listaTmp.AddRange(My.Computer.FileSystem.GetFiles(lbl_carpeta.Content, FileIO.SearchOption.SearchTopLevelOnly, EXTENSIONES_VIDEOS))
            'listaVideos.AddRange(ArmarVideos(listaTmp))

            'For Each video In listaVideos
            '    Dim subtitulo As Archivo = BuscarMejorMatch(video, listaSubtitulos)
            '    video.Subtitulo = subtitulo
            '    If Not (video.Subtitulo Is Nothing) Then
            '        lstVideos.Items.Add(video)
            '    End If
            'Next
            Dim listaVideos As List(Of Video) = SubtitulosManager.AnalizarSubtitulos(lbl_carpeta.Content)
            lstVideos.Items.Clear()
            For Each elem In listaVideos
                lstVideos.Items.Add(elem)
            Next

        End If
    End Sub


    Private Sub btnMarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnMarcar.Click
        lstVideos.SelectAll()
    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnDesmarcar.Click
        lstVideos.UnselectAll()
    End Sub

    Private Sub btnRenombrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRenombrar.Click
        'Dim elem As Video
        'For Each elem In lstVideos.SelectedItems
        '    Dim pathActual As String = elem.Subtitulo.Path
        '    Dim pathNuevo As String = pathActual.Replace(elem.Subtitulo.Nombre, elem.Nombre)
        '    If pathActual <> pathNuevo Then
        '        My.Computer.FileSystem.RenameFile(pathActual, NombreCorto(pathNuevo))
        '    End If
        'Next
        SubtitulosManager.RenombrarSubtitulos(lstVideos.SelectedItems)
        MsgBox("Listo!")
    End Sub

End Class
