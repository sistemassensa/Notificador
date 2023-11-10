<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDI
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDI))
        Me.tmrNotificaciones = New System.Windows.Forms.Timer(Me.components)
        Me.tmrTrabajos = New System.Windows.Forms.Timer(Me.components)
        Me.NtiMensaje = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CerrarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstatusTimerTrabajosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstatusTimerNotificacionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarArchivosParaPortalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrSolicitudAutorizacionCorreo = New System.Windows.Forms.Timer(Me.components)
        Me.tmrServicioMonedero = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrNotificaciones
        '
        Me.tmrNotificaciones.Interval = 60000
        '
        'tmrTrabajos
        '
        Me.tmrTrabajos.Interval = 3600000
        '
        'NtiMensaje
        '
        Me.NtiMensaje.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NtiMensaje.Icon = CType(resources.GetObject("NtiMensaje.Icon"), System.Drawing.Icon)
        Me.NtiMensaje.Text = "SES Notificaciones"
        Me.NtiMensaje.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CerrarToolStripMenuItem, Me.EstatusTimerTrabajosToolStripMenuItem, Me.EstatusTimerNotificacionesToolStripMenuItem, Me.CopiarArchivosParaPortalToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(224, 92)
        '
        'CerrarToolStripMenuItem
        '
        Me.CerrarToolStripMenuItem.Name = "CerrarToolStripMenuItem"
        Me.CerrarToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.CerrarToolStripMenuItem.Text = "&Cerrar"
        '
        'EstatusTimerTrabajosToolStripMenuItem
        '
        Me.EstatusTimerTrabajosToolStripMenuItem.Name = "EstatusTimerTrabajosToolStripMenuItem"
        Me.EstatusTimerTrabajosToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.EstatusTimerTrabajosToolStripMenuItem.Text = "Estatus Timer &Trabajos"
        '
        'EstatusTimerNotificacionesToolStripMenuItem
        '
        Me.EstatusTimerNotificacionesToolStripMenuItem.Name = "EstatusTimerNotificacionesToolStripMenuItem"
        Me.EstatusTimerNotificacionesToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.EstatusTimerNotificacionesToolStripMenuItem.Text = "Estatus Timer &Notificaciones"
        '
        'CopiarArchivosParaPortalToolStripMenuItem
        '
        Me.CopiarArchivosParaPortalToolStripMenuItem.Name = "CopiarArchivosParaPortalToolStripMenuItem"
        Me.CopiarArchivosParaPortalToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.CopiarArchivosParaPortalToolStripMenuItem.Text = "C&opiar archivos para portal"
        '
        'tmrSolicitudAutorizacionCorreo
        '
        Me.tmrSolicitudAutorizacionCorreo.Interval = 60000
        '
        'tmrServicioMonedero
        '
        '
        'frmMDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 374)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMDI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modulo de notificaciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmrNotificaciones As System.Windows.Forms.Timer
    Friend WithEvents tmrTrabajos As System.Windows.Forms.Timer
    Friend WithEvents NtiMensaje As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CerrarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstatusTimerTrabajosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstatusTimerNotificacionesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopiarArchivosParaPortalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrSolicitudAutorizacionCorreo As Timer
    Friend WithEvents tmrServicioMonedero As Timer
End Class
