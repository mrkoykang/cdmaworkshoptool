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

Imports System.Threading
Imports System.Text.RegularExpressions

Public Class DmPort
    Public Const ESC_COMPL As Byte = &H20
    Public Const ESC_ASYNC As Byte = &H7D
    Public Const FLAG_ASYNC As Byte = &H7E
    Public Const CRC_SEED As UInt16 = &HFFFF
    Public Const PRLPacketSize As Integer = 40

    Public ReadOnly CRCTable As UInt16() = {&H0, &H1189, &H2312, &H329B, &H4624, &H57AD, _
  &H6536, &H74BF, &H8C48, &H9DC1, &HAF5A, &HBED3, _
  &HCA6C, &HDBE5, &HE97E, &HF8F7, &H1081, &H108, _
  &H3393, &H221A, &H56A5, &H472C, &H75B7, &H643E, _
  &H9CC9, &H8D40, &HBFDB, &HAE52, &HDAED, &HCB64, _
  &HF9FF, &HE876, &H2102, &H308B, &H210, &H1399, _
  &H6726, &H76AF, &H4434, &H55BD, &HAD4A, &HBCC3, _
  &H8E58, &H9FD1, &HEB6E, &HFAE7, &HC87C, &HD9F5, _
  &H3183, &H200A, &H1291, &H318, &H77A7, &H662E, _
  &H54B5, &H453C, &HBDCB, &HAC42, &H9ED9, &H8F50, _
  &HFBEF, &HEA66, &HD8FD, &HC974, &H4204, &H538D, _
  &H6116, &H709F, &H420, &H15A9, &H2732, &H36BB, _
  &HCE4C, &HDFC5, &HED5E, &HFCD7, &H8868, &H99E1, _
  &HAB7A, &HBAF3, &H5285, &H430C, &H7197, &H601E, _
  &H14A1, &H528, &H37B3, &H263A, &HDECD, &HCF44, _
  &HFDDF, &HEC56, &H98E9, &H8960, &HBBFB, &HAA72, _
  &H6306, &H728F, &H4014, &H519D, &H2522, &H34AB, _
  &H630, &H17B9, &HEF4E, &HFEC7, &HCC5C, &HDDD5, _
  &HA96A, &HB8E3, &H8A78, &H9BF1, &H7387, &H620E, _
  &H5095, &H411C, &H35A3, &H242A, &H16B1, &H738, _
  &HFFCF, &HEE46, &HDCDD, &HCD54, &HB9EB, &HA862, _
  &H9AF9, &H8B70, &H8408, &H9581, &HA71A, &HB693, _
  &HC22C, &HD3A5, &HE13E, &HF0B7, &H840, &H19C9, _
  &H2B52, &H3ADB, &H4E64, &H5FED, &H6D76, &H7CFF, _
  &H9489, &H8500, &HB79B, &HA612, &HD2AD, &HC324, _
  &HF1BF, &HE036, &H18C1, &H948, &H3BD3, &H2A5A, _
  &H5EE5, &H4F6C, &H7DF7, &H6C7E, &HA50A, &HB483, _
  &H8618, &H9791, &HE32E, &HF2A7, &HC03C, &HD1B5, _
  &H2942, &H38CB, &HA50, &H1BD9, &H6F66, &H7EEF, _
  &H4C74, &H5DFD, &HB58B, &HA402, &H9699, &H8710, _
  &HF3AF, &HE226, &HD0BD, &HC134, &H39C3, &H284A, _
  &H1AD1, &HB58, &H7FE7, &H6E6E, &H5CF5, &H4D7C, _
  &HC60C, &HD785, &HE51E, &HF497, &H8028, &H91A1, _
  &HA33A, &HB2B3, &H4A44, &H5BCD, &H6956, &H78DF, _
  &HC60, &H1DE9, &H2F72, &H3EFB, &HD68D, &HC704, _
  &HF59F, &HE416, &H90A9, &H8120, &HB3BB, &HA232, _
  &H5AC5, &H4B4C, &H79D7, &H685E, &H1CE1, &HD68, _
  &H3FF3, &H2E7A, &HE70E, &HF687, &HC41C, &HD595, _
  &HA12A, &HB0A3, &H8238, &H93B1, &H6B46, &H7ACF, _
  &H4854, &H59DD, &H2D62, &H3CEB, &HE70, &H1FF9, _
  &HF78F, &HE606, &HD49D, &HC514, &HB1AB, &HA022, _
  &H92B9, &H8330, &H7BC7, &H6A4E, &H58D5, &H495C, _
  &H3DE3, &H2C6A, &H1EF1, &HF78}


    Public Function GetBufferWithCRC(ByVal s As String) As Byte()
        Return GetBufferWithCRC(cdmaTerm.String_To_Bytes(s), (s.Length / 2))
    End Function
    Public Function GetBufferWithCRC(ByVal bs As Byte()) As Byte()
        Return GetBufferWithCRC(bs, (bs.Length))
    End Function


    Private Function GetBufferWithCRC(ByVal data As Byte(), ByVal count As Integer) As Byte()
        Dim crc As UInt16 = 0
        Dim txBuffer = New List(Of Byte)()
        Dim result As Byte = 0
        Try


            For i As Integer = 0 To count - 1
                If CheckByte(result, data(i)) Then
                    txBuffer.Add(ESC_ASYNC)
                End If

                txBuffer.Add(result)
            Next

            crc = CRC_SEED

            For i As Integer = 0 To count - 1
                crc = ComputeCRC(crc, data(i))
            Next

            crc = crc Xor &HFFFF

            If CheckByte(result, Convert.ToByte(crc And &HFF)) Then
                txBuffer.Add(ESC_ASYNC)
            End If

            txBuffer.Add(result)

            If CheckByte(result, Convert.ToByte(crc >> 8 And &HFF)) Then
                txBuffer.Add(ESC_ASYNC)
            End If

            txBuffer.Add(result)

            txBuffer.Add(FLAG_ASYNC)

            '' Return txBuffer.ToArray(Of Byte)()
        Catch ex As Exception
            logger.addToLog("Crc err:" + ex.ToString)
            Return (txBuffer.ToArray)
        End Try

        Return txBuffer.ToArray
    End Function
    Private Function CheckByte(ByRef result As Byte, ByVal chkByte As Byte) As Boolean
        Select Case chkByte
            Case FLAG_ASYNC, ESC_ASYNC
                result = CByte(chkByte Xor ESC_COMPL)
                Return True
            Case Else
                result = chkByte
                Return False
        End Select
    End Function

    Private Function ComputeCRC(ByVal crc As UInt16, ByVal data As Byte) As UInt16
        Return Convert.ToUInt16((crc >> 8) Xor CRCTable((crc Xor data) And &HFF))
    End Function

    Public Sub WriteRead2(ByVal data As Byte(), ByRef result As Byte())
        Try

            ''cdmaTerm.mySerialPort2.Open()
            ''TODO:wtf
            ''GC.SuppressFinalize(cdmaTerm.mySerialPort.BaseStream)

        Catch ex As Exception
            logger.addToLog("DmPort Wr err:" + ex.ToString)
        End Try
        Try
            ''Dim txBuffer = cdmaTerm.gimmeCRC_AsByte_FromByte(data)
            Dim txBuffer = data

            ''cdmaTerm.mySerialPort.DiscardOutBuffer()
            '' cdmaTerm.mySerialPort.DiscardInBuffer()


            cdmaTerm.mySerialPort2.Write(txBuffer)

            '' Dim buffer = New Byte(cdmaTerm.mySerialPort2.ReadBufferSize - 1) {}
            Dim buffer = New Byte(&H1000) {}
            Dim responseList = New List(Of Byte)()

            ''kludgy fix for nv read timing err
            ''Thread.Sleep(50)

            ''test to fix random hang?
            '' Dim readCount As Integer = cdmaTerm.mySerialPort2.Read(buffer)
            Dim readCount As Integer = 0


            readCount = cdmaTerm.mySerialPort2.Read(buffer)

            ''Test for late response
            If readCount = 0 Then
                Thread.Sleep(150)

                readCount = cdmaTerm.mySerialPort2.Read(buffer)
            ElseIf ((buffer(0) = &H26) And readCount < 136) Then
                readCount = cdmaTerm.mySerialPort2.Read(buffer)
                ''test
                ''resend command for only half of response recieved
                Dim buffer2 = New Byte(&H1000) {}
                cdmaTerm.mySerialPort2.Write(txBuffer)
                readCount = cdmaTerm.mySerialPort2.Read(buffer2)
                Array.Copy(buffer2, buffer, buffer.Length)
            End If
            ''Test for REALLY late response
            If readCount = 0 Then
                Thread.Sleep(100)
                readCount = cdmaTerm.mySerialPort2.Read(buffer)
            Else
                For i As Integer = 0 To readCount - 1
                    responseList.Add(buffer(i))
                Next
            End If

            result = responseList.ToArray()

        Catch ex As Exception

            logger.addToLog("DmPort Tx Err: " + ex.ToString)
        Finally
        End Try

        ''Return Nothing
    End Sub

    Public Function WriteRead(ByVal data As Byte()) As Byte()
        Try

            ''cdmaTerm.mySerialPort2.Open()
            ''TODO:wtf
            ''GC.SuppressFinalize(cdmaTerm.mySerialPort.BaseStream)

        Catch ex As Exception
            logger.addToLog("DmPort Wr err:" + ex.ToString)
        End Try
        Try
            ''Dim txBuffer = cdmaTerm.gimmeCRC_AsByte_FromByte(data)
            Dim txBuffer = data

            ''cdmaTerm.mySerialPort.DiscardOutBuffer()
            '' cdmaTerm.mySerialPort.DiscardInBuffer()


            cdmaTerm.mySerialPort2.Write(txBuffer)

            '' Dim buffer = New Byte(cdmaTerm.mySerialPort2.ReadBufferSize - 1) {}
            Dim buffer = New Byte(&H1000) {}
            Dim responseList = New List(Of Byte)()

            ''kludgy fix for nv read timing err
            ''Thread.Sleep(50)

            ''test to fix random hang?
            '' Dim readCount As Integer = cdmaTerm.mySerialPort2.Read(buffer)
            Dim readCount As Integer = 0


            readCount = cdmaTerm.mySerialPort2.Read(buffer)

            ''Test for late response
            If readCount = 0 Then
                Thread.Sleep(150)

                readCount = cdmaTerm.mySerialPort2.Read(buffer)
            ElseIf ((buffer(0) = &H26) And readCount < 136) Then
                readCount = cdmaTerm.mySerialPort2.Read(buffer)
                ''test
                ''resend command for only half of response recieved
                Dim buffer2 = New Byte(&H1000) {}
                cdmaTerm.mySerialPort2.Write(txBuffer)
                readCount = cdmaTerm.mySerialPort2.Read(buffer2)
                Array.Copy(buffer2, buffer, buffer.Length)
            End If
            ''Test for REALLY late response
            If readCount = 0 Then
                Thread.Sleep(100)
                readCount = cdmaTerm.mySerialPort2.Read(buffer)
            Else
                For i As Integer = 0 To readCount - 1
                    responseList.Add(buffer(i))
                Next
            End If

            Return responseList.ToArray()

        Catch ex As Exception

            logger.addToLog("DmPort Tx Err: " + ex.ToString)
        Finally
        End Try

        Return Nothing
    End Function



    Public Function FindSPC(ByVal startAddress As UInt32, ByVal endAddress As UInt32, ByVal [resume] As Boolean) As String()
        Dim buffer__1 = New Byte(1023) {}
        Dim start As UInt32

        'If [resume] Then
        '    start = _lastAddress
        'Else
        '    start = startAddress
        'End If

        For j As Integer = 0 To 1023
            '' NotifyEvent(StatusTypes.Information, String.Format("Reading 1KB starting at {0:X}", start))
            For i As Integer = 1 To 63
                Dim response = WriteRead(New Byte() {4, Convert.ToByte(start And &HFF), Convert.ToByte((start And &HFF00) >> 8), Convert.ToByte((start And &HFF0000) >> 16), Convert.ToByte((start And &HFF000000UI) >> 24), 4, _
                 0})
                If response(0) = 4 Then
                    Buffer.BlockCopy(response, 7, buffer__1, i * 16, 16)
                End If

                start = start + 16
                ''  _lastAddress = start
            Next

            Buffer.BlockCopy(buffer__1, 1008, buffer__1, 0, 16)
            Dim enc As New System.Text.ASCIIEncoding()

            Dim matches = Regex.Matches(enc.GetString(buffer__1), "[0-9][0-9][0-9][0-9][0-9][0-9]")

            If matches.Count > 0 Then
                ''NotifyEvent(StatusTypes.Information, String.Format("{0} SPC candidate(s) found", matches.Count))
                logger.addToLog((matches.Count + " SPC candidate(s) found"))
                '' Return (From m In matches).ToArray(Of String)()

            Else
                logger.addToLog("No SPC candidate found in this iteration")
                ''NotifyEvent(StatusTypes.Information, "No SPC candidate found in this iteration")
            End If
        Next

        Return Nothing
    End Function

    Public Function ReadRam(ByVal startAddress As UInt32, ByVal endAddress As UInt32, ByVal [resume] As Boolean) As String()

        '' Dim buffer__1 = New Byte(1023) {}
        Dim start As UInt32
        start = startAddress

        For j As Integer = 0 To 1023

            '' NotifyEvent(StatusTypes.Information, String.Format("Reading 1KB starting at {0:X}", start))
            For i As Integer = 1 To 63
                Dim request As Byte() = {4, Convert.ToByte(start And &HFF), Convert.ToByte((start And &HFF00) >> 8), Convert.ToByte((start And &HFF0000) >> 16), Convert.ToByte((start And &HFF000000UI) >> 24), 4, _
                 0}
                ''car ramrod?

                request = GetBufferWithCRC(request)

                cdmaTerm.dispatchQ.addCommandToQ(New Command(request, "Ram Read i: " + i.ToString + "j: " + j.ToString))
                '' Dim response = WriteRead(New Byte() {4, Convert.ToByte(start And &HFF), Convert.ToByte((start And &HFF00) >> 8), Convert.ToByte((start And &HFF0000) >> 16), Convert.ToByte((start And &HFF000000UI) >> 24), 4, _
                '' 0})

                ''If response(0) = 4 Then
                ''Buffer.BlockCopy(response, 7, buffer__1, i * 16, 16)
                ''End If

                start = start + 16
                ''  _lastAddress = start
            Next

            '' Buffer.BlockCopy(buffer__1, 1008, buffer__1, 0, 16)


            '' Dim enc As New System.Text.ASCIIEncoding()

            '' Dim matches = Regex.Matches(enc.GetString(buffer__1), "[0-9][0-9][0-9][0-9][0-9][0-9]")


        Next

        Return Nothing
    End Function

    Public Function ScanRam2(ByVal startAddress As String, ByVal endAddress As String) As Boolean

        '' Dim buffer__1 = New Byte(1023) {}
        Dim startAdr As Long
        Dim endAdr As Long
        Dim current As Long
        startAdr = Long.Parse(startAddress, Globalization.NumberStyles.HexNumber)
        endAdr = Long.Parse(endAddress, Globalization.NumberStyles.HexNumber)

        current = startAdr

        While current <= endAdr

            Dim request As New List(Of Byte)
            Dim addressBs As Byte() = cdmaTerm.String_To_Bytes(current.ToString("x8"))
            request.Add(addressBs(3))
            request.Add(addressBs(2))
            request.Add(addressBs(1))
            request.Add(addressBs(0))


            request.Add(&H10)
            request.Add(0)

            cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_PEEKB_F, request.ToArray, ("DIAG_PEEKB_F: " + current.ToString("x8").ToUpper)))


            current = current + &H10000
        End While



        Return True

    End Function

    Public Function ReadRam2(ByVal startAddress As String, ByVal endAddress As String) As Boolean

        '' Dim buffer__1 = New Byte(1023) {}
        Dim startAdr As Long
        Dim endAdr As Long
        Dim current As Long
        startAdr = Long.Parse(startAddress, Globalization.NumberStyles.HexNumber)
        endAdr = Long.Parse(endAddress, Globalization.NumberStyles.HexNumber)

        current = startAdr

        While current <= endAdr

            Dim request As New List(Of Byte)
            Dim addressBs As Byte() = cdmaTerm.String_To_Bytes(current.ToString("x8"))
            request.Add(addressBs(3))
            request.Add(addressBs(2))
            request.Add(addressBs(1))
            request.Add(addressBs(0))


            request.Add(&H10)
            request.Add(0)

            cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_PEEKB_F, request.ToArray, ("DIAG_PEEKB_F: " + current.ToString("x8"))))


            current = current + &H10
        End While



        Return True

    End Function

    Function unescapeReturnedBytes(ByVal bytes As Byte()) As Byte()
        Dim fixedBytes As New List(Of Byte)
        Dim count As Integer = bytes.Count
        Try

            For i As Integer = 0 To count - 1
                If i + 1 < count - 1 Then
                    If bytes(i) = &H7D And bytes(i + 1) = &H5D Then
                        fixedBytes.Add(&H7D)
                        i = i + 1
                    ElseIf i < count - 2 AndAlso bytes(i) = &H7D And bytes(i + 1) = &H5E Then
                        fixedBytes.Add(&H7E)
                        i = i + 1
                    Else
                        fixedBytes.Add(bytes(i))
                    End If

                Else
                    fixedBytes.Add(bytes(i))
                End If

            Next

        Catch ex As Exception
            logger.addToLog("unescape err: " + ex.ToString)
        End Try
        Return fixedBytes.ToArray
    End Function

End Class
