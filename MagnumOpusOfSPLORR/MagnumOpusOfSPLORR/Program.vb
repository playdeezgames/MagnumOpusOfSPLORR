Imports MOOS.Data
Imports MOOS.Game
Imports Spectre.Console

Module Program
    Const NeverMindText = "Never Mind"
    Private Sub Welcome()
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[aqua]* Magnum Opus of SPLORR!! *[/]")
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        AnsiConsole.WriteLine()
    End Sub
    Private Const NewGameText = "New Game"
    Private Const LoadGameText = "Load Game..."
    Private Const SaveGameText = "Save Game..."
    Private Const AbandonGameText = "Abandon Game"
    Private Const EmbarkText = "Embark!"
    Private Const QuitText = "Quit"
    Private Sub MainMenu()
        Dim done = False
        While Not done
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "[olive]Main Menu:[/]"
            }
            If Store.Exists Then
                prompt.AddChoice(EmbarkText)
                prompt.AddChoice(SaveGameText)
                prompt.AddChoice(AbandonGameText)
            Else
                prompt.AddChoice(NewGameText)
                prompt.AddChoice(LoadGameText)
            End If
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case SaveGameText
                    HandleSaveGame()
                Case AbandonGameText
                    HandleAbandonGame()
                Case LoadGameText
                    HandleLoadGame()
                Case NewGameText
                    Game.NewGame()
                Case QuitText
                    done = ConfirmQuit()
                Case EmbarkText
                    Embark.Run()
            End Select
        End While
        Game.Finish()
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
    Private Sub HandleSaveGame()
        Dim fileName = AnsiConsole.Ask(Of String)("Filename:")
        If Not String.IsNullOrEmpty(fileName) Then
            Store.Save(fileName)
        End If
    End Sub
    Private Sub HandleLoadGame()
        Dim fileName = AnsiConsole.Ask(Of String)("Filename:")
        If Not String.IsNullOrEmpty(fileName) Then
            Store.Load(fileName)
        End If
    End Sub
    Private Sub HandleAbandonGame()
        Dim prompt = New ConfirmationPrompt("Are you sure you want to abandon the game?")
        If AnsiConsole.Prompt(prompt) Then
            Game.Finish()
        End If
    End Sub
End Module
