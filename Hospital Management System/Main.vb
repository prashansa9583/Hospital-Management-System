Public Class Main
#Region "Variables"
    Public pathStorage As String = "C:\ProgramData\Athasoft\Hospital Management System\"
    Public pathPatients As String = pathStorage & "Patients\"
    Public pathDoctors As String = pathStorage & "Doctors\"
    Public pathPharmacists As String = pathStorage & "Pharmacists\"
    Public pathNurses As String = pathStorage & "Nurses\"
    Public pathLaboratorists As String = pathStorage & "Laboratorists\"
    Public pathAccountants As String = pathStorage & "Accountants\"
    Public pathAppointments As String = pathStorage & "Appointments\"
    Public pathAccount As String = pathStorage & "Account\"
    Public pathGeneric As String = pathStorage & "Generic\"
    Public allPaths() As String = {pathAccount, pathAppointments, pathGeneric, pathPatients, pathDoctors, pathPharmacists, pathNurses, pathLaboratorists, pathAccountants}
    Public userName As String = Nothing : Public passWord As String = Nothing
    Public PID As Integer = Nothing : Public DID As Integer = Nothing : Public PHID As Integer = Nothing : Public NID As Integer = Nothing : Public LID As Integer = Nothing : Public AID As Integer = Nothing
    Public allIDs() As String = {"PID.txt", "DID.txt", "PHID.txt", "NID.txt", "LID.txt", "AID.txt"}
    Public selectedCategory As String = "Patients"
#End Region

#Region "Custom Subroutines"
    Private Sub LoadAccount()
        Dim sReader As New System.IO.StreamReader(pathAccount & "username.txt")
        userName = sReader.ReadToEnd
        sReader.Close()
        sReader = New System.IO.StreamReader(pathAccount & "password.txt")
        passWord = sReader.ReadToEnd
        sReader.Close()
    End Sub

    Public Sub LoadAppointments()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathAppointments)
            Form5.AddAppointment(fileFound.Replace(pathAppointments, ""))
        Next
        Label2.Text = "Appointments (" & FlowLayoutPanel3.Controls.Count & ")"
    End Sub

    Public Sub LoadGeneric()
        Dim sReader As New System.IO.StreamReader(pathGeneric & "PID.txt")
        PID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
        sReader = New System.IO.StreamReader(pathGeneric & "DID.txt")
        DID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
        sReader = New System.IO.StreamReader(pathGeneric & "PHID.txt")
        PHID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
        sReader = New System.IO.StreamReader(pathGeneric & "NID.txt")
        NID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
        sReader = New System.IO.StreamReader(pathGeneric & "LID.txt")
        LID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
        sReader = New System.IO.StreamReader(pathGeneric & "AID.txt")
        AID = Integer.Parse(sReader.ReadToEnd)
        sReader.Close()
    End Sub

    Public Sub LoadPatients()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathPatients)
            Dim newPatient As New Patient
            newPatient.Text = fileFound.Replace(pathPatients, "")
            FlowLayoutPanel2.Controls.Add(newPatient)
        Next
        Label1.Text = "Patients (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub LoadDoctors()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathDoctors)
            Dim newDoctor As New Patient
            newDoctor.Text = fileFound.Replace(pathDoctors, "")
            FlowLayoutPanel2.Controls.Add(newDoctor)
        Next
        Label1.Text = "Doctors (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub LoadPharmacists()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathPharmacists)
            Dim newPharmacist As New Patient
            newPharmacist.Text = fileFound.Replace(pathPharmacists, "")
            FlowLayoutPanel2.Controls.Add(newPharmacist)
        Next
        Label1.Text = "Pharmacist (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub LoadNurses()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathNurses)
            Dim newNurse As New Patient
            newNurse.Text = fileFound.Replace(pathNurses, "")
            FlowLayoutPanel2.Controls.Add(newNurse)
        Next
        Label1.Text = "Nurses (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub LoadLaboratorists()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathLaboratorists)
            Dim newLaboratorist As New Patient
            newLaboratorist.Text = fileFound.Replace(pathLaboratorists, "")
            FlowLayoutPanel2.Controls.Add(newLaboratorist)
        Next
        Label1.Text = "Laboratorists (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub LoadAccountants()
        FlowLayoutPanel2.Controls.Clear()
        For Each fileFound As String In My.Computer.FileSystem.GetDirectories(pathAccountants)
            Dim newAccountant As New Patient
            newAccountant.Text = fileFound.Replace(pathAccountants, "")
            FlowLayoutPanel2.Controls.Add(newAccountant)
        Next
        Label1.Text = "Accountants (" & FlowLayoutPanel2.Controls.Count & ")"
    End Sub

    Public Sub UpdateFPanel2()
        If selectedCategory = "Patients" Then
            LoadPatients()
        ElseIf selectedCategory = "Doctors" Then
            LoadDoctors()
        ElseIf selectedCategory = "Pharmacists" Then
            LoadPharmacists()
        ElseIf selectedCategory = "Nurses" Then
            LoadNurses()
        ElseIf selectedCategory = "Laboratorists" Then
            LoadLaboratorists()
        ElseIf selectedCategory = "Accountants" Then
            LoadAccountants()
        End If
    End Sub

    Private Sub SearchPatient()
        For Each btn As Button In FlowLayoutPanel2.Controls
            btn.Visible = False
        Next
        For Each btn As Button In FlowLayoutPanel2.Controls
            If btn.Text.ToLower.Contains(ToolStripTextBox1.Text.ToLower) Then
                btn.Visible = True
            End If
        Next
    End Sub

    Private Sub SearchAppointment()
        For Each btn As Button In FlowLayoutPanel3.Controls
            btn.Visible = False
        Next
        For Each btn As Button In FlowLayoutPanel3.Controls
            If btn.Text.ToLower.Contains(ToolStripTextBox2.Text.ToLower) Then
                btn.Visible = True
            End If
        Next
    End Sub
