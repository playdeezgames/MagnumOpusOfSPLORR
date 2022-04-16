Public Class Spawner
    ReadOnly Property Id As Long

    Sub New(spawnerId As Long)
        Id = spawnerId
    End Sub

    ReadOnly Property CharacterTypes As Dictionary(Of CharacterType, Integer)
        Get
            Dim table = SpawnerCharacterTypeData.ReadForSpawner(Id)
            Dim result As New Dictionary(Of CharacterType, Integer)
            For Each entry In table
                result(New CharacterType(entry.Key)) = CInt(entry.Value)
            Next
            Return result
        End Get
    End Property

    Sub SetCharacterTypeWeight(characterType As CharacterType, weight As Integer)
        If weight > 0 Then
            SpawnerCharacterTypeData.Write(Id, characterType.Id, weight)
        Else
            SpawnerCharacterTypeData.Clear(Id, characterType.Id)
        End If
    End Sub

    Property Name As String
        Get
            Return SpawnerData.ReadName(Id)
        End Get
        Set(value As String)
            SpawnerData.WriteName(Id, value)
        End Set
    End Property

    Property SpawnNothingWeight As Long
        Get
            Return SpawnerData.ReadSpawnNothingWeight(Id).Value
        End Get
        Set(value As Long)
            SpawnerData.WriteSpawnNothingWeight(Id, value)
        End Set
    End Property

    Property Cooldown As Long
        Get
            Return SpawnerData.ReadCooldown(Id).Value
        End Get
        Set(value As Long)
            SpawnerData.WriteCooldown(Id, value)
        End Set
    End Property

    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    ReadOnly Property CanDestroy As Boolean
        Get
            Return True
        End Get
    End Property

    Public Sub Destroy()
        SpawnerData.Clear(Id)
    End Sub
End Class
