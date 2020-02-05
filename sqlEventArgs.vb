Imports System.Data.SqlClient
Imports System.IO

Public Class sqlEventArgs : Inherits System.EventArgs

    Sub New(ByVal File As FileInfo, ByVal sql As SqlCommand, ByVal Progress As Integer)
        _file = File
        _sql = sql
        _Progress = Progress

    End Sub

    Private _file As FileInfo
    Public ReadOnly Property File() As FileInfo
        Get
            Return _file
        End Get
    End Property

    Private _sql As SqlCommand
    Public ReadOnly Property SQL() As SqlCommand
        Get
            Return _sql
        End Get
    End Property

    Private _Progress As Integer
    Public ReadOnly Property Progress() As Integer
        Get
            Return _Progress
        End Get
    End Property

End Class
