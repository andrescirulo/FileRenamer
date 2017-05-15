Imports FileRenamer

Public Class SubtitulosPage
    Implements FoldersTreeViewListener

    Dim listaVideos As List(Of Video)
    Dim listaSubtitulos As List(Of Subtitulo)

    Dim dummyNode As Object = Nothing

    Private Sub SubtitulosPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Dim carpetaInicial As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        carpetaInicial = carpetaInicial.Replace("Mis Documentos", "Descargas")
        carpetaInicial = carpetaInicial.Replace("My Documents", "Downloads")

        If Not My.Computer.FileSystem.DirectoryExists(carpetaInicial) Then
            carpetaInicial = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        End If

        lbl_carpeta.Content = FileRenamer.Language.renombrar_sin_seleccionar
        lstVideos.Items.Clear()

        tree_carpetas.AddListener(Me)
    End Sub

    Public Sub OnSelectedItemChanged(FilePath As String) Implements FoldersTreeViewListener.OnSelectedItemChanged
        lbl_carpeta.Content = FilePath

        If lbl_carpeta.Content Is Nothing Then
            Return
        End If

        If Not (lbl_carpeta.Content.EndsWith("\")) Then
            lbl_carpeta.Content = lbl_carpeta.Content & "\"
        End If

        CargarListaVideos(lbl_carpeta.Content)
    End Sub

    Private Sub CargarListaVideos(carpeta As String)
        Dim listaVideos As List(Of Video) = SubtitulosManager.AnalizarSubtitulos(lbl_carpeta.Content)
        lstVideos.Items.Clear()
        For Each elem In listaVideos
            lstVideos.Items.Add(New FilaSubtitulo(elem))
        Next
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnMarcar.InnerButtonClick
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            elem.SetSelected(True)
        Next
    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnDesmarcar.InnerButtonClick
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            elem.SetSelected(False)
        Next
    End Sub

    Private Sub btnRenombrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRenombrar.InnerButtonClick
        Dim vids As New List(Of Video)
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            If (elem.IsSelected) Then
                vids.Add(elem.Video)
            End If
        Next
        'HAGO VALIDACIONES
        If (vids.Count = 0) Then
            MsgBox(FileRenamer.Language.subtitulos_error_archivos)
            Return
        End If

        SubtitulosManager.RenombrarSubtitulos(vids)
        MsgBox(vids.Count & " Subtitulos Renombrados!")
        CargarListaVideos(lbl_carpeta.Content)
    End Sub

End Class
