Public Class Form8
    Dim canClose As Boolean = False
    Dim canReset As Boolean = False

    Private Sub Form8_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If canClose = False And canReset = False Then
            Main.Close()
        ElseIf canClose = True Then
            Main.Show()
            Reset.PlayLoginSound()
        ElseIf canReset = True Then
            Reset.Show()
        End If
    End Sub

    Private Sub Form8_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Main.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(MaskedTextBox1.Text) Then
            MessageBox.Show("Make sure that all data have been entered.", "Create Account - Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If TextBox1.Text <> Main.userName Or MaskedTextBox1.Text <> Main.passWord Then
                MessageBox.Show("Wrong username and/or password.", "Create Account - Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                canClose = True
                Close()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        MaskedTextBox1.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub




    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            MaskedTextBox1.PasswordChar = ""
        Else
            MaskedTextBox1.PasswordChar = "*"
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim msgReply = MessageBox.Show("Reset the account? The username and password will be lost.", "Hospital Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If msgReply = Windows.Forms.DialogResult.Yes Then
            Dim userInput = InputBox("In order to reset your account you must enter your current password.", "Reset Account - Hospital Management System")
            If userInput = Main.passWord Then
                Main.userName = ""
                Main.passWord = ""
                canReset = True
                Close()
            End If
        End If
    End Sub

End Class