﻿'' cdmaDevTerm
'' Copyright (c) Dillon Graham 2010-2013 Chromableed Studios
'' www.chromableedstudios.com
''     
'' cdmadevterm by ¿k? with help from ajh and jh and many others
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

Imports System.Threading
Imports System.Text
Imports cdmaDevLib.cdmaTerm

''Possibly the constructors in here will all be internal to the commandFactory only for decoding purposes

Public Class Command
    Inherits ICommand

    Public bytesToTx As Byte() ''outgoing byte array
    Public bytesRxd As Byte() = New Byte() {} ''incoming byte array
    Public appendCRC As Boolean = True ''should the crc+7e be added
    Public fixedLength As Boolean ''is the command a fixed length or varied with data packet
    Public length As Double ''length of byte response
    Public decoderString As String = "" ''string to be passed to the decoder
    Public commandSuccess As Boolean = True ''tracks whether the command was successfull (needed?)
    Public debuggingText As String ''string to define command for logging purposes
    Public currentQcdm As Qcdm.Cmd = Qcdm.Cmd.NOT_A_COMMAND ''current qcdm command
    Public currentNv As NvItems.NvItems = NvItems.NvItems.NOT_AN_NV_ITEM ''current nv item
    Public currentNvInt As Integer ''current nv item
    Public badNvRead As Boolean = False
    Public inactiveNvRead As Boolean = False
    Public badSecurityNvRead As Boolean = False
    Public Sent As Boolean = False

    '' COMMAND OBJECT
    ''not preferred command constructor for raw bytes (factory is better-might get decoded that way)
    '' Requires:
    '' New ( Byte(), String, String )
    '' New ( Command, decoderType, loggingTxt )
    Public Sub New(ByVal bytesIn As Byte(), ByVal decoderIn As String, ByVal debuggingTextIn As String)
        bytesToTx = bytesIn
        appendCRC = False
        decoderString = decoderIn
        debuggingText = debuggingTextIn
    End Sub


    ''command with just Byte Array to TX and Logging Text
    Public Sub New(ByVal bytesIn As Byte(), ByVal debuggingTextIn As String)
        bytesToTx = bytesIn
        appendCRC = False
        debuggingText = "Raw Byte(): " + debuggingTextIn
    End Sub

    Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal debuggingTextIn As String)
        ''sets the Class Level Variable used to decode the item
        currentQcdm = qcdm

        Dim qcdmArray As Byte() = {Byte.Parse(qcdm)}
        bytesToTx = cdmaTerm.myD.GetBufferWithCRC(qcdmArray)

        debuggingText = debuggingTextIn + " / new crc1"
    End Sub

    Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal qcData As Byte(), ByVal debuggingTextIn As String)
        ''sets the Class Level Variable used to decode the item
        currentQcdm = qcdm

        Dim packet As New List(Of Byte)

        packet.Add(Byte.Parse(qcdm))

        For Each b As Byte In qcData
            packet.Add(b)
        Next

        ''add crc
        bytesToTx = cdmaTerm.myD.GetBufferWithCRC(packet.ToArray)

        debuggingText = debuggingTextIn + " / new crc2"


    End Sub


    Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal nv As Integer, ByVal nvItemData As Byte(), ByVal debuggingTextIn As String)
        currentQcdm = qcdm
        currentNv = nv
        currentNvInt = nv

        Dim s As String = Integer.Parse(nv).ToString("X")
        While s.Length < 4
            s = "0" + s
        End While

        Dim request As New List(Of Byte)()

        request.Add(Byte.Parse(qcdm))
        request.Add(Convert.ToByte((s(2) + s(3)), 16))
        request.Add(Convert.ToByte((s(0) + s(1)), 16))
        For Each b As Byte In nvItemData
            request.Add(b)
        Next

        ''Fill to proper length with 0x00
        While request.Count < 133
            request.Add(&H0)
        End While


        bytesToTx = cdmaTerm.myD.GetBufferWithCRC(request.ToArray)

        debuggingText = debuggingTextIn + " / new crc3"

    End Sub

    Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal nv As NvItems.NvItems, ByVal nvItemData As Byte(), ByVal debuggingTextIn As String)

        currentQcdm = qcdm
        currentNv = nv
        Dim s As String = Integer.Parse(nv).ToString("X")
        While s.Length < 4
            s = "0" + s
        End While

        Dim request As New List(Of Byte)()

        request.Add(Byte.Parse(qcdm))
        request.Add(Convert.ToByte((s(2) + s(3)), 16))
        request.Add(Convert.ToByte((s(0) + s(1)), 16))
        For Each b As Byte In nvItemData
            request.Add(b)
        Next

        ''Fill to proper length with 0x00
        While request.Count < 133
            request.Add(&H0)
        End While

        ''new crc
        bytesToTx = cdmaTerm.myD.GetBufferWithCRC(request.ToArray)

        debuggingText = debuggingTextIn + " / new crc4"

    End Sub

    Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal subsys As Qcdm.SubsysCmd, ByVal data As Byte(), ByVal debuggingTextIn As String, ByVal StupiFixForBadConstructorClash As String)

        currentQcdm = qcdm

        Dim request As New List(Of Byte)()

        request.Add(Byte.Parse(qcdm))
        request.Add(Byte.Parse(subsys))

        For Each b As Byte In data
            request.Add(b)
        Next

        ''new crc
        bytesToTx = cdmaTerm.myD.GetBufferWithCRC(request.ToArray)

        debuggingText = debuggingTextIn + " / new crc4"

    End Sub
    
    Overridable Sub decode()

    End Sub

    ''function to send a bite array returns tru if it works
    Public Overrides Function tx() As Boolean
        If cdmaTerm.portIsOpen = False Then
            logger.add("Port not open err, please connect.")
            cdmaTerm.Q.InterruptQuiet()
        Else
            Try
                Dim port As New DmPort

                bytesRxd = port.WriteRead(bytesToTx)
                Sent = True

                ''untested fix for 7d/5e/5d return issue
                ''
                bytesRxd = port.unescapeReturnedBytes(bytesRxd)
                logger.add(vbNewLine + vbNewLine)
                logger.add(debuggingText)

                Dim appendString As String = vbNewLine + debuggingText +
                    vbNewLine + "TX: " + vbNewLine + hexSpace(bytesToTx.ToHexString()) + vbNewLine + vbNewLine +
                            "RX: " + vbNewLine + hexSpace(bytesRxd.ToHexString()) + vbNewLine
                appendString += vbNewLine + "RX(ascii): " + SecretDecoderRing.getAsciiStrings(bytesRxd) + vbNewLine + vbNewLine

                '' thePhone.SerialData = appendString
                logger.add(appendString)

                If bytesRxd.Count > 0 Then
                    Return True
                End If

            Catch ex As Exception
                logger.add("Command.tx err: " + ex.ToString)
                cdmaTerm.Q.Interrupt()
            End Try
        End If

        Return False
    End Function

    Private Function hexSpace(ByVal hexString As String) As String
        Dim sb As New System.Text.StringBuilder
        Try

            For i = 0 To hexString.Length - 1 Step 2
                sb.Append(hexString.Substring(i, 2) & " ")
            Next
        Catch
            Return sb.ToString
        End Try

        Return sb.ToString
    End Function

    Public Function bytesRxdLessHdlc() As Byte()
        If (bytesRxd.Count < 9) Then
            Return New Byte() {}
        End If
        Dim output As Byte() = New Byte(bytesRxd.Count - 10) {}
        Buffer.BlockCopy(bytesRxd, 6, output, 0, bytesRxd.Count - 9) ''todo: skip more than 2? suspect mystery numbers
        Return output
    End Function
    
    Public Function GetStatus() As String
        Dim result = "OK"
        If bytesRxd.Length < 136 Then
            badSecurityNvRead = True
            result = "Access denied"
        ElseIf bytesRxd(131) = 5 Or (bytesRxd.Length = 137 And bytesRxd(132) = 5) Then
            inactiveNvRead = True
            result = "Inactive item"
        ElseIf bytesRxd(131) = 6 Or (bytesRxd.Length = 137 And bytesRxd(132) = 6) Then
            badNvRead = True
            result = "Parameter bad"
        End If
        Return result
    End Function

    Public Function GetNvData() As String
        If (badNvRead Or badSecurityNvRead Or inactiveNvRead) Then
            Return ""
        End If

        Dim nvData As String = bytesRxd.ToHexString().Substring(6, 256)
        Dim ret =
        hexSpace(nvData.Substring(0, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(32, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(64, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(96, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(64, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(80, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(96, 32)) _
        + vbCrLf _
        + hexSpace(nvData.Substring(128, 32)) _
        + vbCrLf
        Return ret
    End Function

End Class