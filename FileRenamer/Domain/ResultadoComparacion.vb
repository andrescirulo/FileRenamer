Public Class ResultadoComparacion
    Public Subtitulo As Subtitulo
    Public Similitud As Double

    Public Sub New(ByRef Subt As Subtitulo, sim As Double)
        Subtitulo = Subt
        Similitud = sim
    End Sub
End Class
