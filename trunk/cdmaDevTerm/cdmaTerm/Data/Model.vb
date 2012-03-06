Imports cdmaTerm.Qcdm
Public Class Model
    Private _commands As New List(Of Command)
    Private _name As String


    Sub New(FileName As String, Carrier As Carrier)
        Dim doc As XDocument = XDocument.Load(FileName)

        Dim nameNode = From name In doc...<name> _
                       Select name
        Name = New String(nameNode.Nodes.ElementAt(0).ToString)

        sendCarrierPrl(Carrier)

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

            Try
                cdmaTerm.dispatchQ.addCommandToQ(New Command(Cmd.DIAG_NV_WRITE_F, Integer.Parse(x.Element("number").Value), byteS.ToArray, currentData))

                ''sloppy bs
            Catch ex As Exception
                Try
                    Dim NvItemCurrent = DirectCast([Enum].Parse(GetType(NvItems.NVItems), x.Element("number").Value, True), NvItems.NVItems)

                    cdmaTerm.dispatchQ.addCommandToQ(New Command(Cmd.DIAG_NV_WRITE_F, NvItemCurrent, byteS.ToArray, currentData))


                Catch ex2 As Exception
                    MessageBox.Show("Error: Nv element appears not to be a number or an nv item")
                End Try
            End Try


        Next

        cdmaTerm.dispatchQ.executeCommandQ()

    End Sub

    Sub sendCarrierPrl(Carrier As Carrier)
        Try
            Dim PrlFile As String
            Dim myPlus As New Prl

            PrlFile = Carrier.Prl
            myPlus.UploadPRL(Application.StartupPath + "/data/prl/" + PrlFile)
        Catch ex As Exception
            MessageBox.Show("Data script prl error: " + ex.ToString)
        End Try

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
