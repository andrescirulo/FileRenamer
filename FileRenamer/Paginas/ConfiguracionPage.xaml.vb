Public Class ConfiguracionPage
    Private Sub chkHabilitar_Checked(sender As Object, e As RoutedEventArgs) Handles chkHabilitar.Checked, chkHabilitar.UnChecked
        HabilitarRadios()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As RoutedEventArgs) Handles btnGuardar.InnerButtonClick
        MenuContextualManager.HabilitarMenuContextual(chkHabilitar.IsChecked, radFondo.IsChecked, radExtendido.IsChecked)
        PaginasManager.IrA(PaginasManager.PAGINA_INICIO)
    End Sub

    Private Sub ConfiguracionPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'CARGAR ESTADO ACTUAL
        Dim estadoMenu As EstadoMenuContextual = MenuContextualManager.ObtenerEstadoMenuContextual
        chkHabilitar.IsChecked = estadoMenu.Habilitado
        If (estadoMenu.Habilitado) Then
            radNombre.IsChecked = Not estadoMenu.EnFondo
            radFondo.IsChecked = estadoMenu.EnFondo
            radNormal.IsChecked = Not estadoMenu.ModoExtendido
            radExtendido.IsChecked = estadoMenu.ModoExtendido
        Else
            'SI NO ESTA HABILITADO PONGO EL VALOR POR DEFECTO
            radNombre.IsChecked = False
            radFondo.IsChecked = True
            radNormal.IsChecked = False
            radExtendido.IsChecked = True
        End If
        HabilitarRadios()
    End Sub

    Private Sub HabilitarRadios()
        groupDonde.IsEnabled = chkHabilitar.IsChecked
        groupCuando.IsEnabled = chkHabilitar.IsChecked
    End Sub
End Class
