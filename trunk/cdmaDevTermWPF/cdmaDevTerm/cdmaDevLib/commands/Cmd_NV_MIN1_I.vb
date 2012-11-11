Public Class Cmd_NV_MIN1_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        Try
            cdmaTerm.MIN1Raw = cdmaTerm.biznytesToStrizings(New Byte() {Me.bytesRxd(11), Me.bytesRxd(10), Me.bytesRxd(9), Me.bytesRxd(8)})
        Catch ex As Exception
            logger.add("Min1 err: " + ex.ToString)
        End Try
    End Sub
End Class
