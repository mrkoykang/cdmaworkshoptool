'' CDMA DEV TERM
'' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
'' www.chromableedstudios.com
'' chromableedstudios ( a t ) gmail ( d o t ) com
''     
'' cdmadevterm by ¿k? with help from ajh and jh
''
'' this was originally developed as a test framework, before many 
'' things about qcdm(and programming) were understood by the author
'' please forgive some code that should never have seen the light of day ;)
''
''-------------------------------------------------------------------------------------------------------------
'' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
'' check out the GPL v3 for details
'' http://www.gnu.org/copyleft/gpl.html
''-------------------------------------------------------------------------------------------------------------
''
''cdmaDevTerm 
''build:
''alphaalphaOrxMEID16MworkingTabbySecretDecoderSamsungAutoMagic(applyDirectlyToTheForehead)HalfBakedCRC_TastesOK!lilEVDOsauceSPEEDYspcMetCalcAT$QCDMG_LOGtXrX_NV_READinPRLsendin(woot!woot!)clean_n_leanNvEditorStyle_PostMortemCleanUp_aNewData
''
''11pm - 10/04/2012
''
''here goes nop
Imports System
Imports System.IO.Ports
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports System.Xml
Imports winAPIcom
Imports System.Management
Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports cdmaDevLib.NvItems.NvItems
Imports cdmaDevLib.Qcdm.Cmd
Imports cdmaDevLib.Qcdm
Imports System.Runtime.CompilerServices


Public Class cdmaTerm

    ''Dim WithEvents mySerialPort As SerialPort = New SerialPort
    Public Shared Q As CommandQueue = New CommandQueue
    Public Shared nvReadQ As CommandQueue = New CommandQueue
    Public Shared RamReadQ As CommandQueue = New CommandQueue
    Public Shared thePhone As Phone = New Phone
    Friend Shared thePhoneRxd As Phone = New Phone
    ''this should probably be either refactored out due to 'blackberry'(winapicom) being standard now
    ''or turned into a fallback if winapicom.dll is not found
    '' "normal" for standard serialPort ----- "blackberry"  for new serialport2 testing type whatchamakallitz
    Public Shared portIsOpen As Boolean = False
    Public Shared serialportType As String = "blackberry"

    Public Shared newCommandRxd As Boolean

    Public Shared myD As New DmPort
    Public Shared sdr As New cdmaDevLib.SecretDecoderRing
    Public Shared SPC As String = ""

    Public Enum SpcReadType
        DefaultNv
        HTC
        LG
        MetroPCS
    End Enum


    Public debugMode = False



