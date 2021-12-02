Imports FlyTcpFramework
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports MySql.Data.MySqlClient
Public Class Form1
#Region "建立全局变量"
    Public L2Server As TcpSvr
    Delegate Sub ReceiveDelegate(Sender As Object, e As NetEventArgs)
    Delegate Sub Job_demo(jobdemo As String)

    Delegate Sub Job_L2(renwu As String, msg As String)
    Public log_name As String = "L2通讯和采集"
    Public mySql_Connection As MySqlConnection
    Dim mysql_net As New Mysql_Connect
    Dim new_log As New Logdemo
#End Region
#Region "建立连接"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        L2Server = New TcpSvr(IPAddress.Parse("127.0.0.1"), 8001, 10, New Coder(Coder.EncodingMothord.ASCII), AppDomain.CurrentDomain.BaseDirectory)
        AddHandler L2Server.RecvData, AddressOf L2RecvData
        AddHandler L2Server.ClientConn, AddressOf L2ClientConnect
        'AddHandler L2Server.ClientClose, AddressOf L2ClientClosed
    End Sub
#End Region
#Region "接收数据"
    Private Sub L2RecvData(sender As Object, e As NetEventArgs)
        If InvokeRequired Then
            Invoke(New ReceiveDelegate(AddressOf L2RecvData), New Object() {sender, e})
        Else
            Dim info As String = e.Client.Datagram   '收到的数据
            Dim ip As String = CType(e.Client.ClientSocket.RemoteEndPoint, IPEndPoint).Address.ToString()
            If ip = "127.0.0.1" Then
                Invoke(New Job_L2(AddressOf l2_logname), "接收L2数据：", "  本地服务器,收到L2客户端数据" & ip & ":" & info)
                BeginInvoke(New EventHandler(AddressOf L2ServerAddInfo), info)   'Invoke保证线程安全
            End If
            If ip = "10.33.16.189" Then
                Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,收到L2客户端数据" & ip & ":" & info)
                If Mid(info, 1, 6) = "2BL101" Then
                    Server_Response("2BL101")
                End If
            End If
        End If

    End Sub
