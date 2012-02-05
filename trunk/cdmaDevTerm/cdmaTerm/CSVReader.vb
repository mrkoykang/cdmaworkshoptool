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
Imports System.Windows.Forms
Imports System.IO
Imports System.Text.RegularExpressions
''from stack overflow
''many thanks http://stackoverflow.com/questions/1029850/visual-basic-how-do-i-read-a-csv-file-and-display-the-values-in-a-datagrid
Public Class CSVReader
    Private Const ESCAPE_SPLIT_REGEX = "({1}[^{1}]*{1})*(?<Separator>{0})({1}[^{1}]*{1})*"
    Private FieldNames As String()
    Private Records As List(Of String())
    Private ReadIndex As Integer

    Public Sub New(ByVal File As String)
        Records = New List(Of String())
        Dim Record As String()
        Dim Reader As New StreamReader(File)
        Dim Index As Integer = 0
        Dim BlankRecord As Boolean = True

        FieldNames = GetEscapedSVs(Reader.ReadLine())
        While Not Reader.EndOfStream
            Record = GetEscapedSVs(Reader.ReadLine())
            BlankRecord = True
            For Index = 0 To Record.Length - 1
                If Record(Index) <> "" Then BlankRecord = False
            Next
            If Not BlankRecord Then Records.Add(Record)
        End While
        ReadIndex = -1
        Reader.Close()
    End Sub

    Private Function GetEscapedSVs(ByVal Data As String, Optional ByVal Separator As String = ";", Optional ByVal Escape As String = """") As String()
        Dim Result As String()
        Dim Index As Integer
        Dim PriorMatchIndex As Integer = 0
        Dim Matches As MatchCollection = _
                Regex.Matches(Data, String.Format(ESCAPE_SPLIT_REGEX, Separator, Escape))

        ReDim Result(Matches.Count)

        For Index = 0 To Result.Length - 2
            Result(Index) = Data.Substring(PriorMatchIndex, Matches.Item(Index).Groups("Separator").Index - PriorMatchIndex)
            PriorMatchIndex = Matches.Item(Index).Groups("Separator").Index + Separator.Length
        Next
        Result(Result.Length - 1) = Data.Substring(PriorMatchIndex)

        For Index = 0 To Result.Length - 1
            If Regex.IsMatch(Result(Index), String.Format("^{0}[^{0}].*[^{0}]{0}$", Escape)) Then _
    Result(Index) = Result(Index).Substring(1, Result(Index).Length - 2)
            Result(Index) = Replace(Result(Index), Escape & Escape, Escape)
            If Result(Index) Is Nothing Then Result(Index) = ""
        Next

        GetEscapedSVs = Result
    End Function

    Public ReadOnly Property FieldCount As Integer
        Get
            Return FieldNames.Length
        End Get
    End Property

    Public Function GetString(ByVal Index As Integer) As String
        Return Records(ReadIndex)(Index)
    End Function

    Public Function GetName(ByVal Index As Integer) As String
        Return FieldNames(Index)
    End Function

    Public Function Read() As Boolean
        ReadIndex = ReadIndex + 1
        Return ReadIndex < Records.Count
    End Function


    Public Sub DisplayResults(ByVal DataView As DataGridView)
        Dim col As DataGridViewColumn
        Dim row As DataGridViewRow
        Dim cell As DataGridViewCell
        Dim header As DataGridViewColumnHeaderCell
        Dim Index As Integer
        ReadIndex = -1

        DataView.Rows.Clear()
        DataView.Columns.Clear()

        For Index = 0 To FieldCount - 1
            col = New DataGridViewColumn()
            col.CellTemplate = New DataGridViewTextBoxCell()
            header = New DataGridViewColumnHeaderCell()
            header.Value = GetName(Index)
            col.HeaderCell = header
            DataView.Columns.Add(col)
        Next

        Do While Read()
            row = New DataGridViewRow()
            For Index = 0 To FieldCount - 1
                cell = New DataGridViewTextBoxCell()
                cell.Value = GetString(Index).ToString()
                row.Cells.Add(cell)
            Next
            DataView.Rows.Add(row)
        Loop
    End Sub
End Class

