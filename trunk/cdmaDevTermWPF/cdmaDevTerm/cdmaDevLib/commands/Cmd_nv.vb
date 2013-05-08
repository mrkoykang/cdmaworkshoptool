Public Class Cmd_nv
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Property ItemNumber As Long
    Property ItemStatus As status
    Property ItemData As String

    Public Enum status
        Ok
        ParameterBad
        AccessDenied
        InactiveItem
    End Enum


    Public Sub New(ByVal c As Command)
        MyBase.New(c.currentQcdm, c.currentNv, New Byte() {}, c.debuggingText) ''TODO: not passing actual data?
        Try

            Dim nvItemNumberPart1 As String
            Dim nvItemNumberPart2 As String
            ''todo:wtfness - this &H14 should probably actually cause  or flag an err
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
            ItemNumber = decL

            If c.badNvRead Then
                ItemStatus = status.ParameterBad
                ItemData = ""
            ElseIf c.badSecurityNvRead Then
                ItemStatus = status.AccessDenied
                ItemData = ""
            ElseIf c.inactiveNvRead Then
                ItemStatus = status.InactiveItem
                ItemData = ""
            Else
                ItemStatus = status.Ok

            End If

            If ItemStatus = status.Ok Then


                ItemData = c.bytesRxd.ToHexString().Substring(6, 256)

            End If
        Catch ex As Exception

            logger.add("Nv item parse err: " + ex.ToString)
        End Try


    End Sub

    ''public getters and setters
    Public Function getItemNumber() As Long
        Return ItemNumber
    End Function

    Public Function getItemName() As String
        Return [Enum].GetName(GetType(NvItems.NvItems), ItemNumber)
    End Function


    Public Function getItemStatus() As String
        Return ItemStatus.ToString
    End Function

    Public Function getItemData() As String

        Return ItemData

    End Function

    Public Overrides Sub decode()
        Try
            ''todo: lazyness calling code lots of times 
            Dim HeaderLength = 6
            Dim HeaderAndCrcEofLength = HeaderLength + 6


            cdmaTerm.thePhoneRxd.NvItems(Me.currentNv) = New Nv(currentNv, _
                                                                SecretDecoderRing.trimFrontAndEndAscii(SecretDecoderRing.getAsciiStrings(bytesRxd)), _
                                                                "0x" + bytesRxd.ToHexString().Substring(HeaderLength, bytesRxd.ToHexString().Length - HeaderAndCrcEofLength))

            cdmaTerm.thePhone.NvItems(Me.currentNv) = New Nv(currentNv, _
                                                             SecretDecoderRing.trimFrontAndEndAscii(SecretDecoderRing.getAsciiStrings(bytesRxd)), _
                                                             "0x" + bytesRxd.ToHexString().Substring(HeaderLength, bytesRxd.ToHexString().Length - HeaderAndCrcEofLength))

        Catch ex As Exception
            logger.add("decode err:" + ex.ToString)

        End Try
    End Sub

End Class
