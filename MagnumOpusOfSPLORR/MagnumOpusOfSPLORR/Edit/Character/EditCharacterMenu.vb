Module EditCharacterMenu
    Private Const ChangeLocationText = "Change Location..."
    Private Const ChangeCharacterTypeText = "Change Character Type..."
    Private Const ChangeWoundsText = "Change Wounds..."
    Private Const AssignPlayerCharacterText = "Make this the player character"
    Private Const EquipItemTypeText = "Equip Item Type..."
    Private Sub ShowStatus(character As Character)
        If character.IsPlayerCharacter Then
            AnsiConsole.MarkupLine("[aqua]This is the player character[/]")
        End If
        AnsiConsole.MarkupLine($"Id: {character.Id}")
        AnsiConsole.MarkupLine($"Name: {character.Name}")
        AnsiConsole.MarkupLine($"Location: {character.Location.UniqueName}")
        AnsiConsole.MarkupLine($"Character Type: {character.CharacterType.UniqueName}")
        AnsiConsole.MarkupLine($"Wounds: {character.Wounds}")
        ShowCounters(character)
        ShowInventory(character)
        ShowEquippedItems(character)
    End Sub

    Private Sub ShowEquippedItems(character As Character)
        Dim equipment = character.EquippedItems
        If equipment.Any Then
            AnsiConsole.MarkupLine($"Equipment:")
            For Each entry In equipment
                AnsiConsole.MarkupLine($"{entry.Key.UniqueName}: {entry.Value.UniqueName}")
            Next
        End If
    End Sub

    Private Sub ShowInventory(character As Character)
        Dim itemStacks = character.Inventory.StackedItems
        If itemStacks.Any Then
            AnsiConsole.MarkupLine($"Items: {String.Join(
                    ", ",
                    itemStacks.Select(Function(x) $"{x.Key.Name}(x{x.Value.Count})"))}")
        End If
    End Sub
    Friend Sub ShowCounters(character As Character)
        Dim counters = character.Counters
        If counters.Any Then
            AnsiConsole.MarkupLine($"Counters: {String.Join(", ", counters.Select(Function(x) $"{x.CounterType.Name}({x.Value})"))}")
        End If
    End Sub
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        prompt.AddChoice(ChangeLocationText)
        prompt.AddChoice(ChangeCharacterTypeText)
        prompt.AddChoice(ChangeWoundsText)
        prompt.AddChoice(InventoryText)
        prompt.AddChoice(CountersText)
        If character.HasAvailableEquipSlot Then
            prompt.AddChoice(EquipItemTypeText)
        End If
        If Not character.IsPlayerCharacter Then
            prompt.AddChoice(AssignPlayerCharacterText)
        End If
        If character.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(character)
            Select Case AnsiConsole.Prompt(CreatePrompt(character))
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(character)
                Case ChangeLocationText
                    HandleChangeLocation(character)
                Case ChangeCharacterTypeText
                    HandleChangeCharacterType(character)
                Case ChangeWoundsText
                    HandleChangeWounds(character)
                Case AssignPlayerCharacterText
                    character.SetAsPlayerCharacter()
                Case EquipItemTypeText
                    HandleEquipItemType(character)
                Case InventoryText
                    EditInventoryMenu.Run("Character Inventory:", character.Inventory)
                Case CountersText
                    EditCountersMenu.Run(character)
                Case DestroyText
                    character.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleEquipItemType(character As Character)
        Dim equipSlot = ChooseEquipSlot("Which Equip Slot?", True, character.AvailableEquipSlots)
        If equipSlot Is Nothing Then
            Return
        End If
        Dim itemType = ChooseItemType("Which Item Type?", True, AllItemTypes.Where(Function(x) x.EquipSlot IsNot Nothing AndAlso x.EquipSlot = equipSlot))
        If itemType Is Nothing Then
            Return
        End If
        character.EquipItemType(equipSlot, itemType)
    End Sub
    Private Sub HandleChangeWounds(character As Character)
        character.Wounds = AnsiConsole.Ask(Of Long)("[olive]New Wounds:[/]")
    End Sub
    Private Sub HandleChangeLocation(character As Character)
        Dim newLocation = ChooseLocation("Which Location", True)
        If newLocation IsNot Nothing Then
            character.Location = newLocation
        End If
    End Sub
    Private Sub HandleChangeCharacterType(character As Character)
        Dim newCharacterType = ChooseCharacterType("Which Character Type", True)
        If newCharacterType IsNot Nothing Then
            character.CharacterType = newCharacterType
        End If
    End Sub
    Private Sub HandleChangeName(character As Character)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub
End Module
