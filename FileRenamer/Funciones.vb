Imports System.IO
Imports CodeProject
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
            CONFIG.CarpetaInicial = ObtenerCarpetaDescargas()
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

    Private Function ObtenerCarpetaDescargas() As String
        Dim carpetaInicial As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        carpetaInicial = carpetaInicial.Replace("Mis Documentos", "Descargas")
        carpetaInicial = carpetaInicial.Replace("My Documents", "Downloads")

        Return carpetaInicial
    End Function

    Public Function ObtenerArchivos(Path As String, searchOption As SearchOption, extensiones() As String) As List(Of String)
        Dim lista As New List(Of String)
        For Each ext In extensiones
            lista.AddRange(ObtenerArchivos(Path, searchOption, ext))
        Next
        Return lista
    End Function
    Public Function ObtenerArchivos(Path As String, searchOption As SearchOption) As List(Of String)
        Return ObtenerArchivos(Path, searchOption, "*")
    End Function

    Public Function ObtenerArchivos(Path As String, searchOption As SearchOption, filtro As String) As List(Of String)
        Dim lista As New List(Of String)
        Dim files() As FileData = FastDirectoryEnumerator.GetFiles(Path, filtro, searchOption)
        For Each elem In files
            If Not elem.Attributes.HasFlag(FileAttributes.System) And Not elem.Attributes.HasFlag(FileAttributes.Hidden) Then
                lista.Add(elem.Path)
            End If
        Next
        Return lista
    End Function

    Public Function ObtenerUnidades() As List(Of DriveInfo)
        Dim lista As New List(Of DriveInfo)
        For Each drive In DriveInfo.GetDrives
            If drive.IsReady Then
                lista.Add(drive)
            End If
        Next
        Return lista
    End Function

    Public Function ObtenerDirectorios(path As String) As List(Of DirectoryInfo)
        Dim lista As New List(Of DirectoryInfo)
        Dim parentDir As New DirectoryInfo(path)
        For Each dire In parentDir.GetDirectories()
            If Not dire.Attributes.HasFlag(FileAttributes.System) And Not dire.Attributes.HasFlag(FileAttributes.Hidden) Then
                lista.Add(dire)
            End If
        Next
        Return lista
    End Function
End Module
