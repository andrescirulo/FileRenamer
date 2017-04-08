Public MustInherit Class Operacion

    Public MustOverride Sub AcomodarFormulario(ByRef form As RenombrarPage)
    Public MustOverride Sub Armar(ByRef form As RenombrarPage)
    Public MustOverride Function Operar(ByVal path As String) As String
    Public MustOverride Property Nombre As String
    Public MustOverride Function GetDescripcion() As String

End Class
