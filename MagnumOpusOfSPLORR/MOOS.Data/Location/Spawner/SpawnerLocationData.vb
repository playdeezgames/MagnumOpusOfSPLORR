Public Module SpawnerLocationData
    Friend Const TableName = "SpawnerLocations"
    Friend Const SpawnerIdColumn = SpawnerData.SpawnerIdColumn
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const CooldownColumn = "Cooldown"

    Public Function ReadSpawnerForLocation(locationId As Long) As Long?
        Dim ids = ReadIdsWithColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, LocationIdColumn, locationId)
        If ids.Any Then
            Return ids.First
        End If
        Return Nothing
    End Function

    Public Function ReadCooldownForLocation(locationId As Long) As Long?
        Dim ids = ReadIdsWithColumnValue(AddressOf Initialize, TableName, CooldownColumn, LocationIdColumn, locationId)
        Return If(ids.Any, ids.First, Nothing)
    End Function

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{SpawnerIdColumn}] INT NOT NULL,
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{CooldownColumn}] INT NOT NULL,
                FOREIGN KEY ([{SpawnerIdColumn}]) REFERENCES [{SpawnerData.TableName}]([{SpawnerData.SpawnerIdColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Sub ClearForLocation(locationId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, LocationIdColumn, locationId)
    End Sub

    Public Sub Write(spawnerId As Long, locationId As Long, cooldown As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{SpawnerIdColumn}],
                [{LocationIdColumn}],
                [{CooldownColumn}]
            ) 
            VALUES
            (
                @{SpawnerIdColumn},
                @{LocationIdColumn},
                @{CooldownColumn}
            );",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId),
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{CooldownColumn}", cooldown))
    End Sub

    Public Function ReadCountForSpawner(spawnerId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{SpawnerIdColumn}]=@{SpawnerIdColumn};",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId)).Value
    End Function

    Friend Sub ClearForSpawner(spawnerId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId)
    End Sub

    Public Sub WriteCooldownForLocation(locationId As Long, cooldown As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] 
            SET 
                [{CooldownColumn}]=@{CooldownColumn} 
            WHERE 
                [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{CooldownColumn}", cooldown))
    End Sub
End Module
