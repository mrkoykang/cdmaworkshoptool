Public Class Carrier

    Private _name As String
    Private _prl As String
    Public Vars As New Dictionary(Of String, String)

    Sub initVars(doc As XDocument, mdn As String, min As String)
        Dim Nodes = From variable In doc...<variable> _
                       Select variable
        For Each node In Nodes
            Vars.Add(node.Element("key").Value.ToString, node.Element("value").Value.ToString)
        Next

        Vars.Add("¿MDN?", mdn)
        Vars.Add("¿MIN?", min)
        ''Name = New String(nameNode.Nodes.ElementAt(0).ToString)

    End Sub

    Public Sub New(FileName As String, mdn As String, min As String)
        Dim doc As XDocument = XDocument.Load(FileName)

        Dim nameNode = From name In doc...<name> _
                       Select name
        Name = New String(nameNode.Nodes.ElementAt(0).ToString)

        Dim prlNode = From prl In doc...<prl> _
                       Select prl
        Prl = New String(prlNode.Nodes.ElementAt(0).ToString)

        initVars(doc, mdn, min)
    End Sub

    Public Property Name As String
        ' Retrieves number.
        Get
            Return _name
        End Get
        ' Assigns to number.
        Set(Name As String)
            _name = Name
        End Set
    End Property

    Public Property Prl As String
        Get
            Return _prl
        End Get
        Set(Prl As String)
            _prl = Prl
        End Set
    End Property

    Function parseVar(var As String) As String
        Dim current As String = var
        For Each d As KeyValuePair(Of String, String) In Vars
            If current = d.Key Then
                current = d.Value
            ElseIf current.Contains(d.Key) Then
                Dim startOffset As Integer = current.IndexOf(d.Key)
                Dim endOffset As Integer = current.IndexOf(d.Key) + d.Key.Length



                ''logger.addToLog("current: " + current + "Start offset: " + startOffset.ToString + " EndOffset: " + endOffset.ToString + " Length: " + current.Length.ToString)
                ''case to handle two vars?
                If startOffset = 0 Then
                    current = parseVar(current.Substring(startOffset, endOffset)) + current.Substring(endOffset)
                ElseIf (endOffset = current.Length) Then
                    current = current.Substring(0, startOffset) + parseVar(current.Substring(startOffset, endOffset - startOffset))
                Else
                    current = current.Substring(0, startOffset) + parseVar(current.Substring(startOffset, endOffset)) + current.Substring(endOffset)
                End If
            End If

        Next

        ''bad magic codes. bad.
        While (current.Contains("¿"))
            current = parseVar(current)
        End While

        Return current

    End Function

    
End Class
