Public Module CharacterTypeData
    Friend Const TableName = "CharacterTypes"
    Friend Const CharacterTypeIdColumn = "CharacterTypeId"
    Friend Const CharacterTypeNameColumn = "CharacterTypeName"
    Friend Const HealthColumn = "Health"
    Friend Const AttackDiceColumn = "AttackDice"
    Friend Const DefendDiceColumn = "DefendDice"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterTypeIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterTypeNameColumn}] TEXT NOT NULL,
                [{HealthColumn}] INT NOT NULL,
                [{AttackDiceColumn}] TEXT NOT NULL,
                [{DefendDiceColumn}] TEXT NOT NULL
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
    Public Function ReadAttackDice(characterTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, AttackDiceColumn)
    End Function
    Public Sub WriteAttackDice(characterTypeId As Long, damageDice As String)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, AttackDiceColumn, damageDice)
    End Sub
    Public Sub WriteDefendDice(characterTypeId As Long, armorDice As String)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, DefendDiceColumn, armorDice)
    End Sub
    Public Function ReadDefendDice(characterTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, DefendDiceColumn)
    End Function
    Public Sub WriteHealth(characterTypeId As Long, health As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, HealthColumn, health)
    End Sub

    Public Function ReadHealth(characterTypeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, HealthColumn)
    End Function

    Public Sub Clear(characterTypeId As Long)
        Dim characterIds = CharacterData.ReadForCharacterType(characterTypeId)
        For Each characterId In characterIds
            CharacterData.Clear(characterId)
        Next
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId)
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, CharacterTypeIdColumn)
        End Get
    End Property

    Public Function Create(characterTypeName As String, health As Long, damageDice As String, armorDice As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{CharacterTypeNameColumn}],
                [{HealthColumn}],
                [{AttackDiceColumn}],
                [{DefendDiceColumn}]
            ) 
            VALUES
            (
                @{CharacterTypeNameColumn},
                @{HealthColumn},
                @{AttackDiceColumn},
                @{DefendDiceColumn}
            );",
            MakeParameter($"@{CharacterTypeNameColumn}", characterTypeName),
            MakeParameter($"@{HealthColumn}", health),
            MakeParameter($"@{AttackDiceColumn}", damageDice),
            MakeParameter($"@{DefendDiceColumn}", armorDice))
        Return LastInsertRowId
    End Function

    Public Function ReadName(characterTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterTypeIdColumn, characterTypeId, CharacterTypeNameColumn)
    End Function
End Module
