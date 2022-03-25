﻿Imports MOOS.Game
Imports Spectre.Console

Module EditCharacterMenu
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
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleChangeName(character As Character)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub
End Module
