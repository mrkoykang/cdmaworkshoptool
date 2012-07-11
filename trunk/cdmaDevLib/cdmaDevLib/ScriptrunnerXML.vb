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
Imports Microsoft.VisualBasic
Imports System.xml.linq

Public Class ScriptrunnerXML

    Public Sub speakNSpell()

        Dim doc As XDocument = XDocument.Load("cdmaDevTerminalScript.xml")
        ' load the xml into an XDocument
        Dim readingDoc = System.Xml.Linq.XDocument.Parse(doc.ToString())

        ' get all <Product> elements from the XDocument
        Dim cmds = From cmd In readingDoc...<cmd> _
                       Select cmd

        ' go through each product
        For Each cmd In cmds
            ' output the value of the <Name> element within product
            Throw New Exception("command sent: " + cmd.Value)

            Dim TermCommandWithoutSpaces As String = cmd.Value.ToString.Replace(" ", String.Empty)
            cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.String_To_Bytes(TermCommandWithoutSpaces), "speakNSpell"))


        Next
    End Sub

    Public Sub dutchToEnglishSpeakNSpell()

        Dim doc As XDocument = XDocument.Load("DutchCdmaDevTerminalScript.xml")
        ' load the xml into an XDocument
        Dim readingDoc = System.Xml.Linq.XDocument.Parse(doc.ToString())

        ' get all <Product> elements from the XDocument
        Dim cmds = From cmd In readingDoc...<cmd> _
                       Select cmd

        ' go through each product
        For Each cmd In cmds
            ' output the value of the <Name> element within product
            Throw New Exception("command sent: " + cmd.Value)

            Dim TermCommandWithoutSpaces As String = cmd.Value.ToString.Replace(" ", String.Empty)
            cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.String_To_Bytes(TermCommandWithoutSpaces), "dutchToEnglishSpeakNSpell"))


        Next
    End Sub

    Public Sub MuteSpeakNSpell(ByVal fileName As String)

        ''Dim doc As XDocument = XDocument.Load("cdmaDevTerminalScript.xml")
        Dim doc As XDocument = XDocument.Load(fileName)
        ' load the xml into an XDocument
        Dim readingDoc = System.Xml.Linq.XDocument.Parse(doc.ToString())

        ' get all <Product> elements from the XDocument
        Dim cmds = From cmd In readingDoc...<cmd> _
                       Select cmd

        ' go through each product
        For Each cmd In cmds
            '' output the value of the <Name> element within product
            ''Throw new Exception("command sent: " + cmd.Value)

            Dim TermCommandWithoutSpaces As String = cmd.Value.ToString.Replace(" ", String.Empty)
            cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.String_To_Bytes(TermCommandWithoutSpaces), "MuteSpeakNSpell"))
            '' System.Threading.Thread.Sleep(200)

        Next
    End Sub
    Public Sub speakButDontSpell()

        Dim doc As XDocument = XDocument.Load("cdmaDevTerminalScript.xml")
        ' load the xml into an XDocument
        Dim readingDoc = System.Xml.Linq.XDocument.Parse(doc.ToString())

        ' get all <Product> elements from the XDocument
        Dim cmds = From cmd In readingDoc...<cmd> _
                       Select cmd

        ' go through each product
        For Each cmd In cmds
            ' output the value of the <Name> element within product
            Throw New Exception("command sent: " + cmd.Value)

            ''Dim TermCommandWithoutSpaces As String = cmd.Value.ToString.Replace(" ", String.Empty)
            ''cdmaTerm. dispatchQ.addCommandToQ(New Command((cdmaTerm.String_To_Bytes(TermCommandWithoutSpaces))


        Next
    End Sub

    Public Function getMahVariable(ByVal incomingString As String) As String


        If incomingString = "$_username" Then
            Return cdmaTerm.evdo_usernameTextbox.Text
        Else
            Return "fail"
        End If




    End Function




End Class
