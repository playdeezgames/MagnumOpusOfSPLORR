Imports MOOS.Data

Public Class Direction
    ReadOnly Property Id As Long
    Sub New(directionId As Long)
        Id = directionId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Property Name As String
        Get
            Return DirectionData.ReadName(Id)
        End Get
        Set(value As String)
            DirectionData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return True
        End Get
    End Property
    Public Sub Destroy()
        DirectionData.Clear(Id)
    End Sub
    Public Shared Operator =(first As Direction, second As Direction) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Direction, second As Direction) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
