Imports System.IO

' '' CDMA DEV TERM
' '' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
' '' www.chromableedstudios.com
' '' chromableedstudios ( a t ) gmail ( d o t ) com
' ''     
' '' cdmadevterm by ¿k? with help from ajh and jh
' ''
' '' this was originally developed as a test framework, before many 
' '' things about qcdm(and programming) were understood by the author
' '' please forgive some code that should never have seen the light of day ;)
' ''
' ''-------------------------------------------------------------------------------------------------------------
' '' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
' '' check out the GPL v3 for details
' '' http://www.gnu.org/copyleft/gpl.html
' ''-------------------------------------------------------------------------------------------------------------

Public Class SixteenDigitCodes

    Private Shared runtime16DigitPass As Dictionary(Of String, String)

    Public Shared Function get16DigitPasswords() As Dictionary(Of String, String)
        Return runtime16DigitPass
    End Function

    Public Shared Sub set16DigitPasswords(path As String)

        runtime16DigitPass = New Dictionary(Of String, String)

        Dim objReader As New StreamReader(path)
        Dim sLine As String = ""
        Dim arrText As New ArrayList()
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        For Each sLine In arrText
            If (sLine.StartsWith(";")) Then
                Continue For
            End If
            Dim seperator As Int16 = sLine.IndexOf(":")

            Try
                runtime16DigitPass.Add(sLine.Substring(0, seperator), sLine.Substring(seperator + 1, 16))
            Catch ex As Exception
                logger.addToLog("Error in 16 digit pass line: " + sLine + " error:" + ex.ToString)
            End Try
        Next

    End Sub

    Public Shared Function get16DigitPassword(model As String) As String
        Return runtime16DigitPass.Item(model)
    End Function


End Class
