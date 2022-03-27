Public Module WinningLocationData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery("CREATE TABLE IF NOT EXISTS [WinningLocations]([LocationId] INT NOT NULL UNIQUE);")
    End Sub
    Function Read(locationId As Long) As Boolean
        Initialize()
        Return ExecuteReader(Of Long)(
            Function(reader) CLng(reader("LocationId")),
            "SELECT [LocationId] FROM [WinningLocations] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId)).Any
    End Function
End Module
