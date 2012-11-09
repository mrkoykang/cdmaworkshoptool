Public Class Cmd_NV_SEC_CODE_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        ''test decode spc
        Try

            If Me.bytesRxd(0) = &H42 Then
                logger.add("Spc not found (send spc or 16 SP required?)")
                logger.add("Spc not found (send spc or 16 SP required?)", logger.logType.msg)
            Else


                '' Dim spcFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
                Dim spcFromPacket As String = cdmaTerm.biznytesToStrizings(Me.bytesRxd)



                Dim thisIsTheSPC As String = ""


                thisIsTheSPC += spcFromPacket(7) + spcFromPacket(9) & _
                spcFromPacket(11) + spcFromPacket(13) & _
                spcFromPacket(15) + spcFromPacket(17)

                'If thisIsTheSPC = "000000" Then
                '    logger.addToLog("cant find meid 1")

                'Else
                cdmaTerm.thePhone.Spc = thisIsTheSPC
                cdmaTerm.thePhoneRxd.Spc = thisIsTheSPC
                ''End If

            End If



        Catch
            logger.add("cant find spc_nv 1", logger.logType.msg)


        End Try
    End Sub
End Class
