Public Class Cmd_NV_LOCK_CODE_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        Try

            ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim stringFromPacket As String = Me.bytesRxd.ToHexString()

            Dim DecodedString As String = ""


            DecodedString += stringFromPacket(7) + stringFromPacket(9) & _
            stringFromPacket(11) + stringFromPacket(13)


            cdmaTerm.thePhone.UserLock = DecodedString
            cdmaTerm.thePhoneRxd.UserLock = DecodedString
        Catch
            logger.add("decoder err: cant get decoded userlock")

        End Try
    End Sub
End Class
