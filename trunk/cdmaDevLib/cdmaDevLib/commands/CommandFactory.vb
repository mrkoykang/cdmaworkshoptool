Imports cdmaDevLib.NvItems.NVItems
Imports cdmaDevLib.Qcdm.Cmd
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
            Case NV_MIN2_I
                cmd = New Cmd_NV_MIN2_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_MEID_I
                cmd = New Cmd_NV_MEID_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_HOME_SID_NID_I
                cmd = New Cmd_NV_HOME_SID_NID_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_LOCK_CODE_I
                cmd = New Cmd_NV_LOCK_CODE_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_SEC_CODE_I
                cmd = New Cmd_NV_SEC_CODE_I(qc, _
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
        Dim cmd As ICommand

        Select Case qc
            Case DIAG_ESN_F
                cmd = New Cmd_DIAG_ESN_F(qc, New Byte() {}, qc.ToString)
            Case False

            Case Else
                cmd = New QcCommand(qc)
        End Select

        Return cmd ''New QcCommand(qc)
    End Function

    Shared Function GetCommand(qc As Qcdm.Cmd, data As Byte()) As ICommand
        Dim cmd As ICommand
        cmd = New Command(qc, data, qc.ToString)
        Return cmd
    End Function
    ''TODO: untested entirely
    Shared Function GetCommand(str As String) As ICommand
        Dim cmd As ICommand
        Dim bytes As Byte() = cdmaDevLib.cdmaTerm.String_To_Bytes(str)

        Try
            Dim qc As Qcdm.Cmd = CType(bytes(0), Qcdm.Cmd)
            If qc = DIAG_NV_READ_F Then

                Dim numS As String = bytes(2).ToString("X2") + bytes(1).ToString("X2")
                Dim num = Integer.Parse(numS)
                Dim nv As Qcdm.Cmd = CType(num, NvItems.NVItems)
                cmd = CommandFactory.GetCommand(nv)
            ElseIf qc = DIAG_NV_WRITE_F Then
                Dim numS As String = bytes(2).ToString("X2") + bytes(1).ToString("X2")
                Dim num = Integer.Parse(numS)
                Dim nv As Qcdm.Cmd = CType(num, NvItems.NVItems)
                cmd = CommandFactory.GetCommand(nv, True, cdmaTerm.String_To_Bytes(str.Substring(6)))
            Else
                cmd = New Command(cdmaTerm.String_To_Bytes(str.Replace(" ", String.Empty)), "Raw bytes")
            End If

        Catch ex As Exception
            cmd = New Command(cdmaTerm.String_To_Bytes(str.Replace(" ", String.Empty)), "Raw bytes")

        End Try

        Return cmd
    End Function
End Class
