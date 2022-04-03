Module EditItemTypeMenu
    Private Const ChangeHealDiceText = "Change Heal Dice..."
    Private Const AddEquipSlotText = "Add Equip Slot..."
    Private Const ChangeEquipSlotText = "Change Equip Slot..."
    Private Const RemoveEquipSlotText = "Remove Equip Slot"
    Private Sub ShowStatus(itemType As ItemType)
        AnsiConsole.MarkupLine($"Id: {itemType.Id}")
        AnsiConsole.MarkupLine($"Name: {itemType.Name}")
        If itemType.CanHeal Then
            AnsiConsole.MarkupLine($"Heal Dice: {itemType.HealDice}")
        End If
        If itemType.HasEquipSlot Then
            AnsiConsole.MarkupLine($"Equip Slot: {itemType.EquipSlot.UniqueName}")
        End If
    End Sub
    Private Function CreatePrompt(itemType As ItemType) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        prompt.AddChoice(ChangeHealDiceText)
        If itemType.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        If itemType.HasEquipSlot Then
            prompt.AddChoice(ChangeEquipSlotText)
            prompt.AddChoice(RemoveEquipSlotText)
        Else
            prompt.AddChoice(AddEquipSlotText)
        End If
        Return prompt
    End Function
    Sub Run(itemType As ItemType)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(itemType)
            Select Case AnsiConsole.Prompt(CreatePrompt(itemType))
                Case GoBackText
                    done = True
                Case DestroyText
                    itemType.Destroy()
                    done = True
                Case ChangeNameText
                    HandleChangeName(itemType)
                Case ChangeHealDiceText
                    HandleChangeHealDice(itemType)
                Case AddEquipSlotText, ChangeEquipSlotText
                    HandleChangeEquipSlot(itemType)
                Case RemoveEquipSlotText
                    HandlRemoveEquipSlot(itemType)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandlRemoveEquipSlot(itemType As ItemType)
        itemType.EquipSlot = Nothing
    End Sub

    Private Sub HandleChangeEquipSlot(itemType As ItemType)
        Dim equipSlot = CommonMenu.ChooseEquipSlot("New Equip Slot:", True)
        If equipSlot IsNot Nothing Then
            itemType.EquipSlot = equipSlot
        End If
    End Sub

    Private Sub HandleChangeHealDice(itemType As ItemType)
        itemType.HealDice = AnsiConsole.Ask("[olive]Heal Dice:[/]", "")
    End Sub
    Private Sub HandleChangeName(itemType As ItemType)
        Dim newName = AnsiConsole.Ask(Of String)("New Item Type Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            itemType.Name = newName
        End If
    End Sub
End Module
