Imports System.Threading
Public Class Logdemo
    Public log_name As String = "L2通讯和采集"
    Public Sub L2log(ByVal msg As String)
        Try
            Dim datestr, riqi, nian As String
            datestr = Format(Now(), "yyyy/MM/dd HH:mm:ss")
            riqi = Format(Now(), "yyyyMM")
            nian = Format(Now(), "yyyy")
            'Panel1.Text = datestr & msg
            If Not My.Computer.FileSystem.DirectoryExists("D:\TGRX_Log") Then
                My.Computer.FileSystem.CreateDirectory("D:\TGRX_Log")
            End If
            If Not My.Computer.FileSystem.DirectoryExists("D:\TGRX_Log\" & log_name) Then
                My.Computer.FileSystem.CreateDirectory("D:\TGRX_Log\" & log_name)
            End If
            If Not My.Computer.FileSystem.DirectoryExists("D:\TGRX_Log\" & log_name & "\L2日志") Then
                My.Computer.FileSystem.CreateDirectory("D:\TGRX_Log\" & log_name & "\L2日志")
            End If
            If Not My.Computer.FileSystem.DirectoryExists("D:\TGRX_Log\" & log_name & "\L2日志\" & nian) Then
                My.Computer.FileSystem.CreateDirectory("D:\TGRX_Log\" & log_name & "\L2日志\" & nian)
            End If
            My.Computer.FileSystem.WriteAllText("D:\TGRX_Log\" & log_name & "\L2日志\" & nian & "\" & riqi & ".txt", datestr & msg & vbCrLf, My.Computer.FileSystem.FileExists("D:\TGRX_Log\" & log_name & "\L2日志\" & nian & "\" & riqi & ".txt"))
            Thread.Sleep(50)
        Catch ex As Exception
            'Txt(ex.Message)
        End Try

    End Sub
End Class
