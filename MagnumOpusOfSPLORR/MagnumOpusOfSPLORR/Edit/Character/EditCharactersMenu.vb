Module EditCharactersMenu
    Private Const CreateCharacterText = "Create Character"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Characters:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateCharacterText)
        For Each character In AllCharacters
            prompt.AddChoice(character.UniqueName)
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
                Case CreateCharacterText
                    HandleCreateCharacter()
                Case Else
                    EditCharacterMenu.Run(AllCharacters.Single(Function(x) x.UniqueName = answer))
            End Select
        End While
    End Sub
    Private Sub HandleCreateCharacter()
        Dim location = ChooseLocation("What Location?", False)
        Dim characterType = ChooseCharacterType("What Character Type?", False)
        Dim name = AnsiConsole.Ask("[olive]New Character Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(name) Then
            EditCharacterMenu.Run(Characters.CreateCharacter(name, location, characterType))
        End If
    End Sub
End Module
