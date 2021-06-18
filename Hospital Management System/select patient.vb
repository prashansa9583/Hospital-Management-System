Public Class Form6
    Dim fPanel As FlowLayoutPanel

#Region "Custom Subroutines"
    Private Sub SearchPatient()
        For Each ctrl As Button In fPanel.Controls
            If ctrl.Text.ToLower.Contains(ToolStripTextBox1.Text.ToLower) Then
                ctrl.Visible = True
            Else
                ctrl.Visible = False
            End If
        Next
    End Sub

    Private Sub CancelSearch()
        ToolStripTextBox1.Clear()
        ToolStripSeparator1.Visible = False
        ToolStripButton2.Visible = False
        For Each ctrl As Button In fPanel.Controls
            ctrl.Visible = True
        Next
    End Sub
#End Region

    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = New Point(Main.Left + 25, Main.Top + 45)
    End Sub

    Private Sub Form6_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim newPanel As New FlowLayoutPanel
        newPanel.BackColor = SystemColors.GradientInactiveCaption
        newPanel.AutoScroll = True
        newPanel.Dock = DockStyle.Fill
        For Each dirFound As String In My.Computer.FileSystem.GetDirectories(Main.pathPatients)
            Dim newPatient As New Patient
            newPatient.Text = dirFound.Replace(Main.pathPatients, "")
            newPanel.Controls.Add(newPatient)
        Next
        Controls.Add(newPanel)
        newPanel.BringToFront()
        fPanel = newPanel
        ToolStripTextBox1.Focus()
    End Sub

    Private Sub Form6_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.TextChanged
        If ToolStripTextBox1.TextLength = 0 Then
            CancelSearch()
        Else
            ToolStripSeparator1.Visible = True
            ToolStripButton2.Visible = True
        End If
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchPatient()
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        CancelSearch()
    End Sub
End Class