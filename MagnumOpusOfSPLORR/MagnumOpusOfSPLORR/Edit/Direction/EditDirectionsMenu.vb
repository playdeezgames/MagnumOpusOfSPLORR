Imports MOOS.Game
Imports Spectre.Console

Module EditDirectionsMenu
    Private Const CreateDirectionText = "Create Direction"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Edit Directions:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(CreateDirectionText)
        For Each direction In Directions.AllDirections
            prompt.AddChoices(direction.UniqueName)
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
                Case CreateDirectionText
                    EditDirectionMenu.Run(Directions.CreateDirection("New Direction"))
                Case Else
                    EditDirectionMenu.Run(FindDirectionByUniqueName(answer))
            End Select
        End While
    End Sub
End Module
