Public Class Col

    Sub New(ByVal CharStart As Integer, ByVal CharEnd As Integer)
        _CharStart = CharStart
        _CharEnd = CharEnd
    End Sub

    Private _CharStart As Integer
    Public Property CharStart() As Integer
        Get
            Return _CharStart
        End Get
        Set(ByVal value As Integer)
            _CharStart = value
        End Set
    End Property

    Private _CharEnd As Integer
    Public Property CharEnd() As Integer
        Get
            Return _CharEnd
        End Get
        Set(ByVal value As Integer)
            _CharEnd = value
        End Set
    End Property

    Public ReadOnly Property Length() As Integer
        Get
            Return 1 + (_CharEnd - _CharStart)
        End Get
    End Property

End Class
