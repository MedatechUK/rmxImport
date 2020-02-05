Imports System.IO
Imports System.Data.SqlClient

Public MustInherit Class filespec

    Private _dir As DirectoryInfo
    Private _sqlHandler As EventHandler
    Private _filesSize As Long

    Public MustOverride ReadOnly Property FileNames() As List(Of String)
    Public MustOverride ReadOnly Property Columns() As Dictionary(Of String, Col)
    Public MustOverride Function OnStart() As SqlCommand
    Public MustOverride Function Process(ByVal cols As Dictionary(Of String, String)) As SqlCommand

    Sub New(ByVal di As DirectoryInfo, ByRef sqlHandler As EventHandler)
        _dir = di
        _sqlHandler = sqlHandler
        _filesSize = 0

        For Each file As String In FileNames
            Dim fi As New FileInfo(IO.Path.Combine(_dir.FullName, file))
            If Not fi.Exists Then
                Throw New Exception(String.Format("File {0} is missing.", fi.FullName))
            Else
                _filesSize += fi.Length
            End If
        Next

    End Sub

    Sub LoadFiles()

        Dim p As Long = 0
        Dim sql As SqlCommand

        sql = OnStart()
        If Not sql Is Nothing Then
            _sqlHandler.Invoke( _
                Me, _
                New sqlEventArgs( _
                    New FileInfo(IO.Path.Combine(_dir.FullName, FileNames(0))), _
                    sql, _
                    100 * (p / _filesSize) _
                ) _
            )
        End If

        For Each file As String In FileNames

            Dim fi As New FileInfo(IO.Path.Combine(_dir.FullName, file))
            Using sr As New StreamReader(fi.FullName)

                While Not sr.EndOfStream

                    Dim cols As New Dictionary(Of String, String)
                    Dim l As String = sr.ReadLine

                    Try
                        p += (l.Length + 2)
                        For Each k As String In Columns.Keys
                            With Columns(k)
                                cols.Add(k, Trim(l.Substring(.CharStart - 1, .Length)).Replace("'", "`"))
                            End With
                        Next

                        sql = Process(cols)
                        If Not sql Is Nothing Then
                            _sqlHandler.Invoke( _
                                Me, _
                                New sqlEventArgs( _
                                    fi, _
                                    sql, _
                                    100 * (p / _filesSize) _
                                ) _
                            )
                        End If

                    Catch : End Try

                End While

            End Using

        Next

    End Sub

End Class
