Module InventoryMenu
    Private Const DropText = "Drop..."
    Private Const EquipText = "Equip..."
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim itemStacks = character.Inventory.StackedItems
            If itemStacks.Any Then
                AnsiConsole.MarkupLine($"Yer Inventory: {String.Join(", ", itemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What now?[/]"}
            prompt.AddChoice(NeverMindText)
            If itemStacks.Any(Function(x) x.Key.HasEquipSlot) Then
                prompt.AddChoice(EquipText)
            End If
            If itemStacks.Any Then
                prompt.AddChoice(DropText)
            End If
            Dim answer = AnsiConsole.Prompt(prompt)
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
    Private Sub HandleEquip(character As Character)
        Dim itemType = ChooseItemTypeNameFromInventory("Equip What?", True, character.Inventory)
        If itemType IsNot Nothing And itemType.HasEquipSlot Then
            If character.EquippedItems.Keys.Any(Function(x) x = itemType.EquipSlot) Then
                'create item of already equipped item type and place it into inventory
                Throw New NotImplementedException
            End If
            character.EquipItemType(itemType.EquipSlot, itemType)
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
