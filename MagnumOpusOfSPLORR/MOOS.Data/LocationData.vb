Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationType] INT NOT NULL
            );")
    End Sub
    Function Create(locationType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Locations]
            (
                [LocationType]
            ) 
            VALUES
            (
                @LocationType
            );",
            MakeParameter("@LocationType", locationType))
        Return LastInsertRowId
    End Function
End Module
