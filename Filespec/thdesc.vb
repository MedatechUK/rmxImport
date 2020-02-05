Imports System.IO
Imports System.Data.SqlClient

Public Class thdesc : Inherits filespec

    Sub New(ByVal di As DirectoryInfo, ByRef sqlHandler As EventHandler)
        MyBase.New(di, sqlHandler)
    End Sub

    Overrides ReadOnly Property FileNames() As List(Of String)
        Get
            Dim ret As New List(Of String)
            With ret
                .Add("thdesc.c01")
            End With
            Return ret
        End Get
    End Property

    Public Overrides ReadOnly Property Columns() As System.Collections.Generic.Dictionary(Of String, Col)
        Get
            Dim ret As New Dictionary(Of String, Col)
            With ret
                .Add("DESCRIPTOR", New Col(1, 4))
                .Add("T_DESCRIPTOR", New Col(5, 24))
                .Add("ABBREVIATION", New Col(25, 30))

            End With
            Return ret
        End Get
    End Property

    Public Overrides Function OnStart() As SqlCommand
        Return New SqlCommand( _
            String.Format( _
             "DELETE FROM ZEMG_THIDESCRI " _
            ) _
        )


    End Function

    Public Overrides Function Process(ByVal cols As Dictionary(Of String, String)) As SqlCommand
        Select Case cols("DESCRIPTOR")
            Case "0000"
                Return Nothing

            Case "9999"
                Return Nothing

            Case Else
                Return New SqlCommand( _
                    String.Format( _
                     "INSERT INTO ZEMG_THIDESCRI " & _
                     "(THRFAREDESKEY, THRFAREDES, APPROVEDABBREV, UPDATEDATE) " & _
                     "VALUES ({0}, '{1}', '{2}', {3})", _
                     cols("DESCRIPTOR"), _
                     cols("T_DESCRIPTOR"), _
                     cols("ABBREVIATION"), _
                     DateDiff(DateInterval.Minute, New Date(1988, 1, 1), Now).ToString _
                    ) _
                )
                
        End Select

    End Function

End Class