#Region "Port Setup"
    ''some of this logic may still be needed but in a different way
    Public Shared Sub GetComs()

        ' Get a list of serial port names.
        thePhone.AvailableComPorts = COMPortInfo.COMPortInfo.GetCOMPortsInfo()

    End Sub

    ''no se
    Public commandInProgress As String

    Dim rxBuff As String

    Dim mySDR As New SecretDecoderRing

    Public Shared mySerialPort2 As New SerialCom("\\.\COM3", 9600, IO.Ports.StopBits.One, IO.Ports.Parity.None, 8) ''actual winapi port
    ''this is the AT return changed
    ''think in a farther program the box can just be replaced with a string VAR?

    Private Function HexToAsciiStr(ByVal incomingString As String) As String
        Dim ret As String = ""
        Try
            '' TEST REPLACE BLANK
            ''DIM HEXVALUE AS STRING = ATRETURNCMDBOX.TEXT.REPLACE("00", STRING.EMPTY)
            '' TEST REPLACE KP
            ''TODO: BUG SEEMS TO CLIP LAST 30'S ASCII 0 WHEN NEXT TO A 0000 BLOCK. 
            ''MAYBE BETTER IS A LOOP THAT CHECKS EACH TWO CHAR FOR 00 VS 30 31 32 3F ETC
            ''DIM HEXVALUE AS STRING = ATRETURNCMDBOX.TEXT.REPLACE("00", "5F")
            ''UPDATE: FIXED BY ADDISON A WHILE AGO(PRETTY SURE..)...

            incomingString = incomingString.Replace(" ", "")
            Dim hexValue As String = ""
            For I = 0 To incomingString.Length - 1
                If (incomingString.Substring(I, 2) = "00") Then
                    hexValue += "5F"
                    I += 1
                Else
                    hexValue += incomingString.Substring(I, 2)
                    I += 1
                End If
            Next
            ''ORIGINAL
            '' DIM HEXVALUE AS STRING = ATRETURNCMDBOX.TEXT
            ' AN OBJECT STORING THE STRING VALUE

            ' WHILE THERE'S STILL SOMETHING TO CONVERT IN THE HEX STRING
            While hexValue.Length > 0
                ' USE TOCHAR() TO CONVERT EACH ASCII VALUE (TWO HEX DIGITS) TO THE ACTUAL CHARACTER
                ''TODO TEST U I NT32 U I NT64
                ret += System.Convert.ToChar(System.Convert.ToUInt64(hexValue.Substring(0, 2), 16)).ToString()
                ' REMOVE FROM THE HEX OBJECT THE CONVERTED VALUE
                hexValue = hexValue.Substring(2, hexValue.Length - 2)
            End While
            ' SHOW WHAT WE JUST CONVERTED
            ''LOGGER.ADDTOLOG(STRVALUE)
        Catch EX As Exception
            Logger.Add("HexToAsciiStr error:" + EX.ToString)
            '' Q.INTERRUPTCOMMANDQ()''todo:should this end it? probably not
        End Try

        Return ret
    End Function



    Public Shared Sub Connect(portName As String)
        Try
            ''ajh dg change 1 - need to check for port being opened already /
            '' dg added checkbox to test bb winapi dll

            Dim nameByDescription = thePhone.AvailableComPorts.Find(Function(f) f.Description = portName)
            If (nameByDescription IsNot Nothing) Then
                portName = thePhone.AvailableComPorts.Find(Function(f) f.Description = portName).Name
            End If


            If (portName = Nothing Or portName = "") Then
                GetComs()
                portName = thePhone.AvailableComPorts.Find(Function(f) f.Name = portName).Name
            End If

            serialportType = "blackberry" ''aka winApiCom
            If (portIsOpen = False) Then

                mySerialPort2.SetPort("\\.\" + portName) ''todo:untested?
                Dim result = mySerialPort2.Open()
                ''ToolStripStatusLabel1.Text = "connect ok || Type : WinApiCom.dll || " + ("\\.\" + GetPlainPortNameFromFriendly(ComNumBox1.Text))
                If (result) Then
                    portIsOpen = True
                    Logger.Add("portIsOpen = true - port " + portName + " opened")
                    Logger.Add("Connected to " + portName, Logger.LogType.Msg)
                Else
                    Logger.Add("can't connect")
                End If

            Else
                Logger.Add("portIsOpen == true - can't connect")
            End If

        Catch ex As Exception
            Logger.Add("ex: " + ex.ToString)

        End Try
    End Sub


#End Region

#Region "onFormOpenAndOnFormClose"
    ''coding koan: make it work, fix it later
    ' ''on form load

    Public Shared Sub initSixteenDigitCodes(spFilePath As String)
        If (System.IO.File.Exists(spFilePath)) Then
            SixteenDigitCodes.set16DigitPasswords(spFilePath)
            cdmaTerm.thePhone.SpSixteenDigit = cdmaTerm.thePhone.SpSixteenDigit
        End If
    End Sub

    '        qcCommandsCombo.DataSource = [Enum].GetValues(GetType(Qcdm.Cmd))
    '        nvItemsCombo.DataSource = [Enum].GetValues(GetType(NvItems.NVItems))

    '        ''set the default nv read mode
    '        readSPCTypeCombo.SelectedIndex = 0
    '        ''this stuff is good leave alone for now
    '        Try
    '            ''assign the box the first com found
    '            ComNumBox1.Text = ComNumBox1.Items.Item(ComNumBox1.Items.Count - 1)
    '        Catch
    '            logger.addToLog("no com devices found")
    '        End Try

    '    Catch ex As Exception
    '        logger.addToLog("Load error:" + e.ToString)
    '    End Try

    'End Sub



    ' ''check if the port is open on close
    'Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    ''kill 'er if she aint dead
    '    If mySerialPort.IsOpen Then
    '        mySerialPort.Close()
    '    End If
    'End Sub





#End Region


#Region "selectionRoutinesAndSelectors"


    ''sub to try converting sp etc
    Public Shared Sub SendA16digitCode(Send16DigitSP As String)
        ' If Send16DigitCodeTextbox = "" Then
        ''try out the textbox parser

        'Dim MySixteenDigitCodes As New SixteenDigitCodes()

        'Q.addCommandToQ(
        '    New Command(
        '        DIAG_PASSWORD_F,
        '        String_To_Bytes(SixteenDigitCodes.get16DigitPassword(select16digitCodeBox)),
        '        "16 digit password"
        '        )
        '    )
        'Q.Run()

        ' Else
        If (Send16DigitSP.Length = 16) Then

            Q.Add(
                New Command(
                    DIAG_PASSWORD_F,
                    Send16DigitSP.ToHexBytes(),
                    "Send custom 16 digit DIAG_PASSWORD_F"
                    )
                )
            Q.Run()
        Else
            Logger.Add("16 digit SP is not 16 digits", Logger.LogType.Msg)
        End If



    End Sub

    ''TODO: use factory
    Shared Sub readSpcFromPhone(ByVal spcType As cdmaTerm.SpcReadType)
        ''first check which read type then go
        If spcType = cdmaTerm.SpcReadType.DefaultNv Then

            Q.Add(CommandFactory.GetCommand(NvItems.NvItems.NV_SEC_CODE_I))


        ElseIf spcType = cdmaTerm.SpcReadType.HTC Then

            Q.Add(New Command(unlockHtcSuperSPC, "unlockHtcSuperSPC byte array method"))
            Q.Add(New Command(readSPC_HTCMethod, "readSPC_HTCMethod byte array method"))
            Q.Add(CommandFactory.GetCommand(NvItems.NvItems.NV_SEC_CODE_I))

        ElseIf spcType = cdmaTerm.SpcReadType.LG Then

            ''ajh7495 start 3
            Q.Add(New Command(unlockLgNvMemory, "Unlock LG NV Memory"))
            Q.Add(New Command(readSPC_LgMethod, "Read SPC _ LG Method")) '' needs response to complete - send
            Q.Add(New Command(readSPC_LgMethod, "ReadSPC_LG", "Read SPC _ LG Method | True | ReadSPC_LG|"))
            ''Q.Run()


            'ElseIf spcType = "Samsung1" Then
            '    ''logger.addToLog("s1 method not here yet")
            '    ''TODO: Find sample, make decoder
            '    Q.addCommandToQ(New Command(readSPC_Samsung1Method, "ReadSPC_NV", "readSPC_Samsung1Method byte array method"))


            'ElseIf spcType = "Samsung2" Then
            '    ''logger.addToLog("s2 method")
            '    ''TODO: Find sample, make decoder
            '    Q.addCommandToQ(New Command(readSPC_Samsung2Method, "ReadSPC_NV", "readSPC_Samsung2Method byte array method"))


            'ElseIf spcType = "Kyocera" Then
            '    ''logger.addToLog("kyocera method")
            '    ''TODO: Find sample, make decoder
            '    Q.addCommandToQ(New Command(readSPC_Kyocera, "ReadSPC_NV", "readSPC_Kyocera byte array method"))


            'ElseIf spcType = "EFS" Then
            '    '' logger.addToLog("efs method")

            '    Q.addCommandToQ(New Command(readSPC_EFSMethod_SubsytemCmd, "readSPC_EFSMethod_SubsytemCmd byte array method"))
            '    Q.addCommandToQ(New Command(readSPC_EFSMethod_EfsCmd, "readSPC_EFSMethod_EfsCmd byte array method"))

            '    ''insert decoder
        ElseIf spcType = cdmaTerm.SpcReadType.MetroPCS Then

            Q.Add(CommandFactory.GetCommand(DIAG_ESN_F))
            Q.Run()


            Try
                Dim ajhBlackMagic As New metroCalc
                ''get esn from gui
                thePhone.Spc = ajhBlackMagic.MetroSPCcalc(thePhone.Esn) ''todo: what to do with result in this design?
            Catch ex As Exception
                Logger.Add("metro spc error: " + ex.ToString)
            End Try

        End If

    End Sub


#End Region


    Public Shared Sub SendTerminalCommand(terminalCommand As String)
        Q.Clear()
        Q.Add(CommandFactory.GetCommand(terminalCommand))
        Q.Run()
    End Sub


    'Private Sub ScanButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanButton.Click
    '    ComNumBox1.Items.Clear()

    '    Dim allTheNames As String = ""
    '    For Each comPort As COMPortInfo.COMPortInfo In COMPortInfo.COMPortInfo.GetCOMPortsInfo
    '        ''allTheNames += (comPort.Name + " " + comPort.Description + " / ")

    '        ComNumBox1.Items.Add(comPort.Name + " " + comPort.Description)
    '    Next
    '    scanAndListComs()

    '    ''logger.addToLog("Ports available: " + allTheNames)


    'End Sub

    'Private Sub ATSendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATSendButton.Click
    '    Try


    '        Dim myDm As New DmPort
    '        Dim atCmd As New List(Of Byte)
    '        For Each c As Char In AtSendCommand.Text
    '            atCmd.Add(System.Convert.ToUInt32(c))
    '        Next

    '        atCmd.Add(&HD)
    '        atCmd.Add(&HA)

    '        AtReturnCmdBox.Text = biznytesToStrizings(myDm.WriteRead(atCmd.ToArray))

    '        ''Pause for 200ms
    '        'System.Threading.Thread.Sleep(200)
    '        'Try
    '        '    AtReturnCmdBox.AppendText(mySerialPort2.ReadString(&H1000))
    '        'Catch
    '        '    logger.addToLog("no more b")
    '        'End Try
    '    Catch
    '        logger.addToLog("Cant Open AT Port")
    '    End Try

    'End Sub

    Public Shared Sub sendATCommand(ByVal atCommand As String)
        Try
            '            ''SEND AN AT COMMAND
            Dim myDm As New DmPort
            Dim atCmd As New List(Of Byte)
            For Each c As Char In atCommand
                atCmd.Add(System.Convert.ToUInt32(c))
            Next

            atCmd.Add(&HD)
            atCmd.Add(&HA)

            Logger.Add(myDm.WriteRead(atCmd.ToArray).ToHexString(), Logger.LogType.InfoAndMsg)

        Catch

            Logger.Add("Cant Open AT Port", Logger.LogType.InfoAndMsg)
        End Try


    End Sub

    'Private Sub modeOfflineButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modeSwitchButton.Click
    '    Q.Clear()
    '    modeSwitch(modeSwitchCombo.Text)
    '    Q.Run()

    '    If modeSwitchCombo.Text = "Reset" Then

    '        ''Attempt to fix crazy BSOD
    '        ''Driver unloaded without cancelling pending operations?
    '        ''TODO: wtf. maybe the bsod was a fluke.. not even sure if this does anything. madness.
    '        While (Q.GetCount > 0)
    '            System.Threading.Thread.Sleep(150)
    '        End While

    '        Try
    '            mySerialPort2.Flush()
    '            mySerialPort2.Dispose()

    '            ConnectButton.Enabled = True
    '            disconnectPortButton.Enabled = False
    '            logger.addToLog("Phone has been reset, port has been disconnected. Reconnect when the phone powers back on")
    '        Catch ex As Exception
    '            logger.addToLog("Mode reset disconnect err: " + ex.ToString)
    '        End Try
    '    End If

    'End Sub


    ''All the commands
#Region "CommandsAsByteArrays"

    ''BEGIN THE RANDOM ARRAYS FOR COMMANDS N SHIT
    ''key presses
    Public Shared ReadOnly keyPress_Pound() As Byte = {&H20, &H0, &H23, &H6E, &HD6, &H7E}
    Public Shared ReadOnly keyPress_Star() As Byte = {&H20, &H0, &H2A, &HAF, &H4B, &H7E}
    Public Shared ReadOnly keyPress_0() As Byte = {&H20, &H0, &H30, &H74, &HF4, &H7E}
    Public Shared ReadOnly keyPress_1() As Byte = {&H20, &H0, &H31, &HFD, &HE5, &H7E}
    Public Shared ReadOnly keyPress_2() As Byte = {&H20, &H0, &H32, &H66, &HD7, &H7E}
    Public Shared ReadOnly keyPress_3() As Byte = {&H20, &H0, &H33, &HEF, &HC6, &H7E}
    Public Shared ReadOnly keyPress_4() As Byte = {&H20, &H0, &H34, &H50, &HB2, &H7E}
    Public Shared ReadOnly keyPress_5() As Byte = {&H20, &H0, &H35, &HD9, &HA3, &H7E}
    Public Shared ReadOnly keyPress_6() As Byte = {&H20, &H0, &H36, &H42, &H91, &H7E}
    Public Shared ReadOnly keyPress_7() As Byte = {&H20, &H0, &H37, &HCB, &H80, &H7E}
    Public Shared ReadOnly keyPress_8() As Byte = {&H20, &H0, &H38, &H3C, &H78, &H7E}
    Public Shared ReadOnly keyPress_9() As Byte = {&H20, &H0, &H39, &HB5, &H69, &H7E}
    Public Shared ReadOnly keyPress_SEND_UP() As Byte = {&H20, &H0, &H50, &H72, &H97, &H7E}
    Public Shared ReadOnly keyPress_END_DN() As Byte = {&H20, &H0, &H51, &HFB, &H86, &H7E}

    ''change modes
    Public Shared ReadOnly modeOfflineD() As Byte = {&H29, &H1, &H0, &H31, &H40, &H7E}
    Public Shared ReadOnly modeReset() As Byte = {&H29, &H2, &H0, &H59, &H6A, &H7E}
    Public Shared ReadOnly modeOnline() As Byte = {&H29, &H4, &H0, &H89, &H3E, &H7E}
    Public Shared ReadOnly modeLow() As Byte = {&H29, &H5, &H0, &H51, &H27, &H7E}

    ''these are commands as byte arrays including crc and eof
    ''00identify
    Public Shared ReadOnly Diag00() As Byte = {&H0, &H78, &HF0, &H7E}
    ''01readesn
    Public Shared ReadOnly Diag01() As Byte = {&H1, &HF1, &HE1, &H7E}

    ''nvread meid
    Public Shared ReadOnly nvmodeMEIDRead() As Byte = {&H26, &H97, &H7, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H69, &H3E, &H7E}

    Public Shared ReadOnly send16digitFs() As Byte = {&H46, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFE, &H74, &H7E}
    Public Shared ReadOnly send16digitSchU350() As Byte = {&H46, &H19, &H45, &H8, &H15, &H20, &H8, &H11, &H6, &HE7, &H20, &H7E}


    ''read spc functions
    Public Shared ReadOnly readSPC_nvMethod() As Byte = {&H26, &H55, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HA5, &H34, &H7E}
    Public Shared ReadOnly readSPC_LgMethod() As Byte = {&H11, &H17, &H0, &H8, &H1C, &HA6, &H7E}
    Public Shared ReadOnly readSPC_HTCMethod() As Byte = {&H41, &H74, &H64, &H77, &H61, &H6F, &H70, &H42, &H4A, &H7E}
    Public Shared ReadOnly readSPC_EFSMethod_SubsytemCmd() As Byte = {&H4B, &H13, &HF, &H0, &H6E, &H76, &H6D, &H2F, &H6E, &H76, &H6D, &H2F, &H6E, &H76, &H6D, &H5F, &H73, &H65, &H63, &H75, &H72, &H69, &H74, &H79, &H0, &H2A, &H69, &H7E}
    Public Shared ReadOnly readSPC_EFSMethod_EfsCmd() As Byte = {&H59, &H4, &H0, &H15, &H6E, &H76, &H6D, &H2F, &H6E, &H76, &H6D, &H2F, &H6E, &H76, &H6D, &H5F, &H73, &H65, &H63, &H75, &H72, &H69, &H74, &H79, &H0, &H85, &H20, &H7E}
    Public Shared ReadOnly readSPC_Samsung1Method() As Byte = {&H26, &H52, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H71, &HF6, &H7E}
    Public Shared ReadOnly readSPC_Samsung2Method() As Byte = {&H26, &H53, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H50, &H68, &H7E}
    Public Shared ReadOnly readSPC_Kyocera() As Byte = {&H29, &H3, &H0, &H81, &H73, &H7E}

    ''send spc
    Public Shared ReadOnly sendSPC_DefMethod000000() As Byte = {&H41, &H30, &H30, &H30, &H30, &H30, &H30, &HDF, &H8A, &H7E}
    ''write spc
    Public Shared ReadOnly writeSPC_DefMethod000000() As Byte = {&H27, &H55, &H0, &H30, &H30, &H30, &H30, &H30, &H30, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H84, &HB7, &H7E}
    ''uh?
    Public Shared ReadOnly unlockHtcSuperSPC() As Byte = {&H41, &H74, &H64, &H77, &H61, &H6F, &H70, &H42, &H4A, &H7E}




    ''random test sht
    ''qm calls this read build id
    Public Shared ReadOnly readChipset() As Byte = {&H7C, &H93, &H49, &H7E}

    ''NAM Information
    ''NV_DIR_NUMBER_I
    Public Shared ReadOnly readMdnNam0() As Byte = {&H26, &HB2, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H69, &H6D, &H7E}
    Public Shared ReadOnly readMin_part2_Nam0() As Byte = {&H26, &H21, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H8F, &H11, &H7E}
    Public Shared ReadOnly readMin_part1_Nam0() As Byte = {&H26, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HAE, &H8F, &H7E}

    Public Shared ReadOnly readUserLock() As Byte = {&H26, &H52, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H71, &HF6, &H7E}

    ''lg test arrays unlock nv
    Public Shared ReadOnly unlockLgNvMemory() As Byte = {&H33, &H7D, &H5F, &HFD, &H18, &H7E}
    Public Shared ReadOnly lg_Lock1() As Byte = {&H21, &H0, &H0, &H2B, &H9F, &H7E}
    Public Shared ReadOnly lg_KeyEmu1() As Byte = {&H20, &H0, &H6, &HC1, &HA0, &H7E}
    Public Shared ReadOnly lg_Lock2() As Byte = {&H21, &H0, &H1, &HA2, &H8E, &H7E}


    ''blank arrays
    Public empty_cmd_133() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public empty_cmd_136() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public empty_cmd_264() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}


#End Region

#Region "WM_COMMNOTIFY"

    Private WM_DEVICECHANGE As Integer = &H219
    Private WM_COMMNOTIFY As Integer = &H44

    Public Enum WM_DEVICECHANGE_WPPARAMS As Integer
        DBT_CONFIGCHANGECANCELED = &H19
        DBT_CONFIGCHANGED = &H18
        DBT_CUSTOMEVENT = &H8006
        DBT_DEVICEARRIVAL = &H8000
        DBT_DEVICEQUERYREMOVE = &H8001
        DBT_DEVICEQUERYREMOVEFAILED = &H8002
        DBT_DEVICEREMOVECOMPLETE = &H8004
        DBT_DEVICEREMOVEPENDING = &H8003
        DBT_DEVICETYPESPECIFIC = &H8005
        DBT_DEVNODES_CHANGED = &H7
        DBT_QUERYCHANGECONFIG = &H17
        DBT_USERDEFINED = &HFFFF
    End Enum


    ''was used to notify when a device is plugged in, not needed?
    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '    If m.Msg = WM_COMMNOTIFY Then
    '        logger.addToLog("WM_COMMNOTIFY Triggered")
    '        ''mySerialPort2.Read(
    '    End If
    '    If m.Msg = WM_DEVICECHANGE Then

    '        Select Case m.WParam
    '            Case WM_DEVICECHANGE_WPPARAMS.DBT_DEVICEARRIVAL
    '                Try
    '                    If automagic350check.Checked = True Then
    '                        If Val(autoFlashCountTextbox.Text) > 0 Then
    '                            ''getterdone
    '                            ''autoFlashCountTextbox.Text = Val(autoFlashCountTextbox.Text) - 1

    '                            ''TODO FIX WHEN FIXING BULK FLASH
    '                            autoFlashCountTextbox.Text = "0"
    '                            u350magicAPPLYDIRECTLYTOTHEFOREHEAD()
    '                            '' logger.addToLog("connect next phone")
    '                            ''autoFlashCountTextbox.Text = Val(autoFlashCountTextbox.Text) - 1  ajh was here - hello world
    '                            '' System.Threading.Thread.Sleep(5000)
    '                        End If

    '                    End If

    '                Finally
    '                    ''System.Threading.Thread.Sleep(5000)
    '                End Try
    '                ''lblMessage.Text = "USB Inserted"

    '            Case WM_DEVICECHANGE_WPPARAMS.DBT_DEVICEREMOVECOMPLETE
    '                ''don nothing
    '        End Select
    '    End If
    '    MyBase.WndProc(m)
    'End Sub

#End Region

#Region "evdoTestCode"

    Public Shared Sub WriteEvdo(evdo_username As String, evdo_password As String)

        ''untested evdo now, potential loss of function

        Dim encoding As New System.Text.ASCIIEncoding()

        Dim evdoUserIn As Byte() = encoding.GetBytes(evdo_username)
        Dim evdoPassIn As Byte() = encoding.GetBytes(evdo_password)

        Dim countedEvdoUser As New List(Of Byte)
        countedEvdoUser.Add(evdoUserIn.Count)
        countedEvdoUser.AddRange(evdoUserIn)
        Dim countedEvdoPass As New List(Of Byte)
        countedEvdoPass.Add(evdoPassIn.Count)
        countedEvdoPass.AddRange(evdoPassIn)

        Dim evdoUser As Byte() = countedEvdoUser.ToArray
        Dim evdoPass As Byte() = countedEvdoPass.ToArray

        Q.Add(CommandFactory.GetCommand(NV_PPP_USER_ID_I, True, evdoUser))
        Q.Add(CommandFactory.GetCommand(NV_PPP_PASSWORD_I, True, evdoPass))
        Q.Add(CommandFactory.GetCommand(NV_PAP_USER_ID_I, True, evdoUser))
        Q.Add(CommandFactory.GetCommand(NV_PAP_PASSWORD_I, True, evdoPass))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_USER_ID_LONG_I, True, evdoUser))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_LONG_I, True, evdoPass))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_NAI_I, True, evdoUser))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_I, True, evdoPass))

    End Sub
    ''not being used? probably could be generic for any 'counted' nv item
    Function getEvdoUser(evdo_username As String) As Byte()
        Dim encoding As New System.Text.ASCIIEncoding()

        Dim evdoUserIn As Byte() = encoding.GetBytes(evdo_username)


        Dim countedEvdoUser As New List(Of Byte)
        countedEvdoUser.Add(evdoUserIn.Count)
        countedEvdoUser.AddRange(evdoUserIn)

        Dim evdoUser As Byte() = countedEvdoUser.ToArray
        Return evdoUser
    End Function
    Function getEvdoPass(evdo_password As String) As Byte()
        Dim encoding As New System.Text.ASCIIEncoding()
        Dim evdoPassIn As Byte() = encoding.GetBytes(evdo_password)
        Dim countedEvdoPass As New List(Of Byte)
        countedEvdoPass.Add(evdoPassIn.Count)
        countedEvdoPass.AddRange(evdoPassIn)

        Dim evdoPass As Byte() = countedEvdoPass.ToArray
        Return evdoPass
    End Function


