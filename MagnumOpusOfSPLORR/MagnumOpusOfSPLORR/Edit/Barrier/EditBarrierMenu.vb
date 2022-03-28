Module EditBarrierMenu
    Private Sub ShowStatus(barrier As Barrier)
        AnsiConsole.MarkupLine($"Id: {barrier.Id}")
        AnsiConsole.MarkupLine($"Item Type: {barrier.ItemType.UniqueName}")
        AnsiConsole.MarkupLine($"Destroys Item: {barrier.DestroysItem}")
        AnsiConsole.MarkupLine($"Self Destructs: {barrier.SelfDestructs}")
    End Sub
    Private Function CreatePrompt(barrier As Barrier) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What now?[/]"}
        prompt.AddChoice(GoBackText)
        If barrier.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(barrier As Barrier)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(barrier)
            Select Case AnsiConsole.Prompt(CreatePrompt(barrier))
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
