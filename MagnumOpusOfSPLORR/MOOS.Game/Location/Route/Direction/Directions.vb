Imports MOOS.Data

Public Module Directions
    Function CreateDirection(directionName As String) As Direction
        Return New Direction(DirectionData.Create(directionName))
    End Function
    ReadOnly Property AllDirections As List(Of Direction)
        Get
            Return DirectionData.All.Select(Function(id) New Direction(id)).ToList
        End Get
    End Property
End Module
