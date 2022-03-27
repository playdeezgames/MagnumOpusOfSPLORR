Module EditLocationRouteMenu
    Private Const AddRouteText = "Add Route..."
    Private Const RemoveRouteText = "Remove Route..."
    Friend Sub ShowStatus(routes As List(Of Route))
        If routes.Any Then
            AnsiConsole.MarkupLine($"Routes: {String.Join(", ", routes.Select(Function(x) x.UniqueName))}")
        End If
    End Sub
    Private Function CreatePrompt(routes As List(Of Route)) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "Routes:"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(AddRouteText)
        If routes.Any Then
            prompt.AddChoice(RemoveRouteText)
        End If
        Return prompt
    End Function
    Sub Run(location As Location)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(location.Routes)
            Select Case AnsiConsole.Prompt(CreatePrompt(location.Routes))
                Case GoBackText
                    done = True
                Case AddRouteText
                    HandleAddRoute(location)
                Case RemoveRouteText
                    HandleRemoveRoute(location)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
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
End Module
