﻿Module EditLocationMenu
    Private Const AddRouteText = "Add Route..."
    Private Const RemoveRouteText = "Remove Route..."
    Private Const ToggleWinningLocation = "Toggle Winning Location"
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
        Dim routes = location.Routes
        If routes.Any Then
            AnsiConsole.MarkupLine($"Routes: {String.Join(", ", routes.Select(Function(x) x.UniqueName))}")
        End If
        Dim itemStacks = location.Inventory.StackedItems
        If itemStacks.Any Then
            AnsiConsole.MarkupLine($"Items: {String.Join(
                    ", ",
                    itemStacks.Select(Function(x) $"{x.Key.Name}(x{x.Value.Count})"))}")
        End If
    End Sub
    Private Function CreatePrompt(location As Location) As SelectionPrompt(Of String)
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
        prompt.AddChoice(AddItemText)
        If Not location.Inventory.IsEmpty Then
            prompt.AddChoice(RemoveItemText)
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
                Case AddRouteText
                    HandleAddRoute(location)
                Case RemoveRouteText
                    HandleRemoveRoute(location)
                Case ToggleWinningLocation
                    HandlToggleWinningLocation(location)
                Case AddItemText
                    HandleAddItem(location)
                Case RemoveItemText
                    HandleRemoveItem(location)
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
    Private Sub HandleRemoveItem(location As Location)
        Dim item = CommonMenu.ChooseItemNameFromInventory("Remove which item?", True, location.Inventory)
        If item IsNot Nothing Then
            item.Destroy()
        End If
    End Sub
    Private Sub HandleAddItem(location As Location)
        Dim itemType = CommonMenu.ChooseItemType("", True)
        If itemType IsNot Nothing Then
            Items.CreateItem(itemType, location.Inventory)
        End If
    End Sub
End Module
