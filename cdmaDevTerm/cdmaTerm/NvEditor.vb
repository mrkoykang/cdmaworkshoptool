'' CDMA DEV TERM
'' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
'' www.chromableedstudios.com
'' chromableedstudios ( a t ) gmail ( d o t ) com
''     
'' cdmadevterm by ¿k? with help from ajh and jh
''
'' this was originally developed as a test framework, before many 
'' things about qcdm(and programming) were understood by the author
'' please forgive some code that should never have seen the light of day ;)
''
''-------------------------------------------------------------------------------------------------------------
'' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
'' check out the GPL v3 for details
'' http://www.gnu.org/copyleft/gpl.html
''-------------------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Xml

Public Class NvEditor

    Private Sub LoadNvEditorBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadNvEditorBtn.Click

        LoadNVForDataGrid()
        NvWriteModeBtn.BackColor = Control.DefaultBackColor
        ScriptingMode.BackColor = Control.DefaultBackColor
    End Sub

    Sub LoadNVForDataGrid()
        DataGridView1.Columns.Clear()


        Dim nvItemList As String() = nvEditorItemListTxtbox.Text.TrimEnd(" ").Replace(",", "").Split(" ")
        cdmaTerm.ReadNvItemList(nvItemList)


        ''  Dim myData As DataSet
        LoadNvEditor()
        WriteMode = False
        NvScriptMode = False


        cdmaTerm.ToolStripStatusLabel1.Text = "NV Read Done"
    End Sub

    Private Sub LoadNvEditor()


        DataGridView1.Columns.Add("item number(hex)", "item number(hex)")

        DataGridView1.Columns.Add("item number(dec)", "item number(dec)")

        DataGridView1.Columns.Add("item status", "item status")

        DataGridView1.Columns.Add("item name", "item name")

        DataGridView1.Columns.Add("item data", "item data")

        DataGridView1.Columns.Add("item data(ascii)", "item data(ascii)")

        '' DataGridView1.Columns.Add("item data(ascii)", "item data(ascii)")


        For Each c As Command In cdmaTerm.nvReadQ.mySynqdQ

            Dim loopNvItem As New NvItem(c)

            Dim loopItemDataAscii As String = ""
            If loopNvItem.getItemData.Replace("00", "").Length > 1 Then
                loopItemDataAscii = Encoding.ASCII.GetString(cdmaTerm.String_To_Bytes(loopNvItem.getItemData.Replace("00", "")))

            End If

            '' DataGridView1.Rows.Add(New String() {loopNvItem.getItemNumber.ToString("x4"), loopNvItem.getItemNumber.ToString, loopNvItem.getItemStatus, loopNvItem.getItemName, loopNvItem.getItemData, cdmaTerm.biznytesToStrizings(cdmaTerm.String_To_Bytes(loopNvItem.getItemData.Replace("00", "")))})
            DataGridView1.Rows.Add(New String() {"0x" + loopNvItem.getItemNumber.ToString("x4").ToUpper, loopNvItem.getItemNumber.ToString, loopNvItem.getItemStatus, loopNvItem.getItemName, loopNvItem.getItemData, loopItemDataAscii})


        Next


        ''result.Tables.Add()
        DataGridView1.AutoResizeColumn(0)
        DataGridView1.AutoResizeColumn(1)
        DataGridView1.AutoResizeColumn(2)
        DataGridView1.AutoResizeColumn(3)
        DataGridView1.AutoResizeColumn(5)


    End Sub




    Private Sub FilterButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterButton.Click


        filterNvItems(FilterCombo.Text)
    End Sub



    Private Sub filterNvItems(ByVal s As String)
        ''RemoveUseless
        '' RemoveAllBad()


        Dim rows As Integer = DataGridView1.Rows.Count
        Dim currentRow As Integer = 0

        While currentRow < rows - 1
            Dim delRow As Boolean = False

            If DataGridView1.Rows(currentRow).Cells("item status").Value = "ParameterBad" Or
               DataGridView1.Rows(currentRow).Cells("item status").Value = "InactiveItem" Then
                delRow = True
            End If
            If s = "All Bad Items" And DataGridView1.Rows(currentRow).Cells("item status").Value = "AccessDenied" Then
                delRow = True
            End If


            If delRow Then
                DataGridView1.Rows.RemoveAt(currentRow)
                rows -= 1
            Else
                currentRow += 1
            End If

            'If r.Cells("item status").Value = "ParameterBad" Or r.Cells("item status").Value = "InactiveItem" Or r.Cells("item status").Value = "AccessDenied" Then
            '    DataGridView1.Rows.RemoveAt(r.Index)

            'End If

        End While


        'For Each r As DataGridViewRow In DataGridView1.Rows



        '    If r.Cells("item status").Value = "ParameterBad" Or r.Cells("item status").Value = "InactiveItem" Or r.Cells("item status").Value = "AccessDenied" Then
        '        DataGridView1.Rows.RemoveAt(r.Index)

        '    End If
        'Next






    End Sub




    Private Sub XMLtoNVILBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XMLtoNVILBtn.Click
        Dim fd As New OpenFileDialog

        fd.ShowDialog()
        XMLtoNVIL(fd.FileName)

    End Sub

    Private Sub XMLtoNVIL(ByVal fileName As String)
        Try
            Dim reader As New Xml.XmlTextReader(fileName)

            Dim xmlD As XDocument = XDocument.Load(reader)

            '' TextBox1.AppendText(xmlD.ToString())

            Dim readingDoc = System.Xml.Linq.XDocument.Parse(xmlD.ToString())

            Dim items = From item In readingDoc...<item> Select item

            Dim lastUsedModel As String = ""

            Dim lastWasDup As Boolean = False
            Dim combinedId As String = ""

            Dim nvItemList As String = ""

            For Each item In items
                Dim currentNvItem As String = item.Attribute("name").ToString.Replace("name=""", "").Replace("""", "")
                ''MessageBox.Show("item: " + currentNvItem + "number: " + Integer.Parse([Enum].Parse(GetType(NvItems.NVItems), currentNvItem)).ToString)
                Try
                    nvItemList += Integer.Parse([Enum].Parse(GetType(NvItems.NVItems), currentNvItem)).ToString + " "
                Catch ex As Exception
                    MessageBox.Show("Enum parse error: " + ex.ToString)
                End Try

            Next


            nvEditorItemListTxtbox.Text = nvItemList

        Catch ex As Exception
            MessageBox.Show("nvil err" + ex.ToString)
        End Try


    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptingMode.Click
        If Not NvScriptMode Then
            DataGridView1.Columns.Add("index 0x", "index 0x")

            Dim cmb As New DataGridViewComboBoxColumn()
            cmb.HeaderText = "Type"
            cmb.Name = "Type"
            cmb.MaxDropDownItems = 2
            cmb.Items.Add("ValueB")
            cmb.Items.Add("ValueS")
            DataGridView1.Columns.Add(cmb)

            DataGridView1.Columns.Add("value", "value")

            Dim cmb2 As New DataGridViewCheckBoxColumn
            cmb2.HeaderText = "add"
            cmb2.Name = "add"
            DataGridView1.Columns.Add(cmb2)

            DataGridView1.AutoResizeColumn(cmb2.Index)
            NvScriptMode = True
            ScriptingMode.BackColor = Color.AliceBlue
        Else
            DataGridView1.Columns.Remove("Type")
            DataGridView1.Columns.Remove("value")
            DataGridView1.Columns.Remove("index 0x")
            DataGridView1.Columns.Remove("add")
            NvScriptMode = False
            ScriptingMode.BackColor = Control.DefaultBackColor
        End If

    End Sub

    Private Sub SaveScriptBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveScriptBtn.Click
        Dim saveFd As New SaveFileDialog()
        saveFd.ShowDialog()
        ''WriteScript(saveFd.FileName)

        MessageBox.Show("save not implemented")


    End Sub


    Private Function getWriteCount() As Integer
        ''todo iterate and count
        Dim writeCount As Integer = 0
        For Each r As DataGridViewRow In DataGridView1.Rows

            If r.Cells("add").Value = True Then
                writeCount += 1
            End If


        Next



        Return writeCount
    End Function


    Private Sub DeleteRowsBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRowsBtn.Click
        For Each selectedrow As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(selectedrow)
        Next
    End Sub

    Private Sub NvWriteModeBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NvWriteModeBtn.Click
        If Not WriteMode Then

            Dim cmb As New DataGridViewComboBoxColumn()
            cmb.HeaderText = "write type"
            cmb.Name = "write type"
            cmb.MaxDropDownItems = 2
            cmb.Items.Add("data")
            cmb.Items.Add("data (ascii)")
            DataGridView1.Columns.Add(cmb)


            Dim cmb2 As New DataGridViewCheckBoxColumn
            cmb2.HeaderText = "write"
            cmb2.Name = "write"
            DataGridView1.Columns.Add(cmb2)

            DataGridView1.AutoResizeColumn(cmb2.Index)
            NvWriteModeBtn.BackColor = Color.AliceBlue
            WriteMode = True
        Else
            DataGridView1.Columns.Remove("write type")
            DataGridView1.Columns.Remove("write")

            WriteMode = False
            NvWriteModeBtn.BackColor = Control.DefaultBackColor
        End If

    End Sub

    Private Sub NvWriteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NvWriteBtn.Click

        WriteNvFromDG()

        MessageBox.Show("write complete")
    End Sub

    Private Sub WriteNvFromDG()
        Try
            cdmaTerm.dispatchQ.clearCommandQ()

            ''Add the actual nv items and a comment with item number
            For Each r As DataGridViewRow In DataGridView1.Rows

                If r.Cells("write").Value = True Then

                    ''string value
                    Dim dataPacket As New List(Of Byte)
                    Dim currentItemNumberI As Integer = Integer.Parse(r.Cells("item number(dec)").Value)

                    If r.Cells("write type").Value = "data" Then
                        ''write nv data as byte[]
                        dataPacket.AddRange(cdmaTerm.String_To_Bytes(r.Cells("item data").Value))
                        ''  writer.WriteAttributeString("valueS", r.Cells("value").Value)

                    ElseIf r.Cells("write type").Value = "data (ascii)" Then
                        dataPacket.AddRange(ASCIIEncoding.ASCII.GetBytes(r.Cells("item data(ascii)").Value))
                        ''write nv data as string

                        ''  writer.WriteAttributeString("valueB", "0x" + r.Cells("value").Value)
                    End If
                    ''Public Sub New(ByVal qcdm As Qcdm.Cmd, ByVal nv As Integer, ByVal nvItemData As Byte(), ByVal debuggingTextIn As String)

                    cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, currentItemNumberI, dataPacket.ToArray, "DG NV Write item Num: " + currentItemNumberI.ToString))

                End If

            Next

            cdmaTerm.dispatchQ.executeCommandQ()

        Catch ex As Exception
            MessageBox.Show("Data Grid Nv Write Err (try nv write mode): " + ex.ToString)
        End Try

    End Sub


    Private Sub CheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAll.Click
        CheckAllBoxes()
    End Sub
    Private CheckingAll As Boolean = True
    Private WriteMode As Boolean = False
    Private NvScriptMode As Boolean = False
    Private Sub CheckAllBoxes()

        For Each r As DataGridViewRow In DataGridView1.Rows
            If WriteMode Then
                r.Cells("write").Value = CheckingAll
            End If

            If NvScriptMode Then
                r.Cells("add").Value = CheckingAll
            End If

        Next

        CheckingAll = Not (CheckingAll)

    End Sub

    ''Key handler for textbox to make enter execute the nv read
    Private Overloads Sub nvEditorItemListTxtbox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nvEditorItemListTxtbox.KeyDown

        If e.KeyCode = Keys.Return Then
            LoadNVForDataGrid()
        End If
    End Sub

    Private Sub UpdateNumbersBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateNumbersBtn.Click
        Dim listOfNvItems As String = ""


        For Each r As DataGridViewRow In DataGridView1.Rows
            listOfNvItems += r.Cells("item number(dec)").Value + " "
        Next
        nvEditorItemListTxtbox.Text = listOfNvItems

    End Sub

  
    Private Sub NvEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

End Class