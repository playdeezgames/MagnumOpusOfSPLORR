Public Module ItemTypeEquipSlotData
    Friend Const TableName = "ItemTypeEquipSlots"
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Sub Initialize()
        ItemTypeData.Initialize()
        EquipSlotData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemTypeIdColumn}] INT NOT NULL UNIQUE,
                [{EquipSlotIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{ItemTypeIdColumn}]) REFERENCES [{ItemTypeData.TableName}]([{ItemTypeData.ItemTypeIdColumn}]),
                FOREIGN KEY ([{EquipSlotIdColumn}]) REFERENCES [{EquipSlotData.TableName}]([{EquipSlotData.EquipSlotIdColumn}])
            );")
    End Sub
    Public Function Read(itemTypeId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId, EquipSlotIdColumn)
    End Function

    Public Sub Clear(itemTypeId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemTypeIdColumn, itemTypeId)
    End Sub

    Public Sub Write(itemTypeId As Long, equipSlotId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{ItemTypeIdColumn}],
                [{EquipSlotIdColumn}]
            ) 
            VALUES
            (
                @{ItemTypeIdColumn},
                @{EquipSlotIdColumn}
            );",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId),
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId))
    End Sub

    Public Function ReadCountForEquipSlot(equipSlotId As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{EquipSlotIdColumn}]=@{EquipSlotIdColumn};",
            MakeParameter($"@{EquipSlotIdColumn}", equipSlotId)).Value
    End Function
End Module
