Public Class Bill

    Dim wardcharge As Double
    Dim service As Double


    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("General Ward")
        ComboBox1.Items.Add("Private Ward")
        ComboBox1.Items.Add("ICU")
        ComboBox1.Items.Add("Bed Not Required")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        If ComboBox1.SelectedItem = "General Ward" Then
            Label4.Text = 2000
        ElseIf ComboBox1.SelectedItem = "Private Ward" Then
            Label4.Text = 5000
        ElseIf ComboBox1.SelectedItem = "ICU" Then
            Label4.Text = 8000
        Else
            Label4.Text = 0
        End If
    End Sub



    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        If ComboBox1.SelectedItem = "General Ward" Then
            Label11.Text = 2500
        ElseIf ComboBox1.SelectedItem = "Private Ward" Then
            Label11.Text = 3500
        ElseIf ComboBox1.SelectedItem = "ICU" Then
            Label11.Text = 4500
        Else
            Label11.Text = 0
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        wardcharge = Label4.Text * TextBox1.Text
        TextBox2.Text = wardcharge

        'service = Label11.Text

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox3.Text = "Rs. " & Format(4000 + wardcharge + Label11.Text)
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Close()
    End Sub

    Dim bitmap As Bitmap
    Private Sub iprint()
        Dim Height As Integer = 596
        bitmap = New Bitmap(620, Height)
        Me.DrawToBitmap(bitmap, New Rectangle(0, 0, 620, Height))
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        iprint()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(bitmap, 0, 0)
    End Sub
End Class
