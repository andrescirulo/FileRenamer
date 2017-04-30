Public Class BackButton

    Private Sub BackButton_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        AddHandler btnAtras.InnerButtonClick, AddressOf btnAtras_Click
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As RoutedEventArgs)
        PaginasManager.Volver()

    End Sub


End Class
