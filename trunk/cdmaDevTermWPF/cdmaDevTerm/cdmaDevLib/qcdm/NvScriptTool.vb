Public Class NvScriptTool
    Public Function GetNvItemName(ByVal nvitem As String) As String
        Return CType(Short.Parse(nvitem), NvItems.NVItems).ToString()
    End Function
    Public Function GetNvItemName(ByVal nvitem As Integer) As String
        Return CType(Short.Parse(nvitem), NvItems.NVItems).ToString()



    End Function
End Class
