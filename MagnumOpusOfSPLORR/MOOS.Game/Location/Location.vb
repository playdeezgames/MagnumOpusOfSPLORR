Imports MOOS.Data

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
    ReadOnly Property IsWinningLocation As Boolean
        Get
            Return WinningLocationData.Read(Id)
        End Get
    End Property
End Class
