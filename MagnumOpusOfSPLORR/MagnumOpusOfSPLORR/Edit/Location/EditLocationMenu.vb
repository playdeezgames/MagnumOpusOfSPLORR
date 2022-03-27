Imports MOOS.Game
Imports Spectre.Console

Module EditLocationMenu
    Private Const AddRouteText = "Add Route..."
    Private Const RemoveRouteText = "Remove Route..."
    Private Const ToggleWinningLocation = "Toggle Winning Location"
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            If location.IsWinningLocation Then
                AnsiConsole.MarkupLine("[aqua]This is a winning location[/]")
            End If
            AnsiConsole.MarkupLine($"Id: {location.Id}")
            AnsiConsole.MarkupLine($"Name: {location.Name}")
            Dim characters = location.Characters
            If characters.Any Then
                AnsiConsole.MarkupLine($"Characters: {String.Join(", ", characters.Select(Function(x) x.UniqueName))}")
            End If
            Dim routes = location.Routes
            If routes.Any Then
                AnsiConsole.MarkupLine($"Routes: {String.Join(", ", routes.Select(Function(x) x.UniqueName))}")
            End If
            Dim prompt = New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ToggleWinningLocation)
            If location.AvailableDirections.Any Then
                prompt.AddChoice(AddRouteText)
            End If
            If location.Routes.Any Then
                prompt.AddChoice(RemoveRouteText)
            End If
            If location.CanDestroy Then
                prompt.AddChoice(DestroyText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(location)
                Case AddRouteText
                    HandleAddRoute(location)
                Case RemoveRouteText
                    HandleRemoveRoute(location)
                Case ToggleWinningLocation
                    HandlToggleWinningLocation(location)
                Case DestroyText
                    location.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandlToggleWinningLocation(location As Location)
        location.IsWinningLocation = Not location.IsWinningLocation
    End Sub

    Private Sub HandleRemoveRoute(location As Location)
        Dim routes = location.Routes
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "Remove which route?"}
        prompt.AddChoice(NeverMindText)
        For Each route In routes
            prompt.AddChoice(route.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                routes.Single(Function(r) r.UniqueName = answer).Destroy()
        End Select
    End Sub
    Private Sub HandleAddRoute(fromLocation As Location)
        Dim toLocation = CommonMenu.ChooseLocation("Route to where?", False)
        Dim direction = CommonMenu.ChooseDirection("Direction of route?", fromLocation.AvailableDirections, False) 'TODO: limit to available directions!
        Routes.CreateRoute(fromLocation, toLocation, direction)
    End Sub
    Private Sub HandleChangeName(location As Location)
        Dim newName = AnsiConsole.Ask(Of String)("New Location Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
