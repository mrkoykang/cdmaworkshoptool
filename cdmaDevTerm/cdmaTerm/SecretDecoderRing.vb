﻿' '' CDMA DEV TERM
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

Imports Microsoft.VisualBasic

Public Class SecretDecoderRing


    '' Sub decoder(ByVal decoderSelect As String)
    Function decoder(ByVal cmd As Command) As Boolean


        ''TODO WTF
        ''sometimes the old command is decoded? tried waiting on thread, still decoded old data
        '' ''try to wait so the decoder doesn't run b4 the data gets there
        ''System.Threading.Thread.Sleep(3000)


        ''TODO maybe add a boolean called is it ready?
        ''when the command is called set is it ready to false
        ''when the data received is done set is it ready true
        '' check at beginning of decoder and wait if not ready?
        Try
            Dim timeoutcounter As Int32 = 0


            While timeoutcounter <= 5
                If cdmaTerm.newCommandRxd = False Then
                    System.Threading.Thread.Sleep(150)
                End If


                timeoutcounter = timeoutcounter + 1
            End While

            Dim decoderSelect As String = cmd.decoderString
            If decoderSelect = "" Then
                ''do nothing
                MessageBox.Show("damn decoder ring: sdr not defined")

            ElseIf decoderSelect = "ReadMeid_NV" Then
                decode_ReadMeid_NV(cmd)
            ElseIf decoderSelect = "ReadSPC_NV" Then
                decode_ReadSPC_NV(cmd)
            ElseIf decoderSelect = "ReadSPC_LG" Then
                decode_ReadSPC_LG(cmd)
            ElseIf decoderSelect = "ReadESN" Then
                decode_ReadESN(cmd)
            ElseIf decoderSelect = "ReadUserLock" Then
                decode_NV_LOCK_CODE_I(cmd)
            ElseIf decoderSelect = "ReadNam0MDN" Then
                decode_ReadNam0MDN(cmd)

            ElseIf decoderSelect = "ReadNam0Min_part1" Then
                ''decode_ReadNam0MDN(cmd)
                decode_NV_MIN1(cmd)

            ElseIf decoderSelect = "ReadNam0Min_part2" Then
                decode_NV_MIN2(cmd)

            Else
                ''do nothing?
                cdmaTerm.logAllBox.AppendText("damn decoder ring: thing fell apart?")
            End If
        Catch
            MessageBox.Show("damn decoder ring: gen err")
            Return False
        Finally
            cdmaTerm.newCommandRxd = False
        End Try
        Return True
    End Function


    ''TODO: Add necesary fields to Command in order to call a nicer decoder
    '' Public Function decoder3(ByVal decoderSelect As Qcdm.Cmd, ByVal nvSelect As NvItems.NVItems) As Boolean
    Public Function decoder3(ByVal cmd As Command) As Boolean
        Try
            Select Case cmd.currentQcdm

                Case Qcdm.Cmd.DIAG_ESN_F
                    ''Read ESN
                    decode_ReadESN(cmd)
                Case Qcdm.Cmd.DIAG_SPC_F
                    ''Read ESN
                    decode_DIAG_SPC_F(cmd)

                Case Qcdm.Cmd.DIAG_NV_PEEK_F
                    ''Read Lg SPC?
                    ''TODO OTHER DECODER? THIS IS FOR LG SPC nv peek 0x17
                    decode_ReadSPC_LG(cmd)

                Case Qcdm.Cmd.DIAG_SUBSYS_CMD_F
                    ''Call Nv Write Decoder
                    decodeSubsysItem(cmd)

                Case Qcdm.Cmd.DIAG_NV_WRITE_F
                    ''Call Nv Write Decoder
                    decodeNvItem(cmd)

                Case Qcdm.Cmd.DIAG_NV_READ_F
                    ''Call Nv Read Decoder
                    decodeNvItem(cmd)

                    ''hm.. random memory leak rofl
                    '' should add test or clear if dooing nv read?
                    ''testing for nv read cdma ws style
                    cdmaTerm.nvReadQ.addCommandToQ(cmd)
                Case Qcdm.Cmd.DIAG_PEEKB_F
                    ''  decode_DIAG_PEEKB_F(cmd)

                    If cdmaTerm.ReadingRamToFile Then

                        cdmaTerm.RamReadQ.addCommandToQ(cmd)
                    End If


                Case Else
                    Return True

            End Select

        Catch ex As Exception
            MessageBox.Show("damn decoder ring3: gen err: " + ex.ToString)
            Return False

        End Try
        Return True
    End Function


    Private Sub decodeNvItem(ByVal cmd As Command)
        Select Case cmd.currentNv
            Case NvItems.NVItems.NV_MEID_I
                ''Read MEID
                decode_ReadMeid_NV(cmd)
            Case NvItems.NVItems.NV_SEC_CODE_I
                ''Read SPC Defualt NV
                decode_ReadSPC_NV(cmd)
            Case NvItems.NVItems.NV_LOCK_CODE_I
                ''Read User Lock
                decode_NV_LOCK_CODE_I(cmd)
            Case NvItems.NVItems.NV_DIR_NUMBER_I
                ''Read MDN
                decode_ReadNam0MDN(cmd)
            Case NvItems.NVItems.NV_NAM_LOCK_I
                ''Read nam lock
                decode_NV_NAM_LOCK_I(cmd.bytesRxd)

            Case NvItems.NVItems.NV_MIN2_I
                ''Read min2
                decode_NV_MIN2(cmd)
            Case NvItems.NVItems.NV_MIN1_I
                ''Read min1
                decode_NV_MIN1(cmd)
            Case NvItems.NVItems.NV_PPP_USER_ID_I ''910 EVDO_P1
                decode_NV_PPP_USER_ID_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_PPP_PASSWORD_I ''906 EVDO_U1
                decode_NV_PPP_PASSWORD_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_PAP_USER_ID_I ''318 EVDO_P2
                decode_NV_PAP_USER_ID_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_PAP_PASSWORD_I ''319 EVDO_U2
                decode_NV_PAP_PASSWORD_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_HDR_AN_AUTH_USER_ID_LONG_I ''1194 EVDO_P3
                decode_NV_HDR_AN_AUTH_USER_ID_LONG(cmd.bytesRxd)
            Case NvItems.NVItems.NV_HDR_AN_AUTH_PASSWORD_LONG_I ''1192 EVDO_U3
                decode_NV_HDR_AN_AUTH_PASSWD_LONG(cmd.bytesRxd)
            Case NvItems.NVItems.NV_HDR_AN_AUTH_NAI_I ''579 EVDO_P4
                decode_NV_HDR_AN_AUTH_NAI_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_HDR_AN_AUTH_PASSWORD_I ''580 EVDO_U4
                decode_NV_HDR_AN_AUTH_PASSWORD_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_DS_QCMIP_I ''0x01cb evdo mode
                decode_NV_DS_QCMIP_I(cmd.bytesRxd)
            Case 11055 ''item 0x2b2f 11055 data 0x7d4 2004 bb reg id
                decode_11055(cmd.bytesRxd)
            Case NvItems.NVItems.NV_HOME_SID_NID_I
                decode_NV_HOME_SID_NID_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_DS_MIP_NUM_PROF_I ''number of profiles
                decode_NV_DS_MIP_NUM_PROF_I(cmd.bytesRxd)
            Case NvItems.NVItems.NV_DS_MIP_ENABLE_PROF_I ''enabled profile
                decode_NV_DS_MIP_ENABLE_PROF_I(cmd.bytesRxd)


        End Select

    End Sub


