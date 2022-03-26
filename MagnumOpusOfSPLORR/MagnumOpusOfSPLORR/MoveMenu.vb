Imports MOOS.Game
Imports Spectre.Console

Module MoveMenu
    Friend Sub Run(character As PlayerCharacter, routes As List(Of Route))
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which way?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each route In routes
            prompt.AddChoice(route.Direction.Name)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Dim chosenRoute = routes.SingleOrDefault(Function(r) r.Direction.Name = answer)
        If chosenRoute IsNot Nothing Then
            character.Location = chosenRoute.ToLocation
        End If
    End Sub
End Module
