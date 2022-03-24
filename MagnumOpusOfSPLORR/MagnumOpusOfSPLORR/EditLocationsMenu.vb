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
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
            End Select
        End While

    End Sub
End Module
