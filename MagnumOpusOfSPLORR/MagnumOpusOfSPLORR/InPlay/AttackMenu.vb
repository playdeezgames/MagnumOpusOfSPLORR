Module AttackMenu
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Attack whom?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each enemy In character.Location.Enemies(character)
            prompt.AddChoice(enemy.Name)
        Next
        Return prompt
    End Function
    Function Run(character As Character) As Boolean
        AnsiConsole.Clear()
        Dim answer = AnsiConsole.Prompt(CreatePrompt(character))
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                HandleAttack(character, character.Location.Enemies(character).First(Function(x) x.Name = answer))
        End Select
        If character.IsDead Then
            HandleDeath(character)
        End If
        Return character.IsDead
    End Function
    Private Sub HandleDeath(character As Character)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Yer dead!")
        AnsiConsole.MarkupLine($"Game over!")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
    Private Function KillEnemy(character As Character, enemy As Character) As Boolean
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"You attack {enemy.Name}!")
        Dim attack = character.RollAttack()
        AnsiConsole.MarkupLine($"You roll {attack} for attack!")
        Dim defend = enemy.RollDefense()
        AnsiConsole.MarkupLine($"{enemy.Name} rolls {defend} for defense!")
        If attack > defend Then
            Dim damage = attack - defend
            AnsiConsole.MarkupLine($"You hit for {damage} damage!")
            enemy.Wounds += damage
            If enemy.IsDead Then
                AnsiConsole.MarkupLine($"You kill {enemy.Name}!")
                enemy.Kill()
                Return True
            End If
        Else
            AnsiConsole.MarkupLine("You miss!")
        End If
        Return False
    End Function
    Private Sub HandleAttack(character As Character, enemy As Character)
        Dim killedEnemy = KillEnemy(character, enemy)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
        If Not killedEnemy Then
            HandleCounterAttack(character, enemy)
        End If
    End Sub
    Private Sub HandleCounterAttack(character As Character, enemy As Character)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{enemy.Name} attacks you!")
        Dim attack = enemy.RollAttack()
        AnsiConsole.MarkupLine($"{enemy.Name} rolls {attack} for attack!")
        Dim defend = character.RollDefense()
        AnsiConsole.MarkupLine($"You roll {defend} for defense!")
        If attack > defend Then
            Dim damage = attack - defend
            AnsiConsole.MarkupLine($"{enemy.Name} hits for {damage} damage!")
            character.Wounds += damage
            If character.IsDead Then
                AnsiConsole.MarkupLine($"{enemy.Name} kills you!")
                character.Kill()
            End If
        Else
            AnsiConsole.MarkupLine($"{enemy.Name} misses!")
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
