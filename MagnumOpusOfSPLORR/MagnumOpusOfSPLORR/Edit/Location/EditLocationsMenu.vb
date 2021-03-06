Module EditLocationsMenu
    Private Const CreateLocationText = "Create Location"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Locations:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateLocationText)
        For Each location In Locations.AllLocations
            prompt.AddChoice(location.UniqueName)
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
                Case CreateLocationText
                    HandleCreateLocation()
                Case Else
                    EditLocationMenu.Run(FindLocationByUniqueName(answer))
            End Select
        End While
    End Sub
    Private Sub HandleCreateLocation()
        Dim name = AnsiConsole.Ask("[olive]New Location Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(name) Then
            EditLocationMenu.Run(CreateLocation(name))
        End If
    End Sub
End Module
