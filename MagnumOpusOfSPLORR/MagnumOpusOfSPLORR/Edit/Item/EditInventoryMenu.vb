Module EditInventoryMenu
    Friend Sub ShowStatus(inventory As Inventory)
        Dim itemStacks = inventory.StackedItems
        If itemStacks.Any Then
            AnsiConsole.MarkupLine($"Items: {String.Join(
                    ", ",
                    itemStacks.Select(Function(x) $"{x.Key.Name}(x{x.Value.Count})"))}")
        End If
    End Sub
    Private Function CreatePrompt(title As String, inventory As Inventory) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(AddItemText)
        If Not inventory.IsEmpty Then
            prompt.AddChoice(RemoveItemText)
        End If
        Return prompt
    End Function
    Sub Run(title As String, inventory As Inventory)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(inventory)
            Select Case AnsiConsole.Prompt(CreatePrompt(title, inventory))
                Case GoBackText
                    done = True
                Case AddItemText
                    HandleAddItem(inventory)
                Case RemoveItemText
                    HandleRemoveItem(inventory)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleRemoveItem(inventory As Inventory)
        Dim item = CommonMenu.ChooseItemNameFromInventory("Remove which item?", True, inventory)
        If item IsNot Nothing Then
            item.Destroy()
        End If
    End Sub
    Private Sub HandleAddItem(inventory As Inventory)
        Dim itemType = CommonMenu.ChooseItemType("", True)
        If itemType IsNot Nothing Then
            Items.CreateItem(itemType, inventory)
        End If
    End Sub
End Module
