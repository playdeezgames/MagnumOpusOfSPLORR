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

    Public Sub Clear(spawnerId As Long, characterTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM 
                [{TableName}] 
            WHERE 
                [{SpawnerIdColumn}]=@{SpawnerIdColumn} AND [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn};",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId),
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId))
    End Sub

    Public Sub Write(spawnerId As Long, characterTypeId As Long, weight As Integer)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{SpawnerIdColumn}],
                [{CharacterTypeIdColumn}],
                [{WeightColumn}]
            ) 
            VALUES
            (
                @{SpawnerIdColumn},
                @{CharacterTypeIdColumn},
                @{WeightColumn}
            );",
            MakeParameter($"@{SpawnerIdColumn}", spawnerId),
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId),
            MakeParameter($"@{WeightColumn}", weight))
    End Sub

    Public Function CountForCharacterType(characterTypeId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn};",
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId)).Value
    End Function

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

    Friend Sub ClearForCharacterType(characterTypeId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId)
    End Sub
End Module
