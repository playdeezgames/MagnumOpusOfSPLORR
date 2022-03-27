Public Module ItemTypeData
    Public Const TableName = "ItemTypes"
    Public Const ItemTypeIdColumn = "ItemTypeId"
    Public Const ItemTypeNameColumn = "ItemTypeName"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemTypeIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{ItemTypeNameColumn}] TEXT NOT NULL
            );")
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader($"{ItemTypeIdColumn}")),
                $"SELECT [{ItemTypeIdColumn}] FROM [{TableName}];")
        End Get
    End Property

    Public Sub WriteName(itemTypeId As Long, itemTypeName As String)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{ItemTypeNameColumn}]=@{ItemTypeNameColumn} WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId),
            MakeParameter($"@{ItemTypeNameColumn}", itemTypeName))
    End Sub

    Public Sub Clear(itemTypeId As Long)
        Initialize()
        ItemData.ClearForItemType(itemTypeId)
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId))
    End Sub

    Public Function ReadName(itemTypeId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            $"SELECT [{ItemTypeNameColumn}] FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId))
    End Function
    Function Create(itemTypeName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}] 
            (
                [{ItemTypeNameColumn}]
            ) 
            VALUES
            (
                @{ItemTypeNameColumn}
            );",
            MakeParameter($"@{ItemTypeNameColumn}", itemTypeName))
        Return LastInsertRowId
    End Function
End Module
