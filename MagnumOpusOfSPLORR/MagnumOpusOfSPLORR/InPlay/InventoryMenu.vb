Imports Spectre.Console
Module InventoryMenu
    Private Const DropText = "Drop..."
    Sub Run(character As Character)
        Dim done = False
        While Not done
            Dim itemStacks = character.Inventory.StackedItems
            If itemStacks.Any Then
                AnsiConsole.MarkupLine($"Items: {String.Join(", ", itemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Yer Inventory:[/]"}
            prompt.AddChoice(NeverMindText)
            If itemStacks.Any Then
                prompt.AddChoice(DropText)
            End If
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    done = True
                Case DropText
                    HandleDrop(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleDrop(character As Character)
        Dim item = CommonMenu.ChooseItemNameFromInventory("Drop what?", True, character.Inventory)
        If item IsNot Nothing Then
            character.Location.Inventory.Add(item)
        End If
    End Sub
End Module
