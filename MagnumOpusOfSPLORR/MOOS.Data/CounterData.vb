Public Module CounterData
    Friend Const TableName = "Counters"
    Friend Const CounterIdColumn = "CounterId"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const CounterTypeColumn = "CounterType"
    Friend Const CounterValueColumn = "CounterValue"
    Friend Sub Initialize()
        CounterData.Initialize()
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
End Module
