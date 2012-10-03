Public Class Cmd_DIAG_VERNO_F
    Inherits Command
    Sub New(ByVal qcdm As Qcdm.Cmd, ByVal qcData As Byte(), ByVal debuggingTextIn As String)
        MyBase.New(qcdm, qcData, debuggingTextIn)
    End Sub
    Public Overrides Sub decode()
        Try

        Catch
            logger.addToLog("decoder err: cant get diag verno f", logger.logType.err)

        End Try
    End Sub
End Class
