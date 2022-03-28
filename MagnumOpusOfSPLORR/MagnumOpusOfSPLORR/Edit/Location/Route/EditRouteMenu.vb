Module EditRouteMenu
    Private Sub ShowStatus(route As Route)
        AnsiConsole.MarkupLine($"Id: {route.Id}")
        AnsiConsole.MarkupLine($"From: {route.FromLocation.UniqueName}")
        AnsiConsole.MarkupLine($"To: {route.ToLocation.UniqueName}")
        AnsiConsole.MarkupLine($"Direction: {route.Direction.UniqueName}")
        If route.Barriers.Any Then
            AnsiConsole.MarkupLine($"Barriers: {String.Join(", ", route.Barriers.Select(Function(barrier) barrier.UniqueName))}")
        End If
    End Sub
    Private Function CreatePrompt(route As Route) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        If route.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(route As Route)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(route)
            Select Case AnsiConsole.Prompt(CreatePrompt(route))
                Case GoBackText
                    done = True
                Case DestroyText
                    route.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
