Module EditLocationMenu
    Private Const ToggleWinningLocation = "Toggle Winning Location"
    Private Const RoutesText = "Routes..."
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
        EditLocationRouteMenu.ShowStatus(location.Routes)
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
                    EditLocationRouteMenu.Run(location)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
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
