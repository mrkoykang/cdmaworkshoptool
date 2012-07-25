Public Class Cmd_NV_MIN2_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Overrides Sub decode()
        cdmaTerm.MIN2Raw = cdmaTerm.biznytesToStrizings(New Byte() {Me.bytesRxd(7), Me.bytesRxd(6)})
    End Sub
End Class
