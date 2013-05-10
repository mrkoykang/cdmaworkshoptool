'' cdmaDevTerm
'' Copyright (c) Dillon Graham 2010-2013 Chromableed Studios
'' www.chromableedstudios.com
''     
'' cdmadevterm by ¿k? with help from ajh and jh and many others
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
Public Class NvItemOLD


    ''empty command
    Public Sub New()

    End Sub

    ''empty command
    Public Sub New(ByVal bytesIn As Byte())

        parseByteResponse(bytesIn)

    End Sub



    ''For cdmaWs compat.
    '0318 (0x013E)   -   OK
    '1F 31 32 33 34 35 36 37 38 39 30 40 65 78 61 6D 
    '70 6C 65 63 65 6C 6C 75 6C 61 72 31 2E 6E 65 74 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 



    ''OK or Not valid or something
    Dim state As String = ""

    ''DEC NVIITEM NUM
    '0318 
    Dim itemNumberShortFirstString As String

    ''HEX NVITEM NUM - padded to 6 digits
    '00013E
    Dim itemNumberFull6DigitsString As String

    ''HEX ITEM NUMBER - trim 00 pairs, not singles
    '(0x013E) 
    Dim itemNumberHexString As String

    ''this is sample nv item data
    '1F 31 32 33 34 35 36 37 38 39 30 40 65 78 61 6D 
    '70 6C 65 63 65 6C 6C 75 6C 61 72 31 2E 6E 65 74 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    Dim itemData As Byte()
    Dim fullByteData As Byte()

    Sub setState(ByVal s As String)
        state = s
    End Sub
    Function getState() As String
        Return state
    End Function
    Sub setItemNumberShortFirstString(ByVal s As String)
        itemNumberShortFirstString = s
    End Sub
    Function getItemNumberShortFirstString() As String
        Return itemNumberShortFirstString
    End Function
    Sub setItemNumberFull6DigitsString(ByVal s As String)
        itemNumberFull6DigitsString = s
    End Sub
    Function getitemNumberFull6DigitsString() As String
        Return itemNumberFull6DigitsString
    End Function
    Sub setItemNumberHexString(ByVal s As String)
        itemNumberHexString = s
    End Sub
    Function getItemNumberHexString() As String
        Return itemNumberHexString
    End Function
    Sub setItemData(ByVal b As Byte())
        itemData = b
    End Sub
    Function getItemData() As Byte()
        Return itemData
    End Function

    Private Sub parseByteResponse(ByVal inArray As Byte())

    End Sub

End Class
