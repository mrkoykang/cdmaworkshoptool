''Copyright 2012 Dillon Graham
''GPL v3 
Public Class logger
    Enum logType
        info
        warn
        err
    End Enum
    Public Shared Sub addToLog(str As String)
        cdmaTerm.thePhone.LogData = str
    End Sub
    Public Shared Sub addToLog(str As String, type As logType)
        ''todo: create logger? I think the end goal is binding the dll to a WPF/xamal solution.. not sure where to go with this
    End Sub
    Public Shared Sub addToByteLog(str As String)
        ''todo
    End Sub
End Class
