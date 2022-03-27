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

    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader($"{BarrierIdColumn}")),
                $"SELECT [{BarrierIdColumn}] FROM [{TableName}];")
        End Get
    End Property

    Public Function ReadItemType(barrierId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{ItemTypeIdColumn}] FROM [{TableName}] WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Function

    Public Sub WriteItemType(barrierId As Long, itemTypeId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{ItemTypeIdColumn}]=@{ItemTypeIdColumn} WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId),
            MakeParameter($"@{ItemTypeIdColumn}", itemTypeId))
    End Sub

    Public Function ReadDestroysItem(barrierId As Long) As Boolean?
        Dim result = ExecuteScalar(Of Long)(
            $"SELECT [{DestroyItemColumn}] FROM [{TableName}] WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
        Return If(result.HasValue, CBool(result.Value), Nothing)
    End Function

    Public Sub WriteDestroysItem(barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{DestroyItemColumn}]=1 WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Sub

    Public Sub ClearDestroysItem(barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{DestroyItemColumn}]=0 WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Sub

    Public Function ReadSelfDestructs(barrierId As Long) As Boolean?
        Dim result = ExecuteScalar(Of Long)(
            $"SELECT [{SelfDestructsColumn}] FROM [{TableName}] WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
        Return If(result.HasValue, CBool(result.Value), Nothing)
    End Function

    Public Sub WriteSelfDestructs(barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{SelfDestructsColumn}]=1 WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Sub

    Public Sub ClearSelfDestructs(barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{SelfDestructsColumn}]=0 WHERE [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"@{BarrierIdColumn}", barrierId))
    End Sub
End Module
