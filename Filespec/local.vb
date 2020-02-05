Imports System.IO
Imports System.Data.SqlClient

Public Class local : Inherits filespec

    Sub New(ByVal di As DirectoryInfo, ByRef sqlHandler As EventHandler)
        MyBase.New(di, sqlHandler)
    End Sub

    Overrides ReadOnly Property FileNames() As List(Of String)
        Get
            Dim ret As New List(Of String)
            With ret
                .Add("local.c01")
            End With
            Return ret
        End Get
    End Property

    Public Overrides ReadOnly Property Columns() As System.Collections.Generic.Dictionary(Of String, Col)
        Get
            Dim ret As New Dictionary(Of String, Col)
            With ret
                .Add("LOCALITY", New Col(1, 6))
                .Add("FILLER_1", New Col(7, 36))
                .Add("FILLER_2", New Col(37, 51))
                .Add("POSTTOWN", New Col(52, 81))
                .Add("DEPENDANT_LOCALITY", New Col(82, 116))
                .Add("D_DEPENDANT_LOCALITY", New Col(117, 151))
            End With
            Return ret
        End Get
    End Property

    Public Overrides Function OnStart() As SqlCommand
        Return New SqlCommand( _
            String.Format( _
             "DELETE FROM ZEMG_LOCALITY " _
            ) _
        )

    End Function

    Public Overrides Function Process(ByVal cols As Dictionary(Of String, String)) As SqlCommand
        Select Case cols("LOCALITY")
            Case "0000"
                Return Nothing

            Case "9999"
                Return Nothing

            Case Else
                Return New SqlCommand( _
                    String.Format( _
                     "INSERT INTO ZEMG_LOCALITY " & _
                     "(LOCALITYKEY, FILLER, FILLER2, POSTTOWN, DEPLOCALITY, DOUBDEPLOCALITY, UPDATEDATE) " & _
                     "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6})", _
                     cols("LOCALITY"), _
                     cols("FILLER_1"), _
                     cols("FILLER_2"), _
                     cols("POSTTOWN"), _
                     cols("DEPENDANT_LOCALITY"), _
                     cols("D_DEPENDANT_LOCALITY"), _
                     DateDiff(DateInterval.Minute, New Date(1988, 1, 1), Now).ToString _
                    ) _
                )                

        End Select

    End Function

End Class
