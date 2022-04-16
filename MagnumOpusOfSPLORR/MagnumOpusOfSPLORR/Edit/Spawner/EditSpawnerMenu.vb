Module EditSpawnerMenu
    Private Const ChangeSpawnNothingWeightText = "Change Spawn Nothing Weight..."
    Private Const ChangeCooldownText = "Change Cooldown..."
    Private Sub ShowStatus(spawner As Spawner)
        AnsiConsole.MarkupLine($"Id: {spawner.Id}")
        AnsiConsole.MarkupLine($"Name: {spawner.Name}")
        AnsiConsole.MarkupLine($"Spawn Nothing Weight: {spawner.SpawnNothingWeight}")
        AnsiConsole.MarkupLine($"Cooldown: {spawner.Cooldown}")
        ShowCharacterTypes(spawner)
    End Sub

    Private Sub ShowCharacterTypes(spawner As Spawner)
        Dim characterTypes = spawner.CharacterTypes
        If characterTypes.any Then
            AnsiConsole.MarkupLine("Character Types:")
            For Each characterType In characterTypes
                AnsiConsole.MarkupLine($"- {characterType.Key.Name}({characterType.Value})")
            Next
        End If
    End Sub

    Private Function CreatePrompt(spawner As Spawner) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeSpawnNothingWeightText)
        prompt.AddChoice(ChangeCooldownText)
        If spawner.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function

    Friend Sub Run(spawner As Spawner)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(spawner)
            Select Case AnsiConsole.Prompt(CreatePrompt(spawner))
                Case GoBackText
                    done = True
                Case DestroyText
                    HandleDestroy(spawner)
                    done = True
                Case ChangeCooldownText
                    HandleChangeCooldown(spawner)
                Case ChangeSpawnNothingWeightText
                    HandleChangeSpawnNothingWeight(spawner)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleChangeSpawnNothingWeight(spawner As Spawner)
        spawner.SpawnNothingWeight = AnsiConsole.Ask(Of Long)("[olive]New Spawn Nothing Weight:[/]")
    End Sub

    Private Sub HandleChangeCooldown(spawner As Spawner)
        spawner.Cooldown = AnsiConsole.Ask(Of Long)("[olive]New Cooldown:[/]")
    End Sub

    Private Sub HandleDestroy(spawner As Spawner)
        If spawner.CanDestroy Then
            spawner.Destroy()
        End If
    End Sub
End Module
