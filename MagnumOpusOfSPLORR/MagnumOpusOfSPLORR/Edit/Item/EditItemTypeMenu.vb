Module EditItemTypeMenu
    Private Sub ShowStatus(itemType As ItemType)
        AnsiConsole.MarkupLine($"Id: {itemType.Id}")
        AnsiConsole.MarkupLine($"Name: {itemType.Name}")
    End Sub
    Private Function CreatePrompt(itemType As ItemType) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        If itemType.CanDestroy Then
            prompt.AddChoice(DestroyText)
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
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleChangeName(itemType As ItemType)
        Dim newName = AnsiConsole.Ask(Of String)("New Item Type Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            itemType.Name = newName
        End If
    End Sub
End Module
