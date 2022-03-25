Imports MOOS.Data

Public Module Game
    Sub NewGame()
        Store.Reset()
        CreatePlayerCharacter()
    End Sub
    Private Sub CreatePlayerCharacter()
        Dim north = Directions.CreateDirection("north")
        Dim east = Directions.CreateDirection("east")
        Dim south = Directions.CreateDirection("south")
        Dim west = Directions.CreateDirection("west")
        Dim location = Locations.CreateLocation("Start")
        Dim character = Characters.CreateCharacter("Tagon", location)
        character.SetAsPlayerCharacter()
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
End Module
