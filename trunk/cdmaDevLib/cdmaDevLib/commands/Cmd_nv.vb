Public Class Cmd_nv
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NVItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub

    Public Overrides Sub decode()
        Try

            cdmaTerm.thePhone.NvData.Add(New Nv(Me.currentNv, SecretDecoderRing.trimFrontAndEndAscii(SecretDecoderRing.getAsciiStrings(bytesRxd))))

        Catch ex As Exception
            Throw New Exception("decode err:" + ex.ToString)

        End Try
    End Sub

End Class
