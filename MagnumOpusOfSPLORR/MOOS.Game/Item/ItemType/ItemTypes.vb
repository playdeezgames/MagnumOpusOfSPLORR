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
    Function FindItemTypeByName(itemTypeName As String) As List(Of ItemType)
        Return ItemTypeData.ReadForName(itemTypeName).Select(Function(id) New ItemType(id)).ToList
    End Function
    Function FindItemTypeByUniqueName(itemTypeUniqueName As String) As ItemType
        Return AllItemTypes.SingleOrDefault(Function(x) x.UniqueName = itemTypeUniqueName)
    End Function
End Module
