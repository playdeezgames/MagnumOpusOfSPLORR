Public Class Route
    ReadOnly Property Id As Long
    Sub New(routeId As Long)
        Id = routeId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Return $"{Direction.Name} from {FromLocation.Name} to {ToLocation.Name}"
        End Get
    End Property
    ReadOnly Property FromLocation As Location
        Get
            Return New Location(RouteData.ReadFromLocation(Id).Value)
        End Get
    End Property

    ReadOnly Property ToLocation As Location
        Get
            Return New Location(RouteData.ReadToLocation(Id).Value)
        End Get
    End Property
    ReadOnly Property Direction As Direction
        Get
            Return New Direction(RouteData.ReadDirection(Id).Value)
        End Get
    End Property
    Public Sub RemoveBarrier(barrier As Barrier)
        RouteBarrierData.Clear(Id, barrier.Id)
    End Sub
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not Barriers.Any
        End Get
    End Property
    Sub Destroy()
        RouteData.Clear(Id)
    End Sub
    ReadOnly Property Barriers As List(Of Barrier)
        Get
            Return RouteBarrierData.ReadForRoute(Id).Select(Function(id) New Barrier(id)).ToList
        End Get
    End Property
    ReadOnly Property AvailableBarriers As List(Of Barrier)
        Get
            Return RouteBarrierData.ReadAvailableForRoute(Id).Select(Function(id) New Barrier(id)).ToList
        End Get
    End Property
    Public Sub AddBarrier(barrier As Barrier)
        RouteBarrierData.Write(Id, barrier.Id)
    End Sub
    ReadOnly Property PassRequirements As Dictionary(Of ItemType, Long)
        Get
            Dim result As New Dictionary(Of ItemType, Long)
            Dim groups = Barriers.GroupBy(Function(barrier) barrier.ItemType.Id)
            For Each group In groups
                result.Add(New ItemType(group.Key), group.LongCount)
            Next
            Return result
        End Get
    End Property
    ReadOnly Property PassCosts As Dictionary(Of ItemType, Long)
        Get
            Dim result As New Dictionary(Of ItemType, Long)
            Dim groups = Barriers.Where(Function(barrier) barrier.DestroysItem).GroupBy(Function(barrier) barrier.ItemType.Id)
            For Each group In groups
                result.Add(New ItemType(group.Key), group.LongCount)
            Next
            Return result
        End Get
    End Property
    Friend Sub SelfDestructBarriers()
        For Each barrier In Barriers.Where(Function(b) b.SelfDestructs)
            BarrierData.Clear(barrier.Id)
        Next
    End Sub
    Public Shared Operator =(first As Route, second As Route) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Route, second As Route) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
