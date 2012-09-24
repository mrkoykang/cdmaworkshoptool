Public Class Cmd_NV_HOME_SID_NID_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        cdmaTerm.thePhone.Sid = System.Convert.ToInt32((Me.bytesRxd(5).ToString("x2") + Me.bytesRxd(4).ToString("x2")), 16).ToString
        cdmaTerm.thePhone.Nid = System.Convert.ToInt32((Me.bytesRxd(7).ToString("x2") + Me.bytesRxd(6).ToString("x2")), 16).ToString
        cdmaTerm.thePhoneRxd.Sid = System.Convert.ToInt32((Me.bytesRxd(5).ToString("x2") + Me.bytesRxd(4).ToString("x2")), 16).ToString
        cdmaTerm.thePhoneRxd.Nid = System.Convert.ToInt32((Me.bytesRxd(7).ToString("x2") + Me.bytesRxd(6).ToString("x2")), 16).ToString

    End Sub
End Class
