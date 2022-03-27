Public Module ItemData
    Friend Sub Initialize()
        ItemTypeData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Items]
            (
                [ItemId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [ItemTypeId] INT NOT NULL,
                [InventoryId] INT NOT NULL,
                FOREIGN KEY([ItemTypeId]) REFERENCES [ItemTypes]([ItemTypeId]),
                FOREIGN KEY([InventoryId]) REFERENCES [Inventories]([InventoryId])
            );")
    End Sub
    Function ReadItemType(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [ItemTypeId] FROM [Items] WHERE [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
    End Function
    Function ReadForInventory(inventoryId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader("ItemId")),
            "SELECT [ItemId] FROM [Items] WHERE [InventoryId]=@InventoryId;",
            MakeParameter("@InventoryId", inventoryId))
    End Function
    Sub ClearForItemType(itemTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [Items] WHERE [ItemTypeId]=@ItemTypeId",
            MakeParameter("@ItemTypeId", itemTypeId))
    End Sub
    Function ReadCountForItemType(itemTypeId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT COUNT(1) FROM [Items] WHERE [ItemTypeId]=@ItemTypeId;",
            MakeParameter("@ItemTypeId", itemTypeId)).Value
    End Function
End Module
