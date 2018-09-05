'https://social.technet.microsoft.com/wiki/contents/articles/13319.vb-net-how-to-make-a-right-click-menu-for-a-tray-icon.aspx
Public Class trayform
    Private Sub trayform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        traymenu.Show(Cursor.Position) 'Shows the Right click menu on the cursor position
        Me.Left = traymenu.Left + 1 'Puts the form behind the menu horizontally
        Me.Top = traymenu.Top + 1 'Puts the form behind the menu vertically
        If Form1._lang = "eng" Then
            EngToolStripMenuItem.Checked = True
        ElseIf Form1._lang = "chs" Then
            ChsToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub trayform_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close() 'Closes the "trayform" Form and consequently every thing in it, including the "traymenu"
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Form1.tray.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
        Form1.FinalizingIniFile()
        End 'Kills the application with BUG: it skips FormClosing Event
    End Sub

    Private Sub EngToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EngToolStripMenuItem.Click
        'EngToolStripMenuItem.Checked = True  ' BUG: checked mark does NOT display; Cause: each time it handles "Me.Load", Properties "Cheacked" reflashed to default
        'ChsToolStripMenuItem.Checked = False
        'Debug.WriteLine("eng: " & EngToolStripMenuItem.CheckState.ToString)
        Form1._lang = "eng"
    End Sub

    Private Sub ChsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChsToolStripMenuItem.Click
        'ChsToolStripMenuItem.Checked = True
        'EngToolStripMenuItem.Checked = False
        'Debug.WriteLine("eng: " & EngToolStripMenuItem.CheckState.ToString)
        Form1._lang = "chs"
    End Sub
End Class