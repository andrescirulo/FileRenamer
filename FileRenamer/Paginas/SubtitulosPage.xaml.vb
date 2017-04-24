﻿Public Class SubtitulosPage
    Dim listaVideos As List(Of Video)
    Dim listaSubtitulos As List(Of Subtitulo)

    Dim dummyNode As Object = Nothing

    Private Sub SubtitulosPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Dim carpetaInicial As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        carpetaInicial = carpetaInicial.Replace("Mis Documentos", "Descargas")
        carpetaInicial = carpetaInicial.Replace("My Documents", "Descargas")

        If Not My.Computer.FileSystem.DirectoryExists(carpetaInicial) Then
            carpetaInicial = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        End If

        tree_carpetas.Items.Clear()
        For Each drive In ObtenerUnidades()
            Dim item As New TreeViewItem()
            item.Header = drive.RootDirectory.FullName
            If ((Not drive.VolumeLabel Is Nothing) AndAlso drive.VolumeLabel <> "") Then
                item.Header = item.Header & " - " & drive.VolumeLabel
            End If
            item.Tag = drive.RootDirectory.FullName
            item.FontWeight = FontWeights.Normal
            item.Items.Add(dummyNode)
            AddHandler item.Expanded, AddressOf OnFolderExpanded
            tree_carpetas.Items.Add(item)
        Next

    End Sub

    Private Sub OnFolderExpanded(sender As Object, e As RoutedEventArgs)
        Dim item As TreeViewItem = sender
        If (item.Items.Count = 1 AndAlso item.Items(0) = dummyNode) Then
            item.Items.Clear()
            Try
                For Each dire In ObtenerDirectorios(item.Tag.ToString())
                    Dim subitem As New TreeViewItem()
                    subitem.Header = NombreCorto(dire.FullName)
                    subitem.Tag = dire.FullName
                    subitem.FontWeight = FontWeights.Normal
                    subitem.Items.Add(dummyNode)
                    AddHandler subitem.Expanded, AddressOf OnFolderExpanded
                    item.Items.Add(subitem)
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub tree_carpetas_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles tree_carpetas.SelectedItemChanged
        Dim tree As TreeView = tree_carpetas
        If (tree.SelectedItem Is Nothing) Then
            Return
        End If

        lbl_carpeta.Content = tree.SelectedItem.Tag

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

    Private Sub btnMarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnMarcar.Click
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            elem.SetSelected(True)
        Next
    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As RoutedEventArgs) Handles btnDesmarcar.Click
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            elem.SetSelected(False)
        Next
    End Sub

    Private Sub btnRenombrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRenombrar.Click
        Dim vids As New List(Of Video)
        Dim elem As FilaSubtitulo
        For Each elem In lstVideos.Items
            If (elem.IsSelected) Then
                vids.Add(elem.video)
            End If
        Next
        SubtitulosManager.RenombrarSubtitulos(vids)
        MsgBox(vids.Count & " Subtitulos Renombrados!")
        CargarListaVideos(lbl_carpeta.Content)
    End Sub

End Class
