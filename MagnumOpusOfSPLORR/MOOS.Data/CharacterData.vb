Public Module CharacterData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [CharacterType] INT NOT NULL
            );")
    End Sub
    Function Create(characterType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]
            (
                [CharacterType]
            ) 
            VALUES
            (
                @CharacterType
            );",
            MakeParameter("@CharacterType", characterType))
        Return LastInsertRowId
    End Function
End Module
