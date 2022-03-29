Public Module LocationInventoryData
    Friend Const TableName = "LocationInventories"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{InventoryIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY ([{InventoryIdColumn}]) REFERENCES [{InventoryData.TableName}]([{InventoryData.InventoryIdColumn}])
            );")
    End Sub
    Function Read(locationId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{InventoryIdColumn}] FROM [{TableName}] WHERE [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Function
    Sub Write(locationId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{LocationIdColumn}],[{InventoryIdColumn}]) VALUES (@{LocationIdColumn},@{InventoryIdColumn});",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Sub
    Friend Sub Clear(locationId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId))
        InventoryData.Purge()
    End Sub
End Module
