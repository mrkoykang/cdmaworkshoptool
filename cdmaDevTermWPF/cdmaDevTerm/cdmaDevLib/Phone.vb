''Copyright 2012 Dillon Graham
''GPL v3 
Imports System.Text
Imports System.Collections.ObjectModel

Public Class Phone
    Implements System.ComponentModel.INotifyPropertyChanged

    Private _Msg As String ''window messages
    Private _LogData As String ''byte log
    Private _Mdn As String
    Private _Min As String
    Private _Spc As String
    Private _Sid As String
    Private _Nid As String
    Private _NumMipProfiles As String
    Private _EnabledMipProfile As String
    Private _RegId As String
    Private _Meid As String
    Private _Esn As String
    Private _UserLock As String
    Private _SpcReadType As cdmaTerm.SpcReadType
    Private _ModeChangeType As Qcdm.Mode
    Private _NamLock As Boolean
    Private _OperationCount As Integer = 0
    'Private _NvData As New ObservableCollection(Of Nv)
    Private _Qcmip As Qcdm.Qcmip
    Private _AvailableComPorts As New List(Of COMPortInfo.COMPortInfo)
    Private _ComPortName As String
    Private _SixteenDigitSP As String
    Private _NvItems As Dictionary(Of NvItems.NvItems, Nv)
    Private _SpSixteenDigit As New Dictionary(Of String, String)
    Private _TermCommand As String
    Private _Username As String
    Private _Password As String
    Private _PrlFilename As String



    Public Sub New()

        Me.LogData = ""
        Me.Mdn = ""
        Me.Min = ""
        Me.Spc = ""
        Me.Sid = ""
        Me.Nid = ""
        Me.RegId = ""
        Me.Meid = ""
        Me.Esn = ""
        Me.UserLock = ""
        Me.NamLock = False
        Me._OperationCount = 0
        Me.ComPortName = ""
        Me.SixteenDigitSP = ""
        Me.NvItems = New Dictionary(Of NvItems.NvItems, Nv)
        Me.TermCommand = ""
        Me.Username = ""
        Me.Password = ""
        Me.PrlFilename = ""
        Me.NumMipProfiles = ""
        Me.EnabledMipProfile = ""
        '' _NvItems = New Dictionary(Of NvItems.NVItems, Nv)
    End Sub
    Public Sub clearViewModel()
        Me.Mdn = ""
        Me.Min = ""
        Me.Spc = ""
        Me.Sid = ""
        Me.Nid = ""
        Me.RegId = ""
        Me.Meid = ""
        Me.Esn = ""
        Me.UserLock = ""
        Me.NamLock = False
        Me._OperationCount = 0
        Me.SixteenDigitSP = ""
        Me.NvItems = New Dictionary(Of NvItems.NvItems, Nv)
        Me.TermCommand = ""
        Me.Username = ""
        Me.Password = ""
        Me.NumMipProfiles = ""
        Me.EnabledMipProfile = ""
        ''TODO: not clearing raw min
    End Sub
    Public Property SpSixteenDigit() As Dictionary(Of String, String)
        Get
            Return _SpSixteenDigit
        End Get
        Set(value As Dictionary(Of String, String))
            _SpSixteenDigit = value
            RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("SpSixteenDigit"))
        End Set
    End Property

    Public Property NvItems() As Dictionary(Of NvItems.NvItems, Nv)
        Get
            Return _NvItems
        End Get
        Set(value As Dictionary(Of NvItems.NvItems, Nv))
            _NvItems = value
            RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("NvItems"))
        End Set
    End Property

    Public Property Msg() As String
        Get
            Return _Msg
        End Get
        Set(value As String)
            If value <> _Msg Then
                If value = "" Then ''todo:wtf
                    _Msg = ""
                    _OperationCount += 1
                    RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Msg"))
                End If
                _OperationCount += 1
                _Msg = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Msg"))
            End If
        End Set
    End Property

    Public Property LogData() As String
        Get
            Return _LogData
        End Get
        Set(value As String)
            If value <> _LogData Then
                If value = "" Then ''todo:wtf
                    _LogData = ""
                    _OperationCount += 1
                    RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("LogData"))
                End If
                _OperationCount += 1
                _LogData = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("LogData"))
            End If
        End Set
    End Property

    Public Property Mdn() As String
        Get
            Return _Mdn
        End Get
        Set(value As String)
            If value <> _Mdn Then
                _Mdn = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Mdn"))

                'If (value <> "") Then
                '    Dim mdnWriteData As New List(Of Byte)
                '    mdnWriteData.Add(&H0)
                '    mdnWriteData.AddRange(ASCIIEncoding.ASCII.GetBytes(Mdn))
                '    cdmaTerm.WriteSingleNv(NvItems.NVItems.NV_DIR_NUMBER_I, mdnWriteData.ToArray)
                'End If

            End If
        End Set
    End Property
    Public Property Min() As String
        Get
            Return _Min
        End Get
        Set(value As String)
            If value <> _Min Then
                _Min = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Min"))
            End If
        End Set
    End Property
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(value As String)
            If value <> _Username Then
                _Username = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Username"))
            End If
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(value As String)
            If value <> _Password Then
                _Password = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Password"))
            End If
        End Set
    End Property
    Public Property PrlFilename() As String
        Get
            Return _PrlFilename
        End Get
        Set(value As String)
            If value <> _PrlFilename Then
                _PrlFilename = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("PrlFilename"))
            End If
        End Set
    End Property
    Public Property TermCommand() As String
        Get
            Return _TermCommand
        End Get
        Set(value As String)
            If value <> _TermCommand Then
                _TermCommand = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("TermCommand"))
            End If
        End Set
    End Property
    Public Property Spc() As String
        Get
            Return _Spc
        End Get
        Set(value As String)
            If value <> _Spc Then
                _Spc = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Spc"))
            End If
        End Set
    End Property
    Public Property Sid() As String
        Get
            Return _Sid
        End Get
        Set(value As String)
            If value <> _Sid Then
                _Sid = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Sid"))
            End If
        End Set
    End Property

    Public Property NumMipProfiles() As String
        Get
            Return _NumMipProfiles
        End Get
        Set(value As String)
            If value <> _NumMipProfiles Then
                _NumMipProfiles = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("NumMipProfiles"))
            End If
        End Set
    End Property

    Public Property EnabledMipProfile() As String
        Get
            Return _EnabledMipProfile
        End Get
        Set(value As String)
            If value <> _EnabledMipProfile Then
                _EnabledMipProfile = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("EnabledMipProfile"))
            End If
        End Set
    End Property
    Public Property Nid() As String
        Get
            Return _Nid
        End Get
        Set(value As String)
            If value <> _Nid Then
                _Nid = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Nid"))
            End If
        End Set
    End Property


    Public Property RegId() As String
        Get
            Return _RegId
        End Get
        Set(value As String)
            If value <> _RegId Then
                _RegId = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("RegId"))
            End If
        End Set
    End Property

    Public Property SixteenDigitSP() As String
        Get
            Return _SixteenDigitSP
        End Get
        Set(value As String)
            If value <> _SixteenDigitSP Then
                _SixteenDigitSP = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("SixteenDigitSP"))
            End If
        End Set
    End Property

    Public Property Esn() As String
        Get
            Return _Esn
        End Get
        Set(value As String)
            If value <> _Esn Then
                _Esn = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Esn"))
            End If
        End Set
    End Property

    Public Property Meid() As String
        Get
            Return _Meid
        End Get
        Set(value As String)
            If value <> _Meid Then
                _Meid = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Meid"))
            End If
        End Set
    End Property

    Public Property UserLock() As String
        Get
            Return _UserLock
        End Get
        Set(value As String)
            If value <> _UserLock Then
                _UserLock = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("UserLock"))
            End If
        End Set
    End Property

    Public Property NamLock() As Boolean
        Get
            Return _NamLock
        End Get
        Set(value As Boolean)
            If value <> _NamLock Then
                _NamLock = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("NamLock"))
            End If
        End Set
    End Property

    Public Property ComPortName() As String
        Get
            Return _ComPortName
        End Get
        Set(value As String)
            If value <> _ComPortName Then
                _ComPortName = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("ComPortName"))
            End If
        End Set
    End Property

    Public Property SpcReadType() As cdmaTerm.SpcReadType
        Get
            Return _SpcReadType
        End Get
        Set(value As cdmaTerm.SpcReadType)
            If value <> _SpcReadType Then
                _SpcReadType = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("SpcReadType"))
            End If
        End Set
    End Property
    Public Property ModeChangeType() As Qcdm.Mode
        Get
            Return _ModeChangeType
        End Get
        Set(value As Qcdm.Mode)
            If value <> _ModeChangeType Then
                _ModeChangeType = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("ModeChangeType"))
            End If
        End Set
    End Property
    Public ReadOnly Property SpcReadTypeValues() As IEnumerable(Of cdmaTerm.SpcReadType)
        Get
            Return [Enum].GetValues(GetType(cdmaTerm.SpcReadType)).Cast(Of cdmaTerm.SpcReadType)()
        End Get
    End Property
    Public ReadOnly Property ModeChangeTypeValues() As IEnumerable(Of Qcdm.Mode)
        Get
            Return [Enum].GetValues(GetType(Qcdm.Mode)).Cast(Of Qcdm.Mode)()
        End Get
    End Property

    Public Property AvailableComPorts() As List(Of COMPortInfo.COMPortInfo)
        Get
            Return _AvailableComPorts
        End Get
        Set(value As List(Of COMPortInfo.COMPortInfo))
            _AvailableComPorts = value
            RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("AvailableComPorts"))
        End Set
    End Property

    Public Property Qcmip() As Qcdm.Qcmip
        Get
            Return _Qcmip
        End Get
        Set(value As Qcdm.Qcmip)
            If value <> _Qcmip Then
                _Qcmip = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Qcmip"))
            End If
        End Set
    End Property
    Public ReadOnly Property QcmipTypeValues() As IEnumerable(Of Qcdm.Qcmip)
        Get
            Return [Enum].GetValues(GetType(Qcdm.Qcmip)).Cast(Of Qcdm.Qcmip)()
        End Get
    End Property

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

End Class
