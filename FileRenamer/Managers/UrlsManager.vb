Module UrlsManager
    Public MAIN_URL As String = "http://www.andrescirulo.com.ar/"

    Public VERSION_URL As String = MAIN_URL & "api/version.php?op=uv&ap=filerenamer"
    Public INSTALADORES_URL As String = MAIN_URL & "instaladores/filerenamer/"
    Public STATS_URL As String = MAIN_URL & "api/stats.php"
    Public CAMBIOS_URL As String = MAIN_URL & "cambios/filerenamer/"

    Public Sub RefrescarURLs()
        VERSION_URL = MAIN_URL & "api/version.php?op=uv&ap=filerenamer"
        INSTALADORES_URL = MAIN_URL & "instaladores/filerenamer/"
        STATS_URL = MAIN_URL & "api/stats.php"
        CAMBIOS_URL = MAIN_URL & "cambios/filerenamer/"
    End Sub
End Module
