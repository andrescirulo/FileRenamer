Class MainWindow

    Public Shared INSTANCE As MainWindow

    Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        INSTANCE = Me
        PaginasManager.Init(Me, PanelPrincipal)
        Inicializar()

        PaginasManager.IrA(PaginasManager.PAGINA_INICIO)
        Me.Visibility = Visibility.Visible
    End Sub

    Private Sub toolbarUpdate_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles toolbarUpdate.MouseUp
        'TabControl1.SelectedIndex = 3
        PaginasManager.IrA(PaginasManager.PAGINA_ACERCA_DE)
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

    Private Sub MainWindow_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If (e.PreviousSize = e.NewSize) Then
            Return
        End If

        Dim w As Double = SystemParameters.PrimaryScreenWidth
        Dim h As Double = SystemParameters.PrimaryScreenHeight

        Me.Left = (w - e.NewSize.Width) / 2
        Me.Top = (h - e.NewSize.Height) / 2
        Me.Visibility = Visibility.Visible
    End Sub
End Class
