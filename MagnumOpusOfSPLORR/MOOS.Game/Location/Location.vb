Public Class Location
    ReadOnly Property Id As Long
    Property Name As String
        Get
            Return LocationData.ReadName(Id)
        End Get
        Set(value As String)
            LocationData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    ReadOnly Property Spawner As LocationSpawner
        Get
            If SpawnerLocationData.ReadSpawnerForLocation(Id).HasValue Then
                Return New LocationSpawner(Id)
            End If
            Return Nothing
        End Get
    End Property
    ReadOnly Property AvailableDirections As List(Of Direction)
        Get
            Dim unavailableDirectionIds As New HashSet(Of Long)(Routes.Select(Function(r) r.Direction.Id))
            Dim result = New HashSet(Of Direction)(AllDirections)
            result.RemoveWhere(Function(d) unavailableDirectionIds.Contains(d.Id))
            Return result.ToList
        End Get
    End Property
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property Characters As List(Of Character)
        Get
            Return CharacterData.ReadForLocation(Id).Select(Function(x) New Character(x)).ToList
        End Get
    End Property
    Function Enemies(character As Character) As List(Of Character)
        Return Characters.Where(Function(x) x <> character).ToList
    End Function
    ReadOnly Property Routes As List(Of Route)
        Get
            Return RouteData.ReadForLocation(Id).Select(Function(id) New Route(id)).ToList
        End Get
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not Characters.Any AndAlso Not Routes.Any
        End Get
    End Property
    Sub Destroy()
        If CanDestroy Then
            LocationData.Clear(Id)
        End If
    End Sub
    Property IsWinningLocation As Boolean
        Get
            Return WinningLocationData.Read(Id)
        End Get
        Set(value As Boolean)
            If IsWinningLocation Then
                WinningLocationData.Clear(Id)
            Else
                WinningLocationData.Write(Id)
            End If
        End Set
    End Property
    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = LocationInventoryData.Read(Id)
            If inventoryId.HasValue Then
                Return New Inventory(inventoryId.Value)
            End If
            Dim result = Inventories.Create()
            LocationInventoryData.Write(Id, result.Id)
            Return result
        End Get
    End Property
    Public Shared Operator =(first As Location, second As Location) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Location, second As Location) As Boolean
        Return first.Id <> second.Id
    End Operator

    Public Sub RemoveSpawner()
        SpawnerLocationData.ClearForLocation(Id)
    End Sub

    Public Sub SetSpawner(spawner As Spawner, cooldown As Long)
        SpawnerLocationData.Write(spawner.Id, Id, cooldown)
    End Sub

    Friend Sub CooldownSpawner()
        If Spawner IsNot Nothing Then
            Dim cooldown = SpawnerLocationData.ReadCooldownForLocation(Id)
            If cooldown.HasValue Then
                cooldown = cooldown.Value - 1
                If cooldown.Value <= 0 Then
                    Spawner.Trigger(Me)
                    cooldown = Spawner.Cooldown
                End If
                SpawnerLocationData.WriteCooldownForLocation(Id, cooldown.Value)
            End If
        End If
    End Sub
End Class
