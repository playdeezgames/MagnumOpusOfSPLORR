Imports Spectre.Console
Module EditGameMenu
    Private Const StopEditingText = "Stop Editing"
    Sub Run()
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Edit Menu:[/]"
                }
            prompt.AddChoice(StopEditingText)
            Select Case AnsiConsole.Prompt(prompt)
                Case StopEditingText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
