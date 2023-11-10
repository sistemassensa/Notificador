Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO

Public Class Conexion

    Public Usuario As String
    Public Contraseña As String
    Dim cadenaconexion As String
    Dim consulta As String
    Public Mensaje As String
    Dim comandosql As SqlCommand
    Dim da As SqlDataAdapter
    Public dt As New DataTable
    Public dtNotificaciones As New DataTable
    Public dtNotificacionesaux As New DataTable
    Public dtResultadorNotificacion As New DataTable
    Public dtTimbresLibres As New DataTable
    Public dtTrabajos As New DataTable
    Public dtaux As New DataTable
    Public dtaux2 As New DataTable
    Public dtauxCopiaArchivos As New DataTable

    Public dtSolicitudAutorizacionCorreo As DataTable
    Public dtSolicitudAutorizacionCorreoaux As DataTable

    Public Servidor As String
    Public BaseDatos As String
    Dim Configuracion As New Configuracion
    Dim conexionSQL As SqlConnection
    Dim transaccion As SqlTransaction

    Public dtWebServices As New DataTable
    Public dtResultadoWS As New DataTable

    Public Function Conectar(Usuario As String, Contraseña As String) As Boolean
        Servidor = Configuracion.Servidor
        BaseDatos = Configuracion.BaseDeDatos
        cadenaconexion = "Data Source=" + Servidor + ";Initial Catalog=" + BaseDatos + ";User ID=" + Usuario + ";Password=" + Contraseña + ";"
        consulta = "Select * From USUARIOS (Nolock) WHERE USULOGIN='" + Usuario + "'"
        da = Nothing
        Try
            da = New SqlDataAdapter(consulta, cadenaconexion)
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Conectar = False
            Else
                Conectar = True
            End If
        Catch ex As Exception
            Mensaje = ex.Message
            Conectar = False
        End Try
    End Function
    Sub DarValor()
        cadenaconexion = "Data Source=" + Configuracion.Servidor + ";Initial Catalog=" + Configuracion.BaseDeDatos + ";User ID=" + Usuario + ";Password=" + Contraseña + ";"
    End Sub

    Sub TraeNotificaciones()
        Mensaje = ""
        Try
            DarValor()
            consulta = "select * from SES_Notificaciones (NOLOCK) WHERE bActivo=1"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtNotificaciones = New DataTable
            da.Fill(dtNotificaciones)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub IniciaTransaccion()
        Mensaje = ""
        Try
            DarValor()
            conexionSQL = New SqlConnection(cadenaconexion)
            conexionSQL.Open()
            transaccion = conexionSQL.BeginTransaction()
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub EjecutaTexto(cTexto As String, Optional prmEjecutaSinExecute As Boolean = False)
        Mensaje = ""
        Try
            DarValor()
            If prmEjecutaSinExecute = False Then
                consulta = "execute(" + cTexto + ")"
            Else
                consulta = cTexto
            End If
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Connection = conexionSQL
            comandosql.Transaction = transaccion
            If comandosql.ExecuteNonQuery() <= 0 Then
                Mensaje = "No se actualizaron los datos"
            End If
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub HacerRollBack()
        Mensaje = ""
        Try
            transaccion.Rollback()
            conexionSQL.Close()
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub TerminaTransaccion()
        Mensaje = ""
        Try
            DarValor()
            transaccion.Commit()
            conexionSQL.Close()
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub TraeNotificacion(cNombreNotificacion As String)
        Mensaje = ""
        Try
            DarValor()
            consulta = "select * from SES_Notificaciones (NOLOCK) WHERE bActivo=1 AND cNombreNotificacion='" + cNombreNotificacion + "'"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtNotificacionesaux = New DataTable
            da.Fill(dtNotificacionesaux)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub EjecutaNotificacion(cNotificacionSQL As String, cParametros As String)
        Mensaje = ""
        Try
            DarValor()
            consulta = cNotificacionSQL.Trim + " " + cParametros.Trim
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtResultadorNotificacion = New DataTable
            da.SelectCommand.CommandTimeout = 60000
            da.Fill(dtResultadorNotificacion)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub EjecutaProcedimiento(cProcedimiento As String, cParametros As String(), cNombresParametros As String())
        Mensaje = ""
        Dim ParametrosSeparados As String
        ParametrosSeparados = ""
        Try
            consulta = cProcedimiento
            da = New SqlDataAdapter(consulta, cadenaconexion)
            da.SelectCommand.CommandType = CommandType.StoredProcedure

            For i As Integer = 0 To DirectCast(cParametros, String()).Length - 1

                da.SelectCommand.Parameters.AddWithValue(cNombresParametros(i).ToString.Trim, cParametros(i))

            Next
            dtaux = New DataTable
            da.Fill(dtaux)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub GuardaHistorialNotificaciones(prmNombreNotificacion As String, prmCodigo As String, prmAsunto As String, prmCuerpo As String, prmPie As String, prmCorreosNotificacion As String, prmArchivoNotificacion As String, prmUsuarioRegistro As String)

        Mensaje = ""
        Try

            Dim campoPDF As SqlBinary
            Dim Archivo As Byte()

            If prmArchivoNotificacion.Trim <> "" Then
                Archivo = File.ReadAllBytes(prmArchivoNotificacion)
                campoPDF = New SqlBinary(Archivo)
            End If

            If prmArchivoNotificacion.Trim <> "" Then
                consulta = "INSERT INTO SES_Notificaciones_Historial (cNombreNotificacion,cCodigo,dFecha,bActivo,cAsuntoCorreo,cCuerpoCorreo,cPieCorreo,cCorreoNotificacion,cArchivoNotificacion,UsuReg)"
                consulta = consulta + " SELECT '" + prmNombreNotificacion + "','" + prmCodigo.Replace("'", "''") + "',GETDATE(),1,'" + prmAsunto + "','" + prmCuerpo.Replace("'", "''") + "','" + prmPie + "','" + prmCorreosNotificacion + "',@ArchivoPDF,'" + prmUsuarioRegistro + "'"
            Else
                consulta = "INSERT INTO SES_Notificaciones_Historial (cNombreNotificacion,cCodigo,dFecha,bActivo,cAsuntoCorreo,cCuerpoCorreo,cPieCorreo,cCorreoNotificacion,UsuReg)"
                consulta = consulta + " SELECT '" + prmNombreNotificacion + "','" + prmCodigo.Replace("'", "''") + "',GETDATE(),1,'" + prmAsunto + "','" + prmCuerpo.Replace("'", "''") + "','" + prmPie + "','" + prmCorreosNotificacion + "','" + prmUsuarioRegistro + "'"
            End If

            conexionSQL = New SqlConnection(cadenaconexion)
            comandosql = New SqlCommand(consulta, conexionSQL)

            If prmArchivoNotificacion.Trim <> "" Then
                comandosql.Parameters.AddWithValue("@ArchivoPDF", campoPDF)
            End If

            conexionSQL.Open()
            comandosql.ExecuteNonQuery()
            conexionSQL.Close()

        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub TraeTrabajosSQL(cSucursal As String)
        Mensaje = ""
        Try
            Dim suc As String = ""
            Select Case cSucursal.Length
                Case 1 : suc = "00"
                Case 2 : suc = "0"
            End Select
            suc = suc + cSucursal
            DarValor()
            consulta = "SELECT cNombreTrabajo as Nombre,cSQL as Codigo,cParametros as Parametros,dFechaInicio as FechaInicio,dFechaUltimaEjecucion as UltimaEjecucion,cFrecuenciaEjecucion as FrecuenciaEjecucion,cHoraEjecucion as HoraEjecucion,bMandaPorCorreo as MandaCorreo FROM SES_Trabajos_SQL (NOLOCK) WHERE (cSucursal='" + suc + "' OR cSucursal IS NULL) AND bActivo=1"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtTrabajos = New DataTable
            da.Fill(dtTrabajos)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub EjecutaTrabajo(cCodigoSQL As String, cParametros As String)
        Mensaje = ""
        Try
            DarValor()
            If cParametros.Trim = "" Then
                consulta = cCodigoSQL
            Else
                consulta = cCodigoSQL + " " + cParametros
            End If
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Connection = conexionSQL
            'Gilberto Madrid 02/10/2018 aumentamos el timeout a 60000
            comandosql.CommandTimeout = 60000
            comandosql.Transaction = transaccion
            comandosql.ExecuteNonQuery()
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub ActualizaFechaEjecucionTrabajo(cNombreTrabajo As String)
        Mensaje = ""
        Try
            DarValor()
            consulta = "UPDATE SES_Trabajos_SQL SET dFechaUltimaEjecucion=GETDATE() WHERE cNombreTrabajo='" + cNombreTrabajo + "'"
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Connection = conexionSQL
            comandosql.Transaction = transaccion
            If comandosql.ExecuteNonQuery() <= 0 Then
                Mensaje = "No se actualizaron los datos"
            End If
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub TraeCorreosParaEnvioInformacionTimbres()
        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT cValor FROM SES_ParametrosConfiguracion (NOLOCK) WHERE cParametroConfiguracion='CorreosEnviaTimbresSinUsar' AND bActivo=1"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtTimbresLibres = New DataTable
            da.Fill(dtTimbresLibres)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Sub fgRegresaConsultaDT(prmConsulta)

        Mensaje = ""
        Try
            DarValor()
            consulta = prmConsulta
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtaux = New DataTable
            da.Fill(dtaux)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub fgRegresaConsultaDTArchivosCopiar(prmConsulta)

        Mensaje = ""
        Try
            DarValor()
            consulta = prmConsulta
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtauxCopiaArchivos = New DataTable
            da.Fill(dtauxCopiaArchivos)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub fgTraeWebServices()

        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT * FROM SES_Trabajos_WS (NOLOCK) WHERE bActivo=1"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtWebServices = New DataTable
            da.Fill(dtWebServices)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub EjecutaWS(cWSSQL As String, cParametros As String)
        Mensaje = ""
        Try
            DarValor()
            consulta = cWSSQL.Trim + " " + cParametros.Trim
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtResultadoWS = New DataTable
            da.SelectCommand.CommandTimeout = 60000
            da.Fill(dtResultadoWS)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Sub

    Function flGuardaHistorialWS(prmNombreWS As String, prmFechaInicio As DateTime, prmFechaFin As DateTime, prmcDatosEnviados As String) As Boolean
        Mensaje = ""
        Try
            flGuardaHistorialWS = False
            DarValor()
            consulta = "INSERT INTO SES_Trabajos_WS_Historial (cNombreTrabajo,dFechaInicioEjecucion,dFechaFinEjecucion,cDatosEnviados) SELECT '" + prmNombreWS + "',@FechaInicio,@FechaFin,@json"
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Parameters.AddWithValue("@FechaInicio", prmFechaInicio)
            comandosql.Parameters.AddWithValue("@FechaFin", prmFechaFin)
            comandosql.Parameters.AddWithValue("@json", prmcDatosEnviados)
            comandosql.Connection = conexionSQL
            comandosql.Transaction = transaccion
            If comandosql.ExecuteNonQuery() <= 0 Then
                Mensaje = "No se guardaron los datos"
                flGuardaHistorialWS = False
            Else
                flGuardaHistorialWS = True
            End If
        Catch ex As Exception
            flGuardaHistorialWS = False
            Mensaje = ex.Message
        End Try
    End Function

    Sub plTraeSolicitudPendientesEnvio()

        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT * FROM OperacionesSupervisadasRemotasPorCorreo (NOLOCK) WHERE cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS' AND ISNULL(bSolicitudEnviada,0)=0"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtSolicitudAutorizacionCorreo = New DataTable
            da.Fill(dtSolicitudAutorizacionCorreo)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub plTraeSolicitudAtendidaPendienteEnvio()

        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT * FROM OperacionesSupervisadasRemotasPorCorreo (NOLOCK) WHERE cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS' AND ISNULL(bSolicitudEnviada,0)=1 AND bPrecioAutorizado IS NOT NULL AND bPrecioFinalModificacionAutorizado IS NOT NULL AND ISNULL(bSolicitudAtendida,0)=0"
            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtSolicitudAutorizacionCorreo = New DataTable
            da.Fill(dtSolicitudAutorizacionCorreo)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub plTraeSolicitudAutorizacionID(prmId As String)

        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT suc.CRFC as RFCSuc,UPPER(suc.CABREVIA) as Sucursal, " + vbCr
            consulta = consulta + " CONVERT(varchar(10),dFecRegSolicitud,103) as FechaSolicitud," + vbCr
            consulta = consulta + " cli.CCLIENTE + ' '+LTRIM(RTRIM(cli.CNOMBRE)) as Cliente,usuaut.USUNOMBRE as UsuarioAutoriza," + vbCr
            consulta = consulta + " ususol.USUNOMBRE as UsuarioSolicita," + vbCr
            consulta = consulta + " case LTRIM(RTRIM(op.cPrecioSolicitado)) when '' then 'NO' else LTRIM(RTRIM(op.cPrecioSolicitado)) end as SolicitudListaPrecio," + vbCr
            consulta = consulta + " case op.bPrecioFinalModificacionSolicitado when 0 then 'NO' else 'SI' end as SolicitudModificacionPrecio," + vbCr

            'Gilberto Madrid 08/12/2020 agregamos los nuevos campos para solicitud debajo del margen y costo
            consulta = consulta + " case ISNULL(op.bDebajoMargen,0) when 0 then 'NO' else 'SI' end as SolicitudDebajoMargen," + vbCr
            consulta = consulta + " case ISNULL(op.bDebajoCosto,0) when 0 then 'NO' else 'SI' end as SolicitudDebajoCosto," + vbCr
            consulta = consulta + " op.cDatosAdjuntos,op.cMensaje,suc.CSUCURSAL,mailsol.cEmail as MailSolicito,mailaut.cEmail as MailAutoriza," + vbCr
            'César Niebla 28/09/2022 Regresamos dentro del resultado el correo configurado para copia de las autorizaciones por correo a otro supervisor
            consulta = consulta + " (Select top 1 cValor From SES_ParametrosConfiguracion (Nolock) where cParametroConfiguracion='CorreoCopia_OperacionesSupervisadasRemotasPorCorreo' and bActivo=1) as MailSupervisorCopia" + vbCr
            consulta = consulta + " FROM OperacionesSupervisadasRemotasPorCorreo op(NOLOCK)" + vbCr
            consulta = consulta + " INNER JOIN CLIENTES cli(NOLOCK) ON cli.CCLIENTE=op.cCliente" + vbCr
            consulta = consulta + " INNER JOIN USUARIOS usuaut (NOLOCK) ON op.cUsuarioAutoriza=usuaut.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN USUARIOS ususol (NOLOCK) ON op.cUsuarioSolicitud=ususol.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN SUCURSALES suc (NOLOCK) ON suc.CSUCURSAL=op.cSucursal" + vbCr
            consulta = consulta + " INNER JOIN parametros_mail mailsol (NOLOCK) ON mailsol.USULOGIN=ususol.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN parametros_mail mailaut (NOLOCK) ON mailaut.USULOGIN=usuaut.USULOGIN" + vbCr
            consulta = consulta + " WHERE cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS'" + vbCr
            consulta = consulta + " AND ISNULL(bSolicitudEnviada,0)=0 AND nID=" + prmId

            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtSolicitudAutorizacionCorreoaux = New DataTable
            da.Fill(dtSolicitudAutorizacionCorreoaux)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub plActualizaSolicitudAutorizacionEnviada(prmId As String)

        Mensaje = ""
        Try
            DarValor()
            consulta = "UPDATE OperacionesSupervisadasRemotasPorCorreo SET bSolicitudEnviada=1 WHERE nID=" + prmId + " AND cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS'"
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Connection = conexionSQL
            comandosql.Transaction = transaccion
            If comandosql.ExecuteNonQuery() <= 0 Then
                Mensaje = "No se actualizaron los datos"
            End If
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub


    Function FlTraeDescripcionMotivoCancelacion(prmIdMotivoRechazo As String) As String
        FlTraeDescripcionMotivoCancelacion = ""
        Try
            DarValor()

            consulta = "SELECT dbo.RegresaMotivoRechazoSolicitudDescripcion('" + prmIdMotivoRechazo.Trim() + "') as cMotivoRechazoSolicitud"

            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtaux2 = New DataTable
            da.Fill(dtaux2)

            If dtaux2.Rows.Count = 0 Then
                FlTraeDescripcionMotivoCancelacion = ""
                Exit Function
            End If

            FlTraeDescripcionMotivoCancelacion = dtaux2.Rows(0)("cMotivoRechazoSolicitud")


        Catch ex As Exception
            Mensaje = ex.Message
        End Try
    End Function

    Sub plTraeSolicitudAutorizacionAtendidaID(prmId As String)

        Mensaje = ""
        Try
            DarValor()
            consulta = "SELECT suc.CRFC as RFCSuc,UPPER(suc.CABREVIA) as Sucursal, " + vbCr
            consulta = consulta + " CONVERT(varchar(10),dFecRegSolicitud,103) as FechaSolicitud," + vbCr
            consulta = consulta + " cli.CCLIENTE + ' '+LTRIM(RTRIM(cli.CNOMBRE)) as Cliente,usuaut.USUNOMBRE as UsuarioAutoriza," + vbCr
            consulta = consulta + " ususol.USUNOMBRE as UsuarioSolicita," + vbCr
            consulta = consulta + " case LTRIM(RTRIM(op.cPrecioSolicitado)) when '' then 'NO' else LTRIM(RTRIM(op.cPrecioSolicitado)) end as SolicitudListaPrecio," + vbCr
            consulta = consulta + " case op.bPrecioFinalModificacionSolicitado when 0 then 'NO' else 'SI' end as SolicitudModificacionPrecio," + vbCr
            consulta = consulta + " case ISNULL(op.bDebajoMargen,0) when 0 then 'NO' else 'SI' end as SolicitudDebajoMargen," + vbCr
            consulta = consulta + " case ISNULL(op.bDebajoCosto,0) when 0 then 'NO' else 'SI' end as SolicitudDebajoCosto," + vbCr
            consulta = consulta + " op.cDatosAdjuntos,op.cMensaje,suc.CSUCURSAL,mailsol.cEmail as MailSolicito,mailaut.cEmail as MailAutoriza" + vbCr
            consulta = consulta + " ,op.bPrecioAutorizado as PrecioAutorizado,op.bPrecioFinalModificacionAutorizado as ModificacionAutorizado" + vbCr

            'César Niebla 18/08/2021 Se agregó al SELECT el campo cMotivoRechazoSolicitud para conocer la causa del Rechazo
            consulta = consulta + " ,op.cMotivoRechazoSolicitud," + vbCr
            'César Niebla 28/09/2022 Regresamos dentro del resultado el correo configurado para copia de las autorizaciones por correo a otro supervisor
            consulta = consulta + " (Select top 1 cValor From SES_ParametrosConfiguracion (Nolock) where cParametroConfiguracion='CorreoCopia_OperacionesSupervisadasRemotasPorCorreo' and bActivo=1) as MailSupervisorCopia" + vbCr
            consulta = consulta + " FROM OperacionesSupervisadasRemotasPorCorreo op(NOLOCK)" + vbCr
            consulta = consulta + " INNER JOIN CLIENTES cli(NOLOCK) ON cli.CCLIENTE=op.cCliente" + vbCr
            consulta = consulta + " INNER JOIN USUARIOS usuaut (NOLOCK) ON op.cUsuarioAutoriza=usuaut.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN USUARIOS ususol (NOLOCK) ON op.cUsuarioSolicitud=ususol.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN SUCURSALES suc (NOLOCK) ON suc.CSUCURSAL=op.cSucursal" + vbCr
            consulta = consulta + " INNER JOIN parametros_mail mailsol (NOLOCK) ON mailsol.USULOGIN=ususol.USULOGIN" + vbCr
            consulta = consulta + " INNER JOIN parametros_mail mailaut (NOLOCK) ON mailaut.USULOGIN=usuaut.USULOGIN" + vbCr
            consulta = consulta + " WHERE op.cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS'" + vbCr
            consulta = consulta + " AND ISNULL(op.bSolicitudEnviada,0)=1 AND op.bPrecioAutorizado IS NOT NULL AND op.bPrecioFinalModificacionAutorizado IS NOT NULL AND op.nID=" + prmId

            da = New SqlDataAdapter(consulta, cadenaconexion)
            dtSolicitudAutorizacionCorreoaux = New DataTable
            da.Fill(dtSolicitudAutorizacionCorreoaux)
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

    Sub plActualizaSolicitudAutorizacionAtendida(prmId As String)

        Mensaje = ""
        Try
            DarValor()
            consulta = "UPDATE OperacionesSupervisadasRemotasPorCorreo SET bSolicitudAtendida=1 WHERE nID=" + prmId + " AND cOperacionSupervisada='Ventas: Permite autorizar cambios de precios y lista de precios a clientes para venta WS'"
            comandosql = New SqlCommand(consulta, conexionSQL)
            comandosql.Connection = conexionSQL
            comandosql.Transaction = transaccion
            If comandosql.ExecuteNonQuery() <= 0 Then
                Mensaje = "No se actualizaron los datos"
            End If
        Catch ex As Exception
            Mensaje = ex.Message
        End Try

    End Sub

End Class
