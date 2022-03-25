Imports Spectre.Console
Imports MOOS.Game

Module EditCharactersMenu
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Characters:[/]"}
            prompt.AddChoice(GoBackText)
            For Each character In AllCharacters
                prompt.AddChoice(character.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case Else
                    EditCharacterMenu.Run(AllCharacters.Single(Function(x) x.UniqueName = answer))
            End Select
        End While
    End Sub
End Module
