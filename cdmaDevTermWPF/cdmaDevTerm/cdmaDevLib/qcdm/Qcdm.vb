Imports System.IO

Public Class Qcdm
    Public Enum Cmd As Short
        ''Random sht for make fix decoder? wat?
        NOT_A_COMMAND = - 1222

        DIAG_VERNO_F = &H0  '' Version Number Request/Response      
        DIAG_ESN_F = &H1  '' Mobile Station ESN Request/Response  
        DIAG_PEEKB_F = &H2  '' Peek byte Request/Response           
        DIAG_PEEKW_F = &H3  '' Peek word Request/Response           
        DIAG_PEEKD_F = &H4  '' Peek dword Request/Response          
        DIAG_POKEB_F = &H5  '' Poke byte Request/Response           
        DIAG_POKEW_F = &H6  '' Poke word Request/Response           
        DIAG_POKED_F = &H7  '' Poke dword Request/Response          
        DIAG_OUTP_F = &H8  '' Byte output Request/Response         
        DIAG_OUTPW_F = &H9  '' Word output Request/Response         
        DIAG_INP_F = &HA '' Byte input Request/Response          
        DIAG_INPW_F = &HB '' Word input Request/Response          
        DIAG_STATUS_F = &HC '' DMSS status Request/Response         
        '' 13-14 Reserved  
        DIAG_LOGMASK_F = &HF '' Set logging mask Request/Response   
        DIAG_LOG_F = &H10 '' Log packet Request/Response         
        DIAG_NV_PEEK_F = &H11 '' Peek at NV memory Request/Response  
        DIAG_NV_POKE_F = &H12 '' Poke at NV memory Request/Response  
        DIAG_BAD_CMD_F = &H13 '' Invalid Command Response            
        DIAG_BAD_PARM_F = &H14 '' Invalid parmaeter Response          
        DIAG_BAD_LEN_F = &H15 '' Invalid packet length Response      
        '' 22-23 Reserved  
        DIAG_BAD_MODE_F = &H18 '' Packet not allowed in this mode        
        ''    ( online vs offline )               
        DIAG_TAGRAPH_F = &H19 '' info for TA power and voice graphs     
        DIAG_MARKOV_F = &H1A '' Markov statistics                      
        DIAG_MARKOV_RESET_F = &H1B '' Reset of Markov statistics             
        DIAG_DIAG_VER_F = &H1C '' Return diag version for comparison to  
        '' detect incompatabilities               
        DIAG_TS_F = &H1D '' Return a timestamp                     
        DIAG_TA_PARM_F = &H1E '' Set TA parameters                      
        DIAG_MSG_F = &H1F '' Request for msg report                 
        DIAG_HS_KEY_F = &H20 '' Handset Emulation -- keypress          
        DIAG_HS_LOCK_F = &H21 '' Handset Emulation -- lock or unlock    
        DIAG_HS_SCREEN_F = &H22 '' Handset Emulation -- display request   
        '' 35 Reserved  
        DIAG_PARM_SET_F = &H24 '' Parameter Download                     
        '' 37 Reserved  
        DIAG_NV_READ_F = &H26 '' Read NV item   
        DIAG_NV_WRITE_F = &H27 '' Write NV item  
        '' 40 Reserved  
        DIAG_CONTROL_F = &H29 '' Mode change request                      
        DIAG_ERR_READ_F = &H2A '' Error record retreival                   
        DIAG_ERR_CLEAR_F = &H2B '' Error record clear                       
        DIAG_SER_RESET_F = &H2C '' Symbol error rate counter reset          
        DIAG_SER_REPORT_F = &H2D '' Symbol error rate counter report         
        DIAG_TEST_F = &H2E '' Run a specified test                     
        DIAG_GET_DIPSW_F = &H2F '' Retreive the current dip switch setting  
        DIAG_SET_DIPSW_F = &H30 '' Write new dip switch setting             
        DIAG_VOC_PCM_LB_F = &H31 '' Start/Stop Vocoder PCM loopback          
        DIAG_VOC_PKT_LB_F = &H32 '' Start/Stop Vocoder PKT loopback          
        '' 51-52 Reserved  
        DIAG_ORIG_F = &H35 '' Originate a call  
        DIAG_END_F = &H36 '' End a call        
        '' 55-57 Reserved  
        DIAG_DLOAD_F = &H3A '' Switch to downloader  
        DIAG_TMOB_F = &H3B '' Test Mode Commands and FTM commands 
        ''note:msm6500 changed to 75(that's DIAG_SUBSYS_CMD_F ) 
        ''DIAG_FTM_CMD_F = 75 '' Test Mode Commands and FTM commands 
        '' 60-62 Reserved  
        DIAG_STATE_F = &H3F '' Return the current state of the phone       
        DIAG_PILOT_SETS_F = &H40 '' Return all current sets of pilots           
        DIAG_SPC_F = &H41 '' Send the Service Prog. Code to allow SP     
        DIAG_BAD_SPC_MODE_F = &H42 '' Invalid nv_read/write because SP is locked  
        DIAG_PARM_GET2_F = &H43 '' get parms obsoletes PARM_GET                
        DIAG_SERIAL_CHG_F = &H44 '' Serial mode change Request/Response         
        '' 69 Reserved  
        DIAG_PASSWORD_F = &H46 '' Send password to unlock secure operations   
        DIAG_BAD_SEC_MODE_F = &H47 '' An operation was attempted which required 
        ''the phone to be in a security state that 
        '' is wasn't - like unlocked.                
        DIAG_PR_LIST_WR_F = &H48 '' Write Preferred Roaming list to the phone.  
        DIAG_PR_LIST_RD_F = &H49 '' Read Preferred Roaming list from the phone. 
        '' 74 Reserved  
        DIAG_SUBSYS_CMD_F = &H4B '' Dispatches a command to a subsystem         
        '' 76-80 Reserved  
        DIAG_FEATURE_QUERY_F = &H51 '' Asks the phone what it supports     
        '' 82 Reserved  
        DIAG_SMS_READ_F = &H53 '' Read SMS message out of NV          
        DIAG_SMS_WRITE_F = &H54 '' Write SMS message into NV           
        DIAG_SUP_FER_F = &H55 '' info for Frame Error Rate 
        '' on multiple channels                
        DIAG_SUP_WALSH_CODES_F = &H56 '' Supplemental channel walsh codes    
        DIAG_SET_MAX_SUP_CH_F = &H57 '' Sets the maximum # supplemental channels                            
        DIAG_PARM_GET_IS95B_F = &H58 '' get parms including SUPP and MUX2:  
        '' obsoletes PARM_GET and PARM_GET_2   
        ''#ifdef CODACOM_SYS_CAL '' 010911 CYI CAL ADJUST (Ãß°¡) => 
        ''LLD0911498 - KDH RF RAM R/W ---> 
        DIAG_RAM_RW_F = &H59          '' calibration RAM control using DM 
        ''100599-kdh for add cpu mode cal Åë½Å->  
        DIAG_CPU_RW_F = &H5A              '' calibration CPU control using DM 
        ''100599-kdh for add cpu mode cal Åë½Å<-  
        ''LLD0911498 - KDH RF RAM R/W <--- 
        ''#else 
        DIAG_FS_OP_F = &H59  '' Performs an Embedded File  
        '' System (EFS) operation.           
        DIAG_AKEY_VERIFY_F = &H5A '' AKEY Verification.                  
        ''#End If ''CODACOM_SYS_CAL '' 010911 CYI CAL ADJUST (Ãß°¡) <=  
        DIAG_BMP_HS_SCREEN_F = &H5B '' Handset emulation - Bitmap screen   
        DIAG_CONFIG_COMM_F = &H5C '' Configure communications            
        DIAG_EXT_LOGMASK_F = &H5D '' Extended logmask for > 32 bits.     
        '' 94-95 reserved  
        DIAG_EVENT_REPORT_F = &H60 '' Static Event reporting.             
        DIAG_STREAMING_CONFIG_F = &H61 '' Load balancing and more!           
        DIAG_PARM_RETRIEVE_F = &H62 '' Parameter retrieval                 
        DIAG_STATUS_SNAPSHOT_F = &H63 '' A state/status snapshot of the DMSS   
        DIAG_RPC_F = &H64 '' Used for RPC                  
        DIAG_GET_PROPERTY_F = &H65 '' Get_property requests         
        DIAG_PUT_PROPERTY_F = &H66 '' Put_property requests         
        DIAG_GET_GUID_F = &H67 '' Get_guid requests             
        DIAG_USER_CMD_F = &H68 '' Invocation of user callbacks  
        DIAG_GET_PERM_PROPERTY_F = &H69 '' Get permanent properties      
        DIAG_PUT_PERM_PROPERTY_F = &H6A '' Put permanent properties      
        DIAG_PERM_USER_CMD_F = &H6B '' Permanent user callbacks      
        DIAG_GPS_SESS_CTRL_F = &H6C '' GPS Session Control           
        DIAG_GPS_GRID_F = &H6D '' GPS search grid               
        DIAG_GPS_STATISTICS_F = &H6E '' GPS Statistics                
        DIAG_TUNNEL_F = &H6F '' DIAG Tunneling command code   
        ''#ifdef CODACOM_SYS_CAL '' 010911 CYI CAL ADJUST (Ãß°¡) => 
        ''DIAG_FS_OP_F = 112 '' Performs an Embedded File  
        ''                                 System (EFS) operation.           
        ''DIAG_AKEY_VERIFY_F     = 113 '' AKEY Verification.                  

        ''#else  
        ''LLD0911498 - KDH RF RAM R/W ---> 
        '' DIAG_RAM_RW_F			   = 112        	 '' calibration RAM control using DM 
        ''100599-kdh for add cpu mode cal Åë½Å->  
        '' DIAG_CPU_RW_F			   = 113         	 '' calibration CPU control using DM 
        ''100599-kdh for add cpu mode cal Åë½Å<-  
        ''LLD0911498 - KDH RF RAM R/W <--- 

        ''#End If ''CODACOM_SYS_CAL '' 010911 CYI CAL ADJUST (Ãß°¡) <=    
        DIAG_MAX_F '' Number of packets defined.  

        '' Software Version Request/Response		   
        '' Software Version Request/Response		   
        DIAG_SW_VERSION_F = &H38   ''2004-12-16 zhong added later 
        DIAG_SET_FTM_TEST_MODE = &H72
    End Enum

    Public Enum FSItems As Short
        BASE = 0
    End Enum

    '' not sure if this is novatel exclusive?
    ''http://www.nynaeve.net/Code/NvtlResearch-1.txt
    Public Enum SubsysCmd As Short

        HDR = &H5
        SMS = &HE
        CM = &HF
        GPS = &HD
        STORAGE = &H13
        OLD_CONTROL = &H32
        CONTROL = &HFA
        ''no idea what this is = to
        LAST_SUBSYS
    End Enum

    Public Enum Response As Short

        DIAG_ERR_SUCCESS = &H0
        DIAG_ERR_FAIL = &H1
        DIAG_ERR_BAD_CMD = &H2
        DIAG_ERR_BAD_PARAM = &H3
        DIAG_ERR_BAD_LENGTH = &H4
        DIAG_ERR_BAD_SECURITY_MODE = &H5
        DIAG_ERR_BAD_REPLY_CMD = &HB
        DIAG_ERR_BAD_SPC_MODE = &H15
        DIAG_ERR_BAD_MODE = &H20
    End Enum

    Public Enum Mode As Short

        MODE_RADIO_OFFLINE = &H1
        MODE_RADIO_RESET = &H2
        MODE_RADIO_ONLINE = &H4
        MODE_RADIO_LOWPOWER = &H5
    End Enum

    Public Enum SubsysStorage As Short

        DIAG_EFS2_OPEN = &H2
        DIAG_EFS2_CLOSE = &H3
        DIAG_EFS2_READ = &H4
        DIAG_EFS2_WRITE = &H5
        DIAG_EFS2_UNLINK = &H8
        DIAG_EFS2_OPENDIR = &HB
        DIAG_EFS2_READDIR = &HC
        DIAG_EFS2_CLOSEDIR = &HD
    End Enum

    Public Enum diagpkt_subsys_cmd_enum_type As Short

        DIAG_SUBSYS_OEM = 0       ''/* Reserved for OEM use */
        DIAG_SUBSYS_ZREX = 1       ''/* ZREX */
        DIAG_SUBSYS_SD = 2       ''/* System Determination */ 	
        DIAG_SUBSYS_BT = 3       ''/* Bluetooth */ 	
        DIAG_SUBSYS_WCDMA = 4       ''/* WCDMA */ 	
        DIAG_SUBSYS_HDR = 5       ''/* 1xEvDO */	
        DIAG_SUBSYS_DIABLO = 6       ''/* DIABLO */ 	
        DIAG_SUBSYS_TREX = 7       ''/* TREX - Off-target testing environments */	
        DIAG_SUBSYS_GSM = 8       ''/* GSM */ 	
        DIAG_SUBSYS_UMTS = 9       ''/* UMTS */	
        DIAG_SUBSYS_HWTC = 10      ''/* HWTC */ 	
        DIAG_SUBSYS_FTM = 11      ''/* Factory Test Mode */	
        DIAG_SUBSYS_REX = 12      ''/* Rex */	
        DIAG_SUBSYS_OS = DIAG_SUBSYS_REX
        DIAG_SUBSYS_GPS = 13      ''/* Global Positioning System */
        DIAG_SUBSYS_WMS = 14      ''/* Wireless Messaging Service (WMS SMS) */
        DIAG_SUBSYS_CM = 15      ''/* Call Manager */	
        DIAG_SUBSYS_HS = 16      ''/* Handset */	
        DIAG_SUBSYS_AUDIO_SETTINGS = 17      ''/* Audio Settings */ 	
        DIAG_SUBSYS_DIAG_SERV = 18      ''/* DIAG Services */ 	
        DIAG_SUBSYS_FS = 19      ''/* File System - EFS2 */	
        DIAG_SUBSYS_PORT_MAP_SETTINGS = 20      ''/* Port Map Settings */ 	
        DIAG_SUBSYS_MEDIAPLAYER = 21      ''/* QCT Mediaplayer */	
        DIAG_SUBSYS_QCAMERA = 22      ''/* QCT QCamera */ 	
        DIAG_SUBSYS_MOBIMON = 23      ''/* QCT MobiMon */	
        DIAG_SUBSYS_GUNIMON = 24      ''/* QCT GuniMon */	
        DIAG_SUBSYS_LSM = 25      ''/* Location Services Manager */	
        DIAG_SUBSYS_QCAMCORDER = 26      ''/* QCT QCamcorder */	
        DIAG_SUBSYS_MUX1X = 27      ''/* Multiplexer */ 	
        DIAG_SUBSYS_DATA1X = 28      ''/* Data */	
        DIAG_SUBSYS_SRCH1X = 29      ''/* Searcher */ 	
        DIAG_SUBSYS_CALLP1X = 30      ''/* Call Processor */	
        DIAG_SUBSYS_APPS = 31      ''/* Applications */	
        DIAG_SUBSYS_SETTINGS = 32      ''/* Settings */	
        DIAG_SUBSYS_GSDI = 33      ''/* Generic SIM Driver Interface */	
        DIAG_SUBSYS_UIMDIAG = DIAG_SUBSYS_GSDI
        DIAG_SUBSYS_TMC = 34      ''/* Task Main Controller */	
        DIAG_SUBSYS_USB = 35      ''/* Universal Serial Bus */	
        DIAG_SUBSYS_PM = 36      ''/* Power Management */	
        DIAG_SUBSYS_DEBUG = 37
        DIAG_SUBSYS_QTV = 38
        DIAG_SUBSYS_CLKRGM = 39      ''/* Clock Regime */	
        DIAG_SUBSYS_DEVICES = 40
        DIAG_SUBSYS_WLAN = 41      ''/* 802.11 Technology */	
        DIAG_SUBSYS_PS_DATA_LOGGING = 42      ''/* Data Path Logging */	
        DIAG_SUBSYS_PS = DIAG_SUBSYS_PS_DATA_LOGGING
        DIAG_SUBSYS_MFLO = 43      ''/* MediaFLO */	
        DIAG_SUBSYS_DTV = 44      ''/* Digital TV */ 	
        DIAG_SUBSYS_RRC = 45      ''/* WCDMA Radio Resource Control state */ 	
        DIAG_SUBSYS_PROF = 46      ''/* Miscellaneous Profiling Related */	
        DIAG_SUBSYS_TCXOMGR = 47
        DIAG_SUBSYS_NV = 48      ''/* Non Volatile Memory */	
        DIAG_SUBSYS_AUTOCONFIG = 49
        DIAG_SUBSYS_PARAMS = 50      ''/* Parameters required for debugging subsystems */ 	
        DIAG_SUBSYS_MDDI = 51      ''/* Mobile Display Digital Interface */	
        DIAG_SUBSYS_DS_ATCOP = 52
        DIAG_SUBSYS_L4LINUX = 53      ''/* L4/Linux */	
        DIAG_SUBSYS_MVS = 54      ''/* Multimode Voice Services */ 	
        DIAG_SUBSYS_CNV = 55      ''/* Compact NV */	
        DIAG_SUBSYS_APIONE_PROGRAM = 56      ''/* apiOne */
        DIAG_SUBSYS_HIT = 57      ''/* Hardware Integration Test */
        DIAG_SUBSYS_DRM = 58      ''/* Digital Rights Management */	
        DIAG_SUBSYS_DM = 59      ''/* Device Management */	
        DIAG_SUBSYS_FC = 60      ''/* Flow Controller */	
        DIAG_SUBSYS_MEMORY = 61      ''/* Malloc Manager */ 	
        DIAG_SUBSYS_FS_ALTERNATE = 62      ''/* Alternate File System */	
        DIAG_SUBSYS_REGRESSION = 63      ''/* Regression Test Commands */	
        DIAG_SUBSYS_SENSORS = 64      ''/* The sensors subsystem */	
        DIAG_SUBSYS_FLUTE = 65      ''/* FLUTE */	
        DIAG_SUBSYS_ANALOG = 66      ''/* Analog die subsystem */	
        DIAG_SUBSYS_APIONE_PROGRAM_MODEM = 67    ''/* apiOne Program On Modem Processor */ 	
        DIAG_SUBSYS_LTE = 68      ''/* LTE */	
        DIAG_SUBSYS_BREW = 69      ''/* BREW */
        DIAG_SUBSYS_PWRDB = 70      ''/* Power Debug Tool */ 	
        DIAG_SUBSYS_CHORD = 71      ''/* Chaos Coordinator */	
        DIAG_SUBSYS_SEC = 72      ''/* Security */	
        DIAG_SUBSYS_TIME = 73      ''/* Time Services */ 	
        DIAG_SUBSYS_Q6_CORE = 74      ''/* Q6 core services */	
        DIAG_SUBSYS_COREBSP = 75      ''/* CoreBSP */	
        '' '/* Command code allocation:
        ''  [0 - 2047]        - HWENGINES 	
        ''  [2048 - 2147]        - MPROC
        ''  [2148 - 2247]        - BUSES
        ''  [2248 - 2347]        - USB
        ''  [2348 - 65535]        - Reserved	
        '' */
            DIAG_SUBSYS_MFLO2 = 76      ''/* Media Flow */
        '' '/* Command code allocation:
        ''    [0 - 1023]       - APPs
        ''    [1024 - 65535]   - Reserved	
        ''*/
            DIAG_SUBSYS_ULOG = 77  ''/* ULog Services */	
        DIAG_SUBSYS_APR = 78  ''/* Asynchronous Packet Router (Yu Andy)*/	
        DIAG_SUBSYS_QNP = 79  ''/*QNP (Ravinder Are  Arun Harnoor)*/	
        DIAG_SUBSYS_STRIDE = 80  ''/* Ivailo Petrov */	
        DIAG_SUBSYS_OEMDPP = 81  ''/* to read/write calibration to DPP partition */	
        DIAG_SUBSYS_Q5_CORE = 82  ''/* Requested by ADSP team */	
        DIAG_SUBSYS_USCRIPT = 83  ''/* core/power team USCRIPT tool */		
        DIAG_SUBSYS_LAST
        ''/* Subsystem IDs reserved for OEM use */
        DIAG_SUBSYS_RESERVED_OEM_0 = 250
        DIAG_SUBSYS_RESERVED_OEM_1 = 251
        DIAG_SUBSYS_RESERVED_OEM_2 = 252
        DIAG_SUBSYS_RESERVED_OEM_3 = 253
        DIAG_SUBSYS_RESERVED_OEM_4 = 254
        DIAG_SUBSYS_LEGACY = 255
    End Enum

    ''WARNING WARNING< all of the EFS code is buyer-beware not very tested code 
    ''(somewhere between probably to possibly not working for any and all models, may even brick your phone--I warned you)

    Public Function ReadEfsFolderByName(ByVal folderName As String) As String

        cdmaTerm.Q.Clear()

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, SubsysStorage.DIAG_EFS2_READ, New Byte() {&HE, &H0},
                                   "Read EFS", "stupidfix"))


        Dim openFolder As New List(Of Byte)
        openFolder.AddRange(New Byte() {SubsysStorage.DIAG_EFS2_OPENDIR, &H0})
        ''  &H2F,
        For Each c As Char In folderName
            openFolder.Add(Convert.ToUInt32(c))
        Next
        openFolder.Add(&H0)
        ''}

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13, openFolder.ToArray, "OPENDIR " + folderName, "stupidfix"))
        Dim j As Integer = 1

        ''TODO: Increase max count to test and fix efs loop not stoping/arithmatic overflow exception
        Dim count As Integer = 0
        ''Dim MaxCount As Integer = &HFE
        Dim MaxCount As Integer = &HFE

        While (ReadEfsDir(j) And count < MaxCount)
            j += 1
            count += 1
        End While

        '' For i = 0 To count
        ''cdmaTerm.Q.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, i, &H0, &H0, &H0}, "READDIR", "stupidfix"))
        ''Next

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13,
                                   New Byte() {SubsysStorage.DIAG_EFS2_CLOSEDIR, &H0, &H1, &H0, &H0, &H0}, "CLOSEDIR /",
                                   "stupidfix"))

        cdmaTerm.Q.Run()

        Return New String("?")
    End Function

    Public Function ReadEfsRoot() As String

        cdmaTerm.Q.Clear()

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, SubsysStorage.DIAG_EFS2_READ, New Byte() {&HE, &H0},
                                   "Read EFS", "stupidfix"))
        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13,
                                   New Byte() {SubsysStorage.DIAG_EFS2_OPENDIR, &H0, &H2F, &H0}, "OPENDIR /",
                                   "stupidfix"))
        Dim j As Integer = 1

        ''TODO: Increase max count to test and fix efs loop not stoping/arithmatic overflow exception
        Dim count As Integer = 0
        Dim MaxCount As Integer = &HFE

        While (ReadEfsDir(j) And count < MaxCount)
            j += 1
            count += 1
        End While

        '' For i = 0 To count
        ''cdmaTerm.Q.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, i, &H0, &H0, &H0}, "READDIR", "stupidfix"))
        ''Next

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13,
                                   New Byte() {SubsysStorage.DIAG_EFS2_CLOSEDIR, &H0, &H1, &H0, &H0, &H0}, "CLOSEDIR /",
                                   "stupidfix"))

        cdmaTerm.Q.Run()

        Return New String("?")
    End Function

    Public LastEfsWorked2 As Boolean = False

    Public Function ReadEfsDir(ByVal dir As Integer) As Boolean

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13,
                                   New Byte() _
                                      {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, dir, &H0, &H0, &H0},
                                   "READDIR", "stupidfix"))

        cdmaTerm.Q.Run()

        Return LastEfsWorked2
    End Function


    Public Sub OpenEfsFileForWrite(ByVal fileName As String)
        '' 4B 13 02 00 41 02 00 00 B6 01 00 00 62 72 65 77 2F 63 75 73 74 6F 6D 65 72 2F 53 68 6F 72 74 2E 70 6E 67 00 E1 A4 7E
        ''       |                             62 72 65 77 2F 63 75 73 74 6F 6D 65 72 2F 53 68 6F 72 74 2E 70 6E 67
        ''       OPEN(fileName)                brew/customer/Short.png
        ''
        ''ascii: K...A...¶...brew/customer/Short.png.á¤~
        ''
        Dim openEfsHeader As Byte() = {&H2, &H0, &H41, &H2, &H0, &H0, &HB6, &H1, &H0, &H0}
        Dim OpenForWrite As New List(Of Byte)
        OpenForWrite.AddRange(openEfsHeader)
        ''todo: not needed in lib i think?
        'If cdmaTerm.EfsPathTxtbox.Text <> "/" Then
        '    fileName = cdmaTerm.EfsPathTxtbox.Text + "/" + fileName
        'End If

        For Each c As Char In fileName
            OpenForWrite.Add(Convert.ToUInt32(c))
        Next
        OpenForWrite.Add(0)

        cdmaTerm.Q.Clear()
        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, SubsysCmd.STORAGE, OpenForWrite.ToArray, "OpenEfsForWrite",
                                   "StupidFix"))
        cdmaTerm.Q.Run()
    End Sub

    Public Sub DeleteFromEFS(ByVal fileName As String, path As String)

        Dim writeHeader As Byte() = {&H8, &H0}
        Dim efsPacket As New List(Of Byte)

        efsPacket.AddRange(writeHeader)

        fileName = path + "/" + fileName

        For Each c As Char In fileName
            efsPacket.Add(Convert.ToUInt32(c))
        Next

        efsPacket.Add(&H0)

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13, efsPacket.ToArray, "DeleteEFS", "stupidfix"))

        Dim EfsDirectory As New List(Of Byte)
        EfsDirectory.AddRange(New Byte() {&HB, 0, 0, 0, 0})

        For Each c As Char In path
            EfsDirectory.Add(Convert.ToUInt32(c))
        Next

        EfsDirectory.Add(0)

        cdmaTerm.Q.Add(New Command(Cmd.DIAG_FS_OP_F, EfsDirectory.ToArray, "EfsDirectory"))
    End Sub

    Dim EfsPacketLength As Integer = 527
    Dim EfsHeaderLength As Integer = 8
    Dim EfsPacketMinusHeader = (EfsPacketLength - EfsHeaderLength)

    Public Function WriteEfsFile(ByVal fileName As String) As Boolean
        Try
            OpenEfsFileForWrite(fileName)
            Return WriteEfsFile(ReadFileToBytes(fileName))
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function WriteEfsFile(ByVal fileNameBytes As Byte()) As Boolean

        Dim currentFileBytes As New List(Of Byte())

        Dim FileLength As Integer = fileNameBytes.Length
        Dim NumOfPackets As Integer = (FileLength/(EfsPacketMinusHeader))


        Dim CurrentPacket As Integer = 0
        Dim CurrentOffset As Integer = 0


        Dim lastPacketSize As Integer = FileLength - ((NumOfPackets - 1)*EfsPacketMinusHeader)

        For i = 0 To NumOfPackets - 1

            Dim insideLoopPacket As New List(Of Byte)

            Dim endOfCurrent As Integer
            If (i = NumOfPackets - 1) Then
                endOfCurrent = ((i)*EfsPacketMinusHeader) - 1 + lastPacketSize
            Else
                endOfCurrent = (i + 1)*EfsPacketMinusHeader
            End If

            For j = i*EfsPacketMinusHeader To endOfCurrent
                ''For j = i * EfsPacketLength To EfsPacketMinusHeader
                insideLoopPacket.Add(fileNameBytes(j))
            Next
            currentFileBytes.Add(insideLoopPacket.ToArray)

            '' currentFileBytes.AddRange(fileNameBytes(i*EfsPacketMinusHeader, fileNameBytes
        Next


        For i = 0 To NumOfPackets - 1

            Dim writeHeader As Byte() = {&H5, &H0, &H0, &H0, &H0, &H0}
            Dim efsPacket As New List(Of Byte)
            efsPacket.AddRange(writeHeader)
            Dim hexPacketNumber As String = CurrentPacket.ToString("x4")
            efsPacket.AddRange(hexPacketNumber.Substring(2, 2).ToHexBytes())
            efsPacket.AddRange(hexPacketNumber.Substring(0, 2).ToHexBytes())
            efsPacket.Add(&H0)
            efsPacket.Add(&H0)

            ''TODO: should be fileName ( from offset to offset + EfsPacketMinusHeader )
            efsPacket.AddRange(currentFileBytes(i))
            '' attach section of binary to packet
            '
            '                           [Packet Count] 
            '4B 13 05 00 00 00 00 00 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            'tx:
            '4B 13 05 00 00 00 00 00 00 00 00 00 12 34 56 78 90 12 34 56 78 90 FF FF FF FF AB CD AB  
            '       |
            '       WRITE (raw data)
            'CD AB CD 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  
            '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

            Dim debugstring As String = "DIAG_EFS2_WRITE " + i.ToString
            cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, &H13, efsPacket.ToArray, debugstring, "stupidfix"))


            CurrentPacket += 2
            CurrentOffset += EfsPacketMinusHeader

        Next
        cdmaTerm.Q.Add(New Command(Cmd.DIAG_SUBSYS_CMD_F, SubsysCmd.STORAGE, New Byte() {&H3, 0, 0, 0, 0, 0},
                                   "DIAG_EFS2_CLOSE", "StupidFix"))

        Return True
    End Function

    Private Function ReadFileToBytes(ByVal filename As String) As Byte()
        Dim fileExists = File.Exists(filename)

        If (fileExists) Then
            Dim input As New FileStream(filename, FileMode.Open)
            Dim bytes(CInt(input.Length - 1)) As Byte
            input.Read(bytes, 0, CInt(input.Length))
            input.Close()
            Return bytes
        End If

        Return Nothing
    End Function

    Public Enum Qcmip
        Simple
        MobileAndSimple
        Mobile
    End Enum
End Class

