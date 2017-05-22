Imports System.IO

Class Mp3Page
    Implements FoldersTreeViewListener, TagResultadoListener

    Dim dummyNode As Object = Nothing

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        txtResultado.AllowDrop = True
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Private Sub RenombrarPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Dim carpetaInicial As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        carpetaInicial = carpetaInicial.Replace("Mis Documentos", "Descargas")
        carpetaInicial = carpetaInicial.Replace("My Documents", "Descargas")

        If Not My.Computer.FileSystem.DirectoryExists(carpetaInicial) Then
            carpetaInicial = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        End If

        lbl_carpeta.Content = FileRenamer.Language.renombrar_sin_seleccionar
        lst_files.Items.Clear()


        tree_carpetas.AddListener(Me)
        tree_carpetas.NavegarA(carpetaInicial)
        TagsManager.Inicializar(TagsSrc, Nothing, Me)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_titulo_nombre, LangMp3.tag_titulo_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_artista_nombre, LangMp3.tag_artista_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_genero_nombre, LangMp3.tag_genero_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_album_nombre, LangMp3.tag_album_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_pista_nombre, LangMp3.tag_pista_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_total_pistas_nombre, LangMp3.tag_total_pistas_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_anio_nombre, LangMp3.tag_anio_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_disco_nombre, LangMp3.tag_disco_valor), TagsSrc)
        TagsManager.AddToContainer(New TagDefaultItem(LangMp3.tag_total_discos_nombre, LangMp3.tag_total_discos_valor), TagsSrc)

    End Sub



    Public Sub OnSelectedItemChanged(FilePath As String) Implements FoldersTreeViewListener.OnSelectedItemChanged
        Try
            lbl_carpeta.Content = FilePath
            If Not (lbl_carpeta.Content.EndsWith("\")) Then
                lbl_carpeta.Content = lbl_carpeta.Content & "\"
            End If

            lst_files.Items.Clear()
            Dim archs As List(Of String)
            archs = ObtenerArchivos(lbl_carpeta.Content, SearchOption.TopDirectoryOnly, {"*.mp3", "*.m4a"})

            For Each arch In archs
                Dim TagsElem As TagLib.Tag = ObtenerTags(arch)

                Dim mp3 As New Mp3File(arch, TagsElem)
                lst_files.Items.Add(mp3)
            Next
            RefrescarEjemplo()
        Catch ex As IOException

        End Try
    End Sub

    Private Sub btn_renombrar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_renombrar.InnerButtonClick
        Dim elem As Mp3File
        Dim elegidos As New List(Of Mp3File)
        For Each elem In lst_files.Items
            If elem.IsChecked Then
                elegidos.Add(elem)
            End If
        Next

        If (elegidos.Count = 0) Then
            MsgBox(FileRenamer.Language.mp3_falta_archivo)
            Return
        End If

        If (txtResultado.Text.Trim.Length = 0) Then
            MsgBox(FileRenamer.Language.mp3_falta_formula)
            Return
        End If

        Dim formRes As New ResultadoWindow
        formRes.lst_vista_previa.Items.Clear()

        Dim ListaResultados As New List(Of RenombrarPreview)
        Dim setArchivos As New HashSet(Of String)

        For Each arch In elegidos
            Dim nuevoNombre As String = ArmarNuevoNombre(arch)
            Dim preview As New RenombrarPreview(arch.Path, arch.Nombre, nuevoNombre)
            setArchivos.Add(nuevoNombre.ToLower.Trim)
            ListaResultados.Add(preview)
        Next

        'HAY ALGUN NOMBRE REPETIDO
        If setArchivos.Count < ListaResultados.Count Then
            MsgBox(FileRenamer.Language.mp3_repetidos)
            Return
        End If
        For Each elemDst In ListaResultados
            formRes.lst_vista_previa.Items.Add(elemDst)
        Next

        If (formRes.ShowDialog()) Then
            For Each elemDst In ListaResultados
                Dim archInicial As String = elemDst.PathCompleto
                Dim archFinal As String = GetDirectorio(archInicial) & elemDst.NombreNuevo
                If (archInicial <> archFinal) Then
                    My.Computer.FileSystem.RenameFile(archInicial, elemDst.NombreNuevo)
                End If
            Next
            OnSelectedItemChanged(lbl_carpeta.Content)
            txtResultado.Text = ""
            RefrescarEjemplo()
            MsgBox(FileRenamer.Language.mp3_correcto)
        End If

    End Sub

    Private Function ArmarNuevoNombre(arch As Mp3File) As String
        Dim nuevoNombre As String = txtResultado.Text
        nuevoNombre = ReemplazarTags(nuevoNombre, arch)
        nuevoNombre = nuevoNombre & ObtenerExtension(arch.Nombre)
        Return nuevoNombre
    End Function

    Private Function ReemplazarTags(nuevoNombre As String, arch As Mp3File) As String
        Dim res As String = nuevoNombre
        res = res.Replace(LangMp3.tag_titulo_valor, arch.Titulo)
        res = res.Replace(LangMp3.tag_artista_valor, arch.Artista)
        res = res.Replace(LangMp3.tag_genero_valor, arch.Genero)
        res = res.Replace(LangMp3.tag_album_valor, arch.Album)
        Dim pistaTmp As String = arch.Pista
        pistaTmp = pistaTmp.PadLeft(2, "0")
        res = res.Replace(LangMp3.tag_pista_valor, pistaTmp)
        res = res.Replace(LangMp3.tag_total_pistas_valor, arch.TotalPistas)
        res = res.Replace(LangMp3.tag_anio_valor, arch.Anio)
        res = res.Replace(LangMp3.tag_disco_valor, arch.Disco)
        res = res.Replace(LangMp3.tag_total_discos_valor, arch.TotalDiscos)

        Return res
    End Function

    Private Function quitarExtension(arch As String) As String
        Return arch.Substring(0, arch.LastIndexOf("."))
    End Function

    Public Sub OnResultadoChange(resultado As String) Implements TagResultadoListener.OnResultadoChange
        Dim texto As String = txtResultado.Text
        Dim pos As Integer = txtResultado.SelectionStart
        txtResultado.Text = texto.Substring(0, txtResultado.SelectionStart) & resultado & texto.Substring(txtResultado.SelectionStart)
        txtResultado.SelectionStart = pos + (resultado.Length)
        RefrescarEjemplo()
    End Sub

    Private Sub RefrescarEjemplo()
        Dim elem As Mp3File
        Dim elemEjemplo As Mp3File = Nothing
        For Each elem In lst_files.Items
            If elem.IsChecked Then
                elemEjemplo = elem
                Exit For
            End If
        Next
        If Not elemEjemplo Is Nothing And txtResultado.Text <> "" Then
            Dim nuevoNombre As String = ArmarNuevoNombre(elemEjemplo)
            txtEjemplo.Text = LangMp3.ejemplo & " " & nuevoNombre
        Else
            txtEjemplo.Text = ""
        End If
    End Sub

    Private Sub txtResultado_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtResultado.TextChanged
        RefrescarEjemplo()
    End Sub

    Private Sub txtResultado_Drop(sender As Object, e As DragEventArgs) Handles txtResultado.Drop
        Dim tagItem = DirectCast(e.Data.GetData("Object"), TagDefaultItem)

        If tagItem IsNot Nothing Then
            TagsManager.EnviarADestino(tagItem)
            tagItem.CloseDragWindow()
        End If
    End Sub

    Private Sub txtResultado_PreviewDragOver(sender As Object, e As DragEventArgs) Handles txtResultado.PreviewDragOver
        e.Handled = True
    End Sub
End Class
