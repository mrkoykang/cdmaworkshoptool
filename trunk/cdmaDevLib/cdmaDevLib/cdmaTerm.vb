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
''8:40pm - 03/07/2011
''
''here goes nop
Imports System
Imports System.IO.Ports
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
''Imports System.Threading
Imports System.Xml
''test dll import for bb port
Imports winAPIcom
Imports System.Management
Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports cdmaDevLib.NvItems.NVItems
Imports cdmaDevLib.Qcdm.Cmd
Imports cdmaDevLib.Qcdm


Public Class cdmaTerm

    ''im WithEvents mySerialPort As SerialPort = New SerialPort
    Public Shared dispatchQ As dispatchQmanager = New dispatchQmanager
    Public Shared nvReadQ As dispatchQmanager = New dispatchQmanager
    Public Shared RamReadQ As dispatchQmanager = New dispatchQmanager
    Public Shared thePhone As Phone = New Phone

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
    ''check for com devices and populate combobox
    'Sub GetComs()


    '    ' Get a list of serial port names.
    '    Dim ports As String() = SerialPort.GetPortNames()

    '    ' Display each port name to the console.
    '    Dim port As String
    '    For Each port In ports
    '        Dim thisPortFound As Boolean = False
    '        For Each s As String In ComNumBox1.Items
    '            If s.Contains(port) Then
    '                thisPortFound = True
    '            End If
    '        Next

    '        If Not thisPortFound Then
    '            ComNumBox1.Items.Add(port)
    '        End If



    '    Next (port)
    'End Sub


    ''no se
    Public commandInProgress As String


    Dim rxBuff As String

    ''ajh7495 start 2
    ''called by myserialport event handler
    Private Sub ReceiveData()
        Try


            'Sub to Receive Data from the Serial Port, Will Run in a Thread
            ''Dim lstItem As ListViewItem
            Dim bRead As Integer
            Dim nRead As Integer
            Dim returnStr As String = ""
            Dim ascStr As String = ""

            ''Number of Bytes to read
            bRead = mySerialPort.BytesToRead
            Dim cData(bRead - 1) As Byte

            mySerialPort.Encoding = Encoding.GetEncoding(65001)

            ''Reading the Data
            nRead = mySerialPort.Read(cData, 0, bRead)
            For Each b As Byte In cData
                ''Ascii String
                ascStr += Chr(b)
                ''Hex String (Modified Padding, to intake compulsory 2 chars, mainly in case of 0)
                returnStr += Hex(b).PadLeft(2, "0")
            Next

            ''kludgy byte log
            logger.addToLog("RX: " + returnStr + vbNewLine + vbNewLine)

            logger.addToByteLog(returnStr)
            newCommandRxd = True

        Catch
            Throw New Exception("read error 1: rec data thread")

        End Try

    End Sub

    Dim mySDR As New SecretDecoderRing


    ''This is the one that works!
    '' MAKE THIS RECIEVE AN ARRAY AS BYTE AND TX TO PORT
    ''TODO: not needed?
    Public Sub sendTermCommand2(ByVal byteArrayToTransmit() As Byte)

        ''When send button clicked Clear the rx buffer
        rxBuff = ""


        If serialportType = "normal" Then
            Try
                ''If port is closed, then open it
                If mySerialPort.IsOpen = False Then mySerialPort.Open()
            Catch e As Exception
                Throw New Exception(mySerialPort.PortName + " err cant open the port: " + e.Message)
            End Try
        ElseIf serialportType = "blackberry" Then
            ''do nothin?

        End If


        Try
            ''kludgy byte log

            If (serialportType = "normal") Then

                logger.addToLog("TX1: " + biznytesToStrizings(byteArrayToTransmit) + vbNewLine + vbNewLine)
                mySerialPort.Write(byteArrayToTransmit, 0, byteArrayToTransmit.Length)

            ElseIf (serialportType = "blackberry") Then

                logger.addToLog("TX2: " + biznytesToStrizings(byteArrayToTransmit) + vbNewLine + vbNewLine)
                mySerialPort2.Write(byteArrayToTransmit)

                Dim rBuff(4000) As Byte
                Dim rxBuff(mySerialPort2.Read(rBuff)) As Byte

                For Each b As Byte In rxBuff
                    rxBuff(b) = rBuff(b)
                Next

                Dim returnStr As String = biznytesToStrizings(rxBuff)
                logger.addToLog("RX: " + returnStr + vbNewLine + vbNewLine)

                logger.addToByteLog(returnStr)
                newCommandRxd = True


            End If
        Catch e As Exception
            Throw New Exception("com error: device does not rx: " + e.Message)
        End Try

        'Pause for 800ms
        System.Threading.Thread.Sleep(200)

        'If the buffer is still empty then no data. End sub
        If rxBuff = "" Then GoTo ends

ends:
    End Sub

    ''TODO: not needed?
    Public Function sendTermCommand3(ByVal byteArrayToTransmit() As Byte) As Boolean

        ''When send button clicked Clear the rx buffer
        rxBuff = ""
        Try

            If serialportType = "normal" Then
                ''If port is closed, then open it
                If mySerialPort.IsOpen = False Then mySerialPort.Open()
            ElseIf serialportType = "blackberry" Then
                ''do nothin?

            End If



        Catch
            Throw New Exception("cant open the port")
            Return False
        End Try
        Try
            ''kludgy byte log
            If (serialportType = "normal") Then

                logger.addToLog("TX1: " + biznytesToStrizings(byteArrayToTransmit) + vbNewLine + vbNewLine)
                mySerialPort.Write(byteArrayToTransmit, 0, byteArrayToTransmit.Length)

            ElseIf (serialportType = "blackberry") Then
                Throw New Exception("bb transmit?")
                logger.addToLog("TX2: " + biznytesToStrizings(byteArrayToTransmit) + vbNewLine + vbNewLine)
                mySerialPort2.Write(byteArrayToTransmit)

                Dim rBuff(4000) As Byte
                Dim rxBuff(mySerialPort2.Read(rBuff)) As Byte

                For Each b As Byte In rxBuff
                    rxBuff(b) = rBuff(b)
                Next

                Dim returnStr As String = biznytesToStrizings(rxBuff)
                logger.addToLog("RX: " + returnStr + vbNewLine + vbNewLine)

                logger.addToByteLog(returnStr)
                newCommandRxd = True


            End If



        Catch
            Throw New Exception("com error: device does not rx1")
            Return False
        End Try

        'Pause for 800ms
        System.Threading.Thread.Sleep(200)

        'If the buffer is still empty then no data. End sub
        If rxBuff = "" Then GoTo ends

ends:
        Return True
    End Function


    ''uh.... no fckin clue
    '' events? wtf
    ''fook dis
    Friend WithEvents mySerialPort As SerialPort = New SerialPort

    Public Shared mySerialPort2 As New SerialCom("\\.\COM3", 9600, IO.Ports.StopBits.One, IO.Ports.Parity.None, 8) ''actual winapi port
    ''this is the AT return changed
    ''think in a farther program the box can just be replaced with a string VAR?

    'Private Sub AtReturnCmdBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AtReturnCmdBox.TextChanged


    '    Try
    '        ' An object storing the hex value

    '        '' test replace blank
    '        ''Dim HexValue As String = AtReturnCmdBox.Text.Replace("00", String.Empty)


    '        '' test replace kp
    '        ''TODO: BUG SEEMS TO CLIP LAST 30's ascii 0 when next to a 0000 block. 
    '        ''maybe better is a loop that checks each two char for 00 vs 30 31 32 3F etc
    '        ''Dim HexValue As String = AtReturnCmdBox.Text.Replace("00", "5F")
    '        ''UPDATE: FIXED BY ADDISON A WHILE AGO(pretty sure..)...


    '        Dim incomingString As String = AtReturnCmdBox.Text.Replace(" ", "")

    '        Dim HexValue As String = ""
    '        For i = 0 To incomingString.Length - 1
    '            If (incomingString.Substring(i, 2) = "00") Then
    '                HexValue += "5F"
    '                i += 1
    '            Else
    '                HexValue += incomingString.Substring(i, 2)
    '                i += 1
    '            End If

    '        Next


    '        ''original
    '        '' Dim HexValue As String = AtReturnCmdBox.Text

    '        ' An object storing the string value

    '        Dim StrValue As String = ""

    '        ' While there's still something to convert in the hex string

    '        While HexValue.Length > 0


    '            ' Use ToChar() to convert each ASCII value (two hex digits) to the actual character
    '            ''TODO test u i nt32 u i nt64
    '            StrValue += System.Convert.ToChar(System.Convert.ToUInt64(HexValue.Substring(0, 2), 16)).ToString()

    '            ' Remove from the hex object the converted value


    '            HexValue = HexValue.Substring(2, HexValue.Length - 2)
    '        End While

    '        ' Show what we just converted

    '        ''Throw new Exception(StrValue)

    '        convertToAsciiTextBox.Text = StrValue

    '    Catch ex As Exception
    '        convertToAsciiTextBox.Text = ("atretn cmd box err:" + ex.ToString)
    '        '' dispatchQ.interruptCommandQ()
    '    End Try




    'End Sub





    ''ajh7495 start 1
    ''event handler calls the receive data threaded sub
    Private Sub mySerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles mySerialPort.DataReceived
        'This event will Receive the data from the selected COM port..
        'If e.EventType = SerialData.Chars Then
        '    Dim thRec As New Thread(AddressOf ReceiveData)
        '    thRec.IsBackground = True
        '    thRec.Priority = ThreadPriority.Highest
        '    thRec.Start()
        '    Thread.Sleep(2)
        'End If




        If e.EventType = SerialData.Chars Then

            ReceiveData()
        End If
    End Sub


    Public Shared Sub connectSub(portName As String)


        Try
            ''ajh dg change 1 - need to check for port being opened already /
            '' dg added checkbox to test bb winapi dll

            serialportType = "blackberry" ''aka winApiCom

            ''todo: ok to nuke normal? i thinks
            If (serialportType = "normal") Then

                ' ''setup com port
                'mySerialPort.BaudRate = &H2580
                ' ''mySerialPort.BaudRate = &H38400
                'mySerialPort.DataBits = 8
                'mySerialPort.StopBits = StopBits.One
                'mySerialPort.PortName = portName
                'mySerialPort.ReadTimeout = -1
                'mySerialPort.WriteTimeout = -1
                'mySerialPort.ReceivedBytesThreshold = 1
                'mySerialPort.ParityReplace = &H3F
                'mySerialPort.NewLine = ChrW(10)
                'mySerialPort.ReadBufferSize = &H1000
                'mySerialPort.WriteBufferSize = &H800

            ElseIf (serialportType = "blackberry") Then

                ''start blackberry com port



                mySerialPort2.SetPort("\\.\" + portName) ''todo:untested?
                mySerialPort2.Open()
                ''ToolStripStatusLabel1.Text = "connect ok || Type : WinApiCom.dll || " + ("\\.\" + GetPlainPortNameFromFriendly(ComNumBox1.Text))
                portIsOpen = True
            End If

        Catch
            Throw New Exception("shootin blanks")
        End Try
    End Sub


#End Region

#Region "onFormOpenAndOnFormClose"
    ''coding koan: make it work, fix it later
    ' ''on form load
    'Private Sub cdmaTerm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        ''debugging?
    '        If debugMode = False Then
    '            TabControl1.Controls.Remove(TabPage4)
    '            TabControl1.Controls.Remove(DataSetup)

    '            LeftTabControl.Controls.Remove(EFS)
    '            LeftTabControl.Controls.Remove(CmdAndADB)

    '        End If


    '        If selectPRLComboBox.Text.StartsWith("cricKet") Then

    '            PictureBox1.Image = My.Resources.Resources.Cricket_Black1
    '        ElseIf selectPRLComboBox.Text.StartsWith("metro") Then
    '            PictureBox1.Image = My.Resources.Resources.Metro_Logo
    '        End If


    '        CheckForIllegalCrossThreadCalls = False

    '        ''try and save testing time AUTOLOAD
    '        ''run the check for coms and populate box sub
    '        GetComs()


    '        If (System.IO.Directory.Exists(Application.StartupPath + "\moto\")) Then
    '            MotoWebBrowser1.Navigate(Application.StartupPath + "\moto\")
    '        End If

    '        If (System.IO.Directory.Exists(Application.StartupPath + "\prl\")) Then

    '        End If

    '        If (System.IO.File.Exists(Application.StartupPath + "\16digitpass.txt")) Then
    '            SixteenDigitCodes.set16DigitPasswords(New String(Application.StartupPath + "\16digitpass.txt"))
    '            select16digitCodeBox.DataSource = New BindingSource(SixteenDigitCodes.get16DigitPasswords, Nothing)
    '            select16digitCodeBox.DisplayMember = "Key"
    '            select16digitCodeBox.ValueMember = "Value"

    '        End If

    '        qcCommandsCombo.DataSource = [Enum].GetValues(GetType(Qcdm.Cmd))
    '        nvItemsCombo.DataSource = [Enum].GetValues(GetType(NvItems.NVItems))

    '        ''set the default nv read mode
    '        readSPCTypeCombo.SelectedIndex = 0
    '        ''this stuff is good leave alone for now
    '        Try
    '            ''assign the box the first com found
    '            ComNumBox1.Text = ComNumBox1.Items.Item(ComNumBox1.Items.Count - 1)
    '        Catch
    '            Throw New Exception("no com devices found")
    '        End Try

    '    Catch ex As Exception
    '        Throw New Exception("Load error:" + e.ToString)
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

#Region "conversionRoutines"

    '' YAY!!! the internetz comes thru again
    '' http://programmerramblings.blogspot.com/2008/03/convert-hex-string-to-byte-array-and.html
    '' takes in the hex string and spits out the byte array
    Public Shared Function String_To_Bytes(ByVal strInput As String) As Byte()
        Try
            ' i variable used to hold position in string
            Dim i As Integer = 0
            ' x variable used to hold byte array element position
            Dim x As Integer = 0
            ' allocate byte array based on half of string length
            Dim bytes As Byte() = New Byte((strInput.Length) / 2 - 1) {}
            ' loop through the string - 2 bytes at a time converting
            '  it to decimal equivalent and store in byte array
            While strInput.Length > i + 1
                ''TODO TEST i nt32 vs int 64
                Dim lngDecimal As Long = Convert.ToInt64(strInput.Substring(i, 2), 16)
                bytes(x) = Convert.ToByte(lngDecimal)
                i = i + 2
                x += 1
            End While
            ' return the finished byte array of decimal values
            Return bytes
        Catch ex As Exception
            Throw New Exception("HexString to Byte() Conversion Error: Try Removing Spaces: " + ex.ToString)

            Return Diag01
        End Try
    End Function
    Shared Function biznytesToStrizings(ByVal byteInput() As Byte) As String
        Dim ascStr As String = ""
        Dim returnStr As String = ""
        Try

            For Each b As Byte In byteInput
                ascStr += Chr(b)        'Ascii String
                returnStr += Hex(b).PadLeft(2, "0")     'Hex String (Modified Padding, to intake compulsory 2 chars, mainly in case of 0)
            Next

        Catch ex As Exception
            Throw New Exception("biz err: " + ex.ToString)
        End Try
        ''returns "" if try catch fails
        Return (returnStr)
    End Function



#End Region

#Region "selectionRoutinesAndSelectors"
    Function scanAndListComs() As String
        ''run the check for coms 
        ' GetComs()

    End Function

    ''sub to try converting sp etc
    Sub SendA16digitCode(Send16DigitCodeTextbox As String, select16digitCodeBox As String)
        If Send16DigitCodeTextbox = "" Then
            ''try out the textbox parser

            Dim MySixteenDigitCodes As New SixteenDigitCodes()

            dispatchQ.addCommandToQ(
                New Command(
                    DIAG_PASSWORD_F,
                    String_To_Bytes(SixteenDigitCodes.get16DigitPassword(select16digitCodeBox)),
                    "16 digit password"
                    )
                )
            dispatchQ.executeCommandQ()

        Else
            If (Send16DigitCodeTextbox.Length <= 16) Then

                dispatchQ.addCommandToQ(
                    New Command(
                        DIAG_PASSWORD_F,
                        String_To_Bytes(Send16DigitCodeTextbox),
                        "Send custom 16 digit DIAG_PASSWORD_F"
                        )
                    )
                dispatchQ.executeCommandQ()
            End If
        End If

    End Sub


    Shared Sub readSpcFromPhone(ByVal spcType As String)
        ''first check which read type then go
        If spcType = "NV" Then

            dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_SEC_CODE_I, New Byte() {}, "readSPC DIAG_NV_READ_F NV_SEC_CODE_I"))


        ElseIf spcType = "HTC" Then

            dispatchQ.addCommandToQ(New Command(unlockHtcSuperSPC, "unlockHtcSuperSPC byte array method"))
            dispatchQ.addCommandToQ(New Command(readSPC_HTCMethod, "readSPC_HTCMethod byte array method"))
            dispatchQ.addCommandToQ(New Command(readSPC_nvMethod, "ReadSPC_NV", "readSPC_nvMethod byte array method"))

        ElseIf spcType = "LG" Then

            ''ajh7495 start 3
            dispatchQ.addCommandToQ(New Command(unlockLgNvMemory, "Unlock LG NV Memory"))
            dispatchQ.addCommandToQ(New Command(readSPC_LgMethod, "Read SPC _ LG Method")) '' needs response to complete - send
            dispatchQ.addCommandToQ(New Command(readSPC_LgMethod, "ReadSPC_LG", "Read SPC _ LG Method | True | ReadSPC_LG|"))
            ''dispatchQ.executeCommandQ()


        ElseIf spcType = "Samsung1" Then
            ''Throw new Exception("s1 method not here yet")
            ''TODO: Find sample, make decoder
            dispatchQ.addCommandToQ(New Command(readSPC_Samsung1Method, "ReadSPC_NV", "readSPC_Samsung1Method byte array method"))


        ElseIf spcType = "Samsung2" Then
            ''Throw new Exception("s2 method")
            ''TODO: Find sample, make decoder
            dispatchQ.addCommandToQ(New Command(readSPC_Samsung2Method, "ReadSPC_NV", "readSPC_Samsung2Method byte array method"))


        ElseIf spcType = "Kyocera" Then
            ''Throw new Exception("kyocera method")
            ''TODO: Find sample, make decoder
            dispatchQ.addCommandToQ(New Command(readSPC_Kyocera, "ReadSPC_NV", "readSPC_Kyocera byte array method"))


        ElseIf spcType = "EFS" Then
            '' Throw new Exception("efs method")

            dispatchQ.addCommandToQ(New Command(readSPC_EFSMethod_SubsytemCmd, "readSPC_EFSMethod_SubsytemCmd byte array method"))
            dispatchQ.addCommandToQ(New Command(readSPC_EFSMethod_EfsCmd, "readSPC_EFSMethod_EfsCmd byte array method"))

            ''insert decoder
        ElseIf spcType = "MetroPCS" Then

            dispatchQ.addCommandToQ(New Command(DIAG_ESN_F, "Read Esn For MetroCalc"))
            dispatchQ.executeCommandQ()


            Try
                Dim ajhBlackMagic As New metroCalc
                ''get esn from gui
                ''SPCTextbox.Text = ajhBlackMagic.MetroSPCcalc(ResultsListBox.Items(3))''todo: what to do with result in this design?
            Catch
                Throw New Exception("metro spc error")
            End Try

        End If

    End Sub


