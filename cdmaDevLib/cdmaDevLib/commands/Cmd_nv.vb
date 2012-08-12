Public Class Cmd_nv
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub

    Public Overrides Sub decode()
        Try

            cdmaTerm.thePhoneRxd.NvItems(Me.currentNv) = New Nv(Me.currentNv, SecretDecoderRing.trimFrontAndEndAscii(SecretDecoderRing.getAsciiStrings(bytesRxd)))
            cdmaTerm.thePhone.NvItems = Nothing ''clear the ref in order to triger the prop changed
            cdmaTerm.thePhone.NvItems = cdmaTerm.thePhoneRxd.NvItems
        Catch ex As Exception
            logger.addToLog("decode err:" + ex.ToString)

        End Try
    End Sub

End Class