#Region "Decoders"

    'Sub decode_Generic()
    '    ''test decode generic
    '    Try

    '        Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
    '        Dim thisIsTheDecodedString As String = ""


    '        thisIsTheDecodedString += stringFromPacket(7) + stringFromPacket(9) & _
    '        stringFromPacket(11) + stringFromPacket(13) & _
    '        stringFromPacket(15) + stringFromPacket(17)



    '        'Else
    '        cdmaTerm.INSERTRETURNCOMMANDTEXTBOX.Text = thisIsTheDecodedString
    '        ''End If


    '    Catch
    '        MessageBox.Show("cheap decoder ring: cant get generic decoded string")

    '    End Try

    'End Sub

    Sub decode_ReadMeid_NV(ByVal cmd As Command)


        ''test decode meid
        Try

            '' Dim meidFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim meidFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)


            ''todo uh. this doesnt seem to work? wtf?
            If meidFromPacket.StartsWith("4") Then
                MessageBox.Show("meid not returned, try sending 16 Digit SP / unlock code")
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
                MessageBox.Show("cant find meid 1")

            Else
                'MessageBox.Show("meid found")
                cdmaTerm.meidExtractedFromPacketTxtbox.Text = thisIsTheMeid
            End If


        Catch
            MessageBox.Show("cant find meid 2")
        Finally
            cdmaTerm.ResultsListBox.Items.Clear()
            Dim c As New esnConverter
            c.OnMeidConversionSub(cdmaTerm.meidExtractedFromPacketTxtbox.Text.ToString)

        End Try

    End Sub
    Sub decode_ReadSPC_LG(ByVal cmd As Command)
        ''test decode spc
        Try


            ''Dim spcFromPacket As String = cdmaTerm.AtReturnCmdBox.Text

            Dim spcFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

            Dim thisIsTheSPC As String = ""


            thisIsTheSPC += spcFromPacket(9) + spcFromPacket(11) & _
            spcFromPacket(13) + spcFromPacket(15) & _
            spcFromPacket(17) + spcFromPacket(19)

            'If thisIsTheSPC = "000000" Then
            '    MessageBox.Show("cant find meid 1")

            'Else
            cdmaTerm.SPCTextbox.Text = thisIsTheSPC
            ''End If


        Catch
            MessageBox.Show("cant find spc_lg 1")

        End Try

    End Sub
    Sub decode_ReadSPC_NV(ByVal cmd As Command)
        ''test decode spc
        Try

            If cmd.bytesRxd(0) = &H42 Then
                MessageBox.Show("Spc not found (send spc or 16 SP required?)")

            Else


                '' Dim spcFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
                Dim spcFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)



                Dim thisIsTheSPC As String = ""


                thisIsTheSPC += spcFromPacket(7) + spcFromPacket(9) & _
                spcFromPacket(11) + spcFromPacket(13) & _
                spcFromPacket(15) + spcFromPacket(17)

                'If thisIsTheSPC = "000000" Then
                '    MessageBox.Show("cant find meid 1")

                'Else
                cdmaTerm.SPCTextbox.Text = thisIsTheSPC
                ''End If

            End If



        Catch
            MessageBox.Show("cant find spc_nv 1")

        End Try

    End Sub
    Sub decode_ReadESN(ByVal cmd As Command)
        ''test decode esn
        Try

            ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

            Dim thisIsTheDecodedString As String = ""


            thisIsTheDecodedString += stringFromPacket(8) + stringFromPacket(9) & _
            stringFromPacket(6) + stringFromPacket(7) & _
            stringFromPacket(4) + stringFromPacket(5) & _
            stringFromPacket(2) + stringFromPacket(3)



            cdmaTerm.readESNTextBox.Text = thisIsTheDecodedString



        Catch
            MessageBox.Show("damn decoder ring: cant get decoded esn")

        Finally
            cdmaTerm.ResultsListBox.Items.Clear()
            Dim c As New esnConverter
            c.OnMeidConversionSub(cdmaTerm.readESNTextBox.Text.ToString)

        End Try
    End Sub


    Sub decode_NV_LOCK_CODE_I(ByVal cmd As Command)
        ''test decode userlock

        Try

            ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

            Dim thisIsTheDecodedString As String = ""


            thisIsTheDecodedString += stringFromPacket(7) + stringFromPacket(9) & _
            stringFromPacket(11) + stringFromPacket(13)



            cdmaTerm.readUserLockTextbox.Text = thisIsTheDecodedString



        Catch
            MessageBox.Show("damn decoder ring: cant get decoded userlock")

        End Try

    End Sub
    Sub decode_ReadNam0MIN_Part2(ByVal cmd As Command)
        ''test decode mdn
        Try

            ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

            Dim thisIsTheDecodedString As String = ""


            thisIsTheDecodedString += stringFromPacket(9) + stringFromPacket(11) & _
            stringFromPacket(13) + stringFromPacket(15) & _
            stringFromPacket(17) + stringFromPacket(19) & _
            stringFromPacket(21) + stringFromPacket(23) & _
            stringFromPacket(25) + stringFromPacket(27)

            Dim buildFullMin As String = cdmaTerm.MIN2RawTxtbox.Text + thisIsTheDecodedString
            cdmaTerm.MIN2RawTxtbox.Text = buildFullMin



        Catch
            MessageBox.Show("damn decoder ring: cant get decoded min2")

        End Try

    End Sub
    Sub decode_ReadNam0MIN_part1(ByVal cmd As Command)
        ''test decode mdn
        '' Try

        ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
        Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

        Dim thisIsTheDecodedString As String = ""


        'thisIsTheDecodedString += stringFromPacket(9) + stringFromPacket(11) & _
        'stringFromPacket(13)


        'cdmaTerm.nam0MINTextbox.Text = thisIsTheDecodedString

        Dim min1 As Int32 = &HF9EBE7
        '' Both values min1 and min2 have to be read out
        Dim min2 As Int32 = &H3E7

        min2 = (min2 + 1) Mod (10) + ((((min2 Mod (100) / 10) + 1) Mod (10)) * 10) + ((((min2 / 100) + 1) Mod (10)) * 100)

        Dim min1a As Int64 = (min1 And &HFFC000) >> 14

        min1a = (min1a + 1) Mod (10) + ((((min1a Mod (100) / 10) + 1) Mod (10)) * 10) + ((((min1a / 100) + 1) Mod (10)) * 100)
        Dim min1b As Int64 = ((min1 And &H3C00) >> 10) Mod (10)
        Dim min1c As Int64 = (min1 And &H3FF)
        min1c = (min1c + 1) Mod (10) + ((((min1c Mod (100) / 10) + 1) Mod (10)) * 10) + ((((min1c / 100) + 1) Mod (10)) * 100)

        ''? string formatter method..?
        ''sprintf_s(buff, 0x30, "%03d %03d %d %03d",min2,min1a,min1b,min1c);  // result

        cdmaTerm.nam0MDNTextbox.Text = (min2.ToString + min1a.ToString + min1b.ToString + min1c.ToString)

        '' cdmaTerm.nam0MDNTextbox.Text = (min2) Mod (&H3D).ToString + min1a Mod (&H3D).ToString + min1b Mod (&HD).ToString + min1c Mod (&H3D).ToString

        '' Catch
        '' MessageBox.Show("damn decoder ring: cant get decoded min1")

        ''End Try

    End Sub
    Sub decode_ReadNam0MDN(ByVal cmd As Command)
        ''test decode mdn
        Try

            ''Dim stringFromPacket As String = cdmaTerm.AtReturnCmdBox.Text
            Dim stringFromPacket As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)

            Dim thisIsTheDecodedString As String = ""


            thisIsTheDecodedString += stringFromPacket(9) + stringFromPacket(11) & _
            stringFromPacket(13) + stringFromPacket(15) & _
            stringFromPacket(17) + stringFromPacket(19) & _
            stringFromPacket(21) + stringFromPacket(23) & _
            stringFromPacket(25) + stringFromPacket(27)

            cdmaTerm.nam0MDNTextbox.Text = thisIsTheDecodedString



        Catch
            MessageBox.Show("damn decoder ring: cant get decoded mdn")

        End Try

    End Sub