#End Region

    ''All the Buttons
#Region "Button Handlers"



    ''send a terminal command button
    'Private Sub SendTermButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendTermButton.Click
    '    ''TODO add routine to stip " " blank spaces
    '    ''which error right now
    '    ''testing term send w/ string to byte()

    '    Dim TermCommandWithoutSpaces As String = TermSendBox.Text.Replace(" ", String.Empty)
    '    ''sendTermCommand2(String_To_Bytes(TermCommandWithoutSpaces))

    '    dispatchQ.clearCommandQ()
    '    dispatchQ.addCommandToQ(New Command(String_To_Bytes(TermCommandWithoutSpaces), "Terminal"))
    '    dispatchQ.executeCommandQ()
    'End Sub



    'Private Sub ScanButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanButton.Click
    '    ComNumBox1.Items.Clear()

    '    Dim allTheNames As String = ""
    '    For Each comPort As COMPortInfo.COMPortInfo In COMPortInfo.COMPortInfo.GetCOMPortsInfo
    '        ''allTheNames += (comPort.Name + " " + comPort.Description + " / ")

    '        ComNumBox1.Items.Add(comPort.Name + " " + comPort.Description)
    '    Next
    '    scanAndListComs()

    '    ''Throw new Exception("Ports available: " + allTheNames)


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
    '        '    Throw new Exception("no more b")
    '        'End Try
    '    Catch
    '        Throw New Exception("Cant Open AT Port")
    '    End Try

    'End Sub

    Shared Sub sendATCommand(ByVal atCommand As String)
        Try
            '            ''SEND AN AT COMMAND
            '            'When Tx button clicked
            '            'Clear buffer
            '            rxBuff = ""

            '            'If port is closed, then open it
            '            If mySerialPort.IsOpen = False Then mySerialPort.Open()

            '            'Write this data to port
            '            mySerialPort.Write(atCommand & vbCr)

            '            'Pause for 800ms
            '            System.Threading.Thread.Sleep(200)

            '            'If the port is open, then close it
            '            If mySerialPort.IsOpen = True Then mySerialPort.Close()

            '            'If the buffer is still empty then no data. End sub
            '            If rxBuff = "" Then GoTo ends

            '            'Else display the recieved data in the RichTextBox
            '            logger.addToByteLog(rxBuff)
            '        Catch
            '            Throw new Exception("Cant Open AT Port")
            '        End Try
            'ends:

            Dim myDm As New DmPort
            Dim atCmd As New List(Of Byte)
            For Each c As Char In atCommand
                atCmd.Add(System.Convert.ToUInt32(c))
            Next

            atCmd.Add(&HD)
            atCmd.Add(&HA)

            logger.addToByteLog(biznytesToStrizings(myDm.WriteRead(atCmd.ToArray)))

            ''Pause for 200ms
            'System.Threading.Thread.Sleep(200)
            'Try
            '    AtReturnCmdBox.AppendText(mySerialPort2.ReadString(&H1000))
            'Catch
            '    Throw new Exception("no more b")
            'End Try
        Catch
            Throw New Exception("Cant Open AT Port")
        End Try



    End Sub

    'Private Sub modeOfflineButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modeSwitchButton.Click
    '    dispatchQ.clearCommandQ()
    '    modeSwitch(modeSwitchCombo.Text)
    '    dispatchQ.executeCommandQ()

    '    If modeSwitchCombo.Text = "Reset" Then

    '        ''Attempt to fix crazy BSOD
    '        ''Driver unloaded without cancelling pending operations?
    '        ''TODO: wtf. maybe the bsod was a fluke.. not even sure if this does anything. madness.
    '        While (dispatchQ.GetCount > 0)
    '            System.Threading.Thread.Sleep(150)
    '        End While

    '        Try
    '            mySerialPort2.Flush()
    '            mySerialPort2.Dispose()

    '            ConnectButton.Enabled = True
    '            disconnectPortButton.Enabled = False
    '            Throw New Exception("Phone has been reset, port has been disconnected. Reconnect when the phone powers back on")
    '        Catch ex As Exception
    '            Throw New Exception("Mode reset disconnect err: " + ex.ToString)
    '        End Try
    '    End If

    'End Sub




#End Region
    ''All the commands
