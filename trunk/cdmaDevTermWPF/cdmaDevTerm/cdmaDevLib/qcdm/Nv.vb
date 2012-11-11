Public Class Nv
    Property Data As String
    Property Item As String

    Sub New(_Item As NvItems.NvItems, _Data As String)
        Data = _Data
        Item = _Item.ToString
    End Sub
End Class
