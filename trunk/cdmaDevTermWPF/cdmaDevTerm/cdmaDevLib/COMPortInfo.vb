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
''
''ref: http://dariosantarelli.wordpress.com/2010/10/18/c-how-to-programmatically-find-a-com-port-by-friendly-name/
''
Imports System.Management

Public Class COMPortInfo
    Public Class COMPortInfo

        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String

        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String

        Public Sub New()
        End Sub

        Public Shared Function GetCOMPortsInfo() As List(Of COMPortInfo)

            Dim comPortInfoList As New List(Of COMPortInfo)()
            Dim options As ConnectionOptions = ProcessConnection.ProcessConnectionOptions()
            Dim connectionScope As ManagementScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, "\root\CIMV2")
            Dim objectQuery As New ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0")
            Dim comPortSearcher As New ManagementObjectSearcher(connectionScope, objectQuery)

            Using comPortSearcher
                Dim caption As String = Nothing
                For Each obj As ManagementObject In comPortSearcher.[Get]()
                    If obj IsNot Nothing Then
                        Dim captionObj As Object = obj("Caption")
                        If captionObj IsNot Nothing Then
                            caption = captionObj.ToString()
                            If (caption.Contains("(COM")) Then
                                Dim comPortInfo As New COMPortInfo()
                                If caption.Contains("(COM") Then
                                    comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", String.Empty).Replace(")", String.Empty)
                                End If
                                comPortInfo.Description = caption
                                comPortInfoList.Add(comPortInfo)
                            End If
                        End If
                    End If
                Next
            End Using

            Return comPortInfoList
        End Function
    End Class

End Class

Friend Class ProcessConnection

    Public Shared Function ProcessConnectionOptions() As ConnectionOptions
        Dim options As New ConnectionOptions()

        options.Impersonation = ImpersonationLevel.Impersonate
        options.Authentication = AuthenticationLevel.[Default]
        options.EnablePrivileges = True

        Return options
    End Function

    Public Shared Function ConnectionScope(ByVal machineName As String, ByVal options As ConnectionOptions, ByVal path As String) As ManagementScope
        Dim connectScope As New ManagementScope()

        connectScope.Path = New ManagementPath("\\" & machineName & path)
        connectScope.Options = options
        connectScope.Connect()

        Return connectScope
    End Function

End Class
