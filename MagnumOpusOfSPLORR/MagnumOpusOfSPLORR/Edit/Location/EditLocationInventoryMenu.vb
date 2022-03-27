Imports MOOS.Game
Imports Spectre.Console

Module EditLocationInventoryMenu
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Inventory of {location.UniqueName}:[/]"}
            prompt.AddChoice(GoBackText)
            'TODO: create item
            For Each item In location.Inventory.Items

            Next
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
