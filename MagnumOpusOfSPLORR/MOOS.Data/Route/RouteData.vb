Public Module RouteData
    Friend Const TableName = "Routes"
    Friend Const RouteIdColumn = "RouteId"
    Friend Const FromLocationIdColumn = "FromLocationId"
    Friend Const ToLocationIdColumn = "ToLocationId"
    Friend Const DirectionIdColumn = DirectionData.DirectionIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        DirectionData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{RouteIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{FromLocationIdColumn}] INT NOT NULL,
                [{ToLocationIdColumn}] INT NOT NULL,
                [{DirectionIdColumn}] INT NOT NULL,
                FOREIGN KEY([{FromLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY([{ToLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY([{DirectionIdColumn}]) REFERENCES [Directions]([DirectionId]),
                UNIQUE([{FromLocationIdColumn}], [{DirectionIdColumn}])
            );")
    End Sub
    Function Create(fromLocationId As Long, toLocationId As Long, directionId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{FromLocationIdColumn}],
                [{ToLocationIdColumn}],
                [{DirectionIdColumn}]
            ) 
            VALUES
            (
                @{FromLocationIdColumn},
                @{ToLocationIdColumn},
                @{DirectionIdColumn}
            );",
            MakeParameter($"@{FromLocationIdColumn}", fromLocationId),
            MakeParameter($"@{ToLocationIdColumn}", toLocationId),
            MakeParameter($"@{DirectionIdColumn}", directionId))
        Return LastInsertRowId
    End Function

    Public Sub Clear(routeId As Long)
        RouteBarrierData.ClearForRoute(routeId)
        ClearForColumnValue(AddressOf Initialize, TableName, RouteIdColumn, routeId)
    End Sub

    Public Function ReadFromLocation(routeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, RouteIdColumn, routeId, FromLocationIdColumn)
    End Function

    Public Function ReadToLocation(routeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, RouteIdColumn, routeId, ToLocationIdColumn)
    End Function

    Public Function ReadDirection(routeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, RouteIdColumn, routeId, DirectionIdColumn)
    End Function

    Function ReadForLocation(locationId As Long) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, RouteIdColumn, FromLocationIdColumn, locationId)
    End Function
    Friend Sub ClearForLocation(locationId As Long)
        Initialize()
        Dim routeIds = ExecuteReader(
            Function(reader) CLng(reader(RouteIdColumn)),
            $"SELECT [{RouteIdColumn}] FROM [{TableName}] WHERE [{FromLocationIdColumn}]=@{LocationData.LocationIdColumn} OR [{ToLocationIdColumn}]=@{LocationData.LocationIdColumn};",
            MakeParameter($"@{LocationData.LocationIdColumn}", locationId))
        For Each routeId In routeIds
            Clear(routeId)
        Next
    End Sub
End Module