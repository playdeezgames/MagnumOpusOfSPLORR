Module EditSpawnersMenu
    Private Const CreateSpawnerText = "Create Spawner"
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Spawners:[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(CreateSpawnerText)
            For Each spawner In AllSpawners
                prompt.AddChoice(spawner.UniqueName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case CreateSpawnerText
                    HandleCreateSpawner()
                Case Else
                    EditSpawnerMenu.Run(AllSpawners.Single(Function(x) x.UniqueName = answer))
            End Select
        End While
    End Sub
    Private Sub HandleCreateSpawner()
        Dim spawnerName = AnsiConsole.Ask("[olive]Spawner Name:[/]", "")
        If Not String.IsNullOrWhiteSpace(spawnerName) Then
            Dim spawnNothingWeight = AnsiConsole.Ask(Of Long)("[olive]Spawn Nothing Weight:[/]")
            Dim cooldown = AnsiConsole.Ask(Of Long)("[olive]Cooldown:[/]")
            EditSpawnerMenu.Run(CreateSpawner(spawnerName, spawnNothingWeight, cooldown))
        End If
    End Sub
End Module
