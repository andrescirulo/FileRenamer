Imports System.IO
Imports Newtonsoft.Json

Module Funciones

    Private CONFIG_DIR As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FileRenamer\"
    Private CONFIG_FILE As String = CONFIG_DIR & "config.json"
    Public CONFIG As Configuracion

    Public Sub Inicializar()
        CargarConfiguracion()
    End Sub

    Public Function NombreCorto(ByVal path As String) As String
        Return path.Substring(path.LastIndexOf("\") + 1)
    End Function

    Public Function GetDirectorio(ByVal path As String) As String
        Return path.Substring(0, path.LastIndexOf("\") + 1)
    End Function

    Private Sub CargarConfiguracion()
        If Not My.Computer.FileSystem.FileExists(CONFIG_FILE) Then
            CONFIG = New Configuracion
            CONFIG.ExtensionesSubtitulos = {"*.srt", "*.sub"}.ToList
            CONFIG.ExtensionesVideos = {"*.avi", "*.mkv", "*.mp4"}.ToList
            If Not My.Computer.FileSystem.DirectoryExists(CONFIG_DIR) Then
                My.Computer.FileSystem.CreateDirectory(CONFIG_DIR)
            End If
            GuardarConfiguracion()
        End If

        Dim serializer As New JsonSerializer
        Dim sr As New StreamReader(CONFIG_FILE)
        Dim reader As New JsonTextReader(sr)
        CONFIG = serializer.Deserialize(reader, GetType(Configuracion))
        sr.Close()
    End Sub

    Public Sub GuardarConfiguracion()
        Try
            Dim serializer As New JsonSerializer()
            Dim sw As New StreamWriter(CONFIG_FILE)
            Dim writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, CONFIG)
            sw.Close()
        Catch ex As Exception
            MsgBox("No se pudo guardar la configuración. " & vbCrLf & "Compruebe que posee permisos suficientes o ejecute la aplicación como Administrador")
        End Try
    End Sub
End Module
