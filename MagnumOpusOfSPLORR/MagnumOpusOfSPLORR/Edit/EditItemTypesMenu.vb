Imports Spectre.Console

Module EditItemTypesMenu
    Private Const CreateItemTypeText = "Create Item Type"
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Item Types:[/]"}
            prompt.AddChoice(GoBackText)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
