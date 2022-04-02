﻿Module EditCharacterTypeMenu
    Private Const ChangeHealthText = "Change Health..."
    Private Sub ShowStatus(characterType As CharacterType)
        AnsiConsole.MarkupLine($"Id: {characterType.Id}")
        AnsiConsole.MarkupLine($"Name: {characterType.Name}")
        AnsiConsole.MarkupLine($"Health: {characterType.Health}")
    End Sub
    Private Function CreatePrompt(characterType As CharacterType) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        prompt.AddChoice(ChangeHealthText)
        If characterType.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(characterType As CharacterType)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(characterType)
            Select Case AnsiConsole.Prompt(CreatePrompt(characterType))
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(characterType)
                Case ChangeHealthText
                    HandleChangeHealth(characterType)
                Case DestroyText
                    characterType.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleChangeHealth(characterType As CharacterType)
        characterType.Health = AnsiConsole.Ask(Of Long)("[olive]New Health:[/]")
    End Sub
    Private Sub HandleChangeName(characterType As CharacterType)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Type Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            characterType.Name = newName
        End If
    End Sub
End Module
