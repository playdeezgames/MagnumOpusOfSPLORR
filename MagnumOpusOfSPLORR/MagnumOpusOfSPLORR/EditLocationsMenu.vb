Imports MOOS.Game
Imports Spectre.Console
Module EditLocationsMenu
    Sub Run()
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Locations:[/]"}
            prompt.AddChoice(GoBackText)
            For Each location In Locations.AllLocations
                prompt.AddChoice(location.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case Else
                    EditLocationMenu.Run(AllLocations.Single(Function(x) x.UniqueName = answer))
            End Select
        End While

    End Sub
End Module
