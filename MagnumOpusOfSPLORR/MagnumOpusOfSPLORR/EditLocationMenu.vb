Imports MOOS.Game
Imports Spectre.Console

Module EditLocationMenu
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.MarkupLine($"Id: {location.Id}")
            AnsiConsole.MarkupLine($"Name: {location.Name}")
            Dim characters = location.Characters
            If characters.Any Then
                AnsiConsole.MarkupLine($"Characters: {String.Join(", ", characters.Select(Function(x) x.UniqueName))}")
            End If
            Dim prompt = New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            prompt.AddChoice(GoBackText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
            End Select
        End While
    End Sub
End Module
