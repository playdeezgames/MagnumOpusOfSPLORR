Public Module InventoryData
    Friend Const TableName = "Inventories"
    Friend Const InventoryIdColumn = "InventoryId"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{InventoryIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT
            );")
    End Sub
    Function Create() As Long
        Initialize()
        ExecuteNonQuery($"INSERT INTO [{TableName}] DEFAULT VALUES;")
        Return LastInsertRowId
    End Function
    Sub Purge()
        Initialize()
        CharacterInventoryData.Initialize()
        LocationInventoryData.Initialize()
        Dim inventoryIds = ExecuteReader(
            Function(reader) CLng(reader(InventoryIdColumn)),
            $"SELECT 
                i.[{InventoryIdColumn}] 
            FROM 
                [{TableName}] i 
                LEFT JOIN [{CharacterInventoryData.TableName}] c ON c.[{CharacterInventoryData.InventoryIdColumn}]=i.[{InventoryIdColumn}]
                LEFT JOIN [{LocationInventoryData.TableName}] l ON l.[{LocationInventoryData.InventoryIdColumn}]=i.[{InventoryIdColumn}]
            WHERE
                c.[{CharacterInventoryData.InventoryIdColumn}] IS NULL
                AND l.[{LocationInventoryData.InventoryIdColumn}] IS NULL")
        For Each inventoryId In inventoryIds
            Clear(inventoryId)
        Next
    End Sub
    Sub Clear(inventoryId As Long)
        Dim itemIds = ItemData.ReadForInventory(inventoryId)
        For Each itemId In itemIds
            ItemData.Clear(itemId)
        Next
        ClearForColumnValue(AddressOf Initialize, TableName, InventoryIdColumn, inventoryId)
    End Sub
End Module
