Imports System.Globalization
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Module ActualizacionesManager


    Public Function ComprobarActualizaciones() As String
        Try
            Dim wrGETURL As HttpWebRequest = WebRequest.Create(VERSION_URL)

            Dim response As WebResponse = wrGETURL.GetResponse
            Dim reader As New StreamReader(response.GetResponseStream)
            Dim resultado As String = reader.ReadToEnd

            Dim serializer As New JsonSerializer
            Dim sr As New StringReader(resultado)
            Dim JsonReader As New JsonTextReader(sr)
            Dim version As VersionResponse = serializer.Deserialize(JsonReader, GetType(VersionResponse))
            sr.Close()
            If (version.Resultado = "OK") Then
                Dim versiones As New List(Of String)
                versiones.Add(version.Version)
                Return HayVersionMasNueva(versiones)
            Else
                Return False
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function HayVersionMasNueva(versiones As List(Of String)) As String
        Dim version As Version = My.Application.Info.Version()
        Dim VersionActual() As Integer = {version.Major, version.Minor, version.Build, version.Revision}
        Dim VersionMayor() As Integer = VersionActual
        Dim versionesList As New List(Of Integer())
        For Each vers In versiones
            Dim VersionArray() As Integer = {0, 0, 0, 0}
            For i = 0 To (vers.Split(".").Count - 1)
                VersionArray(i) = vers.Split(".")(i)
            Next
            versionesList.Add(VersionArray)
        Next
        For Each vers In versionesList
            If EsVersionMayor(vers, VersionMayor) Then
                VersionMayor = vers
            End If
        Next
        If EsVersionMayor(VersionMayor, VersionActual) Then
            Return "v" & VersionMayor(0) & "." & VersionMayor(1) & "." & VersionMayor(2) & "." & VersionMayor(3)
        End If
        Return Nothing
    End Function

    Private Function EsVersionMayor(vers() As Integer, versionMayor() As Integer) As Boolean
        If (vers(0) > versionMayor(0)) Then
            Return True
        End If
        If (vers(0) = versionMayor(0) And vers(1) > versionMayor(1)) Then
            Return True
        End If
        If (vers(0) = versionMayor(0) And vers(1) = versionMayor(1) And vers(2) > versionMayor(2)) Then
            Return True
        End If
        If (vers(0) = versionMayor(0) And vers(1) = versionMayor(1) And vers(2) = versionMayor(2) And vers(3) > versionMayor(3)) Then
            Return True
        End If
        Return False
    End Function

    Public Sub IniciarDesargaActualizacion(versionNueva As String)
        Dim fileName As String = "FileRenamer_[version]_[lang].msi"
        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        fileName = fileName.Replace("[version]", versionNueva)
        fileName = fileName.Replace("[lang]", culture.TwoLetterISOLanguageName.ToLower)

        Dim URL As String = INSTALADORES_URL & fileName

        Dim pathDest As String = CONFIG_DIR & "\" & fileName
        Dim wrGETURL As HttpWebRequest = WebRequest.Create(URL)
        wrGETURL.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"
        AcercaDePage.NotificarProgreso(-1, -1)
        Dim response As WebResponse = wrGETURL.GetResponse

        Dim Buffer(4096) As Byte, BlockSize As Integer
        Dim SourceStream As Stream = response.GetResponseStream
        Dim tamTotal As Long = response.ContentLength

        'Memory stream to store data  
        Dim TempStream As New MemoryStream
        Do
            BlockSize = SourceStream.Read(Buffer, 0, 4096)
            If BlockSize > 0 Then TempStream.Write(Buffer, 0, BlockSize)
            AcercaDePage.NotificarProgreso(TempStream.Length, tamTotal)
        Loop While BlockSize > 0
        SourceStream.Close()
        AcercaDePage.NotificarProgreso(tamTotal, tamTotal)

        'return the document binary data  
        Dim MsiBytes As Byte() = TempStream.ToArray()


        Dim MsiFile As FileStream = File.Create(pathDest)
        MsiFile.Write(MsiBytes, 0, MsiBytes.Length)
        MsiFile.Flush()
        MsiFile.Close()
        TempStream.Close()
        TempStream.Dispose()

        Dim p As New Process
        p.StartInfo = New ProcessStartInfo
        p.StartInfo.FileName = pathDest
        p.Start()
        End
    End Sub

    Public Function ObtenerVersionString() As String
        Dim version As Version = My.Application.Info.Version()
        Dim VersionActual() As Integer = {version.Major, version.Minor, version.Build, version.Revision}
        Dim versionString As String = ""
        Dim agregar As Boolean = False
        For i = 3 To 0 Step -1
            If (VersionActual(i) > 0 Or agregar) Then
                agregar = True
                versionString = "." & VersionActual(i) & versionString
            End If
        Next
        versionString = "v" & versionString.Substring(1)
        Return versionString
    End Function

    Public Function ObtenerNombreArchivoCambios(version As String) As String
        Dim fileName As String = "Cambios_[version]_[lang].txt"
        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        fileName = fileName.Replace("[version]", version)
        fileName = fileName.Replace("[lang]", culture.TwoLetterISOLanguageName.ToLower)
        Return fileName
    End Function
End Module
