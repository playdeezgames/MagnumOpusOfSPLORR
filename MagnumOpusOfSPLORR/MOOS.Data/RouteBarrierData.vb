Public Module RouteBarrierData
    Friend Const TableName = "RouteBarriers"
    Friend Const RouteIdColumn = RouteData.RouteIdColumn
    Friend Const BarrierIdColumn = BarrierData.BarrierIdColumn
    Friend Sub Initialize()
        RouteData.Initialize()
        BarrierData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{RouteIdColumn}] INT NOT NULL,
                [{BarrierIdColumn}] INT NOT NULL,
                UNIQUE([{RouteIdColumn}],[{BarrierIdColumn}]),
                FOREIGN KEY ([{RouteIdColumn}]) REFERENCES [{RouteData.TableName}]([{RouteData.RouteIdColumn}]),
                FOREIGN KEY ([{BarrierIdColumn}]) REFERENCES [{BarrierData.TableName}]([{BarrierData.BarrierIdColumn}])
            );")
    End Sub
    Function ReadForRoute(routeId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader(BarrierIdColumn)),
            $"SELECT [{BarrierIdColumn}] FROM [{TableName}] WHERE [{RouteIdColumn}]=@{RouteIdColumn}",
            MakeParameter($"@{RouteIdColumn}", routeId))
    End Function

    Public Sub Clear(routeId As Long, barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{RouteIdColumn}]=@{RouteIdColumn} AND [{BarrierIdColumn}]=@{BarrierIdColumn};",
            MakeParameter($"{RouteIdColumn}", routeId),
            MakeParameter($"{BarrierIdColumn}", barrierId))
    End Sub

    Public Sub Write(routeId As Long, barrierId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{RouteIdColumn}],[{BarrierIdColumn}]) VALUES(@{RouteIdColumn},@{BarrierIdColumn});",
            MakeParameter($"{RouteIdColumn}", routeId),
            MakeParameter($"{BarrierIdColumn}", barrierId))
    End Sub
End Module
