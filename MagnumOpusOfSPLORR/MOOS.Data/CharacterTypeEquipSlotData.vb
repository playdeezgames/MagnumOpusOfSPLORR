Public Module CharacterTypeEquipSlotData
    Friend Const TableName = "CharacterTypeEquipSlots"
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Sub Initialize()
        EquipSlotData.Initialize()
        CharacterTypeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterTypeIdColumn}] INT NOT NULL,
                [{EquipSlotIdColumn}] INT NOT NULL,
                UNIQUE([{CharacterTypeIdColumn}],[{EquipSlotIdColumn}])
            );")
    End Sub

    Public Function ReadCountForEquipSlot(equipSlotId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{EquipSlotIdColumn}]=@{EquipSlotIdColumn};",
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId)).Value
    End Function

    Public Function ReadForCharacterType(characterTypeId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(Of Long)(
            Function(reader) CLng(reader(EquipSlotIdColumn)),
            $"SELECT [{EquipSlotIdColumn}] FROM [{TableName}] WHERE [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn};",
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId))
    End Function

    Public Sub Clear(characterTypeId As Long, equipSlotId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{CharacterTypeIdColumn}]=@{CharacterTypeIdColumn} AND [{EquipSlotIdColumn}]=@{EquipSlotIdColumn};",
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId),
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId))
    End Sub

    Public Sub Write(characterTypeId As Long, equipSlotId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{CharacterTypeIdColumn}], [{EquipSlotIdColumn}]) VALUES(@{CharacterTypeIdColumn},@{EquipSlotIdColumn});",
            MakeParameter($"@{CharacterTypeIdColumn}", characterTypeId),
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId))
    End Sub
End Module
