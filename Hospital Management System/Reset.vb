Public Class Reset
    Dim canClose As Boolean = False

#Region "Custom Subroutine"
    Public Sub PlayLoginSound()
        Dim pathSound As String = "C:\Windows\media\Windows Notify Calendar.wav"
        If My.Computer.FileSystem.FileExists(pathSound) Then
            My.Computer.Audio.Play(pathSound, AudioPlayMode.Background)
        End If
    End Sub
#End Region

    Private Sub Form7_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Main.Hide()
    End Sub

    Private Sub Form7_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If canClose = False Then
            Main.Close()
        Else
            Main.Show()
            PlayLoginSound()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(MaskedTextBox1.Text) Or String.IsNullOrWhiteSpace(MaskedTextBox2.Text) Then
            MessageBox.Show("Make sure that all data have been entered.", "Create Account - Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If MaskedTextBox1.Text <> MaskedTextBox2.Text Then
                MessageBox.Show("The passwords you entered do not match.", "Create Account - Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim sWriter As New System.IO.StreamWriter(Main.pathAccount & "username.txt")
                sWriter.Write(TextBox1.Text)
                sWriter.Close()
                sWriter = New System.IO.StreamWriter(Main.pathAccount & "password.txt")
                sWriter.Write(MaskedTextBox1.Text)
                sWriter.Close()
                canClose = True
                Close()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        MaskedTextBox1.Clear()
        MaskedTextBox2.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Main.Close()
    End Sub


End Class