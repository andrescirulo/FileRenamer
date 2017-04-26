Class MainWindow

    Public Shared INSTANCE As MainWindow

    Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        INSTANCE = Me
        Inicializar()
    End Sub

    Private Sub toolbarUpdate_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles toolbarUpdate.MouseUp
        TabControl1.SelectedIndex = 3
        toolbarUpdate.Visibility = Visibility.Collapsed
    End Sub

    Public Shared Sub SetToolbarUpdateVisible(Visible As Boolean)
        If Not INSTANCE Is Nothing Then
            If (Visible) Then
                INSTANCE.toolbarUpdate.Visibility = Visibility.Visible
            Else
                INSTANCE.toolbarUpdate.Visibility = Visibility.Collapsed
            End If
        End If
    End Sub

End Class
