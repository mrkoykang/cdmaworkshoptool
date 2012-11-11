Public Class Cmd_NV_DS_MIP_NUM_PROF_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        Try
            cdmaTerm.thePhone.NumMipProfiles = bytesRxd(3).ToString
            cdmaTerm.thePhoneRxd.NumMipProfiles = bytesRxd(3).ToString
        Catch ex As Exception
            logger.add("Cmd_NV_DS_MIP_NUM_PROF_I err: " + ex.ToString)
        End Try
    End Sub
End Class
