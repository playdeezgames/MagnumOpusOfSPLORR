Imports MOOS.Data

Public Module Game
    Sub NewGame()
        Store.Reset()
        CreateDirections()
        CreateLocationsAndRoutes()
        CreatePlayerCharacter()
    End Sub

    Private Sub CreateLocationsAndRoutes()
        Dim first = Locations.CreateLocation("Start")
        Dim second = Locations.CreateLocation("Middle")
        Dim third = Locations.CreateLocation("Finish")

        Routes.CreateRoute(first, second, AllDirections.Single(Function(d) d.Name = "north"))
        Routes.CreateRoute(second, first, AllDirections.Single(Function(d) d.Name = "south"))
        Routes.CreateRoute(second, third, AllDirections.Single(Function(d) d.Name = "east"))
        Routes.CreateRoute(third, second, AllDirections.Single(Function(d) d.Name = "west"))
    End Sub

    Private Sub CreateDirections()
        Directions.CreateDirection("north")
        Directions.CreateDirection("east")
        Directions.CreateDirection("south")
        Directions.CreateDirection("west")
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim location = Locations.AllLocations.Single(Function(l) l.Name = "Start")
        Dim character = Characters.CreateCharacter("Tagon", Location)
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
