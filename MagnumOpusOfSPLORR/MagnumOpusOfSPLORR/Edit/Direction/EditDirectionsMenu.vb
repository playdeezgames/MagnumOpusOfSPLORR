Imports MOOS.Game
Imports Spectre.Console

Module EditDirectionsMenu
    Private Const CreateDirectionText = "Create Direction"
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Edit Directions:[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(CreateDirectionText)
            For Each direction In Directions.AllDirections
                prompt.AddChoices(direction.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case CreateDirectionText
                    EditDirectionMenu.Run(Directions.CreateDirection("New Direction"))
                Case Else
                    EditDirectionMenu.Run(AllDirections.Single(Function(x) x.UniqueName = answer))
            End Select
        End While
    End Sub
End Module
