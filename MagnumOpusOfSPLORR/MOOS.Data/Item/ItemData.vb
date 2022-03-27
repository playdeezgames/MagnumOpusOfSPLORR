Public Module ItemData
    Public Const TableName = "Items"
    Public Const ItemIdColumn = "ItemId"
    Public Const ItemTypeIdColumn = "ItemTypeId"
    Public Const InventoryIdColumn = "InventoryId"
    Friend Sub Initialize()
        ItemTypeData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{ItemTypeIdColumn}] INT NOT NULL,
                [{InventoryIdColumn}] INT NOT NULL,
                FOREIGN KEY([{ItemTypeIdColumn}]) REFERENCES [{ItemTypeData.TableName}]([{ItemTypeData.ItemTypeIdColumn}]),
                FOREIGN KEY([{InventoryIdColumn}]) REFERENCES [{InventoryData.TableName}]([{InventoryData.InventoryIdColumn}])
            );")
    End Sub

    Public Function Create(itemTypeId As Long, inventoryId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{ItemTypeIdColumn}],
                [{InventoryIdColumn}]
            )
            VALUES
            (
                @{ItemTypeIdColumn},
                @{InventoryIdColumn}
            );",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
        Return LastInsertRowId
    End Function

    Public Sub Clear(itemId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{ItemIdColumn}]=@{ItemIdColumn};",
            MakeParameter($"@{ItemIdColumn}", itemId))
    End Sub

    Public Sub WriteInventory(itemId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE 
                [{TableName}] 
            SET 
                [{InventoryIdColumn}]=@{InventoryIdColumn} 
            WHERE 
                [{ItemIdColumn}]=@{ItemIdColumn};",
            MakeParameter($"@{ItemIdColumn}", itemId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Sub

    Function ReadItemType(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{ItemTypeIdColumn}] FROM [{TableName}] WHERE [{ItemIdColumn}]=@{ItemIdColumn};",
            MakeParameter($"@{ItemIdColumn}", itemId))
    End Function
    Function ReadForInventory(inventoryId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader($"{ItemIdColumn}")),
            $"SELECT [{ItemIdColumn}] FROM [{TableName}] WHERE [{InventoryIdColumn}]=@{InventoryIdColumn};",
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Function
    Sub ClearForItemType(itemTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn}",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId))
    End Sub
    Function ReadCountForItemType(itemTypeId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId)).Value
    End Function
End Module
