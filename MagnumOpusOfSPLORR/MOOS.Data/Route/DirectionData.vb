Public Module DirectionData
    Friend Const TableName = "Directions"
    Friend Const DirectionIdColumn = "DirectionId"
    Friend Const DirectionNameColumn = "DirectionName"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DirectionIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{DirectionNameColumn}] TEXT NOT NULL
            );")
    End Sub
    Function Create(directionName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]([{DirectionNameColumn}]) VALUES(@{DirectionNameColumn});",
            MakeParameter($"@{DirectionNameColumn}", directionName))
        Return LastInsertRowId
    End Function
    Public Sub WriteName(directionId As Long, directionName As String)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{DirectionNameColumn}]=@{DirectionNameColumn} WHERE [{DirectionIdColumn}]=@{DirectionIdColumn};",
            MakeParameter($"@{DirectionIdColumn}", directionId),
            MakeParameter($"@{DirectionNameColumn}", directionName))
    End Sub
    Public Sub Clear(directionId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{DirectionIdColumn}]=@{DirectionIdColumn};",
            MakeParameter($"@{DirectionIdColumn}", directionId))
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader($"{DirectionIdColumn}")),
                $"SELECT [{DirectionIdColumn}] FROM [{TableName}];")
        End Get
    End Property
    Function ReadName(directionId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            $"SELECT 
                [{DirectionNameColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{DirectionIdColumn}]=@{DirectionIdColumn};",
            MakeParameter($"@{DirectionIdColumn}", directionId))
    End Function
End Module
