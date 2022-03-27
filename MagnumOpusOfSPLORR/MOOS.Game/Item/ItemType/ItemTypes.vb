Imports MOOS.Data

Public Module ItemTypes
    ReadOnly Property AllItemTypes As List(Of ItemType)
        Get
            Return ItemTypeData.All.Select(Function(id) New ItemType(id)).ToList
        End Get
    End Property
    Function CreateItemType(itemTypeName As String) As ItemType
        Return New ItemType(ItemTypeData.Create(itemTypeName))
    End Function
End Module
