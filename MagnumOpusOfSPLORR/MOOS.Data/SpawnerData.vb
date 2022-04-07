Public Module SpawnerData
    Friend Const TableName = "Spawners"
    Friend Const SpawnerIdColumn = "SpawnerId"
    Friend Const SpawnerNameColumn = "SpawnerName"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{SpawnerIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{SpawnerNameColumn}] TEXT NOT NULL
            );")
    End Sub
    Public Function ReadName(spawnerId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, SpawnerIdColumn, spawnerId, SpawnerNameColumn)
    End Function
    Public Function Create(spawnerName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]([{SpawnerNameColumn}]) VALUES(@{SpawnerNameColumn});",
            MakeParameter($"@{SpawnerNameColumn}", spawnerName))
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
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{SpawnerIdColumn}]=@{SpawnerIdColumn};",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId))
    End Sub
End Module
