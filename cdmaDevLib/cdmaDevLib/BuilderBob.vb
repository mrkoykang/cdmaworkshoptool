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
Imports System.Text
Public Class BuilderBob

    ' ''... this class just shouldn't exist.....
    ' ''just waiting for a refactor to nuke it

    'Public Function buildATerminalCommand(ByVal emptyCommandArray As Byte(), ByVal commandPrefixArray As Byte(), ByVal dataArray As Byte()) As Byte()
    '    ''fixed length, kludgy

    '    Dim buildATermCommandLocalArray As Byte() = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    '    emptyCommandArray.CopyTo(buildATermCommandLocalArray, 0)

    '    For n = 0 To (commandPrefixArray.Length - 1)
    '        buildATermCommandLocalArray(n) = commandPrefixArray(n)
    '    Next
    '    ''testies123
    '    For n = commandPrefixArray.Length To ((commandPrefixArray.Length - 1) + dataArray.Length - 1)
    '        buildATermCommandLocalArray(n) = dataArray(n - (commandPrefixArray.Length - 1))
    '    Next

    '    Return attachACrc(buildATermCommandLocalArray)

    'End Function
    ' ''todo:wtf
    'Private Function attachACrc(ByVal command() As Byte) As Byte()
    '    ''also fixed length
    '    Dim ultraStageThree As Byte() = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    '    Dim commandsCrc As Byte() = cdmaTerm.gimmeCRC_AsByte_FromByte(command)

    '    cdmaTerm.empty_cmd_136.CopyTo(ultraStageThree, 0)

    '    command.CopyTo(ultraStageThree, 0)

    '    ultraStageThree(133) = commandsCrc(0)
    '    ultraStageThree(134) = commandsCrc(1)
    '    ultraStageThree(135) = &H7E

    '    ''command(command.Length + 1) = commandsCrc(0)
    '    ''command(command.Length + 2) = commandsCrc(1)
    '    Return (ultraStageThree)

    'End Function

    'Public Function buildDataArray(ByVal incomingAscii As String) As Byte()
    ' Dim encoding As New System.Text.ASCIIEncoding()
    '    Return encoding.GetBytes(incomingAscii)
    'End Function

    'Public Function generateNSizeArray(ByVal n As Integer) As Byte()
    '    Dim s As String = ""
    '    For i = 0 To n
    '        s += "00"
    '    Next
    '    Return cdmaTerm.String_To_Bytes(s)
    'End Function





End Class
