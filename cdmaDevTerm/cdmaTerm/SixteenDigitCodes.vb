' '' CDMA DEV TERM
' '' Copyright (c) Dillon Graham 2010-2011 Chromableed Studios
' '' www.chromableedstudios.com
' '' chromableedstudios ( a t ) gmail ( d o t ) com
' ''     
' '' cdmadevterm by ¿k? with help from ajh and jh
' ''
' '' this was originally developed as a test framework, before many 
' '' things about qcdm(and programming) were understood by the author
' '' please forgive some code that should never have seen the light of day ;)
' ''
' ''-------------------------------------------------------------------------------------------------------------
' '' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
' '' check out the GPL v3 for details
' '' http://www.gnu.org/copyleft/gpl.html
' ''-------------------------------------------------------------------------------------------------------------

Public Class SixteenDigitCodes
    Public Function SetModel(ByVal spModel As String) As String
        ''
        If spModel = "" Then
            MessageBox.Show("unknown 16 digit code")
            Return "4620050202200505314A437E"


            ''kludgy lookup table
        ElseIf spModel = "MMA700" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SCHA130" Then
            Return "46775463076253647379CE7E"
        ElseIf spModel = "SCHA160" Then
            Return "4620055574A8800822145E7E"
        ElseIf spModel = "SCHA310" Then
            Return "460000031020020502E72C7E"
        ElseIf spModel = "SCHA375" Then
            Return "4600000375200112106EAF7E"
        ElseIf spModel = "SCHA410" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHA460_Num1" Then
            Return "46444F43544F5220A975817E"
        ElseIf spModel = "SCHA460_Num2" Then
            Return "464E4F56494E4B492198867E"
        ElseIf spModel = "SCHA475" Then
            Return "460000047520020206FDBC7E"
        ElseIf spModel = "SCHA530" Then
            Return "460000053020020815CA807E"
        ElseIf spModel = "SCHA562" Then
            Return "4601050F045F678FF9F3197E"
        ElseIf spModel = "SCHA563" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHA565_Num1" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHA565_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHA570" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA595" Then
            Return "460000047520020206FDBC7E"
        ElseIf spModel = "SCHA603" Then
            Return "462D4843533330364130DE7E"
        ElseIf spModel = "SCHA605" Then
            Return "462D4843533330364130DE7E"
        ElseIf spModel = "SCHA610" Then
            Return "460200361020030314648f7E"
        ElseIf spModel = "SCHA612" Then
            Return "4601050F045F678FF9F3197E"
        ElseIf spModel = "SCHA630" Then
            Return "4612F3141F6F789FFA54BF7E"
        ElseIf spModel = "SCHA633" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHA640" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA650" Then
            Return "46020036502003040370677E"
        ElseIf spModel = "SCHA655" Then
            Return "46020036502003040370677E"
        ElseIf spModel = "SCHA656" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHA660" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHA670" Then
            Return "460200361020030314648f7E"
        ElseIf spModel = "SCHA685" Then
            Return "462D48435335303541C2BF7E"
        ElseIf spModel = "SCHA690" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA725" Then
            Return "462D48435335303541C2BF7E"
        ElseIf spModel = "SCHA770" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA790" Then
            Return "4604020224062105092D667E"
        ElseIf spModel = "SCHA815" Then
            Return "4604020224062105092D667E"
        ElseIf spModel = "SCHA840" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA850" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA870" Then
            Return "461981091019770418AD417E"
        ElseIf spModel = "SCHA880" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA890" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA895" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHA915" Then
            Return "46200509226500A915DA2E7E"
        ElseIf spModel = "SCHA930" Then
            Return "46200510072005102150D47E"
        ElseIf spModel = "SCHA950" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SCHA970" Then
            Return "461967041020000214D7297E"
        ElseIf spModel = "SCHA990" Then
            Return "46200509902005121921337E"
        ElseIf spModel = "SCHB239" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHB259" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHB279" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHB309" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHE159_Num1" Then
            Return "466905110834560250906A7E"
        ElseIf spModel = "SCHE159_Num2" Then
            Return "4617D40000004000003FA97E"
        ElseIf spModel = "SCHE170" Then
            Return "46908001201080704794887E"
        ElseIf spModel = "SCHE250" Then
            Return "465D3C920C2D1C5CCA2B67E"
        ElseIf spModel = "SCHF359_Num1" Then
            Return "469020000080000006B417E"
        ElseIf spModel = "SCHF359_Num2" Then
            Return "46C07000008000000C5737E"
        ElseIf spModel = "SCHF509" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHF519" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHF679" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHI220" Then
            Return "462FF811282FF9F3F617287E"
        ElseIf spModel = "SCHI600" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHI700" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHI730" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHI760" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHI770" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHI830" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHI910" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHL160" Then
            Return "46775463076253647379CE7E"
        ElseIf spModel = "SCHL210" Then
            Return "461004062010F225605E7B7E"
        ElseIf spModel = "SCHL310" Then
            Return "462505062010F32560BEC47E"
        ElseIf spModel = "SCHM450" Then
            Return "461000EE01A04D0D005AF37E"
        ElseIf spModel = "SCHN181" Then
            Return "4600000375200112106EAF7E"
        ElseIf spModel = "SCHN191" Then
            Return "4600000375200112106EAF7E"
        ElseIf spModel = "SCHN195" Then
            Return "4600000375200112106EAF7E"
        ElseIf spModel = "SCHN255_Num1" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SCHN255_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHN345" Then
            Return "466905110834560250906A7E"
            ''IFFY WTF
        ElseIf spModel = "SCHN356_Num1" Then
            Return " 000003752001121086DF7E"
        ElseIf spModel = "SCHN356_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHN362" Then
            Return "462D4843533330364130DE7E"
        ElseIf spModel = "SCHN375" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHN380" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHN392" Then
            Return "462D4843533330364130DE7E"
        ElseIf spModel = "SCHN393" Then
            Return "462D4843533330364130DE7E"
        ElseIf spModel = "SCHN395" Then
            Return "462D4843533539334EFB8F7E"
        ElseIf spModel = "SCHN415" Then
            Return "466905110834560250906A7E"
        ElseIf spModel = "SCHN480" Then
            Return "466905110834560250906A7E"
        ElseIf spModel = "SCHN485" Then
            Return "466905110834560250906A7E"
        ElseIf spModel = "SCHN500_Num1" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHN500_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHN530" Then
            Return "46775463076253647379CE7E"
        ElseIf spModel = "SCHR200" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SCHR210" Then
            Return "4620071122200802289A657E"
        ElseIf spModel = "SCHR211" Then
            Return "4620071122200802289A657E"
        ElseIf spModel = "SCHR300" Then
            Return "461981091019770418AD417E"
        ElseIf spModel = "SCHR310" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHR311" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHR400" Then
            Return "461981091019770418AD417E"
        ElseIf spModel = "SCHR410" Then
            Return "461981091019770418AD417E"
        ElseIf spModel = "SCHR420" Then
            Return "462008022120080801DC6E7E"
        ElseIf spModel = "SCHR430" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHR450" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHR460" Then
            Return "46200810292009022875657E"
        ElseIf spModel = "SCHR470" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHR500" Then
            Return "46200510072005102150D47E"
        ElseIf spModel = "SCHR510" Then
            Return "46200510072005102150D47E"
        ElseIf spModel = "SCHR550" Then
            Return "462007121420071231B8D47E"
        ElseIf spModel = "SCHR560" Then
            Return "462009010620080131A24B7E"
        ElseIf spModel = "SCHR561" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHR610" Then
            Return "46200510072005102150D47E"
        ElseIf spModel = "SCHR800" Then
            Return "4620080606200812315F777E"
        ElseIf spModel = "SCHR810" Then
            Return "46200810212008022889657E"
        ElseIf spModel = "SCHS109" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHS179" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHS229" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHS250_Num1" Then
            Return "46141C102802D192A02DA17E"
        ElseIf spModel = "SCHS250_Num2" Then
            Return "4649EE000000800100E48B7E"
        ElseIf spModel = "SCHS259" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHS269" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHS299" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHS350" Then
            Return "4649EE000000800100E48B7E"
        ElseIf spModel = "SCHS399" Then
            Return "465903365113726913282B7E"
        ElseIf spModel = "SCHT191_Num1" Then
            Return "46000003752001121086DF7E"
        ElseIf spModel = "SCHT191_Num2" Then
            Return "460000353736303000A5F37E"
        ElseIf spModel = "SCHU340_Num1" Then
            Return "4620060822119013207F827E"
        ElseIf spModel = "SCHU340_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SCHU350" Then
            Return "461945081520081106E7207E"
        ElseIf spModel = "SCHU410" Then
            Return "461945081520070115ABDD7E"
        ElseIf spModel = "SCHU430_Num1" Then
            Return "461945081520070115ABDD7E"
            '' ElseIf spModel = " Dim SCHU430_Num2 " Then Return   "46012708600E48104B
        ElseIf spModel = "SCHU440" Then
            Return "462007111420080131FB2D7E"
        ElseIf spModel = "SCHU450" Then
            Return "46197619781978198028DF7E"
        ElseIf spModel = "SCHU470" Then
            Return "462007091020070470F1047E"
        ElseIf spModel = "SCHU510" Then
            Return "462206062010F50065212C7E"
        ElseIf spModel = "SCHU520" Then
            Return "46200510072005102150D47E"
        ElseIf spModel = "SCHU540" Then
            Return "46200610062006120401DD7E"
        ElseIf spModel = "SCHU550" Then
            Return "4620070101200705055FA77E"
        ElseIf spModel = "SCHU620" Then
            Return "46916310BDE04A5078AB637E"
        ElseIf spModel = "SCHU650" Then
            Return "461945081520070115ABDD7E"
        ElseIf spModel = "SCHU700" Then
            Return "46020067202006081563D37E"
        ElseIf spModel = "SCHU740" Then
            Return "460110131637430677D1A77E"
        ElseIf spModel = "SCHU750" Then
            Return "46197619781978198028DF7E"
        ElseIf spModel = "SCHU810" Then
            Return "461A04120C090CF0D2C7097E"
        ElseIf spModel = "SCHU900" Then
            Return "462007040920071231C6467E"
        ElseIf spModel = "SCHU940" Then
            Return "46020067202006081563D37E"
        ElseIf spModel = "SCHV122" Then
            Return "46FFFFFFFFFFFFFFFFFE747E"
        ElseIf spModel = "SCHV30" Then
            Return "46040B0000000001001F8A7E"
            '' ElseIf spModel = " Dim SCHV42 " Then Return  "46054E321F5577CC33
            '' ElseIf spModel = " Dim SCHV500 " Then Return  "461332241618886910
            '' ElseIf spModel = " Dim SCHV540 " Then Return  "461847000040007F01
            '' ElseIf spModel = " Dim SCHV740 " Then Return  "46002A02D1C26A002A
            '' ElseIf spModel = " Dim SCHV840 " Then Return  "46E9FD000000800100
        ElseIf spModel = "SCHW339" Then
            Return "465903365113726913282B7E"
            '' ElseIf spModel = " Dim SCHX130 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX137 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX140 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX210_Num1 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX210_Num2 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX250 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX290_Num1 " Then Return  "46540B000000600000
            '' ElseIf spModel = " Dim SCHX290_Num2 " Then Return  "4608BC1847FFB5171C
            '' ElseIf spModel = " Dim SCHX420 " Then Return  "466810000000000000
            '' ElseIf spModel = " Dim SCHX140 " Then Return  "460D00A0E120329FE5
        ElseIf spModel = "SCHX430" Then
            Return "4601050F045F678FF9F3197E"
            '' ElseIf spModel = " Dim SCHX"460 " Then Return  "460D00A0E120329FE5
            '' ElseIf spModel = " Dim SCHX780 " Then Return  "46540B000000600000
            '' ElseIf spModel = " Dim SCHX820 " Then Return  "46540B000000000100
            '' ElseIf spModel = " Dim SCHX839 " Then Return  "462D4843532D303641
        ElseIf spModel = "SCHX979" Then
            Return "46000003752001121086DF7E6EAF7E"
        ElseIf spModel = "SPHA303" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SPHA460_Num1" Then
            Return "46444F43544F5220A975817E"
        ElseIf spModel = "SPHA460_Num2" Then
            Return "464E4F56494E4B492198867E"
        ElseIf spModel = "SPHA460_Num3" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA505" Then
            Return "462D48435335303541C2BF7E"
        ElseIf spModel = "SPHA523" Then
            Return "46FFFFFFFFFFFFFFFFFE747E"
        ElseIf spModel = "SPHA533" Then
            Return "46FFFFFFFFFFFFFFFFFE747E"
        ElseIf spModel = "SPHA560" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SPHA580" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SPHA600" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA620" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA640" Then
            Return "462004071020040331DC647E"
        ElseIf spModel = "SPHA660" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA680" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA700_Num1" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SPHA700" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA740_Num1" Then
            Return "462D48435335303541C2BF7E"
        ElseIf spModel = "SPHA740_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA820" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA840_Num1" Then
            Return "464E4F56494E4B492198867E"
            '' ElseIf spModel = " Dim SPHA840_Num2 " Then Return  "464E4F5649E4B44921
        ElseIf spModel = "SPHA880" Then
            Return "4620055574A8800822145E7E"
        ElseIf spModel = "SPHA900" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHA940" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SPHA960" Then
            Return "4620050202200505314A437E"
            '' ElseIf spModel = " Dim SPHI325_Num1 " Then Return  "462007041320080115
            '' ElseIf spModel = " Dim SPHI325_Num2 " Then Return  "461000F501184F4900
        ElseIf spModel = "SPHI500" Then
            Return "4600000000000000009BF37E"
        ElseIf spModel = "SPHM300" Then
            Return "461981091019770418AD417E"
        ElseIf spModel = "SPHM320" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM330" Then
            Return "46021C5143AB4A002025527E"
        ElseIf spModel = "SPHM500" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM520" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM540_Num1" Then
            Return "46021C5143AB4A002025527E"
        ElseIf spModel = "SPHM540_Num2" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM550" Then
            Return "46021C5143AB4A002025527E"
        ElseIf spModel = "SPHM560" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM600" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM610" Then
            Return "4620050202200505314A437E"
        ElseIf spModel = "SPHM620" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM800" Then
            Return "4601F2030F5F678FF9A23F7E"
        ElseIf spModel = "SPHM810" Then
            Return "4601F2030F5F678FF9A23F7E"




        Else
            MessageBox.Show("iFFy Digit Code Selected?")
            Return "4601F2030F5F678FF9A23F7E"
        End If

    End Function


    ''for ref i guess
    ''Public send16digitFs() As Byte = {&H46, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFE, &H74, &H7E}

    ''Public send16digitSchU350() As Byte = {&H46, &H19, &H45, &H8, &H15, &H20, &H8, &H11, &H6, &HF, &H50, &H7E}




    ' ''strings
    'Public MMA700 As String = "4620050202200505314A437E"
    'Public SCHA130 As String = "46775463076253647379CE7E"
    'Public SCHA160 As String = "4620055574A8800822145E7E"
    'Public SCHA310 As String = "460000031020020502E72C7E"
    'Public SCHA375 As String = "4600000375200112106EAF7E"
    'Public SCHA410 As String = "4600000000000000009BF37E"
    'Public SCHA460_Num1 As String = "46444F43544F5220A975817E"
    'Public SCHA460_Num2 As String = "464E4F56494E4B492198867E"
    'Public SCHA475 As String = "460000047520020206FDBC7E"
    'Public SCHA530 As String = "460000053020020815CA807E"
    'Public SCHA562 As String = "4601050F045F678FF9F3197E"
    'Public SCHA563 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHA565_Num1 As String = "4600000000000000009BF37E"
    'Public SCHA565_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHA570 As String = "462004071020040331DC647E"
    'Public SCHA595 As String = "460000047520020206FDBC7E"
    'Public SCHA603 As String = "462D4843533330364130DE7E"
    'Public SCHA605 As String = "462D4843533330364130DE7E"
    'Public SCHA610 As String = "460200361020030314648f7E"
    'Public SCHA612 As String = "4601050F045F678FF9F3197E"
    'Public SCHA630 As String = "4612F3141F6F789FFA54BF7E"
    'Public SCHA633 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHA640 As String = "462004071020040331DC647E"
    'Public SCHA650 As String = "46020036502003040370677E"
    'Public SCHA655 As String = "46020036502003040370677E"
    'Public SCHA656 As String = "4600000000000000009BF37E"
    'Public SCHA660 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHA670 As String = "460200361020030314648f7E"
    'Public SCHA685 As String = "462D48435335303541C2BF7E"
    'Public SCHA690 As String = "462004071020040331DC647E"
    'Public SCHA725 As String = "462D48435335303541C2BF7E"
    'Public SCHA770 As String = "462004071020040331DC647E"
    'Public SCHA790 As String = "4604020224062105092D667E"
    'Public SCHA815 As String = "4604020224062105092D667E"
    'Public SCHA840 As String = "462004071020040331DC647E"
    'Public SCHA850 As String = "462004071020040331DC647E"
    'Public SCHA870 As String = "461981091019770418AD417E"
    'Public SCHA880 As String = "462004071020040331DC647E"
    'Public SCHA890 As String = "462004071020040331DC647E"
    'Public SCHA895 As String = "462004071020040331DC647E"
    'Public SCHA915 As String = "46200509226500A915DA2E7E"
    'Public SCHA930 As String = "46200510072005102150D47E"
    'Public SCHA950 As String = "4620050202200505314A437E"
    'Public SCHA970 As String = "461967041020000214D7297E"
    'Public SCHA990 As String = "46200509902005121921337E"
    'Public SCHB239 As String = "465903365113726913282B7E"
    'Public SCHB259 As String = "465903365113726913282B7E"
    'Public SCHB279 As String = "465903365113726913282B7E"
    'Public SCHB309 As String = "465903365113726913282B7E"
    'Public SCHE159_Num1 As String = "466905110834560250906A7E"
    'Public SCHE159_Num2 As String = "4617D40000004000003FA97E"
    'Public SCHE170 As String = "46908001201080704794887E"
    'Public SCHE250 As String = "465D3C920C2D1C5CCA2B67E"
    'Public SCHF359_Num1 As String = "469020000080000006B417E"
    'Public SCHF359_Num2 As String = "46C07000008000000C5737E"
    'Public SCHF509 As String = "465903365113726913282B7E"
    'Public SCHF519 As String = "465903365113726913282B7E"
    'Public SCHF679 As String = "465903365113726913282B7E"
    'Public SCHI220 As String = "462FF811282FF9F3F617287E"
    'Public SCHI600 As String = "4600000000000000009BF37E"
    'Public SCHI700 As String = "4600000000000000009BF37E"
    'Public SCHI730 As String = "4600000000000000009BF37E"
    'Public SCHI760 As String = "4600000000000000009BF37E"
    'Public SCHI770 As String = "4600000000000000009BF37E"
    'Public SCHI830 As String = "462004071020040331DC647E"
    'Public SCHI910 As String = "4600000000000000009BF37E"
    'Public SCHL160 As String = "46775463076253647379CE7E"
    'Public SCHL210 As String = "461004062010F225605E7B7E"
    'Public SCHL310 As String = "462505062010F32560BEC47E"
    'Public SCHM450 As String = "461000EE01A04D0D005AF37E"
    'Public SCHN181 As String = "4600000375200112106EAF7E"
    'Public SCHN191 As String = "4600000375200112106EAF7E"
    'Public SCHN195 As String = "4600000375200112106EAF7E"
    'Public SCHN255_Num1 As String = "4600000000000000009BF37E"
    'Public SCHN255_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHN345 As String = "466905110834560250906A7E"
    ' ''IFFY WTF
    'Public SCHN356_Num1 As String = " 000003752001121086DF7E"
    'Public SCHN356_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHN362 As String = "462D4843533330364130DE7E"
    'Public SCHN375 As String = "46000003752001121086DF7E"
    'Public SCHN380 As String = "46000003752001121086DF7E"
    'Public SCHN392 As String = "462D4843533330364130DE7E"
    'Public SCHN393 As String = "462D4843533330364130DE7E"
    'Public SCHN395 As String = "462D4843533539334EFB8F7E"
    'Public SCHN415 As String = "466905110834560250906A7E"
    'Public SCHN480 As String = "466905110834560250906A7E"
    'Public SCHN485 As String = "466905110834560250906A7E"
    'Public SCHN500_Num1 As String = "46000003752001121086DF7E"
    'Public SCHN500_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHN530 As String = "46775463076253647379CE7E"
    'Public SCHR200 As String = "462004071020040331DC647E"
    'Public SCHR210 As String = "4620071122200802289A657E"
    'Public SCHR211 As String = "4620071122200802289A657E"
    'Public SCHR300 As String = "461981091019770418AD417E"
    'Public SCHR310 As String = "462007111420080131FB2D7E"
    'Public SCHR311 As String = "462007111420080131FB2D7E"
    'Public SCHR400 As String = "461981091019770418AD417E"
    'Public SCHR410 As String = "461981091019770418AD417E"
    'Public SCHR420 As String = "462008022120080801DC6E7E"
    'Public SCHR430 As String = "462007111420080131FB2D7E"
    'Public SCHR450 As String = "462007111420080131FB2D7E"
    'Public SCHR460 As String = "46200810292009022875657E"
    'Public SCHR470 As String = "462007111420080131FB2D7E"
    'Public SCHR500 As String = "46200510072005102150D47E"
    'Public SCHR510 As String = "46200510072005102150D47E"
    'Public SCHR550 As String = "462007121420071231B8D47E"
    'Public SCHR560 As String = "462009010620080131A24B7E"
    'Public SCHR561 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHR610 As String = "46200510072005102150D47E"
    'Public SCHR800 As String = "4620080606200812315F777E"
    'Public SCHR810 As String = "46200810212008022889657E"
    'Public SCHS109 As String = "46000003752001121086DF7E"
    'Public SCHS179 As String = "465903365113726913282B7E"
    'Public SCHS229 As String = "46000003752001121086DF7E"
    'Public SCHS250_Num1 As String = "46141C102802D192A02DA17E"
    'Public SCHS250_Num2 As String = "4649EE000000800100E48B7E"
    'Public SCHS259 As String = "465903365113726913282B7E"
    'Public SCHS269 As String = "465903365113726913282B7E"
    'Public SCHS299 As String = "465903365113726913282B7E"
    'Public SCHS350 As String = "4649EE000000800100E48B7E"
    'Public SCHS399 As String = "465903365113726913282B7E"
    'Public SCHT191_Num1 As String = "46000003752001121086DF7E"
    'Public SCHT191_Num2 As String = "460000353736303000A5F37E"
    'Public SCHU340_Num1 As String = "4620060822119013207F827E"
    'Public SCHU340_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SCHU350 As String = "461945081520081106E7207E"
    'Public SCHU410 As String = "461945081520070115ABDD7E"
    'Public SCHU430_Num1 As String = "461945081520070115ABDD7E"
    ' ''Public Dim SCHU430_Num2 As String =  "46012708600E48104B
    'Public SCHU440 As String = "462007111420080131FB2D7E"
    'Public SCHU450 As String = "46197619781978198028DF7E"
    'Public SCHU470 As String = "462007091020070470F1047E"
    'Public SCHU510 As String = "462206062010F50065212C7E"
    'Public SCHU520 As String = "46200510072005102150D47E"
    'Public SCHU540 As String = "46200610062006120401DD7E"
    'Public SCHU550 As String = "4620070101200705055FA77E"
    'Public SCHU620 As String = "46916310BDE04A5078AB637E"
    'Public SCHU650 As String = "461945081520070115ABDD7E"
    'Public SCHU700 As String = "46020067202006081563D37E"
    'Public SCHU740 As String = "460110131637430677D1A77E"
    'Public SCHU750 As String = "46197619781978198028DF7E"
    'Public SCHU810 As String = "461A04120C090CF0D2C7097E"
    'Public SCHU900 As String = "462007040920071231C6467E"
    'Public SCHU940 As String = "46020067202006081563D37E"
    'Public SCHV122 As String = "46FFFFFFFFFFFFFFFFFE747E"
    'Public SCHV30 As String = "46040B0000000001001F8A7E"
    ' ''Public Dim SCHV42 As String = "46054E321F5577CC33
    ' ''Public Dim SCHV500 As String = "461332241618886910
    ' ''Public Dim SCHV540 As String = "461847000040007F01
    ' ''Public Dim SCHV740 As String = "46002A02D1C26A002A
    ' ''Public Dim SCHV840 As String = "46E9FD000000800100
    'Public SCHW339 As String = "465903365113726913282B7E"
    ' ''Public Dim SCHX130 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX137 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX140 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX210_Num1 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX210_Num2 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX250 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX290_Num1 As String = "46540B000000600000
    ' ''Public Dim SCHX290_Num2 As String = "4608BC1847FFB5171C
    ' ''Public Dim SCHX420 As String = "466810000000000000
    ' ''Public Dim SCHX140 As String = "460D00A0E120329FE5
    'Public SCHX430 As String = "4601050F045F678FF9F3197E"
    ' ''Public Dim SCHX"460 As String = "460D00A0E120329FE5
    ' ''Public Dim SCHX780 As String = "46540B000000600000
    ' ''Public Dim SCHX820 As String = "46540B000000000100
    ' ''Public Dim SCHX839 As String = "462D4843532D303641
    'Public SCHX979 As String = "46000003752001121086DF7E6EAF7E"
    'Public SPHA303 As String = "462004071020040331DC647E"
    'Public SPHA460_Num1 As String = "46444F43544F5220A975817E"
    'Public SPHA460_Num2 As String = "464E4F56494E4B492198867E"
    'Public SPHA460_Num3 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA505 As String = "462D48435335303541C2BF7E"
    'Public SPHA523 As String = "46FFFFFFFFFFFFFFFFFE747E"
    'Public SPHA533 As String = "46FFFFFFFFFFFFFFFFFE747E"
    'Public SPHA560 As String = "4620050202200505314A437E"
    'Public SPHA580 As String = "4620050202200505314A437E"
    'Public SPHA600 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA620 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA640 As String = "462004071020040331DC647E"
    'Public SPHA660 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA680 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA700_Num1 As String = "4620050202200505314A437E"
    'Public SPHA700 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA740_Num1 As String = "462D48435335303541C2BF7E"
    'Public SPHA740_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA820 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA840_Num1 As String = "464E4F56494E4B492198867E"
    ' ''Public Dim SPHA840_Num2 As String = "464E4F5649E4B44921
    'Public SPHA880 As String = "4620055574A8800822145E7E"
    'Public SPHA900 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHA940 As String = "4620050202200505314A437E"
    'Public SPHA960 As String = "4620050202200505314A437E"
    ' ''Public Dim SPHI325_Num1 As String = "462007041320080115
    ' ''Public Dim SPHI325_Num2 As String = "461000F501184F4900
    'Public SPHI500 As String = "4600000000000000009BF37E"
    'Public SPHM300 As String = "461981091019770418AD417E"
    'Public SPHM320 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM330 As String = "46021C5143AB4A002025527E"
    'Public SPHM500 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM520 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM540_Num1 As String = "46021C5143AB4A002025527E"
    'Public SPHM540_Num2 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM550 As String = "46021C5143AB4A002025527E"
    'Public SPHM560 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM600 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM610 As String = "4620050202200505314A437E"
    'Public SPHM620 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM800 As String = "4601F2030F5F678FF9A23F7E"
    'Public SPHM810 As String = "4601F2030F5F678FF9A23F7E"


End Class
