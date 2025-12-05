Imports System.IO
Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports Newtonsoft
Imports EncryptSiemens
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports Renci.SshNet

Public Class frmMDI

    Public Usuario As String
    Public Contraseña As String
    Dim Configuracion As New Configuracion
    Dim conexion As New Conexion()
    Dim correo As MailMessage
    Dim servidor As SmtpClient
    Dim vlMensajeLog As String

    Private Sub frmMDI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Configuracion.UltimoUsuario = Usuario
        Configuracion.Save()

        '02/08/2018 Gilberto Madrid (se libera la primera version del programa para notificar)
        'Me.Text = Me.Text + "| Version 1.1 | 27/09/2018 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"
        'Me.Text = Me.Text + "| Version 1.2.1 | 02/10/2018 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"
        'Gilberto Madrid 31/07/2019 se agrega el proceso de copiado de archivos para el portal
        'Me.Text = Me.Text + "| Version 1.2.2 | 03/10/2018 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"
        'Me.Text = Me.Text + "| Version 1.2.3 | 31/07/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 06/08/2019 corregimos un bug al momento de copiar los archivos del portal
        'Me.Text = Me.Text + "| Version 1.2.4 | 06/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 07/08/2019 agregamos un datatable extra para el copiado de archivos
        'Me.Text = Me.Text + "| Version 1.2.5 | 06/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 07/08/2019 agregamos un datatable extra para el copiado de archivos
        'Me.Text = Me.Text + "| Version 1.2.6 | 06/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 08/08/2019 dejamos temporalmente manual la copia de los archivos del portal
        'Me.Text = Me.Text + "| Version 1.2.7 | 06/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 09/08/2019 dejamos temporalmente manual la copia de los archivos del portal
        'Me.Text = Me.Text + "| Version 1.2.8 | 09/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 09/08/2019 ocultamos y mostramos dependiendo del chk de uso de portal
        'Me.Text = Me.Text + "| Version 1.2.9 | 09/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 10/08/2019 agregamos un parametro para omitir la pregunta cuando se lance el copiado de archivos del portal con el timer
        'Me.Text = Me.Text + "| Version 1.3 | 09/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 26/08/2019 truncamos el mensaje al copiar archivos al portal
        'Me.Text = Me.Text + "| Version 1.3.1 | 09/08/2019 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 14/07/2020 se movio de lugar el guardado en el historial de notificaciones
        'Me.Text = Me.Text + "| Version 1.3.2 | 14/07/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 17/11/2020 se agrego la opcion de webservice para el proveedor siemens
        'Me.Text = Me.Text + "| Version 1.4 | 17/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 18/11/2020 por error se fue el timer de trabajos en 1 segundo, se modifico a una hora
        'Me.Text = Me.Text + "| Version 1.4.1 | 18/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 25/11/2020 se agrego la funcionalidad de autorizacion de cambio de precio por correo
        'Me.Text = Me.Text + "| Version 1.5 | 25/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 25/11/2020 se modificaron algunas cosas del html del envio del correo de la solicitud de autorizacion
        'Me.Text = Me.Text + "| Version 1.5.1 | 26/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 28/11/2020 se valido que los datos adjuntos si viniera informacion ya que estaba agregando el pdf de cotizacion aun cuando no seleccionaron ninguna del envio del correo de la solicitud de autorizacion
        'Me.Text = Me.Text + "| Version 1.5.2 | 28/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 30/11/2020 se cambiaron los vectores para el webservice de siemens y al webservice de produccion
        'Me.Text = Me.Text + "| Version 1.5.3 | 30/11/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 01/12/2020 se modifica el mensaje para el supervisor cuando autoriza un cambio de precio por correo
        'Me.Text = Me.Text + "| Version 1.5.4 | 01/12/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 08/12/2020 se anexaron 2 campos mas a la solicitud de precios por correo para vender por debajo del margen y del costo
        'Me.Text = Me.Text + "| Version 1.5.5 | 08/12/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 16/12/2020 se cambio el webservice de siemens ya que cambiaron de servidor
        'Me.Text = Me.Text + "| Version 1.5.6 | 16/12/2020 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"


        'César Niebla 04/10/2022 Se implementó el parametro de CorreoCopia_OperacionesSupervisadasRemotasPorCorreo para que en automático le llegue copia de los correos de solicitud y notificación de atención a un supervisor principal
        'Me.Text = Me.Text + "| Version 1.6.0 | 04/10/2022 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 08/11/2023 Se implemento etiquetar los correos con no-reply
        'Me.Text = Me.Text + "| Version 1.6.2 | 08/11/2023 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 09/11/2023 Se agrega etiqueta configurable al correo
        'Me.Text = Me.Text + "| Version 1.6.5 | 09/11/2023 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 16/11/2023 Se agrega el llamado al servicio web rest de siemens y se quitan los popup para loguear en archivos
        'Me.Text = Me.Text + "| Version 1.6.6 | 16/11/2023 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 03/01/2024 Se agrega log al crear el archivo para las notificaciones ya que en algunos lugares no se esta creando
        'Me.Text = Me.Text + "| Version 1.6.7 | 03/01/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 19/01/2024 Se arregla bug ya que se dejo que solo obtuviera de la BD el nuevo WS de siemens
        'Me.Text = Me.Text + "| Version 1.6.8 | 19/01/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 09/02/2024 Se agrega el consumo a WS de Azure para que se actualice la IP publica sola
        'Me.Text = Me.Text + "| Version 1.7 | 09/02/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 09/02/2024 Se agrega el consumo a WS de Azure para que se actualice la IP publica sola
        'Me.Text = Me.Text + "| Version 1.7.1 | 09/02/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 03/05/2024 Se modifica como generar el archivo de notificaciones
        'Me.Text = Me.Text + "| Version 1.7.2 | 03/05/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 30/05/2024 Se agrega un try dentro de la creacion de archivo de notificaciones  ya que algunos reportes no aceptan los parametros con nombre y solo con indice
        'Me.Text = Me.Text + "| Version 1.7.3 | 30/05/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 31/05/2024 Se agrega un try dentro de la creacion de archivo de notificaciones  ya que algunos reportes no aceptan los parametros con nombre y solo con indice
        'Me.Text = Me.Text + "| Version 1.7.4 | 31/05/2024 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 05/11/2025 Se agrega un try dentro de la creacion de archivo de notificaciones  ya que algunos reportes no aceptan los parametros con nombre y solo con indice
        'Me.Text = Me.Text + "| Version 1.7.5 | 31/05/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 06/11/2025 Se cambia de libreria para enviar correos
        'Me.Text = Me.Text + "| Version 1.7.6 | 31/05/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 06/11/2025 Se actualizaron las librerias
        'Me.Text = Me.Text + "| Version 1.7.7 | 06/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 06/11/2025 Modificamos certificados
        'Me.Text = Me.Text + "| Version 1.7.8 | 06/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 11/11/2025 Regresamos la version a SmtpClient
        'Me.Text = Me.Text + "| Version 1.7.9 | 11/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 11/11/2025 Hacemos pruebas para el envio de las notificaciones mediante plantillas
        'Me.Text = Me.Text + "| Version 1.7.9.1 | 11/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 12/11/2025 Se implementa el sendmailapi para probar en producción
        'Me.Text = Me.Text + "| Version 1.8 | 12/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 12/11/2025 Se corrije bug al recorrer los archivos a adjuntar en sendmailapi
        'Me.Text = Me.Text + "| Version 1.8.1 | 12/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 12/11/2025 Se agrega un campo nuevo para saber la ruta web para agregarlo en los archivos adjuntos ya que estaba agregando la ruta del FTP
        'Me.Text = Me.Text + "| Version 1.8.2 | 12/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 12/11/2025 Se arregla bug en el parametro de correos al enviar sendmailapi ya que se estaba enviando un arreglo de string en vez de un string
        'Me.Text = Me.Text + "| Version 1.8.3 | 12/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 12/11/2025 Se quita que en el asunto reemplace los acentos en formato HTML y mejor solo se quitan 
        'Me.Text = Me.Text + "| Version 1.8.4 | 12/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 13/11/2025 Se quita que en el asunto reemplace los acentos en formato HTML y mejor solo se quitan 
        'Me.Text = Me.Text + "| Version 1.8.5 | 13/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 13/11/2025 se establece el usepasive en false para subir archivos al FTP
        'Me.Text = Me.Text + "| Version 1.8.6 | 19/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 25/11/2025 Se hacen ajustes al subir archivos al FTP para poder subirlo como FTP en las sucursales que este configurado como tal
        'Me.Text = Me.Text + "| Version 1.8.7 | 25/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 25/11/2025 faltaron logs y validacion en el catch de try dentro de la funcion que sube el archivo al servidor SFTP
        'Me.Text = Me.Text + "| Version 1.8.8 | 25/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 25/11/2025 falto un exit function al llamar la funcion de subir el archivo a un sftp
        'Me.Text = Me.Text + "| Version 1.8.9 | 25/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 26/11/2025 Se cambia el app.config y se copia el package.config
        'Me.Text = Me.Text + "| Version 1.9 | 26/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 26/11/2025 se quita dll y se agrega a mano
        'Me.Text = Me.Text + "| Version 1.9.1 | 26/11/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 02/12/2025 Se quita el subir archivos a FTP y se anexan adjuntos en el correo ya que la plantilla si lo permite
        'Me.Text = Me.Text + "| Version 1.9.2 | 02/12/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 02/12/2025 agregamos un redim al objeto attachments ya que si iba el valor null marcaba error
        'Me.Text = Me.Text + "| Version 1.9.3 | 02/12/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 03/12/2025 cambiamos un arreglo de objeto a una list de objeto para hacerlo como arraylist
        'Me.Text = Me.Text + "| Version 1.9.4 | 03/12/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        'Gilberto Madrid 04/12/2025 se agrega metodo para borrar logs, se agregan logs en la ejecucion de los ws y se quitan 2 ws que son obten ip publica y actualizar ip en azure
        Me.Text = Me.Text + "| Version 1.9.5 | 04/12/2025 | Servidor: " + Configuracion.Servidor + " | BD: " + Configuracion.BaseDeDatos + " |"

        NtiMensaje.Text = "| V1.9.5 | BD: " + Configuracion.BaseDeDatos

        'If Configuracion.FuncionPortal = True Then
        ' Me.Text = Me.Text + " USO PORTAL |"
        'NtiMensaje.Text = NtiMensaje.Text + " | USO PORTAL"
        '
        'CopiarArchivosParaPortalToolStripMenuItem.Visible = True
        'Else
        CopiarArchivosParaPortalToolStripMenuItem.Visible = False
        'End If

        conexion.Usuario = Usuario
        conexion.Contraseña = Contraseña

        'Gilberto Madrid 11/11/2025 lo usamos para pruebas
        'BorraLogs()
        'plSendMailApi("Notificaciones", "Alerta de Facturación con modificación en precios SEN8406155U8    (TIJUANA) (PEVC940201U77 - 11/11/2025 - $10,944.74 MXN)", "<b><font face=""arial"" color=#084B8A>Buen dia:</font></b><pre></pre><font face=""arial"" color=#084B8A>Le informamos que ha sido facturado por <b>$10,944.74 MXN</b> con modificaci&oacute;n en precios en la sucursal: <b>TIJUANA</b></font><pre></pre><font face=""arial"" color=#084B8A> . </font><pre></pre><font face=""arial"" color=#084B8A><b>Fecha:</b> <u>11/11/2025</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Folio:</b> <u>J366867</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Importe:</b> <u>$10,944.74 MXN</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Cliente:</b> <u>012725(001) CARLOS DARWIN PEREZ VAZQUEZ</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Agente: </b> <u>COTA  KEVIN</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Registrado por: </b> <u>KEVIN COTA</u></font><pre></pre><table border = ""1""><tr><th>Tipo</th><th>Articulo</th><th>Descripcion</th><th>Unidad</th><th>Moneda</th><th>Costo</th><th>Costo Flete</th><th>Costo Arancel</th><th>CostoFinal</th><th>PrecioFinal</th><th>Utilidad %</th><th>Precio Lista</th><th>Diferencia</th></tr><tr><td>A</td><td>CINT0007            </td><td>CINTA SUPER 33 *NACIONAL* 3/4"" X 20 MT</td><td>RLL</td><td>DOLAR</td><td>$3.47 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$3.47 USD</td><td>$3.85 USD</td><td>11.00</td><td>$3.85 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>CINT0037            </td><td>CINTA TEMFLEX 1600 NEGRA 3/4"" 18 MTS (UU009472471)</td><td>RLL</td><td>DOLAR</td><td>$0.90 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.90 USD</td><td>$0.99 USD</td><td>10.99</td><td>$0.99 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>SIEM0002            </td><td>Q120  BREAKER 1X20 QP</td><td>PZA</td><td>PESOS</td><td>$79.56 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$79.56 MXN</td><td>$87.21 MXN</td><td>9.61</td><td>$89.90 MXN</td><td>$-2.70 MXN</td></tr><tr><td>A</td><td>SIEM0003            </td><td>Q130 BREAKER 1X30 QP</td><td>PZA</td><td>PESOS</td><td>$79.56 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$79.56 MXN</td><td>$87.21 MXN</td><td>9.61</td><td>$89.90 MXN</td><td>$-2.70 MXN</td></tr><tr><td>A</td><td>COND0077            </td><td>ROLLO CABLE THHN 12, NEGRO 100 MTRS COND/VIAK</td><td>RLL</td><td>PESOS</td><td>$970.55 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$970.55 MXN</td><td>$983.95 MXN</td><td>1.38</td><td>$1,077.31 MXN</td><td>$-93.36 MXN</td></tr><tr><td>A</td><td>COND0078            </td><td>ROLLO CABLE THHN 12, BLANCO 100 MTRS COND/VIAK</td><td>RLL</td><td>PESOS</td><td>$970.55 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$970.55 MXN</td><td>$983.95 MXN</td><td>1.38</td><td>$1,077.31 MXN</td><td>$-93.36 MXN</td></tr><tr><td>A</td><td>VIAK0042            </td><td>ALAMBRE 2X12 UF PLANO VIAKON</td><td>RLL</td><td>PESOS</td><td>$2,282.28 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$2,282.28 MXN</td><td>$2,487.69 MXN</td><td>9.00</td><td>$2,487.69 MXN</td><td>$0.00 MXN</td></tr><tr><td>A</td><td>TPVC0031            </td><td>COPLE PVC 1/2 GRIS</td><td>PZA</td><td>DOLAR</td><td>$0.09 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.09 USD</td><td>$0.10 USD</td><td>14.00</td><td>$0.10 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>TPVC0048            </td><td>ADAPTER MACHO PVC 1 GRIS</td><td>PZA</td><td>DOLAR</td><td>$0.11 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.11 USD</td><td>$0.13 USD</td><td>14.00</td><td>$0.13 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>LEVI0020            </td><td>49875 ROSETA S/CADENA LEVITON</td><td>PZA</td><td>DOLAR</td><td>$1.29 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$1.29 USD</td><td>$1.46 USD</td><td>13.00</td><td>$1.46 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>TUBE0058            </td><td>643S COPLE STEEL 1</td><td>PZA</td><td>DOLAR</td><td>$0.47 USD</td><td>$0.00 USD</td><td>$0.02 USD</td><td>$0.50 USD</td><td>$0.58 USD</td><td>21.79</td><td>$0.58 USD</td><td>$0.00 USD</td></tr></table>", "sistemas.gerencia@sensa.com.mx,notificaciones@sensa.com.mx", "C:\Users\sistemas.gerencia\Desktop\pagoPC.pdf,C:\Users\sistemas.gerencia\Desktop\pagoPC.xml")
        'flSubirArchivoFTP("Notificaciones", "C:\Users\sistemas.gerencia\Desktop\pagoPC.pdf")
        'flSubirArchivoSFTP("NotificacionesSFTP", "C:\Users\sistemas.gerencia\Desktop\pagoPC.pdf")

        tmrNotificaciones.Enabled = True
        tmrTrabajos.Enabled = True
        tmrSolicitudAutorizacionCorreo.Enabled = True

        EscondeFormulario()

    End Sub
    Sub EscondeFormulario()

        Me.ShowIcon = True
        Me.ShowInTaskbar = True
        Me.Hide()

    End Sub

    Private Sub tmrNotificaciones_Tick(sender As System.Object, e As System.EventArgs) Handles tmrNotificaciones.Tick

        tmrNotificaciones.Enabled = False

        Try

            'Gilberto Madrid 09/08/2019 se agrego un nuevo parametro para saber si funcionara como notificador o como aplicacion para el portal
            'If Configuracion.FuncionPortal = False Then

            'Gilberto Madrid 27/09/2018 se agrega esto para que se comunique con el motor de timbrado y poder saber si esta abierta esta aplicacion o no
            ActualizaComunicacionMotor()

            BorraPDFS()
            'Gilberto Madrid 04/12/2025 agregamos el borrado de logs y que solo deje los ultimos 7 días
            BorraLogs()

            EjecutaNotificaciones()

            'End If

        Catch ex As Exception
            Loggers.e("Error ejecución notificaciones " + ex.Message, ex, MyClass.GetType.Name)
        End Try

        tmrNotificaciones.Enabled = True

    End Sub

    Sub ActualizaComunicacionMotor()

        conexion.fgRegresaConsultaDT("SELECT * FROM SES_ComunicacionServicios (NOLOCK)")

        If conexion.dtaux.Rows.Count > 0 Then

            conexion.IniciaTransaccion()

            conexion.EjecutaTexto("'UPDATE SES_ComunicacionServicios SET dFechaUltimaComunicacion=GETDATE(),bServicioDetenidoNotificado=0'")

            conexion.TerminaTransaccion()
        Else
            conexion.IniciaTransaccion()

            conexion.EjecutaTexto("'INSERT INTO SES_ComunicacionServicios (dFechaUltimaComunicacion,bServicioDetenidoNotificado,cMailNotificacionServicioDetenido) SELECT GETDATE(),0,''sistemas.gerencia@sensa.com.mx,sistemas@sensa.com.mx,sistemascln@sensa.com.mx'''")

            conexion.TerminaTransaccion()

        End If

    End Sub

    Sub EjecutaNotificaciones()

        conexion.TraeNotificaciones()

        If conexion.dtNotificaciones.Rows.Count > 0 Then

            Dim ExcluirColumnas As String
            Dim ColorearColumnas As String
            Dim ColorColumnas As String
            Dim IgnoraFechaHora As Boolean
            Dim bCancelaNotificacionParametrosNulos As Boolean
            'Dim dtEjecutaNotificacion As DataTable
            'Dim dtaux As DataTable
            Dim NombreNotificacion As String
            Dim EjecutaNotificacion As String
            Dim ParametrosNotificacion As String
            Dim PrevioNotificacion As String
            Dim FechaInicio As Date
            Dim FechaUltimaEjecucion As Date
            Dim HoraEjecucion As String
            Dim FrecuenciaEjecucion As String
            Dim MandaCorreo As Boolean
            'Dim AsuntoCorreo As String
            'Dim CuerpoCorreo As String
            Dim vlBandera As Boolean
            'Dim CorreosNotificacion As String
            Dim cReporte As String
            Dim cProcedimientoReporte As String
            Dim cNombreParametros As String

            For i As Integer = 0 To conexion.dtNotificaciones.Rows.Count - 1

                ParametrosNotificacion = ""
                vlBandera = True
                ExcluirColumnas = conexion.dtNotificaciones.Rows(i)("cExcluirColumnas")
                ColorearColumnas = conexion.dtNotificaciones.Rows(i)("cColoreaColumnas")
                ColorColumnas = conexion.dtNotificaciones.Rows(i)("cColorColumnas")
                IgnoraFechaHora = conexion.dtNotificaciones.Rows(i)("bIgnoraFechaHoraEjecucion")
                bCancelaNotificacionParametrosNulos = conexion.dtNotificaciones.Rows(i)("bCancelarNotificacionParametrosNulos")
                NombreNotificacion = conexion.dtNotificaciones.Rows(i)("cNombreNotificacion")
                EjecutaNotificacion = conexion.dtNotificaciones.Rows(i)("cSQL")
                PrevioNotificacion = conexion.dtNotificaciones.Rows(i)("cPrevioEjecutar")
                FechaInicio = conexion.dtNotificaciones.Rows(i)("dFechaInicio")
                FechaUltimaEjecucion = conexion.dtNotificaciones.Rows(i)("dFechaUltimaEjecucion")
                HoraEjecucion = conexion.dtNotificaciones.Rows(i)("cHoraEjecucion")
                FrecuenciaEjecucion = conexion.dtNotificaciones.Rows(i)("cFrecuenciaEjecucion")
                MandaCorreo = conexion.dtNotificaciones.Rows(i)("bMandaCorreo")
                cReporte = IIf(IsDBNull(conexion.dtNotificaciones.Rows(i)("cReporte")) = True, "", conexion.dtNotificaciones.Rows(i)("cReporte"))
                cProcedimientoReporte = IIf(IsDBNull(conexion.dtNotificaciones.Rows(i)("cProcedimientoReporte")) = True, "", conexion.dtNotificaciones.Rows(i)("cProcedimientoReporte"))
                cNombreParametros = IIf(IsDBNull(conexion.dtNotificaciones.Rows(i)("cNombresParametros")) = True, "", conexion.dtNotificaciones.Rows(i)("cNombresParametros"))

                If Trim(PrevioNotificacion) <> "" Then
                    conexion.IniciaTransaccion()
                    conexion.EjecutaTexto(Trim(PrevioNotificacion))
                    If conexion.Mensaje <> "" Then
                        conexion.HacerRollBack()
                        vlBandera = False
                    Else
                        conexion.TerminaTransaccion()
                    End If
                Else
                    ParametrosNotificacion = conexion.dtNotificaciones.Rows(i)("cParametros")
                End If

                If vlBandera = True Then

                    conexion.TraeNotificacion(NombreNotificacion)

                    If conexion.dtNotificacionesaux.Rows.Count > 0 Then

                        ParametrosNotificacion = IIf(IsDBNull(conexion.dtNotificacionesaux.Rows(0)("cParametros")) = True, "", conexion.dtNotificacionesaux.Rows(0)("cParametros"))

                    End If

                    If bCancelaNotificacionParametrosNulos = True And ParametrosNotificacion = "" Then
                        Continue For
                    End If

                    If IgnoraFechaHora = False Then

                        Select Case FrecuenciaEjecucion

                            Case "Diario"

                                If FechaUltimaEjecucion.ToString("yyyy-MM-dd") < Date.Now.ToString("yyyy-MM-dd") Then

                                    If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                        plEjecutaNotificacion(EjecutaNotificacion, ParametrosNotificacion, NombreNotificacion, ExcluirColumnas, ColorearColumnas, ColorColumnas, MandaCorreo, cReporte, cProcedimientoReporte, cNombreParametros)
                                    End If

                                End If

                            Case "Semanal"

                                If DateDiff(DateInterval.Day, FechaUltimaEjecucion, Date.Now) >= 7 Then
                                    If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                        plEjecutaNotificacion(EjecutaNotificacion, ParametrosNotificacion, NombreNotificacion, ExcluirColumnas, ColorearColumnas, ColorColumnas, MandaCorreo, cReporte, cProcedimientoReporte, cNombreParametros)
                                    End If
                                End If

                            Case "Mensual"
                                If FechaUltimaEjecucion.ToString("yyyy-MM") < Date.Now.ToString("yyyy-MM") Then

                                    'Gilberto Madrid 05/04/2017 agregamos esto para preguntar por el dia de la fecha de la ultima ejecucion,
                                    'si el dia de 
                                    If FechaUltimaEjecucion.ToString("dd") <= Date.Now.ToString("dd") Then

                                        If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                            plEjecutaNotificacion(EjecutaNotificacion, ParametrosNotificacion, NombreNotificacion, ExcluirColumnas, ColorearColumnas, ColorColumnas, MandaCorreo, cReporte, cProcedimientoReporte, cNombreParametros)
                                        End If

                                    End If

                                End If


                        End Select

                    Else

                        plEjecutaNotificacion(EjecutaNotificacion, ParametrosNotificacion, NombreNotificacion, ExcluirColumnas, ColorearColumnas, ColorColumnas, MandaCorreo, cReporte, cProcedimientoReporte, cNombreParametros)

                    End If

                End If

            Next

        End If

    End Sub

    Sub plEjecutaNotificacion(cNotificacionSQL As String, cParametrosNotificacion As String, cNombreNotificacion As String, cExcluirColumnas As String, cColoreaColumnas As String, cColorColumnas As String, bMandaCorreo As Boolean, cReporte As String, cProcedimientoReporte As String, cNombresParametros As String)

        Dim AsuntoCorreo As String
        Dim CuerpoCorreo As String
        Dim PieCorreo As String
        Dim dtResultadosNotificacion As DataTable
        Dim CorreosNotificacion
        Dim ParametrosSeparados
        Dim NombresParametrosSeparados
        Dim vlNombreArchivo As String
        Dim ArchivoNotificacion As Attachment
        vlNombreArchivo = ""

        'Gilberto Madrid 26/06/2018 obtenemos el usuario que registro lo que se esta notificando
        Dim UsuarioRegistro As String
        UsuarioRegistro = ""

        Try
            conexion.EjecutaNotificacion(cNotificacionSQL, cParametrosNotificacion)

            If conexion.dtResultadorNotificacion.Rows.Count > 0 Then

                dtResultadosNotificacion = conexion.dtResultadorNotificacion

                conexion.TraeNotificacion(cNombreNotificacion)

                CuerpoCorreo = IIf(IsDBNull(conexion.dtNotificacionesaux.Rows(0)("cCuerpoCorreo")) = True, "", conexion.dtNotificacionesaux.Rows(0)("cCuerpoCorreo"))
                AsuntoCorreo = IIf(IsDBNull(conexion.dtNotificacionesaux.Rows(0)("cAsuntoCorreo")) = True, "", conexion.dtNotificacionesaux.Rows(0)("cAsuntoCorreo"))

                'Gilberto Madrid 27/02/2016 se agrega al final del correo
                PieCorreo = IIf(IsDBNull(conexion.dtNotificacionesaux.Rows(0)("cPieCorreo")) = True, "", conexion.dtNotificacionesaux.Rows(0)("cPieCorreo"))

                CuerpoCorreo = CuerpoCorreo + CreaHTMLDesdeDataTable(dtResultadosNotificacion, cExcluirColumnas, cColoreaColumnas, cColorColumnas) + PieCorreo

                CorreosNotificacion = dtResultadosNotificacion.Rows(0)("CorreoNotificacion").ToString.Split(",")

                UsuarioRegistro = dtResultadosNotificacion.Rows(0)("UsuRegNotificacion").ToString

                If cReporte.Trim <> "" Then

                    ParametrosSeparados = cParametrosNotificacion.Replace("'", "").Split(",")
                    NombresParametrosSeparados = cNombresParametros.Split(",")
                    conexion.EjecutaProcedimiento(cProcedimientoReporte, ParametrosSeparados, NombresParametrosSeparados)
                    If conexion.Mensaje = "" Then
                        If conexion.dtaux.Rows.Count > 0 Then
                            vlNombreArchivo = CreaArchivoNotificacion(cReporte, ParametrosSeparados, NombresParametrosSeparados)
                        Else
                            vlNombreArchivo = ""
                        End If
                    Else
                        vlNombreArchivo = ""
                    End If

                End If

                If bMandaCorreo = True And DirectCast(CorreosNotificacion, String()).Length > 0 Then
                    'Gilberto Madrid 12/11/2025 si esta encendido este parametro se manda por sendmailapi
                    If Configuracion.FuncionPortal = False Then
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                        correo = New MailMessage
                        servidor = New SmtpClient

                        correo.From = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)
                        For j As Integer = 0 To DirectCast(CorreosNotificacion, String()).Length - 1
                            If CorreosNotificacion(j).Trim() <> "" Then
                                correo.CC.Add(CorreosNotificacion(j).Trim())
                            End If
                        Next

                        If vlNombreArchivo.Trim <> "" Then

                            ArchivoNotificacion = New Attachment(vlNombreArchivo)
                            correo.Attachments.Add(ArchivoNotificacion)

                        End If

                        correo.Subject = AsuntoCorreo
                        correo.Body = CuerpoCorreo
                        correo.IsBodyHtml = True

                        correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                        servidor.Host = Configuracion.ServidorSalidaCorreo
                        servidor.Port = CInt(Configuracion.PuertoCorreo)
                        servidor.EnableSsl = Configuracion.UsaSSL
                        servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                        servidor.Send(correo)
                    Else
                        'Gilberto Madrid 02/12/2025 quitamos esto ya que la plantilla permite mandar archivos adjuntos
                        'Dim vlArchivosFTP As String
                        'vlArchivosFTP = flSubirArchivoFTP("Notificaciones", vlNombreArchivo)
                        plSendMailApi("Notificaciones", AsuntoCorreo, CuerpoCorreo, dtResultadosNotificacion.Rows(0)("CorreoNotificacion"), vlNombreArchivo)
                    End If


                    'If vlNombreArchivo <> "" Then
                    '    Kill(vlNombreArchivo)
                    'End If

                End If

                'Gilberto Madrid 14/07/2020 cambiamos de lugar el guardado de la notificacion en el historial ya que si no hay internet y no se envía el notificador no intenta enviarlo nuevamente
                GuardaHistorialNotificacion(cNombreNotificacion.Trim, cParametrosNotificacion.Trim, AsuntoCorreo.Trim, CuerpoCorreo.Trim, PieCorreo.Trim, dtResultadosNotificacion.Rows(0)("CorreoNotificacion").ToString.Trim, vlNombreArchivo.Trim, UsuarioRegistro)

            End If
        Catch ex As Exception
            Loggers.e("Error al enviar correo de notificaciones " + cNotificacionSQL + " " + ex.Message, ex, MyClass.GetType.Name)
        End Try
    End Sub

    Sub GuardaHistorialNotificacion(prmNombreNotificacion As String, prmCodigo As String, prmAsunto As String, prmCuerpo As String, prmPie As String, Optional prmCorreosNotificacion As String = "", Optional prmArchivoNotificacion As String = "", Optional prmUsuarioRegistro As String = "")

        conexion.IniciaTransaccion()
        If conexion.Mensaje = "" Then
            conexion.GuardaHistorialNotificaciones(prmNombreNotificacion, prmCodigo, prmAsunto, prmCuerpo, prmPie, prmCorreosNotificacion, prmArchivoNotificacion, prmUsuarioRegistro)
            If conexion.Mensaje = "" Then
                conexion.TerminaTransaccion()
            Else
                conexion.HacerRollBack()
            End If
        Else
            conexion.HacerRollBack()
        End If

    End Sub

    Function CreaArchivoNotificacion(cNombreReporte As String, cParametros As String(), Optional cNombresParametros As String() = Nothing) As String
        Dim documento As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim filedest As CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim OpcionExportar As CrystalDecisions.Shared.ExportOptions
        Dim NombreArchivo As String
        NombreArchivo = ""
        Try
            documento = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            documento.Load(Application.StartupPath + "\" + cNombreReporte)



            For i As Integer = 0 To DirectCast(cParametros, String()).Length - 1

                Try
                    If cNombresParametros Is Nothing Then
                        documento.SetParameterValue(i, cParametros(i))
                    Else
                        documento.SetParameterValue(cNombresParametros(i), cParametros(i))
                    End If
                Catch ex As Exception
                    documento.SetParameterValue(i, cParametros(i))
                End Try

            Next
            documento.DataSourceConnections(0).SetConnection(Configuracion.Servidor, Configuracion.BaseDeDatos, False)
            documento.SetDatabaseLogon(conexion.Usuario, conexion.Contraseña)
            filedest = New CrystalDecisions.Shared.DiskFileDestinationOptions
            OpcionExportar = New CrystalDecisions.Shared.ExportOptions
            OpcionExportar.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            OpcionExportar.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            filedest.DiskFileName = Application.StartupPath + "\ArchivoNotificacion_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"
            NombreArchivo = filedest.DiskFileName
            OpcionExportar.ExportDestinationOptions = filedest.Clone
            documento.Export(OpcionExportar)
            documento.Dispose()
            Loggers.d("Archivo creado correctamente", MyClass.GetType().Name)
            Return NombreArchivo
        Catch ex As Exception
            Loggers.e("Error al crear archivo de notificaciones " + ex.Message, ex, MyClass.GetType.Name)
            Return ""
        End Try
    End Function

    Function CreaHTMLDesdeDataTable(dt As DataTable, ExcluyeColumnas As String, ColoreaColumnas As String, ColorColumnas As String) As String

        Dim html As New StringBuilder()
        Dim vlCampoAgregado As Boolean

        vlCampoAgregado = False

        html.Append("<table border = '1'>")
        html.Append("<tr>")

        For Each column As DataColumn In dt.Columns

            If ExcluyeColumnas.Contains(column.ColumnName) = False Then
                vlCampoAgregado = True
                html.Append("<th>")
                html.Append(column.ColumnName)
                html.Append("</th>")

            End If
        Next
        html.Append("</tr>")

        For Each row As DataRow In dt.Rows
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns

                If ExcluyeColumnas.Contains(column.ColumnName) = False Then
                    vlCampoAgregado = True
                    If ColoreaColumnas.Contains(column.ColumnName) = False Then
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Else
                        html.Append("<td><FONT COLOR=" + ColorColumnas + ">")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    End If

                End If
            Next
            html.Append("</tr>")
        Next

        html.Append("</table>")

        If vlCampoAgregado = True Then
            Return html.ToString()
        Else
            Return ""
        End If

    End Function

    Sub BorraPDFS()
        Try
            Dim Archivos = Directory.GetFiles(My.Application.Info.DirectoryPath, "*.pdf")
            For Each archivo As String In Archivos
                If Math.Abs(DateDiff(DateInterval.Day, File.GetCreationTime(archivo), Date.Now)) >= 1 Then
                    File.Delete(archivo)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub BorraLogs()
        Try
            Dim Archivos = Directory.GetFiles(My.Application.Info.DirectoryPath + "\logs", "*.log")
            For Each archivo As String In Archivos
                If Math.Abs(DateDiff(DateInterval.Day, File.GetCreationTime(archivo), Date.Now)) >= 7 Then
                    File.Delete(archivo)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tmrTrabajos_Tick(sender As System.Object, e As System.EventArgs) Handles tmrTrabajos.Tick

        tmrTrabajos.Enabled = False

        Try

            'Gilberto Madrid 09/08/2019 se agrego un nuevo parametro para saber si funcionara como notificador o como aplicacion para el portal
            'If Configuracion.FuncionPortal = False Then
            EjecutaTrabajosSQL()
            'Gilberto Madrid 11/11/2025 se quito el else ya que se bajo la pagina de sensaelectrica
            'Else
            'Gilberto Madrid 30/07/2019 agregamos proceso para copiado de archivos electronicos para clientes del portal
            'Gilberto Madrid 08/08/2019 se queda temporalmente manual
            'Gilberto Madrid 09/08/2019 se habilito de nuevo
            'Gilberto Madrid 10/08/2019 se agrego un parametro para omitir la pregunta
            'Gilberto Madrid 05/11/2025 se quito esto ya que se dio de baja la pagina
            'plCopiaArchivosRutaPortal(True)
            'Gilberto Madrid 07/09/2020 se agrega la ejecucion de los webservice para comunicarnos con los proveedores
            plEjecutaWebServices()

            'End If


        Catch ex As Exception

            Loggers.e("Error ejecución trabajos " + ex.Message, ex, MyClass.GetType.Name)

        End Try

        tmrTrabajos.Enabled = True

    End Sub

    Sub plEjecutaWebServices()

        conexion.fgTraeWebServices()

        If conexion.Mensaje = "" Then

            Dim IgnoraFechaHora As Boolean
            Dim FrecuenciaEjecucion As String
            Dim FechaUltimaEjecucion As DateTime
            Dim HoraEjecucion As String
            Dim vlCodigoSQL As String
            Dim vlParametrosSQL As String
            Dim vlNombreWS As String
            Dim vlExcluirColumnas As String

            Dim cUrl As String
            Dim cMetodoConsumo As String
            Dim cTipoContenido As String
            Dim cCabeceros As String

            IgnoraFechaHora = False

            For i As Integer = 0 To conexion.dtWebServices.Rows.Count - 1

                FrecuenciaEjecucion = conexion.dtWebServices.Rows(i)("cFrecuenciaEjecucion").ToString.Trim
                FechaUltimaEjecucion = DateTime.Parse(conexion.dtWebServices.Rows(i)("dFechaUltimaEjecucion").ToString.Trim)
                HoraEjecucion = conexion.dtWebServices.Rows(i)("cHoraEjecucion").ToString.Trim

                vlCodigoSQL = conexion.dtWebServices.Rows(i)("cSQL").ToString.Trim
                vlParametrosSQL = conexion.dtWebServices.Rows(i)("cParametros").ToString.Trim
                vlNombreWS = conexion.dtWebServices.Rows(i)("cNombreTrabajo").ToString.Trim
                vlExcluirColumnas = ""

                cUrl = conexion.dtWebServices.Rows(i)("cUrl").ToString.Trim
                cMetodoConsumo = conexion.dtWebServices.Rows(i)("cMetodoConsumo").ToString.Trim
                cTipoContenido = conexion.dtWebServices.Rows(i)("cTipoContenido").ToString.Trim
                cCabeceros = conexion.dtWebServices.Rows(i)("cCabeceros").ToString.Trim


                If IgnoraFechaHora = False Then

                    Select Case FrecuenciaEjecucion

                        Case "Diario"

                            If FechaUltimaEjecucion.ToString("yyyy-MM-dd") <= Date.Now.ToString("yyyy-MM-dd") Then

                                If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                    plEjecutaWS(vlCodigoSQL, vlParametrosSQL, vlNombreWS, vlExcluirColumnas, cUrl, cMetodoConsumo, cTipoContenido, cCabeceros)
                                End If

                            End If

                        Case "Semanal"

                            If DateDiff(DateInterval.Day, FechaUltimaEjecucion, Date.Now) >= 7 Then
                                If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                    plEjecutaWS(vlCodigoSQL, vlParametrosSQL, vlNombreWS, vlExcluirColumnas, cUrl, cMetodoConsumo, cTipoContenido, cCabeceros)
                                End If
                            End If

                        Case "Mensual"
                            If FechaUltimaEjecucion.ToString("yyyy-MM") < Date.Now.ToString("yyyy-MM") Then

                                If FechaUltimaEjecucion.ToString("dd") <= Date.Now.ToString("dd") Then

                                    If Date.Now.ToString("HH:ss") > HoraEjecucion Then
                                        plEjecutaWS(vlCodigoSQL, vlParametrosSQL, vlNombreWS, vlExcluirColumnas, cUrl, cMetodoConsumo, cTipoContenido, cCabeceros)
                                    End If

                                End If

                            End If


                    End Select

                Else

                    plEjecutaWS(vlCodigoSQL, vlParametrosSQL, vlNombreWS, vlExcluirColumnas, cUrl, cMetodoConsumo, cTipoContenido, cCabeceros)

                End If

            Next

        End If


    End Sub
    Sub plEjecutaWS(prmCodigoSQLEjecuta As String, prmParametrosWS As String, prmNombreWS As String, prmExcluirColumnas As String, Optional prmUrl As String = "", Optional prmCmetodo As String = "", Optional prmCtipoContenido As String = "", Optional prmCCabeceros As String = "")

        Dim dtResultadoWS As DataTable
        Dim vlCuerpoCorreo As String
        Dim vlFechaHoraInicio As DateTime
        Dim vlFechaHoraFin As DateTime

        Try
            vlFechaHoraInicio = Now
            conexion.EjecutaWS(prmCodigoSQLEjecuta, prmParametrosWS)

            If conexion.dtResultadoWS.Rows.Count > 0 Then

                dtResultadoWS = conexion.dtResultadoWS

                Select Case prmNombreWS

                    Case "EnviaDatosSiemensInventarioWS"

                        Loggers.d("Se ejecutara el servicio web EnviaDatosSiemensInventarioWS", MyClass.GetType.Name)

                        'Gilberto Madrid 16/12/2020 el proveedor cambio de servidor el webservice que entrara forzosamente en el 2021
                        Dim vlServicioWeb As New WSSiemens2020.Service1
                        'Dim vlServicioWeb As New WSSiemens.Service1
                        'Dim vlServicioWeb As New PruebasWSSiemens.Service1

                        'Gilberto Madrid 16/12/2020 el proveedor cambio de servidor el webservice que entrara forzosamente en el 2021
                        Dim vlMensajeServicioWeb As WSSiemens2020.MessagesResult
                        'Dim vlMensajeServicioWeb As WSSiemens.MessagesResult
                        'Dim vlMensajeServicioWeb As PruebasWSSiemens.MessagesResult


                        Dim bytValue() As Byte

                        'Gilberto Madrid 30/11/2020 vector de prueba
                        'Dim bytKey() As Byte = {79, 242, 104, 80, 218, 138, 178, 203, 173, 112, 10, 162, 102, 225, 59, 30, 143, 158, 244, 198, 242, 56, 68, 73, 186, 210, 224, 238, 95, 203, 124, 139}
                        Dim bytKey() As Byte = {198, 236, 93, 174, 190, 115, 137, 224, 105, 200, 175, 82, 92, 231, 129, 19, 161, 23, 60, 224, 169, 0, 228, 222, 46, 7, 195, 240, 42, 16, 244, 250}

                        Dim bytEncoded() As Byte

                        'Gilberto Madrid 30/11/2020 vector de prueba    
                        'Dim bytIV() As Byte = {243, 117, 38, 243, 155, 152, 230, 196, 211, 248, 88, 100, 201, 153, 217, 120}
                        Dim bytIV() As Byte = {223, 47, 110, 137, 208, 171, 140, 53, 160, 108, 81, 131, 159, 194, 158, 43}

                        Dim textoJson As String

                        Dim ListaProductos As New List(Of ProductoWSInventario)
                        Dim nuevoProducto As ProductoWSInventario

                        For Each Renglon As DataRow In dtResultadoWS.Rows

                            nuevoProducto = New ProductoWSInventario
                            With nuevoProducto

                                .CustomerNumberSAP = Renglon("cNumeroClienteSAP").ToString.Trim
                                .ProductoId = Renglon("CARTICULO").ToString.Trim
                                .PartNumber = Renglon("CDESCORTA").ToString.Trim
                                .NetExistence = Renglon("NEXISTENCIA")
                                .StoreLocation = Renglon("cLugarExpedicion").ToString.Trim
                                .StoreName = Renglon("cNombreTienda").ToString.Trim
                                .DateExtraction = Now.ToString("yyyy-MM-dd HH:mm:ss")

                            End With

                            ListaProductos.Add(nuevoProducto)

                        Next

                        textoJson = Json.JsonConvert.SerializeObject(ListaProductos, Json.Formatting.None)

                        Loggers.d("Este es el json que se enviara al servicio web EnviaDatosSiemensInventarioWS" + vbCr + textoJson, MyClass.GetType.Name)

                        bytValue = Encoding.UTF8.GetBytes(textoJson.ToCharArray)

                        bytEncoded = EncryptSiemens.Encryption.EncryptToBytes_AesCustomer(bytValue, bytKey, bytIV)

                        vlMensajeServicioWeb = vlServicioWeb.RegisterPartnerInventoryT(dtResultadoWS.Rows(0)("cNumeroClienteSAP").ToString.Trim, bytEncoded)

                        If vlMensajeServicioWeb.ProcessEstatus.ToUpper.Trim() = "COMPLETO" Then

                            vlFechaHoraFin = Now
                            'aqui guardamos en el historial
                            conexion.IniciaTransaccion()
                            conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, textoJson)
                            conexion.TerminaTransaccion()

                            Loggers.d("WS EnviaDatosSiemensInventarioWS ejecutado correctamente", MyClass.GetType.Name)

                        End If

                    Case "EnviaDatosSiemensVentaWS"

                        Loggers.d("Se ejecutara el servicio web EnviaDatosSiemensVentaWS", MyClass.GetType.Name)

                        'Gilberto Madrid 16/12/2020 el proveedor cambio de servidor el webservice que entrara forzosamente en el 2021
                        'Dim vlServicioWeb As New WSSiemens.Service1
                        'Dim vlServicioWeb As New PruebasWSSiemens.Service1
                        Dim vlServicioWeb As New WSSiemens2020.Service1
                        vlServicioWeb.Timeout = 6000000

                        'Gilberto Madrid 16/12/2020 el proveedor cambio de servidor el webservice que entrara forzosamente en el 2021
                        'Dim vlMensajeServicioWeb As WSSiemens.MessagesResult
                        'Dim vlMensajeServicioWeb As PruebasWSSiemens.MessagesResult
                        Dim vlMensajeServicioWeb As WSSiemens2020.MessagesResult


                        Dim bytValue() As Byte
                        Dim bytKey() As Byte = {79, 242, 104, 80, 218, 138, 178, 203, 173, 112, 10, 162, 102, 225, 59, 30, 143, 158, 244, 198, 242, 56, 68, 73, 186, 210, 224, 238, 95, 203, 124, 139}
                        Dim bytEncoded() As Byte
                        Dim bytIV() As Byte = {243, 117, 38, 243, 155, 152, 230, 196, 211, 248, 88, 100, 201, 153, 217, 120}
                        Dim textoJson As String

                        Dim ListaProductos As New List(Of ProductoWSVentas)
                        Dim nuevoProducto As ProductoWSVentas

                        For Each Renglon As DataRow In dtResultadoWS.Rows

                            nuevoProducto = New ProductoWSVentas

                            With nuevoProducto

                                .CustomerNumberSAP = Renglon("cNumeroClienteSAP").ToString.Trim
                                .ProductoId = Renglon("CARTICULO").ToString.Trim
                                .PartNumber = Renglon("CDESCORTA").ToString.Trim
                                '.SerialNumber = ""
                                .SaleDate = DateTime.Parse(Renglon("DFECHA").ToString.Trim).ToString("yyyy-MM-dd HH:mm:ss")
                                '.CustomerIndustrySector = ""
                                '.ProcessApply = ""
                                .Quantity = Renglon("NCANTIDAD")
                                '.EndCustomerName = ""
                                '.Type = ""


                            End With


                            ListaProductos.Add(nuevoProducto)

                        Next

                        textoJson = Json.JsonConvert.SerializeObject(ListaProductos, Json.Formatting.None)

                        Loggers.d("Este es el json que se enviara al servicio web EnviaDatosSiemensVentaWS" + vbCr + textoJson, MyClass.GetType.Name)

                        bytValue = Encoding.UTF8.GetBytes(textoJson.ToCharArray)

                        bytEncoded = EncryptSiemens.Encryption.EncryptToBytes_AesCustomer(bytValue, bytKey, bytIV)

                        vlMensajeServicioWeb = vlServicioWeb.RegisterPartnerSalesT(dtResultadoWS.Rows(0)("cNumeroClienteSAP").ToString.Trim, bytEncoded)

                        If vlMensajeServicioWeb.ProcessEstatus.ToUpper.Trim() = "COMPLETO" Then

                            vlFechaHoraFin = Now
                            'aqui guardamos en el historial
                            conexion.IniciaTransaccion()
                            conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, textoJson)
                            conexion.TerminaTransaccion()

                            Loggers.d("WS EnviaDatosSiemensVentaWS ejecutado correctamente", MyClass.GetType.Name)

                        End If

                    Case "EnviaDatosSiemensVentaWS_REST"

                        Dim uri As Uri
                        Dim vlCuerpo As String

                        Loggers.d("Se ejecutara el servicio web EnviaDatosSiemensVentaWS_REST", MyClass.GetType.Name)

                        uri = New Uri(prmUrl)
                        vlCuerpo = Newtonsoft.Json.JsonConvert.SerializeObject(dtResultadoWS, Newtonsoft.Json.Formatting.None)

                        Loggers.d("El cuerpo que se enviara al servicio EnviaDatosSiemensVentaWS_REST - " + vbCr + vlCuerpo, MyClass.GetType.Name)

                        Dim data As Byte() = Encoding.UTF8.GetBytes(vlCuerpo)
                        Dim vlRequest As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)

                        Select Case prmCmetodo
                            Case "GET"
                                vlRequest.Method = HttpMethod.Get.Method
                            Case "POST"
                                vlRequest.Method = HttpMethod.Post.Method
                            Case "PUT"
                                vlRequest.Method = HttpMethod.Put.Method
                            Case "DELETE"
                                vlRequest.Method = HttpMethod.Delete.Method

                        End Select

                        vlRequest.ContentType = prmCtipoContenido
                        vlRequest.ContentLength = data.Length

                        Dim vlCabeceros() As CabecerosRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(Of CabecerosRequest())(prmCCabeceros)

                        For Each cab As CabecerosRequest In vlCabeceros
                            vlRequest.Headers.Add(cab.cabecero, cab.valor)
                        Next

                        Dim stream As Stream = vlRequest.GetRequestStream()

                        stream.Write(data, 0, data.Length)
                        stream.Close()

                        Dim result As HttpWebResponse = CType(vlRequest.GetResponse(), HttpWebResponse)

                        Dim streamResponse = result.GetResponseStream()

                        Dim reader = New StreamReader(streamResponse)


                        Dim receiveContent = reader.ReadToEnd()

                        reader.Close()

                        If result.StatusCode >= HttpStatusCode.OK And result.StatusCode < HttpStatusCode.Ambiguous Then

                            vlFechaHoraFin = Now
                            'aqui guardamos en el historial
                            conexion.IniciaTransaccion()
                            conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, vlCuerpo)
                            conexion.TerminaTransaccion()

                            Loggers.d("WS EnviaDatosSiemensVentaWS_REST ejecutado correctamente", MyClass.GetType.Name)
                        Else
                            Loggers.d("Error al ejecutar el servicio EnviaDatosSiemensVentaWS_REST - " + result.StatusCode.ToString + " - " + receiveContent, MyClass.GetType.Name)
                        End If

                    'Case "ObtenerIPPublica"

                    '    Dim uri As Uri
                    '    Dim vlCuerpo As String

                    '    uri = New Uri(prmUrl)

                    '    Dim vlRequest As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)

                    '    Select Case prmCmetodo
                    '        Case "GET"
                    '            vlRequest.Method = HttpMethod.Get.Method
                    '        Case "POST"
                    '            vlRequest.Method = HttpMethod.Post.Method
                    '        Case "PUT"
                    '            vlRequest.Method = HttpMethod.Put.Method
                    '        Case "DELETE"
                    '            vlRequest.Method = HttpMethod.Delete.Method

                    '    End Select

                    '    vlRequest.ContentType = prmCtipoContenido

                    '    Dim result As HttpWebResponse = CType(vlRequest.GetResponse(), HttpWebResponse)

                    '    Dim streamResponse = result.GetResponseStream()

                    '    Dim reader = New StreamReader(streamResponse)

                    '    Dim receiveContent = reader.ReadToEnd()

                    '    reader.Close()

                    '    If result.StatusCode >= HttpStatusCode.OK And result.StatusCode < HttpStatusCode.Ambiguous Then

                    '        vlFechaHoraFin = Now
                    '        'aqui guardamos en el historial
                    '        conexion.IniciaTransaccion()
                    '        conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, receiveContent)
                    '        conexion.TerminaTransaccion()

                    '        Loggers.d("WS ObtenerIPPublica ejecutado correctamente", MyClass.GetType.Name)

                    '    End If

                    'Case "ActualizaIPFirewallAzure"

                    '    Dim vlObtenerTokenAzure As String

                    '    vlObtenerTokenAzure = flObtenerTokenAzure()

                    '    If vlObtenerTokenAzure.Trim = "" Then
                    '        Exit Sub
                    '    End If

                    '    Dim uri As Uri
                    '    Dim vlCuerpo As String

                    '    uri = New Uri(prmUrl)
                    '    vlCuerpo = dtResultadoWS.Rows(0)("ip").ToString.Trim

                    '    'Gilberto Madrid 09/02/2024 validamos que no nos regrese 0 ya que en ese caso no pudo obtener la IP
                    '    If Trim(vlCuerpo) = "0" Then
                    '        Loggers.d("WS ActualizaIPFirewallAzure no se actualizo la ip ya que el store regreso 0", MyClass.GetType.Name)
                    '    End If

                    '    Dim data As Byte() = Encoding.UTF8.GetBytes(vlCuerpo)
                    '    Dim vlRequest As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)

                    '    Select Case prmCmetodo
                    '        Case "GET"
                    '            vlRequest.Method = HttpMethod.Get.Method
                    '        Case "POST"
                    '            vlRequest.Method = HttpMethod.Post.Method
                    '        Case "PUT"
                    '            vlRequest.Method = HttpMethod.Put.Method
                    '        Case "DELETE"
                    '            vlRequest.Method = HttpMethod.Delete.Method

                    '    End Select

                    '    vlRequest.ContentType = prmCtipoContenido
                    '    vlRequest.ContentLength = data.Length


                    '    vlRequest.Headers.Add("Authorization", vlObtenerTokenAzure)

                    '    Dim stream As Stream = vlRequest.GetRequestStream()

                    '    stream.Write(data, 0, data.Length)
                    '    stream.Close()

                    '    Dim result As HttpWebResponse = CType(vlRequest.GetResponse(), HttpWebResponse)

                    '    Dim streamResponse = result.GetResponseStream()

                    '    Dim reader = New StreamReader(streamResponse)


                    '    Dim receiveContent = reader.ReadToEnd()

                    '    reader.Close()

                    '    If result.StatusCode >= HttpStatusCode.OK And result.StatusCode < HttpStatusCode.Ambiguous Then

                    '        vlFechaHoraFin = Now
                    '        'aqui guardamos en el historial
                    '        conexion.IniciaTransaccion()
                    '        conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, vlCuerpo)
                    '        conexion.TerminaTransaccion()

                    '        Loggers.d("WS ActualizaIPFirewallAzure ejecutado correctamente", MyClass.GetType.Name)

                    '    End If

                    Case "EjecutaPlantillaMailing"

                        Dim uri As Uri
                        Dim vlCuerpo As String

                        uri = New Uri(prmUrl)
                        vlCuerpo = dtResultadoWS.Rows(0)("Body").ToString.Trim

                        If Trim(vlCuerpo) = "" Then
                            Loggers.d("No se obtuvo resultados del query para la ejecución de la plantilla", MyClass.GetType.Name)
                            conexion.IniciaTransaccion()
                            conexion.EjecutaTexto("UPDATE SES_Trabajos_WS SET bActivo=0 WHERE cNombreTrabajo='EjecutaPlantillaMailing'")
                            conexion.TerminaTransaccion()
                        End If

                        Dim data As Byte() = Encoding.UTF8.GetBytes(vlCuerpo)
                        Dim vlRequest As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)

                        Select Case prmCmetodo
                            Case "GET"
                                vlRequest.Method = HttpMethod.Get.Method
                            Case "POST"
                                vlRequest.Method = HttpMethod.Post.Method
                            Case "PUT"
                                vlRequest.Method = HttpMethod.Put.Method
                            Case "DELETE"
                                vlRequest.Method = HttpMethod.Delete.Method

                        End Select

                        vlRequest.ContentType = prmCtipoContenido
                        vlRequest.ContentLength = data.Length

                        Dim vlCabeceros() As CabecerosRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(Of CabecerosRequest())(prmCCabeceros)

                        For Each cab As CabecerosRequest In vlCabeceros
                            vlRequest.Headers.Add(cab.cabecero, cab.valor)
                        Next

                        Dim stream As Stream = vlRequest.GetRequestStream()

                        stream.Write(data, 0, data.Length)
                        stream.Close()

                        Dim result As HttpWebResponse = CType(vlRequest.GetResponse(), HttpWebResponse)

                        Dim streamResponse = result.GetResponseStream()

                        Dim reader = New StreamReader(streamResponse)


                        Dim receiveContent = reader.ReadToEnd()

                        reader.Close()

                        If result.StatusCode >= HttpStatusCode.OK And result.StatusCode < HttpStatusCode.Ambiguous Then

                            vlFechaHoraFin = Now
                            'aqui guardamos en el historial
                            conexion.IniciaTransaccion()
                            conexion.flGuardaHistorialWS(prmNombreWS, vlFechaHoraInicio, vlFechaHoraFin, vlCuerpo)
                            conexion.TerminaTransaccion()

                            Loggers.d("WS Plantilla mailing ejecutado correctamente", MyClass.GetType.Name)

                        End If

                        conexion.IniciaTransaccion()
                        conexion.EjecutaTexto("UPDATE SES_Trabajos_WS SET bActivo=0 WHERE cNombreTrabajo='EjecutaPlantillaMailing'")
                        conexion.TerminaTransaccion()

                End Select

                vlCuerpoCorreo = CreaHTMLDesdeDataTable(dtResultadoWS, prmExcluirColumnas, "", "")

            End If

        Catch ex As Exception
            Loggers.e("Error al ejecutar los WS " + ex.Message, ex, MyClass.GetType.Name)
        End Try

    End Sub

    Function flObtenerTokenAzure() As String

        flObtenerTokenAzure = ""

        If conexion.flTraeWsTokenAzure() = True Then
            If conexion.Mensaje = "" Then

                Dim http As New Chilkat.Http

                Dim req As New Chilkat.HttpRequest

                Dim vlCabeceros() As CabecerosRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(Of CabecerosRequest())(conexion.dtaux2.Rows(0)("cCabeceros").ToString.Trim)

                For Each cab As CabecerosRequest In vlCabeceros
                    req.AddParam(cab.cabecero, cab.valor)
                Next

                Dim resp As Chilkat.HttpResponse = http.PostUrlEncoded(conexion.dtaux2.Rows(0)("cUrl").ToString.Trim, req)
                If (http.LastMethodSuccess = False) Then
                    Loggers.e("Error al ejecutar el WS para obtener toke", New Exception("Error"), MyClass.GetType.Name)
                End If

                Dim respStatusCode As Integer = resp.StatusCode

                Dim json As New Chilkat.JsonObject
                json.EmitCompact = False
                json.Load(resp.BodyStr)

                Dim access_token As String = json.StringOf("access_token")

                flObtenerTokenAzure = "Bearer " + access_token

            End If
        End If

    End Function
    Class ProductoWSInventario

        Public CustomerNumberSAP As String = ""
        Public ProductoId As String = ""
        Public PartNumber As String = ""
        Public NetExistence As Int32 = 0
        Public StoreLocation As String = ""
        Public StoreName As String = ""
        Public DateExtraction As String = ""

    End Class

    Class ProductoWSVentas

        Public CustomerNumberSAP As String = ""
        Public ProductoId As String = ""
        Public PartNumber As String = ""
        'Public SerialNumber As String = ""
        Public SaleDate As String = ""
        'Public CustomerIndustrySector As String = ""
        'Public ProcessApply As String = ""
        Public Quantity As Int32 = 0
        'Public EndCustomerName As String = ""
        'Public Type As String = ""

    End Class

    Sub EjecutaTrabajosSQL()

        conexion.TraeTrabajosSQL(Configuracion.noSucursal.ToString)

        Dim fechainicio As Date
        Dim fechaejecucion As Date
        Dim fechapivote As Date
        Dim horaEjecucion As String = ""
        Dim cCuerpoCorreo As String = ""
        Dim CorreosTimbre
        Dim bMandaCorreo As Boolean
        Dim vlcMensajeError As String

        If conexion.Mensaje = "" Then
            For i As Integer = 0 To conexion.dtTrabajos.Rows.Count - 1
                Try

                    vlcMensajeError = ""

                    fechainicio = Date.Parse(conexion.dtTrabajos.Rows(i)("FechaInicio").ToString)
                    If IsDBNull(conexion.dtTrabajos.Rows(i)("UltimaEjecucion")) = True Then
                        fechaejecucion = Nothing
                    Else
                        fechaejecucion = Date.Parse(conexion.dtTrabajos.Rows(i)("UltimaEjecucion").ToString)
                    End If
                    If fechaejecucion = Nothing Then
                        fechapivote = fechainicio
                    Else
                        fechapivote = fechaejecucion
                    End If
                    If conexion.dtTrabajos.Rows(i)("MandaCorreo") = True Then
                        bMandaCorreo = True
                    Else
                        bMandaCorreo = False
                    End If

                    horaEjecucion = conexion.dtTrabajos.Rows(i)("HoraEjecucion").ToString.Trim
                    Select Case (conexion.dtTrabajos.Rows(i)("FrecuenciaEjecucion").ToString.Trim)
                        Case "Diario"
                            If fechapivote.AddDays(1).ToString("yyyy-MM-dd") <= Date.Now.ToString("yyyy-MM-dd") Then
                                If Date.Now.ToString("HH:mm") >= horaEjecucion Then
                                    conexion.IniciaTransaccion()
                                    conexion.EjecutaTrabajo(conexion.dtTrabajos.Rows(i)("Codigo").ToString.Trim, conexion.dtTrabajos.Rows(i)("Parametros").ToString.Trim)
                                    If conexion.Mensaje = "" Then
                                        conexion.TerminaTransaccion()
                                    Else
                                        vlcMensajeError = conexion.Mensaje
                                        conexion.HacerRollBack()
                                    End If

                                    conexion.IniciaTransaccion()
                                    conexion.ActualizaFechaEjecucionTrabajo(conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim)
                                    conexion.TerminaTransaccion()

                                    If bMandaCorreo = True Then
                                        cCuerpoCorreo = cCuerpoCorreo + "Nombre del Trabajo: " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " | Fecha de ejecucion: " + Date.Now.ToString("yyyy-MM-dd HH:mm:ss")

                                        If vlcMensajeError.Trim <> "" Then
                                            cCuerpoCorreo = cCuerpoCorreo + "Mensaje de error: " + vlcMensajeError.Trim
                                        End If

                                        cCuerpoCorreo = cCuerpoCorreo + vbCrLf
                                    End If

                                    Loggers.d("Trabajo programado " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " ejecutado correctamente", MyClass.GetType.Name)

                                End If
                            End If
                        Case "Mensual"
                            If fechapivote.AddMonths(1).ToString("yyyy-MM-dd") = Date.Now.ToString("yyyy-MM-dd") Then
                                If Date.Now.ToString("HH:mm") >= horaEjecucion Then
                                    conexion.IniciaTransaccion()
                                    conexion.EjecutaTrabajo(conexion.dtTrabajos.Rows(i)("Codigo").ToString.Trim, conexion.dtTrabajos.Rows(i)("Parametros").ToString.Trim)
                                    If conexion.Mensaje = "" Then
                                        conexion.TerminaTransaccion()
                                    Else
                                        vlcMensajeError = conexion.Mensaje
                                        conexion.HacerRollBack()
                                    End If
                                    conexion.TerminaTransaccion()
                                    conexion.IniciaTransaccion()
                                    conexion.ActualizaFechaEjecucionTrabajo(conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim)
                                    conexion.TerminaTransaccion()
                                    If bMandaCorreo = True Then
                                        cCuerpoCorreo = cCuerpoCorreo + "Nombre del Trabajo: " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " | Fecha de ejecucion: " + Date.Now.ToString("yyyy-MM-dd HH:mm:ss")

                                        If vlcMensajeError.Trim <> "" Then
                                            cCuerpoCorreo = cCuerpoCorreo + "Mensaje de error: " + vlcMensajeError.Trim
                                        End If

                                        cCuerpoCorreo = cCuerpoCorreo + vbCrLf
                                    End If

                                    Loggers.d("Trabajo programado " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " ejecutado correctamente", MyClass.GetType.Name)

                                End If
                            End If
                        Case "Anual"
                            If fechapivote.AddYears(1).ToString("yyyy-MM-dd") = Date.Now.ToString("yyyy-MM-dd") Then
                                If Date.Now.ToString("HH:mm") >= horaEjecucion Then
                                    conexion.IniciaTransaccion()
                                    conexion.EjecutaTrabajo(conexion.dtTrabajos.Rows(i)("Codigo").ToString.Trim, conexion.dtTrabajos.Rows(i)("Parametros").ToString.Trim)
                                    If conexion.Mensaje = "" Then
                                        conexion.TerminaTransaccion()
                                    Else
                                        vlcMensajeError = conexion.Mensaje
                                        conexion.HacerRollBack()
                                    End If
                                    conexion.TerminaTransaccion()
                                    conexion.IniciaTransaccion()
                                    conexion.ActualizaFechaEjecucionTrabajo(conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim)
                                    conexion.TerminaTransaccion()
                                    If bMandaCorreo = True Then

                                        cCuerpoCorreo = cCuerpoCorreo + "Nombre del Trabajo: " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " | Fecha de ejecucion: " + Date.Now.ToString("yyyy-MM-dd HH:mm:ss")

                                        If vlcMensajeError.Trim <> "" Then
                                            cCuerpoCorreo = cCuerpoCorreo + "Mensaje de error: " + vlcMensajeError.Trim
                                        End If

                                        cCuerpoCorreo = cCuerpoCorreo + vbCrLf
                                    End If

                                    Loggers.d("Trabajo programado " + conexion.dtTrabajos.Rows(i)("Nombre").ToString.Trim + " ejecutado correctamente", MyClass.GetType.Name)

                                End If
                            End If
                    End Select
                Catch ex As Exception
                    Loggers.e("Error al ejecutar los trabajos programados " + ex.Message, ex, MyClass.GetType.Name)
                    conexion.Mensaje = ex.Message
                End Try
            Next
            If cCuerpoCorreo <> "" Then
                Try
                    Dim cCorreosSinSeparar As String
                    conexion.TraeCorreosParaEnvioInformacionTimbres()
                    If conexion.Mensaje = "" Then
                        If conexion.dtTimbresLibres.Rows.Count > 0 Then
                            cCorreosSinSeparar = conexion.dtTimbresLibres.Rows(0)("cValor").ToString.Trim
                            CorreosTimbre = Split(conexion.dtTimbresLibres.Rows(0)("cValor"), ",")
                        Else
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                    'Gilberto Madrid 12/11/2025 si esta encendido este parametro se manda por sendmailapi
                    If Configuracion.FuncionPortal = False Then
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                        correo = New MailMessage
                        servidor = New SmtpClient
                        correo.From = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                        For i As Integer = 0 To DirectCast(CorreosTimbre, String()).Length - 1
                            If CorreosTimbre(i).Trim() <> "" Then
                                correo.CC.Add(CorreosTimbre(i).Trim())
                            End If
                        Next
                        correo.Subject = "Correo informativo de trabajos programados " + " Suc : " + Configuracion.noSucursal.ToString.Trim + " | " + " | BD | " + Configuracion.BaseDeDatos
                        correo.Body = cCuerpoCorreo

                        correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                        servidor.Host = Configuracion.ServidorSalidaCorreo
                        servidor.Port = CInt(Configuracion.PuertoCorreo)
                        servidor.EnableSsl = Configuracion.UsaSSL
                        servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                        servidor.Send(correo)
                    Else
                        plSendMailApi("Notificaciones", "Correo informativo de trabajos programados " + " Suc : " + Configuracion.noSucursal.ToString.Trim + " | " + " | BD | " + Configuracion.BaseDeDatos, cCuerpoCorreo, cCorreosSinSeparar, "")
                    End If


                Catch ex As Exception
                    Loggers.e("Error al enviar correo de los trabajos programados " + ex.Message, ex, MyClass.GetType.Name)
                End Try

            End If
        End If


    End Sub

    Private Sub CerrarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CerrarToolStripMenuItem.Click
        If MessageBox.Show("¿Desea cerrar la aplicacion?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            Application.ExitThread()
        End If
    End Sub

    Private Sub EstatusTimerTrabajosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EstatusTimerTrabajosToolStripMenuItem.Click
        If tmrTrabajos.Enabled = True Then

            MessageBox.Show("El timer de trabajos está habilitado para ejecutarse cada " + tmrTrabajos.Interval.ToString.Trim, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else

            If MessageBox.Show("El timer de trabajos esta deshabilitado, ¿desea habilitarlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tmrTrabajos.Enabled = True

            End If

        End If
    End Sub

    Private Sub EstatusTimerNotificacionesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EstatusTimerNotificacionesToolStripMenuItem.Click
        If tmrNotificaciones.Enabled = True Then

            MessageBox.Show("El timer de notificaciones está habilitado para ejecutarse cada " + tmrNotificaciones.Interval.ToString.Trim, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else

            If MessageBox.Show("El timer de notificaciones esta deshabilitado, ¿desea habilitarlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tmrNotificaciones.Enabled = True

            End If

        End If
    End Sub

    Sub plCopiaArchivosRutaPortal(Optional prmOmitePregunta As Boolean = False)

        Try

            Dim vlsql As String = ""
            Dim SeparaRuta
            Dim RutaLocalPDF As String
            Dim RutaPortalPDF As String
            Dim RutaLocalXML As String
            Dim RutaPortalXML As String
            Dim vlMensaje As String = ""

            vlsql = "SELECT cli.CCLIENTE + ' - ' + cli.CNOMBRE as Cliente,COUNT(*) as Numero,'Facturas activas' as Facturas"
            vlsql = vlsql + " FROM AdsumFE_facturasEncabezado ele(NOLOCK) " + vbCr
            vlsql = vlsql + " INNER JOIN CLIENTES cli (NOLOCK) ON CONVERT(int,cli.CCLIENTE)=ele.nCliente " + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_Configuracion con (NOLOCK) ON con.nRFCEmisor=ele.nRFCEmisor AND con.nSucursal=" + Convert.ToInt32(Configuracion.noSucursal).ToString.Trim + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_RFCEmisores emi (NOLOCK) ON emi.nRFCEmisor=con.nRFCEmisor " + vbCr
            vlsql = vlsql + " WHERE ISNULL(ele.bArchivoCopiado,0)=0 AND ISNULL(cli.bAccesaPortal,0)=1 " + vbCr
            vlsql = vlsql + " AND ISNULL(con.cRutaFacturasPortal,'')<>'' " + vbCr
            vlsql = vlsql + " GROUP BY cli.CCLIENTE + ' - ' + cli.CNOMBRE " + vbCr
            vlsql = vlsql + " UNION " + vbCr
            vlsql = vlsql + " SELECT cli.CCLIENTE + ' - ' + cli.CNOMBRE as Cliente,COUNT(*) as Numero,'Facturas canceladas' as Facturas" + vbCr
            vlsql = vlsql + " FROM AdsumFE_facturasEncabezado ele(NOLOCK) " + vbCr
            vlsql = vlsql + " INNER JOIN CLIENTES cli (NOLOCK) ON CONVERT(int,cli.CCLIENTE)=ele.nCliente " + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_Configuracion con (NOLOCK) ON con.nRFCEmisor=ele.nRFCEmisor  AND con.nSucursal=" + Convert.ToInt32(Configuracion.noSucursal).ToString.Trim + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_RFCEmisores emi (NOLOCK) ON emi.nRFCEmisor=con.nRFCEmisor " + vbCr
            vlsql = vlsql + " WHERE ISNULL(ele.bArchivoCopiado,0)=0 AND ISNULL(cli.bAccesaPortal,0)=1 " + vbCr
            vlsql = vlsql + " AND ISNULL(con.cRutaFacturasPortal,'')<>'' AND ele.nStatus=0 " + vbCr
            vlsql = vlsql + " GROUP BY cli.CCLIENTE + ' - ' + cli.CNOMBRE " + vbCr

            conexion.fgRegresaConsultaDTArchivosCopiar(vlsql)

            If conexion.dtauxCopiaArchivos.Rows.Count > 0 Then

                'Gilberto Madrid 10/08/2019 se agrego un parametro para omitir la pregunta
                If prmOmitePregunta = False Then

                    vlMensaje = "Se copiaran los siguientes archivos, para continuar requerirá confirmación: " + vbCrLf + vbCrLf

                    For i As Integer = 0 To conexion.dtauxCopiaArchivos.Rows.Count - 1

                        'Gilberto Madrid 26/04/2019 si la cadena rebasa los 1000 caracteres, ya no los anexara
                        If vlMensaje.Trim.Length < 1000 Then
                            vlMensaje = vlMensaje + "Cliente: " + conexion.dtauxCopiaArchivos.Rows(i)("Cliente").ToString.Trim + " - Archivos: " + conexion.dtauxCopiaArchivos.Rows(i)("Numero").ToString.Trim + " (" + conexion.dtauxCopiaArchivos.Rows(i)("Facturas").ToString.Trim + ")" + vbCrLf
                        End If

                    Next

                    vlMensaje = vlMensaje + vbCrLf + vbCrLf + "¿Desea continuar?"

                    If MessageBox.Show(vlMensaje, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                End If

            Else
                Exit Sub
            End If

            vlsql = "SELECT "
            vlsql = vlsql + " case LTRIM(RTRIM(ele.cTipoComprobante)) when 'INGRESO' then LTRIM(RTRIM(cRutaFacturasPortal)) when 'EGRESO' then REPLACE(LTRIM(RTRIM(cRutaFacturasPortal)),'FACTURAS','NOTAS') else REPLACE(LTRIM(RTRIM(cRutaFacturasPortal)),'FACTURAS','PAGOS') end" + vbCr
            vlsql = vlsql + " +CONVERT(varchar(4),YEAR(ele.dFecha)) " + vbCr
            vlsql = vlsql + " +RIGHT('00'+CONVERT(varchar(2),MONTH(ele.dFecha)),2) " + vbCr
            vlsql = vlsql + " + case ele.nStatus when 1 then '\PDFS_TIMBRADOS\' else '\PDFCANCELADO\' end " + vbCr
            vlsql = vlsql + " +LTRIM(RTRIM(emi.CRFC)) " + vbCr
            vlsql = vlsql + " + case when LTRIM(RTRIM(ele.cSerie))='' then '__' else '__'+LTRIM(RTRIM(ele.cSerie))+'__' end " + vbCr
            vlsql = vlsql + " + CONVERT(varchar(10),ele.nFolio)+'.pdf' as RutaPortal " + vbCr
            vlsql = vlsql + " ,case LTRIM(RTRIM(ele.cTipoComprobante)) when 'INGRESO' then LTRIM(RTRIM(cRutaFacturas)) when 'EGRESO' then REPLACE(LTRIM(RTRIM(cRutaFacturas)),'FACTURAS','NOTAS') else REPLACE(LTRIM(RTRIM(cRutaFacturas)),'FACTURAS','PAGOS') end" + vbCr
            vlsql = vlsql + " +CONVERT(varchar(4),YEAR(ele.dFecha)) " + vbCr
            vlsql = vlsql + " +RIGHT('00'+CONVERT(varchar(2),MONTH(ele.dFecha)),2) " + vbCr
            vlsql = vlsql + " + case ele.nStatus when 1 then '\PDFS_TIMBRADOS\' else '\PDFCANCELADO\' end " + vbCr
            vlsql = vlsql + " +LTRIM(RTRIM(emi.CRFC)) " + vbCr
            vlsql = vlsql + " + case when LTRIM(RTRIM(ele.cSerie))='' then '__' else '__'+LTRIM(RTRIM(ele.cSerie))+'__' end " + vbCr
            vlsql = vlsql + " + CONVERT(varchar(10),ele.nFolio)+'.pdf' as Ruta " + vbCr
            vlsql = vlsql + " ,ele.nFactura" + vbCr
            vlsql = vlsql + " FROM AdsumFE_facturasEncabezado ele(NOLOCK) " + vbCr
            vlsql = vlsql + " INNER JOIN CLIENTES cli (NOLOCK) ON CONVERT(int,cli.CCLIENTE)=ele.nCliente " + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_Configuracion con (NOLOCK) ON con.nRFCEmisor=ele.nRFCEmisor AND con.nSucursal=" + Convert.ToInt32(Configuracion.noSucursal).ToString.Trim + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_RFCEmisores emi (NOLOCK) ON emi.nRFCEmisor=con.nRFCEmisor " + vbCr
            vlsql = vlsql + " WHERE ISNULL(ele.bArchivoCopiado,0)=0 AND ISNULL(cli.bAccesaPortal,0)=1 " + vbCr
            vlsql = vlsql + " AND ISNULL(con.cRutaFacturasPortal,'')<>'' " + vbCr
            vlsql = vlsql + " UNION " + vbCr
            vlsql = vlsql + " SELECT " + vbCr
            vlsql = vlsql + " case LTRIM(RTRIM(ele.cTipoComprobante)) when 'INGRESO' then LTRIM(RTRIM(cRutaFacturasPortal)) when 'EGRESO' then REPLACE(LTRIM(RTRIM(cRutaFacturasPortal)),'FACTURAS','NOTAS') else REPLACE(LTRIM(RTRIM(cRutaFacturasPortal)),'FACTURAS','PAGOS') end" + vbCr
            vlsql = vlsql + " +CONVERT(varchar(4),YEAR(ele.dFecha)) " + vbCr
            vlsql = vlsql + " +RIGHT('00'+CONVERT(varchar(2),MONTH(ele.dFecha)),2) " + vbCr
            vlsql = vlsql + " + case ele.nStatus when 1 then '\PDFS_TIMBRADOS\' else '\PDFCANCELADO\' end " + vbCr
            vlsql = vlsql + " +LTRIM(RTRIM(emi.CRFC)) " + vbCr
            vlsql = vlsql + " + case when LTRIM(RTRIM(ele.cSerie))='' then '__' else '__'+LTRIM(RTRIM(ele.cSerie))+'__' end " + vbCr
            vlsql = vlsql + " + CONVERT(varchar(10),ele.nFolio)+'.pdf' as RutaPortal " + vbCr
            vlsql = vlsql + " ,case LTRIM(RTRIM(ele.cTipoComprobante)) when 'INGRESO' then LTRIM(RTRIM(cRutaFacturas)) when 'EGRESO' then REPLACE(LTRIM(RTRIM(cRutaFacturas)),'FACTURAS','NOTAS') else REPLACE(LTRIM(RTRIM(cRutaFacturas)),'FACTURAS','PAGOS') end" + vbCr
            vlsql = vlsql + " +CONVERT(varchar(4),YEAR(ele.dFecha)) " + vbCr
            vlsql = vlsql + " +RIGHT('00'+CONVERT(varchar(2),MONTH(ele.dFecha)),2) " + vbCr
            vlsql = vlsql + " + case ele.nStatus when 1 then '\PDFS_TIMBRADOS\' else '\PDFCANCELADO\' end " + vbCr
            vlsql = vlsql + " +LTRIM(RTRIM(emi.CRFC)) " + vbCr
            vlsql = vlsql + " + case when LTRIM(RTRIM(ele.cSerie))='' then '__' else '__'+LTRIM(RTRIM(ele.cSerie))+'__' end " + vbCr
            vlsql = vlsql + " + CONVERT(varchar(10),ele.nFolio)+'.pdf' as Ruta " + vbCr
            vlsql = vlsql + " ,ele.nFactura" + vbCr
            vlsql = vlsql + " FROM AdsumFE_facturasEncabezado ele(NOLOCK) " + vbCr
            vlsql = vlsql + " INNER JOIN CLIENTES cli (NOLOCK) ON CONVERT(int,cli.CCLIENTE)=ele.nCliente " + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_Configuracion con (NOLOCK) ON con.nRFCEmisor=ele.nRFCEmisor  AND con.nSucursal=" + Convert.ToInt32(Configuracion.noSucursal).ToString.Trim + vbCr
            vlsql = vlsql + " INNER JOIN AdsumFE_RFCEmisores emi (NOLOCK) ON emi.nRFCEmisor=con.nRFCEmisor " + vbCr
            vlsql = vlsql + " WHERE ISNULL(ele.bArchivoCopiado,0)=0 AND ISNULL(cli.bAccesaPortal,0)=1 " + vbCr
            vlsql = vlsql + " AND ISNULL(con.cRutaFacturasPortal,'')<>'' AND ele.nStatus=0 " + vbCr


            conexion.fgRegresaConsultaDTArchivosCopiar(vlsql)

            If conexion.dtauxCopiaArchivos.Rows.Count > 0 Then


                For i As Integer = 0 To conexion.dtauxCopiaArchivos.Rows.Count - 1

                    RutaLocalPDF = conexion.dtauxCopiaArchivos.Rows(i)("Ruta").ToString.Trim
                    RutaPortalPDF = conexion.dtauxCopiaArchivos.Rows(i)("RutaPortal").ToString.Trim

                    SeparaRuta = RutaPortalPDF.Split("\")

                    If Directory.Exists(RutaPortalPDF.Replace(SeparaRuta(DirectCast(SeparaRuta, String()).Length - 1), "")) = False Then
                        Directory.CreateDirectory(RutaPortalPDF.Replace(SeparaRuta(DirectCast(SeparaRuta, String()).Length - 1), ""))
                    End If

                    If File.Exists(RutaLocalPDF) = True Then
                        File.Copy(RutaLocalPDF, RutaPortalPDF, True)
                    End If

                    RutaLocalXML = RutaLocalPDF.Replace(".pdf", ".xml")
                    RutaPortalXML = RutaPortalPDF.Replace(".pdf", ".xml")

                    If RutaLocalPDF.Trim.ToUpper.Contains("PDFS_TIMBRADOS") = True Then
                        RutaLocalXML = RutaLocalXML.Replace("PDFS_TIMBRADOS", "XMLTIMBRADO")
                        RutaPortalXML = RutaPortalXML.Replace("PDFS_TIMBRADOS", "XMLTIMBRADO")
                    Else
                        RutaLocalXML = RutaLocalXML.Replace("PDFCANCELADO", "ACUSECANCELACION")
                        RutaPortalXML = RutaPortalXML.Replace("PDFCANCELADO", "ACUSECANCELACION")
                    End If

                    SeparaRuta = RutaPortalXML.Split("\")

                    If Directory.Exists(RutaPortalXML.Replace(SeparaRuta(DirectCast(SeparaRuta, String()).Length - 1), "")) = False Then
                        Directory.CreateDirectory(RutaPortalXML.Replace(SeparaRuta(DirectCast(SeparaRuta, String()).Length - 1), ""))
                    End If

                    If File.Exists(RutaLocalXML) = True Then
                        File.Copy(RutaLocalXML, RutaPortalXML, True)
                    End If
                    conexion.IniciaTransaccion()
                    conexion.EjecutaTexto("UPDATE AdsumFE_facturasEncabezado SET bArchivoCopiado=1 WHERE nFactura=" + conexion.dtauxCopiaArchivos.Rows(i)("nFactura").ToString.Trim, True)
                    conexion.TerminaTransaccion()

                Next

                Loggers.d("Archivos copiados correctamente", MyClass.GetType().Name)

            End If

            Exit Sub

        Catch ex As Exception

            Loggers.e("Error copiado de archivos " + ex.Message, ex, MyClass.GetType.Name)

        End Try



    End Sub

    Private Sub CopiarArchivosParaPortalToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopiarArchivosParaPortalToolStripMenuItem.Click
        plCopiaArchivosRutaPortal()
    End Sub

    Private Sub tmrSolicitudAutorizacionCorreo_Tick(sender As Object, e As EventArgs) Handles tmrSolicitudAutorizacionCorreo.Tick

        tmrSolicitudAutorizacionCorreo.Enabled = False

        Try

            EjecutaSolicitudAutorizacionCorreo()

        Catch ex As Exception
            Loggers.e("Error ejecución autorización por correo " + ex.Message, ex, MyClass.GetType.Name)
        End Try

        tmrSolicitudAutorizacionCorreo.Enabled = True

    End Sub

    Sub EjecutaSolicitudAutorizacionCorreo()

        conexion.plTraeSolicitudPendientesEnvio()

        If conexion.Mensaje = "" Then

            If conexion.dtSolicitudAutorizacionCorreo.Rows.Count > 0 Then

                For i As Integer = 0 To conexion.dtSolicitudAutorizacionCorreo.Rows.Count - 1

                    plEnviaCorreoSolicitud(conexion.dtSolicitudAutorizacionCorreo.Rows(i)("nID").ToString.Trim)

                Next

            End If

        End If

        conexion.plTraeSolicitudAtendidaPendienteEnvio()

        If conexion.Mensaje = "" Then

            If conexion.dtSolicitudAutorizacionCorreo.Rows.Count > 0 Then

                For i As Integer = 0 To conexion.dtSolicitudAutorizacionCorreo.Rows.Count - 1

                    plEnviaCorreoSolicitudAtendida(conexion.dtSolicitudAutorizacionCorreo.Rows(i)("nID").ToString.Trim)

                Next

            End If

        End If

    End Sub

    Sub plEnviaCorreoSolicitud(prmID As String)

        conexion.plTraeSolicitudAutorizacionID(prmID)
        If conexion.Mensaje = "" Then

            If conexion.dtSolicitudAutorizacionCorreoaux.Rows.Count > 0 Then
                Dim vlPermisos As String
                Dim vlPermisosSinHtml As String
                Dim vlAsunto As String
                Dim vlCuerpoCorreo As String

                vlPermisos = ""
                vlPermisosSinHtml = ""

                If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudModificacionPrecio").ToString.Trim <> "NO" Then
                    vlPermisos = "PRECIO FINAL"
                    vlPermisosSinHtml = "PRECIO FINAL"

                    'Gilberto Madrid 08/12/2020 anexamos si quedo debajo del costo o del margen
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoMargen").ToString.Trim <> "NO" And conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoCosto").ToString.Trim <> "NO" Then

                        vlPermisos = vlPermisos + " (debajo del margen - <span style=""color:red"">debajo del costo</span>)"
                        vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del margen - debajo del costo)"
                    Else

                        If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoMargen").ToString.Trim <> "NO" Then

                            vlPermisos = vlPermisos + " (debajo del margen)"
                            vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del margen)"
                        Else

                            vlPermisos = vlPermisos + " (<span style=""color:red"">debajo del costo</span>)"
                            vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del costo)"

                        End If

                    End If


                End If

                If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim <> "NO" Then

                    If vlPermisos = "" Then
                        vlPermisos = "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                        vlPermisosSinHtml = "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                    Else
                        vlPermisos = vlPermisos + " - " + "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                        vlPermisosSinHtml = vlPermisosSinHtml + " - " + "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                    End If

                End If

                vlAsunto = "Solicitud para venta a cliente " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("RFCSuc").ToString.Trim + " (" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Sucursal").ToString.Trim + ") (" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + " - " + vlPermisosSinHtml + ")"

                vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Ha recibido una solicitud de autorización para facturar a este cliente fuera de su nivel de precios establecido en la sucursal de <b>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Sucursal").ToString.Trim + "</b>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br>Una vez recibida su respuesta se le notificará por email al solicitante </p>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + " <p><b>¿Autoriza la solicitud?</b>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><br><a style=""color:green"" href=" + "http://sensa.com.mx/sitioWSSensa.php?id_peticion=" + prmID + "&accion=y&suc=" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("CSUCURSAL").ToString.Trim + "" + "><b>SI</b></a>"

                '19/08/2021 César Niebla Agregamos links de rechazo 
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><br><a style=""color:red"" href=" + "http://sensa.com.mx/sitioWSSensa.php?id_peticion=" + prmID + "&accion=n1&suc=" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("CSUCURSAL").ToString.Trim + "" + "><b>NO (" + conexion.FlTraeDescripcionMotivoCancelacion("1") + ")</b></a>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><br><a style=""color:red"" href=" + "http://sensa.com.mx/sitioWSSensa.php?id_peticion=" + prmID + "&accion=n2&suc=" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("CSUCURSAL").ToString.Trim + "" + "><b>NO (" + conexion.FlTraeDescripcionMotivoCancelacion("2") + ")</b></a>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><br><a style=""color:red"" href=" + "http://sensa.com.mx/sitioWSSensa.php?id_peticion=" + prmID + "&accion=n3&suc=" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("CSUCURSAL").ToString.Trim + "" + "><b>NO (" + conexion.FlTraeDescripcionMotivoCancelacion("3") + ")</b></a>"

                vlCuerpoCorreo = vlCuerpoCorreo + " </p>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr

                'Gilberto Madrid 12/11/2025 si esta encendido este parametro se manda por sendmailapi
                If Configuracion.FuncionPortal = False Then
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    correo = New MailMessage
                    servidor = New SmtpClient
                    correo.From = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)
                    correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim)

                    'César Niebla 28/09/2022 En caso de que el mail de supervisor configurado para copia no venga dentro de los correos lo anexamos
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim <> conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim Then
                        correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim)
                    End If

                    correo.CC.Add("notificaciones@sensa.com.mx")


                    Dim vlArchivosAdjuntos
                    Dim vlNombreArchivo As String
                    Dim ArchivoSolicitudAutorizacion As Attachment
                    Dim vlparametros

                    'Gilberto Madrid 28/11/2020 faltaba validar que no viniera en blanco el dato de cotizaciones 
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim <> "" Then
                        vlArchivosAdjuntos = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim.Split(",")

                        For j As Integer = 0 To DirectCast(vlArchivosAdjuntos, String()).Length - 1

                            If vlArchivosAdjuntos(j).ToString.Trim.Contains("_Libre") Then
                                vlparametros = vlArchivosAdjuntos(j).ToString.Trim.Replace("_Libre", "").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("CotizacionLibre.rpt", vlparametros)

                                ArchivoSolicitudAutorizacion = New Attachment(vlNombreArchivo)
                                correo.Attachments.Add(ArchivoSolicitudAutorizacion)

                            Else

                                vlparametros = (vlArchivosAdjuntos(j).ToString.Trim + ",").Split(",")

                                vlNombreArchivo = CreaArchivoNotificacion("Cotizacion.rpt", vlparametros)

                                ArchivoSolicitudAutorizacion = New Attachment(vlNombreArchivo)
                                correo.Attachments.Add(ArchivoSolicitudAutorizacion)

                            End If



                        Next
                    End If


                    correo.Subject = vlAsunto
                    correo.Body = vlCuerpoCorreo
                    correo.IsBodyHtml = True
                    correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                    servidor.Host = Configuracion.ServidorSalidaCorreo
                    servidor.Port = CInt(Configuracion.PuertoCorreo)
                    servidor.EnableSsl = Configuracion.UsaSSL
                    servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                    servidor.Send(correo)

                    vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Se ha enviado su solicitud de autorización para facturar a este cliente fuera del nivel de precios establecido para la sucursal de <b>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Sucursal").ToString.Trim + "</b>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br>Una vez se tenga la respuesta del supervisor se le notificará por email.</p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr

                    correo.CC.Clear()
                    correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSolicito").ToString.Trim)
                    correo.CC.Add("notificaciones@sensa.com.mx")

                    correo.Body = vlCuerpoCorreo
                    correo.IsBodyHtml = True

                    correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                    servidor.Host = Configuracion.ServidorSalidaCorreo
                    servidor.Port = Configuracion.PuertoCorreo
                    servidor.EnableSsl = Configuracion.UsaSSL
                    servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                    servidor.Send(correo)

                Else
                    Dim cCorreosSinSeparar As String
                    Dim vlArchivosSinSeparar As String
                    cCorreosSinSeparar = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim <> conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim Then
                        cCorreosSinSeparar = cCorreosSinSeparar + "," + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim
                    End If
                    cCorreosSinSeparar = cCorreosSinSeparar + ",notificaciones@sensa.com.mx"

                    Dim vlArchivosAdjuntos
                    Dim vlparametros
                    Dim vlNombreArchivo As String
                    Dim ArchivoSolicitudAutorizacion As String
                    vlArchivosSinSeparar = ""

                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim <> "" Then
                        vlArchivosAdjuntos = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim.Split(",")
                        For j As Integer = 0 To DirectCast(vlArchivosAdjuntos, String()).Length - 1
                            If vlArchivosAdjuntos(j).ToString.Trim.Contains("_Libre") Then
                                vlparametros = vlArchivosAdjuntos(j).ToString.Trim.Replace("_Libre", "").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("CotizacionLibre.rpt", vlparametros)
                                'vlArchivosSinSeparar = vlArchivosSinSeparar + flSubirArchivoFTP("Notificaciones", vlNombreArchivo) + ","
                                vlArchivosSinSeparar = vlNombreArchivo + ","
                            Else
                                vlparametros = (vlArchivosAdjuntos(j).ToString.Trim + ",").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("Cotizacion.rpt", vlparametros)
                                'vlArchivosSinSeparar = vlArchivosSinSeparar + flSubirArchivoFTP("Notificaciones", vlNombreArchivo) + ","
                                vlArchivosSinSeparar = vlArchivosSinSeparar + vlNombreArchivo + ","
                            End If
                        Next
                    End If

                    plSendMailApi("Notificaciones", vlAsunto, vlCuerpoCorreo, cCorreosSinSeparar, vlArchivosSinSeparar)

                    vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Se ha enviado su solicitud de autorización para facturar a este cliente fuera del nivel de precios establecido para la sucursal de <b>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Sucursal").ToString.Trim + "</b>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br>Una vez se tenga la respuesta del supervisor se le notificará por email.</p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr
                    cCorreosSinSeparar = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSolicito").ToString.Trim + ",notificaciones@sensa.com.mx"

                    plSendMailApi("Notificaciones", vlAsunto, vlCuerpoCorreo, cCorreosSinSeparar, vlArchivosSinSeparar)

                End If




                conexion.IniciaTransaccion()
                conexion.plActualizaSolicitudAutorizacionEnviada(prmID)
                conexion.TerminaTransaccion()


            End If


        End If

    End Sub

    Sub plEnviaCorreoSolicitudAtendida(prmID As String)

        conexion.plTraeSolicitudAutorizacionAtendidaID(prmID)
        If conexion.Mensaje = "" Then

            If conexion.dtSolicitudAutorizacionCorreoaux.Rows.Count > 0 Then

                Dim vlPermisos As String
                Dim vlPermisosSinHtml As String
                Dim vlAsunto As String
                Dim vlMotivoRechazoSolicitud As String ' 18/08/2021 César Niebla Variable para manejar la causa de rechazo a una solicitud
                Dim vlCuerpoCorreo As String
                Dim vlbAprobado As Boolean

                vlbAprobado = False

                vlPermisos = ""
                vlPermisosSinHtml = ""

                If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("PrecioAutorizado") = True Or conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("ModificacionAutorizado") = True Then
                    vlbAprobado = True
                End If

                If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudModificacionPrecio").ToString.Trim <> "NO" Then
                    vlPermisos = "PRECIO FINAL"
                    vlPermisosSinHtml = "PRECIO FINAL"

                    'Gilberto Madrid 08/12/2020 anexamos si quedo debajo del costo o del margen
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoMargen").ToString.Trim <> "NO" And conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoCosto").ToString.Trim <> "NO" Then

                        vlPermisos = vlPermisos + " (debajo del margen - <span style=""color:red"">debajo del costo</span>)"
                        vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del margen - debajo del costo)"
                    Else

                        If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudDebajoMargen").ToString.Trim <> "NO" Then

                            vlPermisos = vlPermisos + " (debajo del margen)"
                            vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del margen)"
                        Else

                            vlPermisos = vlPermisos + " (<span style=""color:red"">debajo del costo</span>)"
                            vlPermisosSinHtml = vlPermisosSinHtml + " (debajo del costo)"

                        End If

                    End If

                End If


                If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim <> "NO" Then

                    If vlPermisos = "" Then
                        vlPermisos = "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                        vlPermisosSinHtml = "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                    Else
                        vlPermisos = vlPermisos + " - " + "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                        vlPermisosSinHtml = vlPermisosSinHtml + " - " + "PRECIO " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("SolicitudListaPrecio").ToString.Trim
                    End If

                End If

                vlAsunto = IIf(vlbAprobado = True, "APROBACION", "RECHAZO") + " a solicitud para venta a cliente " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("RFCSuc").ToString.Trim + " (" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Sucursal").ToString.Trim + ") (" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + " - " + vlPermisosSinHtml + ")"


                'César Niebla 18/08/2021 En caso de que se haya rechazado la solicitud procedemos a obtener el motivo de la cancelación
                vlMotivoRechazoSolicitud = ""

                If Not vlbAprobado Then
                    vlMotivoRechazoSolicitud = conexion.FlTraeDescripcionMotivoCancelacion(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMotivoRechazoSolicitud").ToString.Trim)
                End If


                vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                'Gilberto Madrid 01/12/2020 se cambia el mensaje para que quede mas claro que ya se aplico el cambio
                If vlbAprobado = True Then
                    vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su respuesta de <span style=""color:green""><b>APROBACION</b></span> para esta solicitud ha sido procesada y notificada al usuario solicitante</p>"
                Else
                    '19/08/2021 César Niebla | Concatenamos el motivo de rechazo al cuerpo de correo de notificación
                    vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su respuesta de <span style=""color:red""><b>RECHAZO (" + vlMotivoRechazoSolicitud + ") </b></span> para esta solicitud ha sido procesada y notificada al usuario solicitante</p>"
                End If
                vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr

                'Gilberto Madrid 12/11/2025 si esta encendido este parametro se manda por sendmailapi
                If Configuracion.FuncionPortal = False Then
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    correo = New MailMessage

                    servidor = New SmtpClient
                    correo.From = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                    correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim)

                    'César Niebla 28/09/2022 En caso de que el mail de supervisor configurado para copia no venga dentro de los correos lo anexamos
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim <> conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim Then
                        correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim)
                    End If

                    correo.CC.Add("notificaciones@sensa.com.mx")

                    Dim vlArchivosAdjuntos
                    Dim vlNombreArchivo As String
                    Dim ArchivoSolicitudAutorizacion As Attachment
                    Dim vlparametros

                    'Gilberto Madrid 28/11/2020 faltaba validar que no viniera en blanco el dato de cotizaciones 
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim <> "" Then

                        vlArchivosAdjuntos = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim.Split(",")

                        For j As Integer = 0 To DirectCast(vlArchivosAdjuntos, String()).Length - 1

                            If vlArchivosAdjuntos(j).ToString.Trim.Contains("_Libre") Then
                                vlparametros = vlArchivosAdjuntos(j).ToString.Trim.Replace("_Libre", "").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("CotizacionLibre.rpt", vlparametros)

                                ArchivoSolicitudAutorizacion = New Attachment(vlNombreArchivo)
                                correo.Attachments.Add(ArchivoSolicitudAutorizacion)

                            Else

                                vlparametros = (vlArchivosAdjuntos(j).ToString.Trim + ",").Split(",")

                                vlNombreArchivo = CreaArchivoNotificacion("Cotizacion.rpt", vlparametros)

                                ArchivoSolicitudAutorizacion = New Attachment(vlNombreArchivo)
                                correo.Attachments.Add(ArchivoSolicitudAutorizacion)

                            End If



                        Next

                    End If

                    correo.Subject = vlAsunto
                    correo.Body = vlCuerpoCorreo
                    correo.IsBodyHtml = True
                    correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)
                    servidor.Host = Configuracion.ServidorSalidaCorreo
                    servidor.Port = CInt(Configuracion.PuertoCorreo)
                    servidor.EnableSsl = Configuracion.UsaSSL
                    servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                    servidor.Send(correo)

                    vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                    If vlbAprobado = True Then
                        vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su solicitud ha sido <span style=""color:green""><b>APROBADA</b></span> por parte del supervisor.</p>"
                    Else
                        '19/08/2021 César Niebla | Concatenamos el motivo de rechazo al cuerpo de correo de notificación
                        vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su solicitud ha sido <span style=""color:red""><b>RECHAZADA (" + vlMotivoRechazoSolicitud + ") </b></span> por parte del supervisor.</p>"
                    End If
                    vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr

                    correo.CC.Clear()
                    correo.CC.Add(conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSolicito").ToString.Trim)
                    correo.CC.Add("notificaciones@sensa.com.mx")

                    correo.Body = vlCuerpoCorreo
                    correo.IsBodyHtml = True

                    correo.Sender = New MailAddress(Configuracion.Sender, Configuracion.Etiqueta)

                    servidor.Host = Configuracion.ServidorSalidaCorreo
                    servidor.Port = CInt(Configuracion.PuertoCorreo)
                    servidor.EnableSsl = Configuracion.UsaSSL
                    servidor.Credentials = New Net.NetworkCredential(Configuracion.Correo, Configuracion.ContrasenaCorreo)
                    servidor.Send(correo)
                Else
                    Dim cCorreosSinSeparar As String
                    Dim cArchivosSinSeparar As String
                    cCorreosSinSeparar = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim

                    'César Niebla 28/09/2022 En caso de que el mail de supervisor configurado para copia no venga dentro de los correos lo anexamos
                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailAutoriza").ToString.Trim <> conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim Then
                        cCorreosSinSeparar = cCorreosSinSeparar + "," + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSupervisorCopia").ToString.Trim
                    End If

                    cCorreosSinSeparar = cCorreosSinSeparar + "," + "notificaciones@sensa.com.mx"

                    Dim vlArchivosAdjuntos
                    Dim vlNombreArchivo As String
                    Dim vlparametros

                    cArchivosSinSeparar = ""

                    If conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim <> "" Then
                        vlArchivosAdjuntos = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cDatosAdjuntos").ToString.Trim.Split(",")
                        For j As Integer = 0 To DirectCast(vlArchivosAdjuntos, String()).Length - 1
                            If vlArchivosAdjuntos(j).ToString.Trim.Contains("_Libre") Then
                                vlparametros = vlArchivosAdjuntos(j).ToString.Trim.Replace("_Libre", "").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("CotizacionLibre.rpt", vlparametros)
                                'cArchivosSinSeparar = cArchivosSinSeparar + flSubirArchivoFTP("Notificaciones", vlNombreArchivo) + ","
                                cArchivosSinSeparar = cArchivosSinSeparar + vlNombreArchivo + ","
                            Else
                                vlparametros = (vlArchivosAdjuntos(j).ToString.Trim + ",").Split(",")
                                vlNombreArchivo = CreaArchivoNotificacion("Cotizacion.rpt", vlparametros)
                                'cArchivosSinSeparar = cArchivosSinSeparar + flSubirArchivoFTP("Notificaciones", vlNombreArchivo) + ","
                                cArchivosSinSeparar = cArchivosSinSeparar + vlNombreArchivo + ","
                            End If
                        Next
                    End If

                    plSendMailApi("Notificaciones", vlAsunto, vlCuerpoCorreo, cCorreosSinSeparar, cArchivosSinSeparar)

                    vlCuerpoCorreo = "<!DOCTYPE html>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<html lang=""en"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta charset=""UTF-8"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "</head>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "<font color=""#084B8A"" face=""Arial"">" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + "    <p><b>Buen día</b></p>" + vbCr
                    If vlbAprobado = True Then
                        vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su solicitud ha sido <span style=""color:green""><b>APROBADA</b></span> por parte del supervisor.</p>"
                    Else
                        '19/08/2021 César Niebla | Concatenamos el motivo de rechazo al cuerpo de correo de notificación
                        vlCuerpoCorreo = vlCuerpoCorreo + "        <p>Su solicitud ha sido <span style=""color:red""><b>RECHAZADA (" + vlMotivoRechazoSolicitud + ") </b></span> por parte del supervisor.</p>"
                    End If
                    vlCuerpoCorreo = vlCuerpoCorreo + "     <p> <b>Fecha Solicitud:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("FechaSolicitud").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Cliente:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("Cliente").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Solicita:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioSolicita").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Autoriza:</b> <u>" + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("UsuarioAutoriza").ToString.Trim + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Permisos solicitado:</b> <u>" + vlPermisos + "</u>"
                    vlCuerpoCorreo = vlCuerpoCorreo + " <br><b>Mensaje:</b> " + conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("cMensaje").ToString.Trim + " </p>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </body>" + vbCr
                    vlCuerpoCorreo = vlCuerpoCorreo + " </html>" + vbCr

                    cCorreosSinSeparar = conexion.dtSolicitudAutorizacionCorreoaux.Rows(0)("MailSolicito").ToString.Trim + ",notificaciones@sensa.com.mx"

                    plSendMailApi("Notificaciones", vlAsunto, vlCuerpoCorreo, cCorreosSinSeparar, cArchivosSinSeparar)


                End If

                conexion.IniciaTransaccion()
                conexion.plActualizaSolicitudAutorizacionAtendida(prmID)
                conexion.TerminaTransaccion()

            End If

        End If

    End Sub

    Private Sub tmrServicioMonedero_Tick(sender As Object, e As EventArgs) Handles tmrServicioMonedero.Tick
        'Dim myString As String = "https://api.clienttoolbox.com"
        'Dim data = Encoding.UTF8.GetBytes("")
        'Dim result_post = SendRequest(New Uri(myString), data, "application/json", "POST")

    End Sub

    Private Sub NtiMensaje_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NtiMensaje.MouseDoubleClick

    End Sub





    'Private Function SendRequest(uri As Uri, jsonDataBytes As Byte(), contentType As String, method As String) As String
    '    Dim response As String
    '    Dim request As WebRequest

    '    request = WebRequest.Create(uri)
    '    request.ContentLength = jsonDataBytes.Length
    '    request.ContentType = contentType
    '    request.Method = method

    '    Using requestStream = request.GetRequestStream
    '        requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
    '        requestStream.Close()

    '        Using responseStream = request.GetResponse.GetResponseStream
    '            Using reader As New StreamReader(responseStream)
    '                response = reader.ReadToEnd()
    '            End Using
    '        End Using
    '    End Using

    '    Return response
    'End Function



    'Private Function GetResponse()
    '    Dim request As HttpWebRequest
    '    Dim response As HttpWebResponse = Nothing
    '    Dim reader As StreamReader
    '    Dim address As Uri
    '    Dim dataSend As String

    '    Dim data As StringBuilder
    '    Dim byteData() As Byte
    '    Dim postStream As Stream = Nothing


    '    address = New Uri("https://api.clienttoolbox.com")

    '    ' CREA EL REQUEST WEB
    '    request = DirectCast(WebRequest.Create(address), HttpWebRequest)

    '    ' SETEA A POST  
    '    request.Method = "GET"
    '    'request.ContentType = "application/x-www-form-urlencoded"
    '    'request.ContentType = "text/xml"
    '    request.ContentType = "application/json"
    '    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE)"
    '    ' Create the data we want to send  

    '    data = New StringBuilder()
    '    data.Append("{""username"":""myuser"",""password"":""mypass""}")
    '    dataSend = "{""username"":""myuser"",""password"":""mypass""}"

    '    ' Create a byte array of the data we want to send  
    '    byteData = UTF8Encoding.UTF8.GetBytes(dataSend)

    '    ' Set the content length in the request headers  
    '    request.ContentLength = byteData.Length

    '    ' Write data  
    '    Try
    '        postStream = request.GetRequestStream()
    '        postStream.Write(byteData, 0, byteData.Length)
    '    Finally
    '        If Not postStream Is Nothing Then postStream.Close()
    '    End Try

    '    Try
    '        ' Get response  

    '        response = DirectCast(request.GetResponse(), HttpWebResponse)

    '        ' Get the response stream into a reader  
    '        reader = New StreamReader(response.GetResponseStream())

    '        ' Console application output  
    '        MsgBox(reader.ReadToEnd())
    '        Console.WriteLine(reader.ReadToEnd())
    '    Finally
    '        If Not response Is Nothing Then response.Close()
    '    End Try
    'End Function

    Sub plSendMailApi(prtNombrePlantilla As String, prtAsuntoCorreo As String, prtCuerpoCorreo As String, prtCorreos As String, prtArchivosAdjuntos As String)

        conexion.plObtenConfiguracionPlantillaCorreos(prtNombrePlantilla)
        Dim baseAddress As String
        Dim vlAcepta As String
        Dim vlContenido As String
        Dim vlMetodo As String
        Dim vlPreAutenticado As Boolean
        Dim vlCabeceros As String
        Dim vlKeyTemplate As String
        Dim vlIconoPDF As String
        Dim vlIconoXML As String

        If conexion.dtaux2.Rows.Count > 0 Then
            If conexion.Mensaje = "" Then
                baseAddress = conexion.dtaux2.Rows(0)("cUrl").ToString.Trim
                vlAcepta = conexion.dtaux2.Rows(0)("cTipoContenidoAcepta").ToString.Trim
                vlContenido = conexion.dtaux2.Rows(0)("cTipoContenido").ToString.Trim
                vlMetodo = conexion.dtaux2.Rows(0)("cMetodoConsumo").ToString.Trim
                vlKeyTemplate = conexion.dtaux2.Rows(0)("cKeyTemplate").ToString.Trim
                vlPreAutenticado = CBool(conexion.dtaux2.Rows(0)("bPreAutenticacion"))
                vlCabeceros = conexion.dtaux2.Rows(0)("cCabeceros").ToString.Trim
                vlIconoPDF = conexion.dtaux2.Rows(0)("cIconoPDF").ToString.Trim
                vlIconoXML = conexion.dtaux2.Rows(0)("cIconoXML").ToString.Trim
            Else
                Loggers.e("Error al obtener la configuracion de la plantilla " + prtNombrePlantilla + vbCr + conexion.Mensaje, MyClass.GetType.Name)
                Exit Sub
            End If
        Else
            Loggers.e("No se pudo obtener la configuracion de la plantilla " + prtNombrePlantilla, MyClass.GetType.Name)
            Exit Sub
        End If

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim http As HttpWebRequest = CType(WebRequest.Create(New Uri(baseAddress)), HttpWebRequest)
        http.Accept = vlAcepta
        http.ContentType = vlContenido
        http.Method = vlMetodo
        http.PreAuthenticate = vlPreAutenticado

        Dim vlCabecerosClase() As CabecerosRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(Of CabecerosRequest())(vlCabeceros)

        For Each cab As CabecerosRequest In vlCabecerosClase
            http.Headers.Add(cab.cabecero, cab.valor)
        Next

        Dim notificacion As New NotificacionesRequest
        Dim from As New from
        Dim merge_info As New merge_info

        notificacion.mail_template_key = vlKeyTemplate
        from.address = Configuracion.Sender
        from.name = Configuracion.Etiqueta
        notificacion.from = from

        Dim RecorreCorreos = prtCorreos.Split(",")

        Dim para(RecorreCorreos.Length - 1) As Para

        For j As Integer = 0 To DirectCast(RecorreCorreos, String()).Length - 1
            para(j) = New Para
            para(j).email_address = New email_address
            para(j).email_address.address = RecorreCorreos(j)
            para(j).email_address.name = RecorreCorreos(j)

        Next

        notificacion.para = para

        merge_info.AsuntoCorreo = flQuitaCaraceteresSinAcento(prtAsuntoCorreo)
        merge_info.CuerpoCorreo = flQuitaCaraceteres(prtCuerpoCorreo)

        'Gilberto Madrid 02/12/2025 quitamos esto ya que se agrego que se adjuntaran los documentos en la misma plantilla
        'merge_info.ArchivosAdjuntos = ""
        'Dim RecorreArchivos = prtArchivosAdjuntos.Split(",")
        'For j As Integer = 0 To DirectCast(RecorreArchivos, String()).Length - 1
        'If RecorreArchivos(j).ToString.Trim() <> "" Then
        ' If RecorreArchivos(j).ToString.Substring(RecorreArchivos(j).ToString().Length - 3).ToUpper() = "PDF" Then
        ' merge_info.ArchivosAdjuntos = merge_info.ArchivosAdjuntos + vlIconoPDF.Replace("REEMPLAZA", RecorreArchivos(j).ToString)
        'Else
        ' If RecorreArchivos(j).ToString.Substring(RecorreArchivos(j).ToString().Length - 3).ToUpper() = "XML" Then
        ' merge_info.ArchivosAdjuntos = merge_info.ArchivosAdjuntos + vlIconoXML.Replace("REEMPLAZA", RecorreArchivos(j).ToString)
        ' Else
        ' merge_info.ArchivosAdjuntos = merge_info.ArchivosAdjuntos + "<a href=" + RecorreArchivos(j).ToString + "<img width=""40px"" height=""40px"" src=""http://sensa.com.mx/adsum/ICONOS/ARCHIVO.png"" /></a>"
        ' End If
        ' End If
        ' End If
        ' Next

        notificacion.merge_info = merge_info

        'Gilberto Madrid 02/12/2025 con esto dejamos que en la plantilla se agreguen los documentos adjuntos
        '***************************************************************************************************
        Dim RecorreArchivos = prtArchivosAdjuntos.Split(",")
        'Dim attachments() As attachments
        Dim vlArchivosAdjuntosBase64 As List(Of attachments) = New List(Of attachments)
        Dim archivoadjunto As attachments
        Dim vlNombreArchivo
        Dim bytesArchivo() As Byte
        Dim cadenaBase64 As String
        Loggers.d("Antes de crear los archivos a byte base 64 - " + prtArchivosAdjuntos, MyClass.GetType.Name)
        For j As Integer = 0 To DirectCast(RecorreArchivos, String()).Length - 1
            If RecorreArchivos(j).ToString.Trim() <> "" Then
                Loggers.d("creando archivo a base 64 - " + RecorreArchivos(j).ToString.Trim(), MyClass.GetType.Name)
                archivoadjunto = New attachments
                bytesArchivo = File.ReadAllBytes(RecorreArchivos(j).ToString.Trim())
                cadenaBase64 = Convert.ToBase64String(bytesArchivo)
                vlNombreArchivo = RecorreArchivos(j).ToString.Trim().Split("\")
                archivoadjunto.name = vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1)
                archivoadjunto.content = cadenaBase64
                If archivoadjunto.name.Substring(archivoadjunto.name.ToString().Length - 3).ToUpper() = "PDF" Then
                    archivoadjunto.mime_type = "application/pdf"
                Else
                    If archivoadjunto.name.Substring(archivoadjunto.name.ToString().Length - 3).ToUpper() = "XML" Then
                        archivoadjunto.mime_type = "text/xml"
                    Else
                        archivoadjunto.mime_type = "text/plain"
                    End If
                End If
                vlArchivosAdjuntosBase64.Add(archivoadjunto)
            End If
        Next

        Loggers.d("terminando de crear los archivos a base 64", MyClass.GetType.Name)

        notificacion.attachments = vlArchivosAdjuntosBase64
        '***************************************************************************************************

        Dim jsonString As String = JsonConvert.SerializeObject(notificacion)

        Dim parsedContent As JObject = JObject.Parse(jsonString)
        Loggers.d("Json que se mandara en la peticion sendmailapi" + vbCr + "Plantilla: " + prtNombrePlantilla + vbCr + parsedContent.ToString(), MyClass.GetType.Name)

        Try
            Dim encoding As New ASCIIEncoding()
            Dim bytes As Byte() = encoding.GetBytes(parsedContent.ToString())


            Using stream As Stream = http.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
                stream.Close()
            End Using

            Dim response As HttpWebResponse = CType(http.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim result As String = reader.ReadToEnd()
                Loggers.d("Resultado de la peticion sendmailapi" + vbCr + "Plantilla: " + prtNombrePlantilla + vbCr + result, MyClass.GetType.Name)
            End Using
        Catch ex As Exception
            Loggers.e("Error en la peticion sendmailapi" + vbCr + "Plantilla: " + prtNombrePlantilla + vbCr + ex.Message, MyClass.GetType.Name)
        End Try

    End Sub

    Function flQuitaCaraceteres(prtTexto As String) As String
        prtTexto = prtTexto.Replace("á", "&aacute;")
        prtTexto = prtTexto.Replace("é", "&eacute;")
        prtTexto = prtTexto.Replace("í", "&iacute;")
        prtTexto = prtTexto.Replace("ó", "&oacute;")
        prtTexto = prtTexto.Replace("ú", "&uacute;")

        prtTexto = prtTexto.Replace("Á", "&Aacute;")
        prtTexto = prtTexto.Replace("É", "&Eacute;")
        prtTexto = prtTexto.Replace("Í", "&Iacute;")
        prtTexto = prtTexto.Replace("Ó", "&Oacute;")
        prtTexto = prtTexto.Replace("Ú", "&Uacute;")

        prtTexto = prtTexto.Replace("ñ", "&ntilde;")
        prtTexto = prtTexto.Replace("Ñ", "&Ntilde;")
        prtTexto = prtTexto.Replace("'", """")

        Return prtTexto

    End Function

    Function flQuitaCaraceteresSinAcento(prtTexto As String) As String
        prtTexto = prtTexto.Replace("á", "a")
        prtTexto = prtTexto.Replace("é", "e")
        prtTexto = prtTexto.Replace("í", "i")
        prtTexto = prtTexto.Replace("ó", "o")
        prtTexto = prtTexto.Replace("ú", "u")

        prtTexto = prtTexto.Replace("Á", "A")
        prtTexto = prtTexto.Replace("É", "E")
        prtTexto = prtTexto.Replace("Í", "I")
        prtTexto = prtTexto.Replace("Ó", "O")
        prtTexto = prtTexto.Replace("Ú", "U")

        Return prtTexto

    End Function

    'Function flSubirArchivoSFTP(prtNombreServidorFTP As String, prtRutaArchivo As String) As String

    '    If prtRutaArchivo.Trim = "" Then
    '        Loggers.e("La ruta del archivo viene vacia " + prtRutaArchivo, MyClass.GetType.Name)
    '        Return ""
    '    End If


    '    Dim baseAddress As String
    '    Dim vlUsuario As String
    '    Dim vlContraseña As String
    '    Dim vlRutaWeb As String
    '    Dim vlPuerto As String
    '    Dim vlcRutaServidorSFTP As String

    '    conexion.plObtenConfiguracionServidorFTP(prtNombreServidorFTP)
    '    If conexion.dtaux2.Rows.Count > 0 Then
    '        If conexion.Mensaje = "" Then
    '            baseAddress = conexion.dtaux2.Rows(0)("cUrl").ToString.Trim
    '            vlUsuario = conexion.dtaux2.Rows(0)("cUsuario").ToString.Trim
    '            vlContraseña = conexion.dtaux2.Rows(0)("cContraseña").ToString.Trim
    '            vlRutaWeb = conexion.dtaux2.Rows(0)("cRutaArchivoWeb").ToString.Trim
    '            vlPuerto = conexion.dtaux2.Rows(0)("cPuerto").ToString.Trim
    '            vlcRutaServidorSFTP = conexion.dtaux2.Rows(0)("cRutaServidorSFTP").ToString.Trim
    '        Else
    '            Loggers.e("Error al obtener la configuracion del servidor FTP " + prtNombreServidorFTP + vbCr + conexion.Mensaje, MyClass.GetType.Name)
    '            Return ""
    '        End If
    '    Else
    '        Loggers.e("No se pudo obtener la configuracion dedel servidor FTP " + prtNombreServidorFTP, MyClass.GetType.Name)
    '        Return ""
    '    End If

    '    Try
    '        Dim vlNombreArchivo = prtRutaArchivo.Split("\")

    '        If vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1).Trim = "" Then
    '            Loggers.e("La ruta del archivo viene vacia " + vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1).Trim, MyClass.GetType.Name)
    '            Return ""
    '        End If

    '        prtNombreServidorFTP = vlcRutaServidorSFTP + vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1)

    '        Using sftpClient As New SftpClient(baseAddress, CInt(vlPuerto), vlUsuario, vlContraseña)
    '            sftpClient.Connect()

    '            ' 2. Abrir el archivo local para lectura
    '            Using fileStream As FileStream = File.OpenRead(prtRutaArchivo)
    '                ' 3. Subir el archivo
    '                ' El método UploadFile establece el nombre del archivo en el destino remoto
    '                ' Si desea que tenga un nombre diferente, puede usar el método UploadFile(FileStream, String)
    '                sftpClient.UploadFile(fileStream, prtNombreServidorFTP)
    '            End Using

    '            ' 4. Desconectar (se hace automáticamente al salir del bloque Using)
    '        End Using

    '        Loggers.d("El archivo se subió con éxito al servidor SFTP " + prtNombreServidorFTP, MyClass.GetType.Name)

    '        Return vlRutaWeb + vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1)

    '    Catch ex As Exception
    '        Loggers.e("Error al subir el archivo al servidor SFTP: " + ex.Message + " " + prtNombreServidorFTP, MyClass.GetType.Name)
    '        Return ""
    '    End Try

    'End Function

    'Function flSubirArchivoFTP(prtNombreServidorFTP As String, prtRutaArchivo As String) As String

    '    If prtRutaArchivo.Trim = "" Then
    '        Loggers.e("La ruta del archivo viene vacia " + prtRutaArchivo, MyClass.GetType.Name)
    '        Return ""
    '    End If

    '    conexion.plObtenConfiguracionServidorFTP(prtNombreServidorFTP)
    '    Dim baseAddress As String
    '    Dim vlUsuario As String
    '    Dim vlContraseña As String
    '    Dim vlRutaWeb As String
    '    Dim vlcRutaServidorSFTP As String

    '    If conexion.dtaux2.Rows.Count > 0 Then
    '        If conexion.Mensaje = "" Then
    '            baseAddress = conexion.dtaux2.Rows(0)("cUrl").ToString.Trim
    '            vlUsuario = conexion.dtaux2.Rows(0)("cUsuario").ToString.Trim
    '            vlContraseña = conexion.dtaux2.Rows(0)("cContraseña").ToString.Trim
    '            vlRutaWeb = conexion.dtaux2.Rows(0)("cRutaArchivoWeb").ToString.Trim
    '            vlcRutaServidorSFTP = conexion.dtaux2.Rows(0)("cRutaServidorSFTP").ToString.Trim
    '        Else
    '            Loggers.e("Error al obtener la configuracion del servidor FTP " + prtNombreServidorFTP + vbCr + conexion.Mensaje, MyClass.GetType.Name)
    '            Return ""
    '        End If
    '    Else
    '        Loggers.e("No se pudo obtener la configuracion dedel servidor FTP " + prtNombreServidorFTP, MyClass.GetType.Name)
    '        Return ""
    '    End If

    '    If vlcRutaServidorSFTP.Trim <> "" Then
    '        Loggers.d("El servidor esta configurado para SFTP y se ejecutara como tal " + prtNombreServidorFTP + vbCr + conexion.Mensaje, MyClass.GetType.Name)
    '        flSubirArchivoFTP = flSubirArchivoSFTP(prtNombreServidorFTP, prtRutaArchivo)
    '        Loggers.d("Termino de ejcutarse como SFTP " + prtNombreServidorFTP + vbCr + conexion.Mensaje, MyClass.GetType.Name)
    '        Exit Function
    '    End If

    '    Try
    '        Dim vlNombreArchivo = prtRutaArchivo.Split("\")

    '        If vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1).Trim = "" Then
    '            Loggers.e("La ruta del archivo viene vacia " + vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1).Trim, MyClass.GetType.Name)
    '            Return ""
    '        End If

    '        prtNombreServidorFTP = baseAddress + vlNombreArchivo(DirectCast(vlNombreArchivo, String()).Length - 1)

    '        Dim ftpUri As New Uri(String.Format(prtNombreServidorFTP))
    '        Dim request As FtpWebRequest = DirectCast(FtpWebRequest.Create(ftpUri), FtpWebRequest)
    '        request.Credentials = New NetworkCredential(vlUsuario, vlContraseña)
    '        request.Method = WebRequestMethods.Ftp.UploadFile
    '        Dim fileToUpload As Byte() = File.ReadAllBytes(prtRutaArchivo)
    '        Dim requestStream As Stream = request.GetRequestStream()
    '        requestStream.Write(fileToUpload, 0, fileToUpload.Length)
    '        requestStream.Close()
    '        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
    '        Loggers.d("El archivo se subió con éxito al servidor FTP. Estado: " + response.StatusDescription + " " + prtNombreServidorFTP, MyClass.GetType.Name)
    '        response.Close()
    '        Return prtNombreServidorFTP.Replace(baseAddress, vlRutaWeb)
    '    Catch ex As Exception
    '        Loggers.e("Error al subir el archivo al servidor FTP: " + ex.Message + " " + prtNombreServidorFTP, MyClass.GetType.Name)
    '        Return ""
    '    End Try

    'End Function
End Class