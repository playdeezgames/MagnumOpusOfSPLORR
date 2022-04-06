Public Module CommonPlayerMenu
    Public Const NeverMindText = "Never Mind" 'both
    Public Const InventoryText = "Inventory..." 'both
    Friend Const OkText = "Ok" 'both
    Public Sub OkPrompt() 'both
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
    Public Function ChooseItemTypeNameFromInventory(title As String, canCancel As Boolean, inventory As Inventory) As ItemType 'both
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        Dim groups = inventory.Items.GroupBy(Function(x) x.ItemType.Name)
        For Each itemType In groups
            prompt.AddChoices(itemType.Key)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return FindItemTypeByName(groups.FirstOrDefault(Function(x) x.Key = answer).Key).First
        End Select
    End Function
    Public Function ChooseItemTypeUniqueNameFromInventory(title As String, canCancel As Boolean, inventory As Inventory) As ItemType 'both
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
End Module
