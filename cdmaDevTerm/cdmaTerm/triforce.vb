' '' CDMA DEV TERM
' '' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
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

Public Class SamsungFullFlashing

    ''This class is pretty sketchy as of current, duplicates and such


    Public samsung_u350_pass_1 As Byte() = {&H27, &HD2, &H1, &H0, &H7, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H7, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_user_1_264 As Byte() = {&H27, &HD1, &H1, &H0, &H18, &H37, &H32, &H30, &H35, &H35, &H35, &H39, &H38, &H35, &H38, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1, &H2C, &H1, &H0, &H0, &H1, &H2, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_user_2_264 As Byte() = {&H27, &H8E, &H3, &H18, &H37, &H32, &H30, &H36, &H36, &H36, &H39, &H38, &H35, &H38, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_pass_2 As Byte() = {&H27, &H8A, &H3, &H7, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_user_3_264 As Byte() = {&H27, &HAA, &H4, &H14, &H30, &H30, &H30, &H30, &H30, &H30, &H38, &H39, &H31, &H31, &H40, &H76, &H7A, &H77, &H33, &H67, &H2E, &H63, &H6F, &H6D, &H6D, &H74, &HF5, &HE0, &HFC, &H68, &HF7, &HF8, &HF8, &H30, &HF8, &H68, &HF7, &H4, &HF7, &H30, &HF8, &H48, &HF4, &HA0, &HF6, &H74, &HF5, &H74, &HF5, &HCC, &HF7, &H30, &HF8, &H30, &HF8, &HCC, &HF7, &HCC, &HF7, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H88, &HFA, &HB4, &HFB, &HE0, &HFC, &H1, &H2C, &H1, &H0, &H0, &H1, &H2, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &H0, &H0, &H0, &H5, &H19, &HAC, &H0, &H1, &H0, &H0, &H0, &HF8, &HF8, &HCE, &H2, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H78, &H42, &HC0, &H2, &H6D, &H15, &HAC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_mms_server_1 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &HF, &H68, &H74, &H74, &H70, &H3A, &H2F, &H2F, &H6D, &H6D, &H73, &H2E, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H2F, &H73, &H65, &H72, &H76, &H6C, &H65, &H74, &H73, &H2F, &H6D, &H6D, &H73, &H0, &HD3, &HEC, &H7E}

    Public samsung_u350_servlets_1 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &H10, &H2F, &H73, &H65, &H72, &H76, &H6C, &H65, &H74, &H73, &H2F, &H6D, &H6D, &H73, &H0, &HEA, &HAE, &H7E}

    Public samsung_u350_upload_address_1 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &H11, &H31, &H31, &H31, &H31, &H31, &H31, &H0, &HA8, &H8D, &H7E}

    Public samsung_u350_user_4_34 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &H14, &H37, &H32, &H30, &H34, &H34, &H34, &H39, &H38, &H35, &H38, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &HF7, &H50, &H7E}

    Public samsung_u350_pass_3 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &H15, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &H91, &HC0, &H7E}

    Public samsung_u350_user_5_34 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H4, &H37, &H32, &H30, &H38, &H34, &H32, &H39, &H38, &H35, &H38, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H7F, &HF2, &H7E}

    Public samsung_u350_pass_4 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H5, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &HBE, &HA3, &H7E}

    Public samsung_u350_user_6_34 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H6, &H37, &H32, &H30, &H38, &H34, &H32, &H39, &H38, &H35, &H38, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &HCE, &H60, &H7E}

    Public samsung_u350_pass_5 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H7, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H0, &H44, &H38, &H7E}

    Public samsung_u350_proxy_1 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H8, &H77, &H61, &H70, &H2E, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &HF2, &HEF, &H7E}

    Public samsung_u350_proxy_2 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H9, &H77, &H61, &H70, &H2E, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &HBB, &H7C, &H7E}

    Public samsung_u350_homepage As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &HA, &H68, &H74, &H74, &H70, &H3A, &H2F, &H2F, &H68, &H6F, &H6D, &H65, &H70, &H61, &H67, &H65, &H0, &HA6, &H2E, &H7E}

    Public samsung_u350_uaprof As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &HB, &H68, &H74, &H74, &H70, &H3A, &H2F, &H2F, &H75, &H61, &H70, &H72, &H6F, &H66, &H2E, &H76, &H74, &H65, &H78, &H74, &H2E, &H63, &H6F, &H6D, &H2F, &H73, &H63, &H68, &H2F, &H75, &H33, &H35, &H30, &H2F, &H75, &H33, &H35, &H30, &H2E, &H78, &H6D, &H6C, &H0, &HF9, &H13, &H7E}

    ''being round 2 definitions


    Public samsung_u350_r2_1 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &HF, &H68, &H74, &H74, &H70, &H3A, &H2F, &H2F, &H6D, &H6D, &H73, &H2E, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H2F, &H73, &H65, &H72, &H76, &H6C, &H65, &H74, &H73, &H2F, &H6D, &H6D, &H73, &H0, &HD3, &HEC, &H7E}

    Public samsung_u350_r2_2 As Byte() = {&H4B, &HFB, &H0, &H0, &HA, &H14, &H0, &HF9, &H51, &H7E}

    Public samsung_u350_r2_3 As Byte() = {&H4B, &HFB, &H0, &H0, &HF, &H6, &H37, &H32, &H30, &H31, &H31, &H31, &H32, &H32, &H32, &H32, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H97, &H1D, &H7E}

    Public samsung_u350_r2_4 As Byte() = {&H27, &HD1, &H1, &H0, &H18, &H37, &H32, &H30, &H32, &H32, &H32, &H33, &H33, &H33, &H33, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1, &H2C, &H1, &H0, &H0, &H1, &H2, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_r2_5 As Byte() = {&H27, &H8E, &H3, &H18, &H37, &H32, &H30, &H33, &H33, &H33, &H34, &H34, &H34, &H34, &H40, &H6D, &H79, &H63, &H72, &H69, &H63, &H6B, &H65, &H74, &H2E, &H63, &H6F, &H6D, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_r2_6 As Byte() = {&H27, &HD2, &H1, &H0, &HA, &H54, &H65, &H46, &H74, &H56, &H54, &H67, &H57, &H54, &H64, &H0, &H0, &H0, &H0, &H0, &H0, &HA, &H54, &H65, &H46, &H74, &H56, &H54, &H67, &H57, &H54, &H65, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public samsung_u350_r2_8 As Byte() = {&H27, &H8A, &H3, &HA, &H54, &H65, &H46, &H74, &H56, &H54, &H67, &H57, &H54, &H66, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}





    Public Sub full_flash_u350()

        ''setup and connect
        cdmaTerm.scanAndListComs()
        cdmaTerm.connectSub()

        cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.modeOfflineD, ""))
        cdmaTerm.dispatchQ.executeCommandQ()
        Threading.Thread.Sleep(250)
        cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.send16digitSchU350, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.writeSPC_DefMethod000000, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.sendSPC_DefMethod000000, ""))

        cdmaTerm.sendPRL(cdmaTerm.selectPRLComboBox.Text)




        ''round 1
        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_pass_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_user_1_264, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_user_2_264, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_pass_2, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_user_3_264, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_mms_server_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_servlets_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_upload_address_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_user_4_34, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_pass_3, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_user_5_34, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_pass_4, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_pass_5, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_proxy_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_proxy_2, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_homepage, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_uaprof, ""))


        ''begin u350 full flash test round 2
        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_1, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_2, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_3, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_4, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_5, ""))

        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_6, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(samsung_u350_r2_8, ""))


        cdmaTerm.dispatchQ.addCommandToQ(New Command(cdmaTerm.modeReset, ""))

        MessageBox.Show("one small loop for man, one giant leap for electronic kind")
    End Sub




End Class
