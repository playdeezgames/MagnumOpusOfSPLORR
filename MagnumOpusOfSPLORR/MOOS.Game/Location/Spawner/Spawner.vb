Public Class Spawner
    ReadOnly Property Id As Long
    Sub New(spawnerId As Long)
        Id = spawnerId
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
