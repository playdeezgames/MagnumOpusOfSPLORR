Imports MOOS.Data
Imports MOOS.Game
Imports Spectre.Console

Module Program
    Private Sub Welcome()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[aqua]* Magnum Opus of SPLORR!! *[/]")
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        Dim prompt = New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
    Private Const NewGameText = "New Game"
    Private Const LoadGameText = "Load Game..."
    Private Const SaveGameText = "Save Game..."
    Private Const AbandonGameText = "Abandon Game"
    Private Const TestText = "Test!"
    Private Const EmbarkText = "Embark!"
    Private Const EditGameText = "Edit Game..."
    Private Const QuitText = "Quit"
    Private Sub MainMenu()
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "[olive]Main Menu:[/]"
            }
            If Store.Exists Then
                prompt.AddChoice(EmbarkText)
                prompt.AddChoice(TestText)
                prompt.AddChoice(SaveGameText)
                prompt.AddChoice(EditGameText)
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
                Case EditGameText
                    EditGameMenu.Run()
                Case NewGameText
                    Game.NewGame()
                Case QuitText
                    done = ConfirmQuit()
                Case EmbarkText
                    Embark.Run(False)
                Case TestText
                    Embark.Run(True)
            End Select
        End While
        Play("L500;C4;L250;G3;G3;L500;G#3;G3;R500;B3;C4;R500")
        Game.Finish()
    End Sub

    Private Function ConfirmQuit() As Boolean
        Dim prompt = New ConfirmationPrompt("[red]Are you sure you want to quit?[/]")
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
        If AnsiConsole.Prompt(New ConfirmationPrompt("[red]Are you sure you want to abandon the game?[/]")) Then
            Game.Finish()
        End If
    End Sub
End Module
