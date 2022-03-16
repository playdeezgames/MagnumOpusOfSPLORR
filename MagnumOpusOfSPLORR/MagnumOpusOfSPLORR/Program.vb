Imports System
Imports MOOS.Game
Imports Spectre.Console

Module Program
    Sub Main(args As String())
        AddHandler Game.PlaySfx, AddressOf HandleSfx
        Console.Title = "Magnum Opus of SPLORR!!"
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[aqua]* Magnum Opus of SPLORR!! *[/]")
        AnsiConsole.MarkupLine("[aqua]***************************[/]")
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        AnsiConsole.WriteLine()
        Dim prompt As New SelectionPrompt(Of String) With {
            .Title = "[olive]Main Menu:[/]"
        }
        prompt.AddChoice("Quit")
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
