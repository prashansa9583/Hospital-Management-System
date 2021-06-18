Public Class Form9
    Public selectedAppointment As String = Nothing

#Region "Custom Subroutines"
    Private Sub DeleteAppointment()
        Dim msgReply = MessageBox.Show("Are you sure you wish to remove this appointment?", "Hospital Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If msgReply = Windows.Forms.DialogResult.Yes Then
            Dim pathToRemove As String = Main.pathAppointments & lblPatientName.Text & " - " & lblCaseType.Text
            My.Computer.FileSystem.DeleteDirectory(pathToRemove, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Main.FlowLayoutPanel3.Controls.Clear()
            Main.LoadAppointments()
            Close()
        End If
    End Sub
#End Region

    Private Sub Form9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
    End Sub

    Private Sub Form9_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Load data
        Dim sReader As New System.IO.StreamReader(Main.pathAppointments & selectedAppointment & "\appointmentData.txt")
        Dim lbData As New ListBox
        While sReader.Peek <> -1
            lbData.Items.Add(sReader.ReadLine)
        End While
        sReader.Close()

        'Visualise data
        lblPatientName.Text = lbData.Items(1)
        lblCaseType.Text = lbData.Items(0)
        lblDate.Text = lbData.Items(2)
        lblTime.Text = lbData.Items(3) & ":" & lbData.Items(4)
        lblAppointmentFor.Text = lbData.Items(5)
        lblLocation.Text = lbData.Items(6)
    End Sub

    Private Sub Form9_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Delete Then
            DeleteAppointment()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dataControls() As Control = {Form5.TextBox1, Form5.lblPatientName, Form5.DateTimePicker1, Form5.ComboBox1, Form5.ComboBox2, Form5.TextBox2, Form5.TextBox3}
        Dim sReader As New System.IO.StreamReader(Main.pathAppointments & selectedAppointment & "\appointmentData.txt")
        For Each ctrl As Control In dataControls
            ctrl.Text = sReader.ReadLine
        Next
        sReader.Close()
        Close()
        If Form5.Visible = False Then Form5.Show(Main)
        Form5.isNewAppointment = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DeleteAppointment()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub


End Class