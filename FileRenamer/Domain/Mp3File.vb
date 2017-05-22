Public Class Mp3File
    Public Property Path As String
    Public Property Nombre As String
    Public Property Tag As TagLib.Tag
    Public Property IsChecked As Boolean = True

    Public Property Titulo As String
    Public Property Artista As String
    Public Property Genero As String
    Public Property Album As String
    Public Property Pista As Integer
    Public Property TotalPistas As Integer
    Public Property Anio As Integer
    Public Property Disco As Integer
    Public Property TotalDiscos As Integer

    Public Sub New(Path As String, Tag As TagLib.Tag)
        Me.Path = Path
        Me.Nombre = NombreCorto(Path)
        Me.Tag = Tag

        'Titulo
        Me.Titulo = Tag.Title

        'Artista
        If (Tag.AlbumArtists.Count > 0) Then
            Me.Artista = Tag.AlbumArtists(0)
        ElseIf Tag.Performers.Count > 0 Then
            Me.Artista = Tag.Performers(0)
        End If

        'Genero
        If (Tag.Genres.Count > 0) Then
            Me.Genero = Tag.Genres(0)
        End If

        'Album
        Me.Album = Tag.Album

        'Pista
        Me.Pista = Tag.Track

        'Total Pistas
        Me.TotalPistas = Tag.TrackCount

        'Año
        Me.Anio = Tag.Year

        'Disco
        Me.Disco = Tag.Disc

        'Total Discos
        Me.TotalDiscos = Tag.DiscCount

    End Sub
End Class
