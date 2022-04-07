Public Module Spawners
    Function CreateSpawner(spawnerName As String) As Spawner
        Return New Spawner(SpawnerData.Create(spawnerName))
    End Function
    ReadOnly Property AllSpawners As List(Of Spawner)
        Get
            Return SpawnerData.All.Select(Function(id) New Spawner(id)).ToList
        End Get
    End Property
End Module
