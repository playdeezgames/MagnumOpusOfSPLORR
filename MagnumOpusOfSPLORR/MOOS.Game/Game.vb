Public Module Game
    Sub NewGame(mazeColumns As Long, mazeRows As Long)
        Store.Reset()
        CreateDirections()
        CreateItemTypes()
        Dim maze = GenerateMaze(mazeColumns, mazeRows)
        CreateLocationsAndRoutes()
        CreatePlayerCharacter()
    End Sub

    Private Function GenerateMaze(mazeColumns As Long, mazeRows As Long) As Maze(Of String)
        Dim maze As New Maze(Of String)(mazeColumns, mazeRows, New Dictionary(Of String, MazeDirection(Of String)) From {
            {"north", New MazeDirection(Of String)("south", 0, -1)},
            {"east", New MazeDirection(Of String)("west", 1, 0)},
            {"south", New MazeDirection(Of String)("north", 0, 1)},
            {"west", New MazeDirection(Of String)("east", -1, 0)}})
        maze.Generate()
        Return maze
    End Function

    Private Sub CreateItemTypes()
        CreateItemType("key")
    End Sub

    Private Sub CreateLocationsAndRoutes()
        Dim first = Locations.CreateLocation("Start")
        Dim second = Locations.CreateLocation("Middle")
        Dim third = Locations.CreateLocation("Finish")
        third.IsWinningLocation = True

        Items.CreateItem(FindItemTypeByName("key").Single, first.Inventory)

        Routes.CreateRoute(first, second, FindDirectionByName("north").Single)
        Routes.CreateRoute(second, first, FindDirectionByName("south").Single)
        Dim finalRoute = Routes.CreateRoute(second, third, FindDirectionByName("east").Single)
        Routes.CreateRoute(third, second, FindDirectionByName("west").Single)

        Dim barrier = Barriers.CreateBarrier(FindItemTypeByName("key").Single, True, True)
        finalRoute.AddBarrier(barrier)
    End Sub

    Private Sub CreateDirections()
        Directions.CreateDirection("north")
        Directions.CreateDirection("east")
        Directions.CreateDirection("south")
        Directions.CreateDirection("west")
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim characterType = CharacterTypes.CreateCharacterType("PC")
        Dim location = Locations.FindLocationByName("Start").Single
        Dim character = Characters.CreateCharacter("Tagon", location, characterType)
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
