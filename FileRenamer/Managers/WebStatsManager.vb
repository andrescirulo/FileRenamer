Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading
Imports Newtonsoft.Json

Public Class WebStatsManager

    Private Shared STATS_URL As String = "http://www.andrescirulo.com.ar/api/stats.php"
    'Private Shared STATS_URL As String = "http://localhost/appsStats/stats.php"
    Private Shared APLICACION As String = "FileRenamer"

    Private Shared stat As WebStat
    Public Shared Sub EnviarEstadisticasUso()
        stat = ArmarStat("STAT_V1.1", Nothing)
        EnviarStat()
    End Sub

    Public Shared Sub EnviarError(mensaje As String)
        stat = ArmarStat("ERROR_V1", mensaje)
        EnviarStat()
    End Sub

    Private Shared Function ArmarStat(operacion As String, mensaje As String) As WebStat
        Dim stat As New WebStat
        stat.aplicacion = APLICACION
        stat.operacion = operacion
        stat.version = ActualizacionesManager.ObtenerVersionString
        stat.idioma = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        stat.sistemaOperativo = GetSOName()
        stat.mensaje = mensaje
        Return stat
    End Function
    Private Shared Sub EnviarStat()
        Dim t As New Thread(AddressOf EnviarStatThread)
        t.Start()
    End Sub

    Private Shared Sub EnviarStatThread()
        Dim json As String = ""
        Try
            Dim serializer As New JsonSerializer()
            Dim sw As New StringWriter()
            Dim writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, stat)
            json = sw.ToString()
            sw.Close()


            Dim byteArray() As Byte = Encoding.UTF8.GetBytes("data=" & json)
            Dim request As HttpWebRequest = WebRequest.Create(STATS_URL)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()

            Dim data As Stream = response.GetResponseStream
            Dim reader As New StreamReader(data)

            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            data.Close()
            response.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class
