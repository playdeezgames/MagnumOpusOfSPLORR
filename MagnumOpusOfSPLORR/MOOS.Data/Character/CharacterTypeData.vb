Public Module CharacterTypeData
    Friend Const TableName = "CharacterTypes"
    Friend Const CharacterTypeIdColumn = "CharacterTypeId"
    Friend Const CharacterTypeNameColumn = "CharacterTypeName"
    Friend Const HealthColumn = "Health"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterTypeIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterTypeNameColumn}] TEXT NOT NULL,
                [{HealthColumn}] INT NOT NULL
            );")
    End Sub

    Public Sub WriteName(characterTypeId As Long, characterTypeName As String)
        WriteColumnValue(
            AddressOf Initialize,
            TableName,
            CharacterTypeIdColumn,
            characterTypeId,
            CharacterTypeNameColumn,
            characterTypeName)
    End Sub

    Public Sub WriteHealth(characterTypeId As Long, health As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, HealthColumn, health)
    End Sub

    Public Function ReadHealth(characterTypeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, HealthColumn)
    End Function

    Public Sub Clear(characterTypeId As Long)
        Initialize()
        Dim characterIds = CharacterData.ReadForCharacterType(characterTypeId)
        For Each characterId In characterIds
            CharacterData.Clear(characterId)
        Next
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn};",
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId))
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader(CharacterTypeIdColumn)),
                $"SELECT [{CharacterTypeIdColumn}] FROM [{TableName}];")
        End Get
    End Property

    Public Function Create(characterTypeName As String, health As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{CharacterTypeNameColumn}],
                [{HealthColumn}]
            ) 
            VALUES
            (
                @{CharacterTypeNameColumn},
                @{HealthColumn}
            );",
            MakeParameter($"@{CharacterTypeNameColumn}", characterTypeName),
            MakeParameter($"@{HealthColumn}", health))
        Return LastInsertRowId
    End Function

    Public Function ReadName(characterTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, CharacterTypeNameColumn)
    End Function
End Module
