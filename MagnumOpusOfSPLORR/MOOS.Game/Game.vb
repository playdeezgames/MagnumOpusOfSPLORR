Imports MOOS.Data

Public Module Game
    Sub Start()
        Store.Reset()
        CreatePlayerCharacter()
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim characterId = CharacterData.Create(CharacterType.Player)
        PlayerData.Write(characterId)
    End Sub

    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
End Module
