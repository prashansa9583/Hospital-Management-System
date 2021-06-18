Public Class Form11
    Public selectedPerson As String = Nothing
    Public selectedButton As String = Nothing
    Dim selectedID As Integer = 0
    Dim strID As String = Nothing

#Region "Custom Subroutines"
    Private Sub AddCategory()
        'Check if all filled
        Dim allFilled As Boolean = True
        For Each ctrl In GroupBox1.Controls
            If TypeOf ctrl Is TextBox Then
                If String.IsNullOrWhiteSpace(ctrl.Text) Then
                    allFilled = False
                End If
            End If
        Next

        If allFilled Then
            If Not String.IsNullOrWhiteSpace(selectedPerson) Then
                Dim oldDirectory As String = Main.pathStorage & selectedButton & "s\" & selectedPerson
                My.Computer.FileSystem.DeleteDirectory(oldDirectory, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            Dim dirPath As String = Main.pathStorage & selectedButton & "s\" & TextBox1.Text & " - " & TextBox2.Text & "\"
            My.Computer.FileSystem.CreateDirectory(dirPath)
            Dim sWriter As New IO.StreamWriter(dirPath & selectedButton.ToLower & "Info.txt")
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    sWriter.WriteLine(ctrl.Text)
                End If
            Next
            sWriter.Close()
            IncrementID()
            Main.UpdateFPanel2()
            Close()
        Else
            MessageBox.Show("Please make sure that all data have been entered.", "Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub RemoveCategory()
        Dim msgReply = MessageBox.Show("Are you sure you wish to remove this " & selectedButton.ToLower & "?", "Hospital Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If msgReply = Windows.Forms.DialogResult.Yes Then
            Dim readPath As String = Main.pathStorage & selectedButton & "s\" & selectedPerson
            My.Computer.FileSystem.DeleteDirectory(readPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Close()
            Main.UpdateFPanel2()
        End If
    End Sub

    Private Sub GetID()
        Main.LoadGeneric()
        If selectedButton = "Pharmacist" Then
            selectedID = Main.PHID
            strID = "PHID"
            Icon = My.Resources.Pharmacist1
        ElseIf selectedButton = "Nurse" Then
            selectedID = Main.NID
            Icon = My.Resources.Nurse1
            strID = "NID"
        ElseIf selectedButton = "Laboratorist" Then
            selectedID = Main.LID
            Icon = My.Resources.Laboratorist1
            strID = "LID"
        ElseIf selectedButton = "Accountant" Then
            selectedID = Main.AID
            Icon = My.Resources.Accountant1
            strID = "AID"
        End If
        If String.IsNullOrWhiteSpace(selectedPerson) Then
            TextBox1.Text = selectedID
        End If
    End Sub

    Private Sub IncrementID()
        Dim sWriter As New IO.StreamWriter(Main.pathGeneric & strID & ".txt")
        If selectedButton = "Pharmacist" Then
            sWriter.WriteLine(Main.PHID + 1)
        ElseIf selectedButton = "Nurse" Then
            sWriter.WriteLine(Main.NID + 1)
        ElseIf selectedButton = "Laboratorist" Then
            sWriter.WriteLine(Main.LID + 1)
        ElseIf selectedButton = "Accountant" Then
            sWriter.WriteLine(Main.AID + 1)
        End If
        sWriter.Close()
    End Sub
#End Region

    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
    End Sub

    Private Sub Form11_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If String.IsNullOrWhiteSpace(selectedPerson) Then
            Text = "Add " & selectedButton
        Else
            Text = "Edit " & selectedButton
            Button1.Text = "Save"
            Button2.Text = "Remove"

            Dim readPath As String = Main.pathStorage & selectedButton & "s\" & selectedPerson & "\" & selectedButton.ToLower & "Info.txt"
            Dim sReader As New IO.StreamReader(readPath)
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    ctrl.Text = sReader.ReadLine()
                End If
            Next
            sReader.Close()
        End If
        GroupBox1.Text = selectedButton & " information:"
        GetID()
    End Sub

    Private Sub Form11_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Enter Then
            AddCategory()
        ElseIf e.KeyCode = Keys.Delete Then
            RemoveCategory()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If String.IsNullOrWhiteSpace(selectedPerson) Then
            TextBox1.Text = selectedID
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AddCategory()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Clear" Then
            For Each ctrl In GroupBox1.Controls
                If TypeOf ctrl Is TextBox Then
                    ctrl.Clear()
                End If
            Next
        Else
            RemoveCategory()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.Length > 10 Or TextBox5.Text.Length < 10 Then
            ErrorProvider1.SetError(TextBox5, "Enter a Valid Contact Number")
        Else
            ErrorProvider1.SetError(TextBox5, "")
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If IsNumeric(TextBox2.Text) Then
            ErrorProvider1.SetError(TextBox2, "Only Text Allowed")
        Else
            ErrorProvider1.SetError(TextBox2, "")
        End If
    End Sub
End Class