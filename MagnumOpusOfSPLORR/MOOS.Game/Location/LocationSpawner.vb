Public Class LocationSpawner
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property Spawner As Spawner
        Get
            Dim spawnerId = SpawnerLocationData.ReadSpawnerForLocation(Id)
            Return If(spawnerId IsNot Nothing, New Spawner(spawnerId.Value), Nothing)
        End Get
    End Property
    ReadOnly Property Cooldown As Long
        Get
            Return SpawnerLocationData.ReadCooldownForLocation(Id).Value
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Return Spawner.Name
        End Get
    End Property

    Friend Sub Trigger(location As Location)
        Dim characterType As CharacterType = Spawner.Generate()
        If characterType IsNot Nothing Then
            CharacterData.Create(location.Id, characterType.Name, characterType.Id, 0)
        End If
    End Sub
End Class
