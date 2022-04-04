Public Module ItemTypeData
    Friend Const TableName = "ItemTypes"
    Friend Const ItemTypeIdColumn = "ItemTypeId"
    Friend Const ItemTypeNameColumn = "ItemTypeName"
    Friend Const HealDiceColumn = "HealDice"
    Friend Const AttackDiceColumn = "AttackDice"
    Friend Const DefendDiceColumn = "DefendDice"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemTypeIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{ItemTypeNameColumn}] TEXT NOT NULL,
                [{HealDiceColumn}] TEXT NULL,
                [{AttackDiceColumn}] TEXT NULL,
                [{DefendDiceColumn}] TEXT NULL
            );")
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, ItemTypeIdColumn)
        End Get
    End Property

    Public Sub WriteHealDice(itemTypeId As Long, healDice As String)
        WriteColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, HealDiceColumn, healDice)
    End Sub

    Public Function ReadHealDice(itemTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, HealDiceColumn)
    End Function

    Public Sub WriteAttackDice(itemTypeId As Long, attackDice As String)
        WriteColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, AttackDiceColumn, attackDice)
    End Sub

    Public Function ReadDefendDice(itemTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, DefendDiceColumn)
    End Function

    Public Sub WriteDefendDice(itemTypeId As Long, defendDice As String)
        WriteColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, DefendDiceColumn, defendDice)
    End Sub

    Public Function ReadAttackDice(itemTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, AttackDiceColumn)
    End Function

    Public Sub WriteName(itemTypeId As Long, itemTypeName As String)
        WriteColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, ItemTypeNameColumn, itemTypeName)
    End Sub

    Public Sub Clear(itemTypeId As Long)
        Initialize()
        ItemData.ClearForItemType(itemTypeId)
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{ItemTypeIdColumn}]=@{ItemTypeIdColumn};",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId))
    End Sub

    Public Function ReadName(itemTypeId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, ItemTypeNameColumn)
    End Function
    Function Create(itemTypeName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}] 
            (
                [{ItemTypeNameColumn}]
            ) 
            VALUES
            (
                @{ItemTypeNameColumn}
            );",
            MakeParameter($"@{ItemTypeNameColumn}", itemTypeName))
        Return LastInsertRowId
    End Function
    Function ReadForName(itemTypeName As String) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader(ItemTypeIdColumn)),
            $"SELECT [{ItemTypeIdColumn}] FROM [{TableName}] WHERE [{ItemTypeNameColumn}]=@{ItemTypeNameColumn};",
            MakeParameter($"@{ItemTypeNameColumn}", itemTypeName))
    End Function
End Module
