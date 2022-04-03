Module Embark
    Private Const MainMenuText = "Main Menu"
    Private Const EndTestText = "End Test"
    Private Const GoText = "Go..."
    Private Const AttackText = "Attack..."
    Private Const PickUpText = "Pick Up..."
    Private Const HealText = "Heal..."
    Private Sub ShowStatus(character As Character)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You exist!")
        Dim location = character.Location
        AnsiConsole.MarkupLine($"Location: {location.Name}")
        ShowRoutes(location)
        ShowInventory(location)
        ShowCharacters(character)
    End Sub
    Private Sub ShowCharacters(character As Character)
        Dim characters = character.Location.Enemies(character)
        If characters.Any Then
            AnsiConsole.MarkupLine($"Enemies: {String.Join(", ", characters.Select(Function(c) $"{c.Name}"))}")
        End If
    End Sub
    Private Sub ShowInventory(location As Location)
        Dim locationItemStacks = location.Inventory.StackedItems
        If locationItemStacks.Any Then
            AnsiConsole.MarkupLine($"Items: {String.Join(", ", locationItemStacks.Select(Function(s) $"{s.Key.Name}(x{s.Value.Count})"))}")
        End If
    End Sub
    Private Sub ShowRoutes(location As Location)
        Dim routes = location.Routes
        If routes.Any Then
            AnsiConsole.MarkupLine($"Exits: {String.Join(", ", routes.Select(Function(r) r.Direction.Name))}")
        End If
    End Sub
    Private Function CreatePrompt(character As Character, isTest As Boolean) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Now what?[/]"
                }
        If character.Location.Routes.Any Then
            prompt.AddChoice(GoText)
        End If
        If character.Location.Enemies(character).Any Then
            prompt.AddChoice(AttackText)
        End If
        If Not character.Location.Inventory.IsEmpty Then
            prompt.AddChoice(PickUpText)
        End If
        If Not character.Inventory.IsEmpty Then
            prompt.AddChoice(InventoryText)
        End If
        If character.CanHeal Then
            prompt.AddChoice(HealText)
        End If
        If isTest Then
            prompt.AddChoice(EndTestText)
        Else
            prompt.AddChoice(MainMenuText)
        End If
        Return prompt
    End Function
    Sub Run(isTest As Boolean)
        Dim tempfile = $"{Guid.NewGuid}.db"
        If isTest Then
            Store.Save(tempfile)
        End If
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            If isTest Then
                AnsiConsole.MarkupLine("[aqua]***TESTING***[/]")
            End If
            Dim character As New PlayerCharacter()
            ShowStatus(character)
            If character.DidWin Then
                done = True
                HandleWin(character)
            Else
                Select Case AnsiConsole.Prompt(CreatePrompt(character, isTest))
                    Case MainMenuText
                        done = True
                    Case EndTestText
                        done = AnsiConsole.Confirm("[red]Are you sure you want to end the test?[/]", False)
                    Case GoText
                        MoveMenu.Run(character, character.Location.Routes)
                    Case PickUpText
                        HandlePickUp(character)
                    Case HealText
                        HealMenu.Run(character)
                    Case AttackText
                        done = AttackMenu.Run(character)
                    Case InventoryText
                        InventoryMenu.Run(character)
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
        End While
        If isTest Then
            Store.Load(tempfile)
        End If
    End Sub
    Private Sub HandlePickUp(character As PlayerCharacter)
        Dim itemType As ItemType = CommonMenu.ChooseItemTypeNameFromInventory("Pick up what?", True, character.Location.Inventory)
        If itemType IsNot Nothing Then
            Dim items = character.Location.Inventory.StackedItems.Single(Function(x) x.Key = itemType).Value
            Dim quantity = If(items.Count = 1, 1, AnsiConsole.Ask(Of Integer)("[olive]How Many?[/]", 0))
            If quantity > 0 Then
                items = items.Take(quantity).ToList()
                For Each item In items
                    character.Inventory.Add(item)
                Next
            End If
        End If
    End Sub
    Private Sub HandleWin(character As PlayerCharacter)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("You win!")
        Dim counter = character.Counters.SingleOrDefault(Function(x) x.CounterType = CounterType.Movement)
        If counter IsNot Nothing Then
            AnsiConsole.MarkupLine($"Moves: {counter.Value}")
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
