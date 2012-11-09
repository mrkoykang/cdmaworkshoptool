Public Class Cmd_DIAG_VERNO_F
    Inherits Command
    Sub New(ByVal qcdm As Qcdm.Cmd, ByVal qcData As Byte(), ByVal debuggingTextIn As String)
        MyBase.New(qcdm, qcData, debuggingTextIn)
    End Sub
    ''diag verno?
    ''#define COMPI_DATE_STRLEN	12 
    ''#define COMPI_TIME_STRLEN	9 
    ''#define SW_VERSION_STRLEN	21 
    ''    typedef struct 
    ''    { 
    ''	    char sw_ver_str[ SW_VERSION_STRLEN ]; 
    ''	    char comp_date[ COMPI_DATE_STRLEN ];      
    ''	    char comp_time[ COMPI_TIME_STRLEN ];	 
    ''    } diag_sw_ver_rsp_type; 
    Public Overrides Sub decode()
        Try

        Catch ex As Exception
            logger.add("err in diag verno f decode: " + ex.ToString(), logger.logType.msg)

        End Try
    End Sub
End Class
