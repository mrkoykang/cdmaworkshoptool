''Copyright 2012 Dillon Graham
''GPL v3 
Imports cdmaDevLib.Phone
Public Class logger
    Enum logType
        info
        msg
        infoAndMsg
    End Enum
    Public Shared Sub add(str As String)
        cdmaTerm.thePhone.LogData = str + Environment.NewLine + cdmaTerm.thePhone.LogData
    End Sub
    Public Shared Sub add(str As String, type As logType)
        ''todo: create logger? I think the end goal is binding the dll to a WPF/xamal solution.. not sure where to go with this
        If type = logType.msg Then
            cdmaTerm.thePhone.Msg = str + Environment.NewLine + cdmaTerm.thePhone.Msg
        ElseIf type = logType.infoAndMsg Then
            add(str, logType.msg)
            add(str)
        Else
            add(str)
        End If
    End Sub
 
End Class
