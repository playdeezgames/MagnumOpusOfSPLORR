Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationName] TEXT NOT NULL
            );")
    End Sub
    Function Create(locationName As String) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Locations]
            (
                [LocationName]
            )
            VALUES
            (
                @LocationName
            );",
            MakeParameter("@LocationName", locationName))
        Return LastInsertRowId
    End Function
    ReadOnly Property All As List(Of Long)
        Get
            Initialize()
            Return ExecuteReader(
                Function(reader) CLng(reader("LocationId")),
                "SELECT [LocationId] FROM [Locations];")
        End Get
    End Property
    Function ReadName(locationId As Long) As String
        Initialize()
        Return ExecuteScalar(
            Function(x) CStr(x),
            "SELECT [LocationName] FROM [Locations] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Function
    Sub WriteName(locationId As Long, locationName As String)
        Initialize()
        ExecuteNonQuery(
            "UPDATE [Locations] SET [LocationName]=@LocationName WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@LocationName", locationName))
    End Sub
    Sub Clear(locationId As Long)
        Initialize()
        CharacterData.ClearForLocation(locationId)
        RouteData.ClearForLocation(locationId)
        LocationInventoryData.Clear(locationId)
        ExecuteNonQuery(
            "DELETE FROM [Locations] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Sub
End Module
