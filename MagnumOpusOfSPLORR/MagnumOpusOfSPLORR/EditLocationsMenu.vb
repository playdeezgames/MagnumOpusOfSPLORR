Imports MOOS.Game
Imports Spectre.Console
Module EditLocationsMenu
    Private Const CreateLocationText = "Create Location"
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Locations:[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(CreateLocationText)
            For Each location In Locations.AllLocations
                prompt.AddChoice(location.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case CreateLocationText
                    EditLocationMenu.Run(CreateLocation("New Location"))
                Case Else
                    EditLocationMenu.Run(AllLocations.Single(Function(x) x.UniqueName = answer))
            End Select
        End While

    End Sub
End Module
