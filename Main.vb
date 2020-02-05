Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Module Main

    Private l As Integer = 0
    Private cn As New SqlConnection("Server=localhost\MEDATECH;Trusted_Connection=True;")
    Private dir As New DirectoryInfo("F:\rawaddress\PAF MAIN FILE\")
    Private db As String = "paf"

    Sub Main()

        Console.CursorVisible = False        
        Dim F As New List(Of filespec)
        Try
            With cn
                .Open()
                .ChangeDatabase(db)
            End With            

            With F
                .Add(New thdesc(dir, AddressOf sqlHandler))
                .Add(New bname(dir, AddressOf sqlHandler))
                .Add(New local(dir, AddressOf sqlHandler))
                .Add(New org(dir, AddressOf sqlHandler))
                .Add(New subbname(dir, AddressOf sqlHandler))
                .Add(New thfare(dir, AddressOf sqlHandler))
                .Add(New fpmainfl(dir, AddressOf sqlHandler))
            End With

            For Each Fs As filespec In F
                Fs.LoadFiles()
                l += 1
            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)

        Finally
            cn.Close()
            Console.CursorVisible = True

        End Try

    End Sub

    Private Sub sqlHandler(ByVal sender As Object, ByVal e As sqlEventArgs)
        Static Fn As String

        If Not String.Compare(Fn, e.File.FullName) = 0 Then
            Console.SetCursorPosition(0, l)
            Fn = e.File.FullName
            Console.Write(Fn)

        End If

        Console.SetCursorPosition(Console.WindowWidth - 25, l)
        ProgressBar(CInt(e.Progress / 5))

        With e.SQL
            .Connection = cn
            .CommandTimeout = 50000
            .ExecuteNonQuery()

        End With

    End Sub

    Private Sub ProgressBar(ByVal progress As Integer)
        Dim pb As New StringBuilder
        Static LastProgress As Integer = -1
        If Not progress = LastProgress Then
            LastProgress = progress
            With pb
                .Append("[")
                Dim f As Boolean = False
                For i As Integer = 0 To 19
                    If progress >= i And progress < i + 1 Then
                        .Append(">")
                        f = True
                    Else
                        If Not f Then
                            .Append("=")
                        Else
                            .Append("-")
                        End If

                    End If

                Next
                .Append("]")
            End With
            Console.Write(pb.ToString)

        End If

    End Sub


End Module
