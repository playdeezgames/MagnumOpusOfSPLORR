Imports MOOS.Data

Public Module Items
    Function CreateItem(itemType As ItemType, inventory As Inventory) As Item
        Return New Item(ItemData.Create(itemType.Id, inventory.Id))
    End Function
End Module
