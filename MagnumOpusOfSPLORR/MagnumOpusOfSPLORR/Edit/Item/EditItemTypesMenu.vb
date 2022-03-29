Imports MOOS.Game
Imports Spectre.Console

Module EditItemTypesMenu
    Private Const CreateItemTypeText = "Create Item Type"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Item Types:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateItemTypeText)
        For Each itemType In AllItemTypes
            prompt.AddChoice(itemType.UniqueName)
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
                Case CreateItemTypeText
                    HandleCreateItemType()
                Case Else
                    EditItemTypeMenu.Run(FindItemTypeByUniqueName(answer))
            End Select
        End While
    End Sub

    Private Sub HandleCreateItemType()
        EditItemTypeMenu.Run(ItemTypes.CreateItemType("New Item Type"))
    End Sub
End Module
