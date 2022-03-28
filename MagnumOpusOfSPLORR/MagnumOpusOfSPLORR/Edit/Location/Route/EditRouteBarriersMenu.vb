Module EditRouteBarriersMenu
    Private Const AddBarrierText = "Add Barrier..."
    Private Function CreatePrompt(route As Route) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Barriers for {route.UniqueName}:[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(AddBarrierText)
        For Each barrier In route.Barriers
            prompt.AddChoice(barrier.UniqueName)
        Next
        Return prompt
    End Function
    Sub Run(route As Route)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim answer = AnsiConsole.Prompt(CreatePrompt(route))
            Select Case answer
                Case GoBackText
                    done = True
                Case AddBarrierText
                    HandleAddBarrier(route)
                Case Else
                    EditRouteBarrierMenu.Run(route, AllBarriers.Single(Function(b) b.UniqueName = answer))
            End Select
        End While
    End Sub
    Private Sub HandleAddBarrier(route As Route)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Add which barrier?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each barrier In Barriers.AllBarriers 'TODO: leave out the barriers that are already associated
            prompt.AddChoice(barrier.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Dim result = AllBarriers.SingleOrDefault(Function(b) b.UniqueName = answer)
        If result IsNot Nothing Then
            route.AddBarrier(result)
        End If
    End Sub
End Module
