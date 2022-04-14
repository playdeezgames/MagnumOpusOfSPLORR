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
        Dim itemType = CommonEditorMenu.ChooseItemTypeUniqueNameFromInventory("Remove which item?", True, inventory)
        If itemType IsNot Nothing Then
            Dim items = inventory.StackedItems.Single(Function(x) x.Key = itemType).Value
            Dim quantity = If(items.Count = 1, 1, AnsiConsole.Ask(Of Integer)("[olive]How Many?[/]", 0))
            If quantity > 0 Then
                items = items.Take(quantity).ToList()
                For Each item In items
                    item.Destroy()
                Next
            End If
        End If
    End Sub
    Private Sub HandleAddItem(inventory As Inventory)
        Dim itemType = CommonEditorMenu.ChooseItemType("", True, AllItemTypes)
        If itemType IsNot Nothing Then
            Dim quantity = AnsiConsole.Ask(Of Long)("[olive]How many?[/]", 0)
            While quantity > 0
                Items.CreateItem(itemType, inventory)
                quantity -= 1
            End While
        End If
    End Sub
End Module
