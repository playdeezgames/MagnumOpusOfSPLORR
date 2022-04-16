Module EditLocationMenu
    Private Const ToggleWinningLocation = "Toggle Winning Location"
    Private Const RoutesText = "Routes..."
    Private Const ChooseSpawnerText = "Choose Spawner..."
    Private Const RemoveSpawnerText = "Remove Spawner"
    Private Sub ShowStatus(location As Location)
        If location.IsWinningLocation Then
            AnsiConsole.MarkupLine("[aqua]This is a winning location[/]")
        End If
        AnsiConsole.MarkupLine($"Id: {location.Id}")
        AnsiConsole.MarkupLine($"Name: {location.Name}")
        Dim characters = location.Characters
        If characters.Any Then
            AnsiConsole.MarkupLine($"Characters: {String.Join(", ", characters.Select(Function(x) x.UniqueName))}")
        End If
        Dim spawner = location.Spawner
        If spawner IsNot Nothing Then
            AnsiConsole.MarkupLine($"Spawner: {spawner.Name}")
            AnsiConsole.MarkupLine($"Cooldown: {spawner.Cooldown}/{spawner.Spawner.Cooldown}")
        End If
        EditRoutesMenu.ShowStatus(location.Routes)
        EditInventoryMenu.ShowStatus(location.Inventory)
    End Sub
    Private Function CreatePrompt(location As Location) As SelectionPrompt(Of String)
        Dim prompt = New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        prompt.AddChoice(ToggleWinningLocation)
        prompt.AddChoice(InventoryText)
        prompt.AddChoice(RoutesText)
        If location.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        prompt.AddChoice(ChooseSpawnerText)
        If location.Spawner IsNot Nothing Then
            prompt.AddChoice(RemoveSpawnerText)
        End If
        Return prompt
    End Function
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(location)
            Select Case AnsiConsole.Prompt(CreatePrompt(location))
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(location)
                Case ToggleWinningLocation
                    HandlToggleWinningLocation(location)
                Case InventoryText
                    EditInventoryMenu.Run($"Location Inventory:", location.Inventory)
                Case DestroyText
                    location.Destroy()
                    done = True
                Case RoutesText
                    EditRoutesMenu.Run(location)
                Case ChooseSpawnerText
                    HandleChooseSpawnerText(location)
                Case RemoveSpawnerText
                    location.RemoveSpawner()
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleChooseSpawnerText(location As Location)
        Dim spawner = ChooseSpawner("Which spawner?", True)
        If spawner IsNot Nothing Then
            Dim cooldown = AnsiConsole.Ask(Of Long)("[olive]Cooldown value?[/]")
            location.SetSpawner(spawner, cooldown)
        End If
    End Sub

    Private Sub HandlToggleWinningLocation(location As Location)
        location.IsWinningLocation = Not location.IsWinningLocation
    End Sub
    Private Sub HandleChangeName(location As Location)
        Dim newName = AnsiConsole.Ask(Of String)("New Location Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
