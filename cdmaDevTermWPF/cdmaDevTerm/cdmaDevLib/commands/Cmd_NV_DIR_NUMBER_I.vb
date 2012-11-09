Public Class Cmd_NV_DIR_NUMBER_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub

    Overrides Sub decode()
        Try
            Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(Me.bytesRxd)

            Dim DecodedString As String = ""

            DecodedString += stringFromPacket(9) + stringFromPacket(11) & _
            stringFromPacket(13) + stringFromPacket(15) & _
            stringFromPacket(17) + stringFromPacket(19) & _
            stringFromPacket(21) + stringFromPacket(23) & _
            stringFromPacket(25) + stringFromPacket(27)

            cdmaTerm.thePhone.Mdn() = DecodedString
            cdmaTerm.thePhoneRxd.Mdn() = DecodedString


        Catch
            logger.add("decoder err: cant get decoded mdn")
        End Try
    End Sub

End Class
