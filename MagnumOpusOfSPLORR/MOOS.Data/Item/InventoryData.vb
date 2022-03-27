Public Module InventoryData
    Public Const TableName = "Inventories"
    Public Const InventoryIdColumn = "InventoryId"
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
End Module
