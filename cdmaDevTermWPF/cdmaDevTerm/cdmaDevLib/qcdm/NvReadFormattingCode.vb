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
Namespace My.Templates
    Partial Public Class NvReadFormatting
        Private count As Integer
        Private NvRead As CommandQueue
        Public Sub New(ByVal data As CommandQueue)
            NvRead = data
            count = NvRead.GetCount()
        End Sub
    End Class
End Namespace


