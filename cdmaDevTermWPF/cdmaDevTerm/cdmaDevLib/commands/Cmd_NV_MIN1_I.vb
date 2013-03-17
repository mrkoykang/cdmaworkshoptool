Public Class Cmd_NV_MIN1_I
    Inherits Command
    Sub New(qc As Qcdm.Cmd, nv As NvItems.NvItems, data() As Byte, debugstr As String)
        MyBase.New(qc, nv, data, debugstr)
    End Sub
    Public Overrides Sub decode()
        Try
            Phone.MIN1Raw = (New Byte() {Me.bytesRxd(11), Me.bytesRxd(10), Me.bytesRxd(9), Me.bytesRxd(8)}).ToHexString()
        Catch ex As Exception
            logger.add("Min1 err: " + ex.ToString)
        End Try
    End Sub


    Public Shared Function decode_NV_MIN1(ByVal min1 As String, ByVal min2 As String) As String
        Try
            Return decode_NV_MIN1(System.Convert.ToInt32(min1, 16), System.Convert.ToInt32(min2, 16))
        Catch ex As Exception
            logger.add("decode_NV_MIN1 string string err: " + ex.ToString)
            Return ""
        End Try
    End Function

    Public Shared Function decode_NV_MIN1(ByVal min1 As Long, ByVal min2 As Long) As String

        min2 = (min2 + 1) Mod 10 + (((((min2 Mod 100) \ 10) + 1) Mod 10) * 10) + ((((min2 \ 100) + 1) Mod 10) * 100)

        Dim min1a As New Integer
        min1a = (min1 And &HFFC000) >> 14

        min1a = (min1a + 1) Mod 10 + (((((min1a Mod 100) \ 10) + 1) Mod 10) * 10) + ((((min1a \ 100) + 1) Mod 10) * 100)

        Dim min1b As New Integer
        min1b = ((min1 And &H3C00) >> 10) Mod 10

        Dim min1c As New Integer
        min1c = (min1 And &H3FF)

        Dim min1c_5b As Integer = (min1c + 1) Mod 10
        Dim min1c_5c As Integer = (((((min1c Mod 100) \ 10) + 1) Mod 10) * 10)
        Dim min1c_5d As Integer = ((((min1c \ 100) + 1) Mod 10) * 100)

        min1c = min1c_5b + min1c_5c + min1c_5d

        Return min2.ToString("000") + min1a.ToString("000") + min1b.ToString("0") + min1c.ToString("000")

    End Function

    Shared Function encode_NV_MIN1(ByVal min1str As String) As String()
        Dim min1 As String = "Min1"
        Dim min2 As String = "Min2"

        'WORD min2=(((byte)_strtoui64(min1str.Mid(0,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(1,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(2,1),NULL,16)+9)%10 );
        Dim min2i As Integer = (((min1str.Substring(0, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(1, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(2, 1)) + 9) Mod 10)

        'DWORD min1a=(((byte)_strtoui64(min1str.Mid(3,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(4,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(5,1),NULL,16)+9)%10);
        Dim min1a As Integer = (((min1str.Substring(3, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(4, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(5, 1)) + 9) Mod 10)

        'DWORD min1b=(byte)_strtoui64(min1str.Mid(6,1),NULL,16);
        Dim min1b As Integer = (min1str.Substring(6, 1))

        'if (min1b==0) min1b=10;
        If (min1b = 0) Then
            min1b = 10
        End If
        'DWORD min1c=(((byte)_strtoui64(min1str.Mid(7,1),NULL,16)+9)%10)*100+(((byte)_strtoui64(min1str.Mid(8,1),NULL,16)+9)%10)*10+(((byte)_strtoui64(min1str.Mid(9,1),NULL,16)+9)%10);
        Dim min1c As Integer = (((min1str.Substring(7, 1)) + 9) Mod 10) * 100 + (((min1str.Substring(8, 1)) + 9) Mod 10) * 10 + (((min1str.Substring(9, 1)) + 9) Mod 10)

        'DWORD min1=min1c+(min1b<<10)+(min1a<<14);
        min1 = (min1c + (min1b << 10) + (min1a << 14)).ToString("x8")

        'min1 = ""
        min2 = min2i.ToString("x4")
        'char *buff=(char*)malloc(0x30);
        'sprintf_s(buff,0x30,"%02X",min2); // Output Min2
        'sprintf_s(buff,0x30,"%08X",min1); // Output Min1

        Return New String() {min1.ToUpper, min2.ToUpper}
    End Function
End Class
