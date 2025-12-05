Imports Newtonsoft.Json

Public Class NotificacionesRequest
    Public mail_template_key As String
    Public from As from

    <JsonProperty("to")>
    Public para() As Para

    Public merge_info As merge_info

    'Public attachments() As attachments
    Public attachments As List(Of attachments)

End Class
