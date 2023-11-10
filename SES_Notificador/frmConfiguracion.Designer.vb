<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguracion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfiguracion))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBaseDatos = New System.Windows.Forms.TextBox()
        Me.txtServidor = New System.Windows.Forms.TextBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSender = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkSSL = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtnPuerto = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtServidorSalida = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcContraseña = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtcCorreo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnSucursal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkFuncionPortal = New System.Windows.Forms.CheckBox()
        Me.txtEtiqueta = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Base de Datos:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(76, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Servidor:"
        '
        'txtBaseDatos
        '
        Me.txtBaseDatos.Enabled = False
        Me.txtBaseDatos.Location = New System.Drawing.Point(147, 69)
        Me.txtBaseDatos.Name = "txtBaseDatos"
        Me.txtBaseDatos.Size = New System.Drawing.Size(220, 20)
        Me.txtBaseDatos.TabIndex = 2
        '
        'txtServidor
        '
        Me.txtServidor.Enabled = False
        Me.txtServidor.Location = New System.Drawing.Point(147, 29)
        Me.txtServidor.Name = "txtServidor"
        Me.txtServidor.Size = New System.Drawing.Size(220, 20)
        Me.txtServidor.TabIndex = 1
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(147, 327)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(122, 23)
        Me.btnGuardar.TabIndex = 17
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtEtiqueta)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtSender)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.chkSSL)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtnPuerto)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtServidorSalida)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtcContraseña)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtcCorreo)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 109)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(235, 193)
        Me.Panel1.TabIndex = 11
        '
        'txtSender
        '
        Me.txtSender.Enabled = False
        Me.txtSender.Location = New System.Drawing.Point(85, 6)
        Me.txtSender.Name = "txtSender"
        Me.txtSender.Size = New System.Drawing.Size(119, 20)
        Me.txtSender.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Correo:"
        '
        'chkSSL
        '
        Me.chkSSL.AutoSize = True
        Me.chkSSL.Enabled = False
        Me.chkSSL.Location = New System.Drawing.Point(85, 137)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(46, 17)
        Me.chkSSL.TabIndex = 15
        Me.chkSSL.Text = "SSL"
        Me.chkSSL.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Usa SSL:"
        '
        'txtnPuerto
        '
        Me.txtnPuerto.Enabled = False
        Me.txtnPuerto.Location = New System.Drawing.Point(85, 109)
        Me.txtnPuerto.Name = "txtnPuerto"
        Me.txtnPuerto.Size = New System.Drawing.Size(119, 20)
        Me.txtnPuerto.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Puerto:"
        '
        'txtServidorSalida
        '
        Me.txtServidorSalida.Enabled = False
        Me.txtServidorSalida.Location = New System.Drawing.Point(85, 83)
        Me.txtServidorSalida.Name = "txtServidorSalida"
        Me.txtServidorSalida.Size = New System.Drawing.Size(119, 20)
        Me.txtServidorSalida.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Servidor salida:"
        '
        'txtcContraseña
        '
        Me.txtcContraseña.Enabled = False
        Me.txtcContraseña.Location = New System.Drawing.Point(85, 58)
        Me.txtcContraseña.Name = "txtcContraseña"
        Me.txtcContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtcContraseña.Size = New System.Drawing.Size(119, 20)
        Me.txtcContraseña.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Contraseña:"
        '
        'txtcCorreo
        '
        Me.txtcCorreo.Enabled = False
        Me.txtcCorreo.Location = New System.Drawing.Point(85, 32)
        Me.txtcCorreo.Name = "txtcCorreo"
        Me.txtcCorreo.Size = New System.Drawing.Size(119, 20)
        Me.txtcCorreo.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Usuario:"
        '
        'txtnSucursal
        '
        Me.txtnSucursal.Enabled = False
        Me.txtnSucursal.Location = New System.Drawing.Point(330, 117)
        Me.txtnSucursal.Name = "txtnSucursal"
        Me.txtnSucursal.Size = New System.Drawing.Size(70, 20)
        Me.txtnSucursal.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(253, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "No. Sucursal;"
        '
        'chkFuncionPortal
        '
        Me.chkFuncionPortal.AutoSize = True
        Me.chkFuncionPortal.Enabled = False
        Me.chkFuncionPortal.Location = New System.Drawing.Point(256, 167)
        Me.chkFuncionPortal.Name = "chkFuncionPortal"
        Me.chkFuncionPortal.Size = New System.Drawing.Size(98, 17)
        Me.chkFuncionPortal.TabIndex = 18
        Me.chkFuncionPortal.Text = "Uso para portal"
        Me.chkFuncionPortal.UseVisualStyleBackColor = True
        '
        'txtEtiqueta
        '
        Me.txtEtiqueta.Enabled = False
        Me.txtEtiqueta.Location = New System.Drawing.Point(85, 160)
        Me.txtEtiqueta.Name = "txtEtiqueta"
        Me.txtEtiqueta.Size = New System.Drawing.Size(119, 20)
        Me.txtEtiqueta.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 163)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Etiqueta correo:"
        '
        'frmConfiguracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(412, 362)
        Me.Controls.Add(Me.chkFuncionPortal)
        Me.Controls.Add(Me.txtnSucursal)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBaseDatos)
        Me.Controls.Add(Me.txtServidor)
        Me.Controls.Add(Me.btnGuardar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmConfiguracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmConfiguracion"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBaseDatos As System.Windows.Forms.TextBox
    Friend WithEvents txtServidor As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtcContraseña As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcCorreo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtnPuerto As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtServidorSalida As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtnSucursal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkFuncionPortal As System.Windows.Forms.CheckBox
    Friend WithEvents txtSender As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtEtiqueta As TextBox
    Friend WithEvents Label10 As Label
End Class
