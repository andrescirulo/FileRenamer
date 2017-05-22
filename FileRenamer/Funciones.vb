Imports System.IO
Imports System.Net
Imports CodeProject
Imports Newtonsoft.Json
Imports Syroot.Windows.IO

Module Funciones

    Public CONFIG_DIR As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FileRenamer\"
    Private CONFIG_FILE As String = CONFIG_DIR & "config.json"
    Public CONFIG As Configuracion

    Public SCREEN_HEIGHT As Integer
    Public SCREEN_WIDTH As Integer

    Public Sub Inicializar()
        InicializarSimple()
        SCREEN_WIDTH = System.Windows.SystemParameters.PrimaryScreenWidth
        SCREEN_HEIGHT = System.Windows.SystemParameters.PrimaryScreenHeight

        Dim DEBUG_URL_FILE As String = CONFIG_DIR & "debug_url.txt"
        If (My.Computer.FileSystem.FileExists(DEBUG_URL_FILE)) Then
            Dim sr As New StreamReader(DEBUG_URL_FILE)
            MAIN_URL = sr.ReadLine()
            sr.Close()
            RefrescarURLs()
        End If

        WebStatsManager.EnviarEstadisticasUso()
    End Sub

    Public Sub InicializarSimple()
        CargarConfiguracion()
    End Sub

    Public Function NombreCorto(ByVal path As String) As String
        Return path.Substring(path.LastIndexOf("\") + 1)
    End Function

    Public Function GetDirectorio(ByVal path As String) As String
        Return path.Substring(0, path.LastIndexOf("\") + 1)
    End Function

    Public Function ObtenerExtension(archivo As String) As String
        Return archivo.Substring(archivo.LastIndexOf("."))
    End Function

    Private Sub CargarConfiguracion()
        If Not My.Computer.FileSystem.FileExists(CONFIG_FILE) Then
            CONFIG = New Configuracion
            CONFIG.ExtensionesSubtitulos = {"*.srt", "*.sub"}.ToList
            CONFIG.ExtensionesVideos = {"*.avi", "*.mkv", "*.mp4"}.ToList
            CONFIG.CarpetaInicial = KnownFolders.Downloads.ExpandedPath
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

    Public Function GetRawByUrl(url As String) As String
        Dim wrGETURL As WebRequest = WebRequest.Create(url)
        Dim response As WebResponse = wrGETURL.GetResponse
        Dim reader As New StreamReader(response.GetResponseStream)
        Dim resultado As String = reader.ReadToEnd
        reader.Close()

        Return resultado
    End Function


    Public Function GetSOName() As String
        Dim os As OperatingSystem = System.Environment.OSVersion
        Dim version As String = os.Version.Major & "." & os.Version.Minor
        Dim osName As String
        Select Case version
            Case "6.1"
                osName = "Windows 7"
            Case "6.2"
                osName = "Windows 8"
            Case "6.3"
                osName = "Windows 8.1"
            Case "10.0"
                osName = "Windows 10"
            Case Else
                osName = os.VersionString
        End Select

        Return osName
    End Function

    Public Function ObtenerTags(filePath As String) As TagLib.Tag
        Dim file As TagLib.File = TagLib.File.Create(filePath)

        Dim TagsElem As TagLib.Tag = file.GetTag(TagLib.TagTypes.Id3v2, False)
        If Not TagsElem Is Nothing Then
            Return TagsElem
        End If

        TagsElem = file.GetTag(TagLib.TagTypes.Id3v1, False)
        If Not TagsElem Is Nothing Then
            Return TagsElem
        End If

        For Each tagType In System.Enum.GetValues(GetType(TagLib.TagTypes))
            TagsElem = file.GetTag(tagType, False)
            If Not TagsElem Is Nothing Then
                Return TagsElem
            End If
        Next
        Return Nothing
    End Function
End Module
