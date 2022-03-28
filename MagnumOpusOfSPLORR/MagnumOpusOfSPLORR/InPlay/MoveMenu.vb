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
            If character.CanPass(chosenRoute) Then
                character.Pass(chosenRoute)
            Else
                ShowPassRequirements(chosenRoute)
            End If
        End If
    End Sub
    Private Sub ShowPassRequirements(route As Route)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("You cannot go that way without:")
        Dim requirements = route.PassRequirements
        For Each requirement In requirements
            AnsiConsole.MarkupLine($"{requirement.Key.Name} (x{requirement.Value})")
        Next
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]That sucks![/]"}
        prompt.AddChoices("Yeah it do.")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
