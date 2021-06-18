Public Class Form3
    Public selectedPatient As String = Nothing

#Region "Custom Subroutines"
    Private Sub DeletePatient()
        Dim msgReply = MessageBox.Show("Are you sure you wish to remove this patient?", "Hospital Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If msgReply = Windows.Forms.DialogResult.Yes Then
            Close()
            My.Computer.FileSystem.DeleteDirectory(Main.pathPatients & selectedPatient, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Main.FlowLayoutPanel2.Controls.Clear()
            Main.LoadPatients()
        End If
    End Sub
#End Region

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
    End Sub

    Private Sub Form3_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lblControls() As Label = {lblName, lblID, lblGender, lblBirthDate, lblCountry, lblCity, lblPostalCode, lblContactNumber, lblAddress, lblEmailAddress}
        Dim sReader As New System.IO.StreamReader(Main.pathStorage & "Patients\" & selectedPatient & "\patientInfo.txt")
        Dim strValues(11) As String
        Dim counter As Integer = 0
        While sReader.Peek <> -1
            strValues(counter) = sReader.ReadLine
            counter += 1
        End While
        counter = 0
        For Each lbl As Label In lblControls
            lbl.Text = strValues(counter)
            counter += 1
        Next
        sReader.Close()

        PictureBox1.Load(Main.pathStorage & "Patients\" & selectedPatient & "\imgPatient.png")
        If (PictureBox1.Image.Size.Width > PictureBox1.Size.Width) And (PictureBox1.Image.Size.Height > PictureBox1.Size.Height) Then
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
            Form4.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        Else
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
            Form4.PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub

    Private Sub Form3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Delete Then
            DeletePatient()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Edit patient
        Dim counter As Integer = 0 : Dim tempMemory As String = Nothing : Dim slashesFound As Integer = 0 : Dim bDay As String = Nothing : Dim bMonth As String = Nothing : Dim bYear As String = Nothing
        For x = 0 To lblBirthDate.Text.Length - 1
            If lblBirthDate.Text.Substring(x, 1) = "/" Then
                If slashesFound = 0 Then
                    bDay = tempMemory
                    slashesFound += 1
                ElseIf slashesFound = 1 Then
                    bMonth = tempMemory
                    bYear = lblBirthDate.Text.Substring(x + 1, 4)
                End If
                tempMemory = ""
            Else
                tempMemory &= lblBirthDate.Text.Substring(x, 1)
            End If
        Next
        Dim allControls() As Control = {Form4.TextBox1, Form4.TextBox8, Form4.ComboBox1, Form4.ComboBox2, Form4.ComboBox3, Form4.ComboBox4, Form4.TextBox2, Form4.TextBox3, Form4.TextBox4, Form4.TextBox5, Form4.TextBox6, Form4.TextBox7}
        Dim allData() As String = {lblName.Text, lblID.Text, lblGender.Text, bDay, bMonth, bYear, lblCountry.Text, lblCity.Text, lblPostalCode.Text, lblContactNumber.Text, lblAddress.Text, lblEmailAddress.Text}
        For Each ctrl As Control In allControls
            ctrl.Text = allData(counter) : counter += 1
        Next
        Form4.PictureBox1.Image = PictureBox1.Image.Clone
        Form4.selectedPatient = lblID.Text & " - " & lblName.Text
        Form4.TextBox1.SelectionStart = Form4.TextBox1.TextLength : Form4.Show(Main) : Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DeletePatient()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Bill.Show()
        Me.Hide()
    End Sub
End Class