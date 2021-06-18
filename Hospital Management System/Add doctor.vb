Public Class Form10
    Public selectedDoctor As String = Nothing

#Region "Custom Subroutines"
    Private Sub AddDoctor()
        'Check for empty textboxes
        Dim allFilled As Boolean = True
        For Each ctrl In GroupBox1.Controls
            If TypeOf ctrl Is TextBox Then
                If String.IsNullOrWhiteSpace(ctrl.Text) Then
                    allFilled = False
                    Exit For
                End If
            End If
        Next

        'Write doctor to file
        If allFilled Then
            Dim dirPath As String = Nothing
            If String.IsNullOrWhiteSpace(selectedDoctor) Then
                dirPath = Main.pathDoctors & TextBox1.Text & " - " & TextBox2.Text
            Else
                My.Computer.FileSystem.DeleteDirectory(Main.pathDoctors & selectedDoctor, FileIO.DeleteDirectoryOption.DeleteAllContents)
                selectedDoctor = TextBox1.Text & " - " & TextBox2.Text
                dirPath = Main.pathDoctors & selectedDoctor
            End If
            My.Computer.FileSystem.CreateDirectory(dirPath)
            Dim sWriter As New IO.StreamWriter(dirPath & "\doctorInfo.txt")
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    sWriter.WriteLine(ctrl.Text)
                End If
            Next
            sWriter.Close()

            'Visualise doctors
            Main.UpdateFPanel2()

            'Increment DID
            If String.IsNullOrWhiteSpace(selectedDoctor) Then
                sWriter = New IO.StreamWriter(Main.pathGeneric & "DID.txt")
                sWriter.Write(Main.DID + 1)
                sWriter.Close()
            End If
            Close()
        Else
            MessageBox.Show("Please make sure that all data have been entered.", "Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub RemoveDoctor()
        Dim msgReply = MessageBox.Show("Are you sure you wish to remove this doctor?", "Hospital Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If msgReply = Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteDirectory(Main.pathDoctors & selectedDoctor, FileIO.DeleteDirectoryOption.DeleteAllContents)
            If Main.selectedCategory = "Doctors" Then
                Main.LoadDoctors()
            End If
            Close()
        End If
    End Sub
#End Region

    Private Sub Form10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
    End Sub

    Private Sub Form10_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Main.LoadGeneric()
        TextBox1.Text = Main.DID
        TextBox2.Focus()

        If Not String.IsNullOrWhiteSpace(selectedDoctor) Then
            Dim sReader As New IO.StreamReader(Main.pathDoctors & selectedDoctor & "\doctorInfo.txt")
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    ctrl.Text = sReader.ReadLine()
                End If
            Next
            sReader.Close()
            Text = "Edit Doctor"
            Button1.Text = "Save"
            Button2.Text = "Remove"
        End If
    End Sub

    Private Sub Form10_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Enter Then
            AddDoctor()
        ElseIf e.KeyCode = Keys.Delete Then
            RemoveDoctor()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If String.IsNullOrWhiteSpace(selectedDoctor) Then
            TextBox1.Text = Main.DID
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        AddDoctor()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Clear" Then
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    ctrl.Clear()
                End If
            Next
        Else
            RemoveDoctor()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If IsNumeric(TextBox2.Text) Then
            ErrorProvider1.SetError(TextBox2, "Only Text Allowed")
        Else
            ErrorProvider1.SetError(TextBox2, "")
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.Length > 10 Or TextBox5.Text.Length < 10 Then
            ErrorProvider1.SetError(TextBox5, "Enter a Valid Contact Number")
        Else
            ErrorProvider1.SetError(TextBox5, "")
        End If
    End Sub

End Class