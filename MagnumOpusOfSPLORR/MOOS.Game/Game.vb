Imports MOOS.Data

Public Module Game
    Sub NewGame()
        Store.Reset()
        CreateDirections()
        CreateItemTypes()
        CreateLocationsAndRoutes()
        CreatePlayerCharacter()
    End Sub

    Private Sub CreateItemTypes()
        CreateItemType("key")
    End Sub

    Private Sub CreateLocationsAndRoutes()
        Dim first = Locations.CreateLocation("Start")
        Dim second = Locations.CreateLocation("Middle")
        Dim third = Locations.CreateLocation("Finish")
        third.IsWinningLocation = True

        Items.CreateItem(ItemTypes.AllItemTypes.Single(Function(i) i.Name = "key"), first.Inventory)

        Routes.CreateRoute(first, second, FindDirectionByName("north").Single)
        Routes.CreateRoute(second, first, FindDirectionByName("south").Single)
        Dim finalRoute = Routes.CreateRoute(second, third, FindDirectionByName("east").Single)
        Routes.CreateRoute(third, second, FindDirectionByName("west").Single)

        Dim barrier = Barriers.CreateBarrier(ItemTypes.AllItemTypes.Single(Function(i) i.Name = "key"), True, True)
        finalRoute.AddBarrier(barrier)
    End Sub

    Private Sub CreateDirections()
        Directions.CreateDirection("north")
        Directions.CreateDirection("east")
        Directions.CreateDirection("south")
        Directions.CreateDirection("west")
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim location = Locations.FindLocationByName("Start").Single
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
