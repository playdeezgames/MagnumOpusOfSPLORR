Public Module CharacterInventoryData
    Public Const TableName = "CharacterInventories"
    Public Const CharacterIdColumn = "CharacterId"
    Public Const InventoryIdColumn = "InventoryId"
    Friend Sub Initialize()
        CharacterData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{InventoryIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{InventoryIdColumn}]) REFERENCES [{InventoryData.TableName}]([{InventoryData.InventoryIdColumn}])
            );")
    End Sub
    Function Read(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{InventoryIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function
    Sub Write(characterId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{CharacterIdColumn}],[{InventoryIdColumn}]) VALUES (@{CharacterIdColumn},@{InventoryIdColumn});",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Sub
    Friend Sub Clear(characterId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Sub
End Module
