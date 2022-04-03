Module AttackMenu
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Attack whom?[/]"}
        prompt.AddChoice(NeverMindText)
        For Each enemy In character.Location.Enemies(character)
            prompt.AddChoice(enemy.Name)
        Next
        Return prompt
    End Function
    Sub Run(character As Character)
        AnsiConsole.Clear()
        Dim answer = AnsiConsole.Prompt(CreatePrompt(character))
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                HandleAttack(character, character.Location.Enemies(character).First(Function(x) x.Name = answer))
        End Select
    End Sub
    Private Sub HandleAttack(character As Character, enemy As Character)
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
            End If
        Else
            AnsiConsole.MarkupLine("You miss!")
        End If


        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
