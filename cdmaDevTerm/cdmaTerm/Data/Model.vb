Imports cdmaTerm.Qcdm
Public Class Model
    Private _commands As New List(Of Command)
    Private _name As String


    Sub New(FileName As String, Carrier As Carrier)
        Dim doc As XDocument = XDocument.Load(FileName)

        Dim nameNode = From name In doc...<name> _
                       Select name
        Name = New String(nameNode.Nodes.ElementAt(0).ToString)

        Dim cmdNodes = From nvItem In doc...<nvItem> _
                       Select nvItem

        For Each x As XElement In cmdNodes

            ''MessageBox.Show(x.Element("number").Value)

            ' Try

            Dim currentData As String = Carrier.parseVar(x.Element("data").Value)

            MessageBox.Show("Model.new tes:" + currentData)
            Dim encoding As New System.Text.ASCIIEncoding()

            Dim byteS As New List(Of Byte)
            byteS.Add(currentData.Length)
            byteS.AddRange(encoding.GetBytes(currentData))


            cdmaTerm.dispatchQ.addCommandToQ(New Command(Cmd.DIAG_NV_WRITE_F, Integer.Parse(x.Element("number").Value), byteS.ToArray, currentData))



            'Catch ex As Exception
            '    MessageBox.Show(ex.ToString)
            'End Try

        Next

        cdmaTerm.dispatchQ.executeCommandQ()

    End Sub


    Public Property Commands As List(Of Command)
        Get
            Return _commands
        End Get
        Set(Comm As List(Of Command))
            _commands = Comm
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(Name As String)
            _name = Name
        End Set
    End Property
End Class
