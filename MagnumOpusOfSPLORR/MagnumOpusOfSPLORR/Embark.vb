Imports MOOS.Data
Imports MOOS.Game
Imports Spectre.Console

Module Embark
    Private Const MainMenuText = "Main Menu"
    Private Const GoText = "Go..."
    Sub Run()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine("You exist!")
            Dim character As New PlayerCharacter()
            Dim location = character.Location
            AnsiConsole.MarkupLine($"Location: {location.Name}")
            Dim routes = location.Routes
            If routes.Any Then
                AnsiConsole.MarkupLine($"Exits: {String.Join(", ", routes.Select(Function(r) r.Direction.Name))}")
            End If
            Dim itemStacks = location.Inventory.StackedItems
            If itemStacks.Any Then
                AnsiConsole.MarkupLine($"Items: {String.Join(", ", itemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
            End If
            If character.DidWin Then
                done = True
                HandleWin()
            Else
                Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Now what?[/]"
                }
                If routes.Any Then
                    prompt.AddChoice(GoText)
                End If
                prompt.AddChoice(MainMenuText)
                Select Case AnsiConsole.Prompt(prompt)
                    Case MainMenuText
                        done = True
                    Case GoText
                        MoveMenu.Run(character, routes)
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
        End While
    End Sub

    Private Sub HandleWin()
        AnsiConsole.MarkupLine("You win!")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
