Imports MOOS.Game
Imports Spectre.Console

Module EditItemTypesMenu
    Private Const CreateItemTypeText = "Create Item Type"
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Item Types:[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(CreateItemTypeText)
            For Each itemType In AllItemTypes
                prompt.AddChoice(itemType.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case CreateItemTypeText
                    HandleCreateItemType()
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleCreateItemType()
        EditItemTypeMenu.Run(ItemTypes.CreateItemType("New Item Type"))
    End Sub
End Module
