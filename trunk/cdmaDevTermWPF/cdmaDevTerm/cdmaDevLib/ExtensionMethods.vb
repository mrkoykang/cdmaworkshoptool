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
Imports System.Runtime.CompilerServices

''ByteExtensions
<Assembly: Microsoft.Scripting.Runtime.ExtensionType(GetType(System.Array), GetType(cdmaDevLib.ExtensionMethods))> 

''StringExtensions
<Assembly: Microsoft.Scripting.Runtime.ExtensionType(GetType(String), GetType(cdmaDevLib.ExtensionMethods))> 

Public Module ExtensionMethods

#Region "StringExtensions"

    <Extension()> _
    Public Function ToHexBytes(ByVal InputValue As String) As Byte()
        Return HexStringToBytes(InputValue.Replace(" ", String.Empty))
    End Function

    ''ref: http://programmerramblings.blogspot.com/2008/03/convert-hex-string-to-byte-array-and.html
    Public Function HexStringToBytes(ByVal strInput As String) As Byte()
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
                Dim lngDecimal As Long = Convert.ToInt64(strInput.Substring(i, 2), 16)
                bytes(x) = Convert.ToByte(lngDecimal)
                i = i + 2
                x += 1
            End While
            Return bytes
        Catch ex As Exception
            Logger.Add("HexStringToByteArray Conversion Error: " + ex.ToString)
            Return New Byte() {}
        End Try
    End Function

    Public Function HexStringHasCrcEof(ByVal strInput As String) As Boolean

        Dim arr = strInput.ToHexBytes()
        If (arr.Length < 4 Or arr.Last() <> &H7E) Then
            Return False
        End If

        Return cdmaTerm.myD.GetBufferWithCRC(arr.SubArray(0, arr.Length - 3)).ToHexString = strInput

    End Function

#End Region

#Region "ByteExtensions"

    <Extension()> _
    Public Function SubArray(ByVal input As Byte(), offset As Integer, length As Integer) As Byte()
        Return input.Skip(offset).Take(length)
    End Function

    <Extension()> _
    Public Function ToHexString(ByVal InputValue As Byte()) As String
        Return BytesToHexString(InputValue)
    End Function

    Function BytesToHexString(ByVal byteInput() As Byte) As String
        Dim ascStr As String = ""
        Dim returnStr As String = ""
        Try

            For Each b As Byte In byteInput
                ascStr += Chr(b)        'Ascii String
                returnStr += Hex(b).PadLeft(2, "0")     'Hex String (Modified Padding, to intake compulsory 2 chars, mainly in case of 0)
            Next

        Catch ex As Exception
            Logger.Add("biz err: " + ex.ToString)
        End Try
        ''returns "" if try catch fails
        Return (returnStr)
    End Function

#End Region

End Module

