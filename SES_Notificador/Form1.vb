Public Class frmIniciaSesion

    Dim frmConfiguracion As frmConfiguracion
    Dim Conexion As Conexion
    Dim Configuracion As New Configuracion
    Dim FormularioPadre As frmMDI


    Private Sub frmIniciaSesion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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