#End Region


#Region "Random Test Code"

    Public Shared Sub SendSpc(ByVal customSPC As String)
        ''dg qc send spc
        If (customSPC = Nothing) Then
            Logger.Add("Spc null, not sent")

            Return

        End If
        Q.Add(CommandFactory.GetCommand(DIAG_SPC_F, ASCIIEncoding.ASCII.GetBytes(customSPC)))

    End Sub

    Public Shared Sub WriteSpc(ByVal customSPC As String)
        If (customSPC = Nothing) Then
            Return
        End If
        Q.Add(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, NvItems.NvItems.NV_SEC_CODE_I, ASCIIEncoding.ASCII.GetBytes(customSPC), "write spc")) ''todo:untested now

    End Sub


    Public Shared Sub LgReadMeid()

        Dim ponyXpress As New CommandQueue

        Q.Add(New Command(unlockLgNvMemory, "Unlock LG NV Memory"))
        Q.Add(New Command(lg_Lock1, "LG Lock 1"))
        Q.Add(New Command(lg_KeyEmu1, "LG Key Emu1"))
        Q.Add(New Command(keyPress_END_DN, "KeyPress END_DN"))
        Q.Add(New Command(keyPress_3, "KeyPress 3"))
        Q.Add(New Command(keyPress_7, "KeyPress 7"))
        Q.Add(New Command(keyPress_3, "KeyPress 3"))
        Q.Add(New Command(keyPress_3, "KeyPress 3"))
        Q.Add(New Command(keyPress_9, "KeyPress 9"))
        Q.Add(New Command(keyPress_2, "KeyPress 2"))
        Q.Add(New Command(keyPress_9, "KeyPress 9"))
        Q.Add(New Command(keyPress_END_DN, "KeyPress END_DN"))

        ''Q.addCommandToQ(New Command(nvmodeMEIDRead, "NV Mode MEID Read"))
        ''Q.addCommandToQ(New Command(nvmodeMEIDRead, True, "ReadMeid_NV", "Any", "NV Mode MEID Read"))
        Q.Add(New Command(readSPC_LgMethod, "ReadSPC_LG", "ReadSPC_LG"))
        Q.Add(New Command(readSPC_LgMethod, "ReadSPC_LG", "ReadSPC_LG"))
        Q.Run()


    End Sub

    Public Shared Sub ReadMIN1()
            Q.Add(CommandFactory.GetCommand(NV_MIN1_I))
            Q.Add(CommandFactory.GetCommand(NV_MIN2_I))
            Q.Run()
    End Sub

    Public Shared Sub switchToP2K()
        sendATCommand("AT+MODE=8\r")
    End Sub

    Public Shared Sub switchToQCDM()
        sendATCommand("AT$QCDMG")
    End Sub

    Public Shared Sub switchToLGDM()
        sendATCommand("AT$LGDMGO")
    End Sub

    Public Shared Sub ModeSwitch(ByVal mode As Qcdm.Mode)

        ''first check which read type then go
        If mode = Qcdm.Mode.MODE_RADIO_OFFLINE Then
            Q.Add(New Command(modeOfflineD, "mode offline")) ''TODO: refactor to actually pass qc enum
        ElseIf mode = Qcdm.Mode.MODE_RADIO_ONLINE Then
            Q.Add(New Command(modeReset, "no mode online(reset sent)"))
        ElseIf mode = Qcdm.Mode.MODE_RADIO_LOWPOWER Then
            Q.Add(New Command(modeOfflineD, "no mode low(offd sent)"))
        ElseIf mode = Qcdm.Mode.MODE_RADIO_RESET Then
            Q.Add(New Command(modeReset, "mode reset"))
        ElseIf mode = "P2K" Then
            switchToP2K()
        End If


    End Sub

    Private Sub ReadNv(rangeStart As String, rangeEnd As String, saveFilePath As String)
        Logger.Add("Reading NV - This may take a while, do not unplug.")
        Dim nv As New NvItems
        nv.readNVItemRange(rangeStart, rangeEnd)
        Q.Run()
        Logger.Add("Reading NV - This may take a while, do not unplug..")
        nvReadQ.checkNvQForBadItems()
        Logger.Add("Reading NV - This may take a while, do not unplug...")
        nvReadQ.generateNvReadReport(saveFilePath)
        Logger.Add("NV Read Complete")
    End Sub


    Public Shared EfsQc As New Qcdm

    Public Shared Sub SendPrlFile(PrlFilePath As String)
        Try
            Dim myPlus As New Prl
            myPlus.UploadPrl(PrlFilePath)
        Catch ex As Exception
            Logger.Add("Prl send err: " + ex.ToString)
        End Try
    End Sub

