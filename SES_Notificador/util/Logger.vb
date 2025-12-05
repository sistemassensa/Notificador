Imports System.IO

''' <summary>
''' Clase que gestiona el registro del sistema.
''' </summary>
Public Class Loggers
    ''' <summary>Enumeración utilizada para guardar los tipos de mensajes.</summary>
    Public Enum TipoMensaje
        ''' <summary>Tipo de mensaje de depuración.</summary>
        DEBUG
        ''' <summary>Tipo de mensaje de información.</summary>
        INFO
        ''' <summary>Tipo de mensaje de adventencia.</summary>
        WARNING
        ''' <summary>Tipo de mensaje de error.</summary>
        LERROR
    End Enum

    ''' <summary>Tamaño máximo de fichero (50MB).</summary>
    Private Const TAM_FICHERO As Integer = 52428800
    ''' <summary>Extensión del fichero de registro.</summary>
    Private Const EXTENSION As String = ".log"


    ''' <summary>Nivel del registro.</summary>
    Private Shared eNivel As TipoMensaje = TipoMensaje.DEBUG


    ''' <summary>Nivel de registro.</summary>
    ''' <value>Establece el nivel de registro.</value>
    ''' <returns>Obtiene el nivel de registro.</returns>
    Public Shared Property Nivel As TipoMensaje
        Get
            Return eNivel
        End Get
        Set(eActual As TipoMensaje)
            eNivel = eActual
        End Set
    End Property


    ''' <summary>
    ''' Añade un mensaje al registro, indicando el tipo de mensaje y pudiendo indicar de forma
    ''' opcional el nombre del fichero y la línea donde se lanzó el mensaje.
    ''' </summary>
    ''' <param name="eTipo">Tipo de mensaje a guardar en el registro.</param>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Private Shared Sub add(ByVal eTipo As TipoMensaje, ByVal sMensaje As String, Optional sNombreClase As String = "")
        If eTipo >= eNivel Then
            ' Nombre de fichero destino.
            Dim sDestino As String = getFileFecha()

            ' Comprueba si el fichero está lleno, en cuyo caso hace una copia del
            ' archivo existente y comienza a rellenar uno nuevo.
            verificar(My.Application.Info.DirectoryPath + "\Logs\" + sDestino)

            If Not Directory.Exists(My.Application.Info.DirectoryPath + "\Logs") Then
                Directory.CreateDirectory(My.Application.Info.DirectoryPath + "\Logs")
            End If

            ' Fichero en el que almacenar el registro.
            Dim objFichero As New FileStream(My.Application.Info.DirectoryPath + "\Logs\" + sDestino, FileMode.Append, FileAccess.Write)

            If Not objFichero Is Nothing And objFichero.CanWrite Then
                ' Mensaje completo a guardar.
                Dim sDeb As String = String.Empty
                ' Fecha actua.
                Dim objFecha As Date = Date.Now

                ' Composición y guardado en el fichero de la fecha y tipo
                sDeb = objFecha.ToString() + "[" + sNombreClase + "]" + " [" + eTipo.ToString + "] - " & sMensaje

                ' Escribe y cierra
                Dim objStream As New StreamWriter(objFichero)
                objStream.WriteLine(sDeb)
                objStream.Close()
                objFichero.Close()
            End If
        End If
    End Sub


    ''' <summary>Obtiene el nombre de fichero actual.</summary>
    ''' <returns>Cadena con el nombre del fichero.</returns>
    Private Shared Function getFileFecha() As String
        Dim objFecha As Date = Date.Now
        Dim sCadena As String = String.Empty

        ' Compone el nombre de fichero de la forma AAAAMMDD.log
        sCadena &= objFecha.Year
        If objFecha.Month < 10 Then
            sCadena &= "0"
        End If
        sCadena &= objFecha.Month
        If objFecha.Day < 10 Then
            sCadena &= "0"
        End If
        sCadena &= objFecha.Day
        sCadena &= EXTENSION

        Return sCadena
    End Function


    ''' <summary>Analiza si el fichero es demasiado grande y hace una copia de seguridad.</summary>
    ''' <param name="sFichero">Fichero a analizar.</param>
    Private Shared Sub verificar(sFichero As String)
        Try
            If My.Computer.FileSystem.GetFileInfo(sFichero).Length >= TAM_FICHERO Then
                My.Computer.FileSystem.MoveFile(sFichero, sFichero & ".old")
            End If
        Catch ex As IOException
        End Try
    End Sub


    ''' <summary>Inserta un mensaje de error en el registro.</summary>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Public Shared Sub e(sMensaje As String, objExcepcion As Exception, Optional sNombreClase As String = "")
        add(TipoMensaje.LERROR, sMensaje & vbNewLine &
            vbTab & objExcepcion.Message & vbNewLine &
            vbTab & objExcepcion.ToString, sNombreClase)
    End Sub


    ''' <summary>Inserta un mensaje de error en el registro.</summary>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Public Shared Sub e(sMensaje As String, Optional sNombreClase As String = "")
        add(TipoMensaje.LERROR, sMensaje, sNombreClase)
    End Sub


    ''' <summary>Inserta un mensaje de advertencia en el registro.</summary>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Public Shared Sub w(sMensaje As String, Optional sNombreClase As String = "")
        add(TipoMensaje.WARNING, sMensaje, sNombreClase)
    End Sub


    ''' <summary>Inserta un mensaje de información en el registro.</summary>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Public Shared Sub i(sMensaje As String, Optional sNombreClase As String = "")
        add(TipoMensaje.INFO, sMensaje, sNombreClase)
    End Sub


    ''' <summary>Inserta un mensaje de depuración en el registro.</summary>
    ''' <param name="sMensaje">Mensaje a guardar en el registro.</param>
    Public Shared Sub d(sMensaje As String, Optional sNombreClase As String = "")
        add(TipoMensaje.DEBUG, sMensaje, sNombreClase)
    End Sub
End Class
