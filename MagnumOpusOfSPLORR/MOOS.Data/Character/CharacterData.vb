Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const WoundsColumn = "Wounds"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn

    Public Function ReadCharacterType(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterTypeIdColumn)
    End Function

    Public Function ReadForCharacterType(characterTypeId As Long) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, CharacterTypeIdColumn, characterTypeId)
    End Function
    Public Sub WriteCharacterType(characterId As Long, characterTypeId As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterTypeIdColumn, characterTypeId)
    End Sub
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationIdColumn}] INT NOT NULL,
                [{CharacterTypeIdColumn}] INT NOT NULL,
                [{CharacterNameColumn}] TEXT NOT NULL,
                [{WoundsColumn}] INT NOT NULL,
                FOREIGN KEY([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY([{CharacterTypeIdColumn}]) REFERENCES [{CharacterTypeData.TableName}]([{CharacterTypeData.CharacterTypeIdColumn}])
            );")
    End Sub
    Function Create(locationId As Long, characterName As String, characterTypeId As Long, wounds As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{LocationIdColumn}],
                [{CharacterNameColumn}],
                [{CharacterTypeIdColumn}],
                [{WoundsColumn}]
            ) 
            VALUES
            (
                @{LocationIdColumn},
                @{CharacterNameColumn},
                @{CharacterTypeIdColumn},
                @{WoundsColumn}
            );",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{CharacterNameColumn}", characterName),
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId),
            MakeParameter($"@{WoundsColumn}", wounds))
        Return LastInsertRowId
    End Function
    Function ReadLocation(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, LocationIdColumn)
    End Function
    Sub WriteLocation(characterId As Long, locationId As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, LocationIdColumn, locationId)
    End Sub
    Function ReadName(characterId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn)
    End Function
    Function ReadForLocation(locationId As Long) As List(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, LocationIdColumn, locationId)
    End Function
    Sub ClearForLocation(locationId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, LocationIdColumn, locationId)
    End Sub
    Public Sub WriteWounds(characterId As Long, wounds As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, WoundsColumn, wounds)
    End Sub
    Public Function ReadWounds(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, WoundsColumn)
    End Function
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, CharacterIdColumn)
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
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub
End Module
