Imports MOOS.Game
Imports Spectre.Console

Module EditCharacterMenu
    Private Const ChangeLocationText = "Change Location"
    Private Const AssignPlayerCharacterText = "Make this the player character"
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            If character.IsPlayerCharacter Then
                AnsiConsole.MarkupLine("[aqua]This is the player character[/]")
            End If
            AnsiConsole.MarkupLine($"Id: {character.Id}")
            AnsiConsole.MarkupLine($"Name: {character.Name}")
            AnsiConsole.MarkupLine($"Location: {character.Location.UniqueName}")
            AnsiConsole.MarkupLine($"Items: {String.Join(
                    ", ",
                    character.Inventory.StackedItems.Select(Function(x) $"{x.Key.Name}(x{x.Value.Count})"))}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(ChangeNameText)
            prompt.AddChoice(ChangeLocationText)
            prompt.AddChoice(AddItemText)
            If Not character.Inventory.IsEmpty Then
                prompt.AddChoice(RemoveItemText)
            End If
            If Not character.IsPlayerCharacter Then
                prompt.AddChoice(AssignPlayerCharacterText)
            End If
            If character.CanDestroy Then
                prompt.AddChoice(DestroyText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(character)
                Case ChangeLocationText
                    HandleChangeLocation(character)
                Case AssignPlayerCharacterText
                    character.SetAsPlayerCharacter()
                Case AddItemText
                    HandleAddItem(character)
                Case RemoveItemText
                    HandlRemoveItem(character)
                Case DestroyText
                    character.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandlRemoveItem(character As Character)
        Dim item = CommonMenu.ChooseItemNameFromInventory("Remove which item?", True, character.Inventory.Items)
        If item IsNot Nothing Then
            item.Destroy()
        End If
    End Sub

    Private Sub HandleAddItem(character As Character)
        Dim itemType = CommonMenu.ChooseItemType("", True)
        If itemType IsNot Nothing Then
            Items.CreateItem(itemType, character.Inventory)
        End If
    End Sub

    Private Sub HandleChangeLocation(character As Character)
        Dim newLocation = ChooseLocation("Which Location", True)
        If newLocation IsNot Nothing Then
            character.Location = newLocation
        End If
    End Sub
    Private Sub HandleChangeName(character As Character)
        Dim newName = AnsiConsole.Ask(Of String)("New Character Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            character.Name = newName
        End If
    End Sub
End Module
