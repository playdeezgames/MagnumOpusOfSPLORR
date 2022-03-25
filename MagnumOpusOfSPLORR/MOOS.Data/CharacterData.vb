Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationId] INT NOT NULL,
                [CharacterName] TEXT NOT NULL,
                FOREIGN KEY([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Create(locationId As Long, characterName As String) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]
            (
                [LocationId],
                [CharacterName]
            ) 
            VALUES
            (
                @LocationId,
                @CharacterName
            );",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@CharacterName", characterName))
        Return LastInsertRowId
    End Function
    Function ReadLocation(characterId As Long) As Long?
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
    Function ReadName(characterId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            "SELECT 
                [CharacterName] 
            FROM 
                [Characters] 
            WHERE 
                [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Function
    Function ReadForLocation(locationId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader("CharacterId")),
            "SELECT 
                [CharacterId] 
            FROM 
                [Characters] 
            WHERE 
                [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Function
    Sub ClearForLocation(locationId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM 
                [Characters] 
            WHERE 
                [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Sub
End Module
