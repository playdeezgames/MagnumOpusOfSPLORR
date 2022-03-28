Module EditRouteBarrierMenu
    Private Function CreatePrompt(route As Route, barrier As Barrier) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{barrier.UniqueName} on {route.UniqueName}:[/]"}
        prompt.AddChoice(NeverMindText)
        prompt.AddChoice(RemoveText)
        Return prompt
    End Function
    Sub Run(route As Route, barrier As Barrier)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt = CreatePrompt(route, barrier)
            Select Case AnsiConsole.Prompt(prompt)
                Case NeverMindText
                    done = True
                Case RemoveText
                    done = True
                    route.RemoveBarrier(barrier)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
