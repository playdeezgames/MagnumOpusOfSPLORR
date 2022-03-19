Imports MOOS.Game
Imports Spectre.Console

Module Embark
    Private Const AbandonGameText = "Abandon Game"
    Const NeverMindText = "Never Mind"
    Private Function ConfirmAbandon() As Boolean
        Dim prompt = New ConfirmationPrompt("Are you sure you want to abandon the game?")
        Return AnsiConsole.Prompt(prompt)
    End Function
    Private Function HandleGameMenu() As Boolean
        Dim prompt As New SelectionPrompt(Of String) With
            {
            .Title = "[olive]Game Menu:[/]"
            }
        prompt.AddChoice(AbandonGameText)
        prompt.AddChoice(NeverMindText)
        Select Case AnsiConsole.Prompt(prompt)
            Case AbandonGameText
                Return ConfirmAbandon
            Case NeverMindText
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Const GameMenuText = "Game Menu"
    Sub Run()
        Game.Start()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine("You exist!")
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Now what?[/]"
                }
            prompt.AddChoice(GameMenuText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GameMenuText
                    done = HandleGameMenu()
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
        Game.Finish()
    End Sub
End Module
