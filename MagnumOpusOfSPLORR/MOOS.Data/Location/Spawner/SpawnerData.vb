Public Module SpawnerData
    Friend Const TableName = "Spawners"
    Friend Const SpawnerIdColumn = "SpawnerId"
    Friend Const SpawnerNameColumn = "SpawnerName"
    Friend Const SpawnNothingWeightColumn = "SpawnNothingWeight"
    Friend Const CooldownColumn = "Cooldown"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{SpawnerIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{SpawnerNameColumn}] TEXT NOT NULL,
                [{SpawnNothingWeightColumn}] INT NOT NULL,
                [{CooldownColumn}] INT NOT NULL
            );")
    End Sub

    Public Function ReadSpawnNothingWeight(spawnerId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, SpawnNothingWeightColumn)
    End Function

    Public Sub WriteSpawnNothingWeight(spawnerId As Long, weight As Long)
        WriteColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, SpawnNothingWeightColumn, weight)
    End Sub

    Public Function ReadCooldown(spawnerId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, CooldownColumn)
    End Function

    Public Sub WriteCooldown(spawnerId As Long, cooldown As Long)
        WriteColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, CooldownColumn, cooldown)
    End Sub

    Public Function ReadName(spawnerId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, SpawnerNameColumn)
    End Function
    Public Function Create(spawnerName As String, spawnNothingWeight As Long, cooldown As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{SpawnerNameColumn}],
                [{SpawnNothingWeightColumn}],
                [{CooldownColumn}]
            ) 
            VALUES
            (
                @{SpawnerNameColumn},
                @{SpawnNothingWeightColumn},
                @{CooldownColumn}
            );",
            MakeParameter($"@{SpawnerNameColumn}", spawnerName),
            MakeParameter($"@{SpawnNothingWeightColumn}", spawnNothingWeight),
            MakeParameter($"@{CooldownColumn}", cooldown))
        Return LastInsertRowId
    End Function
    Public Sub WriteName(spawnerId As Long, spawnerName As String)
        WriteColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, SpawnerNameColumn, spawnerName)
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, SpawnerIdColumn)
        End Get
    End Property
    Public Sub Clear(spawnerId As Long)
        SpawnerLocationData.ClearForSpawner(spawnerId)
        ClearForColumnValue(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId)
    End Sub
End Module
