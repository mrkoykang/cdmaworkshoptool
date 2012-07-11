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

Public Class metroCalc

    ''metro calc jacked from ajh the mad scientist

    Public incomingEsn11 As String
    Public Function MetroSPCcalc(ByVal inESN) As String
        incomingEsn11 = inESN
        Return Strings.Right((PartA() * PartB() * PartC()), 6)
    End Function
    Public Function POWER(ByVal number, ByVal gotopower)
        ''i got the power!
        Return (number ^ gotopower)
    End Function
    Public Function PartA()
        Return (POWER(2, (5 + incomingEsn11.Substring(0, 1) + incomingEsn11.Substring(1, 1) + incomingEsn11.Substring(2, 1))) - 1)
        ''Should work? lol g/l
    End Function
    Public Function PartB()
        Return (incomingEsn11.Substring(8, 3) + 199)
        '' do a test on this part, to see if the substring call does bring up the last 3, i may b a r tard.  Bartard... .. oh man
    End Function

    Public Function PartC()
        Dim rtn As Integer = 23
        Dim d As Integer
        For d = 3 To 10
            rtn = rtn + Val(incomingEsn11(d))
        Next
        Return rtn
        '' The grand finale... <3 copy and paste!... this could also be done in a for loop but i dunno how those work in here... improve dillon... improve!
    End Function
End Class
