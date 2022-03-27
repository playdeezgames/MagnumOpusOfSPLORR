Public Module PlayerData
    Friend Const TableName = "Players"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const PlayerIdColumn = "PlayerId"
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL UNIQUE,
                [{CharacterIdColumn}] INT NOT NULL,
                CHECK([{PlayerIdColumn}]=1),
                FOREIGN KEY([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Function Read() As Long?
        Initialize()
        Return ExecuteScalar(Of Long)($"SELECT [{CharacterIdColumn}] FROM [{TableName}];")
    End Function
    Sub Write(characterId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{PlayerIdColumn}],
                [{CharacterIdColumn}]
            ) 
            VALUES
            (
                1,
                @{CharacterIdColumn}
            );",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Sub
    Sub Clear()
        Initialize()
        ExecuteNonQuery($"DELETE FROM [{TableName}];")
    End Sub
End Module
