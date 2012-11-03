' '' CDMA DEV TERM
' '' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
' '' www.chromableedstudios.com
' '' chromableedstudios ( a t ) gmail ( d o t ) com
' ''     
' '' cdmadevterm by ¿k? with help from ajh and jh
' ''
' '' this was originally developed as a test framework, before many 
' '' things about qcdm(and programming) were understood by the author
' '' please forgive some code that should never have seen the light of day ;)
' ''
' ''-------------------------------------------------------------------------------------------------------------
' '' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
' '' check out the GPL v3 for details
' '' http://www.gnu.org/copyleft/gpl.html
' ''-------------------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Array
Imports System.IO
Imports cdmaDevLib.Qcdm.Cmd

Public Class Prl

    Public PRLPacketSize As Integer = 120

    Public Function UploadPrl(ByVal prlData As Byte()) As Boolean
        Dim request As Byte() = New Byte(125) {}
        request(0) = DIAG_PR_LIST_WR_F

        Dim frameCount As Integer = Convert.ToInt32(prlData.Length) / PRLPacketSize
        If frameCount * PRLPacketSize < prlData.Length Then
            frameCount += 1
        End If

        For i As Integer = 1 To frameCount
            request(1) = Convert.ToByte(i - 1)
            ''test for lost last packet?
            request(2) = Convert.ToByte(If((i <> frameCount), 1, 0))

            request(3) = 0

            Dim readCount As Integer = PRLPacketSize

            If i = frameCount Then
                readCount = Convert.ToInt32((prlData.Length - ((i - 1) * PRLPacketSize)))
            End If

            Dim offset As Integer = ((i - 1) * PRLPacketSize)

            Buffer.BlockCopy(prlData, offset, request, 6, readCount)

            request(4) = Convert.ToByte(readCount << 3 And &HFF)
            request(5) = Convert.ToByte(readCount >> 5 And &HFF)

            Dim prlDebug As String = "prl write packet " + i.ToString

            Dim prlBytesToTx As Byte() = cdmaTerm.myD.GetBufferWithCRC(request)

            cdmaTerm.dispatchQ.add(New Command(prlBytesToTx, prlDebug))

        Next
        Return True

    End Function

    Public Function DownloadPrl(ByVal filename As String) As Boolean
        ''OUT
        ''typedef struct
        ''{
        ''    byte cmd_code;                      /* Command code */
        ''    byte seq_num;                       /* Sequence number */
        ''    byte nam;                           /* PR_LIST for this NAM */
        ''} ''diag_pr_list_rd_req_type;

        ''IN
        ''DIAG_RL_RD_OK         0     /* No error */
        ''DIAG_RL_RD_NV_ERR     1     /* NV error */

        ''#define DIAG_PR_LIST_BLOCK_SIZE 120   /* Size in bytes of the PR_LIST block */
        ''typedef struct
        ''{
        ''    byte cmd_code;                               /* Command code */
        ''    byte rl_status;                              /* Status of block, as above */
        ''    nv_stat_enum_type nv_stat;                   /* Status returned by NV */
        ''    byte seq_num;                                /* Sequence number */
        ''    byte more;                                   /* More to come? */
        ''    WORD num_bits;                               /* Number of valid data bits */
        ''    byte pr_list_data[DIAG_PR_LIST_BLOCK_SIZE];  /* The block of PR_LIST */
        ''} diag_pr_list_rd_rsp_type;
        Dim prlData As New List(Of Byte)
        Dim request As Byte() = New Byte(125) {}
        request(0) = DIAG_PR_LIST_RD_F

        Dim frameCount As Integer = 0
      
        For i As Integer = 1 To 40 ''todo: actually check byte
            Dim cmd As Command = CommandFactory.GetCommand(DIAG_PR_LIST_RD_F, New Byte() {frameCount, 1})

            cdmaTerm.dispatchQ.add(cmd) ''todo: what is the actual packet
            cdmaTerm.dispatchQ.executeCommandQ()
            If (cmd.bytesRxd IsNot Nothing) Then

                If (cmd.bytesRxd(0) = 0) Then
                    Exit For
                End If

                Dim b = cmd.bytesRxdLessHdlc()
                prlData.AddRange(b)
            End If
            frameCount += 1
        Next

        System.IO.File.WriteAllBytes(filename, prlData.ToArray)
        Return True

    End Function


    Sub UploadPRL(ByVal PrlFile As String)
        UploadPRL(ReadPrlFile(PrlFile))
    End Sub

    Private Function ReadPrlFile(ByVal filename As String) As Byte()
        Dim input As New FileStream(filename, FileMode.Open)
        Dim bytes(CInt(input.Length - 1)) As Byte
        input.Read(bytes, 0, CInt(input.Length))
        input.Close()
        Return bytes
    End Function

End Class
