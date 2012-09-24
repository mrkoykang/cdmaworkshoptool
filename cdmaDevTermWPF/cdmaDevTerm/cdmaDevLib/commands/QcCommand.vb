Public Class QcCommand
    Inherits Command

    Sub New(qc As Qcdm.Cmd)
        MyBase.New(qc, qc.ToString)
    End Sub
End Class
