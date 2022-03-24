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
        Throw New NotImplementedException
    End Function
End Module
