<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIniciaSesion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIniciaSesion))
        Me.pbIniciaSesion = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bntIniciaSesion = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtContraseña = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        CType(Me.pbIniciaSesion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbIniciaSesion
        '
        Me.pbIniciaSesion.Dock = System.Windows.Forms.DockStyle.Top
        Me.pbIniciaSesion.Location = New System.Drawing.Point(0, 0)
        Me.pbIniciaSesion.Name = "pbIniciaSesion"
        Me.pbIniciaSesion.Size = New System.Drawing.Size(466, 135)
        Me.pbIniciaSesion.TabIndex = 5
        Me.pbIniciaSesion.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(228, 265)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(234, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Todos los derechos reservados Sistemas SENSA 2018"
        '
        'bntIniciaSesion
        '
        Me.bntIniciaSesion.Location = New System.Drawing.Point(277, 225)
        Me.bntIniciaSesion.Name = "bntIniciaSesion"
        Me.bntIniciaSesion.Size = New System.Drawing.Size(118, 23)
        Me.bntIniciaSesion.TabIndex = 11
        Me.bntIniciaSesion.Text = "Iniciar Sesíon"
        Me.bntIniciaSesion.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(95, 225)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(118, 23)
        Me.btnCancelar.TabIndex = 12
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 185)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Contraseña:"
        '
        'txtContraseña
        '
        Me.txtContraseña.Location = New System.Drawing.Point(128, 182)
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContraseña.Size = New System.Drawing.Size(241, 20)
        Me.txtContraseña.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Usuario:"
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(128, 146)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(241, 20)
        Me.txtUsuario.TabIndex = 8
        '
        'frmIniciaSesion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(466, 281)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.bntIniciaSesion)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtContraseña)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUsuario)
        Me.Controls.Add(Me.pbIniciaSesion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIniciaSesion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.pbIniciaSesion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbIniciaSesion As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents bntIniciaSesion As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtContraseña As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox

End Class
