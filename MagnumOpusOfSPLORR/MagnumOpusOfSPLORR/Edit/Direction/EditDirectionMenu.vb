Module EditDirectionMenu
    Private Sub ShowStatus(direction As Direction)
        AnsiConsole.MarkupLine($"Id: {direction.Id}")
        AnsiConsole.MarkupLine($"Name: {direction.Name}")
    End Sub
    Private Function CreatePrompt(direction As Direction) As SelectionPrompt(Of String)
        Dim prompt = New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
        prompt.AddChoice(GoBackText)
        prompt.AddChoice(ChangeNameText)
        If direction.CanDestroy Then
            prompt.AddChoice(DestroyText)
        End If
        Return prompt
    End Function
    Sub Run(direction As Direction)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            ShowStatus(direction)
            Select Case AnsiConsole.Prompt(CreatePrompt(direction))
                Case GoBackText
                    done = True
                Case ChangeNameText
                    HandleChangeName(direction)
                Case DestroyText
                    direction.Destroy()
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleChangeName(direction As Direction)
        Dim newName = AnsiConsole.Ask(Of String)("New Direction Name:")
        If Not String.IsNullOrWhiteSpace(newName) Then
            direction.Name = newName
        End If
    End Sub
End Module
