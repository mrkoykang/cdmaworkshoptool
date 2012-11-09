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

Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Cryptography


Public Class esnConverter
    Public Shared Function ConversionSub(ByVal incomingToConvert) As String
        '    '' Dim serie As String = cdmaTerm.meidExtractedFromPacketTxtbox.Text.ToString
        Dim ret As String = ""
        Dim serie As String = incomingToConvert

        Select Case ChecaSerie(incomingToConvert)
            Case 0
                logger.add("Invalid MEID/ESN")
                '            Exit Select
            Case 1
                '            cdmaTerm.ResultsListBox.Items.Add("MEID/ESN Hexadecimal")
                '            cdmaTerm.ResultsListBox.Items.Add(" ")
                '            cdmaTerm.ResultsListBox.Items.Add("MEID/ESN Decimal: ")
                ret = (Convierte(1, serie))

                Exit Select
            Case 2

                '            cdmaTerm.ResultsListBox.Items.Add("MEID/ESN Decimal")
                '            cdmaTerm.ResultsListBox.Items.Add(" ")
                '            cdmaTerm.ResultsListBox.Items.Add("MEID/ESN Hexadecimal: ")
                ret = (Convierte(2, serie))
                Exit Select
            Case 3

                '            cdmaTerm.ResultsListBox.Items.Add("Meid Hexadecimal")
                '            cdmaTerm.ResultsListBox.Items.Add(" ")
                '            cdmaTerm.ResultsListBox.Items.Add(("Meid Decimal: "))
                '   ret = (Convierte(3, serie))
                '            cdmaTerm.ResultsListBox.Items.Add(("pESN Hexadecimal: "))
                '            cdmaTerm.ResultsListBox.Items.Add("80" & pesn(meidh2byte(serie)))
                '            cdmaTerm.ResultsListBox.Items.Add(("pESN Decimal: "))
                ret = Convierte(1, ("80" & pesn(meidh2byte(serie))))
                Exit Select
            Case 4

                '            cdmaTerm.ResultsListBox.Items.Add("Meid Decimal")
                '            cdmaTerm.ResultsListBox.Items.Add(" ")
                '            cdmaTerm.ResultsListBox.Items.Add(("Meid Hexadecimal: "))
                '            cdmaTerm.ResultsListBox.Items.Add(Convierte(4, serie))
                '            cdmaTerm.ResultsListBox.Items.Add(("pESN Hexadecimal:"))
                '            cdmaTerm.ResultsListBox.Items.Add("80" & pesn(meidh2byte(Convierte(4, serie))))
                '            cdmaTerm.ResultsListBox.Items.Add(("pESN Decimal: "))
                ret = (Convierte(1, ("80" & pesn(meidh2byte(Convierte(4, serie))))))
                Exit Select
            Case Else
                logger.add("Invalid MEID/ESN")
                '            Exit Select
        End Select
        Return ret
    End Function
    Public Shared Function hex2dec(ByVal hexa As String) As String
        Dim num As Double = 0
        hexa = hexa.ToLower
        Dim i As Integer = (hexa.Length - 1)
        Do While (i >= 0)
            If (hexa.Chars(i) = "a"c) Then
                num = (num + (10 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            ElseIf (hexa.Chars(i) = "b"c) Then
                num = (num + (11 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            ElseIf (hexa.Chars(i) = "c"c) Then
                num = (num + (12 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            ElseIf (hexa.Chars(i) = "d"c) Then
                num = (num + (13 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            ElseIf (hexa.Chars(i) = "e"c) Then
                num = (num + (14 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            ElseIf (hexa.Chars(i) = "f"c) Then
                num = (num + (15 * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            Else
                Dim ch As Char = hexa.Chars(i)
                num = (num + (Integer.Parse(ch.ToString) * Math.Pow(16, CDbl(((hexa.Length - 1) - i)))))
            End If
            i -= 1
        Loop
        Return num.ToString
    End Function

    Public Shared Function ChecaSerie(ByVal serie As String) As Integer
        Dim match As Match = Regex.Match(serie, "^([0-9a-fA-F]){8}$")
        Dim match2 As Match = Regex.Match(serie, "^([0-9]){11}$")
        Dim match3 As Match = Regex.Match(serie, "^([0-9a-fA-F]){14}$")
        Dim match4 As Match = Regex.Match(serie, "^([0-9]){18}$")
        If match.Success Then
            Return 1
        End If
        If match2.Success Then
            Return 2
        End If
        If match3.Success Then
            Return 3
        End If
        If match4.Success Then
            Return 4
        End If
        Return 0
    End Function

    Public Shared Function Convierte(ByVal tipo As Integer, ByVal serie As String) As String
        Dim length As Integer
        Dim num2 As Integer
        If (tipo = 1) Then
            Dim str As String = hex2dec(serie.Substring(0, 2))
            If (str.Length < 3) Then
                length = str.Length
                ''num2()
                For num2 = 0 To (3 - length) - 1
                    str = ("0" & str)
                Next num2
            End If
            If (str.Length > 3) Then
                Return "Serial Out Of Range"
            End If
            Dim str2 As String = hex2dec(serie.Substring(2))
            If (str2.Length < 8) Then
                length = str2.Length
                ''num2()
                For num2 = 0 To (8 - length) - 1
                    str2 = ("0" & str2)
                Next num2
            End If
            If (str2.Length > 8) Then
                Return "Serial Out Of Range"
            End If
            Return (str & str2)
        End If
        If (tipo = 2) Then
            Dim str3 As String = dec2hex(serie.Substring(0, 3))
            If (str3.Length < 2) Then
                str3 = ("0" & str3)
            End If
            If (str3.Length > 2) Then
                Return "Serial Out Of Range"
            End If
            Dim str4 As String = dec2hex(serie.Substring(3))
            If (str4.Length < 6) Then
                length = str4.Length
                ''num2()
                For num2 = 0 To (6 - length) - 1
                    str4 = ("0" & str4)
                Next num2
            End If
            If (str4.Length > 6) Then
                Return "Serial Out Of Range"
            End If
            Return (str3 & str4)
        End If
        If (tipo = 3) Then
            Dim str5 As String = hex2dec(serie.Substring(0, 8))
            If (str5.Length < 10) Then
                length = str5.Length
                ''num2()
                For num2 = 0 To (10 - length) - 1
                    str5 = ("0" & str5)
                Next num2
            End If
            If (str5.Length > 10) Then
                Return "Serial Out Of Range"
            End If
            Dim str6 As String = hex2dec(serie.Substring(8))
            If (str6.Length < 8) Then
                length = str6.Length
                ''num2()
                For num2 = 0 To (8 - length) - 1
                    str6 = ("0" & str6)
                Next num2
            End If
            If (str6.Length > 8) Then
                Return "Serial Out Of Range"
            End If
            Return (str5 & str6)
        End If
        If (tipo = 4) Then
            Dim str7 As String = dec2hex(serie.Substring(0, 10))
            If (str7.Length < 8) Then
                length = str7.Length
                ''num2()
                For num2 = 0 To (10 - length) - 1
                    str7 = ("0" & str7)
                Next num2
            End If
            If (str7.Length > 8) Then
                Return "Serial Out Of Range"
            End If
            Dim str8 As String = dec2hex(serie.Substring(10))
            If (str8.Length < 6) Then
                length = str8.Length
                ''num2()
                For num2 = 0 To (6 - length) - 1
                    str8 = ("0" & str8)
                Next num2
            End If
            If (str8.Length > 6) Then
                Return "Serial Out Of Range"
            End If
            Return (str7 & str8)
        End If
        Return "0"
    End Function

    Public Shared Function pesn(ByVal meid As Byte()) As String
        Dim buffer As Byte() = SHA1.Create.ComputeHash(meid)
        Dim builder As New StringBuilder
        Dim i As Integer
        For i = 0 To 3 - 1
            builder.Append(buffer((buffer.Length - (3 - i))).ToString("x2"))
        Next i
        Return builder.ToString
    End Function

    Public Shared Function meidh2byte(ByVal meid As String) As Byte()
        Dim buffer As Byte() = New Byte(7 - 1) {}
        Dim i As Integer
        For i = 0 To 7 - 1
            buffer(i) = CByte(Integer.Parse(hex2dec(meid.Substring((2 * i), 2))))
        Next i
        Return buffer
    End Function

    Public Shared Function dec2hex(ByVal dec As String) As String
        Dim str As String = Nothing
        Dim num2 As Double
        Dim i As Double = Double.Parse(dec)
        Do While (i > 0)
            Dim str2 As String
            num2 = (i Mod 16)
            If (num2 = 10) Then
                str2 = "a"
            ElseIf (num2 = 11) Then
                str2 = "b"
            ElseIf (num2 = 12) Then
                str2 = "c"
            ElseIf (num2 = 13) Then
                str2 = "d"
            ElseIf (num2 = 14) Then
                str2 = "e"
            ElseIf (num2 = 15) Then
                str2 = "f"
            Else
                str2 = num2.ToString
            End If
            str = (str2 & str)
            i = ((i - num2) / 16)
        Loop
        Return str.ToUpper
    End Function
End Class