#End Region


    Public Shared Sub ReadAllEvdo()
        AddAllEvdo()
        Q.Run()
    End Sub
    Public Shared Sub AddAllEvdo()

        Q.Add(CommandFactory.GetCommand(NV_PPP_USER_ID_I))
        Q.Add(CommandFactory.GetCommand(NV_PPP_PASSWORD_I))
        Q.Add(CommandFactory.GetCommand(NV_PAP_USER_ID_I))
        Q.Add(CommandFactory.GetCommand(NV_PAP_PASSWORD_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_USER_ID_LONG_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_LONG_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_NAI_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_I))
        Q.Add(CommandFactory.GetCommand(NV_DS_MIP_NUM_PROF_I))
        Q.Add(CommandFactory.GetCommand(NV_DS_MIP_ENABLE_PROF_I))

    End Sub

    Public Shared Function GetComFriendlyNames()
        Return COMPortInfo.COMPortInfo.GetCOMPortsInfo()
    End Function

    Public Shared Sub ReadEvdoMode()

        Q.Add(New Command(DIAG_NV_READ_F, NV_DS_QCMIP_I, New Byte() {}, "NV_DS_QCMIP_I Read EVDO mode"))

    End Sub

    Public Shared Sub WriteEvdoMode(evdoMode As Integer)

        Dim type As Byte() = {evdoMode}
        Q.Add(CommandFactory.GetCommand(NV_DS_QCMIP_I, True, type))

    End Sub

    Public Shared Sub WriteMdn(mdn As String)
        Dim WriteData As New List(Of Byte)
        WriteData.Add(&H0)
        WriteData.AddRange(ASCIIEncoding.ASCII.GetBytes(mdn))
        cdmaTerm.WriteNv(NvItems.NvItems.NV_DIR_NUMBER_I, WriteData.ToArray)
    End Sub

    Private Shared Sub EncodeMIN(MIN1 As String)

        Dim minStrings() As String = SecretDecoderRing.encode_NV_MIN1(MIN1)
        MIN1Raw = minStrings(0)
        MIN2Raw = minStrings(1)

    End Sub

    Public Shared MIN1Raw As New String("")
    Public Shared MIN2Raw As New String("")
    Public Shared Sub WriteMin(MinNumber As String)
        EncodeMIN(MinNumber)

        Dim MIN1 As New List(Of Byte)
        ''Random Mystery Zeros...
        MIN1.Add(&H0)
        For i As Integer = MIN1Raw.ToHexBytes().Length - 1 To 0 Step -1
            MIN1.Add(MIN1Raw.ToHexBytes()(i))
        Next
        For i As Integer = MIN1Raw.ToHexBytes().Length - 1 To 0 Step -1
            MIN1.Add(MIN1Raw.ToHexBytes()(i))
        Next
        Dim MIN2 As New List(Of Byte)
        ''Random Mystery Zeros...
        MIN2.Add(&H0)
        For i As Integer = MIN2Raw.ToHexBytes().Length - 1 To 0 Step -1
            MIN2.Add(MIN2Raw.ToHexBytes()(i))
        Next
        For i As Integer = MIN2Raw.ToHexBytes().Length - 1 To 0 Step -1
            MIN2.Add(MIN2Raw.ToHexBytes()(i))
        Next
        ''Totally untested probably dangerous
        Q.Add(New Command(DIAG_NV_WRITE_F, NV_MIN1_I, MIN1.ToArray, "DIAG_NV_WRITE_F, NV_MIN1_I, MIN1.ToArray"))
        Q.Add(New Command(DIAG_NV_WRITE_F, NV_MIN2_I, MIN2.ToArray, "DIAG_NV_WRITE_F, NV_MIN2_I, MIN2.ToArray"))

        Logger.Add("min write attempted... unstable feature: warning")
    End Sub

    Shared Function DecodeMin() As String
        Return SecretDecoderRing.decode_NV_MIN1(MIN1Raw, MIN2Raw)
    End Function

    Public Shared Sub Disconnect()
        Try
            cdmaTerm.thePhone.clearViewModel()
            cdmaTerm.thePhoneRxd.clearViewModel()
            mySerialPort2.Flush()
            mySerialPort2.Dispose()
            portIsOpen = False
            Logger.Add("disconnected")
        Catch ex As Exception
            Logger.Add("disconnect err" + ex.ToString)
        End Try
    End Sub

    ''hm.. two items?
    ''requestnvitemidwrite 11055 0xD4 0x07
    ''requestnvitemidwrite 11089 0x01 0xD4 0x07
    ''http://www.mobile-files.com/forum/archive/index.php/t-584.html
    Public Shared Sub ReadBBRegId()
        Q.Add(New Command(DIAG_NV_READ_F, 11055, New Byte() {}, "Read Bb reg id")) ''todo command factory and decoders?
        Q.Add(New Command(DIAG_NV_READ_F, 11089, New Byte() {}, "Read Bb reg id 2"))

    End Sub

    Public Shared Sub WriteBBRegId(ByVal bbRegId As Byte())
        Q.Add(New Command(DIAG_NV_WRITE_F, 11055, New Byte() {bbRegId(1), bbRegId(0)}, "Read Bb reg id"))
        '' 26 51 2B 01 D4 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF
        Dim regId2 As Byte() = {&H1, bbRegId(1), bbRegId(0), &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HFF}
        Q.Add(New Command(DIAG_NV_WRITE_F, 11089, regId2, "Read Bb reg id 2"))
    End Sub

    Public Shared Sub WriteBbRegId(regId As String)
        Try
            WriteBBRegId(Integer.Parse(regId).ToString("x4").ToHexBytes())
            Q.Run()
        Catch ex As Exception
            Logger.Add("Write Bb reg err: " + ex.ToString)
        End Try
    End Sub

    Private Shared Sub WriteSidAndNid(sid As String, nid As String)
        WriteSidAndNid(Integer.Parse(sid).ToString("x4").ToHexBytes(), Integer.Parse(nid).ToString("x4").ToHexBytes())
        Q.Run()
    End Sub

    Private Shared Sub WriteSidAndNid(ByVal sid As Byte(), ByVal nid As Byte())
        Dim SidNid As New List(Of Byte)
        SidNid.Add(0)
        SidNid.Add(sid(1))
        SidNid.Add(sid(0))
        SidNid.Add(nid(1))
        SidNid.Add(nid(0))

        Q.Add(New Command(DIAG_NV_WRITE_F, NV_HOME_SID_NID_I, SidNid.ToArray, "Write SID/NID"))
    End Sub

    Public Shared ReadingRamToFile = False
    Public Shared ReadingRamFile As String

    Public Shared Sub ReadRam(RamStartAddress As String, RamStartOffset As String, RamEndAddress As String, RamEndOffset As String, outFileName As String, search As Boolean)

        ReadingRamToFile = True
        RamReadQ.Clear()
        ''myD.ReadRam(Integer.Parse(ReadRamStartAddressTextbox.Text), Integer.Parse(ReadRamEndAddressTextbox.Text), False)

        myD.ReadRam2(RamStartAddress + RamStartOffset, RamEndAddress + RamEndOffset)
        Q.Run()

        RamReadQ.generateRamReadReport(outFileName)

        If search Then
            SearchBin(outFileName)
        End If
        Logger.Add("Ram Read Complete", Logger.LogType.InfoAndMsg)
    End Sub

    Private Shared Function SearchBin(ByVal fileName As String) As List(Of String)
        Dim ResultsList As New List(Of String)
        If System.IO.File.Exists(fileName) = True Then
            Dim objReader1 As New System.IO.StreamReader(fileName)
            Dim NextRegexLine As String ''= "623456"
            Do While objReader1.Peek() <> -1
                NextRegexLine = objReader1.ReadLine()
                Dim re1 As String = "(\d)"    'Any Single Digit 1
                Dim re2 As String = "(\d)"    'Any Single Digit 2
                Dim re3 As String = "(\d)"    'Any Single Digit 3
                Dim re4 As String = "(\d)"    'Any Single Digit 4
                Dim re5 As String = "(\d)"    'Any Single Digit 5
                Dim re6 As String = "(\d)"    'Any Single Digit 6
                Dim r As Regex = New Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(NextRegexLine)
                If (m.Success) Then
                    Dim d1 = m.Groups(1)
                    Dim d2 = m.Groups(2)
                    Dim d3 = m.Groups(3)
                    Dim d4 = m.Groups(4)
                    Dim d5 = m.Groups(5)
                    Dim d6 = m.Groups(6)
                    '' logger.addToLog("( " + d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString() + " )")
                    Dim possibleSpc As String = d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString()
                    If ResultsList.Contains(possibleSpc) = False Then
                        ResultsList.Add(possibleSpc)
                    End If
                    ''ResultsListBox1.Items.Add(d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString())
                End If
            Loop
            Logger.Add("Search Bin For SPC Done", Logger.LogType.InfoAndMsg)
        Else
            Logger.Add("File Does Not Exist", Logger.LogType.InfoAndMsg)
        End If

        Return ResultsList
    End Function

    Public Shared Sub ReadAllNam()
        Q.Add(CommandFactory.GetCommand(NV_NAM_LOCK_I))
        Q.Add(CommandFactory.GetCommand(NV_DIR_NUMBER_I))
        Q.Add(CommandFactory.GetCommand(NV_MIN1_I))
        Q.Add(CommandFactory.GetCommand(NV_MIN2_I))
        Q.Add(CommandFactory.GetCommand(NV_HOME_SID_NID_I))
        Q.Add(CommandFactory.GetCommand(NV_MEID_I))
        Q.Add(CommandFactory.GetCommand(NV_LOCK_CODE_I))
        Q.Add(CommandFactory.GetCommand(DIAG_VERNO_F))

        Q.Run()

        DecodeMin()
    End Sub

    Public Shared Sub ReadNv(ByVal nv As NvItems.NvItems)
        AddNv(nv)
        Q.Run()
    End Sub

    Public Shared Sub ReadNv(ByVal nv As Integer)
        Dim nv64 As Int64 = nv
        If (NvItems.NvItems.IsDefined(GetType(NvItems.NvItems), nv64)) Then
            Dim nvi As NvItems.NvItems = CType(nv, NvItems.NvItems)
            ReadNv(nvi)
        Else
            Q.Add(New Command(Qcdm.Cmd.DIAG_NV_READ_F, nv, New Byte() {}, "Read Nv " + nv.ToString))
            Q.Run()
        End If
    End Sub

    Public Shared Sub AddNv(ByVal nv As NvItems.NvItems)
        Q.Add(CommandFactory.GetCommand(nv))
    End Sub

    Public Shared Sub WriteNv(ByVal nv As Integer, writeData As String)
        Dim nv64 As Int64 = nv
        If (NvItems.NvItems.IsDefined(GetType(NvItems.NvItems), nv64)) Then
            Dim nvi As NvItems.NvItems = CType(nv, NvItems.NvItems)
            WriteNv(nvi, writeData)
        Else
            Q.Add(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, nv, GetNvWriteDataByteList(writeData).ToArray, "Write Nv " + nv.ToString))
            Q.Run()
        End If
    End Sub

    Public Shared Sub WriteNv(ByVal nv As Integer, writeData() As Byte)
        Dim nv64 As Int64 = nv
        If (NvItems.NvItems.IsDefined(GetType(NvItems.NvItems), nv64)) Then
            Dim nvi As NvItems.NvItems = CType(nv, NvItems.NvItems)
            WriteNv(nvi, writeData)
        Else
            Q.Add(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, nv, writeData, "Write Nv " + nv.ToString))
            Q.Run()
        End If
    End Sub

    Public Shared Sub WriteNv(ByVal nv As NvItems.NvItems, writeData As String)
        AddWriteNv(nv, writeData)
        Q.Run()
    End Sub

    Public Shared Sub WriteNv(ByVal nv As NvItems.NvItems, writeData() As Byte)
        AddWriteNv(nv, writeData)
        Q.Run()
    End Sub

    Public Shared Sub AddWriteNv(ByVal nv As NvItems.NvItems, writeData As String)
        AddWriteNv(nv, GetNvWriteDataByteList(writeData).ToArray)
    End Sub
    Public Shared Sub AddWriteNv(ByVal nv As NvItems.NvItems, writeData() As Byte)
        Q.Add(CommandFactory.GetCommand(nv, True, writeData))
    End Sub

    Private Shared Function GetNvWriteDataByteList(writeData As String) As List(Of Byte)
        Dim encoding As New System.Text.ASCIIEncoding()
        Dim data() As Byte
        Dim writeDataList As New List(Of Byte)
        If (writeData.StartsWith("0x")) Then
            writeDataList.AddRange(writeData.Substring(2).ToHexBytes())
        Else
            data = encoding.GetBytes(writeData)
            writeDataList.Add(data.Count)
            writeDataList.AddRange(data)
        End If
        Return writeDataList
    End Function

    Public Shared Sub ReadQc(ByVal qc As Qcdm.Cmd)
        AddQc(qc)
        Q.Run()
    End Sub
    Public Shared Sub AddQc(ByVal qc As Qcdm.Cmd)
        Q.Add(CommandFactory.GetCommand(qc))
    End Sub

    Public Enum phoneKeys
        One = &H31
        Two = &H32
        Three = &H33
        Four = &H34
        Five = &H35
        Six = &H36
        Seven = &H37
        Eight = &H38
        Nine = &H39
        Zero = &H30
        Pound = &H23
        Star = &H2A
        SendKey = &H50
        EndKey = &H51
    End Enum


    Public Shared Sub KeyPress(k As phoneKeys)
        cdmaTerm.Q.Run()
    End Sub

    Public Shared Sub WriteNamLock(lockNam As Boolean)
        Dim namLock(1) As Byte
        If lockNam Then
            namLock(1) = 1
        End If

        Q.Add(CommandFactory.GetCommand(NV_NAM_LOCK_I, True, namLock))
        Q.Run()
    End Sub

    Public Shared Sub ReadNvList(ReadNvList As String, fileName As String)
        Try
            Dim nvItemList As String() = ReadNvList.Replace(",", "").Split(" ")
            ReadNvItemList(nvItemList)
            Logger.Add("Reading NV List - This may take a while, do not unplug...")
            nvReadQ.generateNvReadReport(fileName)
            Logger.Add("NV Read Complete")
        Catch ex As Exception
            Logger.Add("Read nv list err: " + ex.ToString)

        End Try

    End Sub

    Public Shared Sub ReadNvItemList(ByVal nvItemList As String())
        Try
            Logger.Add("Reading NV List - This may take a while, do not unplug.")
            nvReadQ.Clear()
            For i = 0 To nvItemList.Count - 1
                If nvItemList(i).Contains("-") Then
                    Dim nv As New NvItems
                    Dim subNvRange As String() = nvItemList(i).Split("-")
                    nv.readNVItemRange(subNvRange(0), subNvRange(1))
                ElseIf True Then
                    Dim debugString As String = "readNVItemList DIAG_NV_READ_F " + nvItemList(i)
                    Dim cmd = New Command(Qcdm.Cmd.DIAG_NV_READ_F, Integer.Parse(nvItemList(i)), New Byte() {}, debugString)
                    Q.Add(cmd)
                    nvReadQ.Add(cmd)
                End If
            Next
            Q.Run()
            Logger.Add("Reading NV List - This may take a while, do not unplug..")
            nvReadQ.checkNvQForBadItems()
        Catch ex As Exception
            Logger.Add("Read NV Item Range Err: " + ex.ToString)
        End Try
    End Sub

    Function ScanForReadableRam(ScanRamStart As String, ScanRamEnd As String) As List(Of String)
        Dim RamScanResultList As New List(Of String)
        ReadingRamToFile = True
        RamReadQ.Clear()

        myD.ScanRam2(ScanRamStart + "0000", ScanRamEnd + "0000")
        Q.Run()
        Dim R As List(Of String) = RamReadQ.generateRamScanReport()

        For Each s As String In R
            RamScanResultList.Add(s)
        Next

        Logger.Add("Ram Read Complete")
        Return RamScanResultList
    End Function

    Private Sub SendAdb(ByVal Adb As String, AndroidSdkPath As String)

        Dim droid As New AndroidD
        Dim thisCmd As String() = {"cd " + AndroidSdkPath, "adb " + Adb}

        Logger.Add(droid.SendCMD(thisCmd))

    End Sub

    Public Shared Sub ReadAllPhone()
        ''evdo
        Q.Add(CommandFactory.GetCommand(NV_PPP_USER_ID_I))
        Q.Add(CommandFactory.GetCommand(NV_PPP_PASSWORD_I))
        Q.Add(CommandFactory.GetCommand(NV_PAP_USER_ID_I))
        Q.Add(CommandFactory.GetCommand(NV_PAP_PASSWORD_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_USER_ID_LONG_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_LONG_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_NAI_I))
        Q.Add(CommandFactory.GetCommand(NV_HDR_AN_AUTH_PASSWORD_I))
        ''evdo profiles/mode
        Q.Add(CommandFactory.GetCommand(NV_DS_QCMIP_I))
        Q.Add(CommandFactory.GetCommand(NV_DS_MIP_NUM_PROF_I))
        Q.Add(CommandFactory.GetCommand(NV_DS_MIP_ENABLE_PROF_I))
        ''nam1
        Q.Add(CommandFactory.GetCommand(NV_NAM_LOCK_I))
        Q.Add(CommandFactory.GetCommand(NV_DIR_NUMBER_I))
        Q.Add(CommandFactory.GetCommand(NV_MIN1_I))
        Q.Add(CommandFactory.GetCommand(NV_MIN2_I))
        Q.Add(CommandFactory.GetCommand(NV_HOME_SID_NID_I))
        Q.Add(CommandFactory.GetCommand(NV_MEID_I))
        Q.Add(CommandFactory.GetCommand(NV_LOCK_CODE_I))
        Q.Add(CommandFactory.GetCommand(DIAG_VERNO_F))

        Q.Run()

        DecodeMin()
    End Sub

    ''untested
    Function upOneDirectory(ByVal CurrPath As String) As String

        If CurrPath.Contains("/") Then
            Dim temp As String = ""

            '' str1 = "brew/mod"
            Dim str1 = CurrPath

            For i = Len(str1) To 1 Step -1
                If Mid(str1, i, 1) = "/" Then
                    Exit For
                Else
                    temp = Mid(str1, i, 1) & temp
                End If
            Next i

            Return CurrPath.Replace("/" + temp, "")
        Else
            Return "/"
        End If

    End Function



    'Private Sub ReadFolder(ByVal folderName As String)
    '    Try
    '        ''not sure if i want these clears here
    '        FolderTreeView1.Nodes.Clear()
    '        FolderTreeView1.Nodes.Add("..")
    '        FileTreeView2.Nodes.Clear()

    '        EfsQc.ReadEfsFolderByName(folderName)
    '    Catch ex As Exception
    '        logger.addToLog("Efs read err 2: " + ex.ToString)
    '    End Try
    'End Sub

    'Private Sub ReloadDataSetup_Click(sender As System.Object, e As System.EventArgs) Handles ReloadDataSetup.Click
    '    Dim runScripts As Boolean = False

    '    Dim fd As New OpenFileDialog

    '    fd.Title = "Select a carrier .xml script"
    '    Dim result = fd.ShowDialog()
    '    If result = Windows.Forms.DialogResult.OK Then
    '        runScripts = True
    '    End If
    '    Dim CarrierXml As String = fd.FileName

    '    fd.Title = "Select a model .xml script"
    '    result = fd.ShowDialog()
    '    If result = Windows.Forms.DialogResult.OK Then
    '        runScripts = runScripts And True
    '    End If
    '    Dim ModelXml As String = fd.FileName

    '    If runScripts Then
    '        loadModel(ModelXml, loadCarrier(CarrierXml))
    '    End If

    'End Sub

    Function loadCarrier(FileName As String, dataMdn As String, dataMin As String) As Carrier
        Dim myCarrier As New Carrier(FileName, dataMdn, dataMin)
        Logger.Add("Loaded: " + myCarrier.Name + " " + myCarrier.Prl)
        Return myCarrier
    End Function
    Function loadModel(FileName As String, Carrier As Carrier, prlFilePath As String) As Model
        Dim myModel As New Model(FileName, Carrier, prlFilePath)
        Return myModel
    End Function

    Public Shared Sub updatePhoneFromViewModel()
        Try
            If (thePhone.Mdn <> thePhoneRxd.Mdn) Then
                WriteMdn(thePhone.Mdn)
            End If
            If (thePhone.Min <> thePhoneRxd.Min) Then
                cdmaTerm.WriteMin(thePhone.Min)
                Q.Run()
            End If
            If (thePhone.UserLock <> thePhoneRxd.UserLock) Then
                cdmaTerm.WriteNv(NV_LOCK_CODE_I, ASCIIEncoding.ASCII.GetBytes(thePhone.UserLock))
            End If
            If (thePhone.Sid <> thePhoneRxd.Sid Or thePhone.Nid <> thePhoneRxd.Nid) Then
                cdmaTerm.WriteSidAndNid(thePhone.Sid, thePhone.Nid)
            End If
            If (thePhone.NamLock <> thePhoneRxd.NamLock) Then
                cdmaTerm.WriteNamLock(thePhone.NamLock)
            End If
            If (thePhone.NumMipProfiles <> thePhoneRxd.NumMipProfiles) Then
                cdmaTerm.WriteNv(NV_DS_MIP_NUM_PROF_I, New Byte() {Integer.Parse(thePhone.NumMipProfiles)})
            End If
            If (thePhone.EnabledMipProfile <> thePhoneRxd.EnabledMipProfile) Then
                cdmaTerm.WriteNv(NV_DS_MIP_ENABLE_PROF_I, New Byte() {Integer.Parse(thePhone.EnabledMipProfile)})
            End If

            updateNvItemsFromViewModel()
        Catch ex As Exception
            Logger.Add("Update err: see log for more info", Logger.LogType.Msg)
            Logger.Add("updatePhoneFromViewModel err: " + ex.ToString)
        End Try
    End Sub

    Public Shared Sub updateNvItemsFromViewModel()
        ''For i As Integer = 0 To cdmaTerm.thePhone.NvItems.Count
        '' If (kvp.Value.Data <> cdmaTerm.thePhoneRxd.NvItems.Item(kvp.Key).Data) Then
        ''cdmaTerm.WriteNv(cdmaTerm.thePhone.NvItems, kvp.Value.Data)
        ''End If
        ''Next
        Dim listIterator As New Dictionary(Of NvItems.NvItems, Nv)(cdmaTerm.thePhone.NvItems)

        For Each kvp As KeyValuePair(Of NvItems.NvItems, Nv) In listIterator
            If (kvp.Value.Data <> cdmaTerm.thePhoneRxd.NvItems.Item(kvp.Key).Data) Then
                cdmaTerm.WriteNv(kvp.Key, kvp.Value.Data)
            End If
        Next
        Q.Run()
    End Sub

    ''thanks to http://www.whiterabbit.org/android/
    ''thanks to kbman http://www.howardforums.com/showthread.php/1593533-service-programming-on-the-moto-droid?p=14467348#post14467348
    Public Shared Sub UnlockMotoEvdo()
        cdmaTerm.WriteNv(8035, New Byte() {0})
    End Sub
    Public Shared Sub RelockMotoEvdo()
        cdmaTerm.WriteNv(8035, New Byte() {1})
    End Sub

End Class
