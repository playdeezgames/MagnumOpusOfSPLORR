Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT
            );")
    End Sub
    Function Create() As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Locations]
            DEFAULT VALUES;")
        Return LastInsertRowId
    End Function
End Module
