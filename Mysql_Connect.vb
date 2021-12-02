Imports MySql.Data.MySqlClient
Imports System.ComponentModel
Public Class Mysql_Connect
    Dim mySqlConnection As MySqlConnection
    Public Function Mysql_command() As MySqlConnection

        Dim mysqlCSB As MySqlConnectionStringBuilder = New MySqlConnectionStringBuilder()
        mysqlCSB.Database = "vbtestdemo"  ' 设置连接的数据库名
        mysqlCSB.Server = "127.0.0.1" '设置连接数据库的IP地址
        mysqlCSB.Port = 3306         ' MySql端口号
        mysqlCSB.UserID = "root" '设置登录数据库的账号
        mysqlCSB.Password = "root"

        mySqlConnection = New MySqlConnection(mysqlCSB.ToString)
        Return mySqlConnection
    End Function


End Class
