Imports MOOS.Data

Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    ReadOnly Property Items As List(Of Item)
        Get
            Return ItemData.ReadForInventory(Id).Select(Function(id) New Item(id)).ToList
        End Get
    End Property
End Class
