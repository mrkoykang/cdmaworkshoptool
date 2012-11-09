Public Class Cmd_NV_MEID_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Overrides Sub decode()
        Try

            Dim meidFromPacket As String = cdmaTerm.biznytesToStrizings(Me.bytesRxd)

            ''todo uh. this doesnt seem to work? wtf?
            If meidFromPacket.StartsWith("4") Then
                logger.add("meid not returned, try sending 16 Digit SP / unlock code")
            End If

            Dim thisIsTheMeid As String = ""


            thisIsTheMeid += meidFromPacket(18) + meidFromPacket(19) & _
            meidFromPacket(16) + meidFromPacket(17) & _
            meidFromPacket(14) + meidFromPacket(15) & _
            meidFromPacket(12) + meidFromPacket(13) & _
            meidFromPacket(10) + meidFromPacket(11) & _
            meidFromPacket(8) + meidFromPacket(9) & _
            meidFromPacket(6) + meidFromPacket(7)

            If thisIsTheMeid = "00000000000000" Then
                logger.add("cant find meid 1")

            Else
                cdmaTerm.thePhone.Meid = thisIsTheMeid
                cdmaTerm.thePhoneRxd.Meid = thisIsTheMeid
            End If


        Catch
            logger.add("cant find meid 2")
        Finally
            ''todo: esn convert here?

        End Try
    End Sub
End Class
