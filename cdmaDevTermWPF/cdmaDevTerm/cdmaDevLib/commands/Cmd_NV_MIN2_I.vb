Public Class Cmd_NV_MIN2_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Overrides Sub decode()
        Try
            cdmaTerm.MIN2Raw = (New Byte() {Me.bytesRxd(7), Me.bytesRxd(6)}).ToHexString()
        Catch ex As Exception
            logger.add("Min2 err: " + ex.ToString)
        End Try
    End Sub
End Class
