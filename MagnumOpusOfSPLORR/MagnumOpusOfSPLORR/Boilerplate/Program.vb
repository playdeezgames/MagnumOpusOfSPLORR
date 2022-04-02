Module Program
    Private Sub Welcome()
        AnsiConsole.Clear()
        Dim figlet = New FigletText("Magnum Opus").Centered()
        figlet.Color = Color.Aqua
        AnsiConsole.Write(figlet)
        figlet = New FigletText("of").Centered()
        figlet.Color = Color.Aqua
        AnsiConsole.Write(figlet)
        figlet = New FigletText("SPLORR!!").Centered()
        figlet.Color = Color.Aqua
        AnsiConsole.Write(figlet)
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        Dim prompt = New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Ok")
        AnsiConsole.Prompt(prompt)
    End Sub
    Private Const NewGameText = "New Game"
    Private Const LoadGameText = "Load Game..."
    Private Const SaveGameText = "Save Game..."
    Private Const UnloadGameText = "Unload Game"
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
                prompt.AddChoice(UnloadGameText)
            Else
                prompt.AddChoice(NewGameText)
                prompt.AddChoice(LoadGameText)
            End If
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case SaveGameText
                    HandleSaveGame()
                Case UnloadGameText
                    HandleUnloadGame()
                Case LoadGameText
                    HandleLoadGame()
                Case EditGameText
                    EditGameMenu.Run()
                Case NewGameText
                    HandleNewGame()
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

    Private Sub HandleNewGame()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "What size?"}
        prompt.AddChoice(NeverMindText)
        prompt.AddChoice("4")
        prompt.AddChoice("5")
        prompt.AddChoice("6")
        prompt.AddChoice("7")
        prompt.AddChoice("8")
        prompt.AddChoice("9")
        prompt.AddChoice("10")
        prompt.AddChoice("11")
        prompt.AddChoice("12")
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing!
            Case Else
                Dim size = CLng(answer)
                Game.NewGame(size, size)
        End Select
    End Sub

    Private Function ConfirmQuit() As Boolean
        Return AnsiConsole.Confirm("[red]Are you sure you want to quit?[/]", False)
    End Function

    Sub Main(args As String())
        AddHandler Game.PlaySfx, AddressOf HandleSfx
        Console.Title = "Magnum Opus of SPLORR!!"
        Welcome()
        MainMenu()
    End Sub
    Private Sub HandleSaveGame()
        Dim fileName = AnsiConsole.Ask(Of String)("Filename:", "")
        If Not String.IsNullOrEmpty(fileName) Then
            Store.Save(fileName)
        End If
    End Sub
    Private Sub HandleLoadGame()
        Dim fileName = AnsiConsole.Ask(Of String)("Filename:", "")
        If Not String.IsNullOrEmpty(fileName) Then
            Store.Load(fileName)
        End If
    End Sub
    Private Sub HandleUnloadGame()
        If AnsiConsole.Confirm("[red]Are you sure you want to unload the game?[/]", False) Then
            Game.Finish()
        End If
    End Sub
End Module
