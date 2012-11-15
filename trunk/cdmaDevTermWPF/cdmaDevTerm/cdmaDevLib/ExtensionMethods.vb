Imports System.Runtime.CompilerServices

Public Module ExtensionMethods
    <Extension()> _
    Public Function ToHexBytes(ByVal InputValue As String) As Byte()
        Return HexStringToByteArray(InputValue.Replace(" ", String.Empty))
    End Function

    '' YAY!!! the internetz comes thru again
    '' http://programmerramblings.blogspot.com/2008/03/convert-hex-string-to-byte-array-and.html
    '' takes in the hex string and spits out the byte array
    Public Function HexStringToByteArray(ByVal strInput As String) As Byte()
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
            Logger.Add("HexStringToByteArray Conversion Error: " + ex.ToString)

            Return New Byte() {&HDE, &HAD, &HBE, &HEF}

        End Try
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

End Module

