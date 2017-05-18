Imports System.Globalization
Imports System.Windows.Threading

Class InicioPage
    Private Sub btnRenombrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRenombrar.InnerButtonClick
        PaginasManager.IrA(PaginasManager.PAGINA_RENOMBRAR)
    End Sub

    Private Sub btnSubtitulos_Click(sender As Object, e As RoutedEventArgs) Handles btnSubtitulos.InnerButtonClick
        PaginasManager.IrA(PaginasManager.PAGINA_SUBTITULOS)
    End Sub

    Private Sub btnConfiguracion_Click(sender As Object, e As RoutedEventArgs) Handles btnConfiguracion.InnerButtonClick
        PaginasManager.IrA(PaginasManager.PAGINA_CONFIGURACION)
    End Sub

    Private Sub btnAcercaDe_Click(sender As Object, e As RoutedEventArgs) Handles btnAcercaDe.InnerButtonClick
        PaginasManager.IrA(PaginasManager.PAGINA_ACERCA_DE)
    End Sub

    Private Sub InicioPage_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        ComprobarIdioma()
    End Sub

    Private Sub ComprobarIdioma()
        txtLanguage.Visibility = Visibility.Collapsed
        Dim culture As CultureInfo = CultureInfo.CurrentUICulture
        Dim idioma = culture.TwoLetterISOLanguageName.ToLower
        If idioma <> "es" And idioma <> "en" Then
            txtLanguage.Visibility = Visibility.Visible
        End If
    End Sub

    Dim timerClipboard As DispatcherTimer
    Private Sub btnMail_Click(sender As Object, e As RoutedEventArgs) Handles btnMail.Click
        timerClipboard = New DispatcherTimer()
        timerClipboard.Interval = TimeSpan.FromMilliseconds(500)
        AddHandler timerClipboard.Tick, AddressOf CopiarMail
        timerClipboard.Start()
    End Sub

    Private Sub CopiarMail()
        Try
            Clipboard.Clear()
            Clipboard.SetText("andres.cirulo@gmail.com")
            timerClipboard.Stop()
            MsgBox("E-mail Address copied to the clipboard")
        Catch ex As Exception
        End Try
    End Sub
End Class
