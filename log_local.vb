Imports System.Threading
Public Class log_local
    Delegate Sub w1(renwu As Integer)
    Public Thread_name As Thread
    Dim i As Integer = 0
    Private Sub log_local_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'log_name()
        Thread_name = New Thread(AddressOf log_name)
        'Thread_name.Abort()
        Thread_name.Start()







        'ListView1.Items.Insert(0, New ListViewItem(New String() {, ,}))
        ' ListView1 = Form1.ListView1.Items.Count
    End Sub
    Sub log_name()
        'Label1.Text = "标题"

        Try
            While True


                'ListView1.Items.Clear()


                Invoke(New w1(AddressOf set_text), i)
                'Label1.Text = i
                'While i < Form1.ListView1.Items.Count
                ' ListView1.Items.Insert(0, New ListViewItem(New String() {Form1.ListView1.Items(i).SubItems(0).Text, Form1.ListView1.Items(i).SubItems(1).Text, Form1.ListView1.Items(i).SubItems(2).Text}))
                i = i + 1
                'End While
                Thread.Sleep(400)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub set_text(ByVal a As Integer)
        'ListView1.Items.Clear()
        Label1.Text = Form1.Label3.Text
        'Dim i As Integer = Form1.ListView1.Items.Count - 1
        'While i >= 0
        '    ListView1.Items.Insert(0, New ListViewItem(New String() {Form1.ListView1.Items(i).SubItems(0).Text, Form1.ListView1.Items(i).SubItems(1).Text, Form1.ListView1.Items(i).SubItems(2).Text}))
        '    i = i - 1
        'End While

    End Sub

    Private Sub log_local_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Thread_name.Abort()
    End Sub
End Class