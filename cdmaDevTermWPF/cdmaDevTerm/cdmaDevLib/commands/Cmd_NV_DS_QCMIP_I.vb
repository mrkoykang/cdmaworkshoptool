Public Class Cmd_NV_DS_QCMIP_I
    Inherits Command

    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub

    Overrides Sub decode()
        ''read evdo type
        ''00 simple
        ''01 mob+simple
        ''02 mobile
        Dim type As Integer = bytesRxd(3)
        cdmaTerm.thePhone.Qcmip = type
        cdmaTerm.thePhoneRxd.Qcmip = type
    End Sub

End Class
