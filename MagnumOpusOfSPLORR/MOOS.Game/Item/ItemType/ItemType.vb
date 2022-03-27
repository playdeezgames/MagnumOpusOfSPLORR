Imports MOOS.Data

Public Class ItemType
    ReadOnly Property Id As Long
    Sub New(itemTypeId As Long)
        Id = itemTypeId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Property Name As String
        Get
            Return ItemTypeData.ReadName(Id)
        End Get
        Set(value As String)
            ItemTypeData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return ItemData.ReadCountForItemType(Id) = 0
        End Get
    End Property
    Sub Destroy()
        ItemTypeData.Clear(Id)
    End Sub
End Class
