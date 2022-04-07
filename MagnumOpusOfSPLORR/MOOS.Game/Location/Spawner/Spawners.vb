﻿Public Module Spawners
    Function CreateSpawner(spawnerName As String, spawnNothingWeight As Long, cooldown As Long) As Spawner
        Return New Spawner(SpawnerData.Create(spawnerName, spawnNothingWeight, cooldown))
    End Function
    ReadOnly Property AllSpawners As List(Of Spawner)
        Get
            Return SpawnerData.All.Select(Function(id) New Spawner(id)).ToList
        End Get
    End Property
End Module
