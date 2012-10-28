Public Class Cmd_DIAG_SPC_F
    Inherits Command
    Sub New(ByVal qcdm As Qcdm.Cmd, ByVal qcData As Byte(), ByVal debuggingTextIn As String)
        MyBase.New(qcdm, qcData, debuggingTextIn)
    End Sub
    Public Overrides Sub decode()
        Try

            If Me.bytesRxd(1) <> 1 Then
                logger.addToLog("Spc not accepted, don't send anything for 10 seconds (or devterm will crash)", logger.logType.infoAndMsg)
                System.Threading.Thread.Sleep(1000)
            ElseIf Me.bytesRxd(1) = 1 And Me.bytesRxd(0) = &H41 Then
                logger.addToLog("Spc Accepted", logger.logType.infoAndMsg)
            End If
        Catch
            logger.addToLog("No spc(diag_spc_f) found", logger.logType.infoAndMsg)

        End Try
    End Sub
End Class
