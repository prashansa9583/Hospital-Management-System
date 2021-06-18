Public Class Form4
    Public selectedPatient As String = Nothing

#Region "Custom Subroutines"
    Private Sub SaveChanges()
        My.Computer.FileSystem.DeleteDirectory(Main.pathPatients & selectedPatient, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Dim pathPatient As String = Main.pathPatients & TextBox8.Text & " - " & TextBox1.Text & "\"
        My.Computer.FileSystem.CreateDirectory(pathPatient)
        Dim interactiveControls() As Control = {TextBox1, TextBox8, ComboBox1, ComboBox2, ComboBox3, ComboBox4, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7}
        Dim sWriter As New System.IO.StreamWriter(pathPatient & "patientInfo.txt")
        For Each ctrl As Control In interactiveControls
            If ctrl Is ComboBox2 Or ctrl Is ComboBox3 Then
                sWriter.Write(ctrl.Text & "/")
            Else
                sWriter.WriteLine(ctrl.Text)
            End If
        Next
        sWriter.Close()
        PictureBox1.Image.Save(pathPatient & "imgPatient.png", System.Drawing.Imaging.ImageFormat.Png)
        Main.FlowLayoutPanel2.Controls.Clear()
        Main.LoadPatients()
        Form3.Show(Main)
        Form3.selectedPatient = TextBox8.Text & " - " & TextBox1.Text
        Close()
    End Sub
#End Region

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
        For x = 1 To 31
            ComboBox2.Items.Add(x)
            If x < 13 Then
                ComboBox3.Items.Add(x)
            End If
        Next
        For x = 1900 To Date.UtcNow.Year
            ComboBox4.Items.Add(x)
        Next
    End Sub

    Private Sub Form4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SaveChanges()
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                PictureBox1.Load(OpenFileDialog1.FileName)
                If (PictureBox1.Image.Size.Width > PictureBox1.Size.Width) And (PictureBox1.Image.Size.Height > PictureBox1.Size.Height) Then
                    PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                Else
                    PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        SaveChanges()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Close()
    End Sub


End Class