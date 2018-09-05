'Module suspended
Module Program
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1)  ' BUG: re-dim global variables in Form1
    End Sub
End Module
