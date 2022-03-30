Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn

    Public Function ReadCharacterType(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT
                [{CharacterTypeIdColumn}]
            FROM
                [{TableName}]
            WHERE
                [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Public Sub WriteCharacterType(characterId As Long, characterTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE 
                [{TableName}] 
            SET 
                [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn} 
            WHERE 
                [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId))
    End Sub

    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationIdColumn}] INT NOT NULL,
                [{CharacterTypeIdColumn}] INT NOT NULL,
                [{CharacterNameColumn}] TEXT NOT NULL,
                FOREIGN KEY([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY([{CharacterTypeIdColumn}]) REFERENCES [{CharacterTypeData.TableName}]([{CharacterTypeData.CharacterTypeIdColumn}])
            );")
    End Sub
    Function Create(locationId As Long, characterName As String, characterTypeId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{LocationIdColumn}],
                [{CharacterNameColumn}],
                [{CharacterTypeIdColumn}]
            ) 
            VALUES
            (
                @{LocationIdColumn},
                @{CharacterNameColumn},
                @{CharacterTypeIdColumn}
            );",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{CharacterNameColumn}", characterName),
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId))
        Return LastInsertRowId
    End Function
    Function ReadLocation(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT
                [{LocationIdColumn}]
            FROM
                [{TableName}]
            WHERE
                [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function
    Sub WriteLocation(characterId As Long, locationId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE 
                [{TableName}] 
            SET 
                [{LocationIdColumn}]=@{LocationIdColumn} 
            WHERE 
                [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub
    Function ReadName(characterId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            $"SELECT 
                [{CharacterNameColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function
    Function ReadForLocation(locationId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader($"{CharacterIdColumn}")),
            $"SELECT 
                [{CharacterIdColumn}] 
            FROM 
                [{TableName}] 
            WHERE 
                [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Function
    Sub ClearForLocation(locationId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM 
                [{TableName}] 
            WHERE 
                [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader($"{CharacterIdColumn}")),
                $"SELECT [{CharacterIdColumn}] FROM [{TableName}]")
        End Get
    End Property
    Sub WriteName(characterId As Long, characterName As String)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{CharacterNameColumn}]=@{CharacterNameColumn} WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{CharacterNameColumn}", characterName))
    End Sub
    Sub Clear(characterId As Long)
        Initialize()
        Dim playerCharacterId = PlayerData.Read
        If playerCharacterId.HasValue AndAlso playerCharacterId.Value = characterId Then
            PlayerData.Clear()
        End If
        CharacterInventoryData.Clear(characterId)
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Sub
End Module
