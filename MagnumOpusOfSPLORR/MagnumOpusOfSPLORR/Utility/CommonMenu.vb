Imports MOOS.Game
Imports Spectre.Console

Module CommonMenu
    Friend Const NeverMindText = "Never Mind"
    Friend Const GoBackText = "Go Back"
    Friend Const ChangeNameText = "Change Name"
    Friend Const DestroyText = "Destroy"
    Function ChooseLocation(title As String, canCancel As Boolean) As Location
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each location In AllLocations
            prompt.AddChoices(location.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return AllLocations.Single(Function(x) x.UniqueName = answer)
        End Select
    End Function
    Function ChooseDirection(title As String, candidates As List(Of Direction), canCancel As Boolean) As Direction
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        If canCancel Then
            prompt.AddChoice(NeverMindText)
        End If
        For Each direction In candidates
            prompt.AddChoices(direction.UniqueName)
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return AllDirections.Single(Function(x) x.UniqueName = answer)
        End Select
    End Function
    Function ChooseDirection(title As String, canCancel As Boolean) As Direction
        Return ChooseDirection(title, AllDirections, canCancel)
    End Function
End Module
