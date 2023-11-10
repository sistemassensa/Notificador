Public Class frmConfiguracion
    Dim configuracion As New Configuracion

    Private Sub frmConfiguracion_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

        If e.KeyChar = ChrW(27) Then
            txtServidor.Enabled = True
            txtBaseDatos.Enabled = True

            txtcCorreo.Enabled = True
            txtcContraseña.Enabled = True
            txtServidorSalida.Enabled = True
            txtnPuerto.Enabled = True
            chkSSL.Enabled = True
            txtnSucursal.Enabled = True
            'Gilberto Madrid 09/08/2019 se agrego un nuevo parametro para saber si funcionara como notificador o como aplicacion para el portal
            chkFuncionPortal.Enabled = True

            txtSender.Enabled = True
            txtEtiqueta.Enabled = True

        End If

    End Sub

    Private Sub frmConfiguracion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txtServidor.Text = configuracion.Servidor
        txtBaseDatos.Text = configuracion.BaseDeDatos

        txtcCorreo.Text = configuracion.Correo
        txtcContraseña.Text = configuracion.ContrasenaCorreo
        txtServidorSalida.Text = configuracion.ServidorSalidaCorreo
        txtnPuerto.Text = configuracion.PuertoCorreo
        chkSSL.Checked = configuracion.UsaSSL
        txtnSucursal.Text = configuracion.noSucursal
        'Gilberto Madrid 09/08/2019 se agrego un nuevo parametro para saber si funcionara como notificador o como aplicacion para el portal
        chkFuncionPortal.Checked = configuracion.FuncionPortal

        txtSender.Text = configuracion.Sender
        txtEtiqueta.Text = configuracion.Etiqueta

        btnGuardar.Focus()

    End Sub

    Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardar.Click
        Try
            configuracion.Servidor = txtServidor.Text.Trim()
            configuracion.BaseDeDatos = txtBaseDatos.Text.Trim()

            configuracion.Correo = txtcCorreo.Text
            configuracion.ContrasenaCorreo = txtcContraseña.Text
            configuracion.ServidorSalidaCorreo = txtServidorSalida.Text
            configuracion.PuertoCorreo = txtnPuerto.Text
            configuracion.UsaSSL = chkSSL.Checked
            configuracion.noSucursal = txtnSucursal.Text
            'Gilberto Madrid 09/08/2019 se agrego un nuevo parametro para saber si funcionara como notificador o como aplicacion para el portal
            configuracion.FuncionPortal = chkFuncionPortal.Checked

            configuracion.Sender = txtSender.Text.Trim
            configuracion.Etiqueta = txtEtiqueta.Text.Trim

            configuracion.Save()
            MessageBox.Show("Se han guardado correctamente los datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Catch ex As Exception
            MessageBox.Show("-Ha ocurrido un error al grabar los datos-" + vbCr + ex.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub
End Class