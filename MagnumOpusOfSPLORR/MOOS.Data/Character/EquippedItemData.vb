﻿Public Module EquippedItemData
    Friend Const TableName = "EquippedItems"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        EquipSlotData.Initialize()
        ItemTypeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL,
                [{EquipSlotIdColumn}] INT NOT NULL,
                [{ItemTypeIdColumn}] INT NOT NULL,
                UNIQUE([CharacterIdColumn],[EquipSlotIdColumn]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{EquipSlotIdColumn}]) REFERENCES [{EquipSlotData.TableName}]([{EquipSlotData.EquipSlotIdColumn}]),
                FOREIGN KEY ([{ItemTypeIdColumn}]) REFERENCES [{ItemTypeData.TableName}]([{ItemTypeData.ItemTypeIdColumn}])
            );")
    End Sub
    Public Function ReadEquippedSlotsForCharacter(characterId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(Of Long)(
            Function(reader) CLng(reader(EquipSlotIdColumn)),
            $"SELECT [{EquipSlotIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Public Function ReadItemTypeForCharacterEquipSlot(characterId As Long, equipSlotId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{ItemTypeIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn} AND [{EquipSlotIdColumn}]=@{EquipSlotIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId))
    End Function
End Module
