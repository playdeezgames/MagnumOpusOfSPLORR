Public Module Inventories
    Function Create() As Inventory
        Return New Inventory(InventoryData.Create())
    End Function
End Module
