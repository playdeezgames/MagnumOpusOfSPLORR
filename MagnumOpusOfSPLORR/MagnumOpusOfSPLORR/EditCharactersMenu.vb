Imports Spectre.Console

Module EditCharactersMenu
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Characters:[/]"}
            prompt.AddChoice(GoBackText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
