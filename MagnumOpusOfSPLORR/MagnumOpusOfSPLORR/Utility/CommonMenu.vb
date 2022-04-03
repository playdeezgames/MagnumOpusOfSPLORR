Module CommonMenu
    Friend Const NeverMindText = "Never Mind"
    Friend Const GoBackText = "Go Back"
    Friend Const ChangeNameText = "Change Name"
    Friend Const DestroyText = "Destroy"
    Friend Const InventoryText = "Inventory..."
    Friend Const CountersText = "Counters..."
    Friend Const AddItemText = "Add Item..."
    Friend Const RemoveItemText = "Remove Item..."
    Friend Const BarriersText = "Barriers..."
    Friend Const RemoveText = "Remove"
    Friend Const OkText = "Ok"
    Function ChooseLocation(title As String, canCancel As Boolean) As Location
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each location In AllLocations
            prompt.AddChoices(location.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindLocationByUniqueName(answer)
        End Select
    End Function
    Function ChooseCharacterType(title As String, canCancel As Boolean) As CharacterType
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each characterType In AllCharacterTypes
            prompt.AddChoices(characterType.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindCharacterTypeByUniqueName(answer)
        End Select
    End Function
    Function ChooseDirection(title As String, candidates As List(Of Direction), canCancel As Boolean) As Direction
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each direction In candidates
            prompt.AddChoices(direction.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindDirectionByUniqueName(answer)
        End Select
    End Function
    Function ChooseDirection(title As String, canCancel As Boolean) As Direction
        Return ChooseDirection(title, AllDirections, canCancel)
    End Function

    Friend Function ChooseEquipSlot(title As String, canCancel As Boolean) As EquipSlot
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each equipSlot In AllEquipSlots
            prompt.AddChoices(equipSlot.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindEquipSlotByUniqueName(answer)
        End Select
    End Function

    Friend Function ChooseItemTypeNameFromInventory(title As String, canCancel As Boolean, inventory As Inventory) As ItemType
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        Dim groups = inventory.Items.GroupBy(Function(x) x.ItemType.UniqueName)
        For Each itemType In groups
            prompt.AddChoices(itemType.Key)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindItemTypeByUniqueName(groups.FirstOrDefault(Function(x) x.Key = answer).Key)
        End Select
    End Function

    Friend Function ChooseItemType(title As String, canCancel As Boolean) As ItemType
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each itemType In AllItemTypes
            prompt.AddChoices(itemType.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindItemTypeByUniqueName(answer)
        End Select
    End Function
    Friend Function ChooseValidDice(title As String) As String
        Dim damageDice As String
        Do
            damageDice = AnsiConsole.Ask(Of String)($"[olive]{title}[/]")
        Loop Until RNG.ValidateDice(damageDice)
        Return damageDice
    End Function
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