#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists(pathStorage) Then
            LoadAccount()
            LoadAppointments()
            LoadGeneric()
            LoadPatients()
        Else
            'Create paths
            For Each path As String In allPaths
                My.Computer.FileSystem.CreateDirectory(path)
            Next

            'Create files
            Dim sWriter As New System.IO.StreamWriter(pathAccount & "username.txt")
            sWriter.Write("")
            sWriter.Close()
            sWriter = New System.IO.StreamWriter(pathAccount & "password.txt")
            sWriter.Write("")
            sWriter.Close()
            For Each id As String In allIDs
                sWriter = New System.IO.StreamWriter(pathGeneric & id)
                sWriter.Write("1")
                sWriter.Close()
            Next
        End If
        If String.IsNullOrWhiteSpace(userName) Then
            Reset.Show()
        Else
            Form8.Show()
        End If
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchPatient()
        End If
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.TextChanged
        If ToolStripTextBox1.TextLength = 0 Then
            For Each btn As Button In FlowLayoutPanel2.Controls
                btn.Visible = True
            Next
            ToolStripButton2.Visible = False
            ToolStripSeparator2.Visible = False
        Else
            ToolStripButton2.Visible = True
            ToolStripSeparator2.Visible = True
        End If
    End Sub

    Private Sub ToolStripTextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox2.TextChanged
        If ToolStripTextBox2.TextLength = 0 Then
            For Each btn As Button In FlowLayoutPanel3.Controls
                btn.Visible = True
            Next
            ToolStripButton5.Visible = False
            ToolStripSeparator1.Visible = False
        Else
            ToolStripButton5.Visible = True
            ToolStripSeparator1.Visible = True
        End If
    End Sub

    Private Sub ToolStripTextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchAppointment()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form2.Show(Me)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form5.Show(Me)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        MsgBox("Software Name : Hospital Management System" & vbNewLine & vbNewLine & "Made By : Khushi, Arfia , Mugdha, Prashansa" & vbNewLine & vbNewLine & "Description: Hospital Management System is a software application that is capable of managing a hospital's patients and their appointments. The user interface is simple and clean in order to achieve maximum efficiency.")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form10.Show(Me)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click, Button8.Click, Button7.Click, Button6.Click
        Form11.Show(Me)
        Dim cButton As Button = TryCast(sender, Object)
        Form11.selectedButton = cButton.Tag
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        SearchPatient()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        ToolStripTextBox1.Clear()
        ToolStripSeparator2.Visible = False
        ToolStripButton2.Visible = False
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        SearchAppointment()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        ToolStripTextBox2.Clear()
        ToolStripSeparator1.Visible = False
        ToolStripButton5.Visible = False
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click, Button9.Click, Button13.Click, Button12.Click, Button11.Click, Button10.Click
        'Initiliaze selection
        Dim cButton As Button = TryCast(sender, Object)
        For Each btn As Button In FlowLayoutPanel4.Controls
            Button9.BackColor = Color.PaleTurquoise
            Button10.BackColor = Color.PaleGreen
            Button11.BackColor = Color.LightPink
            Button12.BackColor = Color.Thistle
            Button13.BackColor = Color.Khaki
            Button14.BackColor = Color.LightSalmon

        Next
        cButton.BackColor = Color.White

        FlowLayoutPanel2.Controls.Clear()

        'Load selection data
        If cButton.Text = "Patients" Then
            LoadPatients()
        ElseIf cButton.Text = "Doctors" Then
            LoadDoctors()
        ElseIf cButton.Text = "Pharmacists" Then
            LoadPharmacists()
        ElseIf cButton.Text = "Nurses" Then
            LoadNurses()
        ElseIf cButton.Text = "Laboratorists" Then
            LoadLaboratorists()
        ElseIf cButton.Text = "Accountants" Then
            LoadAccountants()
        End If

        'Init labels
        ToolStripLabel1.Text = "Search " & cButton.Text.ToLower & ":"
        Label1.Text = cButton.Text & " (" & FlowLayoutPanel2.Controls.Count & ")"
        Label5.Text = "Current selection: " & cButton.Text
        selectedCategory = cButton.Text
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Me.Close()
    End Sub
