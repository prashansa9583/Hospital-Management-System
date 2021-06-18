Public Class Form5
    Public isNewAppointment As Boolean = True

#Region "Custom Subroutines"
    Private Sub SetAppointment()
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or lblPatientName.Text = "unknown" Or String.IsNullOrWhiteSpace(ComboBox1.Text) Or String.IsNullOrWhiteSpace(ComboBox2.Text) Or _
            String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please make sure that all data have been entered.", "Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            'Write appointment to file
            Dim pathAppointment As String = Main.pathAppointments & lblPatientName.Text & " - " & TextBox1.Text
            My.Computer.FileSystem.CreateDirectory(pathAppointment)
            Dim sWriter As New System.IO.StreamWriter(pathAppointment & "\appointmentData.txt")
            Dim dataControls() As Control = {TextBox1, lblPatientName, DateTimePicker1, ComboBox1, ComboBox2, TextBox2, TextBox3}
            For Each ctrl As Control In dataControls
                sWriter.WriteLine(ctrl.Text)
            Next
            sWriter.Close()
            Main.FlowLayoutPanel3.Controls.Clear()
            Main.LoadAppointments()

            If isNewAppointment = False Then
                Form9.selectedAppointment = lblPatientName.Text & " - " & TextBox1.Text
                Form9.Show(Main)
            End If
            Close()
        End If
    End Sub

    Public Sub AddAppointment(ByVal appointmentName As String)
        Dim newAppointment As New Appointment
        newAppointment.Text = appointmentName
        Main.FlowLayoutPanel3.Controls.Add(newAppointment)
    End Sub
#End Region

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
        For x = 0 To 59
            Dim xValue As String = x
            If xValue.Length = 1 Then xValue = "0" & xValue
            If x < 24 Then
                ComboBox1.Items.Add(xValue)
            End If
            ComboBox2.Items.Add(xValue)
        Next
    End Sub

    Private Sub Form5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SetAppointment()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SetAppointment()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If My.Computer.FileSystem.GetDirectories(Main.pathPatients).Count > 0 Then
            Form6.Show(Main)
        Else
            MessageBox.Show("No patients were found.", "Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    
End Class