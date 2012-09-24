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

Public Class p2k

    ''method stubs from
    ''http://forum.motofan.ru/index.php?showtopic=11780&st=20#
    Function P2K_Init(ByVal p As IntPtr) As Integer
        Throw New NotImplementedException
    End Function

    Function P2K_Restart() As Integer
        Throw New NotImplementedException
    End Function

    Function P2K_GetStatus() As Integer
        Throw New NotImplementedException
    End Function

    Function File_Close() As Integer
        Throw New NotImplementedException
    End Function

    Function File_Delete(ByVal FileName As String) As Integer
        Throw New NotImplementedException
    End Function

    Function File_SetPointer(ByVal Offset As Byte, ByVal Cardinal As Integer) As Integer
        Throw New NotImplementedException
    End Function

    Function File_Count() As Integer
        Throw New NotImplementedException
    End Function

    Function File_List(ByVal Buffer As Byte(), ByVal _File_List As String) As Integer
        Throw New NotImplementedException
    End Function

    Function Seem_Write(ByVal SeemNo As Integer, ByVal RecordNo As Integer, ByVal StartOffset As Integer, ByVal BytesCount As Integer, ByVal Data As Byte()) As Integer
        Throw New NotImplementedException
    End Function

    Function File_VolInfo(ByVal Buffer As Byte()) As Integer
        Throw New NotImplementedException
    End Function

    Function Seem_Read(ByVal SeemNo As Integer, ByVal RecordNo As Integer, ByVal StartOffset As Integer, ByVal BytesCount As Integer, ByVal Data As Byte()) As Integer
        Throw New NotImplementedException
    End Function

    Function File_Create(ByVal FileName As String, ByVal Attribute As Integer) As Integer
        Throw New NotImplementedException
    End Function

    Function File_Write(ByVal Buffer As Byte(), ByVal Size As Integer) As Integer
        Throw New NotImplementedException
    End Function

End Class
