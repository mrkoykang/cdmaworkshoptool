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
Public Class calculateCRC

    ''sometimes profoundly stupid ways to do things are computationaly unimportant. eh.
    Function flipBitsOfSingleByteAsString(ByVal singleByte As String) As String

        If singleByte = "0" Then
            Return "0"
        ElseIf singleByte = "1" Then
            Return "8"
        ElseIf singleByte = "2" Then
            Return "4"
        ElseIf singleByte = "3" Then
            Return "C"
        ElseIf singleByte = "4" Then
            Return "2"
        ElseIf singleByte = "5" Then
            Return "A"
        ElseIf singleByte = "6" Then
            Return "6"
        ElseIf singleByte = "7" Then
            Return "E"
        ElseIf singleByte = "8" Then
            Return "1"
        ElseIf singleByte = "9" Then
            Return "9"
        ElseIf singleByte = "A" Then
            Return "5"
        ElseIf singleByte = "B" Then
            Return "D"
        ElseIf singleByte = "C" Then
            Return "3"
        ElseIf singleByte = "D" Then
            Return "B"
        ElseIf singleByte = "E" Then
            Return "7"
        Else
            Return "F"
        End If


    End Function
    Dim testArray() As Byte = {&H75, &H6E}
    Dim singleByte As Byte
    Dim wholeByteArray As Byte()

    ''not used anymore
    ''Public Function flipByte(ByVal inByte As Byte) As Byte
    ''    ''its easy as abc
    ''    Dim inString As String = inByte.ToString()
    ''    Dim a As String = inString(0)
    ''    Dim b As String = inString(1)
    ''    Dim c As String = (flipBitsOfSingleByteAsString(b) + flipBitsOfSingleByteAsString(a))
    ''    ''123 


    ''    Dim flippedByte As Byte = Val("&H" + c)
    ''    Return flippedByte
    ''End Function


    Public Function doStep3theInvert(ByVal inByte As Byte()) As Byte()
        ''think this does the inverse
        ''Dim test6() As Byte = {&H5B, &H16}

        ''TODO test code nt32 vs i nt64
        Dim theValueOfTheArray As Int64 = Val("&H" + cdmaTerm.biznytesToStrizings(inByte))
        Dim a As Boolean = True
        Dim workingWitBytes() As Byte = BitConverter.GetBytes((a Xor (theValueOfTheArray)))
        Dim dosBytes(1) As Byte
        dosBytes(0) = workingWitBytes(1)
        dosBytes(1) = workingWitBytes(0)
        ''dosbytes is out as byte array

        Return dosBytes
        ''MessageBox.Show("test6: " + cdmaTerm.biznytesToStrizings(dosBytes))

    End Function

    Public Function FLiPallBytesInByteArray(ByVal inByteArray As Byte()) As Byte()

        Try
            Dim buildABearBuildAString As String = ""
            Dim buildABearStartingString As String = cdmaTerm.biznytesToStrizings(inByteArray)


            ''TODO random test/ try and fix crc
            ''For n = 0 To (buildABearStartingString.Length / 2)
            For n = 0 To (buildABearStartingString.Length - 1)


                Dim a As String = flipBitsOfSingleByteAsString(buildABearStartingString(n))
                Dim b As String = flipBitsOfSingleByteAsString(buildABearStartingString(n + 1))

                buildABearBuildAString += b + a
                n = n + 1
            Next


            '' Return FLiPpeDbytaArray
            Return cdmaTerm.String_To_Bytes(buildABearBuildAString)

        Catch
            MessageBox.Show("calculateCRC.FLiPallBytesInByteArray error")

            Return testArray

        End Try
    End Function

End Class
