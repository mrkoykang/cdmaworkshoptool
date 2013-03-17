'' '' CDMA DEV TERM
'' '' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
'' '' www.chromableedstudios.com
'' '' chromableedstudios ( a t ) gmail ( d o t ) com
'' ''     
'' '' cdmadevterm by ¿k? with help from ajh and jh
'' ''
'' '' this was originally developed as a test framework, before many 
'' '' things about qcdm(and programming) were understood by the author
'' '' please forgive some code that should never have seen the light of day ;)
'' ''
'' ''-------------------------------------------------------------------------------------------------------------
'' '' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
'' '' check out the GPL v3 for details
'' '' http://www.gnu.org/copyleft/gpl.html
'' ''-------------------------------------------------------------------------------------------------------------

''TODO: whole decoding strategy needs to be rethought

'Imports Microsoft.VisualBasic

Public Class SecretDecoderRing


    '    '' Sub decoder(ByVal decoderSelect As String)
    '    Function decoder(ByVal cmd As Command) As Boolean


    '        ''TODO WTF
    '        ''sometimes the old command is decoded? tried waiting on thread, still decoded old data
    '        '' ''try to wait so the decoder doesn't run b4 the data gets there
    '        ''System.Threading.Thread.Sleep(3000)


    '        ''TODO maybe add a boolean called is it ready?
    '        ''when the command is called set is it ready to false
    '        ''when the data received is done set is it ready true
    '        '' check at beginning of decoder and wait if not ready?
    '        Try
    '            Dim timeoutcounter As Int32 = 0


    '            While timeoutcounter <= 5
    '                If cdmaTerm.newCommandRxd = False Then
    '                    System.Threading.Thread.Sleep(150)
    '                End If


    '                timeoutcounter = timeoutcounter + 1
    '            End While

    '            Dim decoderSelect As String = cmd.decoderString
    '            If decoderSelect = "" Then
    '                ''do nothing
    '                logger.addToLog("damn decoder ring: sdr not defined")

    '            ElseIf decoderSelect = "ReadMeid_NV" Then
    '                decode_ReadMeid_NV(cmd)
    '            ElseIf decoderSelect = "ReadSPC_NV" Then
    '                decode_ReadSPC_NV(cmd)
    '            ElseIf decoderSelect = "ReadSPC_LG" Then
    '                decode_ReadSPC_LG(cmd)
    '            ElseIf decoderSelect = "ReadESN" Then
    '                decode_ReadESN(cmd)
    '            ElseIf decoderSelect = "ReadUserLock" Then
    '                decode_NV_LOCK_CODE_I(cmd)
    '            ElseIf decoderSelect = "ReadNam0MDN" Then
    '                decode_ReadNam0MDN(cmd)

    '            ElseIf decoderSelect = "ReadNam0Min_part1" Then
    '                ''decode_ReadNam0MDN(cmd)
    '                decode_NV_MIN1(cmd)

    '            ElseIf decoderSelect = "ReadNam0Min_part2" Then
    '                decode_NV_MIN2(cmd)

    '            Else
    '                ''do nothing?
    '                cdmaTerm.logAllBox.AppendText("damn decoder ring: thing fell apart?")
    '            End If
    '        Catch
    '            logger.addToLog("damn decoder ring: gen err")
    '            Return False
    '        Finally
    '            cdmaTerm.newCommandRxd = False
    '        End Try
    '        Return True
    '    End Function


    ''TODO: Add necesary fields to Command in order to call a nicer decoder
    '' Public Function decoder3(ByVal decoderSelect As Qcdm.Cmd, ByVal nvSelect As NvItems.NVItems) As Boolean
    Public Shared Function decoder3(ByVal cmd As Command) As Boolean
        Try
            Select Case cmd.currentQcdm


                Case Qcdm.Cmd.DIAG_SPC_F
                    ''Read ESN
                    decode_DIAG_SPC_F(cmd)

                Case Qcdm.Cmd.DIAG_NV_PEEK_F
                    ''Read Lg SPC?
                    ''TODO OTHER DECODER? THIS IS FOR LG SPC nv peek 0x17
                    decode_ReadSPC_LG(cmd)

                    ''todo:experimental needed for efs
                    'Case Qcdm.Cmd.DIAG_SUBSYS_CMD_F
                    '    ''Call Nv Write Decoder
                    '    decodeSubsysItem(cmd)

                Case Qcdm.Cmd.DIAG_NV_WRITE_F
                    ''Call Nv Write Decoder
                    decodeNvItem(cmd)

                Case Qcdm.Cmd.DIAG_NV_READ_F
                    ''Call Nv Read Decoder
                    decodeNvItem(cmd)

                    ''hm.. random memory leak rofl
                    '' should add test or clear if dooing nv read?
                    ''testing for nv read cdma ws style
                    cdmaTerm.nvReadQ.Add(cmd)
                Case Qcdm.Cmd.DIAG_PEEKB_F
                    ''  decode_DIAG_PEEKB_F(cmd)

                    If cdmaTerm.ReadingRamToFile Then

                        cdmaTerm.RamReadQ.Add(cmd)
                    End If


                Case Else
                    Return True

            End Select

        Catch ex As Exception
            logger.add("decoder err: " + ex.ToString)
            Return False

        End Try
        Return True
    End Function


    Private Shared Sub decodeNvItem(ByVal cmd As Command)
        Select Case cmd.currentNv
 
            Case 11055 ''item 0x2b2f 11055 data 0x7d4 2004 bb reg id
                decode_11055(cmd.bytesRxd)

        End Select

    End Sub

    '#Region "Decoders"

    '    'Sub decode_Generic()
    '    '    ''test decode generic
    '    '    Try

    '    '        Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
    '    '        Dim DecodedString As String = ""


    '    '        DecodedString += stringFromPacket(7) + stringFromPacket(9) & _
    '    '        stringFromPacket(11) + stringFromPacket(13) & _
    '    '        stringFromPacket(15) + stringFromPacket(17)



    '    '        'Else
    '    '        cdmaTerm.INSERTRETURNCOMMANDTEXTBOX.Text = DecodedString
    '    '        ''End If


    '    '    Catch
    '    '        logger.addToLog("cheap decoder ring: cant get generic decoded string")

    '    '    End Try

    '    'End Sub

    Shared Sub decode_ReadSPC_LG(ByVal cmd As Command)
        ''test decode spc
        Try

            Dim bytesIn As String = cmd.bytesRxd.ToHexString()

            Dim lgSpc As String = ""


            lgSpc += bytesIn(9) + bytesIn(11) & _
            bytesIn(13) + bytesIn(15) & _
            bytesIn(17) + bytesIn(19)

            cdmaTerm.thePhone.Spc = lgSpc
            cdmaTerm.thePhoneRxd.Spc = lgSpc

        Catch ex As Exception
            Logger.Add("er spc_lg 1 " + ex.ToString())

        End Try

    End Sub

    Private Shared MIN2Raw As String

    ''#End Region


    '    ''New untested EVDO decoders

    '    Private Sub decode_NV_PPP_USER_ID_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_PPP_USER_ID_I.Text = trimFrontAndEndAscii(DecodedString)

    '            ''set the display to the first username and pass
    '            cdmaTerm.evdo_usernameTextbox.Text = trimFrontAndEndAscii(DecodedString)


    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_PPP_USER_ID_I")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_PPP_PASSWORD_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_PPP_PASSWORD_I.Text = trimFrontAndEndAscii(DecodedString)

    '            ''set the display to the first username and pass
    '            cdmaTerm.evdo_passwordTextbox.Text = trimFrontAndEndAscii(DecodedString)

    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_PPP_PASSWORD_I")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_PAP_USER_ID_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_PAP_USER_ID_I.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_PAP_USER_ID_I")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_PAP_PASSWORD_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_PAP_PASSWORD_I.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_PAP_PASSWORD_I")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_HDR_AN_AUTH_USER_ID_LONG(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_HDR_AN_AUTH_USER_ID_LONG.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring:  decode_NV_HDR_AN_AUTH_USER_ID_LONG")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_HDR_AN_AUTH_PASSWD_LONG(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_HDR_AN_AUTH_PASSWD_LONG.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring:decode_NV_HDR_AN_AUTH_PASSWD_LONG")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_HDR_AN_AUTH_NAI_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_HDR_AN_AUTH_NAI_I.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_RSVD_ITEM_579_I")

    '        End Try
    '    End Sub

    '    Private Sub decode_NV_HDR_AN_AUTH_PASSWORD_I(ByVal bytesRxd As Byte())
    '        Try
    '            Dim DecodedString As String = getAsciiStrings(bytesRxd)
    '            cdmaTerm.txtBoxNV_HDR_AN_AUTH_PASSWORD_I.Text = trimFrontAndEndAscii(DecodedString)
    '        Catch
    '            logger.addToLog("damn decoder ring: decode_NV_RSVD_ITEM_580_I")

    '        End Try
    '    End Sub

    Public Shared Function trimFrontAndEndAscii(ByVal str As String) As String
        If (str.Length - 7) <= 0 Then
            Logger.Add("No ascii response to command l: " + str.Length.ToString)
            Return ""
        End If
        Return str.Substring(4, str.Length - 7)
    End Function

    Public Shared Function trimFrontAndEndAsciiSpecific(ByVal before As String, ByVal start As Integer, ByVal lengthMinus As Integer) As String
        Dim after As String = before
        Return after.Substring(start, before.Length - lengthMinus)
    End Function

    Public Shared Function getAsciiStrings(ByVal bytes As Byte()) As String
        Dim bString = bytes.ToHexString()
        Try
            Dim HexValue As String = ""
            For i = 0 To bString.Length - 1
                If (bString.Substring(i, 2) = "00") Then
                    ''skip null for now
                    i += 1
                Else
                    HexValue += bString.Substring(i, 2)
                    i += 1
                End If
            Next
            Dim StrValue As String = ""
            While HexValue.Length > 0
                ' Use ToChar() to convert each ASCII value (two hex digits) to the actual character
                ''TODO test u i nt32 u i nt64
                StrValue += System.Convert.ToChar(System.Convert.ToUInt64(HexValue.Substring(0, 2), 16)).ToString()
                HexValue = HexValue.Substring(2, HexValue.Length - 2)
            End While

            Return StrValue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Shared Sub decode_DIAG_SPC_F(ByVal cmd As Command)
        If cmd.bytesRxd(1) <> 1 Then
            Logger.Add("Spc not accepted, don't send anything for 10 seconds (or devterm will crash)")
            System.Threading.Thread.Sleep(1000)
        ElseIf cmd.bytesRxd(1) = 1 And cmd.bytesRxd(0) = &H41 Then
            Logger.Add("Spc Accepted")
        End If
    End Sub

    Private Shared Sub decode_11055(ByVal bytesRxd As Byte())
        ''TODO untested
        ''System.Convert.ToInt32((bytesRxd(4)+bytesRxd(3)), 16) needed?
        ''cdmaTerm.BbRegIdTextbox.Text = Integer.Parse(bytesRxd(4).ToString + bytesRxd(3).ToString).ToString
        ''cdmaTerm.BbRegIdTextbox.Text = System.Convert.ToInt32((bytesRxd(4).ToString + bytesRxd(3).ToString), 16).ToString
        cdmaTerm.thePhone.RegId = System.Convert.ToInt32((bytesRxd(4).ToString("x2") + bytesRxd(3).ToString("x2")), 16).ToString
        cdmaTerm.thePhoneRxd.RegId = System.Convert.ToInt32((bytesRxd(4).ToString("x2") + bytesRxd(3).ToString("x2")), 16).ToString
    End Sub



    '    Private Sub decode_DIAG_PEEKB_F(ByVal cmd As Command)
    '        ''unused.. could add test to see if read is bad
    '        If cdmaTerm.ReadingRamToFile Then
    '            '' cdmaTerm.Q.SaveTextToFile()
    '        End If

    '    End Sub




    '    Private Sub decode_NV_DS_MIP_ENABLE_PROF_I(ByVal bytesRxd As Byte())
    '        cdmaTerm.SelectedProfileCombo.Text = bytesRxd(3).ToString
    '    End Sub

    '    Private Sub decodeSubsysItem(ByVal cmd As Command)
    '        ''wtfthisistheworstmethodever
    '        If cmd.bytesToTx(2) = Qcdm.SubsysStorage.DIAG_EFS2_READDIR Then

    '            Dim checkByte As Integer = cmd.bytesRxd.Length() - 5

    '            'If (cmd.bytesRxd(checkByte) = 0) Then
    '            '    cdmaTerm.EfsQc.LastEfsWorked = False
    '            'Else
    '            '    cdmaTerm.EfsQc.LastEfsWorked = True
    '            'End If
    '            ''  Dim strM As String = "Keep Reading Folders?" + cdmaTerm.biznytesToStrizings(cmd.bytesRxd)
    '            '' Dim continueEfs As Boolean = logger.addToLog(strM, "Keep Reading?", MessageBoxButtons.OKCancel)
    '            ''Dim continueEfs As Boolean

    '            'If logger.addToLog(strM, "Keep Reading?", MessageBoxButtons.OKCancel, _
    '            '  Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
    '            '    continueEfs = True

    '            'Else
    '            '    continueEfs = False
    '            'End If


    '            Dim step1 As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)
    '            step1 = step1.Substring(80, step1.Length - 86)
    '            Dim cleanedUp As String = getAsciiStrings(cdmaTerm.String_To_Bytes(step1))

    '            If (cleanedUp.Length > 1) And cmd.bytesRxd(16) = 1 Then

    '                addToEfsTreeView(cleanedUp, "folder")


    '                'If cmd.bytesRxd.Length > 40 Then
    '                '    '' Dim cleanedUp As String = trimFrontAndEndAsciiSpecific(getAsciiStrings(cmd.bytesRxd), 39, 4)


    '                '    ''Dim cleanedUp As String = getAsciiStrings(cdmaTerm.String_To_Bytes(cdmaTerm.biznytesToStrizings(cmd.bytesRxd).Substring(40, (cmd.bytesRxd.Length - 40))))


    '                '    step1 += "!"
    '                '    '' Array.Copy(cmd.bytesRxd, 39, cleanedUp, 0, (cmd.bytesRxd.Length - 43))

    '                '    ''  cdmaTerm.TreeView2.Nodes.Add(cleanedUp)
    '                'End If


    '                cdmaTerm.EfsQc.LastEfsWorked2 = True

    '                ''TODO: check for length begin index
    '            ElseIf (cleanedUp.Length > 1) Then


    '                cdmaTerm.EfsQc.LastEfsWorked2 = True

    '                addToEfsTreeView(cleanedUp, "file")
    '            Else
    '                ''uh..?
    '                cdmaTerm.EfsQc.LastEfsWorked2 = False

    '            End If

    '        End If

    '        If (cmd.bytesToTx(2) = Qcdm.SubsysStorage.DIAG_EFS2_READDIR) And (cmd.bytesRxd.Length < 40) Then
    '            ''TODO EFS STOP FIX
    '            cdmaTerm.EfsQc.LastEfsWorked2 = False


    '        End If


    '    End Sub

    '    Public Function addToEfsTreeView(ByVal f As String, ByVal efsType As String) As Boolean

    '        If efsType = "folder" Then

    '            cdmaTerm.FolderTreeView1.Nodes.Add(f)
    '        Else
    '            cdmaTerm.FileTreeView2.Nodes.Add(f)

    '            Return True
    '        End If

    '        Return False
    '    End Function


End Class
