Public Module DirectionData
    Friend Sub Initialize()
        ExecuteNonQuery("CREATE TABLE IF NOT EXISTS [Directions]([DirectionId],[DirectionName]);")
    End Sub
    Function Create(directionName As String) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Directions]([DirectionName]) VALUES(@DirectionName);",
            MakeParameter("@DirectionName", directionName))
        Return LastInsertRowId
    End Function
End Module
