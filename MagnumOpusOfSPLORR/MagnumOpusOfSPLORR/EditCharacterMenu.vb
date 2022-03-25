Imports MOOS.Game
Imports Spectre.Console

Module EditCharacterMenu
    Private Const ChangeLocationText = "Change Location"
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {character.Id}")
            AnsiConsole.MarkupLine($"Name: {character.Name}")
            AnsiConsole.MarkupLine($"Location: {character.Location.UniqueName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeLocationText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(character)
                Case ChangeLocationText
                    HandleChangeLocation(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleChangeLocation(character As Character)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which location?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each location In AllLocations
            prompt.AddChoices(location.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                character.Location = AllLocations.Single(Function(x) x.UniqueName = answer)
        End Select
    End Sub

    Private Sub HandleChangeName(character As Character)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub
End Module
