Imports MOOS.Game
Imports Spectre.Console

Module EditLocationMenu
    Private Const AddRouteText = "Add Route..."
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
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
            prompt.AddChoice(AddRouteText)
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
                Case DestroyText
                    location.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleAddRoute(fromLocation As Location)
        Dim toLocation = CommonMenu.ChooseLocation("Route to where?", False)
        Dim direction = CommonMenu.ChooseDirection("Direction of route?", False)
        Routes.CreateRoute(fromLocation, toLocation, direction)
    End Sub
    Private Sub HandleChangeName(location As Location)
        Dim newName = AnsiConsole.Ask(Of String)("New Location Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            location.Name = newName
        End If
    End Sub
End Module
