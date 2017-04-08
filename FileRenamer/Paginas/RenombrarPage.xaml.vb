Imports System.Collections.ObjectModel
Imports System.Windows.Forms

Class RenombrarPage
    Dim oFileD As OpenFileDialog
    Dim oFolderD As FolderBrowserDialog

    Dim OPERACIONES As List(Of Operacion)

    Private Function GetOperaciones() As List(Of Operacion)
        If OPERACIONES Is Nothing Then
            OPERACIONES = New List(Of Operacion)
            OPERACIONES.Add(New ReemplazoEnNombre)
            OPERACIONES.Add(New Reemplazo)
            OPERACIONES.Add(New AgregarAlFinalDelNombre)
            OPERACIONES.Add(New AgregarAlFinalDeTodo)
            OPERACIONES.Add(New AgregarAlPrincipio)
            OPERACIONES.Add(New EliminarEntreCaracteres)
            OPERACIONES.Add(New SoloEntreCaracteres)
            OPERACIONES.Add(New AgregarPadding)
        End If
        Return OPERACIONES
    End Function

    Private Sub RenombrarPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
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

        For Each oper In GetOperaciones()
            cmb_operacion.Items.Add(oper)
        Next
        cmb_operacion.SelectedIndex = 0
    End Sub

    Private Sub btn_carpeta_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_carpeta.Click
        If (oFolderD.ShowDialog() = Forms.DialogResult.OK) Then
            lbl_carpeta.Content = oFolderD.SelectedPath

            If Not (lbl_carpeta.Content.EndsWith("\")) Then
                lbl_carpeta.Content = lbl_carpeta.Content & "\"
            End If

            lst_files.Items.Clear()
            Dim archs As ReadOnlyCollection(Of String)
            If (chk_subcarpetas.IsChecked) Then
                archs = My.Computer.FileSystem.GetFiles(lbl_carpeta.Content, FileIO.SearchOption.SearchAllSubDirectories)
            Else
                archs = My.Computer.FileSystem.GetFiles(lbl_carpeta.Content)
            End If

            For Each arch In archs
                Dim elem As String = arch.Replace(lbl_carpeta.Content, "")

                lst_files.Items.Add(elem)
            Next
            lst_files.SelectAll()

        End If
    End Sub

    Private Sub btn_agregar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_agregar.Click

        Dim op As Operacion = cmb_operacion.SelectedItem
        op.Armar(Me)

        lst_reemplazos.Items.Add(op)

        txt_src_reemp.Text = ""
        txt_dest_reemp.Text = ""
    End Sub

    Private Sub btn_renombrar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_renombrar.Click
        Dim elegidos As New List(Of String)
        For Each elem In lst_files.SelectedItems
            elegidos.Add(elem)
        Next
        Dim op As Operacion
        Dim formRes As New ResultadoWindow

        formRes.lst_vista_previa.Items.Clear()

        For Each arch In elegidos
            Dim res As String = NombreCorto(arch)

            For Each op In lst_reemplazos.Items
                res = op.Operar(res)
            Next
            formRes.lst_vista_previa.Items.Add(res)
        Next

        If (formRes.ShowDialog()) Then
            For Each arch In elegidos
                Dim nombreInicial = NombreCorto(arch)
                Dim res As String = NombreCorto(arch)

                For Each op In lst_reemplazos.Items
                    res = op.Operar(res)
                Next

                Dim archInicial As String = lbl_carpeta.Content & arch
                Dim archFinal As String = GetDirectorio(archInicial) & res
                If (archInicial <> archFinal) Then
                    My.Computer.FileSystem.RenameFile(archInicial, res)
                End If
            Next

            lbl_carpeta.Content = "Sin Seleccionar"
            lst_files.Items.Clear()
            lst_reemplazos.Items.Clear()
            MsgBox("Archivos Renombrados Correctamente!!")
        End If

    End Sub



    Private Sub cmb_operacion_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmb_operacion.SelectionChanged
        If (cmb_operacion.SelectedIndex >= 0) Then
            cmb_operacion.SelectedItem.AcomodarFormulario(Me)
        End If
    End Sub

    Private Sub btn_quitar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_quitar.Click
        If (lst_reemplazos.SelectedIndex >= 0) Then
            lst_reemplazos.Items.Remove(lst_reemplazos.SelectedItem)
        End If
    End Sub


    Private Function quitarExtension(arch As String) As String
        Return arch.Substring(0, arch.LastIndexOf("."))
    End Function


End Class
