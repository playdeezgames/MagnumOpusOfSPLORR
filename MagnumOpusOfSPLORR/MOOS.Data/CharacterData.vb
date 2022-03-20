Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [CharacterType] INT NOT NULL,
                [LocationId] INT NOT NULL,
                FOREIGN KEY([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Create(characterType As Long, locationId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]
            (
                [CharacterType],
                [LocationId]
            ) 
            VALUES
            (
                @CharacterType,
                @LocationId
            );",
            MakeParameter("@CharacterType", characterType),
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
