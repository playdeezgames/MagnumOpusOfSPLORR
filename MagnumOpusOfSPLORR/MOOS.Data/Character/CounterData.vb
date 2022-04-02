Public Module CounterData
    Friend Const TableName = "Counters"
    Friend Const CounterIdColumn = "CounterId"
    Friend Const CounterTypeColumn = "CounterType"
    Friend Const CounterValueColumn = "CounterValue"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CounterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterIdColumn}] INT NOT NULL,
                [{CounterTypeColumn}] INT NOT NULL,
                [{CounterValueColumn}] INT NOT NULL,
                UNIQUE([{CharacterIdColumn}],[{CounterTypeColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub

    Public Sub WriteCounterValue(counterId As Long, counterValue As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CounterIdColumn, counterId, CounterValueColumn, counterValue)
    End Sub

    Function ReadForCharacter(characterId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader(CounterIdColumn)),
            $"SELECT [{CounterIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Public Function ReadCounterType(counterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CounterIdColumn, counterId, CounterTypeColumn)
    End Function

    Public Function ReadCounterValue(counterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CounterIdColumn, counterId, CounterTypeColumn)
    End Function

    Public Function Create(characterId As Long, counterType As Long, value As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{CharacterIdColumn}],
                [{CounterTypeColumn}],
                [{CounterValueColumn}]
            ) 
            VALUES
            (
                @{CharacterIdColumn},
                @{CounterTypeColumn},
                @{CounterValueColumn}
            );",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{CounterTypeColumn}", counterType),
            MakeParameter($"@{CounterValueColumn}", value))
        Return LastInsertRowId
    End Function
End Module
