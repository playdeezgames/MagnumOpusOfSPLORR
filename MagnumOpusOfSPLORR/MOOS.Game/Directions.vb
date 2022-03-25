Imports MOOS.Data

Public Module Directions
    Function CreateDirection(directionName As String) As Direction
        Return New Direction(DirectionData.Create(directionName))
    End Function
End Module
