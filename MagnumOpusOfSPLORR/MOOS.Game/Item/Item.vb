Imports MOOS.Data

Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    ReadOnly Property ItemType As ItemType
        Get
            Return New ItemType(ItemData.ReadItemType(Id).Value)
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Return ItemType.Name
        End Get
    End Property
    Public Sub Destroy()
        ItemData.Clear(Id)
    End Sub
    Public Shared Operator =(first As Item, second As Item) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Item, second As Item) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
