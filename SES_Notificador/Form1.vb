
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class frmIniciaSesion

    Dim frmConfiguracion As frmConfiguracion
    Dim Conexion As Conexion
    Dim Configuracion As New Configuracion
    Dim FormularioPadre As frmMDI

    Sub plEnviaPruebaApiCorreo()

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim baseAddress As String = "https://api.zeptomail.com/v1.1/email/template"

        Dim http As HttpWebRequest = CType(WebRequest.Create(New Uri(baseAddress)), HttpWebRequest)
        http.Accept = "application/json"
        http.ContentType = "application/json"
        http.Method = "POST"
        http.PreAuthenticate = True
        http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60n/kKhDv8pzzKrJewwkVUGVF+jFkV12FT343D4G/HA9cdtlUyYBATyT/UXGGNuFjEb9b0gzB8C2jII3t98n19WCiiF9mqRe1U4J3x17qnvhDzNWGlYkRKPKIkIxwxok2hlFswj+g==")

        Dim jsonString As String = "{'mail_template_key': '2d6f.3729e003e89830a5.k1.39e249d1-bf22-11f0-99c3-5254005934b4.19a73ebaee6'," &
                                 "'from': {'address': 'no-reply@sensa.com.mx', 'name': 'noreply'}," &
                                  "'to': [{'email_address': {'address': 'sistemas.gerencia@sensa.com.mx','name': 'Gilberto de Jesús Madrid Velarde'}},{'email_address': {'address': 'sistemas@sensa.com.mx','name': 'prueba'}}]," &
                                  "'merge_info': {'CuerpoCorreo':'<html><head><meta charset=""UTF-8""></head><body><b><font face=""arial"" color=#084B8A>Buen dia:</font></b><pre></pre><font face=""arial"" color=#084B8A>Le informamos que ha sido facturado por <b>$10,944.74 MXN</b> con modificaci&oacute;n en precios en la sucursal: <b>TIJUANA</b></font><pre></pre><font face=""arial"" color=#084B8A> . </font><pre></pre><font face=""arial"" color=#084B8A><b>Fecha:</b> <u>11/11/2025</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Folio:</b> <u>J366867</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Importe:</b> <u>$10,944.74 MXN</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Cliente:</b> <u>012725(001) CARLOS DARWIN PEREZ VAZQUEZ</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Agente: </b> <u>COTA  KEVIN</u></font><pre></pre><font face=""arial"" color=#084B8A><b>Registrado por: </b> <u>KEVIN COTA</u></font><pre></pre><table border = ""1""><tr><th>Tipo</th><th>Articulo</th><th>Descripcion</th><th>Unidad</th><th>Moneda</th><th>Costo</th><th>Costo Flete</th><th>Costo Arancel</th><th>CostoFinal</th><th>PrecioFinal</th><th>Utilidad %</th><th>Precio Lista</th><th>Diferencia</th></tr><tr><td>A</td><td>CINT0007            </td><td>CINTA SUPER 33 *NACIONAL* 3/4"" X 20 MT</td><td>RLL</td><td>DOLAR</td><td>$3.47 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$3.47 USD</td><td>$3.85 USD</td><td>11.00</td><td>$3.85 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>CINT0037            </td><td>CINTA TEMFLEX 1600 NEGRA 3/4"" 18 MTS (UU009472471)</td><td>RLL</td><td>DOLAR</td><td>$0.90 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.90 USD</td><td>$0.99 USD</td><td>10.99</td><td>$0.99 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>SIEM0002            </td><td>Q120  BREAKER 1X20 QP</td><td>PZA</td><td>PESOS</td><td>$79.56 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$79.56 MXN</td><td>$87.21 MXN</td><td>9.61</td><td>$89.90 MXN</td><td>$-2.70 MXN</td></tr><tr><td>A</td><td>SIEM0003            </td><td>Q130 BREAKER 1X30 QP</td><td>PZA</td><td>PESOS</td><td>$79.56 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$79.56 MXN</td><td>$87.21 MXN</td><td>9.61</td><td>$89.90 MXN</td><td>$-2.70 MXN</td></tr><tr><td>A</td><td>COND0077            </td><td>ROLLO CABLE THHN 12, NEGRO 100 MTRS COND/VIAK</td><td>RLL</td><td>PESOS</td><td>$970.55 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$970.55 MXN</td><td>$983.95 MXN</td><td>1.38</td><td>$1,077.31 MXN</td><td>$-93.36 MXN</td></tr><tr><td>A</td><td>COND0078            </td><td>ROLLO CABLE THHN 12, BLANCO 100 MTRS COND/VIAK</td><td>RLL</td><td>PESOS</td><td>$970.55 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$970.55 MXN</td><td>$983.95 MXN</td><td>1.38</td><td>$1,077.31 MXN</td><td>$-93.36 MXN</td></tr><tr><td>A</td><td>VIAK0042            </td><td>ALAMBRE 2X12 UF PLANO VIAKON</td><td>RLL</td><td>PESOS</td><td>$2,282.28 MXN</td><td>$0.00 MXN</td><td>$0.00 MXN</td><td>$2,282.28 MXN</td><td>$2,487.69 MXN</td><td>9.00</td><td>$2,487.69 MXN</td><td>$0.00 MXN</td></tr><tr><td>A</td><td>TPVC0031            </td><td>COPLE PVC 1/2 GRIS</td><td>PZA</td><td>DOLAR</td><td>$0.09 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.09 USD</td><td>$0.10 USD</td><td>14.00</td><td>$0.10 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>TPVC0048            </td><td>ADAPTER MACHO PVC 1 GRIS</td><td>PZA</td><td>DOLAR</td><td>$0.11 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$0.11 USD</td><td>$0.13 USD</td><td>14.00</td><td>$0.13 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>LEVI0020            </td><td>49875 ROSETA S/CADENA LEVITON</td><td>PZA</td><td>DOLAR</td><td>$1.29 USD</td><td>$0.00 USD</td><td>$0.00 USD</td><td>$1.29 USD</td><td>$1.46 USD</td><td>13.00</td><td>$1.46 USD</td><td>$0.00 USD</td></tr><tr><td>A</td><td>TUBE0058            </td><td>643S COPLE STEEL 1</td><td>PZA</td><td>DOLAR</td><td>$0.47 USD</td><td>$0.00 USD</td><td>$0.02 USD</td><td>$0.50 USD</td><td>$0.58 USD</td><td>21.79</td><td>$0.58 USD</td><td>$0.00 USD</td></tr></table></body></html>','ArchivosAdjuntos':'<a href=""https://sensa.com.mx/adsum/FACTURAS/BRAVO/ArchivoNuevo.pdf""><img width=""40px"" height=""40px"" src=""http://sensa.com.mx/adsum/ICONOS/PDF.png"" /></a><a href=""https://sensa.com.mx/adsum/FACTURAS/BRAVO/ArchivoNuevo.pdf""><img width=""40px"" height=""40px"" src=""http://sensa.com.mx/adsum/ICONOS/XML.png"" /></a>','AsuntoCorreo':'Alerta de Facturación con modificación en precios SEN8406155U8    (TIJUANA) (PEVC940201U77 - 11/11/2025 - $10,944.74 MXN)'}}"

        Dim parsedContent As JObject = JObject.Parse(jsonString)
        Console.WriteLine(parsedContent.ToString())

        Dim encoding As New ASCIIEncoding()
        Dim bytes As Byte() = encoding.GetBytes(parsedContent.ToString())


        Using stream As Stream = http.GetRequestStream()
            stream.Write(bytes, 0, bytes.Length)
            stream.Close()
        End Using

        Dim response As HttpWebResponse = CType(http.GetResponse(), HttpWebResponse)
        Using reader As New StreamReader(response.GetResponseStream())
            Dim result As String = reader.ReadToEnd()
            Console.WriteLine(result)
        End Using
    End Sub
    Sub plSubirArchivoFTP()
        Dim ftpUri As New Uri(String.Format("ftp://ftp.sensa.com.mx/public_html/adsum/FACTURAS/BRAVO/ArchivoNuevo.pdf"))
        Dim request As FtpWebRequest = DirectCast(FtpWebRequest.Create(ftpUri), FtpWebRequest)
        request.Credentials = New NetworkCredential("sensacom", "Al87tF6pSHEr")
        request.Method = WebRequestMethods.Ftp.UploadFile
        Dim fileToUpload As Byte() = File.ReadAllBytes("C:/Users/sistemas.gerencia/Desktop/ArchivoNotificacion_20251110101348.pdf")
        Dim requestStream As Stream = request.GetRequestStream()
        requestStream.Write(fileToUpload, 0, fileToUpload.Length)
        requestStream.Close()
        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        Console.WriteLine(String.Format("El archivo se subió con éxito. Estado: {0}", response.StatusDescription))
        response.Close()
    End Sub
    Private Sub frmIniciaSesion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Gilberto Madrid -- codigo para enviar correo de notificaciones por API
        'plEnviaPruebaApiCorreo()

        'Gilberto Madrid -- codigo para subir un archivo un aun FTP
        'plSubirArchivoFTP


        If System.Globalization.CultureInfo.CurrentCulture.ToString().ToUpper.Contains("MX") = False Then
            MessageBox.Show("La configuracion Regional no esta establecida en Español(Mexico), Cambielo para poder acceder por favor", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.ExitThread()
        End If

        Dim archivo As New IO.FileInfo(My.Application.Info.DirectoryPath + "\Imagen_Notificador_SES.jpg")
        If archivo.Exists = True Then
            pbIniciaSesion.ImageLocation = My.Application.Info.DirectoryPath + "\Imagen_Notificador_SES.jpg"
            pbIniciaSesion.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            MessageBox.Show("No se ha podido cargar la imagen", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        txtUsuario.Text = Configuracion.UltimoUsuario
        txtContraseña.Focus()

    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click

        If (MessageBox.Show("Esta Seguro de Cerrar la Aplicacion?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes) Then
            Application.ExitThread()
        End If

    End Sub

    Private Sub txtContraseña_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtContraseña.KeyPress

        If e.KeyChar = ChrW(13) Then
            bntIniciaSesion_Click(bntIniciaSesion, Nothing)
        End If

        If e.KeyChar = ChrW(27) Then
            frmConfiguracion = New frmConfiguracion()
            frmConfiguracion.ShowDialog()
        End If

    End Sub

    Private Sub PlResaltaFuenteControl(ByRef prmControl As Object, ByVal prmResaltar As Boolean)
        Dim fuente As System.Drawing.Font

        If prmResaltar Then
            fuente = New System.Drawing.Font(bntIniciaSesion.Font.FontFamily, bntIniciaSesion.Font.Size, System.Drawing.FontStyle.Bold)
        Else
            fuente = New System.Drawing.Font(bntIniciaSesion.Font.FontFamily, bntIniciaSesion.Font.Size, System.Drawing.FontStyle.Regular)
        End If

        prmControl.font = fuente
    End Sub

    Private Sub btnCancelar_GotFocus(sender As Object, e As System.EventArgs) Handles btnCancelar.GotFocus
        PlResaltaFuenteControl(sender, True)
    End Sub
    Private Sub btnCancelar_LostFocus(sender As Object, e As System.EventArgs) Handles btnCancelar.LostFocus
        PlResaltaFuenteControl(sender, False)
    End Sub
    Private Sub bntIniciaSesion_GotFocus(sender As Object, e As System.EventArgs) Handles bntIniciaSesion.GotFocus
        PlResaltaFuenteControl(sender, True)
    End Sub
    Private Sub bntIniciaSesion_LostFocus(sender As Object, e As System.EventArgs) Handles bntIniciaSesion.LostFocus
        PlResaltaFuenteControl(sender, False)
    End Sub

    Private Sub txtUsuario_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsuario.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtContraseña.Focus()
        End If
    End Sub

    Private Sub bntIniciaSesion_MouseEnter(sender As System.Object, e As System.EventArgs) Handles bntIniciaSesion.MouseEnter
        PlResaltaFuenteControl(sender, True)
    End Sub

    Private Sub bntIniciaSesion_MouseLeave(sender As System.Object, e As System.EventArgs) Handles bntIniciaSesion.MouseLeave
        PlResaltaFuenteControl(sender, False)
    End Sub

    Private Sub btnCancelar_MouseEnter(sender As System.Object, e As System.EventArgs) Handles btnCancelar.MouseEnter
        PlResaltaFuenteControl(sender, True)
    End Sub

    Private Sub btnCancelar_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnCancelar.MouseLeave
        PlResaltaFuenteControl(sender, False)
    End Sub

    Private Sub bntIniciaSesion_Click(sender As System.Object, e As System.EventArgs) Handles bntIniciaSesion.Click

        If (txtUsuario.Text = "") Then
            MessageBox.Show("Debe Proporcionar Un Usuario", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        If (txtContraseña.Text = "") Then
            MessageBox.Show("Debe Proporcionar Una Contraseña", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        Conexion = New Conexion()
        If Conexion.Conectar(txtUsuario.Text.Trim(), txtContraseña.Text.Trim()) Then
            FormularioPadre = New frmMDI()
            FormularioPadre.Usuario = txtUsuario.Text.Trim()
            FormularioPadre.Contraseña = txtContraseña.Text.Trim()
            FormularioPadre.Show()
            Me.Hide()
        Else
            MessageBox.Show(Conexion.Mensaje + vbCr + "Verifique su usuario y contraseña" + vbCr + "O bien que no esten activadas las letras mayusculas", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtContraseña.Clear()
            txtContraseña.Focus()
        End If

    End Sub


End Class
