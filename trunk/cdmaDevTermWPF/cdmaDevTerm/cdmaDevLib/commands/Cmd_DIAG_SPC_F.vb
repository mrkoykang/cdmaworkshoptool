Public Class Cmd_DIAG_SPC_F
    Inherits Command
    Sub New(ByVal qcdm As Qcdm.Cmd, ByVal qcData As Byte(), ByVal debuggingTextIn As String)
        MyBase.New(qcdm, qcData, debuggingTextIn)
    End Sub
    Public Overrides Sub decode()
        Try

            If Me.bytesRxd(1) <> 1 Then
                logger.add("Spc not accepted, don't send anything for 10 seconds (or devterm will crash)", logger.LogType.infoAndMsg)
                System.Threading.Thread.Sleep(1000)
            ElseIf Me.bytesRxd(1) = 1 And Me.bytesRxd(0) = &H41 Then
                logger.add("Spc Accepted", logger.LogType.infoAndMsg)
            End If
        Catch
            logger.add("No spc(diag_spc_f) found", logger.LogType.infoAndMsg)

        End Try
    End Sub
End Class
