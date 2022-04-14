Public Module WinningLocationData
    Friend Const TableName = "WinningLocations"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery($"CREATE TABLE IF NOT EXISTS [{TableName}]([{LocationIdColumn}] INT NOT NULL UNIQUE);")
    End Sub
    Function Read(locationId As Long) As Boolean
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, LocationIdColumn, LocationIdColumn, locationId).Any
    End Function

    Public Sub Clear(locationId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, LocationIdColumn, locationId)
    End Sub

    Public Sub Write(locationId As Long) 'TODO: this is a REPLACE
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}] ([{LocationIdColumn}]) VALUES (@{LocationIdColumn});",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub
End Module
