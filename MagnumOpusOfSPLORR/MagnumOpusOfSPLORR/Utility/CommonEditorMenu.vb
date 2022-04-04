Public Module CommonEditorMenu
    Public Const GoBackText = "Go Back" 'editor
    Public Const ChangeNameText = "Change Name" 'editor
    Public Const DestroyText = "Destroy" 'editor
    Public Const CountersText = "Counters..." 'editor
    Public Const AddItemText = "Add Item..." 'editor
    Public Const RemoveItemText = "Remove Item..." 'editor
    Public Const BarriersText = "Barriers..." 'editor
    Public Const RemoveText = "Remove" 'editor
    Function ChooseLocation(title As String, canCancel As Boolean) As Location 'editor
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
    Function ChooseCharacterType(title As String, canCancel As Boolean) As CharacterType 'editor
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
    Function ChooseDirection(title As String, candidates As List(Of Direction), canCancel As Boolean) As Direction 'editor
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
    Public Function ChooseEquipSlot(title As String, canCancel As Boolean) As EquipSlot 'editor
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
    Public Function ChooseItemType(title As String, canCancel As Boolean) As ItemType 'editor
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
    Public Function ChooseValidDice(title As String) As String 'editor
        Dim damageDice As String
        Do
            damageDice = AnsiConsole.Ask(Of String)($"[olive]{title}[/]")
        Loop Until RNG.ValidateDice(damageDice)
        Return damageDice
    End Function
End Module
