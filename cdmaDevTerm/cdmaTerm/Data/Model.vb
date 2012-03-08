Imports cdmaTerm
Imports cdmaTerm.Qcdm
Imports cdmaTerm.Qcdm.Cmd
Public Class Model
    Private _commands As New List(Of Command)
    Private _name As String
    Private _spcType As String
    Private _writeSpc As String

    Sub New(FileName As String, Carrier As Carrier)
        Dim doc As XDocument = XDocument.Load(FileName)

        Dim nameNode = From name In doc...<name> _
                       Select name
        Name = New String(nameNode.Nodes.ElementAt(0).ToString)

        Dim spcNode = From spc In doc...<spc> _
                       Select spc
        Spc = New String(spcNode.Nodes.ElementAt(0).ToString)

        Dim writeSpcNode = From spc In doc...<spc> _
                       Select spc
        WriteSpc = New String(writeSpcNode.Nodes.ElementAt(0).ToString)


        cdmaTerm.modeSwitch("Offline")
        If (Spc <> "16digit") Then

            cdmaTerm.readSpcFromPhone(Spc)
            cdmaTerm.sendAnySPC(cdmaTerm.SPCTextbox.Text)
        Else
            Dim sixteenDigitNode = From SP In doc...<SP> _
                       Select SP
            Dim sp16digit As New String(sixteenDigitNode.Nodes.ElementAt(0).ToString)
            cdmaTerm.dispatchQ.addCommandToQ(
                    New Command(
                        DIAG_PASSWORD_F,
                        cdmaTerm.String_To_Bytes(sp16digit),
                        "Send custom 16 digit DIAG_PASSWORD_F"
                        )
                    )

        End If

        cdmaTerm.writeAnySpc(WriteSpc)

        sendCarrierPrl(Carrier)

        Dim cmdNodes = From nvItem In doc...<nvItem> _
                       Select nvItem

        For Each x As XElement In cmdNodes
            ''MessageBox.Show(x.Element("number").Value)
            ' Try

            Dim currentData As String = Carrier.parseVar(x.Element("data").Value)

            MessageBox.Show("Model.new data after var replace: " + currentData)

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

        cdmaTerm.modeSwitch("Reset")
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
    Public Property Spc As String
        Get
            Return _spcType
        End Get
        Set(Spc As String)
            _spcType = Spc
        End Set
    End Property
    Public Property WriteSpc As String
        Get
            Return _writeSpc
        End Get
        Set(WriteSpc As String)
            _writeSpc = WriteSpc
        End Set
    End Property

End Class
