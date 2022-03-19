Imports MOOS.Game
Imports Spectre.Console

Module Program
    Private Sub Welcome()
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[aqua]* Magnum Opus of SPLORR!! *[/]")
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        AnsiConsole.WriteLine()
    End Sub
    Private Const EmbarkText = "Embark!"
    Private Const QuitText = "Quit"
    Private Sub MainMenu()
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "[olive]Main Menu:[/]"
            }
            prompt.AddChoice(EmbarkText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = ConfirmQuit()
                Case EmbarkText
                    Embark.Run()
            End Select
        End While
    End Sub

    Private Function ConfirmQuit() As Boolean
        Dim prompt = New ConfirmationPrompt("Are you sure you want to quit?")
        Return AnsiConsole.Prompt(prompt)
    End Function

    Sub Main(args As String())
        AddHandler Game.PlaySfx, AddressOf HandleSfx
        Console.Title = "Magnum Opus of SPLORR!!"
        Welcome()
        MainMenu()
    End Sub
End Module
