Module EditCharacterTypeMenu
    Private Const ChangeHealthText = "Change Health..."
    Private Const ChangeDamageDiceText = "Change Damage Dice..."
    Private Const ChangeArmorDiceText = "Change Armor Dice..."
    Private Const AddEquipSlotText = "Add Equip Slot..."
    Private Const RemoveEquipSlotText = "Remove Equip Slot..."
    Private Sub ShowStatus(characterType As CharacterType)
        AnsiConsole.MarkupLine($"Id: {characterType.Id}")
        AnsiConsole.MarkupLine($"Name: {characterType.Name}")
        AnsiConsole.MarkupLine($"Health: {characterType.Health}")
        AnsiConsole.MarkupLine($"Attack: {characterType.AttackDice}")
        AnsiConsole.MarkupLine($"Defend: {characterType.DefendDice}")
        ShowEquipSlots(characterType)
    End Sub
    Private Sub ShowEquipSlots(characterType As CharacterType)
        Dim equipSlots = characterType.EquipSlots
        If equipSlots.Any Then
            AnsiConsole.MarkupLine($"Equip Slot: {String.Join(", ", equipSlots.Select(Function(x) $"{x.Name}"))}")
        End If
    End Sub
    Private Function CreatePrompt(characterType As CharacterType) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        prompt.AddChoice(ChangeHealthText)
        prompt.AddChoice(ChangeDamageDiceText)
        prompt.AddChoice(ChangeArmorDiceText)
        If characterType.AvailableEquipSlots.Any Then
            prompt.AddChoice(AddEquipSlotText)
        End If
        If characterType.HasEquipSlots Then
            prompt.AddChoice(RemoveEquipSlotText)
        End If
        If characterType.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(characterType As CharacterType)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(characterType)
            Select Case AnsiConsole.Prompt(CreatePrompt(characterType))
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(characterType)
                Case ChangeHealthText
                    HandleChangeHealth(characterType)
                Case ChangeDamageDiceText
                    HandleChangeDamageDice(characterType)
                Case ChangeArmorDiceText
                    HandleChangeArmorDice(characterType)
                Case AddEquipSlotText
                    HandleAddEquipSlot(characterType)
                Case RemoveEquipSlotText
                    HandleRemoveEquipSlot(characterType)
                Case DestroyText
                    characterType.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleRemoveEquipSlot(characterType As CharacterType)
        Dim equipSlot = CommonEditorMenu.ChooseEquipSlot("Remove which?", True, characterType.EquipSlots)
        If equipSlot IsNot Nothing Then
            CharacterTypeEquipSlotData.Clear(characterType.Id, equipSlot.Id)
        End If
    End Sub
    Private Sub HandleAddEquipSlot(characterType As CharacterType)
        Dim equipSlot = CommonEditorMenu.ChooseEquipSlot("Add which?", True, characterType.AvailableEquipSlots)
        If equipSlot IsNot Nothing Then
            CharacterTypeEquipSlotData.Write(characterType.Id, equipSlot.Id)
        End If
    End Sub
    Private Sub HandleChangeArmorDice(characterType As CharacterType)
        characterType.DefendDice = CommonEditorMenu.ChooseValidDice("New Armor Dice:")
    End Sub
    Private Sub HandleChangeDamageDice(characterType As CharacterType)
        characterType.AttackDice = CommonEditorMenu.ChooseValidDice("New Damage Dice:")
    End Sub
    Private Sub HandleChangeHealth(characterType As CharacterType)
        characterType.Health = AnsiConsole.Ask(Of Long)("[olive]New Health:[/]")
    End Sub
    Private Sub HandleChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Type Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
