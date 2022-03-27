Imports MOOS.Game
Imports Spectre.Console

Module EditItemTypeMenu
    Sub Run(itemType As ItemType)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {itemType.Id}")
            AnsiConsole.MarkupLine($"Name: {itemType.Name}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            If itemType.CanDestroy Then
                prompt.AddChoice(DestroyText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
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
