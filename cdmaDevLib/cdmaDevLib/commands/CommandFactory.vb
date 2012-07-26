Imports cdmaDevLib.NvItems.NVItems
Public Class CommandFactory

    Public Shared Function GetCommand(nv As NvItems.NVItems) As ICommand
        Return GetCommand(nv, False, New Byte() {})
    End Function
    Public Shared Function GetCommand(nv As NvItems.NVItems, write As Boolean, data() As Byte) As ICommand
        Dim cmd As ICommand
        Dim qc As Qcdm.Cmd = Qcdm.Cmd.DIAG_NV_READ_F
        If write Then
            qc = Qcdm.Cmd.DIAG_NV_WRITE_F
        End If
        Select Case nv
            Case NV_DIR_NUMBER_I
                cmd = New Cmd_NV_DIR_NUMBER_I(qc, _
                                                nv, _
                                                data, _
                                                qc.ToString() & " " & nv.ToString())

            Case NV_MIN1_I
                cmd = New Cmd_NV_MIN1_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())


            Case Else
                cmd = New Cmd_nv(qc, _
                                  nv, _
                                  data, _
                                  qc.ToString() & " " & nv.ToString())
        End Select

        Return cmd
    End Function
    Public Shared Function GetCommand(qc As Qcdm.Cmd) As ICommand
        Return New QcCommand(qc)
    End Function

End Class
