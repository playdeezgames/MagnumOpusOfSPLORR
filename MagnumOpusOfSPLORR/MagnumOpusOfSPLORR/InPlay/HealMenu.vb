Module HealMenu
    Sub Run(character As Character)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What will you use?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each entry In character.Inventory.StackedItems.Where(Function(x) x.Key.CanHeal)
            prompt.AddChoice(entry.Key.Name)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                HandleChoice(character, AllItemTypes.First(Function(x) x.Name = answer))
        End Select
    End Sub
    Private Sub HandleChoice(character As Character, itemType As ItemType)
        Dim healing = RNG.RollDice(itemType.HealDice)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"You heal {healing} wounds.")
        character.Wounds -= healing
        OkPrompt()
    End Sub
End Module