#Region "CommandsAsByteArrays"

    ''BEGIN THE RANDOM ARRAYS FOR COMMANDS N SHIT
    ''key presses
    Dim keyPress_Pound() As Byte = {&H20, &H0, &H23, &H6E, &HD6, &H7E}
    Dim keyPress_Star() As Byte = {&H20, &H0, &H2A, &HAF, &H4B, &H7E}
    Dim keyPress_0() As Byte = {&H20, &H0, &H30, &H74, &HF4, &H7E}
    Dim keyPress_1() As Byte = {&H20, &H0, &H31, &HFD, &HE5, &H7E}
    Dim keyPress_2() As Byte = {&H20, &H0, &H32, &H66, &HD7, &H7E}
    Dim keyPress_3() As Byte = {&H20, &H0, &H33, &HEF, &HC6, &H7E}
    Dim keyPress_4() As Byte = {&H20, &H0, &H34, &H50, &HB2, &H7E}
    Dim keyPress_5() As Byte = {&H20, &H0, &H35, &HD9, &HA3, &H7E}
    Dim keyPress_6() As Byte = {&H20, &H0, &H36, &H42, &H91, &H7E}
    Dim keyPress_7() As Byte = {&H20, &H0, &H37, &HCB, &H80, &H7E}
    Dim keyPress_8() As Byte = {&H20, &H0, &H38, &H3C, &H78, &H7E}
    Dim keyPress_9() As Byte = {&H20, &H0, &H39, &HB5, &H69, &H7E}
    Dim keyPress_SEND_UP() As Byte = {&H20, &H0, &H50, &H72, &H97, &H7E}
    Dim keyPress_END_DN() As Byte = {&H20, &H0, &H51, &HFB, &H86, &H7E}

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

    ''send prl 1030
    Public Shared ReadOnly sendPRL_1030_Part0() As Byte = {&H48, &H0, &H1, &H0, &HC0, &H3, &H1, &H3E, &H4, &H6, &H80, &H81, &HC0, &H37, &H61, &H94, &H52, &HBC, &H5D, &HD8, &HC5, &H14, &HAF, &H17, &H75, &HAA, &HBB, &H98, &H3B, &H20, &HA2, &H95, &HE2, &HEF, &HC, &HE2, &H64, &H65, &H8F, &HD2, &H5D, &H86, &HB5, &H57, &H73, &H7, &H61, &HB3, &H96, &HA4, &HDA, &HD8, &HAE, &H75, &HDB, &H3C, &HF7, &HD1, &H0, &H58, &HB0, &HCE, &H26, &H46, &H58, &HFD, &H25, &HCA, &H85, &H10, &H8, &H2, &H44, &H74, &H80, &H60, &H12, &H25, &H64, &H3, &H0, &H92, &HA3, &H20, &H14, &H4, &HAE, &H91, &H0, &H80, &H25, &H7C, &H88, &H4, &H1, &H24, &HAA, &H40, &H30, &H9, &H13, &HD2, &H1, &H40, &H49, &H7C, &H90, &HC, &H2, &H46, &H74, &H80, &H50, &H12, &H36, &HE4, &H2, &H80, &H96, &H7C, &H20, &H14, &H4, &H94, &H39, &H0, &H1, &H85, &H7E}
    Public Shared ReadOnly sendPRL_1030_Part1() As Byte = {&H48, &H1, &H1, &H0, &HC0, &H3, &HA0, &H26, &H2C, &HC8, &H5, &H1, &H24, &H76, &H40, &H30, &H9, &H33, &HD2, &H1, &H80, &H49, &H64, &H90, &HC, &H2, &H5C, &H68, &H80, &H40, &H13, &H25, &H24, &H2, &H80, &H94, &H35, &H20, &H14, &H4, &HC3, &H49, &H0, &HA0, &H26, &H43, &HC8, &H5, &H1, &H2A, &HA4, &H40, &H20, &H9, &H5A, &H42, &H1, &H0, &H4A, &HE7, &H10, &H8, &H2, &H4C, &HEC, &H80, &H60, &H12, &H6C, &H24, &H3, &H0, &H93, &HA3, &H20, &H14, &H4, &H9A, &HF9, &H0, &HA0, &H26, &H49, &HC8, &H5, &H1, &H21, &HCA, &H40, &H28, &H9, &H18, &HF2, &H1, &H40, &H4C, &H6E, &H90, &HA, &H2, &H63, &H8C, &H80, &H50, &H12, &H6D, &HA4, &H3, &H0, &H93, &H9B, &H20, &H14, &H4, &HC8, &HC9, &H0, &HA0, &H25, &H6, &H48, &H6, &H1, &H29, &HE2, &H40, &H4C, &H84, &H7E}
    Public Shared ReadOnly sendPRL_1030_Part2() As Byte = {&H48, &H2, &H0, &H0, &H70, &H2, &H18, &H9, &H54, &HA2, &H1, &H0, &H4B, &H9C, &H90, &H6, &H2, &H4F, &HC4, &H80, &H50, &H12, &H7F, &HA4, &H3, &H0, &H98, &H10, &H20, &H10, &H4, &H9D, &H59, &H0, &HC0, &H25, &H61, &H8, &H4, &H1, &H21, &H14, &H40, &H10, &H9, &HC, &H42, &H0, &H80, &H48, &HD3, &H10, &H0, &H2, &H47, &H5C, &H80, &H20, &H12, &H44, &HE4, &H1, &H0, &H92, &H44, &H20, &H0, &H4, &H93, &H41, &H0, &H0, &H24, &HBC, &H48, &H2, &H1, &H27, &H2C, &H40, &H0, &H8, &H42, &HD1, &H5, &H1, &H21, &HCA, &H40, &H28, &H9, &H18, &HF2, &H1, &H40, &H4C, &H6E, &H90, &HA, &H2, &H63, &H8C, &H80, &H50, &H12, &H6D, &HA4, &H3, &H0, &H93, &H9B, &H20, &H14, &H4, &HC8, &HC9, &H0, &HA0, &H25, &H6, &H48, &H6, &H1, &H29, &HE2, &H40, &HF6, &H23, &H7E}

    ''send prl 2004
    Public Shared ReadOnly sendPRL_2004_Part0() As Byte = {&H48, &H0, &H1, &H0, &HC0, &H3, &HB, &HE9, &H7, &HD4, &H3, &H0, &H4, &H0, &H0, &HF2, &H0, &HB, &H2, &H9, &H77, &H6, &H9, &H31, &H45, &H2B, &HCC, &HE5, &HA9, &H36, &HB2, &HEE, &H6, &H10, &H5B, &H9D, &H92, &HEE, &HDA, &H3F, &H3C, &HF8, &HCA, &HFA, &H22, &H64, &H1, &H86, &H70, &H68, &H6, &H5, &H1A, &HD5, &H5D, &HCC, &H1C, &H4, &H1, &H40, &H4, &H1, &H0, &H1, &H1, &H40, &H1, &H1, &H0, &H2, &H1, &H70, &H2, &H1, &H30, &H6, &H7, &H21, &H45, &H35, &H27, &H9, &H38, &H80, &H6, &HF, &H51, &HA9, &H38, &H47, &H6C, &H4B, &H25, &H85, &H46, &H5D, &HE0, &HC, &H65, &H8F, &HC0, &H6, &HC, &H41, &H45, &H5A, &HAC, &HE4, &H7D, &H5D, &H11, &H30, &H32, &H57, &H8B, &HB8, &H6, &H8, &H2B, &H39, &H6A, &H4D, &HAE, &H0, &HC7, &HE0, &H6, &H75, &HFB, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part1() As Byte = {&H48, &H1, &H1, &H0, &HC0, &H3, &H4, &H10, &H96, &HF, &HA0, &H6, &HB, &H39, &HA9, &H38, &H47, &H6C, &HFA, &H20, &HD4, &H4C, &H8F, &HC0, &H38, &HC0, &H0, &H0, &H1, &H80, &HA0, &H40, &H70, &H8, &H32, &HCE, &H3, &H1, &H40, &H30, &H60, &H20, &H4, &H4C, &H8E, &H30, &H60, &H20, &H2, &HAC, &H8E, &H30, &H60, &H50, &H3, &H44, &H8E, &H30, &H60, &H20, &H3, &H44, &H8E, &H30, &H60, &H28, &H4, &H22, &H8E, &H30, &H60, &H28, &HD, &H4E, &H8E, &H30, &H60, &H28, &HEF, &H56, &H8E, &H30, &H60, &H20, &H1, &HB8, &H8E, &H30, &H60, &H20, &H3, &H70, &H8E, &H30, &H70, &H20, &H8, &H44, &H8E, &H30, &H60, &H70, &H20, &H2E, &H8E, &H30, &H40, &H10, &H27, &HEE, &H2, &H30, &H60, &H10, &H21, &H6E, &H2, &H30, &H60, &H10, &H24, &H1A, &H2, &H30, &H60, &H10, &H2B, &HB3, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part2() As Byte = {&H48, &H2, &H1, &H0, &HC0, &H3, &H27, &H52, &H2, &H30, &H60, &H10, &H21, &HEE, &H2, &H30, &H70, &H10, &H22, &H56, &H2, &H30, &H60, &H10, &H30, &HB0, &H2, &H30, &H60, &H18, &H30, &HB0, &H2, &H30, &H60, &H10, &H21, &HE2, &H2, &H30, &H70, &H10, &H25, &H3A, &H2, &H30, &H60, &H28, &H3, &H86, &H8E, &H30, &H70, &H20, &H3, &H10, &H8E, &H30, &H60, &H58, &H20, &HAC, &H8E, &H30, &H60, &H58, &H0, &H18, &H8E, &H30, &H60, &H20, &H0, &H18, &H8E, &H40, &H70, &H18, &H32, &HD0, &H3, &H2, &H40, &H30, &H70, &H78, &H20, &H20, &H2, &H30, &H60, &H20, &H1, &HE8, &H8E, &H30, &H60, &H20, &H1, &H78, &H8E, &H30, &H60, &H30, &H2, &H0, &H8E, &H30, &H60, &H30, &H2, &H8, &H8E, &H30, &H60, &H30, &HA, &H74, &H8E, &H30, &H60, &H30, &HA, &H68, &H8E, &H30, &H5C, &HED, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part3() As Byte = {&H48, &H3, &H1, &H0, &HC0, &H3, &H60, &H30, &H1, &HB0, &H8E, &H30, &H60, &H30, &H3, &H80, &H8E, &H30, &H60, &H30, &H2, &HEC, &H8E, &H30, &H60, &H30, &H3, &H4, &H8E, &H30, &H60, &H30, &H1, &H78, &H8E, &H30, &H70, &H30, &HA, &H70, &H8E, &H30, &H60, &H28, &H0, &H2A, &H8E, &H30, &H60, &H48, &H0, &H2A, &H8E, &H30, &H60, &H18, &H0, &H2A, &H8E, &H30, &H60, &H10, &H0, &H2A, &H8E, &H30, &H60, &H60, &HD, &HAE, &H8E, &H30, &H40, &H10, &H22, &H4E, &H2, &H30, &H60, &H68, &H2A, &H94, &H2, &H30, &H60, &H10, &H24, &HFA, &H2, &H30, &H70, &H10, &H25, &H26, &H2, &H30, &H60, &H68, &H2A, &H3C, &H2, &H30, &H60, &H10, &H27, &HFA, &H2, &H30, &H60, &H18, &H28, &H9A, &H2, &H30, &H60, &H10, &H32, &HD2, &H2, &H30, &H60, &H18, &H22, &H6, &H2, &H30, &H1F, &HDD, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part4() As Byte = {&H48, &H4, &H1, &H0, &HC0, &H3, &H70, &H10, &H32, &HD6, &H2, &H30, &H60, &H20, &H1, &H4, &H8E, &H30, &H60, &H20, &H0, &H6C, &H8E, &H30, &H60, &H20, &H0, &HFC, &H8E, &H30, &H70, &H20, &HC, &H4C, &H8E, &H30, &H60, &H50, &H0, &H2A, &H8E, &H30, &H60, &H58, &H0, &H2A, &H8E, &H30, &H60, &H28, &H0, &H2A, &H8E, &H30, &H60, &H68, &H0, &H2A, &H8E, &H30, &H60, &H68, &H0, &H92, &H8E, &H30, &H40, &H10, &H25, &H9E, &H2, &H30, &H60, &H10, &H27, &HEA, &H2, &H30, &H70, &H10, &H24, &H5A, &H2, &H30, &H60, &H20, &H0, &H54, &H8E, &H30, &H60, &H20, &H2, &HB8, &H8E, &H30, &H60, &H20, &H2, &HA8, &H8E, &H30, &H70, &H18, &H2C, &H46, &H8E, &H30, &H60, &H58, &H20, &H70, &H8E, &H30, &H60, &H58, &H20, &HC0, &H8E, &H30, &H60, &H58, &H20, &H54, &H8E, &H30, &HD2, &HC, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part5() As Byte = {&H48, &H5, &H1, &H0, &HC0, &H3, &H40, &H10, &H27, &H5A, &H2, &H30, &H70, &H10, &H27, &H1E, &H2, &H30, &H60, &H10, &H26, &HDA, &H2, &H30, &H70, &H10, &H28, &H32, &H2, &H30, &H70, &H28, &H8, &H46, &H2, &H30, &H60, &H20, &H0, &H50, &H8E, &H30, &H60, &H20, &H1, &H44, &H8E, &H30, &H60, &H20, &H0, &HE0, &H8E, &H30, &H60, &H20, &H8, &H48, &H8E, &H30, &H60, &H40, &H8, &H70, &H8E, &H30, &H60, &H20, &H1, &HC8, &H8E, &H30, &H50, &H10, &H21, &H62, &H2, &H30, &H60, &H10, &H25, &H46, &H2, &H30, &H60, &H10, &H22, &H7A, &H2, &H30, &H60, &H10, &H27, &H72, &H2, &H30, &H60, &H68, &H32, &HE4, &H2, &H30, &H60, &H10, &H22, &H3A, &H2, &H30, &H60, &H10, &H24, &HAA, &H2, &H30, &H60, &H10, &H32, &HF4, &H2, &H30, &H60, &H10, &H32, &HF6, &H2, &H30, &H93, &HAF, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part6() As Byte = {&H48, &H6, &H1, &H0, &HC0, &H3, &H60, &H68, &H2A, &H14, &H2, &H30, &H60, &H68, &H2B, &HA4, &H2, &H30, &H70, &H10, &H25, &HF2, &H2, &H30, &H60, &H28, &HC, &HD6, &H2, &H30, &H60, &H20, &H0, &HD0, &H2, &H30, &H60, &H20, &HD, &H1C, &H2, &H30, &H60, &H28, &H2, &H6A, &H2, &H30, &H60, &H28, &H1, &HEC, &H2, &H30, &H60, &H28, &HC, &HA, &H2, &H30, &H70, &H20, &HB, &HDC, &H2, &H30, &H60, &H20, &H3, &H78, &H8E, &H30, &H60, &H20, &H1, &H98, &H8E, &H30, &H60, &H20, &H4, &H10, &H8E, &H30, &H60, &H20, &H0, &HA8, &H8E, &H30, &H60, &H28, &HA, &HCE, &H8E, &H30, &H60, &H30, &H2, &H5C, &H8E, &H30, &H60, &H30, &HE, &HDC, &H8E, &H30, &H60, &H20, &H0, &HE4, &H8E, &H30, &H60, &H20, &H1, &H1C, &H8E, &H30, &H60, &H20, &H2, &HE0, &H8E, &H30, &H17, &H7D, &H5E, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part7() As Byte = {&H48, &H7, &H1, &H0, &HC0, &H3, &H60, &H20, &H2, &HF0, &H8E, &H30, &H60, &H20, &H1, &H20, &H8E, &H30, &H60, &H20, &H1, &H38, &H8E, &H30, &H60, &H20, &H0, &HE8, &H8E, &H30, &H60, &H20, &H19, &H34, &H8E, &H30, &H60, &H20, &H1, &H6C, &H8E, &H30, &H60, &H20, &H2, &HBC, &H8E, &H30, &H60, &H20, &HC, &HE8, &H8E, &H30, &H60, &H20, &HC, &HD0, &H8E, &H30, &H60, &H20, &HC, &HE4, &H8E, &H30, &H60, &H20, &HC, &H8, &H8E, &H30, &H60, &H20, &H4, &H84, &H8E, &H30, &H60, &H20, &HB, &HFC, &H8E, &H30, &H60, &H20, &HC, &H0, &H8E, &H30, &H60, &H20, &HC, &H10, &H8E, &H30, &H60, &H20, &HB, &HF8, &H8E, &H30, &H60, &H20, &HC, &H4, &H8E, &H30, &H60, &H20, &HC, &H14, &H8E, &H30, &H60, &H20, &HC, &HC, &H8E, &H30, &H60, &H20, &HB, &HF4, &H8E, &H30, &H31, &HE5, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part8() As Byte = {&H48, &H8, &H1, &H0, &HC0, &H3, &H60, &H20, &HB, &HF0, &H8E, &H30, &H60, &H20, &H0, &H94, &H8E, &H30, &H60, &H20, &HF, &HEC, &H8E, &H30, &H60, &H20, &H0, &HC8, &H8E, &H30, &H70, &H20, &H8, &HF8, &H8E, &H30, &H60, &H28, &H0, &H52, &H8E, &H30, &H60, &H28, &H7, &HEE, &H8E, &H30, &H60, &H50, &H7, &HEE, &H8E, &H30, &H60, &H60, &H0, &HE2, &H8E, &H30, &H60, &H10, &H0, &HE2, &H8E, &H30, &H60, &H28, &H10, &H8E, &H8E, &H30, &H60, &H60, &H0, &HBA, &H8E, &H30, &H60, &H58, &H0, &HFE, &H8E, &H30, &H60, &H28, &H0, &HFE, &H8E, &H30, &H60, &H28, &H1, &H7A, &H8E, &H30, &H60, &H28, &H1, &H16, &H8E, &H30, &H60, &H58, &H0, &H8A, &H8E, &H30, &H60, &H28, &H0, &H8A, &H8E, &H30, &H60, &H28, &H0, &HBE, &H8E, &H30, &H60, &H58, &H0, &HBE, &H8E, &H30, &H84, &H70, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part9() As Byte = {&H48, &H9, &H1, &H0, &HC0, &H3, &H60, &H28, &H0, &HE2, &H8E, &H30, &H60, &H28, &H0, &HBA, &H8E, &H30, &H60, &H28, &H11, &HFA, &H8E, &H30, &H60, &H28, &H2, &H92, &H8E, &H30, &H60, &H28, &H2, &HF2, &H8E, &H30, &H60, &H28, &H2, &H56, &H8E, &H30, &H60, &H28, &H2, &HBA, &H8E, &H30, &H60, &H28, &H10, &H86, &H8E, &H30, &H60, &H28, &H8, &HD2, &H8E, &H30, &H60, &H28, &H1, &H66, &H8E, &H30, &H40, &H10, &H24, &H92, &H2, &H30, &H60, &H10, &H24, &H76, &H2, &H30, &H60, &H10, &H25, &H92, &H2, &H30, &H70, &H10, &H25, &HE, &H2, &H30, &H60, &H20, &H1, &HA0, &H8E, &H30, &H60, &H20, &H4, &H44, &H8E, &H30, &H60, &H20, &H3, &HBC, &H8E, &H30, &H60, &H28, &HA, &HBE, &H8E, &H30, &H60, &H68, &HA, &HBE, &H8E, &H30, &H70, &H20, &H8, &H1C, &H8E, &H30, &HF1, &H7A, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartA() As Byte = {&H48, &HA, &H1, &H0, &HC0, &H3, &H60, &H18, &H1, &H1E, &H8E, &H30, &H60, &H28, &H1, &H1E, &H8E, &H30, &H60, &H10, &HD, &HA8, &H8E, &H30, &H40, &H10, &H32, &HDA, &H2, &H30, &H70, &H68, &H32, &HDA, &H2, &H30, &H70, &H10, &H24, &HF2, &H2, &H30, &H70, &H10, &H22, &HFA, &H2, &H30, &H60, &H20, &H0, &H4, &H8E, &H30, &H60, &H20, &H0, &H8, &H8E, &H30, &H60, &H20, &H0, &H80, &H8E, &H30, &H60, &H20, &H1, &HC8, &H8E, &H30, &H40, &H10, &H23, &H6E, &H2, &H30, &H60, &H10, &H32, &H52, &H2, &H30, &H70, &H10, &H23, &H3A, &H2, &H30, &H70, &H68, &HA8, &H8E, &H2, &H30, &H60, &H20, &H9, &HF0, &H2, &H30, &H60, &H28, &HB, &H6, &H2, &H30, &H60, &H20, &H1, &H4C, &H2, &H30, &H60, &H28, &HC, &H76, &H2, &H30, &H60, &H58, &H23, &H12, &H2, &H30, &H52, &H73, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartB() As Byte = {&H48, &HB, &H1, &H0, &HC0, &H3, &H60, &H58, &H26, &H92, &H2, &H30, &H60, &H68, &H2E, &H28, &H2, &H30, &H60, &H68, &H2F, &HCC, &H2, &H30, &H70, &H20, &H4, &H7C, &H2, &H30, &H60, &H20, &H2, &HAC, &H8E, &H30, &H60, &H28, &H1, &H52, &H8E, &H30, &H70, &H28, &H4, &H22, &H8E, &H30, &H60, &H68, &HA, &H54, &H8E, &H30, &H40, &H10, &H26, &H76, &H2, &H30, &H70, &H10, &H32, &H4E, &H2, &H30, &H60, &H8, &H28, &HB4, &H2, &H30, &H60, &H28, &H0, &H6A, &H8E, &H30, &H60, &H28, &HD, &H96, &H8E, &H30, &H70, &H28, &H8, &HA, &H8E, &H30, &H60, &H20, &H0, &H60, &H8E, &H30, &H60, &H20, &H1, &H18, &H8E, &H30, &H60, &H20, &H8, &H14, &H8E, &H30, &H60, &H20, &H8, &H4, &H8E, &H30, &H60, &H20, &H8, &H8, &H8E, &H30, &H60, &H20, &H8, &HC, &H8E, &H30, &HFE, &H5, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartC() As Byte = {&H48, &HC, &H1, &H0, &HC0, &H3, &H40, &H20, &H0, &H38, &H8E, &H30, &H60, &H60, &H0, &H38, &H8E, &H30, &H60, &H58, &H0, &H38, &H8E, &H30, &H40, &H20, &H0, &H2C, &H8E, &H30, &H60, &H20, &H10, &H14, &H8E, &H30, &H60, &H20, &H3, &HCC, &H8E, &H30, &H60, &H20, &H1, &H34, &H8E, &H30, &H60, &H20, &H0, &HAC, &H8E, &H30, &H60, &H20, &H2, &H14, &H8E, &H30, &H60, &H20, &H0, &H9C, &H8E, &H30, &H60, &H20, &HB, &HC8, &H8E, &H30, &H40, &H18, &H29, &HE2, &H2, &H30, &H60, &H18, &H2E, &HEA, &H2, &H30, &H70, &H10, &H27, &HE2, &H2, &H30, &H60, &H68, &H29, &HDC, &H2, &H30, &H70, &H10, &H29, &HDC, &H2, &H30, &H60, &H28, &H8, &H9A, &H8E, &H30, &H60, &H20, &H3, &H50, &H8E, &H30, &H60, &H20, &H0, &H94, &H8E, &H30, &H60, &H28, &H0, &HA6, &H8E, &H30, &HE9, &H3B, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartD() As Byte = {&H48, &HD, &H1, &H0, &HC0, &H3, &H70, &H28, &HD, &HBE, &H8E, &H30, &H60, &H20, &H0, &H10, &H8E, &H30, &H60, &H20, &H1, &HF4, &H8E, &H30, &H60, &H20, &H0, &H70, &H8E, &H30, &H60, &H20, &H0, &H24, &H8E, &H30, &H60, &H60, &H0, &HEE, &H8E, &H30, &H60, &H28, &H0, &HEE, &H8E, &H30, &H60, &H20, &H2, &H58, &H8E, &H30, &H60, &H28, &H2, &H86, &H8E, &H30, &H60, &H10, &H3, &HEC, &H8E, &H30, &H60, &H20, &H3, &HEC, &H8E, &H30, &H60, &H68, &H3, &HEC, &H8E, &H30, &H60, &H10, &HD, &H80, &H8E, &H30, &H60, &H20, &HB, &HCC, &H8E, &H30, &H60, &H20, &HB, &HD8, &H8E, &H30, &H60, &H20, &HB, &HC4, &H8E, &H30, &H60, &H28, &HD, &HAA, &H8E, &H30, &H60, &H68, &HD, &HAA, &H8E, &H30, &H60, &H20, &H0, &H2C, &H8E, &H30, &H60, &H20, &H0, &H9C, &H8E, &H30, &H1A, &HFA, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartE() As Byte = {&H48, &HE, &H1, &H0, &HC0, &H3, &H60, &H20, &H0, &HAC, &H8E, &H30, &H60, &H20, &H1, &H34, &H8E, &H30, &H60, &H20, &H1, &HC4, &H8E, &H30, &H60, &H20, &H3, &H28, &H8E, &H30, &H60, &H20, &H3, &HCC, &H8E, &H30, &H60, &H20, &H10, &H14, &H8E, &H30, &H60, &H50, &H1, &H54, &H8E, &H30, &H60, &H20, &H1, &H54, &H8E, &H30, &H60, &H20, &HE, &HF0, &H8E, &H30, &H50, &H18, &H2E, &H72, &H2, &H30, &H70, &H10, &H32, &HD6, &H2, &H30, &H70, &H20, &H0, &HFC, &H8E, &H30, &H60, &H20, &H0, &H40, &H8E, &H30, &H60, &H68, &H0, &H40, &H8E, &H30, &H60, &H20, &H0, &HC0, &H8E, &H30, &H40, &H10, &H24, &HA, &H2, &H30, &H70, &H8, &H24, &HA, &H2, &H30, &H60, &H20, &H0, &H78, &H8E, &H30, &H50, &H10, &H21, &HCA, &H2, &H30, &H60, &H28, &H9, &H2A, &H8E, &H30, &HC3, &H6D, &H7E}
    Public Shared ReadOnly sendPRL_2004_PartF() As Byte = {&H48, &HF, &H1, &H0, &HC0, &H3, &H70, &H28, &H9, &H1A, &H8E, &H30, &H60, &H20, &H2, &H20, &H8E, &H30, &H60, &H20, &H9, &H18, &H8E, &H30, &H60, &H20, &H9, &H2C, &H8E, &H30, &H50, &H10, &H27, &H46, &H2, &H30, &H60, &H28, &HD, &H96, &H8E, &H30, &H70, &H48, &HD, &H9A, &H8E, &H30, &H60, &H20, &H0, &HBC, &H8E, &H30, &H60, &H20, &HD, &H98, &H8E, &H30, &H40, &H10, &H2C, &HF8, &H2, &H30, &H70, &H68, &H2E, &H34, &H2, &H30, &H60, &H18, &H2E, &H2E, &H2, &H30, &H60, &H20, &H2, &H54, &H2, &H30, &H60, &H20, &H3, &H0, &H2, &H30, &H60, &H28, &H9, &H7D, &H5E, &H2, &H30, &H60, &H28, &HA, &HEE, &H2, &H30, &H60, &H28, &HA, &HF6, &H2, &H30, &H60, &H20, &HA, &HFC, &H2, &H30, &H60, &H28, &HB, &H16, &H2, &H30, &H60, &H28, &HB, &H1E, &H2, &H30, &HB0, &H94, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part10() As Byte = {&H48, &H10, &H1, &H0, &HC0, &H3, &H60, &H28, &HB, &H22, &H2, &H30, &H60, &H28, &H10, &HBA, &H2, &H30, &H60, &H10, &H32, &H26, &H2, &H30, &H60, &H60, &H32, &HA6, &H2, &H30, &H60, &H68, &H32, &HB0, &H2, &H30, &H60, &H28, &HB, &H72, &H2, &H30, &H60, &H28, &H1, &H82, &H2, &H30, &H60, &H28, &H1, &H86, &H2, &H30, &H60, &H28, &H2, &H5E, &H2, &H30, &H60, &H28, &H2, &H96, &H2, &H30, &H60, &H28, &H4, &H9A, &H2, &H30, &H60, &H28, &H9, &H76, &H2, &H30, &H60, &H28, &H9, &H7A, &H2, &H30, &H60, &H68, &H2E, &HEC, &H2, &H30, &H60, &H28, &H9, &H86, &H2, &H30, &H60, &H28, &H9, &H8E, &H2, &H30, &H60, &H28, &H9, &H9A, &H2, &H30, &H60, &H28, &H9, &HA6, &H2, &H30, &H60, &H28, &H9, &HAA, &H2, &H30, &H60, &H28, &H9, &HAE, &H2, &H30, &HB5, &H3D, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part11() As Byte = {&H48, &H11, &H1, &H0, &HC0, &H3, &H70, &H28, &H3, &HA, &H2, &H30, &H60, &H28, &H4, &H56, &H8E, &H30, &H60, &H20, &H9, &HA8, &H8E, &H30, &H60, &H20, &H1, &H30, &H8E, &H30, &H60, &H28, &H3, &HFE, &H8E, &H30, &H60, &H30, &H3, &H40, &H8E, &H30, &H60, &H30, &HB, &H74, &H8E, &H30, &H70, &H20, &HE, &H34, &H8E, &H30, &H60, &H20, &H1, &H2C, &H8E, &H30, &H60, &H20, &H4, &H20, &H8E, &H30, &H60, &H20, &H2, &H3C, &H8E, &H30, &H60, &H28, &H1, &H12, &H8E, &H30, &H60, &H60, &H1, &H12, &H8E, &H30, &H60, &H60, &H3, &HC6, &H8E, &H30, &H60, &H60, &H4, &H20, &H8E, &H30, &H60, &H20, &H17, &HF4, &H8E, &H30, &H60, &H20, &H9, &H88, &H8E, &H30, &H60, &H20, &H1, &H74, &H8E, &H30, &H40, &H20, &H2, &H7C, &H8E, &H30, &H60, &H20, &H1, &HE0, &H8E, &H30, &H69, &H2, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part12() As Byte = {&H48, &H12, &H1, &H0, &HC0, &H3, &H60, &H20, &HE, &H28, &H8E, &H30, &H70, &H20, &H3, &H18, &H8E, &H30, &H60, &H20, &H0, &H34, &H8E, &H30, &H60, &H60, &H0, &H34, &H8E, &H30, &H40, &H28, &H2, &HE6, &H8E, &H30, &H60, &H20, &HA, &HB8, &H8E, &H30, &H70, &H28, &H2, &H3A, &H8E, &H30, &H60, &H20, &H2, &H78, &H8E, &H30, &H60, &H20, &H2, &H94, &H8E, &H30, &H60, &H20, &HC, &H18, &H8E, &H30, &H60, &H60, &H2, &H94, &H8E, &H30, &H60, &H60, &H0, &H34, &H8E, &H30, &H50, &H10, &H32, &HD4, &H2, &H30, &H60, &H28, &H0, &HA, &H2, &H30, &H60, &H28, &H9, &H2E, &H2, &H30, &H60, &H20, &HE, &HF4, &H2, &H30, &H60, &H78, &H20, &H20, &H2, &H30, &H60, &H20, &H9, &H60, &H2, &H30, &H70, &H28, &H1, &HBA, &H2, &H30, &H60, &H20, &H0, &H28, &H8E, &H30, &H52, &H1, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part13() As Byte = {&H48, &H13, &H1, &H0, &HC0, &H3, &H60, &H50, &H0, &H28, &H8E, &H30, &H60, &H50, &H3, &HF4, &H8E, &H30, &H60, &H50, &H20, &H74, &H8E, &H30, &H60, &H20, &H4, &H28, &H8E, &H30, &H60, &H20, &H1, &HAC, &H8E, &H30, &H60, &H28, &H9, &H4A, &H8E, &H30, &H60, &H20, &H3, &HF4, &H8E, &H30, &H60, &H20, &H4, &H24, &H8E, &H30, &H60, &H50, &H4, &H24, &H8E, &H30, &H60, &H68, &H0, &HA0, &H8E, &H30, &H60, &H20, &H0, &HA0, &H8E, &H30, &H60, &H50, &H0, &HA0, &H8E, &H30, &H40, &H10, &H20, &H1C, &H8E, &H30, &H70, &H60, &H21, &H2, &H8E, &H30, &H60, &H28, &H0, &H82, &H8E, &H30, &H60, &H68, &H0, &H82, &H8E, &H30, &H60, &H20, &H1, &H7C, &H8E, &H30, &H50, &H10, &H28, &H6A, &H2, &H30, &H60, &H20, &H9, &HF0, &H2, &H30, &H60, &H28, &HB, &H6, &H2, &H30, &HE3, &H43, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part14() As Byte = {&H48, &H14, &H1, &H0, &HC0, &H3, &H60, &H20, &H1, &H4C, &H2, &H30, &H60, &H28, &HC, &H76, &H2, &H30, &H60, &H10, &H23, &H12, &H2, &H30, &H60, &H10, &H26, &H92, &H2, &H30, &H60, &H68, &H2E, &H28, &H2, &H30, &H60, &H68, &H2F, &HCC, &H2, &H30, &H70, &H20, &H4, &H7C, &H2, &H30, &H60, &H70, &H9, &HD4, &H8E, &H30, &H60, &H20, &H9, &HD4, &H8E, &H30, &H60, &H38, &H9, &HEE, &H8E, &H30, &H60, &H20, &H4, &H44, &H8E, &H30, &H60, &H20, &HA, &HF0, &H8E, &H30, &H70, &H30, &H9, &HD4, &H8E, &H30, &H60, &H60, &H0, &H76, &H8E, &H30, &H60, &H28, &H0, &H76, &H8E, &H30, &H60, &H60, &H0, &H22, &H8E, &H30, &H60, &H58, &H0, &H22, &H8E, &H30, &H60, &H28, &H0, &H22, &H8E, &H30, &H60, &H28, &H1, &H4A, &H8E, &H30, &H60, &H60, &HB, &H34, &H8E, &H30, &H70, &H85, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part15() As Byte = {&H48, &H15, &H1, &H0, &HC0, &H3, &H60, &H58, &HB, &H34, &H8E, &H30, &H60, &H20, &HB, &H0, &H8E, &H30, &H60, &H18, &HB, &H0, &H8E, &H30, &H50, &H28, &HB, &H42, &H8E, &H30, &H60, &H20, &H2, &HC, &H8E, &H30, &H50, &H10, &H32, &H32, &H2, &H30, &H60, &H60, &H1, &HBC, &H8E, &H30, &H60, &H20, &H1, &HBC, &H8E, &H30, &H60, &H20, &H9, &H18, &H8E, &H30, &H60, &H20, &HD, &HE0, &H8E, &H30, &H40, &H10, &H26, &HA2, &H2, &H30, &H60, &H10, &H31, &H66, &H2, &H30, &H70, &H10, &H27, &H36, &H2, &H30, &H60, &H20, &H4, &H88, &H2, &H30, &H60, &H28, &HD, &HFA, &H2, &H30, &H60, &H20, &HD, &HF0, &H2, &H30, &H60, &H20, &H3, &H68, &H2, &H30, &H60, &H28, &HC, &H8E, &H2, &H30, &H60, &H20, &HC, &H94, &H2, &H30, &H70, &H28, &HC, &H8E, &H2, &H30, &HAB, &H26, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part16() As Byte = {&H48, &H16, &H1, &H0, &HC0, &H3, &H60, &H20, &H0, &HC, &H8E, &H30, &H60, &H20, &H0, &H3C, &H8E, &H30, &H60, &H18, &H0, &H3C, &H8E, &H30, &H60, &H50, &H25, &H90, &H8E, &H30, &H60, &H20, &HC, &H90, &H8E, &H30, &H60, &H20, &H2, &H90, &H8E, &H30, &H40, &H28, &H0, &H72, &H8E, &H30, &H60, &H8, &H0, &H72, &H8E, &H30, &H60, &H28, &H0, &HAA, &H8E, &H30, &H60, &H20, &H1, &HA8, &H8E, &H30, &H60, &H20, &H2, &HE4, &H8E, &H30, &H60, &H20, &HEE, &H78, &H8E, &H30, &H60, &H20, &H3, &H78, &H8E, &H30, &H60, &H20, &H4, &H40, &H8E, &H30, &H60, &H20, &H0, &HF0, &H8E, &H30, &H60, &H8, &H3, &H78, &H8E, &H30, &H60, &H28, &H1, &H9A, &H8E, &H30, &H60, &H28, &HA, &HD2, &H8E, &H30, &H60, &H28, &HA, &HE2, &H8E, &H30, &H60, &H28, &H2, &H32, &H8E, &H30, &H39, &H7, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part17() As Byte = {&H48, &H17, &H1, &H0, &HC0, &H3, &H70, &H20, &H8, &HC8, &H8E, &H30, &H60, &H50, &H20, &H80, &H8E, &H30, &H60, &H28, &H2, &H92, &H8E, &H30, &H40, &H68, &H2A, &HA4, &H2, &H30, &H60, &H68, &H2B, &H48, &H2, &H30, &H60, &H10, &H22, &H6E, &H2, &H30, &H70, &H10, &H26, &HC2, &H2, &H30, &H70, &H10, &H23, &HAA, &H2, &H30, &H60, &H28, &H3, &HCE, &H8E, &H30, &H70, &H28, &H8, &H8A, &H8E, &H30, &H60, &H20, &H0, &H74, &H8E, &H30, &H60, &H20, &H3, &HD4, &H8E, &H30, &H60, &H20, &H8, &H8C, &H8E, &H30, &H60, &H20, &H8, &H7C, &H8E, &H30, &H60, &H20, &H8, &H80, &H8E, &H30, &H60, &H28, &H8, &H76, &H8E, &H30, &H40, &H10, &H30, &HD2, &H2, &H30, &H60, &H10, &H24, &HDA, &H2, &H30, &H60, &H10, &H32, &H1E, &H2, &H30, &H70, &H10, &H23, &HA, &H2, &H30, &H61, &HFD, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part18() As Byte = {&H48, &H18, &H1, &H0, &HC0, &H3, &H60, &H28, &H0, &H9E, &H8E, &H30, &H60, &H8, &H4, &H22, &H8E, &H30, &H60, &H18, &H4, &H22, &H8E, &H30, &H60, &H28, &HB, &HB6, &H8E, &H30, &H60, &H28, &HB, &HAA, &H8E, &H30, &H60, &H28, &H4, &H22, &H8E, &H30, &H70, &H28, &H0, &HC2, &H8E, &H30, &H60, &H20, &H0, &HB8, &H8E, &H30, &H60, &H20, &H0, &HDC, &H8E, &H30, &H60, &H20, &H8, &H8C, &H8E, &H30, &H40, &H28, &HE, &H42, &H8E, &H30, &H70, &H20, &H18, &H8, &H8E, &H30, &H60, &H20, &HE, &H4C, &H8E, &H30, &H60, &H20, &H2, &H38, &H8E, &H30, &H60, &H20, &HE, &H40, &H8E, &H30, &H60, &H20, &HE, &H44, &H8E, &H30, &H60, &H28, &HE, &H46, &H8E, &H30, &H40, &H28, &H8, &H5A, &H8E, &H30, &H40, &H20, &H9, &H50, &H8E, &H30, &H40, &H28, &HB, &H82, &H8E, &H30, &HC4, &HA3, &H7E}
    Public Shared ReadOnly sendPRL_2004_Part19() As Byte = {&H48, &H19, &H0, &H0, &H88, &H1, &H60, &H8, &H22, &H0, &H2, &H30, &H60, &H8, &H27, &H24, &H2, &H30, &H60, &H20, &HB, &H98, &H2, &H30, &H60, &H28, &H3, &H7A, &H2, &H30, &H60, &H28, &HA, &H4A, &H2, &H30, &H60, &H20, &HA, &H50, &H2, &H30, &H40, &H20, &HE, &H4, &H2, &H30, &H60, &H20, &HE, &H14, &H2, &HF9, &HA, &H20, &H0, &HDC, &H8E, &H30, &H60, &H20, &H8, &H8C, &H8E, &H30, &H40, &H28, &HE, &H42, &H8E, &H30, &H70, &H20, &H18, &H8, &H8E, &H30, &H60, &H20, &HE, &H4C, &H8E, &H30, &H60, &H20, &H2, &H38, &H8E, &H30, &H60, &H20, &HE, &H40, &H8E, &H30, &H60, &H20, &HE, &H44, &H8E, &H30, &H60, &H28, &HE, &H46, &H8E, &H30, &H40, &H28, &H8, &H5A, &H8E, &H30, &H40, &H20, &H9, &H50, &H8E, &H30, &H40, &H28, &HB, &H82, &H8E, &H30, &HE8, &H6B, &H7E}

    ''send prl 1001
    Public Shared ReadOnly sendPRL_1001_Part0() As Byte = {&H48, &H0, &H1, &H0, &HC0, &H3, &H7, &H2A, &H3, &HE9, &H0, &H3, &H1, &H7A, &H65, &HB9, &HD9, &H2E, &HED, &HA3, &HF3, &HCF, &H8C, &HAF, &HA2, &H26, &H40, &H18, &H67, &H6, &H98, &HC5, &H14, &HAF, &H17, &H76, &H72, &HD4, &H9B, &H5B, &HD, &H6A, &HAE, &HE6, &HE, &H8A, &H4, &HE4, &H6C, &H42, &H8A, &H6A, &H4E, &H12, &H71, &H64, &H1A, &H93, &H84, &H76, &HD2, &HC2, &HA3, &H80, &H31, &H96, &H3F, &H32, &HA, &H2A, &HD5, &H67, &H23, &HE8, &H89, &H81, &H92, &HBC, &H5D, &HD8, &H8C, &HE5, &HA9, &H36, &HB8, &H2, &HC2, &H12, &HC1, &HF4, &H96, &H79, &H40, &H0, &H20, &HA, &H8E, &H3, &H47, &H20, &H70, &H60, &H3A, &H39, &H3F, &H52, &H0, &H0, &H49, &H16, &H9C, &H0, &H2, &H0, &HA8, &HC0, &H34, &H70, &H2B, &H86, &H1, &HA3, &H81, &H54, &H30, &HD, &H75, &HB1, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part1() As Byte = {&H48, &H1, &H1, &H0, &HC0, &H3, &H1C, &HB1, &H19, &HC0, &H48, &HE4, &H18, &HC, &H7, &H47, &H20, &H54, &H60, &H3A, &H39, &H3A, &HD2, &H0, &H0, &H49, &HC7, &H9C, &H0, &H2, &H4D, &HB4, &HC0, &H0, &H12, &H83, &H27, &H0, &H0, &H80, &H28, &H30, &HD, &H1C, &H5, &H11, &H80, &H68, &HE0, &H39, &HC, &H3, &H47, &H0, &HE0, &H60, &H1A, &H38, &H42, &H43, &H0, &HD1, &HC2, &H1C, &H18, &HA, &H8E, &H65, &H9C, &HA0, &H10, &H10, &H44, &HC6, &H1, &HA3, &H81, &H56, &H30, &HD, &H1C, &HD, &H11, &H80, &H68, &HE0, &H68, &H8C, &H7, &H47, &HD, &H4E, &H60, &H22, &H3F, &H7A, &HB3, &H1, &H11, &HC0, &H6E, &H18, &H6, &H8E, &H6, &HE0, &HC0, &H34, &H70, &H84, &H47, &H1, &HA3, &H90, &H17, &H30, &H2D, &H1C, &H9F, &HB9, &H0, &H0, &H24, &H2D, &HCC, &H0, &HDA, &HDC, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part2() As Byte = {&H48, &H2, &H1, &H0, &HC0, &H3, &H1, &H24, &H1A, &H60, &H0, &H9, &H3A, &H93, &H0, &H0, &H48, &H7B, &H98, &H0, &H2, &H44, &HAC, &HE0, &H0, &H10, &H38, &H66, &H2, &H23, &H81, &H88, &H38, &HD, &H1C, &H82, &HB1, &H81, &H8, &HE0, &H3, &HC, &H8, &H47, &H0, &H18, &H60, &H1A, &H39, &H96, &H82, &H80, &H80, &H40, &H7A, &H18, &H6, &H8E, &H2, &HF0, &HE0, &H34, &H70, &H2, &HA6, &H2, &H23, &H80, &H15, &H30, &H9, &H1C, &H0, &HA9, &H80, &HE8, &HE0, &H5, &H4C, &H0, &H47, &HD, &HAE, &H60, &H4A, &H39, &H12, &H72, &H0, &H0, &H4A, &HA5, &H18, &H2, &H2, &H49, &HF4, &HC0, &H0, &H12, &H52, &H67, &H0, &H0, &H95, &H1E, &H30, &H28, &H4, &H9F, &HE9, &H80, &H0, &H25, &H13, &H4C, &H2, &H1, &H32, &HD2, &H60, &H0, &H9, &H10, &H33, &H0, &HB, &HC5, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part3() As Byte = {&H48, &H3, &H1, &H0, &HC0, &H3, &H80, &H4C, &HB5, &H9C, &H0, &H2, &H2, &H8, &HC0, &H34, &H70, &H6, &HC6, &H1, &HA3, &H80, &H7D, &H5E, &H30, &HD, &H1C, &H31, &H31, &HC0, &H68, &HE0, &H5, &H4C, &H7, &H47, &H0, &H2A, &H60, &H42, &H38, &H1, &H53, &H1, &H11, &HC0, &HA, &H98, &H14, &H8E, &H1, &H24, &HC0, &HA4, &H72, &H16, &H25, &H0, &H0, &H92, &HA3, &H30, &H0, &H4, &H89, &HE9, &H80, &H0, &H24, &HEE, &H4C, &H0, &H1, &H32, &HE4, &H60, &H50, &H9, &H11, &HD3, &H0, &H0, &H49, &H2A, &H98, &H0, &H2, &H65, &HE8, &HC0, &H0, &H13, &H2F, &H66, &H0, &H0, &H95, &HA, &H30, &H28, &H4, &HAE, &H91, &H81, &H40, &H24, &HBE, &H4E, &H0, &H1, &H3, &H78, &H60, &H1A, &H38, &HC, &HC3, &H0, &HD1, &HC1, &H4, &H18, &H6, &H8E, &H1, &H50, &HC0, &H62, &H6, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part4() As Byte = {&H48, &H4, &H1, &H0, &HC0, &H3, &H34, &H70, &HAC, &HE6, &H2, &H23, &H80, &H72, &H30, &HD, &H1C, &H4, &H71, &H80, &H68, &HE0, &H5C, &HC, &H3, &H47, &H2, &HF0, &H60, &H1A, &H38, &H9, &H3, &H0, &HD1, &HC0, &H4E, &H18, &H6, &H8E, &H1, &HD0, &HC0, &H34, &H71, &H93, &H46, &H1, &HA3, &H80, &HB6, &H30, &HD, &H1C, &HA, &HF1, &H80, &H68, &HE1, &H9D, &HC, &H3, &H47, &HC, &HD0, &H60, &H1A, &H38, &H67, &H23, &H0, &HD1, &HC3, &H2, &H18, &H6, &H8E, &H9, &H8, &HC0, &H34, &H70, &HBF, &HC6, &H1, &HA3, &H86, &H0, &H30, &HD, &H1C, &H30, &H41, &H80, &H68, &HE1, &H7F, &HC, &H3, &H47, &HC, &H4, &H60, &H1A, &H38, &H60, &HA3, &H0, &HD1, &HC3, &H3, &H18, &H6, &H8E, &H17, &HE8, &HC0, &H34, &H70, &HBF, &H6, &H1, &HA3, &H80, &H4A, &H30, &H59, &H5C, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part5() As Byte = {&H48, &H5, &H1, &H0, &HC0, &H3, &HD, &H1C, &H3F, &HB1, &H80, &H68, &HE0, &H19, &HC, &H3, &H47, &H8, &HF8, &H70, &H1A, &H38, &H2, &H93, &H1, &H11, &HC1, &HFB, &H98, &H8, &H8E, &HF, &HDC, &HC0, &H74, &H70, &HE, &H26, &H4, &HA3, &H80, &H71, &H30, &H1, &H1C, &H42, &H39, &H80, &H88, &HE0, &H17, &H4C, &H0, &H47, &H0, &HFE, &H60, &H42, &H38, &H7, &HF3, &H1, &H11, &HC0, &H5E, &H98, &H8, &H8E, &H2, &H2C, &HC0, &H44, &H70, &H8, &HA6, &H4, &H23, &H80, &H45, &H30, &H11, &H1C, &H2, &HF9, &H80, &H88, &HE0, &H17, &HCC, &H8, &H47, &H0, &HE2, &H60, &H22, &H38, &H5, &HD3, &H1, &H11, &HC4, &H7D, &H5E, &H98, &H8, &H8E, &H5, &H24, &HC0, &H44, &H70, &H2F, &H26, &H2, &H23, &H81, &H2B, &H30, &H11, &H1C, &HA, &HE9, &H80, &H88, &HE2, &H10, &HCC, &H41, &H5C, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part6() As Byte = {&H48, &H6, &H1, &H0, &HC0, &H3, &H4, &H47, &H8, &HD2, &H60, &H22, &H38, &HB, &H33, &H1, &H11, &HC9, &H24, &H90, &H0, &H2, &H48, &HEC, &HC0, &H0, &H12, &H59, &H26, &H0, &H0, &H92, &H87, &H38, &H0, &H4, &H6, &H81, &H80, &H68, &HE0, &H88, &H8C, &H3, &H47, &H3, &HBC, &H60, &H1A, &H38, &H55, &HF3, &H1, &H11, &HC2, &HAF, &H98, &H14, &H8E, &H10, &H38, &HE0, &H34, &H70, &H11, &HE6, &H1, &H23, &H80, &H8F, &H30, &H11, &H1C, &H36, &HA1, &H80, &H8, &HE6, &H5B, &H48, &H0, &H1, &H32, &HDA, &H70, &H50, &H9, &H27, &H93, &H80, &H0, &H40, &H1, &H18, &H6, &H8E, &H0, &H10, &HC0, &H34, &H70, &H8, &H6, &H1, &HA3, &H80, &HE4, &H30, &HD, &H1C, &H8D, &HB9, &H0, &H0, &H26, &H4A, &H4C, &H0, &H1, &H23, &H3A, &H70, &H0, &H8, &H15, &H63, &H22, &HB1, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part7() As Byte = {&H48, &H7, &H1, &H0, &HC0, &H3, &H0, &HD1, &HC0, &H54, &H98, &H8, &H8E, &H8, &H44, &HC0, &H44, &H70, &HA5, &H46, &H5, &H23, &H93, &H3B, &H20, &H0, &H4, &HC9, &H39, &HC0, &H0, &H20, &HD, &H4C, &H4, &H47, &HD, &H96, &H60, &H22, &H38, &H40, &H53, &H81, &H11, &HC0, &H18, &H18, &H6, &H8E, &H2, &H30, &HC0, &H34, &H70, &H81, &H46, &H1, &HA3, &H84, &H2, &H30, &HD, &H1C, &H20, &H21, &H80, &H68, &HE1, &H1, &H8C, &H3, &H47, &H0, &H38, &H40, &H1A, &H38, &H1, &HC3, &H2, &H51, &HC0, &HE, &H1C, &H10, &H8E, &H0, &H58, &H80, &H34, &H71, &H1, &H46, &H1, &HA3, &H81, &HE6, &H30, &HD, &H1C, &H4, &HD1, &H80, &H68, &HE0, &H15, &H8C, &H3, &H47, &H2, &H14, &H60, &H1A, &H38, &H4, &HE3, &H0, &HD1, &HC2, &HF2, &H18, &H6, &H8E, &H53, &HC4, &HDC, &HFC, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part8() As Byte = {&H48, &H8, &H1, &H0, &HC0, &H3, &H80, &H20, &H12, &HEE, &HA6, &H1, &H0, &H93, &HF1, &H38, &H0, &H4, &H22, &H69, &H80, &H88, &HE0, &H6A, &HC, &H3, &H47, &H0, &H94, &H60, &H1A, &H38, &H5, &H33, &H1, &H11, &HC3, &H6F, &H9C, &H8, &H8E, &H0, &H20, &HC0, &H34, &H70, &H1F, &H46, &H1, &HA3, &H80, &H38, &H30, &HD, &H1C, &H0, &H91, &H80, &H68, &HE0, &H1D, &HCC, &H9, &H47, &H0, &HEE, &H60, &H22, &H38, &H12, &HC3, &H0, &HD1, &HC0, &HA1, &H98, &H8, &H8E, &H7, &HD8, &HC0, &H4, &H70, &H3E, &HC6, &H1, &HA3, &H81, &HF6, &H30, &H29, &H1C, &H36, &H1, &H80, &H8, &HE1, &H79, &H8C, &H3, &H47, &HB, &HD8, &H60, &H1A, &H38, &H5E, &H23, &H0, &HD1, &HC3, &H6A, &H98, &H8, &H8E, &H1B, &H54, &HC0, &HA4, &H70, &H2, &HC6, &H1, &HA3, &H80, &H4E, &H7A, &HC8, &H7E}
    Public Shared ReadOnly sendPRL_1001_Part9() As Byte = {&H48, &H9, &H1, &H0, &HC0, &H3, &H30, &HD, &H1C, &H2, &HB1, &H80, &H68, &HE0, &H26, &H8C, &H3, &H47, &H1, &HC4, &H60, &H1A, &H38, &H19, &H43, &H0, &HD1, &HC0, &HF3, &H18, &H6, &H8E, &H20, &H28, &HC0, &H34, &H70, &H15, &H46, &H3, &HA3, &H80, &HAA, &H30, &HD, &H1C, &H3B, &HC1, &H80, &H68, &HE5, &HCE, &H4A, &H2, &H1, &H32, &HD6, &H70, &H0, &H8, &H7, &HE3, &H80, &HD1, &HC0, &H10, &H18, &H6, &H8E, &H0, &H80, &HC0, &HA4, &H70, &HC, &H6, &H1, &HA3, &H80, &H3C, &H20, &HD, &H1C, &H87, &H29, &H40, &H0, &H21, &H25, &H4C, &H4, &H47, &H9, &H1A, &H70, &H22, &H38, &H11, &H3, &H0, &HD1, &HC2, &H46, &H18, &H6, &H8E, &H12, &H58, &HC0, &H34, &H72, &H74, &H65, &H0, &H0, &H86, &HCB, &H30, &H11, &H1C, &H36, &H69, &HC0, &HC8, &HE0, &H17, &HC3, &H5B, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartA() As Byte = {&H48, &HA, &H1, &H0, &HC0, &H3, &H8C, &H3, &H47, &HD, &H98, &H60, &H1A, &H39, &H67, &HC2, &H0, &H0, &H4B, &H8D, &H1C, &H14, &H2, &H8, &HAC, &HC0, &H44, &H70, &H13, &H6, &H1, &HA3, &H84, &HD4, &H30, &HD, &H1C, &HF, &HF9, &H80, &H88, &HE1, &HC6, &H8E, &H3, &H47, &H1, &H2C, &H60, &H1A, &H38, &H21, &H3, &H0, &HD1, &HC0, &H8F, &H18, &H6, &H8E, &H2, &H24, &HC0, &H44, &H70, &H11, &H26, &H4, &HA3, &H81, &HE3, &H30, &H25, &H1C, &H10, &H81, &H81, &H28, &HE2, &HFE, &H8C, &H3, &H47, &H9, &H88, &H60, &H1A, &H38, &HB, &HA3, &H0, &HD1, &HC0, &H9F, &H10, &H6, &H8E, &H3, &HC0, &HC0, &H34, &H70, &HE2, &H86, &H1, &HA3, &H81, &H8C, &H38, &HD, &H1C, &H0, &HD1, &H80, &H68, &HE0, &H6, &H8C, &H9, &H47, &H2, &HE6, &H40, &H22, &H38, &H55, &H17, &H23, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartB() As Byte = {&H48, &HB, &H1, &H0, &HC0, &H3, &HC3, &H0, &HD1, &HC0, &H8E, &H9C, &H8, &H8E, &H4, &HF0, &HC0, &H34, &H70, &H29, &H46, &H1, &HA3, &H86, &HC, &H30, &HD, &H1C, &HA, &H51, &H81, &H28, &HE0, &H6, &H8C, &H9, &H47, &H32, &HD4, &H50, &H0, &H8, &H1, &H43, &H0, &HD1, &HC0, &HA, &H18, &HE, &H8E, &H7, &HE8, &HC0, &H74, &H72, &H7, &H46, &H3, &HA3, &H82, &H14, &H30, &HD, &H1C, &H6, &HB1, &H80, &H68, &HE1, &H29, &H4C, &H4, &H47, &H3, &HF4, &H60, &H1A, &H38, &H21, &H23, &H0, &HD1, &HC1, &H9, &H18, &HE, &H8E, &H1, &H40, &HC0, &HA4, &H70, &HA, &H6, &H1, &HA3, &H80, &H50, &H30, &H1D, &H1C, &H80, &H71, &H0, &H8, &HE4, &H20, &H4E, &H9, &H47, &H0, &H82, &H60, &H22, &H38, &H4, &H13, &H2, &H91, &HC0, &H5F, &H18, &H6, &H8E, &H50, &H23, &H82, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartC() As Byte = {&H48, &HC, &H1, &H0, &HC0, &H3, &HD4, &HA0, &H0, &H10, &H9D, &H46, &H5, &HA3, &H84, &HF7, &H30, &H11, &H1C, &H27, &H51, &H80, &H68, &HE0, &H88, &H8C, &H3, &H47, &HA, &HF0, &H70, &H1A, &H38, &H3, &HB3, &H0, &H51, &HC0, &H1D, &H98, &H8, &H8E, &H0, &H44, &HC0, &H94, &H70, &H2, &H26, &H4, &H23, &H80, &H11, &H30, &H11, &H1C, &H5, &H29, &H80, &H88, &HE1, &H66, &H8C, &H9, &H47, &HB, &H34, &H60, &H42, &H38, &H58, &H3, &H0, &H91, &HC2, &HC0, &H18, &H6, &H8E, &H16, &H84, &HA0, &H44, &H70, &H20, &HC6, &H1, &HA3, &H99, &H19, &H28, &H0, &H4, &H6, &HF1, &H81, &H28, &HE0, &H37, &H8C, &H3, &H47, &H9, &H18, &H60, &H1A, &H38, &H6F, &H3, &H0, &HD1, &HC9, &HA8, &H90, &H0, &H2, &H62, &HCC, &HC0, &H0, &H12, &H73, &H67, &H0, &H0, &H80, &H2A, &HAA, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartD() As Byte = {&H48, &HD, &H1, &H0, &HC0, &H3, &H6, &H30, &HD, &H1C, &H0, &HF1, &H80, &H68, &HE0, &H7, &H8C, &H2, &H47, &H25, &H90, &H60, &H3A, &H38, &H64, &H83, &H0, &HD1, &HC0, &HA4, &H18, &H6, &H8E, &H0, &HE4, &H80, &H44, &H70, &H7, &H26, &H0, &HA3, &H80, &H55, &H30, &H11, &H1C, &H6, &HA1, &H80, &H68, &HE0, &H5C, &H8C, &H3, &H47, &HEE, &H78, &H60, &H1A, &H38, &H1B, &HC3, &H0, &HD1, &HC1, &H10, &H18, &H6, &H8E, &H1, &HE0, &HC0, &H34, &H70, &H37, &H86, &H0, &HA3, &H80, &HCD, &H30, &H11, &H1C, &H2B, &H49, &H80, &H88, &HE1, &H5C, &H4C, &H4, &H47, &H2, &H32, &H60, &H22, &H38, &H46, &H43, &H80, &HD1, &HC8, &H20, &H18, &HE, &H8E, &H5, &H24, &HC0, &H44, &H72, &HAA, &H44, &H5, &H0, &H95, &HA4, &H30, &H28, &H4, &H89, &HB9, &H80, &H0, &H24, &H47, &H6F, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartE() As Byte = {&H48, &HE, &H1, &H0, &HC0, &H3, &HD8, &H4E, &H0, &H1, &H3, &HCE, &H60, &H22, &H38, &H44, &H53, &H81, &H11, &HC0, &H1D, &H18, &H6, &H8E, &H7, &HA8, &HC0, &H34, &H70, &H88, &HC6, &H1, &HA3, &H84, &H3E, &H30, &HD, &H1C, &H22, &H1, &H80, &H68, &HE1, &HE, &HCC, &H4, &H47, &H30, &HD2, &H40, &H0, &H9, &H26, &HD3, &H0, &H0, &H4C, &H87, &H98, &H0, &H2, &H46, &H14, &HE0, &H0, &H10, &H9, &HE6, &H2, &H23, &H82, &H11, &H30, &H5, &H1C, &H10, &H89, &H80, &H48, &HE1, &H76, &HCC, &H4, &H47, &HB, &HAA, &H60, &H22, &H38, &H21, &H13, &H1, &H11, &HC0, &H30, &H9C, &H8, &H8E, &H1, &H70, &HC0, &H34, &H70, &HD, &HC6, &H1, &HA3, &H84, &H46, &H30, &HD, &H1C, &H39, &H9, &H0, &H88, &HE3, &H1, &HE, &H3, &H47, &HE, &H4C, &H60, &H1A, &H38, &H76, &HAC, &H7E}
    Public Shared ReadOnly sendPRL_1001_PartF() As Byte = {&H48, &HF, &H0, &H0, &H10, &H1, &H11, &HC3, &H0, &HD1, &HC3, &H90, &H18, &H6, &H8E, &H1C, &H88, &HC0, &H34, &H70, &HE4, &H66, &H2, &H23, &H84, &H2D, &H20, &H11, &H1C, &H25, &H41, &H0, &H68, &HE1, &H70, &H48, &H4, &H47, &HE3, &H3F, &H80, &H68, &HE1, &HE, &HCC, &H4, &H47, &H30, &HD2, &H40, &H0, &H9, &H26, &HD3, &H0, &H0, &H4C, &H87, &H98, &H0, &H2, &H46, &H14, &HE0, &H0, &H10, &H9, &HE6, &H2, &H23, &H82, &H11, &H30, &H5, &H1C, &H10, &H89, &H80, &H48, &HE1, &H76, &HCC, &H4, &H47, &HB, &HAA, &H60, &H22, &H38, &H21, &H13, &H1, &H11, &HC0, &H30, &H9C, &H8, &H8E, &H1, &H70, &HC0, &H34, &H70, &HD, &HC6, &H1, &HA3, &H84, &H46, &H30, &HD, &H1C, &H39, &H9, &H0, &H88, &HE3, &H1, &HE, &H3, &H47, &HE, &H4C, &H60, &H1A, &H38, &HB0, &H41, &H7E}

    ''send prl 1050
    Dim sendPRL_1050_Part1() As Byte
    Dim sendPRL_1050_Part2() As Byte
    Dim sendPRL_1050_Part3() As Byte
    Dim sendPRL_1050_Part4() As Byte
    Dim sendPRL_1050_Part5() As Byte
    Dim sendPRL_1050_Part6() As Byte
    Dim sendPRL_1050_Part7() As Byte
    Dim sendPRL_1050_Part8() As Byte
    Dim sendPRL_1050_Part9() As Byte
    Dim sendPRL_1050_PartA() As Byte



    ''blank arrays
    Public empty_cmd_133() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public empty_cmd_136() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public empty_cmd_264() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}


#End Region

#Region "BadTestCode"
    ''drop it here, trim it when the idea/function it deals wit works







#End Region

#Region "AutoMagic"


    'Sub u350magicAPPLYDIRECTLYTOTHEFOREHEAD()
    '    '' ''sham-wow!
    '    Try
    '        scanAndListComs()
    '        connectSub()
    '        dispatchQ.clearCommandQ()
    '        dispatchQ.addCommandToQ(New Command(modeOfflineD, "u350m.1 mode offline"))
    '        dispatchQ.executeCommandQ()
    '        Threading.Thread.Sleep(300)
    '        dispatchQ.addCommandToQ(New Command(send16digitSchU350, "u350m.2 16sp"))
    '        dispatchQ.addCommandToQ(New Command(writeSPC_DefMethod000000, "u350m.3 write spc 000000"))
    '        dispatchQ.addCommandToQ(New Command(sendSPC_DefMethod000000, "u350m.4 send spc 000000"))

    '        sendPRL(selectPRLComboBox.Text)
    '        dispatchQ.addCommandToQ(New Command(modeReset, "u350m.5 mode reset"))


    '        dispatchQ.executeCommandQ()
    '        mySerialPort.Close()
    '        Throw New Exception("u350 flash magic! bam! ohhh by ¿k? ajh and ws.six")

    '    Catch ex As Exception
    '        Throw New Exception("u350 automagic error, broken magic wand?")
    '    End Try


    'End Sub
    'Sub SAMSUNGmagicAPPLYDIRECTLYTOTHEFOREHEAD()
    '    '' ''sham-wow!
    '    Try

    '        If portIsOpen = False Then
    '            ComNumBox1.Items.Clear()
    '            scanAndListComs()
    '            connectSub()
    '        End If

    '        ''TODO: FIX SHIT
    '        dispatchQ.addCommandToQ(New Command(modeOfflineD, "SAMSUNGmagic.1 modeOfflineD"))
    '        dispatchQ.executeCommandQ()
    '        Threading.Thread.Sleep(300)

    '        ''sendTermCommand2(send16digitSchU350)
    '        ''Threading.Thread.Sleep(200)
    '        SendA16digitCode()

    '        dispatchQ.addCommandToQ(New Command(writeSPC_DefMethod000000, "SAMSUNGmagic.2 writeSPC_DefMethod000000"))


    '        dispatchQ.addCommandToQ(New Command(sendSPC_DefMethod000000, "SAMSUNGmagic.3 sendSPC_DefMethod000000"))


    '        sendPRL(selectPRLComboBox.Text)

    '        dispatchQ.addCommandToQ(New Command(modeReset, "SAMSUNGmagic.4 modeReset"))
    '        dispatchQ.executeCommandQ()

    '        disconnectPort()
    '        Throw New Exception("sumsung flash magic! bam! ohhh! by ¿k? ajh and ws.six")

    '    Catch ex As Exception
    '        Throw New Exception("sumsung automagic error, broken magic wand?")
    '    End Try


    'End Sub

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
    '        Throw New Exception("WM_COMMNOTIFY Triggered")
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
    '                            '' Throw new Exception("connect next phone")
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

#Region "crcTestCode"








    'Shared Function gimmeCRC_AsByte_FromByte(ByVal incomingBytes As Byte()) As Byte()
    '    ''read in string as bytes
    '    ''Dim ffff_crc_Step1 As Byte()
    '    Dim flipper As New calculateCRC
    '    Dim crc_ccitt As New cdmaCRC(InitialCrcValue.NonZero1)
    '    Dim aTermCommandWithoutSpaces As String = TextBox5.Text.Replace(" ", String.Empty)


    '    ''testCRC_box_pt5.Text = biznytesToStrizings(String_To_Bytes(aTermCommandWithoutSpaces))
    '    ''testCRC_box_1.Text = biznytesToStrizings(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces)))
    '    ''testCRC_box_2.Text = biznytesToStrizings(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces))))
    '    ''testCRC_box_3.Text = biznytesToStrizings(flipper.doStep3theInvert(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces)))))
    '    Return flipper.FLiPallBytesInByteArray(flipper.doStep3theInvert(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(incomingBytes))))

    'End Function
    'Function gimmeCRC_AsString_FromString(ByVal incomingString As String) As String
    '    ''read in string as bytes
    '    ''Dim ffff_crc_Step1 As Byte()
    '    Dim flipper As New calculateCRC
    '    Dim crc_ccitt As New cdmaCRC(InitialCrcValue.NonZero1)
    '    Dim aTermCommandWithoutSpaces As String = TextBox5.Text.Replace(" ", String.Empty)


    '    ''testCRC_box_pt5.Text = biznytesToStrizings(String_To_Bytes(aTermCommandWithoutSpaces))
    '    ''testCRC_box_1.Text = biznytesToStrizings(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces)))
    '    ''testCRC_box_2.Text = biznytesToStrizings(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces))))
    '    ''testCRC_box_3.Text = biznytesToStrizings(flipper.doStep3theInvert(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(String_To_Bytes(aTermCommandWithoutSpaces)))))
    '    Return biznytesToStrizings(flipper.FLiPallBytesInByteArray(flipper.doStep3theInvert(crc_ccitt.ComputeChecksumBytes(flipper.FLiPallBytesInByteArray(String_To_Bytes(incomingString))))))


    'End Function


#End Region

#Region "evdoTestCode"

    Sub sendAllEVDO(evdo_username As String, evdo_password As String)

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

        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PPP_USER_ID_I, evdoUser, "Evdo write PPP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PPP_PASSWORD_I, evdoPass, "Evdo write PPP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PAP_USER_ID_I, evdoUser, "Evdo write PAP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PAP_PASSWORD_I, evdoPass, "Evdo write PAP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_USER_ID_LONG_I, evdoUser, "Evdo write HDR_AN_AUTH_USER_ID_LONG"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_PASSWORD_LONG_I, evdoPass, "Evdo write HDR_AN_AUTH_PASSWD_LONG"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_NAI_I, evdoUser, "Evdo write HDR_AN_AUTH_NAI_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_PASSWORD_I, evdoPass, "Evdo write HDR_AN_AUTH_PASSWORD_I"))


        'sendEVDO_u1()

        'sendEVDO_p1()

        'sendEVDO_u2()

        'sendEVDO_p2()

        'sendEVDO_u3()

        'sendEVDO_p3()

        'sendEVDO_u4()

        'sendEVDO_p4()

    End Sub

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

    Sub sendEVDO_u1(evdo_username As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H8E, &H3, &H1B}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_usernameTextbox.Text)), "sendEVDO_u1"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PPP_USER_ID_I, getEvdoUser(evdo_username), "Evdo write PPP_USER_ID_I"))

    End Sub
    Sub sendEVDO_p1(EvdoPass As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H8A, &H3, &H8}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_passwordTextbox.Text)), "sendEVDO_p1"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PPP_PASSWORD_I, getEvdoPass(EvdoPass), "Evdo write PPP_PASSWORD_I"))

    End Sub
    Sub sendEVDO_u2(evdo_username As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H3E, &H1, &H1B}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_usernameTextbox.Text)), "sendEVDO_u2"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PAP_USER_ID_I, getEvdoUser(evdo_username), "Evdo write PAP_USER_ID_I"))

    End Sub
    Sub sendEVDO_p2(evdo_password As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H3F, &H1, &H8}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_passwordTextbox.Text)), "sendEVDO_p2"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_PAP_PASSWORD_I, getEvdoPass(evdo_password), "Evdo write PAP_PASSWORD_I"))

    End Sub
    Sub sendEVDO_u3(evdo_username As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &HAA, &H4, &H1B}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_usernameTextbox.Text)), "sendEVDO_u3"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_USER_ID_LONG_I, getEvdoUser(evdo_username), "Evdo write HDR_AN_AUTH_USER_ID_LONG"))

    End Sub
    Sub sendEVDO_p3(evdo_password As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &HA8, &H4, &H8}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_passwordTextbox.Text)), "sendEVDO_p3"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_PASSWORD_LONG_I, getEvdoPass(evdo_password), "Evdo write HDR_AN_AUTH_PASSWD_LONG"))

    End Sub
    Sub sendEVDO_u4(evdo_username As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H43, &H2, &H1C}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_usernameTextbox.Text)), "sendEVDO_u4"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_NAI_I, getEvdoUser(evdo_username), "Evdo write HDR_AN_AUTH_NAI_I"))

    End Sub
    Sub sendEVDO_p4(evdo_password As String)
        ''build a command test evdo1
        'Dim bob As New BuilderBob
        'Dim prefix As Byte() = {&H27, &H44, &H2, &H8}

        'dispatchQ.addCommandToQ(New Command(bob.buildATerminalCommand(empty_cmd_133, prefix, bob.buildDataArray("0" + evdo_passwordTextbox.Text)), "sendEVDO_p4"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HDR_AN_AUTH_PASSWORD_I, getEvdoPass(evdo_password), "Evdo write HDR_AN_AUTH_PASSWORD_I"))

    End Sub



#End Region


#Region "prlTestCode"

    Public Sub sendPRL(ByVal selectPRL As String)


        If selectPRL = "cricKet_1030" Then

            dispatchQ.addCommandToQ(New Command(sendPRL_1030_Part0, "prl 1030 pt1"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1030_Part1, "prl 1030 pt2"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1030_Part2, "prl 1030 pt3"))


        ElseIf selectPRL = "cricket_1050" Then


        ElseIf selectPRL = "metro_2004" Then

            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part0, "sendPRL_2004_Part0"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part1, "sendPRL_2004_Part1"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part2, "sendPRL_2004_Part2"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part3, "sendPRL_2004_Part3"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part4, "sendPRL_2004_Part4"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part5, "sendPRL_2004_Part5"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part6, "sendPRL_2004_Part6"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part7, "sendPRL_2004_Part7"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part8, "sendPRL_2004_Part8"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part9, "sendPRL_2004_Part9"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartA, "sendPRL_2004_PartA"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartB, "sendPRL_2004_PartB"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartC, "sendPRL_2004_PartC"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartD, "sendPRL_2004_PartD"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartE, "sendPRL_2004_PartE"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_PartF, "sendPRL_2004_PartF"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part10, "sendPRL_2004_Part10"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part11, "sendPRL_2004_Part11"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part12, "sendPRL_2004_Part12"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part13, "sendPRL_2004_Part13"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part14, "sendPRL_2004_Part14"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part15, "sendPRL_2004_Part15"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part16, "sendPRL_2004_Part16"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part17, "sendPRL_2004_Part17"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part18, "sendPRL_2004_Part18"))
            dispatchQ.addCommandToQ(New Command(sendPRL_2004_Part19, "sendPRL_2004_Part19"))



        ElseIf selectPRL = "metro_1001" Then

            ''TODO: ADD BETTER COMMENTS FOR DEBUG STRING
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part0, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part1, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part2, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part3, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part4, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part5, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part6, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part7, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part8, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_Part9, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartA, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartB, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartC, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartD, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartE, "prl"))
            dispatchQ.addCommandToQ(New Command(sendPRL_1001_PartF, "prl"))

        End If
    End Sub





#End Region

#Region "Random Test Code"

    Shared Sub sendAnySPC(ByVal customSPC As String)
        ''dg qc send spc
        dispatchQ.addCommandToQ(New Command(DIAG_SPC_F, ASCIIEncoding.ASCII.GetBytes(customSPC), "DIAG_SPC_F customSPC Sent"))

    End Sub

    Public Shared Sub writeAnySpc(ByVal customSPC As String)

        dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, NvItems.NVItems.NV_SEC_CODE_I, ASCIIEncoding.ASCII.GetBytes(customSPC), "write spc")) ''todo:untested now

    End Sub


    'Private Sub zeroSPCPRLButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles zeroSPCPRLButton.Click

    '    If BrandComboBox.Text = "Samsung" Then
    '        SAMSUNGmagicAPPLYDIRECTLYTOTHEFOREHEAD()
    '    Else
    '        If portIsOpen = False Then
    '            ComNumBox1.Items.Clear()
    '            scanAndListComs()
    '            connectSub()
    '        End If


    '        ''ajh7495 start 4
    '        ''dispatchQ.addCommandToQ(New Command(readSpcFromPhone, "Read SPC From Phone")) 
    '        '' possibly change command class to accept
    '        readSpcFromPhone(readSPCTypeCombo.Text)

    '        ''dispatchQ.addCommandToQ(New Command(sendAnySPC, "Send SPC (Uses SPC Box)")) 
    '        '' possibly change command class to accept || accept response required
    '        sendAnySPC(SPCTextbox.Text)

    '        dispatchQ.addCommandToQ(New Command(writeSPC_DefMethod000000, "Write SPC Def Method 000000"))

    '        ''dg try to fix method signature hell
    '        dispatchQ.addCommandToQ(New Command(modeOfflineD, "Mode Offline D"))
    '        '' requires response
    '        dispatchQ.addCommandToQ(New Command(sendSPC_DefMethod000000, "Send SPC Def Method 000000"))
    '        ''dispatchQ.addCommandToQ(New Command(sendPRL, "Send PRL (Uses PRL Box)")) 
    '        '' possibly change command class to accept 

    '        sendPRL(selectPRLComboBox.Text)
    '        dispatchQ.executeCommandQ()
    '        Throw new Exception("zero/prl sent: mode reset suggested")
    '        ''ajh7495 end 4
    '    End If



    'End Sub


    Private Sub lgReadMeid()

        Dim ponyXpress As New dispatchQmanager

        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(unlockLgNvMemory, "Unlock LG NV Memory"))
        dispatchQ.addCommandToQ(New Command(lg_Lock1, "LG Lock 1"))
        dispatchQ.addCommandToQ(New Command(lg_KeyEmu1, "LG Key Emu1"))
        dispatchQ.addCommandToQ(New Command(keyPress_END_DN, "KeyPress END_DN"))
        dispatchQ.addCommandToQ(New Command(keyPress_3, "KeyPress 3"))
        dispatchQ.addCommandToQ(New Command(keyPress_7, "KeyPress 7"))
        dispatchQ.addCommandToQ(New Command(keyPress_3, "KeyPress 3"))
        dispatchQ.addCommandToQ(New Command(keyPress_3, "KeyPress 3"))
        dispatchQ.addCommandToQ(New Command(keyPress_9, "KeyPress 9"))
        dispatchQ.addCommandToQ(New Command(keyPress_2, "KeyPress 2"))
        dispatchQ.addCommandToQ(New Command(keyPress_9, "KeyPress 9"))
        dispatchQ.addCommandToQ(New Command(keyPress_END_DN, "KeyPress END_DN"))

        ''dispatchQ.addCommandToQ(New Command(nvmodeMEIDRead, "NV Mode MEID Read"))
        ''dispatchQ.addCommandToQ(New Command(nvmodeMEIDRead, True, "ReadMeid_NV", "Any", "NV Mode MEID Read"))
        dispatchQ.addCommandToQ(New Command(readSPC_LgMethod, "ReadSPC_LG", "ReadSPC_LG"))
        dispatchQ.addCommandToQ(New Command(readSPC_LgMethod, "ReadSPC_LG", "ReadSPC_LG"))
        dispatchQ.executeCommandQ()


    End Sub

    Private Sub ReadMIN1()

        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN1_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN1_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN2_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN2_I"))

        dispatchQ.executeCommandQ()


        DecodeMin()

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

    Public Shared Sub modeSwitch(ByVal mode As String)

        ''first check which read type then go
        If mode = "Offline" Then
            dispatchQ.addCommandToQ(New Command(modeOfflineD, "mode offline"))

        ElseIf mode = "Online" Then
            dispatchQ.addCommandToQ(New Command(modeReset, "no mode online(reset sent)"))

        ElseIf mode = "Low" Then
            dispatchQ.addCommandToQ(New Command(modeOfflineD, "no mode low(offd sent)"))

        ElseIf mode = "Reset" Then
            dispatchQ.addCommandToQ(New Command(modeReset, "mode reset"))

        ElseIf mode = "P2K" Then

            switchToP2K()
        End If


    End Sub

    Private Sub readNV(rangeStart As String, rangeEnd As String, saveFilePath As String)
        nvReadQ.clearCommandQ()
        logger.addToLog("Reading NV - This may take a while, do not unplug.")
        Dim nv As New NvItems
        nv.readNVItemRange(rangeStart, rangeEnd)
        dispatchQ.executeCommandQ()
        logger.addToLog("Reading NV - This may take a while, do not unplug..")
        nvReadQ.checkNvQForBadItems()
        logger.addToLog("Reading NV - This may take a while, do not unplug...")
        nvReadQ.generateNvReadReport(saveFilePath)
        logger.addToLog("NV Read Complete")
    End Sub

    Private Sub NVItemWrite(WriteFromFile As Boolean, strFileName As String, NVItemNumber As String, NVItemValue As Byte())
        Dim nv As New NvItems()
        If WriteFromFile = True Then
            nv.writeNVItemRange(strFileName)
        Else
            nv.WriteNVItem(Integer.Parse(NVItemNumber), NVItemValue)
        End If
    End Sub

    Public Shared EfsQc As New Qcdm

    Sub SendAnyPrl(PrlFilePath As String)
        Try
            Dim PrlFile As String
            Dim myPlus As New Prl

            myPlus.UploadPRL(PrlFilePath)

        Catch ex As Exception
            Throw New Exception("Prl send err: " + ex.ToString)
        End Try

    End Sub

