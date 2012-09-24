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
Public Enum InitialCrcValue
    Zeros
    NonZero1 = 65535
    NonZero2 = 7439
End Enum

Public Class cdmaCRC

 
    Const poly As UShort = 4129
    Private table As UShort() = New UShort(255) {}
    Private initialValue As UShort = 0

    Public Function ComputeChecksum(ByVal bytes As Byte()) As UShort
        Dim crc As UShort = Me.initialValue
        For i As Integer = 0 To bytes.Length - 1
            crc = CUShort(((crc << 8) Xor table(((crc >> 8) Xor (255 And bytes(i))))))
        Next
        Return crc
    End Function

    Public Function ComputeChecksumBytes(ByVal bytes As Byte()) As Byte()
        Dim crc As UShort = ComputeChecksum(bytes)
        Return New Byte() {CByte((crc >> 8)), CByte((crc And 255))}
    End Function

    Public Sub New(ByVal initialValue As InitialCrcValue)
        Me.initialValue = CUShort(initialValue)
        Dim temp As UShort, a As UShort
        For i As Integer = 0 To table.Length - 1
            temp = 0
            a = CUShort((i << 8))
            For j As Integer = 0 To 7
                If ((temp Xor a) And 32768) <> 0 Then
                    temp = CUShort(((temp << 1) Xor poly))
                Else
                    temp <<= 1
                End If
                a <<= 1
            Next
            table(i) = temp
        Next
    End Sub

End Class
