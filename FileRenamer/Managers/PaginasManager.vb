Public Class PaginasManager

    Public Shared PAGINA_INICIO As String = "Inicio"
    Public Shared PAGINA_RENOMBRAR As String = "Renombrar"
    Public Shared PAGINA_SUBTITULOS As String = "Subtitulos"
    Public Shared PAGINA_MP3 As String = "Mp3"
    Public Shared PAGINA_CONFIGURACION As String = "Configuracion"
    Public Shared PAGINA_ACERCA_DE As String = "AcercaDe"

    Private Shared Container As Grid
    Private Shared Window As MainWindow
    Private Shared Paginas As Dictionary(Of String, UserControl)
    Private Shared Historial As List(Of String)

    Public Shared Sub Init(Window As MainWindow, Container As Grid)
        PaginasManager.Container = Container
        PaginasManager.Window = Window
        Paginas = New Dictionary(Of String, UserControl)

        Paginas.Add(PAGINA_INICIO, New InicioPage)
        Paginas.Add(PAGINA_RENOMBRAR, New RenombrarPage)
        Paginas.Add(PAGINA_SUBTITULOS, New SubtitulosPage)
        Paginas.Add(PAGINA_MP3, New Mp3Page)
        Paginas.Add(PAGINA_CONFIGURACION, New ConfiguracionPage)
        Paginas.Add(PAGINA_ACERCA_DE, New AcercaDePage)

        Historial = New List(Of String)
    End Sub

    Public Shared Sub IrA(pagina As String)
        Window.Visibility = Visibility.Hidden
        Container.Children.Clear()
        Container.Children.Add(Paginas(pagina))
        Historial.Add(pagina)
    End Sub

    Public Shared Sub Volver()
        If (Historial.Count > 1) Then
            Historial.RemoveAt(Historial.Count - 1)
            Dim anterior As String = Historial.Last
            Historial.RemoveAt(Historial.Count - 1)

            IrA(anterior)
        End If
    End Sub

End Class
