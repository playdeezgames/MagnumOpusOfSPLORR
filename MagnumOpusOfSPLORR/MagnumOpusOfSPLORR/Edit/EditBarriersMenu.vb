Module EditBarriersMenu
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Barriers:[/]"}
        prompt.AddChoices(GoBackText)
        Return prompt
    End Function
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim answer = AnsiConsole.Prompt(CreatePrompt)
            Select Case answer
                Case GoBackText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
