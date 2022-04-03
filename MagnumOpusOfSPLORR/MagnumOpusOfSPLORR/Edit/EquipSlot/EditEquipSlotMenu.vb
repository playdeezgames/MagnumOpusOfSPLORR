Module EditEquipSlotMenu
    Private Sub ShowStatus(equipSlot As EquipSlot)
        AnsiConsole.MarkupLine($"Id: {equipSlot.Id}")
        AnsiConsole.MarkupLine($"Name: {equipSlot.Name}")
    End Sub
    Private Function CreatePrompt(equipSlot As EquipSlot) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        If equipSlot.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(equipSlot As EquipSlot)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(equipSlot)
            Select Case AnsiConsole.Prompt(CreatePrompt(equipSlot))
                Case GoBackText
                    done = True
                Case DestroyText
                    equipSlot.Destroy()
                    done = True
                Case ChangeNameText
                    HandleChangeName(equipSlot)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleChangeName(equipSlot As EquipSlot)
        Dim newName = AnsiConsole.Ask(Of String)("New Equip Slot Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            equipSlot.Name = newName
        End If
    End Sub
End Module
