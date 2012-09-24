Public Class Cmd_NV_NAM_LOCK_I
    Inherits Command

    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Overrides Sub decode()
        If bytesRxd(4) = 0 Then
            cdmaTerm.thePhone.NamLock = False
            cdmaTerm.thePhoneRxd.NamLock = False
        ElseIf bytesRxd(4) = 1 Then
            cdmaTerm.thePhone.NamLock = True
            cdmaTerm.thePhoneRxd.NamLock = True
        End If
    End Sub
End Class
