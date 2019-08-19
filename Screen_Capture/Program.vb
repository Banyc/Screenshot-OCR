Imports System

Module Program
    Private _mainController As Controller.Controller
    Private _viewController As View.ViewController

    Public Sub Main()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        _mainController = New Controller.Controller()
        _viewController = New View.ViewController(_mainController)  ' Add Event to mainController
        Controller.Hotkeys.Hotkeys.Init(_mainController)
        _mainController.Start()

        Application.Run()  ' BUG: re-dim global variables in Form1
    End Sub

End Module
