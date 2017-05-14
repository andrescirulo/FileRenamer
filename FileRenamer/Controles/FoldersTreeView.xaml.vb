Public Class FoldersTreeView

    Dim dummyNode As Object = Nothing
    Dim Listeners As New List(Of FoldersTreeViewListener)

    Private Sub FoldersTreeView_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        tree_carpetas.Items.Clear()
        Dim listaEspeciales As New List(Of CarpetaEspecial)
        listaEspeciales.Add(New CarpetaEspecial(Carpetas.Escritorio, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
        listaEspeciales.Add(New CarpetaEspecial(Carpetas.Documentos, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
        listaEspeciales.Add(New CarpetaEspecial(Carpetas.Musica, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)))
        listaEspeciales.Add(New CarpetaEspecial(Carpetas.Imagenes, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)))
        listaEspeciales.Add(New CarpetaEspecial(Carpetas.Videos, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)))
        If (My.Computer.FileSystem.DirectoryExists(ObtenerCarpetaDescargas())) Then
            listaEspeciales.Add(New CarpetaEspecial(Carpetas.Descargas, ObtenerCarpetaDescargas()))
        End If

        listaEspeciales = listaEspeciales.OrderBy(Of String)(Function(f)
                                                                 Return f.Nombre
                                                             End Function).ToList()


        For Each elem In listaEspeciales
            Dim item As New TreeViewItem()
            item.Header = elem.Nombre
            item.Tag = elem.Path
            item.FontWeight = FontWeights.Normal
            item.Items.Add(dummyNode)
            AddHandler item.Expanded, AddressOf OnFolderExpanded
            tree_carpetas.Items.Add(item)
        Next

        Dim itemMiPc As New TreeViewItem()
        itemMiPc.Header = Carpetas.Equipo
        itemMiPc.FontWeight = FontWeights.Normal
        AddHandler itemMiPc.Expanded, AddressOf OnFolderExpanded
        tree_carpetas.Items.Add(itemMiPc)


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
            itemMiPc.Items.Add(item)
        Next
    End Sub

    Private Sub OnFolderExpanded(sender As Object, e As RoutedEventArgs)
        Dim item As TreeViewItem = sender
        'If (item.Items.Count = 1 AndAlso item.Items(0) = dummyNode) Then
        If (item.Items.Count = 1 AndAlso item.Items(0) Is Nothing) Then
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

    Friend Sub AddListener(listener As FoldersTreeViewListener)
        If Not Listeners.Contains(listener) Then
            Listeners.Add(listener)
        End If
    End Sub

    Private Sub tree_carpetas_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles tree_carpetas.SelectedItemChanged
        Dim tree As TreeView = tree_carpetas
        If (tree.SelectedItem Is Nothing) Then
            Return
        End If

        For Each listener In Listeners
            listener.OnSelectedItemChanged(tree.SelectedItem.Tag)
        Next
    End Sub

    Public Sub NavegarA(Path As String)
        If Not My.Computer.FileSystem.DirectoryExists(Path) Then
            Return
        End If
        Dim item As TreeViewItem
        For Each item In tree_carpetas.Items
            If Path.StartsWith(item.Tag()) Then
                item.IsExpanded = True
                item.UpdateLayout()
            End If
        Next
        tree_carpetas.UpdateLayout()
        Me.UpdateLayout()
        tree_carpetas.InvalidateVisual()
    End Sub
End Class
