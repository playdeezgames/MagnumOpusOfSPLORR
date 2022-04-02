Public Module BarrierData
    Friend Const TableName = "Barriers"
    Friend Const BarrierIdColumn = "BarrierId"
    Friend Const ItemTypeIdColumn = ItemTypeData.ItemTypeIdColumn
    Friend Const DestroyItemColumn = "DestroysItem"
    Friend Const SelfDestructsColumn = "SelfDestructs"
    Friend Sub Initialize()
        ItemTypeData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{BarrierIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{ItemTypeIdColumn}] INT NOT NULL,
                [{DestroyItemColumn}] INT NOT NULL,
                [{SelfDestructsColumn}] INT NOT NULL,
                FOREIGN KEY ([{ItemTypeIdColumn}]) REFERENCES [{ItemTypeData.TableName}]([{ItemTypeData.ItemTypeIdColumn}])
            );")
    End Sub

    Public Function Create(itemTypeId As Long, destroysItem As Boolean, selfDestructs As Boolean) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{ItemTypeIdColumn}],
                [{DestroyItemColumn}],
                [{SelfDestructsColumn}]
            ) 
            VALUES
            (
                @{ItemTypeIdColumn},
                @{DestroyItemColumn},
                @{SelfDestructsColumn}
            );",
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId),
            MakeParameter($"@{DestroyItemColumn}", destroysItem),
            MakeParameter($"@{SelfDestructsColumn}", selfDestructs))
        Return LastInsertRowId
    End Function

    Public Sub Clear(barrierId As Long)
        Initialize()
        RouteBarrierData.ClearForBarrier(barrierId)
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Sub

    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader($"{BarrierIdColumn}")),
                $"SELECT [{BarrierIdColumn}] FROM [{TableName}];")
        End Get
    End Property

    Public Function ReadItemType(barrierId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, ItemTypeIdColumn)
    End Function

    Public Sub WriteItemType(barrierId As Long, itemTypeId As Long)
        WriteColumnValue(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, ItemTypeIdColumn, itemTypeId)
    End Sub

    Public Function ReadDestroysItem(barrierId As Long) As Boolean?
        Dim result = ReadColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, DestroyItemColumn)
        Return If(result.HasValue, CBool(result.Value), Nothing)
    End Function

    Public Sub WriteDestroysItem(barrierId As Long)
        WriteColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, DestroyItemColumn, 1)
    End Sub

    Public Sub ClearDestroysItem(barrierId As Long)
        WriteColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, DestroyItemColumn, 0)
    End Sub

    Public Function ReadSelfDestructs(barrierId As Long) As Boolean?
        Dim result = ReadColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, SelfDestructsColumn)
        Return If(result.HasValue, CBool(result.Value), Nothing)
    End Function

    Public Sub WriteSelfDestructs(barrierId As Long)
        WriteColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, SelfDestructsColumn, 1)
    End Sub

    Public Sub ClearSelfDestructs(barrierId As Long)
        WriteColumnValue(Of Long)(AddressOf Initialize, TableName, BarrierIdColumn, barrierId, SelfDestructsColumn, 0)
    End Sub
End Module
