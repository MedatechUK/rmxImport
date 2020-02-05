Imports System.IO
Imports System.Data.SqlClient

Public Class fpmainfl : Inherits filespec

    Sub New(ByVal di As DirectoryInfo, ByRef sqlHandler As EventHandler)
        MyBase.New(di, sqlHandler)
    End Sub

    Overrides ReadOnly Property FileNames() As List(Of String)
        Get
            Dim ret As New List(Of String)
            With ret                
                .Add("fpmainfl.c02")
                .Add("fpmainfl.c03")
                .Add("fpmainfl.c04")
                .Add("fpmainfl.c05")
                .Add("fpmainfl.c06")
            End With
            Return ret
        End Get
    End Property

    Public Overrides ReadOnly Property Columns() As System.Collections.Generic.Dictionary(Of String, Col)
        Get
            Dim ret As New Dictionary(Of String, Col)
            With ret
                .Add("OUTCODE", New Col(1, 4))
                .Add("INCODE", New Col(5, 7))
                .Add("ADDRESS", New Col(8, 15))
                .Add("LOCALITY", New Col(16, 21))
                .Add("THOROUGHFARE", New Col(22, 29))
                .Add("DESCRIPTOR", New Col(30, 33))
                .Add("DEP_TFARE", New Col(34, 41))
                .Add("DEP_DESC", New Col(42, 45))
                .Add("NUMBER", New Col(46, 49))
                .Add("BNAME", New Col(50, 57))
                .Add("SNAME", New Col(58, 65))
                .Add("HOUSEHOLDS", New Col(66, 69))
                .Add("ORGANISATION", New Col(70, 77))
                .Add("TYPE", New Col(78, 78))
                .Add("CONCATENATION", New Col(79, 79))
                .Add("SUFFIX", New Col(80, 81))
                .Add("SMALLORG", New Col(82, 82))
                .Add("POBOX", New Col(83, 88))
            End With
            Return ret
        End Get
    End Property

    Public Overrides Function OnStart() As SqlCommand
        Return New SqlCommand( _
            String.Format( _
             "DELETE FROM ZEMG_FPMAINFL " _
            ) _
        )

    End Function

    Public Overrides Function Process(ByVal cols As Dictionary(Of String, String)) As SqlCommand
        Select Case cols("ADDRESS")
            Case "00000000"
                Return Nothing

            Case "99999999"
                Return Nothing

            Case Else
                Return New SqlCommand( _
                    String.Format( _
                        "INSERT INTO ZEMG_FPMAINFL " & _
                        "(OUTCODE, INCODE, ADDRESSKEY, LOCALITYKEY, THRFAREKEY, THRFAREDESKEY " & _
                        ",DEPTHRFAREKEY, DEPTHRFAREDESKEY, BUILDINGNUMBER, BUILDINGNAMEKEY, " & _
                        "SUBBUILDINGNAMEKEY, NUMBERHOUSEHOLDS, ORGKEY, " & _
                        "POSTCODETYPE, CANCATINDICATOR, DELIVERYPOINTSUFFIX, " & _
                        "SMALLUSERORGINDICATO, POBOXNUMBER, UPDATEDATE) " & _
                        "VALUES ('{0}','{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, '{13}', '{14}', '{15}', '{16}', '{17}', {18} )", _
                        cols("OUTCODE"), _
                        cols("INCODE"), _
                        cols("ADDRESS"), _
                        cols("LOCALITY"), _
                        cols("THOROUGHFARE"), _
                        cols("DESCRIPTOR"), _
                        cols("DEP_TFARE"), _
                        cols("DEP_DESC"), _
                        cols("NUMBER"), _
                        cols("BNAME"), _
                        cols("SNAME"), _
                        cols("HOUSEHOLDS"), _
                        cols("ORGANISATION"), _
                        cols("TYPE"), _
                        cols("CONCATENATION"), _
                        cols("SUFFIX"), _
                        cols("SMALLORG"), _
                        cols("POBOX"), _
                        DateDiff(DateInterval.Minute, New Date(1988, 1, 1), Now).ToString _
                    ) _
                )                

        End Select

    End Function

End Class
