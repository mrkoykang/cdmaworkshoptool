Public Class Phone
    Implements System.ComponentModel.INotifyPropertyChanged

    Private _SerialData As String
    Private _Mdn As String
    Private _Min As String
    Private _Spc As String
    Private _Sid As String
    Private _Nid As String
    Private _RegId As String
    Private _Meid As String
    Private _Esn As String
    Private _UserLock As String
    Private _SpcReadType As cdmaTerm.SpcReadType
    Private _NamLock As Boolean

    Public Property SerialData() As String
        Get
            Return _SerialData
        End Get
        Set(value As String)
            If value <> _SerialData Then
                _SerialData = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("SerialData"))
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


    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged



End Class
