Public Module LocationData
    Friend Const TableName = "Locations"
    Friend Const LocationIdColumn = "LocationId"
    Friend Const LocationNameColumn = "LocationName"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationNameColumn}] TEXT NOT NULL
            );")
    End Sub
    Function Create(locationName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{LocationNameColumn}]
            )
            VALUES
            (
                @{LocationNameColumn}
            );",
            MakeParameter($"@{LocationNameColumn}", locationName))
        Return LastInsertRowId
    End Function
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, LocationIdColumn)
        End Get
    End Property
    Function ReadName(locationId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, LocationIdColumn, locationId, LocationNameColumn)
    End Function
    Sub WriteName(locationId As Long, locationName As String)
        WriteColumnValue(AddressOf Initialize, TableName, LocationIdColumn, locationId, LocationNameColumn, locationName)
    End Sub
    Sub Clear(locationId As Long)
        CharacterData.ClearForLocation(locationId)
        RouteData.ClearForLocation(locationId)
        LocationInventoryData.Clear(locationId)
        ClearForColumnValue(AddressOf Initialize, TableName, LocationIdColumn, locationId)
    End Sub
    Function ReadForName(locationName As String) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, LocationIdColumn, LocationNameColumn, locationName)
    End Function
End Module
