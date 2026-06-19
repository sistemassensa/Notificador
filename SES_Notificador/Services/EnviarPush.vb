Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public Class EnviarPush

    Public Sub EnviarNotificacionPush(
    usuario As String,
    titulo As String,
    mensaje As String)

        Try

            Loggers.d("Leemos las variables de entorno para el sendpush")
            Dim vlUrlAPiSendPush = Environment.GetEnvironmentVariable("CRM_API_SENDPUSH")
            Dim vlAPiKeySendPush = Environment.GetEnvironmentVariable("CRM_APIKEY_SENDPUSH")

            'Gilberto Madrid 15/06/2026 si no se encuentran las variables de entorno no hacemos nada
            If String.IsNullOrWhiteSpace(vlUrlAPiSendPush) Or String.IsNullOrWhiteSpace(vlAPiKeySendPush) Then
                Loggers.d("variables de entorno para sendpush estan vacias y salimos")
                Exit Sub
            End If

            Dim request =
                CType(
                    WebRequest.Create(
                        vlUrlAPiSendPush
                    ),
                    HttpWebRequest
                )

            request.Method = "POST"

            request.Timeout = 15000

            request.ContentType =
                "application/json"

            request.Headers.Add(
                "X-PUSH-API-KEY",
                vlAPiKeySendPush
            )

            Dim body = New With {
                .usuario = usuario,
                .titulo = titulo,
                .mensaje = mensaje
            }

            Dim json As String = JsonConvert.SerializeObject(body)

            Dim bytes() As Byte =
            Encoding.UTF8.GetBytes(
                json
            )

            Loggers.d("armamos el request del sendpush y lo mandamos")

            request.ContentLength =
                bytes.Length

            Using stream =
                request.GetRequestStream()

                stream.Write(
                    bytes,
                    0,
                    bytes.Length
                )

            End Using

            Using response =
                CType(
                    request.GetResponse(),
                    HttpWebResponse
                )

                Using reader As New StreamReader(
                    response.GetResponseStream()
                )

                    Dim result =
                        reader.ReadToEnd()

                    Loggers.d("se hace el request del push y guardamos la respuesta " + result)

                End Using

            End Using

        Catch ex As Exception

            Loggers.d("marco error el request del push y nos quedamos con el error " + ex.Message)

        End Try

    End Sub

End Class
