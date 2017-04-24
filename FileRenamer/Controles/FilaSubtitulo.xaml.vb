Public Class FilaSubtitulo

    Public Video As Video

    Public Sub New(ByRef video As Video)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.video = video
        chkElegido.Content = video.Nombre & "." & video.Extension
        lblSubtitulo.Content = video.Subtitulo.Nombre & "." & video.Subtitulo.Extension
    End Sub

    Public Function IsSelected() As Boolean
        Return chkElegido.IsChecked
    End Function

    Public Sub SetSelected(selected As Boolean)
        chkElegido.IsChecked = selected
    End Sub
End Class
