Public Module EquipSlotData
    Friend Const EquipSlotIdColumn = "EquipSlotId"
    Friend Const EquipSlotNameColumn = "EquipSlotName"
    Friend Const TableName = "EquipSlots"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{EquipSlotIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{EquipSlotNameColumn}] TEXT NOT NULL
            );")
    End Sub
    ReadOnly Property All As List(Of Long)
        Get
            Return ReadAllIds(AddressOf Initialize, TableName, EquipSlotIdColumn)
        End Get
    End Property

    Public Sub Clear(equipSlotId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, EquipSlotIdColumn, equipSlotId)
    End Sub

    Public Function ReadName(equipSlotId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, EquipSlotIdColumn, equipSlotId, EquipSlotNameColumn)
    End Function

    Public Sub WriteName(equipSlotId As Long, name As String)
        WriteColumnValue(AddressOf Initialize, TableName, EquipSlotIdColumn, equipSlotId, EquipSlotNameColumn, name)
    End Sub

    Public Function Create(equipSlotName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]([{EquipSlotNameColumn}]) VALUES (@{EquipSlotNameColumn});",
            MakeParameter($"@{EquipSlotNameColumn}", equipSlotName))
        Return LastInsertRowId
    End Function
End Module
