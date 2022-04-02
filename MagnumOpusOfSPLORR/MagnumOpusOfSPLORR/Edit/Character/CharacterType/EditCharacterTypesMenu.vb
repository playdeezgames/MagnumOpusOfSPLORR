Module EditCharacterTypesMenu
    Private Const CreateCharacterTypeText = "Create Character Type"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Character Types:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateCharacterTypeText)
        For Each characterType In AllCharacterTypes
            prompt.AddChoice(characterType.UniqueName)
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
                Case CreateCharacterTypeText
                    HandleCreateCharacterType()
                Case Else
                    EditCharacterTypeMenu.Run(FindCharacterTypeByUniqueName(answer))
            End Select
        End While
    End Sub

    Private Sub HandleCreateCharacterType()
        Dim name = AnsiConsole.Ask("[olive]New Character Type Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(name) Then
            Dim health = AnsiConsole.Ask(Of Long)("[olive]Health:[/]")
            EditCharacterTypeMenu.Run(CreateCharacterType(name, health))
        End If
    End Sub
End Module
