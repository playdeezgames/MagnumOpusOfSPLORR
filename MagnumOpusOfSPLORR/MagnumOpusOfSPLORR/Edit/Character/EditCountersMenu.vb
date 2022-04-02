Module EditCountersMenu
    Private Function CreatePrompt(character As Character) As SelectionPrompt(Of String)
        Dim result As New SelectionPrompt(Of String) With {.Title = "[olive]Counters:[/]"}
        result.AddChoice(NeverMindText)
        For Each counterType In AllCounterTypes
            result.AddChoice(counterType.Name)
        Next
        Return result
    End Function
    Friend Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowCounters(character)
            Dim answer = AnsiConsole.Prompt(CreatePrompt(character))
            Select Case answer
                Case NeverMindText
                    done = True
                Case Else
                    HandleChangeCounter(character, FindCounterTypeByName(answer))
            End Select
        End While
    End Sub
    Private Sub HandleChangeCounter(character As Character, counterType As CounterType)
        Dim counterValue = AnsiConsole.Ask(Of Long)("[olive]New Value For Counter:[/]", 0)
        character.SetCounter(counterType, counterValue)
    End Sub
End Module
