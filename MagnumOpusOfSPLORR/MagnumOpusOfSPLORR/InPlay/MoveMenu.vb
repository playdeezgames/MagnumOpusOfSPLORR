Imports MOOS.Game
Imports Spectre.Console

Module MoveMenu
    Friend Sub Run(character As PlayerCharacter, routes As List(Of Route))
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Go which way?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each route In routes
            prompt.AddChoice(route.Direction.Name)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Dim chosenRoute = routes.SingleOrDefault(Function(r) r.Direction.Name = answer)
        If chosenRoute IsNot Nothing Then
            chosenRoute.Pass(character)
        End If
    End Sub
End Module
