Imports System.IO
'https://social.technet.microsoft.com/wiki/contents/articles/13319.vb-net-how-to-make-a-right-click-menu-for-a-tray-icon.aspx
Public Class trayform
    Property IsRunning As Boolean = False  'Prevent Me from closing while there are still functions running
    Private _controller As Controller.Controller
    Public Sub New(controller As Controller.Controller)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        _controller = controller
    End Sub
    Private Sub trayform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        traymenu.Show(Cursor.Position) 'Shows the Right click menu on the cursor position
        Me.Left = traymenu.Left + 1 'Puts the form behind the menu horizontally
        Me.Top = traymenu.Top + 1 'Puts the form behind the menu vertically
    End Sub

    Private Sub trayform_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        GC.Collect()
        If Not IsRunning Then
            Me.Close() 'Closes the "trayform" Form and consequently every thing in it, including the "traymenu"  'BUG: it will interrupt the Upload Item's Event
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        _controller.Close()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim settingsForm As SettingsForm = New SettingsForm(_controller)

        settingsForm.Show()
        SettingsForm.Activate()
    End Sub

    Private Sub UploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadToolStripMenuItem.Click
        IsRunning = True  'Prevent Me from closing when Me deactivated
        'https://stackoverflow.com/questions/6200329/how-to-load-a-file-from-folder-to-memory-stream-buffer
        If OpenFileDialog1.ShowDialog() <> DialogResult.Cancel Then
            Dim imgPath = OpenFileDialog1.FileName
            Dim binReader As New BinaryReader(File.Open(imgPath, FileMode.Open))
            Dim binData = binReader.ReadBytes(binReader.BaseStream.Length)
            Controller.HttpRequests.AutoDirectOCR(binData)
        End If
        Me.Close()
    End Sub

    Private Sub ClearClipboard_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearClipboardToolStripMenuItem.Click
        Clipboard.Clear()
    End Sub
End Class