#End Region


    ''New untested EVDO decoders

    Private Sub decode_NV_PPP_USER_ID_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_PPP_USER_ID_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)

            ''set the display to the first username and pass
            cdmaTerm.evdo_usernameTextbox.Text = trimFrontAndEndAscii(thisIsTheDecodedString)


        Catch
            MessageBox.Show("damn decoder ring: decode_NV_PPP_USER_ID_I")

        End Try
    End Sub

    Private Sub decode_NV_PPP_PASSWORD_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_PPP_PASSWORD_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)

            ''set the display to the first username and pass
            cdmaTerm.evdo_passwordTextbox.Text = trimFrontAndEndAscii(thisIsTheDecodedString)

        Catch
            MessageBox.Show("damn decoder ring: decode_NV_PPP_PASSWORD_I")

        End Try
    End Sub

    Private Sub decode_NV_PAP_USER_ID_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_PAP_USER_ID_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring: decode_NV_PAP_USER_ID_I")

        End Try
    End Sub

    Private Sub decode_NV_PAP_PASSWORD_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_PAP_PASSWORD_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring: decode_NV_PAP_PASSWORD_I")

        End Try
    End Sub

    Private Sub decode_NV_HDR_AN_AUTH_USER_ID_LONG(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_HDR_AN_AUTH_USER_ID_LONG.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring:  decode_NV_HDR_AN_AUTH_USER_ID_LONG")

        End Try
    End Sub

    Private Sub decode_NV_HDR_AN_AUTH_PASSWD_LONG(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_HDR_AN_AUTH_PASSWD_LONG.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring:decode_NV_HDR_AN_AUTH_PASSWD_LONG")

        End Try
    End Sub

    Private Sub decode_NV_HDR_AN_AUTH_NAI_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_HDR_AN_AUTH_NAI_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring: decode_NV_RSVD_ITEM_579_I")

        End Try
    End Sub

    Private Sub decode_NV_HDR_AN_AUTH_PASSWORD_I(ByVal bytesRxd As Byte())
        Try
            Dim thisIsTheDecodedString As String = getAsciiStrings(bytesRxd)
            cdmaTerm.txtBoxNV_HDR_AN_AUTH_PASSWORD_I.Text = trimFrontAndEndAscii(thisIsTheDecodedString)
        Catch
            MessageBox.Show("damn decoder ring: decode_NV_RSVD_ITEM_580_I")

        End Try
    End Sub

    Private Function trimFrontAndEndAscii(ByVal before As String) As String
        Dim after As String = before
        Return after.Substring(4, before.Length - 7)

    End Function

    Private Function trimFrontAndEndAsciiSpecific(ByVal before As String, ByVal start As Integer, ByVal lengthMinus As Integer) As String
        Dim after As String = before
        Return after.Substring(start, before.Length - lengthMinus)

    End Function

    Public Function getAsciiStrings(ByVal bytes As Byte()) As String


        Dim bString = cdmaTerm.biznytesToStrizings(bytes)

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

            ' While there's still something to convert in the hex string

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

    Private Sub decode_DIAG_SPC_F(ByVal cmd As Command)

        If cmd.bytesRxd(1) <> 1 Then
            MessageBox.Show("Spc not accepted, don't send anything for 10 seconds (or devterm will crash)")
            System.Threading.Thread.Sleep(1000)
        ElseIf cmd.bytesRxd(1) = 1 And cmd.bytesRxd(0) = &H41 Then
            MessageBox.Show("Spc Accepted")
        End If

    End Sub

    Private Sub decode_NV_MIN1(ByVal cmd As Command)
        '' MessageBox.Show("rxd: " + cdmaTerm.biznytesToStrizings(cmd.bytesRxd))
        ''MessageBox.Show("rxd: " + getAsciiStrings(cmd.bytesRxd))

        ''cdmaTerm.MIN1RawTxtbox.Text = cdmaTerm.biznytesToStrizings(New Byte() {cmd.bytesRxd(7), cmd.bytesRxd(6), cmd.bytesRxd(5), cmd.bytesRxd(4)})

        ''test to fix lg
        cdmaTerm.MIN1RawTxtbox.Text = cdmaTerm.biznytesToStrizings(New Byte() {cmd.bytesRxd(11), cmd.bytesRxd(10), cmd.bytesRxd(9), cmd.bytesRxd(8)})


        'Dim min1 As New Integer
        'min1 = &HF9D260
        'Dim min2 As New Integer
        'min2 = &H3E7

        'min2 = (min2 + 1) Mod 10 + ((((min2 Mod 100 / 10) + 1) Mod 10) * 10) + ((((min2 / 100) + 1) Mod 10) * 100)

        'Dim min1a As New Int64
        'min1a = (min1 And &HFFC000) >> 14
        'min1a = (min1a + 1) Mod 10 + ((((min1a Mod 100 / 10) + 1) Mod 10) * 10) + ((((min1a / 100) + 1) Mod 10) * 100)

        'Dim min1b As New Int64
        'min1b = ((min1 And &H3C00) >> 10) Mod 10

        'Dim min1c As New Int64
        'min1c = (min1 And &H3FF)
        'min1c = (min1c + 1) Mod 10 + ((((min1c Mod 100 / 10) + 1) Mod 10) * 10) + ((((min1c / 100) + 1) Mod 10) * 100)

        'MessageBox.Show("test Min Decode: " + "min2: " + min2 + "min1a: " + min1a + "min1b: " + min1b + "min1c: " + min1c)



    End Sub

    Private Sub decode_NV_MIN2(ByVal cmd As Command)
        ''this worked samsung but not lg
        ''cdmaTerm.MIN2RawTxtbox.Text = cdmaTerm.biznytesToStrizings(New Byte() {cmd.bytesRxd(4), cmd.bytesRxd(4)})

        cdmaTerm.MIN2RawTxtbox.Text = cdmaTerm.biznytesToStrizings(New Byte() {cmd.bytesRxd(7), cmd.bytesRxd(6)})
    End Sub

    Public Function decode_NV_MIN1(ByVal min1 As String, ByVal min2 As String) As String
        Try

            Return decode_NV_MIN1(System.Convert.ToInt32(min1, 16), System.Convert.ToInt32(min2, 16))

        Catch ex As Exception
            MessageBox.Show("decode_NV_MIN1 string string err: " + ex.ToString)
            Return ""
        End Try

    End Function
    Public Function decode_NV_MIN1(ByVal min1 As Long, ByVal min2 As Long) As String

        min2 = (min2 + 1) Mod 10 + (((((min2 Mod 100) \ 10) + 1) Mod 10) * 10) + ((((min2 \ 100) + 1) Mod 10) * 100)

        Dim min1a As New Integer
        min1a = (min1 And &HFFC000) >> 14

        min1a = (min1a + 1) Mod 10 + (((((min1a Mod 100) \ 10) + 1) Mod 10) * 10) + ((((min1a \ 100) + 1) Mod 10) * 100)

        Dim min1b As New Integer
        min1b = ((min1 And &H3C00) >> 10) Mod 10

        Dim min1c As New Integer
        min1c = (min1 And &H3FF)

        Dim min1c_5b As Integer = (min1c + 1) Mod 10
        Dim min1c_5c As Integer = (((((min1c Mod 100) \ 10) + 1) Mod 10) * 10)
        Dim min1c_5d As Integer = ((((min1c \ 100) + 1) Mod 10) * 100)

        min1c = min1c_5b + min1c_5c + min1c_5d

        Return min2.ToString("000") + min1a.ToString("000") + min1b.ToString("0") + min1c.ToString("000")

    End Function

    Function encode_NV_MIN1(ByVal min1str As String) As String()
        Dim min1 As String = "Min1"
        Dim min2 As String = "Min2"

        'WORD min2=(((byte)_strtoui64(min1str.Mid(0,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(1,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(2,1),NULL,16)+9)%10 );
        Dim min2i As Integer = (((min1str.Substring(0, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(1, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(2, 1)) + 9) Mod 10)

        'DWORD min1a=(((byte)_strtoui64(min1str.Mid(3,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(4,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(5,1),NULL,16)+9)%10);
        Dim min1a As Integer = (((min1str.Substring(3, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(4, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(5, 1)) + 9) Mod 10)

        'DWORD min1b=(byte)_strtoui64(min1str.Mid(6,1),NULL,16);
        Dim min1b As Integer = (min1str.Substring(6, 1))

        'if (min1b==0) min1b=10;
        If (min1b = 0) Then
            min1b = 10
        End If
        'DWORD min1c=(((byte)_strtoui64(min1str.Mid(7,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(8,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(9,1),NULL,16)+9)%10);
        Dim min1c As Integer = (((min1str.Substring(7, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(8, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(9, 1)) + 9) Mod 10)

        'DWORD min1=min1c+(min1b<<10)+(min1a<<14);
        min1 = (min1c + (min1b << 10) + (min1a << 14)).ToString("x8")

        'min1 = ""
        min2 = min2i.ToString("x4")
        'char *buff=(char*)malloc(0x30);
        'sprintf_s(buff,0x30,"%02X",min2); // Output Min2
        'sprintf_s(buff,0x30,"%08X",min1); // Output Min1

        Return New String() {min1.ToUpper, min2.ToUpper}
    End Function

    Private Sub decode_NV_DS_QCMIP_I(ByVal bytesRxd As Byte())
        ''read evdo type
        ''00 simple
        ''01 mob+simple
        ''02 mobile
        Dim type As Integer = bytesRxd(3)
        cdmaTerm.evdoModeCombo.SelectedIndex = type


    End Sub

    Private Sub decode_11055(ByVal bytesRxd As Byte())
        ''TODO untested
        ''System.Convert.ToInt32((bytesRxd(4)+bytesRxd(3)), 16) needed?

        ''cdmaTerm.BbRegIdTextbox.Text = Integer.Parse(bytesRxd(4).ToString + bytesRxd(3).ToString).ToString
        ''cdmaTerm.BbRegIdTextbox.Text = System.Convert.ToInt32((bytesRxd(4).ToString + bytesRxd(3).ToString), 16).ToString
        cdmaTerm.BbRegIdTextbox.Text = System.Convert.ToInt32((bytesRxd(4).ToString("x2") + bytesRxd(3).ToString("x2")), 16).ToString



    End Sub

    Private Sub decode_NV_HOME_SID_NID_I(ByVal bytesRxd As Byte())
        cdmaTerm.SIDTextbox.Text = System.Convert.ToInt32((bytesRxd(5).ToString("x2") + bytesRxd(4).ToString("x2")), 16).ToString
        cdmaTerm.NIDTextbox.Text = System.Convert.ToInt32((bytesRxd(7).ToString("x2") + bytesRxd(6).ToString("x2")), 16).ToString


    End Sub

    Private Sub decode_DIAG_PEEKB_F(ByVal cmd As Command)
        ''unused.. could add test to see if read is bad
        If cdmaTerm.ReadingRamToFile Then
            '' cdmaTerm.dispatchQ.SaveTextToFile()
        End If

    End Sub

    Private Sub decode_NV_NAM_LOCK_I(ByVal bytesRxd As Byte())
        If bytesRxd(4) = 0 Then
            cdmaTerm.NamLockCheckbox.Checked = False
        ElseIf bytesRxd(4) = 1 Then
            cdmaTerm.NamLockCheckbox.Checked = True
        End If

    End Sub

    Private Sub decode_NV_DS_MIP_NUM_PROF_I(ByVal bytesRxd As Byte())

        cdmaTerm.NumberOfProfilesComboBox1.Text = bytesRxd(3).ToString

    End Sub
    Private Sub decode_NV_DS_MIP_ENABLE_PROF_I(ByVal bytesRxd As Byte())
        cdmaTerm.SelectedProfileCombo.Text = bytesRxd(3).ToString
    End Sub

    Private Sub decodeSubsysItem(ByVal cmd As Command)
        ''wtfthisistheworstmethodever
        If cmd.bytesToTx(2) = Qcdm.SubsysStorage.DIAG_EFS2_READDIR Then

            Dim checkByte As Integer = cmd.bytesRxd.Length() - 5

            'If (cmd.bytesRxd(checkByte) = 0) Then
            '    cdmaTerm.EfsQc.LastEfsWorked = False
            'Else
            '    cdmaTerm.EfsQc.LastEfsWorked = True
            'End If
            ''  Dim strM As String = "Keep Reading Folders?" + cdmaTerm.biznytesToStrizings(cmd.bytesRxd)
            '' Dim continueEfs As Boolean = MessageBox.Show(strM, "Keep Reading?", MessageBoxButtons.OKCancel)
            ''Dim continueEfs As Boolean

            'If MessageBox.Show(strM, "Keep Reading?", MessageBoxButtons.OKCancel, _
            '  Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            '    continueEfs = True

            'Else
            '    continueEfs = False
            'End If


            Dim step1 As String = cdmaTerm.biznytesToStrizings(cmd.bytesRxd)
            step1 = step1.Substring(80, step1.Length - 86)
            Dim cleanedUp As String = getAsciiStrings(cdmaTerm.String_To_Bytes(step1))

            If (cleanedUp.Length > 1) And cmd.bytesRxd(16) = 1 Then

                addToEfsTreeView(cleanedUp, "folder")


                'If cmd.bytesRxd.Length > 40 Then
                '    '' Dim cleanedUp As String = trimFrontAndEndAsciiSpecific(getAsciiStrings(cmd.bytesRxd), 39, 4)


                '    ''Dim cleanedUp As String = getAsciiStrings(cdmaTerm.String_To_Bytes(cdmaTerm.biznytesToStrizings(cmd.bytesRxd).Substring(40, (cmd.bytesRxd.Length - 40))))


                '    step1 += "!"
                '    '' Array.Copy(cmd.bytesRxd, 39, cleanedUp, 0, (cmd.bytesRxd.Length - 43))

                '    ''  cdmaTerm.TreeView2.Nodes.Add(cleanedUp)
                'End If


                cdmaTerm.EfsQc.LastEfsWorked2 = True

                ''TODO: check for length begin index
            ElseIf (cleanedUp.Length > 1) Then


                cdmaTerm.EfsQc.LastEfsWorked2 = True

                addToEfsTreeView(cleanedUp, "file")
            Else
                ''uh..?
                cdmaTerm.EfsQc.LastEfsWorked2 = False

            End If

        End If

        If (cmd.bytesToTx(2) = Qcdm.SubsysStorage.DIAG_EFS2_READDIR) And (cmd.bytesRxd.Length < 40) Then
            ''TODO EFS STOP FIX
            cdmaTerm.EfsQc.LastEfsWorked2 = False


        End If


    End Sub



    Public Function addToEfsTreeView(ByVal f As String, ByVal efsType As String) As Boolean

        If efsType = "folder" Then

            cdmaTerm.FolderTreeView1.Nodes.Add(f)
        Else
            cdmaTerm.FileTreeView2.Nodes.Add(f)

            Return True
        End If

        Return False
    End Function

 

   




End Class
