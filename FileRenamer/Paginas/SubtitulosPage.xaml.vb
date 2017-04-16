﻿Imports System.Windows.Forms

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
        SubtitulosManager.RenombrarSubtitulos(lstVideos.SelectedItems)
        MsgBox("Listo!")
    End Sub

End Class