#End Region
    Public Sub Server_Response(msg As String)
        Try
            'L2Server.SendText("hello")
            'If IsRun Then
            Dim clientIP As String = ""
            For Each session As Session In L2Server.SessionTable.Values
                '        clientIP = CType(session.ClientSocket.RemoteEndPoint, IPEndPoint).Address.ToString()
                '        If clientIP = "10.33.16.187" Or clientIP = "10.33.16.189" Then
                '            Dim 日期 As String = Format(Date.Now, "yyyyMMdd")
                '            Dim 时间 As String = Format(Date.Now, "hhmmss")
                '            Dim 发送设备 As String = "L1"
                '            Dim 接收设备 As String = "2B"
                '            Dim 功能码 As String = "A"
                '            Dim 控制域 As String = ""
                L2Server.SendText(session, "hell0")
                Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,向L2发送委托应答电文：")
                Exit For
                '        Else
                'Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,没有连接的客户端,请先建立服务，等待中控连接后在发送")
                ' End If
            Next
            'Else
            '    Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,Server发送数据报错,没有连接的客户端。")
            'End If
        Catch ex As Exception
            Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,Server发送数据报错,没有连接的客户端。" & ex.Message)
        End Try
    End Sub

    Sub L2ServerAddInfo(ByVal sender As String, ByVal e As EventArgs)
        'mySql_Connection = mysql_net.Mysql_command

        Try
            Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,收到非规约内报文，丢弃！！！！！！！！！！！！！！！！！！！")
            '    If sender <> "" Then
            '        If Mid(sender, 1, 6) = "999999" Then
            '            '心跳
            '        End If
            '        '委托电文
            '        If Mid(sender, 1, 6) = "2BL101" Then
            '            'Server_发送应答("2BL101")
            '            'Dim SQLiteConn As New SQLiteConnection
            '            'Dim SQLiteCmd As New SQLiteCommand
            '            'SQLiteConn.ConnectionString = L2_SQLiteStr
            '            'SQLiteConn.Open()
            '            'SQLiteCmd.Connection = SQLiteConn
            '            'SQLiteCmd.CommandText = "Insert Into Task (钢卷号,试样组号,内检编号,取样位置,实验次数,轧制宽度,厚度,钢种,拉伸规格,拉伸方向,拉伸数量,洛氏硬度,金相,冲击,扩孔,弯曲规格,弯曲方向,加工完成,委托时间)
            '            '         Values('" & Trim(Mid(sender, 26, 15)) & "','" & Trim(Mid(sender, 41, 15)) & "','" & Trim(Mid(sender, 56, 10)) & "','" & Trim(Mid(sender, 66, 1)) & "','" & Trim(Mid(sender, 67, 1)) & "', '" & Trim(Mid(sender, 68, 7)) & "',
            '            '                '" & Trim(Mid(sender, 75, 7)) & "','" & Trim(Mid(sender, 82, 20)) & "','" & Trim(Mid(sender, 102, 6)) & "','" & Trim(Mid(sender, 108, 2)) & "','" & Trim(Mid(sender, 110, 2)) & "', '" & Trim(Mid(sender, 112, 2)) & "',
            '            '                '" & Trim(Mid(sender, 114, 2)) & "','" & Trim(Mid(sender, 116, 2)) & "','" & Trim(Mid(sender, 118, 2)) & "','" & Trim(Mid(sender, 120, 4)) & "','" & Trim(Mid(sender, 124, 2)) & "','0','" & Format(Now(), "yyyy/MM/dd HH:mm:ss") & "')"
            '            'SQLiteCmd.ExecuteNonQuery()
            '            'SQLiteConn.Close()
            '            mySql_Connection.Open()

            '        End If
            '        '删除电文
            '        If Mid(sender, 1, 6) = "2BL102" Then
            '            'Server_发送应答("2BL102")
            '            'Dim SQLiteConn As New SQLiteConnection
            '            'Dim SQLiteCmd As New SQLiteCommand
            '            'SQLiteConn.ConnectionString = L2_SQLiteStr
            '            'SQLiteConn.Open()
            '            'SQLiteCmd.Connection = SQLiteConn
            '            'SQLiteCmd.CommandText = "Delete From Task where 钢卷号= '" & Trim(Mid(sender, 26, 15)) & "' and  试样组号 = '" & Trim(Mid(sender, 41, 15)) & "' and 内检编号 = '" & Trim(Mid(sender, 56, 10)) & "'"
            '            'SQLiteCmd.ExecuteNonQuery()
            '            'SQLiteConn.Close()
            '        End If

            '    End If
        Catch ex As Exception
            Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,收到非规约内报文，丢弃！！！！！！！！！！！！！！！！！！！")
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Start_job()
        Label2.Text = ""
    End Sub
    Sub Start_job()
        Try
            If L2Server.IsRun = False Then
                L2Server.Start()
                If L2Server.IsRun Then
                    Invoke(New Job_demo(AddressOf new_log.L2log), "本地服务器：server已上线,允许L2连接")
                    Label2.Text = "服务端已打开，请连接"


                Else
                    Invoke(New Job_demo(AddressOf new_log.L2log), "本地服务器：server下线")
                End If
            End If
        Catch ex As Exception
            Invoke(New Job_L2(AddressOf l2_logname), "本地服务器：", "IP和端口号错误或已占用")
        End Try
    End Sub
    Private Sub PrepareListView(listView As ListView)
        listView.BringToFront()

    End Sub

    Public Sub l2_logname(renwu As String, msg As String)
        Try
            PrepareListView(ListView1)
            ListView1.Items.Insert(0, New ListViewItem(New String() {Format(Now(), "yyyy/MM/dd HH:mm:ss"), renwu, msg}))
            If ListView1.Items.Count > 300 Then
                ListView1.Items.RemoveAt(ListView1.Items.Count - 1)
            End If
            new_log.L2log(msg)
        Catch ex As Exception
            'Txt(ex.Message)
        End Try
    End Sub
    Private Sub L2ClientConnect(sender As Object, e As NetEventArgs)
        If InvokeRequired Then
            Invoke(New ReceiveDelegate(AddressOf L2ClientConnect), New Object() {sender, e})
        Else
            Dim ip As String = CType(e.Client.ClientSocket.RemoteEndPoint, IPEndPoint).Address.ToString()
            If ip = "127.0.0.1" Or ip = "10.33.16.189" Then
                'IsRun = True
                Invoke(New Job_L2(AddressOf l2_logname), "客户端连接" & ip, "  本地服务器,L2客户端连接,IP:" & ip)
                Label2.Text = "客户端连接"
            Else
                L2Server.CloseSession(e.Client)
                Invoke(New Job_demo(AddressOf new_log.L2log), "  本地服务器,非法L2客户端IP连接，强制关闭，IP:" & ip)

            End If
        End If
    End Sub


End Class
