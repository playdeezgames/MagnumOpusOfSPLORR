Public Module WinningLocationData
    Friend Const TableName = "WinningLocations"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery($"CREATE TABLE IF NOT EXISTS [{TableName}]([{LocationIdColumn}] INT NOT NULL UNIQUE);")
    End Sub
    Function Read(locationId As Long) As Boolean 'TODO: this is a READER
        Initialize()
        Return ExecuteReader(Of Long)(
            Function(reader) CLng(reader($"{LocationIdColumn}")),
            $"SELECT [{LocationIdColumn}] FROM [{TableName}] WHERE [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId)).Any
    End Function

    Public Sub Clear(locationId As Long)
        Initialize()
        ExecuteNonQuery(
            $"DELETE FROM [{TableName}] WHERE [{LocationIdColumn}]=@{LocationIdColumn};",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub

    Public Sub Write(locationId As Long) 'TODO: this is a REPLACE
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}] ([{LocationIdColumn}]) VALUES (@{LocationIdColumn});",
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub
End Module
