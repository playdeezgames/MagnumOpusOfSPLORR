Module EditRoutesMenu
    Private Const AddRouteText = "Add Route..."
    Friend Sub ShowStatus(routes As List(Of Route))
        If routes.Any Then
            AnsiConsole.MarkupLine($"Routes: {String.Join(", ", routes.Select(Function(x) x.UniqueName))}")
        End If
    End Sub
    Private Function CreatePrompt(routes As List(Of Route)) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "Routes:"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(AddRouteText)
        For Each route In routes
            prompt.AddChoice(route.UniqueName)
        Next
        Return prompt
    End Function
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(location.Routes)
            Dim answer = AnsiConsole.Prompt(CreatePrompt(location.Routes))
            Select Case answer
                Case GoBackText
                    done = True
                Case AddRouteText
                    HandleAddRoute(location)
                Case Else
                    EditRouteMenu.Run(location.Routes.Single(Function(r) r.UniqueName = answer))
            End Select
        End While
    End Sub
    Private Sub HandleAddRoute(fromLocation As Location)
        Dim toLocation = CommonMenu.ChooseLocation("Route to where?", False)
        Dim direction = CommonMenu.ChooseDirection("Direction of route?", fromLocation.AvailableDirections, False) 'TODO: limit to available directions!
        Routes.CreateRoute(fromLocation, toLocation, direction)
    End Sub
End Module
