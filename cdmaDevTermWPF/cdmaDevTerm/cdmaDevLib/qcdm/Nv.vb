''this class is mostly used for display purpose
''check out cmd_nv for the 'actual' nv class
Public Class Nv
    Property DisplayData As String
    Property Data As String
    Property DataHex As String
    Property Item As String

    Sub New(_nvItem As NvItems.NvItems, _Data As String, _DataHex As String)
        Data = _Data
        DisplayData = _Data
        DataHex = _DataHex
        Item = _nvItem.ToString
    End Sub
End Class
