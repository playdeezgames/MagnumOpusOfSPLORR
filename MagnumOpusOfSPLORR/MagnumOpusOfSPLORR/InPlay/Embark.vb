Module Embark
    Private Const MainMenuText = "Main Menu"
    Private Const GoText = "Go..."
    Private Const PickUpText = "Pick Up..."
    Private Sub ShowStatus(character As Character)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You exist!")
        Dim location = character.Location
        AnsiConsole.MarkupLine($"Location: {location.Name}")
        Dim routes = location.Routes
        If routes.Any Then
            AnsiConsole.MarkupLine($"Exits: {String.Join(", ", routes.Select(Function(r) r.Direction.Name))}")
        End If
        Dim locationItemStacks = location.Inventory.StackedItems
        If locationItemStacks.Any Then
            AnsiConsole.MarkupLine($"Items: {String.Join(", ", locationItemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
        End If
    End Sub
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Now what?[/]"
                }
        If character.Location.Routes.Any Then
            prompt.AddChoice(GoText)
        End If
        If Not character.Location.Inventory.IsEmpty Then
            prompt.AddChoice(PickUpText)
        End If
        If Not character.Inventory.IsEmpty Then
            prompt.AddChoice(InventoryText)
        End If
        prompt.AddChoice(MainMenuText)
        Return prompt
    End Function
    Sub Run()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            Dim character As New PlayerCharacter()
            ShowStatus(character)
            If character.DidWin Then
                done = True
                HandleWin()
            Else
                Select Case AnsiConsole.Prompt(CreatePrompt(character))
                    Case MainMenuText
                        done = True
                    Case GoText
                        MoveMenu.Run(character, character.Location.Routes)
                    Case PickUpText
                        HandlePickUp(character)
                    Case InventoryText
                        InventoryMenu.Run(character)
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
        End While
    End Sub
    Private Sub HandlePickUp(character As PlayerCharacter)
        Dim item As Item = CommonMenu.ChooseItemNameFromInventory("Pick up what?", True, character.Location.Inventory)
        If item IsNot Nothing Then
            character.Inventory.Add(item)
        End If
    End Sub
    Private Sub HandleWin()
        AnsiConsole.MarkupLine("You win!")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
