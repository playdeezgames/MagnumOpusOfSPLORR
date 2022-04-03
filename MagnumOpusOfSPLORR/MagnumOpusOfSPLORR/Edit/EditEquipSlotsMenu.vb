Module EditEquipSlotsMenu
    Private Const CreateEquipSlotText = "Create Equip Slot"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Equip Slots:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateEquipSlotText)
        For Each equipSlot In AllEquipSlots
            prompt.AddChoice(equipSlot.UniqueName)
        Next
        Return prompt
    End Function
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim answer = AnsiConsole.Prompt(CreatePrompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case CreateEquipSlotText
                    HandleCreateEquipSlot()
                Case Else
                    EditEquipSlotMenu.Run(FindEquipSlotByUniqueName(answer))
            End Select
        End While
    End Sub
    Private Sub HandleCreateEquipSlot()
        Dim equipSlotName = AnsiConsole.Ask("[olive]Equip Slot Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(equipSlotName) Then
            EditEquipSlotMenu.Run(CreateEquipSlot(equipSlotName))
        End If
    End Sub
End Module
