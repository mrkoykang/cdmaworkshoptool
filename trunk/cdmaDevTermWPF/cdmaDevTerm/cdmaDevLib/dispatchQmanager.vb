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

Imports System
Imports System.Collections
Imports System.IO
Imports System.Threading

Public Class dispatchQmanager


    Private myQ As New Queue
    ''sync wrapper for queue
    Public mySynqdQ As Queue = Queue.Synchronized(myQ)
    Public badItemCount As Integer = 0
    Public inturruptCommandQFlag As Boolean = False

    Public Sub add(ByVal inCommand As ICommand)
        mySynqdQ.Enqueue(inCommand)
    End Sub

    Public Sub clearCommandQ()

        mySynqdQ.Clear()

    End Sub
    Public Sub interruptCommandQ()

        ''catch all errors and rx type
        logger.addToLog("Transmit error in message queue")
        mySynqdQ.Clear()

    End Sub
    Public Sub silentInterruptCommandQ()

        logger.addToLog("Message queue was silently cleared")
        mySynqdQ.Clear()

    End Sub

    ''Returns true if all commands execute
    Public Function executeCommandQ() As Boolean

        If cdmaTerm.portIsOpen = False Then
            logger.addToLog("Dispatch Queue Error: Port Not Open, Please Connect")
            silentInterruptCommandQ()
        Else

            Dim whileWaiting As Boolean
            whileWaiting = False
            ''for i to q.lengh
            For i = 0 To (mySynqdQ.Count - 1)

                ''check for empty queue
                If mySynqdQ.Count = 0 Then
                    Exit For
                End If

                ''dequeue current command into 
                ''command object to be used for this loop
                Dim thisC As Command = mySynqdQ.Dequeue()


                'Dim worker As New Thread(AddressOf thisC.tx)
                'worker.Start()
                'If Not worker.Join(TimeSpan.FromSeconds(2)) Then
                '    worker.Abort()
                '    logger.addToLog("Timedout! ERR")
                'End If
                thisC.tx()

                ''send the command
                thisC.decode()

                If thisC.commandSuccess = False Then
                    interruptCommandQ()
                    Return False
                End If

                ''TODO???

                ''cdmaTerm.logAllBox += thisC.commandNameSent
                logger.addToLog("-Q" + i.ToString + ": " + thisC.debuggingText & vbNewLine)

            Next

        End If
        Return True

    End Function

    Public Sub checkNvQForBadItems()
        Try

            For Each c As Command In mySynqdQ

                If c.bytesRxd.Length < 136 Then
                    c.badSecurityNvRead = True
                    badItemCount += 1
                ElseIf c.bytesRxd(131) = 5 Or (c.bytesRxd.Length = 137 And c.bytesRxd(132) = 5) Then

                    '' TEST RX: 267D5E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000500206C7E

                    'inactive or bad?
                    ''c.badNvRead = True
                    c.inactiveNvRead = True
                    badItemCount += 1
                ElseIf c.bytesRxd(131) = 6 Or (c.bytesRxd.Length = 137 And c.bytesRxd(132) = 6) Then

                    'inactive or bad?
                    c.badNvRead = True
                    ''c.emptyNvRead = True
                    badItemCount += 1
                Else
                    ''not needed?
                    'Dim nvData As String = cdmaTerm.biznytesToStrizings(c.bytesRxd).Substring(6, 256)
                    'If nvData.CompareTo(emptyA) = 0 Then
                    '    ''if nv data is empty
                    '    ''compare nvdata to empty array
                    '    c.emptyNvRead = True

                    '    badItemCount += 1
                    'End If

                End If

            Next

        Catch ex As Exception
            logger.addToLog("nv check err: " + ex.ToString)
        End Try

    End Sub
    'Public Sub generateRamBin(ByVal fileName As String)

    '    For Each c As Command In mySynqdQ
    '        ''.Substring(4, 2)
    '        SaveTextToFile(cdmaTerm.biznytesToStrizings(c.bytesRxd), fileName)
    '    Next


    'End Sub

    Public Sub generateRamReadReport(ByVal fileName As String)
        Dim myFileStream As FileStream
        Try
            myFileStream = File.OpenWrite(fileName)

            For Each c As Command In mySynqdQ

                If c.bytesRxd.Count > 22 Then
                    For i As Integer = 7 To 22

                        myFileStream.WriteByte(c.bytesRxd(i))

                    Next
                End If


                'For lngLoop = 0 To intByte - 1
                '    myFileStream.WriteByte(bteWrite(lngLoop))
                'Next
            Next


            myFileStream.Close()
        Catch ex As Exception
            logger.addToLog("Ram read err: " + ex.ToString)
        End Try



    End Sub

    Public Function generateRamScanReport() As List(Of String)
        Dim Ranges As New List(Of String)
        Try

            Dim startAddress As String = ""
            Dim endAddress As String = ""
            Dim inReadableRange As Boolean = False



            For Each c As Command In mySynqdQ

                If c.bytesRxd.Count > 22 Then
                    If inReadableRange = False Then
                        startAddress = c.bytesRxd(4).ToString("x2") + c.bytesRxd(3).ToString("x2")
                        inReadableRange = True
                    End If


                    '' For i As Integer = 7 To 22

                    ''     myFileStream.WriteByte(c.bytesRxd(i))

                    ''Next
                Else
                    If inReadableRange Then
                        endAddress = c.bytesRxd(5).ToString("x2") + c.bytesRxd(4).ToString("x2")
                        inReadableRange = False
                        Ranges.Add(startAddress.ToUpper + " : " + endAddress.ToUpper)
                    End If

                End If


                'For lngLoop = 0 To intByte - 1
                '    myFileStream.WriteByte(bteWrite(lngLoop))
                'Next
            Next


        Catch ex As Exception
            logger.addToLog("Ram scan err: " + ex.ToString)
        End Try

        Return Ranges

    End Function



    Public Sub generateNvReadReport(ByVal fileName As String)
        ''this is what the file to write looks like

        '        [NV Items]

        ''if ver 2.7
        '[Complete items - 12]
        ''if ver 3.5
        '[Complete items - 12, Items size - 128]

        '0318 (0x013E)   -   OK
        '1F 31 32 33 34 35 36 37 38 39 30 40 65 78 61 6D 
        '70 6C 65 63 65 6C 6C 75 6C 61 72 31 2E 6E 65 74 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 

        ''open a reader(fileName), read into array?

        ''print out header/pick 2.7/3.5?

        ''SaveTextToFile(vbCrLf, fileName)

        ''2.7
        '' SaveTextToFile("[NV Items]", fileName)
        ''3.5
        SaveTextToFile("[NV items]", fileName)

        SaveTextToFile(vbCrLf, fileName)

        ''2.7
        ''Dim completeItems As String = "[Complete items - " + mySynqdQ.Count.ToString + "]"
        ''3.5
        Dim completeItems As String = "[Complete items - " + (mySynqdQ.Count - badItemCount).ToString + ", Items size - 128]"


        SaveTextToFile(completeItems, fileName)
        SaveTextToFile(vbCrLf, fileName)
        SaveTextToFile(vbCrLf, fileName)


        For Each c As Command In mySynqdQ

            '' logger.addToLog("rxd: " + cdmaTerm.biznytesToStrizings(c.bytesRxd))
            Dim nvOutputArray As New List(Of String)
            ''20 38 25 bad response length 20 results in arg out of range
            ''


            ''use the sent data to grab the item #
            ''test to fix 7d 5e bug
            Dim nvItemNumberPart1 As String
            Dim nvItemNumberPart2 As String

            If c.bytesRxd(0) = &H14 Then
                nvItemNumberPart1 = cdmaTerm.biznytesToStrizings(c.bytesToTx).Substring(4, 2)
                nvItemNumberPart2 = cdmaTerm.biznytesToStrizings(c.bytesToTx).Substring(2, 2)
            Else
                nvItemNumberPart1 = cdmaTerm.biznytesToStrizings(c.bytesRxd).Substring(4, 2)
                nvItemNumberPart2 = cdmaTerm.biznytesToStrizings(c.bytesRxd).Substring(2, 2)
            End If





            Dim nvItemNumberS As String = nvItemNumberPart1 + nvItemNumberPart2
            ''0085 (0x0055)   -   OK
            Dim hexString As String = nvItemNumberS


            Dim decL As Long = Long.Parse(hexString, Globalization.NumberStyles.HexNumber)

            If c.badNvRead Then

                Dim itemString As String = decL.ToString("d5") + " (0x" + hexString + ")   -   Parameter bad"
                SaveTextToFile(itemString, fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(vbCrLf, fileName)
            ElseIf c.badSecurityNvRead Then
                Dim itemString As String = decL.ToString("d5") + " (0x" + hexString + ")   -   Access denied"
                SaveTextToFile(itemString, fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(vbCrLf, fileName)
            ElseIf c.inactiveNvRead Then
                Dim itemString As String = decL.ToString("d5") + " (0x" + hexString + ")   -   Inactive item"
                SaveTextToFile(itemString, fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(vbCrLf, fileName)
            Else

                Dim nvData As String = cdmaTerm.biznytesToStrizings(c.bytesRxd).Substring(6, 256)
                ''3.5 dec length 5 hex length 

                Dim itemString As String = decL.ToString("d5") + " (0x" + hexString + ")   -   OK"


                '' logger.addToLog("nvItemNumberS: " + nvItemNumberS + " itemString: " + itemString)
                ''logger.addToLog("num " + nvItemNumberS + " rxd: " + nvData)

                SaveTextToFile(itemString, fileName)
                SaveTextToFile(vbCrLf, fileName)
                ''TODO:TEST IF THIS IS ACTUALLY THE WHOLE PACKET?
                SaveTextToFile(hexSpace(nvData.Substring(0, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(32, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(64, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(96, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(64, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(80, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(96, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(hexSpace(nvData.Substring(128, 32)), fileName)
                SaveTextToFile(vbCrLf, fileName)
                SaveTextToFile(vbCrLf, fileName)


            End If


        Next





    End Sub

    ''  SaveTextToFile()

    Public Function SaveTextToFile(ByVal strData As String, _
 ByVal FullPath As String, _
   Optional ByVal ErrInfo As String = "") As Boolean

        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try


            objReader = New StreamWriter(FullPath, True)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message

        End Try
        Return bAns
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


    Public Function GetCount() As Integer
        Return mySynqdQ.Count
    End Function


End Class
