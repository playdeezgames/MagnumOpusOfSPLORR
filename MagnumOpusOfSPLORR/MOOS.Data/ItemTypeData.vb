Public Module ItemTypeData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [ItemTypes]
            (
                [ItemTypeId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [ItemTypeName] TEXT NOT NULL
            );")
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader("ItemTypeId")),
                "SELECT [ItemTypeId] FROM [ItemTypes];")
        End Get
    End Property

    Public Sub WriteName(itemTypeId As Long, itemTypeName As String)
        Initialize()
        ExecuteNonQuery(
            "UPDATE [ItemTypes] SET [ItemTypeName]=@ItemTypeName WHERE [ItemTypeId]=@ItemTypeId;",
            MakeParameter("@ItemTypeId", itemTypeId),
            MakeParameter("@ItemTypeName", itemTypeName))
    End Sub

    Public Sub Clear(itemTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [ItemTypes] WHERE [ItemTypeId]=@ItemTypeId;",
            MakeParameter("@ItemTypeId", itemTypeId))
    End Sub

    Public Function ReadName(itemTypeId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            "SELECT [ItemTypeName] FROM [ItemTypes] WHERE [ItemTypeId]=@ItemTypeId;",
            MakeParameter("@ItemTypeId", itemTypeId))
    End Function
    Function Create(itemTypeName As String) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [ItemTypes] 
            (
                [ItemTypeName]
            ) 
            VALUES
            (
                @ItemTypeName
            );",
            MakeParameter("@ItemTypeName", itemTypeName))
        Return LastInsertRowId
    End Function
End Module
