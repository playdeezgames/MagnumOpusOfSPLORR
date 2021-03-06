Public Module Game
    Private ReadOnly walker As New Dictionary(Of String, MazeDirection(Of String)) From {
            {"north", New MazeDirection(Of String)("south", 0, -1)},
            {"east", New MazeDirection(Of String)("west", 1, 0)},
            {"south", New MazeDirection(Of String)("north", 0, 1)},
            {"west", New MazeDirection(Of String)("east", -1, 0)}}
    Sub NewGame(mazeColumns As Long, mazeRows As Long)
        Store.Reset()
        CreateDirections()
        CreateEquipSlots()
        CreateItemTypes()
        Dim maze = GenerateMaze(mazeColumns, mazeRows)
        CreateWorld(maze)
        CreatePlayerCharacter()
    End Sub

    Private Sub CreateEquipSlots()
        EquipSlots.CreateEquipSlot("weapon")
        EquipSlots.CreateEquipSlot("shield")
    End Sub

    Private Function GenerateMaze(mazeColumns As Long, mazeRows As Long) As Maze(Of String)
        Dim maze As New Maze(Of String)(mazeColumns, mazeRows, walker)
        maze.Generate()
        Return maze
    End Function
    Private Sub CreateItemTypes()
        CreateItemType("key")

        Dim sword = CreateItemType("shortsword")
        sword.AttackDice = "1d2/2+1d2/2"
        sword.EquipSlot = FindEquipSlotByName("weapon").Single

        Dim shield = CreateItemType("shield")
        shield.DefendDice = "1d3/3"
        shield.EquipSlot = FindEquipSlotByName("shield").Single

        Dim food = CreateItemType("hardtack")
        food.HealDice = "1d1"
    End Sub
    Private Sub CreateLocations(columns As Long, rows As Long)
        For column = 0 To columns - 1
            For row = 0 To rows - 1
                CreateLocation($"Cell({column},{row})")
            Next
        Next
    End Sub
    Private Sub CreateRoutes(maze As Maze(Of String))
        For column = 0 To maze.Columns - 1
            For row = 0 To maze.Rows - 1
                Dim cell = maze.GetCell(column, row)
                Dim location = Locations.FindLocationByName($"Cell({column},{row})").Single
                For Each direction In cell.Directions.Where(Function(x) cell.GetDoor(x).Open)
                    Dim nextColumn = column + walker(direction).DeltaX
                    Dim nextRow = row + walker(direction).DeltaY
                    Dim nextLocation = Locations.FindLocationByName($"Cell({nextColumn},{nextRow})").Single
                    Routes.CreateRoute(location, nextLocation, Directions.FindDirectionByName(direction).Single)
                Next
            Next
        Next
    End Sub
    Private Sub CreateWinningLocation()
        Dim finish = RNG.FromList(Locations.AllLocations.Where(Function(x) x.Routes.Count = 1).ToList)
        finish.IsWinningLocation = True
        Dim barrier = Barriers.CreateBarrier(FindItemTypeByName("key").Single, True, True)
        finish.Routes.Single.ToLocation.Routes.Single(Function(x) x.ToLocation = finish).AddBarrier(barrier)
    End Sub
    Private Sub PlaceKey()
        Dim keyCell = RNG.FromList(Locations.AllLocations.Where(Function(x) x.Routes.Count = 1 AndAlso Not x.IsWinningLocation).ToList)
        Items.CreateItem(FindItemTypeByName("key").Single, keyCell.Inventory)
    End Sub
    Private Sub CreateWorld(maze As Maze(Of String))
        CreateLocations(maze.Columns, maze.Rows)
        CreateRoutes(maze)
        CreateWinningLocation()
        PlaceKey()
        'TODO: place sword?
        'TODO: place shield?
        'TODO: place food?
        'TODO: place enemies?
    End Sub
    Private Sub CreateDirections()
        Directions.CreateDirection("north")
        Directions.CreateDirection("east")
        Directions.CreateDirection("south")
        Directions.CreateDirection("west")
    End Sub
    Private Sub CreatePlayerCharacter()
        Dim characterType = CharacterTypes.CreateCharacterType("PC", 5, "1d3/3", "1d3/3+1d3/3")
        characterType.AddEquipSlot(EquipSlots.FindEquipSlotByName("weapon").First)
        characterType.AddEquipSlot(EquipSlots.FindEquipSlotByName("shield").First)
        Dim location = RNG.FromList(Locations.AllLocations.Where(Function(x) x.Routes.Count = 1 AndAlso Not x.IsWinningLocation).ToList)
        Dim character = Characters.CreateCharacter("Tagon", location, characterType)
        character.SetAsPlayerCharacter()
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
End Module
