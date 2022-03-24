Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationId] INT NOT NULL,
                FOREIGN KEY([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Create(locationId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]
            (
                [LocationId]
            ) 
            VALUES
            (
                @LocationId
            );",
            MakeParameter("@LocationId", locationId))
        Return LastInsertRowId
    End Function
    Function ReadLocationId(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                [LocationId]
            FROM
                [Characters]
            WHERE
                [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Function
End Module
