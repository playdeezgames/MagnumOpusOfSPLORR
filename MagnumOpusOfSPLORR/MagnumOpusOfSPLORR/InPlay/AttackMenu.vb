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
    Private Function Strike(attacker As Character, defender As Character) As Boolean
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{attacker.DisplayName} {attacker.AttackVerb} {defender.DisplayName}!")
        Dim attack = attacker.RollAttack()
        AnsiConsole.MarkupLine($"{attacker.DisplayName} {attacker.RollVerb} {attack} for attack!")
        Dim defend = defender.RollDefense()
        AnsiConsole.MarkupLine($"{defender.DisplayName} {defender.RollVerb} {defend} for defense!")
        If attack > defend Then
            Dim damage = attack - defend
            AnsiConsole.MarkupLine($"{attacker.DisplayName} {attacker.HitVerb} for {damage} damage!")
            defender.Wounds += damage
            If defender.IsDead Then
                AnsiConsole.MarkupLine($"{attacker.DisplayName} {attacker.KillVerb} {defender.DisplayName}!")
                defender.Kill()
                Return True
            End If
        Else
            AnsiConsole.MarkupLine($"{attacker.DisplayName} {attacker.MissVerb}!")
        End If
        Return False
    End Function
    Private Sub HandleAttack(character As Character, enemy As Character)
        Dim killedEnemy = Strike(character, enemy)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
        If Not killedEnemy Then
            HandleCounterAttack(character, enemy)
        End If
    End Sub
    Private Sub HandleCounterAttack(character As Character, enemy As Character)
        Strike(enemy, character)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