#End Region



    Private Sub ReadAllEvdo()

        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PPP_USER_ID_I, New Byte() {}, "Evdo NV_PPP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PPP_PASSWORD_I, New Byte() {}, "Evdo NV_PPP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PAP_USER_ID_I, New Byte() {}, "Evdo NV_PAP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PAP_PASSWORD_I, New Byte() {}, "Evdo NV_PAP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_USER_ID_LONG_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_USER_ID_LONG_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_PASSWORD_LONG_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_PASSWD_LONG"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_NAI_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_NAI_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_PASSWORD_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_PASSWORD_I"))

        ''profiles
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_MIP_NUM_PROF_I, New Byte() {}, "Evdo NV_DS_MIP_NUM_PROF_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_MIP_ENABLE_PROF_I, New Byte() {}, "Evdo NV_DS_MIP_ENABLE_PROF_I"))


    End Sub

    Private Sub writeNam0MdnButton_Click(mdn As String)
        dispatchQ.clearCommandQ()
        Dim s As String = "WriteMdn: " + mdn
        Dim mdnWriteData As New List(Of Byte)
        mdnWriteData.Add(&H0)
        mdnWriteData.AddRange(ASCIIEncoding.ASCII.GetBytes(mdn))

        Dim s2 As String = mdn
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_DIR_NUMBER_I, mdnWriteData.ToArray, s))
        ''dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_BANNER_I, ASCIIEncoding.ASCII.GetBytes(nam0MDNTextbox.Text), s))

        dispatchQ.executeCommandQ()


        ReadSingleNv(NV_DIR_NUMBER_I)

    End Sub


    'Sub runCustomQcScript()
    '    dispatchQ.clearCommandQ()
    '    For i As Integer = 0 To commandMacroListBox.Items.Count - 1


    '        'Next


    '        'For Each s As String In commandMacroListBox.Items
    '        Dim currentQc As Qcdm.Cmd = CType(commandMacroListBox.Items(i), Qcdm.Cmd)
    '        Dim debugInfo As String = "UserQcScrpit: " + currentQc.ToString
    '        Dim data As Byte() = String_To_Bytes(qcMacroDataTextbox.Text)


    '        ''test if qc type is nv read or write
    '        If currentQc = DIAG_NV_READ_F Or currentQc = DIAG_NV_READ_F Then

    '            '' Dim currentNV As NvItems.NVItems = [Enum].Parse(GetType(NvItems.NVItems), commandMacroListBox.Items(i + 1))

    '            ''working
    '            '' Dim currentNV As NvItems.NVItems = [Enum].Parse(GetType(NvItems.NVItems), nvItemsCombo.Text)
    '            Dim currentNV As NvItems.NVItems = [Enum].Parse(GetType(NvItems.NVItems), commandMacroListBox.Items(i + 1))


    '            ''not working
    '            '' Dim currentNV As NvItems.NVItems = CType(commandMacroListBox.Items(i + 1), NvItems.NVItems)

    '            dispatchQ.addCommandToQ(New Command(currentQc, currentNV, data, debugInfo))
    '            ''increment counter for nv item
    '            i += 1


    '        Else
    '            dispatchQ.addCommandToQ(New Command(currentQc, data, debugInfo))


    '        End If




    '    Next
    '    dispatchQ.executeCommandQ()
    'End Sub

    Function GetComFriendlyNames()


        ''  Dim m_oFriendlyNameMap As Hashtable = BuildPortNameHash(System.IO.Ports.SerialPort.GetPortNames())
        Return 0
    End Function

    Private Shared Function GetPlainPortNameFromFriendly(ByVal FriendlyName As String) As String

        Return FriendlyName.Split(" ")(0)

        ''Throw New NotImplementedException
    End Function


    Private Sub ReadEvdoMode()

        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_QCMIP_I, New Byte() {}, "NV_DS_QCMIP_I Read EVDO mode"))

    End Sub


    Sub WriteEvdoMode(evdoMode As Integer)

        Dim type As Byte() = {evdoMode}

        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_DS_QCMIP_I, type, "DIAG_NV_READ_F,NV_DS_QCMIP_I Read EVDO mode"))

    End Sub


    Private Sub EncodeMIN(MIN1 As String)

        Dim minStrings() As String = mySDR.encode_NV_MIN1(MIN1)
        MIN1Raw = minStrings(0)
        MIN2Raw = minStrings(1)

        '' MIN1Txtbox.Text = mySDR.decode_NV_MIN1(MIN1Raw, MIN2Raw.Text)
    End Sub

    Public Shared MIN1Raw As New String("")
    Public Shared MIN2Raw As New String("")
    Private Sub WriteMIN(MinNumber As String)
        EncodeMIN(MinNumber)

        Dim MIN1 As New List(Of Byte)
        ''Random Mystery Zeros...
        MIN1.Add(&H0)

        For i As Integer = String_To_Bytes(MIN1Raw).Length - 1 To 0 Step -1
            MIN1.Add(String_To_Bytes(MIN1Raw)(i))
        Next
        For i As Integer = String_To_Bytes(MIN1Raw).Length - 1 To 0 Step -1
            MIN1.Add(String_To_Bytes(MIN1Raw)(i))
        Next

        Dim MIN2 As New List(Of Byte)
        ''Random Mystery Zeros...
        MIN2.Add(&H0)

        For i As Integer = String_To_Bytes(MIN2Raw).Length - 1 To 0 Step -1
            MIN2.Add(String_To_Bytes(MIN2Raw)(i))
        Next
        For i As Integer = String_To_Bytes(MIN2Raw).Length - 1 To 0 Step -1
            MIN2.Add(String_To_Bytes(MIN2Raw)(i))
        Next


        ''Totally untested probably dangerous


        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_MIN1_I, MIN1.ToArray, "DIAG_NV_WRITE_F, NV_MIN1_I, MIN1.ToArray"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_MIN2_I, MIN2.ToArray, "DIAG_NV_WRITE_F, NV_MIN2_I, MIN2.ToArray"))



        Throw New Exception("unplug now or face certian disaster")

    End Sub

    Function DecodeMin() As String
        Return mySDR.decode_NV_MIN1(MIN1Raw, MIN2Raw)
    End Function

    Public Shared Sub disconnectPort()
        Try
            If serialportType = "normal" Then
                '' mySerialPort.Close()

            ElseIf serialportType = "blackberry" Then
                mySerialPort2.Flush()
                mySerialPort2.Dispose()
                portIsOpen = False


            End If
        Catch ex As Exception
            Throw New Exception("disconnect err" + ex.ToString)
        End Try
    End Sub


    ''hm.. two items?
    ''requestnvitemidwrite 11055 0xD4 0x07
    ''requestnvitemidwrite 11089 0x01 0xD4 0x07

    ''http://www.mobile-files.com/forum/archive/index.php/t-584.html
    Private Sub ReadBBRegId()
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, 11055, New Byte() {}, "Read Bb reg id"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, 11089, New Byte() {}, "Read Bb reg id 2"))

    End Sub

    Private Sub WriteBBRegId(ByVal bbRegId As Byte())

        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, 11055, New Byte() {bbRegId(1), bbRegId(0)}, "Read Bb reg id"))

        '' 26 51 2B 01 D4 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF
        Dim regId2 As Byte() = {&H1, bbRegId(1), bbRegId(0), &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HFF}
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, 11089, regId2, "Read Bb reg id 2"))

    End Sub


    Private Sub WriteBbRegId(regId As String)
        Try
            dispatchQ.clearCommandQ()
            WriteBBRegId(String_To_Bytes(Integer.Parse(regId).ToString("x4")))
            dispatchQ.executeCommandQ()
        Catch ex As Exception
            Throw New Exception("Write Bb reg err: " + ex.ToString)
        End Try

    End Sub

    Private Sub WriteSID(sid As String, nid As String)
        dispatchQ.clearCommandQ()
        WriteSID(String_To_Bytes(Integer.Parse(sid).ToString("x4")), String_To_Bytes(Integer.Parse(nid).ToString("x4")))
        dispatchQ.executeCommandQ()
    End Sub

    Private Sub WriteSID(ByVal sid As Byte(), ByVal nid As Byte())
        Dim SidNid As New List(Of Byte)
        SidNid.Add(0)

        SidNid.Add(sid(1))
        SidNid.Add(sid(0))
        SidNid.Add(nid(1))
        SidNid.Add(nid(0))

        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_HOME_SID_NID_I, SidNid.ToArray, "Write SID/NID"))

    End Sub

    Public Shared ReadingRamToFile = False
    Public Shared ReadingRamFile As String

    Private Sub ReadRam(RamStartAddress As String, RamStartOffset As String, RamEndAddress As String, RamEndOffset As String, outFileName As String, search As Boolean)

        ReadingRamToFile = True
        RamReadQ.clearCommandQ()

        dispatchQ.clearCommandQ()
        ''myD.ReadRam(Integer.Parse(ReadRamStartAddressTextbox.Text), Integer.Parse(ReadRamEndAddressTextbox.Text), False)

        myD.ReadRam2(RamStartAddress + RamStartOffset, RamEndAddress + RamEndOffset)

        dispatchQ.executeCommandQ()

        RamReadQ.generateRamReadReport(outFileName)

        If search Then
            SearchBin(outFileName)
        End If

        Throw New Exception("Ram Read Complete")
    End Sub


    Private Sub SearchBin(ByVal fileName As String)
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
                    '' Throw new Exception("( " + d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString() + " )")


                    Dim possibleSpc As String = d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString()
                    If ResultsList.Contains(possibleSpc) = False Then
                        ResultsList.Add(possibleSpc)
                    End If

                    ''ResultsListBox1.Items.Add(d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString())
                End If

            Loop

            Throw New Exception("Search Bin For SPC Done")
        Else

            MsgBox("File Does Not Exist")

        End If


    End Sub

    Private Sub ReadAllNam()

        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_NAM_LOCK_I, New Byte() {}, "DIAG_NV_READ_F NV_NAM_LOCK_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DIR_NUMBER_I, New Byte() {}, "DIAG_NV_READ_F NV_DIR_NUMBER_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN1_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN1_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN2_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN2_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HOME_SID_NID_I, New Byte() {}, "DIAG_NV_READ_F NV_HOME_SID_NID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MEID_I, New Byte() {}, "DIAG_NV_READ_F NV_MEID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_LOCK_CODE_I, New Byte() {}, "DIAG_NV_READ_F NV_LOCK_CODE_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_VERNO_F, New Byte() {}, "DIAG_VERNO_F"))

        dispatchQ.executeCommandQ()

        DecodeMin()

    End Sub

    Private Sub ReadSingleNv(ByVal nv As NvItems.NVItems)
        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, nv, New Byte() {}, nv.ToString))
        dispatchQ.executeCommandQ()
    End Sub

    Private Sub ReadSingleQc(ByVal qc As Qcdm.Cmd)
        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(qc, qc.ToString))
        dispatchQ.executeCommandQ()
    End Sub

    Private Sub WriteNamLock(lockNam As Boolean)
        Dim namLock(1) As Byte
        If lockNam Then
            namLock(1) = 1
        End If

        dispatchQ.clearCommandQ()
        dispatchQ.addCommandToQ(New Command(DIAG_NV_WRITE_F, NV_NAM_LOCK_I, namLock, "NV_NAM_LOCK_I"))
        dispatchQ.executeCommandQ()
    End Sub

    Private Sub readNVList(ReadNvList As String, fileName As String)
        Try
            Dim nvItemList As String() = ReadNvList.Replace(",", "").Split(" ")
            ReadNvItemList(nvItemList)
            logger.addToLog("Reading NV List - This may take a while, do not unplug...")
            nvReadQ.generateNvReadReport(fileName)
            logger.addToLog("NV Read Complete")
        Catch ex As Exception
            Throw New Exception("Read nv list err: " + ex.ToString)

        End Try

    End Sub

    Public Sub ReadNvItemList(ByVal nvItemList As String())



        Try
            logger.addToLog("Reading NV List - This may take a while, do not unplug.")
            nvReadQ.clearCommandQ()


            For i = 0 To nvItemList.Count - 1

                If nvItemList(i).Contains("-") Then
                    Dim nv As New NvItems
                    Dim subNvRange As String() = nvItemList(i).Split("-")
                    nv.readNVItemRange(subNvRange(0), subNvRange(1))
                ElseIf True Then
                    Dim debugString As String = "readNVItemList DIAG_NV_READ_F " + nvItemList(i)

                    dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_NV_READ_F, Integer.Parse(nvItemList(i)), New Byte() {}, debugString))

                End If

            Next
            dispatchQ.executeCommandQ()
            logger.addToLog("Reading NV List - This may take a while, do not unplug..")
            nvReadQ.checkNvQForBadItems()
        Catch ex As Exception
            Throw New Exception("Read NV Item Range Err: " + ex.ToString)
        End Try


    End Sub

    Function ScanForReadableRam(ScanRamStart As String, ScanRamEnd As String) As List(Of String)
        Dim RamScanResultList As New List(Of String)

        ReadingRamToFile = True
        RamReadQ.clearCommandQ()

        dispatchQ.clearCommandQ()

        myD.ScanRam2(ScanRamStart + "0000", ScanRamEnd + "0000")

        dispatchQ.executeCommandQ()

        Dim R As List(Of String) = RamReadQ.generateRamScanReport()

        For Each s As String In R
            RamScanResultList.Add(s)
        Next

        logger.addToLog("Ram Read Complete")
        Return RamScanResultList
    End Function

    Private Sub SendAdb(ByVal Adb As String, AndroidSdkPath As String)

        Dim droid As New AndroidD
        Dim thisCmd As String() = {"cd " + AndroidSdkPath, "adb " + Adb}

        logger.addToLog(droid.SendCMD(thisCmd))

    End Sub

    Private Sub ReadAll()
        dispatchQ.clearCommandQ()
        ''evdo
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PPP_USER_ID_I, New Byte() {}, "Evdo NV_PPP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PPP_PASSWORD_I, New Byte() {}, "Evdo NV_PPP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PAP_USER_ID_I, New Byte() {}, "Evdo NV_PAP_USER_ID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_PAP_PASSWORD_I, New Byte() {}, "Evdo NV_PAP_PASSWORD_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_USER_ID_LONG_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_USER_ID_LONG_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_PASSWORD_LONG_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_PASSWD_LONG"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_NAI_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_NAI_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HDR_AN_AUTH_PASSWORD_I, New Byte() {}, "Evdo NV_HDR_AN_AUTH_PASSWORD_I"))
        ''evdo profiles/mode
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_QCMIP_I, New Byte() {}, "Evdo NV_DS_QCMIP_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_MIP_NUM_PROF_I, New Byte() {}, "Evdo NV_DS_MIP_NUM_PROF_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DS_MIP_ENABLE_PROF_I, New Byte() {}, "Evdo NV_DS_MIP_ENABLE_PROF_I"))
        ''nam1
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_NAM_LOCK_I, New Byte() {}, "DIAG_NV_READ_F NV_NAM_LOCK_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_DIR_NUMBER_I, New Byte() {}, "DIAG_NV_READ_F NV_DIR_NUMBER_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN1_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN1_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MIN2_I, New Byte() {}, "DIAG_NV_READ_F NV_MIN2_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_HOME_SID_NID_I, New Byte() {}, "DIAG_NV_READ_F NV_HOME_SID_NID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_MEID_I, New Byte() {}, "DIAG_NV_READ_F NV_MEID_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_NV_READ_F, NV_LOCK_CODE_I, New Byte() {}, "DIAG_NV_READ_F NV_LOCK_CODE_I"))
        dispatchQ.addCommandToQ(New Command(DIAG_VERNO_F, New Byte() {}, "DIAG_VERNO_F"))

        dispatchQ.executeCommandQ()

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
    '        Throw New Exception("Efs read err 2: " + ex.ToString)
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
        Throw New Exception("Loaded: " + myCarrier.Name + " " + myCarrier.Prl)
        Return myCarrier
    End Function
    Function loadModel(FileName As String, Carrier As Carrier, prlFilePath As String) As Model
        Dim myModel As New Model(FileName, Carrier, prlFilePath)
        Return myModel
    End Function

End Class
