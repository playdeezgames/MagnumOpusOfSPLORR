Public Module ItemData
    Friend Const TableName = "Items"
    Friend Const ItemIdColumn = "ItemId"
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
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
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub

    Public Sub WriteInventory(itemId As Long, inventoryId As Long)
        WriteColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId, InventoryIdColumn, inventoryId)
    End Sub

    Function ReadItemType(itemId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, ItemIdColumn, itemId, ItemTypeIdColumn)
    End Function
    Function ReadForInventory(inventoryId As Long) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, ItemIdColumn, InventoryIdColumn, inventoryId)
    End Function
    Sub ClearForItemType(itemTypeId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId)
    End Sub
    Function ReadCountForItemType(itemTypeId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId)).Value
    End Function
End Module