End Class

Public Class Patient
    Inherits Button

    Public Sub New()
        AutoSize = False
        Dock = DockStyle.Top
        BackColor = Color.AliceBlue
        ForeColor = Color.DarkSlateGray
        FlatStyle = Windows.Forms.FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        Margin = New Padding(7, 7, 7, 0)
        Size = New Size(390, 70)
        Font = New Font("Segoe UI", 14)
    End Sub

    Private Sub whenClicked() Handles Me.Click
        If Me.Parent.GetContainerControl Is Form6 Then
            Form5.lblPatientName.Text = Text
            Form6.Close()
        ElseIf Me.Parent.GetContainerControl Is Main.SplitContainer1 Then
            'Show proper form
            If Main.selectedCategory = "Patients" Then
                Form3.Show(Main)
                Form3.selectedPatient = Me.Text
            ElseIf Main.selectedCategory = "Doctors" Then
                Form10.Show(Main)
                Form10.selectedDoctor = Me.Text
            Else
                Form11.selectedButton = Main.selectedCategory.Substring(0, Main.selectedCategory.Length - 1)
                Form11.Show(Main)
                Form11.selectedPerson = Me.Text
            End If
        End If
    End Sub
End Class

Public Class Appointment
    Inherits Button
    Public Sub New()
        AutoSize = False
        Dock = DockStyle.Top
        BackColor = Color.AliceBlue
        ForeColor = Color.DarkSlateGray
        FlatStyle = Windows.Forms.FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        Margin = New Padding(10, 10, 10, 10)
        Size = New Size(160, 160)
        TextAlign = ContentAlignment.BottomCenter
        Font = New Font("Segoe UI", 10)
        Image = My.Resources.iconAppointment64
    End Sub

    Private Sub whenClicked() Handles Me.Click
        'Load data
        Dim sReader As New System.IO.StreamReader(Main.pathAppointments & Text & "\appointmentData.txt")
        Dim lbData As New ListBox
        While sReader.Peek <> -1
            lbData.Items.Add(sReader.ReadLine)
        End While
        sReader.Close()

        'Visualise data
        Form9.lblPatientName.Text = lbData.Items(1)
        Form9.lblCaseType.Text = lbData.Items(0)
        Form9.lblDate.Text = lbData.Items(2)
        Form9.lblTime.Text = lbData.Items(3) & ":" & lbData.Items(4)
        Form9.lblAppointmentFor.Text = lbData.Items(5)
        Form9.lblLocation.Text = lbData.Items(6)

        Form9.Show(Main)
        Form9.selectedAppointment = Text
    End Sub
End Class