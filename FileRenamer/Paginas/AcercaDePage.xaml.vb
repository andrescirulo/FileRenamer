﻿Imports System.Threading

Public Class AcercaDePage

    Shared myself As AcercaDePage

    Private CAMBIOS_URL As String = "https://github.com/andrescirulo/FileRenamer/raw/master/FileRenamer/Cambios/"
    'Private CAMBIOS_URL As String = "http://127.0.0.1/"


    Private Sub CategoriasPage_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        myself = Me
        Dim version As Version = My.Application.Info.Version()
        Dim versionString As String = "v" & version.Major & "." & version.Minor & "." & version.Revision & "." & version.Build
        txtTitulo.Text = "FileRenamer " & versionString
        txtVersion.Text = ""
        txtContacto.Text = FileRenamer.Language.acerca_de_contacto

        Dim t As New Thread(AddressOf VerificarActualizaciones)
        t.Start()
    End Sub

    Dim versionNueva As String

    Private Sub VerificarActualizaciones()
        versionNueva = ComprobarActualizaciones()
        myself.Dispatcher.Invoke(Sub()
                                     progressVersion.Visibility = Visibility.Collapsed
                                     If versionNueva Is Nothing Then
                                         txtVersion.Text = FileRenamer.Language.acerca_de_no_actualizaciones
                                         btnDescargar.Visibility = Visibility.Collapsed
                                         progressPanel.Visibility = Visibility.Collapsed
                                         flowDoc.Visibility = Visibility.Collapsed
                                     Else
                                         txtVersion.Text = FileRenamer.Language.acerca_de_actualizaciones & ": " & versionNueva
                                         btnDescargar.Visibility = Visibility.Visible
                                         flowDoc.Visibility = Visibility.Visible
                                         MainWindow.SetToolbarUpdateVisible(True)
                                         ObtenerDetallesDeVersion(versionNueva)
                                     End If
                                 End Sub)
    End Sub

    Private Sub ObtenerDetallesDeVersion(versionNueva As String)
        Dim fileName As String = ObtenerNombreArchivoCambios(versionNueva)
        Dim URL As String = CAMBIOS_URL & fileName
        Dim cambios As String = ""
        Try
            cambios = GetRawByUrl(URL)
        Catch ex As Exception
            cambios = FileRenamer.Language.acerca_de_cambios_error
        End Try
        Dim md As New Markdown.Xaml.Markdown
        flowDoc.Document = md.Transform(cambios)
        flowDoc.Document.PagePadding = New Thickness(10)
    End Sub

    Private Sub btnDescargar_Click(sender As Object, e As RoutedEventArgs) Handles btnDescargar.Click
        progressPanel.Visibility = Visibility.Visible
        txtDescargando.Visibility = Visibility.Visible
        btnDescargar.Visibility = Visibility.Collapsed
        Dim t As New Thread(AddressOf DescargarActualizacion)
        t.Start()
    End Sub

    Private Sub DescargarActualizacion()
        IniciarDesargaActualizacion(versionNueva)
    End Sub

    Public Shared Sub NotificarProgreso(progress As Long, total As Long)
        myself.Dispatcher.Invoke(Sub()
                                     If (progress = -1) Then
                                         myself.progressDownload.IsIndeterminate = True
                                         myself.progressDownload.Visibility = Visibility.Visible
                                         myself.txtProgreso.Text = "-"
                                     Else
                                         If (progress < total) Then
                                             myself.progressDownload.IsIndeterminate = False
                                             myself.progressDownload.Value = ((progress * 1.0) / total) * 100
                                             Dim actualMB As String = FormatNumber(progress / 1024.0 / 1024.0, 2)
                                             Dim totalMB As String = FormatNumber(total / 1024.0 / 1024.0) & "MB"
                                             myself.txtProgreso.Text = actualMB & "/" & totalMB
                                         Else
                                             myself.progressPanel.Visibility = Visibility.Collapsed
                                         End If
                                     End If
                                 End Sub)
    End Sub

End Class
