Public Module RouteData
    Friend Const TableName = "Routes"
    Friend Sub Initialize()
        LocationData.Initialize()
        DirectionData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Routes]
            (
                [RouteId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [FromLocationId] INT NOT NULL,
                [ToLocationId] INT NOT NULL,
                [Directionid] INT NOT NULL,
                FOREIGN KEY([FromLocationId]) REFERENCES [Locations]([LocationId]),
                FOREIGN KEY([ToLocationId]) REFERENCES [Locations]([LocationId]),
                FOREIGN KEY([Directionid]) REFERENCES [Directions]([DirectionId]),
                UNIQUE([FromLocationId], [DirectionId])
            );")
    End Sub
    Function Create(fromLocationId As Long, toLocationId As Long, directionId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Routes]
            (
                [FromLocationId],
                [ToLocationId],
                [DirectionId]
            ) 
            VALUES
            (
                @FromLocationId,
                @ToLocationId,
                @DirectionId
            );",
            MakeParameter("@FromLocationId", fromLocationId),
            MakeParameter("@ToLocationId", toLocationId),
            MakeParameter("@DirectionId", directionId))
        Return LastInsertRowId
    End Function

    Public Sub Clear(routeId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [Routes] WHERE [RouteId]=@RouteId;",
            MakeParameter("@RouteId", routeId))
    End Sub

    Public Function ReadFromLocation(routeId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [FromLocationId] FROM [Routes] WHERE [RouteId] = @RouteId;",
            MakeParameter("@RouteId", routeId))
    End Function

    Public Function ReadToLocation(routeId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [ToLocationId] FROM [Routes] WHERE [RouteId] = @RouteId;",
            MakeParameter("@RouteId", routeId))
    End Function

    Public Function ReadDirection(routeId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [DirectionId] FROM [Routes] WHERE [RouteId] = @RouteId;",
            MakeParameter("@RouteId", routeId))
    End Function

    Function ReadForLocation(locationId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader("RouteId")),
            "SELECT [RouteId] FROM [Routes] WHERE [FromLocationId]=@FromLocationId;",
            MakeParameter("@FromLocationId", locationId))
    End Function
    Friend Sub ClearForLocation(locationId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [Routes] WHERE [FromLocationId]=@LocationId OR [ToLocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Sub
End Module