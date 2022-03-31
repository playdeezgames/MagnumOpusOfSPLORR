Public Module CounterData
    Friend Const TableName = "Counters"
    Friend Const CounterIdColumn = "CounterId"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const CounterTypeColumn = "CounterType"
    Friend Const CounterValueColumn = "CounterValue"
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
        Initialize()
        ExecuteNonQuery(
            $"UPDATE [{TableName}] SET [{CounterValueColumn}]=@{CounterValueColumn} WHERE [{CounterIdColumn}]=@{CounterIdColumn};",
            MakeParameter($"@{CounterIdColumn}", counterId),
            MakeParameter($"@{CounterValueColumn}", counterValue))
    End Sub

    Function ReadForCharacter(characterId As Long) As List(Of Long)
        Initialize()
        Return ExecuteReader(
            Function(reader) CLng(reader(CounterIdColumn)),
            $"SELECT [{CounterIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Public Function ReadCounterType(counterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{CounterTypeColumn}] FROM [{TableName}] WHERE [{CounterIdColumn}]=@{CounterIdColumn};",
            MakeParameter($"@{CounterIdColumn}", counterId))
    End Function

    Public Function ReadCounterValue(counterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{CounterValueColumn}] FROM [{TableName}] WHERE [{CounterIdColumn}]=@{CounterIdColumn};",
            MakeParameter($"@{CounterIdColumn}", counterId))
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
