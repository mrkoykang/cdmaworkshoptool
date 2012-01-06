''cwTool / cT .5d - by ¿kernelpanic?
''Copyright (c) 2009-2011 Chromableed Studios 
''www.chromableedstudios.com
''
''provided as-is under the GPL v3

Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Cryptography

Public Class cwTool
    Public Function OpenFile() As String

        OpenFD.InitialDirectory = "C:\"
        OpenFD.Title = "Select the file to parse.."
        OpenFD.Filter = "Any Files|*.*|Text Files|*.txt|Binary Files|*.bin"
        OpenFD.ShowDialog()
        Return OpenFD.FileName

    End Function
    Public Function SaveFile() As String

        SaveFD.InitialDirectory = "C:\"
        SaveFD.Title = "Save Ascii Hex File as.."
        SaveFD.Filter = "Any Files|*.*|Text Files|*.txt|Binary Files|*.bin"
        SaveFD.ShowDialog()
        Return SaveFD.FileName

    End Function

    Dim InputFileName As String
    Private Sub RemovePlanTextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemovePlanTextButton.Click

        ''take the nv item read and remove plain text
        DoTheParse(OpenFile(), SaveFile())

    End Sub

    Public Sub DoTheParse(ByVal fileIn As String, ByVal fileOut As String)

        Dim TextLine As String

        If System.IO.File.Exists(fileIn) = True Then

            Dim objReader As New System.IO.StreamReader(fileIn)
            Dim objWriter As New System.IO.StreamWriter(fileOut)

            Do While objReader.Peek() <> -1

                TextLine = objReader.ReadLine()

                If TextLine.Contains("[") = True Then

                ElseIf TextLine.Contains("(") = True Then

                Else
                    objWriter.WriteLine(TextLine)
                End If

            Loop

            objWriter.Close()
            MessageBox.Show("Done.")
        Else

            MsgBox("File Does Not Exist")

        End If
    End Sub

    Private Sub searchForSpcButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchForSpcButton.Click
        ''clear any previous results
        ResultsListBox.Items.Clear()
        ''run the check
        RegExpSpc()
        ''copy first spc to clipboard
        CopyFirstSPC()

    End Sub

    Sub CopyFirstSPC()
        Try
            ''copy the spc if there is one
            Dim FirstSPC As String = ResultsListBox.Items(0)
            Clipboard.SetDataObject(FirstSPC)
        Catch
            ''lazy
        End Try
    End Sub

    Sub RegExpSpc()


        Dim FILE_NAME2 As String = OpenFile()


        If System.IO.File.Exists(FILE_NAME2) = True Then

            Dim objReader1 As New System.IO.StreamReader(FILE_NAME2)


            Dim NextRegexLine As String ''= "623456"
            Do While objReader1.Peek() <> -1

                NextRegexLine = objReader1.ReadLine()

                Dim re1 As String = "(\d)"    'Any Single Digit 1
                Dim re2 As String = "(\d)"    'Any Single Digit 2
                Dim re3 As String = "(\d)"    'Any Single Digit 3
                Dim re4 As String = "(\d)"    'Any Single Digit 4
                Dim re5 As String = "(\d)"    'Any Single Digit 5
                Dim re6 As String = "(\d)"    'Any Single Digit 6

                Dim r As Regex = New Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(NextRegexLine)
                If (m.Success) Then
                    Dim d1 = m.Groups(1)
                    Dim d2 = m.Groups(2)
                    Dim d3 = m.Groups(3)
                    Dim d4 = m.Groups(4)
                    Dim d5 = m.Groups(5)
                    Dim d6 = m.Groups(6)
                    '' MessageBox.Show("( " + d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString() + " )")

                    ResultsListBox.Items.Add(d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString() + d5.ToString() + d6.ToString())
                End If

            Loop

            MessageBox.Show("done")
        Else

            MsgBox("File Does Not Exist")

        End If

    End Sub

    Private Sub ResultsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResultsListBox.SelectedIndexChanged
        Try
            Dim SelectedSPC As String = ResultsListBox.SelectedItem
            Clipboard.SetDataObject(SelectedSPC)
        Catch
        End Try
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        MessageBox.Show("cwTool is Provided as-is under the terms of the GPL v3 chromableedstudios ( a+ ) gmail ( do+ ) com (c) Copyright Chromableed Studios www.chromableedstudios.com")
    End Sub

    Private Sub ConvertMeidHexToDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertMeidHexToDec.Click
        ResultsListBox.Items.Clear()
        ''try new code for meid conversion
        ''using this code with permisson from badillo of GSM forum see readme for more info

        OnMeidConversionSub()
        ''more debugging nonsense.. ima lil sloppy, nice to be able to uncomment and see right away tho
        '' MessageBox.Show("hi" + MeidConvertInBox.Text)

    End Sub

    Sub OnMeidConversionSub()
        Dim serie As String = Me.MeidConvertInBox.Text.ToString
        Select Case ChecaSerie(serie)
            Case 0
                Me.ResultsListBox.Items.Add("Invalid MEID/ESN")
                Exit Select
            Case 1
                Me.ResultsListBox.Items.Add("MEID/ESN Hexadecimal")
                Me.ResultsListBox.Items.Add(" ")
                Me.ResultsListBox.Items.Add("MEID/ESN Decimal: ")
                Me.ResultsListBox.Items.Add(Convierte(1, serie))

                Exit Select
            Case 2

                Me.ResultsListBox.Items.Add("MEID/ESN Decimal")
                Me.ResultsListBox.Items.Add(" ")
                Me.ResultsListBox.Items.Add("MEID/ESN Hexadecimal: ")
                Me.ResultsListBox.Items.Add(Convierte(2, serie))
                Exit Select
            Case 3

                Me.ResultsListBox.Items.Add("Meid Hexadecimal")
                Me.ResultsListBox.Items.Add(" ")
                Me.ResultsListBox.Items.Add(("Meid Decimal: "))
                Me.ResultsListBox.Items.Add(Convierte(3, serie))
                Me.ResultsListBox.Items.Add(("pESN Hexadecimal: "))
                Me.ResultsListBox.Items.Add("80" & pesn(meidh2byte(serie)))
                Me.ResultsListBox.Items.Add(("pESN Decimal: "))
                Me.ResultsListBox.Items.Add(Convierte(1, ("80" & pesn(meidh2byte(serie)))))
                Exit Select
            Case 4

                Me.ResultsListBox.Items.Add("Meid Decimal")
                Me.ResultsListBox.Items.Add(" ")
                Me.ResultsListBox.Items.Add(("Meid Hexadecimal: "))
                Me.ResultsListBox.Items.Add(Convierte(4, serie))
                Me.ResultsListBox.Items.Add(("pESN Hexadecimal:"))
                Me.ResultsListBox.Items.Add("80" & pesn(meidh2byte(Convierte(4, serie))))
                Me.ResultsListBox.Items.Add(("pESN Decimal: "))
                Me.ResultsListBox.Items.Add(Convierte(1, ("80" & pesn(meidh2byte(Convierte(4, serie))))))
                Exit Select
            Case Else
                Me.ResultsListBox.Items.Add("Invalid MEID/ESN")
                Exit Select
        End Select
    End Sub

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

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "aseriesoftubes" Then
            ResultsListBox.Items.Clear()
            findInterestingNV(OpenFile())
            ''step 1 do stuff
            ''step 2 ?
            ''step 3 profit! yay1

        ElseIf TextBox2.Text = "showmethemoney" Then
            findInterestingNVrpt(OpenFile(), SaveFile())

        ElseIf TextBox2.Text = "cleannsimple" Then

            NVrpt(OpenFile(), SaveFile())

        End If
    End Sub

    Private Sub findInterestingNV(ByVal fileIn As String)

        Dim TextLine As String


        If System.IO.File.Exists(fileIn) = True Then

            Dim objReader As New System.IO.StreamReader(fileIn)

            Do While objReader.Peek() <> -1

                Dim nothingtoseehere As Boolean = True
                Dim ItemNumber As Integer
                TextLine = objReader.ReadLine()

                If TextLine.Contains("[") = True Then

                ElseIf TextLine.Contains("(") And TextLine.Contains("OK") = True Then
                    Dim ItemNumberS As String() = TextLine.Split(" ")
                    ItemNumber = Integer.Parse(ItemNumberS(0))
                    Dim NvData As String = ""
                    For i As Integer = 0 To 7

                        NvData += objReader.ReadLine()

                    Next
                    ''thanks to everyone in the flashing world that helped me understand nv -k
                    If (NvData.Contains("40") And NvData.Contains("63 6F 6D")) _
                        Or NvData.Contains("63 72 69 63 6B 65 74") _
                        Or NvData.Contains("73 65 63 72 65 74") _
                        Or (NvData.Contains("6D 6D 73") And NvData.Contains("68 74 74 70")) _
                        Or (NvData.Contains("6D 6D 73") And NvData.Contains("2F")) _
                        Or NvData.Contains("76 7A 77") Then
                        ''If NvData.Contains("40") Then

                        nothingtoseehere = False
                    End If
                Else

                End If

                If Not nothingtoseehere Then
                    ResultsListBox.Items.Add(ItemNumber.ToString)
                End If


            Loop


            MessageBox.Show("Done.")
        Else

            MsgBox("File Does Not Exist")

        End If

    End Sub

    Private Sub NVrpt(ByVal fileIn As String, ByVal fileOut As String)
        Try
            Dim interesting As New List(Of Integer)
            For Each s As String In ResultsListBox.Items
                interesting.Add(Integer.Parse(s))
            Next

            Dim TextLine As String

            If System.IO.File.Exists(fileIn) = True Then

                Dim objReader As New System.IO.StreamReader(fileIn)
                Dim objWriter As New System.IO.StreamWriter(fileOut)

                Do While objReader.Peek() <> -1

                    Dim ItemNumber As Integer
                    TextLine = objReader.ReadLine()

                    If TextLine.Contains("[") = True Then

                    ElseIf TextLine.Contains("(") And TextLine.Contains("OK") = True Then
                        Dim ItemNumberS As String() = TextLine.Split(" ")
                        ItemNumber = Integer.Parse(ItemNumberS(0))
                        Dim NvData As String = ""
                        For i As Integer = 0 To 7

                            NvData += objReader.ReadLine()

                        Next

                        If True Then
                            objWriter.WriteLine("")
                            objWriter.WriteLine("////////////////////////¿k?////////////////////////")
                            objWriter.WriteLine("")
                            objWriter.WriteLine(ItemNumber.ToString)
                            objWriter.WriteLine(NvData)
                            Try
                                objWriter.WriteLine(getStrings(NvData.Replace(" ", "")))
                            Catch ex As Exception
                                MessageBox.Show("bad")
                            End Try


                        End If
                    Else

                    End If

                Loop

                '' TextBox1.Text = TextLine
                objWriter.Close()
                MessageBox.Show("Done.")
            Else

                MsgBox("File Does Not Exist")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub

    Private Sub findInterestingNVrpt(ByVal fileIn As String, ByVal fileOut As String)
        Try
            Dim interesting As New List(Of Integer)
            For Each s As String In ResultsListBox.Items
                interesting.Add(Integer.Parse(s))
            Next

            Dim TextLine As String

            If System.IO.File.Exists(fileIn) = True Then

                Dim objReader As New System.IO.StreamReader(fileIn)
                Dim objWriter As New System.IO.StreamWriter(fileOut)

                Do While objReader.Peek() <> -1

                    Dim ItemNumber As Integer
                    TextLine = objReader.ReadLine()

                    If TextLine.Contains("[") = True Then

                    ElseIf TextLine.Contains("(") And TextLine.Contains("OK") = True Then
                        Dim ItemNumberS As String() = TextLine.Split(" ")
                        ItemNumber = Integer.Parse(ItemNumberS(0))
                        Dim NvData As String = ""
                        For i As Integer = 0 To 7

                            NvData += objReader.ReadLine()

                        Next

                        If interesting.Contains(ItemNumber) Then
                            objWriter.WriteLine("")
                            objWriter.WriteLine("////////////////////////¿k?////////////////////////")
                            objWriter.WriteLine("")
                            objWriter.WriteLine(ItemNumber.ToString)
                            objWriter.WriteLine(NvData)
                            Try
                                objWriter.WriteLine(getStrings(NvData.Replace(" ", "")))
                            Catch ex As Exception
                                MessageBox.Show("bad")
                            End Try


                        End If
                    Else

                    End If

                Loop

                '' TextBox1.Text = TextLine
                objWriter.Close()
                MessageBox.Show("Done.")
            Else

                MsgBox("File Does Not Exist")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        

    End Sub

    Private Function getStrings(ByVal HexValue As String) As String
        Dim StrValue As String = ""

        While HexValue.Length > 0

            StrValue += System.Convert.ToChar(System.Convert.ToUInt64(HexValue.Substring(0, 2), 16)).ToString()

            HexValue = HexValue.Substring(2, HexValue.Length - 2)
        End While

        Return StrValue
    End Function

End Class
