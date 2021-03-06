Public Module CharacterInventoryData
    Friend Const TableName = "CharacterInventories"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
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
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, InventoryIdColumn)
    End Function
    Sub Write(characterId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{CharacterIdColumn}],[{InventoryIdColumn}]) VALUES (@{CharacterIdColumn},@{InventoryIdColumn});",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Sub
    Friend Sub Clear(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
        InventoryData.Purge()
    End Sub
End Module
