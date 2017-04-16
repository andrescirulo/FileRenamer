Imports Microsoft.Win32

Public Class MenuContextualManager

    Private Shared MainKeyName As String = "Software\Classes\directory\shell\ProcesarSubtitulos"
    Private Shared BackKeyName As String = "Software\Classes\directory\Background\shell\ProcesarSubtitulos"

    Public Shared Sub HabilitarMenuContextual(AgregarMenuContextual As Boolean, EnFondo As Boolean, ModoExtendido As Boolean)
        Dim executableName As String = "SubtituloContextApp.exe "
        Dim pathParam As String = ""

        Dim usedKey As String = ""

        Dim classesRoot As RegistryKey = ObtenerRoot()

        classesRoot.DeleteSubKeyTree(BackKeyName, False)
        classesRoot.DeleteSubKeyTree(MainKeyName, False)

        If (AgregarMenuContextual) Then
            Dim path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
            path = New Uri(path).LocalPath
            If Not (path.EndsWith("\")) Then
                path = path & "\"
            End If


            If (EnFondo) Then
                pathParam = """%V"""
                usedKey = BackKeyName
            Else
                pathParam = """%1"""
                usedKey = MainKeyName
            End If
            path = path & executableName & pathParam

            Dim cmdKeyName As String = usedKey & "\command"

            Dim mainKey As RegistryKey = classesRoot.CreateSubKey(usedKey, RegistryKeyPermissionCheck.ReadWriteSubTree)
            Dim cmdKey As RegistryKey = classesRoot.CreateSubKey(cmdKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree)

            mainKey.SetValue("", "Procesar Subtitulos")
            cmdKey.SetValue("", path)
            If (ModoExtendido) Then
                mainKey.SetValue("Extended", "")
            End If
        End If
    End Sub

    Private Shared Function ObtenerRoot() As RegistryKey
        Dim regView As RegistryView = RegistryView.Registry32
        If Environment.Is64BitOperatingSystem Then
            regView = RegistryView.Registry64
        End If
        Return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView)
    End Function

    Public Shared Function ObtenerEstadoMenuContextual() As EstadoMenuContextual
        Dim estado As New EstadoMenuContextual
        Dim classesRoot As RegistryKey = ObtenerRoot()
        Dim mainKey As RegistryKey = classesRoot.OpenSubKey(MainKeyName)
        Dim backKey As RegistryKey = classesRoot.OpenSubKey(BackKeyName)

        If (mainKey Is Nothing And backKey Is Nothing) Then
            estado.Habilitado = False
        Else
            estado.Habilitado = True
            If mainKey Is Nothing Then
                estado.EnFondo = True
                estado.ModoExtendido = Not (backKey.GetValue("Extended") Is Nothing)
            Else
                estado.EnFondo = False
                estado.ModoExtendido = Not (mainKey.GetValue("Extended") Is Nothing)
            End If
        End If

        Return estado
    End Function
End Class
