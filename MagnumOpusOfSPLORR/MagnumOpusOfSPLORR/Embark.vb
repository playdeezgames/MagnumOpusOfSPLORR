Imports MOOS.Data
Imports MOOS.Game
Imports Spectre.Console

Module Embark
    Private Const MainMenuText = "Main Menu"
    Sub Run()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine("You exist!")
            Dim character As New PlayerCharacter()
            AnsiConsole.MarkupLine($"Location: {character.Location.Id}")
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Now what?[/]"
                }
            prompt.AddChoice(MainMenuText)
            Select Case AnsiConsole.Prompt(prompt)
                Case MainMenuText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
