Module EditBarriersMenu
    Private Const CreateBarrierText = "Create Barrier"
    Private Function CreatePrompt() As SelectionPrompt(Of String)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Barriers:[/]"}
        prompt.AddChoices(GoBackText)
        prompt.AddChoices(CreateBarrierText)
        For Each barrier In AllBarriers
            prompt.AddChoice(barrier.UniqueName)
        Next
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
                Case CreateBarrierText
                    HandleCreateBarrier()
                Case Else
                    EditBarrierMenu.Run(AllBarriers.Single(Function(barrier) barrier.UniqueName = answer))
            End Select
        End While
    End Sub
    Private Sub HandleCreateBarrier()
        Dim itemType = CommonEditorMenu.ChooseItemType("Item Type:", False)
        Dim destroysItem = AnsiConsole.Confirm("Destroys Item?", False)
        Dim selfDestructs = AnsiConsole.Confirm("Self Destructs?", False)
        Barriers.CreateBarrier(itemType, destroysItem, selfDestructs)
    End Sub
End Module
