Public Module DirectionData
    Friend Const TableName = "Directions"
    Friend Const DirectionIdColumn = "DirectionId"
    Friend Const DirectionNameColumn = "DirectionName"
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Directions]
            (
                [DirectionId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [DirectionName] TEXT NOT NULL
            );")
    End Sub
    Function Create(directionName As String) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Directions]([DirectionName]) VALUES(@DirectionName);",
            MakeParameter("@DirectionName", directionName))
        Return LastInsertRowId
    End Function
    Public Sub WriteName(directionId As Long, directionName As String)
        Initialize()
        ExecuteNonQuery(
            "UPDATE [Directions] SET [DirectionName]=@DirectionName WHERE [DirectionId]=@DirectionId;",
            MakeParameter("@DirectionId", directionId),
            MakeParameter("@DirectionName", directionName))
    End Sub
    Public Sub Clear(directionId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [Directions] WHERE [DirectionId]=@DirectionId;",
            MakeParameter("@DirectionId", directionId))
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader("DirectionId")),
                "SELECT [DirectionId] FROM [Directions];")
        End Get
    End Property
    Function ReadName(directionId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            "SELECT 
                [DirectionName] 
            FROM 
                [Directions] 
            WHERE 
                [DirectionId]=@DirectionId;",
            MakeParameter("@DirectionId", directionId))
    End Function
End Module
