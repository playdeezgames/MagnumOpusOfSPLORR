Module EditSpawnerMenu
    Private Sub ShowStatus(spawner As Spawner)
        AnsiConsole.MarkupLine($"Id: {spawner.Id}")
        AnsiConsole.MarkupLine($"Name: {spawner.Name}")
        AnsiConsole.MarkupLine($"Spawn Nothing Weight: {spawner.SpawnNothingWeight}")
        AnsiConsole.MarkupLine($"Cooldown: {spawner.Cooldown}")
    End Sub
    Private Function CreatePrompt(spawner As Spawner) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(GoBackText)
        If spawner.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Friend Sub Run(spawner As Spawner)
        Dim done = False
        While Not done
            ShowStatus(spawner)
            Select Case AnsiConsole.Prompt(CreatePrompt(spawner))
                Case GoBackText
                    done = True
                Case DestroyText
                    HandleDestroy(spawner)
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleDestroy(spawner As Spawner)
        spawner.Destroy()
    End Sub
End Module
