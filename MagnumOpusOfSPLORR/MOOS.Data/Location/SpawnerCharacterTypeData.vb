Public Module SpawnerCharacterTypeData
    Friend Const TableName = "SpawnerCharacterTypes"
    Friend Const SpawnerIdColumn = SpawnerData.SpawnerIdColumn
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn
    Friend Const WeightColumn = "Weight"
    Friend Sub Initialize()
        SpawnerData.Initialize()
        CharacterTypeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{SpawnerIdColumn}] INT NOT NULL,
                [{CharacterTypeIdColumn}] INT NOT NULL,
                [{WeightColumn}] INT NOT NULL,
                UNIQUE([{SpawnerIdColumn}],[{CharacterTypeIdColumn}]),
                FOREIGN KEY ([{SpawnerIdColumn}]) REFERENCES [{SpawnerData.TableName}]([{SpawnerData.SpawnerIdColumn}]),
                FOREIGN KEY ([{CharacterTypeIdColumn}]) REFERENCES [{CharacterTypeData.TableName}]([{CharacterTypeData.CharacterTypeIdColumn}])
            );")
    End Sub
    Public Function ReadForSpawner(spawnerId As Long) As Dictionary(Of Long, Long)
        Initialize()
        Dim records = ExecuteReader(
            Function(reader) New Tuple(Of Long, Long)(CLng(reader(CharacterTypeIdColumn)), CLng(reader(WeightColumn))),
            $"SELECT 
                [{CharacterTypeIdColumn}],
                [{WeightColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{SpawnerIdColumn}]=@{SpawnerIdColumn};",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId))
        Dim result As New Dictionary(Of Long, Long)
        For Each record In records
            result(record.Item1) = record.Item2
        Next
        Return result
    End Function
End Module
