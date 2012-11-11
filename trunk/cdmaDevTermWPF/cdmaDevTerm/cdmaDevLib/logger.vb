''Copyright 2012 Dillon Graham
''GPL v3 
Imports cdmaDevLib.Phone
Public Class Logger
    Enum LogType
        Info
        Msg
        InfoAndMsg
    End Enum
    Public Shared Sub Add(str As String)
        cdmaTerm.thePhone.LogData = str + Environment.NewLine + cdmaTerm.thePhone.LogData
    End Sub
    Public Shared Sub Add(str As String, type As LogType)
        ''todo: create logger? I think the end goal is binding the dll to a WPF/xamal solution.. not sure where to go with this
        If type = LogType.msg Then
            cdmaTerm.thePhone.Msg = str + Environment.NewLine + cdmaTerm.thePhone.Msg
        ElseIf type = LogType.infoAndMsg Then
            Add(str, LogType.msg)
            Add(str)
        Else
            Add(str)
        End If
    End Sub

End Class
