Module InventoryMenu
    Private Const DropText = "Drop..."
    Private Const EquipText = "Equip..."
    Private Sub ShowInventoryStacks(character As Character)
        Dim itemStacks = character.Inventory.StackedItems
        If itemStacks.Any Then
            AnsiConsole.MarkupLine($"Yer Inventory: {String.Join(", ", itemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
        End If
    End Sub
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim itemStacks = character.Inventory.StackedItems
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What now?[/]"}
        prompt.AddChoice(NeverMindText)
        If itemStacks.Any(Function(x) x.Key.HasEquipSlot) Then
            prompt.AddChoice(EquipText)
        End If
        If itemStacks.Any Then
            prompt.AddChoice(DropText)
        End If
        Return prompt
    End Function
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowInventoryStacks(character)
            ShowEquipment(character)
            Dim answer = AnsiConsole.Prompt(CreatePrompt(character))
            Select Case answer
                Case NeverMindText
                    done = True
                Case EquipText
                    HandleEquip(character)
                Case DropText
                    HandleDrop(character)
                    done = character.Inventory.IsEmpty
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub ShowEquipment(character As Character)
        Dim equippedItems = character.EquippedItems
        If equippedItems.Any Then
            AnsiConsole.MarkupLine("Equipped:")
            For Each entry In equippedItems
                AnsiConsole.MarkupLine($"- {entry.Key.Name}: {entry.Value.Name}")
            Next
        End If
    End Sub
    Private Sub HandleEquip(character As Character)
        Dim itemType = ChooseItemTypeNameFromInventory("Equip What?", True, character.Inventory)
        If itemType IsNot Nothing And itemType.HasEquipSlot Then
            If character.EquippedItems.Keys.Any(Function(x) x = itemType.EquipSlot) Then
                Dim entry = character.EquippedItems.Single(Function(x) x.Key = itemType.EquipSlot)
                CreateItem(entry.Value, character.Inventory)
            End If
            character.EquipItemType(itemType.EquipSlot, itemType)
            Dim item = character.Inventory.Items.First(Function(x) x.ItemType = itemType)
            item.Destroy()
        End If
    End Sub
    Private Sub HandleDrop(character As Character)
        Dim itemType = CommonPlayerMenu.ChooseItemTypeNameFromInventory("Drop what?", True, character.Inventory)
        If itemType IsNot Nothing Then
            Dim items = character.Inventory.StackedItems.Single(Function(x) x.Key = itemType).Value
            Dim quantity = If(items.Count = 1, 1, AnsiConsole.Ask(Of Integer)("[olive]How Many?[/]", 0))
            If quantity > 0 Then
                items = items.Take(quantity).ToList()
                For Each item In items
                    character.Location.Inventory.Add(item)
                Next
            End If
        End If
    End Sub
End Module
