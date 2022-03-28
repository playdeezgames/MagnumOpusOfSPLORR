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
            Function(reader) CLng(reader(RouteIdColumn)),
            $"SELECT [{BarrierIdColumn}] FROM [{TableName}] WHERE [{RouteIdColumn}]=@{RouteIdColumn}",
            MakeParameter($"@{RouteIdColumn}", routeId))
    End Function
End Module
