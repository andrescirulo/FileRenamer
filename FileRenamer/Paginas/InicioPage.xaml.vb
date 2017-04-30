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
    End Sub
End Class
