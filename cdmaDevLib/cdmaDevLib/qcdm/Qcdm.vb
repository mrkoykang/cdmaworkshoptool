Imports System.IO

Public Class Qcdm

    Public Enum Cmd As Short
        ''Random sht for make fix decoder? wat?
        NOT_A_COMMAND = -1222

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

    ''WARNING WARNING< all of the EFS code is buyer-beware not very tested code 
    ''(somewhere between probably to possibly not working for any and all models, may even brick your phone--I warned you)

    Public Function ReadEfsFolderByName(ByVal folderName As String) As String

        cdmaTerm.dispatchQ.clearCommandQ()

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, SubsysStorage.DIAG_EFS2_READ, New Byte() {&HE, &H0}, "Read EFS", "stupidfix"))


        Dim openFolder As New List(Of Byte)
        openFolder.AddRange(New Byte() {SubsysStorage.DIAG_EFS2_OPENDIR, &H0})
        ''  &H2F,
        For Each c As Char In folderName
            openFolder.Add(System.Convert.ToUInt32(c))
        Next
        openFolder.Add(&H0)
        ''}

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, openFolder.ToArray, "OPENDIR " + folderName, "stupidfix"))
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
        ''cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, i, &H0, &H0, &H0}, "READDIR", "stupidfix"))
        ''Next

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_CLOSEDIR, &H0, &H1, &H0, &H0, &H0}, "CLOSEDIR /", "stupidfix"))

        cdmaTerm.dispatchQ.executeCommandQ()

        Return New String("?")

    End Function

    Public Function ReadEfsRoot() As String

        cdmaTerm.dispatchQ.clearCommandQ()

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, SubsysStorage.DIAG_EFS2_READ, New Byte() {&HE, &H0}, "Read EFS", "stupidfix"))
        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_OPENDIR, &H0, &H2F, &H0}, "OPENDIR /", "stupidfix"))
        Dim j As Integer = 1

        ''TODO: Increase max count to test and fix efs loop not stoping/arithmatic overflow exception
        Dim count As Integer = 0
        Dim MaxCount As Integer = &HFE

        While (ReadEfsDir(j) And count < MaxCount)
            j += 1
            count += 1
        End While

        '' For i = 0 To count
        ''cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, i, &H0, &H0, &H0}, "READDIR", "stupidfix"))
        ''Next

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_CLOSEDIR, &H0, &H1, &H0, &H0, &H0}, "CLOSEDIR /", "stupidfix"))

        cdmaTerm.dispatchQ.executeCommandQ()

        Return New String("?")

    End Function
    Public LastEfsWorked2 As Boolean = False

    Public Function ReadEfsDir(ByVal dir As Integer) As Boolean

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, New Byte() {SubsysStorage.DIAG_EFS2_READDIR, &H0, &H1, &H0, &H0, &H0, dir, &H0, &H0, &H0}, "READDIR", "stupidfix"))

        cdmaTerm.dispatchQ.executeCommandQ()



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
            OpenForWrite.Add(System.Convert.ToUInt32(c))
        Next
        OpenForWrite.Add(0)

        cdmaTerm.dispatchQ.clearCommandQ()
        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, SubsysCmd.STORAGE, OpenForWrite.ToArray, "OpenEfsForWrite", "StupidFix"))
        cdmaTerm.dispatchQ.executeCommandQ()

    End Sub

    Public Sub DeleteFromEFS(ByVal fileName As String, path As String)

        Dim writeHeader As Byte() = {&H8, &H0}
        Dim efsPacket As New List(Of Byte)

        efsPacket.AddRange(writeHeader)

        fileName = path + "/" + fileName

        For Each c As Char In fileName
            efsPacket.Add(System.Convert.ToUInt32(c))
        Next

        efsPacket.Add(&H0)

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, efsPacket.ToArray, "DeleteEFS", "stupidfix"))

        Dim EfsDirectory As New List(Of Byte)
        EfsDirectory.AddRange(New Byte() {&HB, 0, 0, 0, 0})

        For Each c As Char In path
            EfsDirectory.Add(System.Convert.ToUInt32(c))
        Next

        EfsDirectory.Add(0)

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_FS_OP_F, EfsDirectory.ToArray, "EfsDirectory"))
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
        Dim NumOfPackets As Integer = (FileLength / (EfsPacketMinusHeader))


        Dim CurrentPacket As Integer = 0
        Dim CurrentOffset As Integer = 0


        Dim lastPacketSize As Integer = FileLength - ((NumOfPackets - 1) * EfsPacketMinusHeader)

        For i = 0 To NumOfPackets - 1

            Dim insideLoopPacket As New List(Of Byte)

            Dim endOfCurrent As Integer
            If (i = NumOfPackets - 1) Then
                endOfCurrent = ((i) * EfsPacketMinusHeader) - 1 + lastPacketSize
            Else
                endOfCurrent = (i + 1) * EfsPacketMinusHeader
            End If

            For j = i * EfsPacketMinusHeader To endOfCurrent
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
            efsPacket.AddRange(cdmaTerm.String_To_Bytes(hexPacketNumber.Substring(2, 2)))
            efsPacket.AddRange(cdmaTerm.String_To_Bytes(hexPacketNumber.Substring(0, 2)))
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
            cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_SUBSYS_CMD_F, &H13, efsPacket.ToArray, debugstring, "stupidfix"))


            CurrentPacket += 2
            CurrentOffset += EfsPacketMinusHeader

        Next
        cdmaTerm.dispatchQ.addCommandToQ(New Command(Cmd.DIAG_SUBSYS_CMD_F, SubsysCmd.STORAGE, New Byte() {&H3, 0, 0, 0, 0, 0}, "DIAG_EFS2_CLOSE", "StupidFix"))

        Return True

    End Function

    Private Function ReadFileToBytes(ByVal filename As String) As Byte()
        Dim fileExists = System.IO.File.Exists(filename)

        If (fileExists) Then
            Dim input As New FileStream(filename, FileMode.Open)
            Dim bytes(CInt(input.Length - 1)) As Byte
            input.Read(bytes, 0, CInt(input.Length))
            input.Close()
            Return bytes
        End If
        
        Return Nothing
    End Function

End Class

