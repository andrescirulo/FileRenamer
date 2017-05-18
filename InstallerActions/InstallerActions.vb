Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Class InstallerActions

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Public Overrides Sub Commit(savedState As IDictionary)
        MyBase.Commit(savedState)

        'Instalo los ensamblados nativos
        EjecutarNgen("install")
    End Sub

    Public Overrides Sub Uninstall(savedState As IDictionary)
        MyBase.Uninstall(savedState)

        'Borro el directorio de configuración
        Dim CONFIG_DIR As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FileManager\"
        If My.Computer.FileSystem.DirectoryExists(CONFIG_DIR) Then
            My.Computer.FileSystem.DeleteDirectory(CONFIG_DIR, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        'Borro la entrada de registro para el menu contextual
        QuitarMenuContextual()

        'Desinstalo los ensamblados nativos
        EjecutarNgen("uninstall")
    End Sub

    Private Function ObtenerRoot() As RegistryKey
        Dim regView As RegistryView = RegistryView.Registry32
        If Environment.Is64BitOperatingSystem Then
            regView = RegistryView.Registry64
        End If
        Return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView)
    End Function

    Private Sub QuitarMenuContextual()
        Dim MainKeyName As String = "Software\Classes\directory\shell\ProcesarSubtitulos"
        Dim BackKeyName As String = "Software\Classes\directory\Background\shell\ProcesarSubtitulos"

        Dim classesRoot As RegistryKey = ObtenerRoot()

        classesRoot.DeleteSubKeyTree(BackKeyName, False)
        classesRoot.DeleteSubKeyTree(MainKeyName, False)

    End Sub

    Private Sub EjecutarNgen(metodo As String)
        Dim runtimeStr As String = RuntimeEnvironment.GetRuntimeDirectory()
        Dim ngenStr As String = Path.Combine(runtimeStr, "ngen.exe")

        ' create a New process...
        Dim Process As New Process()
        Process.StartInfo.FileName = ngenStr

        ' get the assembly (exe) path And filename.
        Dim assemblyPath As String = Context.Parameters("targetdir") & "AC FileManager.exe"

        ' add the argument to the filename as the assembly path.
        ' Use quotes--important if there are spaces in the name.
        ' Use the "install" verb And ngen.exe will compile all deps.
        Process.StartInfo.Arguments = metodo & " """ & assemblyPath & """"
        Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        'start ngen. it will do its magic.
        Process.Start()
        Process.WaitForExit()
    End Sub

End Class
