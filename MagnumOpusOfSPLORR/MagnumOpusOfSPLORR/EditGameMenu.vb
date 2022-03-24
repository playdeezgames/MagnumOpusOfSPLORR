Imports Spectre.Console
Module EditGameMenu
    Private Const LocationsText = "Locations..."
    Sub Run()
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Edit Menu:[/]"
                }
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(LocationsText)
            Select Case AnsiConsole.Prompt(prompt)
                Case LocationsText
                    EditLocationsMenu.Run()
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
