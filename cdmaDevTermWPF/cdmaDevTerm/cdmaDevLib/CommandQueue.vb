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
Imports System.ComponentModel

Public Class CommandQueue

    Private myQ As New Queue
    ''sync wrapper for queue
    Public mySynqdQ As Queue = Queue.Synchronized(myQ)
    Public badItemCount As Integer = 0
    Public inturruptCommandQFlag As Boolean = False

    Public Sub Add(ByRef inCommand As Command)
        mySynqdQ.Enqueue(inCommand)
    End Sub

    Public Sub Clear()

        mySynqdQ.Clear()
    End Sub

    Public Sub Interrupt()
        ''catch all errors and rx type
        Logger.Add("Transmit error in message queue")
        mySynqdQ.Clear()
    End Sub

    Public Sub InterruptQuiet()
        Logger.Add("Message queue was silently cleared")
        mySynqdQ.Clear()
    End Sub
    Private pendingOperations As Boolean = False
    ''Returns true if all commands execute
    Public Function Run() As Boolean
        'If (BackgroundWorker1.IsBusy Or pendingOperations) Then
        '    pendingOperations = True
        'Else
        ''BackgroundWorker1.RunWorkerAsync() ''still testing bgw
        doRun()
        'End If

        Return True ''todo:always returns true
    End Function

    Private Function doRun()
        SyncLock mySynqdQ.SyncRoot
            If cdmaTerm.portIsOpen = False Then
                Logger.Add("Dispatch Queue Error: Port Not Open, Please Connect", Logger.LogType.InfoAndMsg)

                InterruptQuiet()
            Else


                'For Each cmd In mySynqdQ
                '    cmd.commandSuccess = cmd.tx()
                '    If cmd.commandSuccess = False Then
                '        Interrupt()
                '        Return False
                '    End If
                '    cmd.decode()
                '    Logger.Add("q count: " + mySynqdQ.Count.ToString + Environment.NewLine + cmd.debuggingText & Environment.NewLine)
                'Next cmd
                While mySynqdQ.Count <> 0

                    Dim thisC As Command = mySynqdQ.Dequeue()
                    thisC.commandSuccess = thisC.tx()
                    If thisC.commandSuccess = False Then
                        Interrupt()
                        Return False
                    End If
                    thisC.decode()
                    Logger.Add("q count: " + mySynqdQ.Count.ToString + Environment.NewLine + thisC.debuggingText & Environment.NewLine)

                End While
            End If
        End SyncLock
        Return True
    End Function

    'Private WithEvents BackgroundWorker1 As New BackgroundWorker()


    'Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, _
    '                                     ByVal e As System.ComponentModel.DoWorkEventArgs) _
    '                                     Handles BackgroundWorker1.DoWork

    '    doRun()

    'End Sub


    'Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, _
    '                                                 ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
    '                                                 Handles BackgroundWorker1.RunWorkerCompleted

    '    Logger.Add("cmd run done...")
    '    If (pendingOperations) Then
    '        Logger.Add("pendingOperations...")

    '        BackgroundWorker1.RunWorkerAsync()
    '        pendingOperations = False
    '    End If


    'End Sub


    Friend Sub checkNvQForBadItems()
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
            Logger.Add("nv check err: " + ex.ToString)
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
            Logger.Add("Ram read err: " + ex.ToString)
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
            Logger.Add("Ram scan err: " + ex.ToString)
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
        SaveTextToFile("[NV items]", fileName, False)

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
                nvItemNumberPart1 = c.bytesToTx.ToHexString().Substring(4, 2)
                nvItemNumberPart2 = c.bytesToTx.ToHexString().Substring(2, 2)
            Else
                nvItemNumberPart1 = c.bytesRxd.ToHexString().Substring(4, 2)
                nvItemNumberPart2 = c.bytesRxd.ToHexString().Substring(2, 2)
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

                Dim nvData As String = c.bytesRxd.ToHexString().Substring(6, 256)
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

    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal Append As Boolean = True) As Boolean

        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath, Append)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            Logger.Add("SaveTextToFile err: " + Ex.ToString)
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
