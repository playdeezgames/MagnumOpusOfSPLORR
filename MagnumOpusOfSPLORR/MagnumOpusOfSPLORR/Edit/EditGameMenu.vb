Imports Spectre.Console
Module EditGameMenu
    Private Const LocationsText = "Locations..."
    Private Const CharactersText = "Characters..."
    Private Const DirectionsText = "Directions..."
    Private Const ItemTypesText = "Item Types..."
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Edit Menu:[/]"
                }
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(DirectionsText)
            prompt.AddChoice(LocationsText)
            prompt.AddChoice(CharactersText)
            prompt.AddChoice(ItemTypesText)
            Select Case AnsiConsole.Prompt(prompt)
                Case LocationsText
                    EditLocationsMenu.Run()
                Case CharactersText
                    EditCharactersMenu.Run()
                Case DirectionsText
                    EditDirectionsMenu.Run()
                Case ItemTypesText
                    EditItemTypesMenu.Run()
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
