Public Class Phone
    Implements System.ComponentModel.INotifyPropertyChanged

    Private _serialData As String

    Public Property SerialData() As String
        Get
            Return _serialData
        End Get
        Set(value As String)
            If value <> _serialData Then
                _serialData = value
                RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("SerialData"))
            End If
        End Set
    End Property

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
End Class
