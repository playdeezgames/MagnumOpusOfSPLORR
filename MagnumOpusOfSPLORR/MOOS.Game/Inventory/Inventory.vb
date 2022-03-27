﻿Imports MOOS.Data

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
    ReadOnly Property StackedItems As Dictionary(Of ItemType, List(Of Item))
        Get
            Dim itemStacks = Items.GroupBy(Function(i) i.ItemType.Id)
            Dim result As New Dictionary(Of ItemType, List(Of Item))
            For Each itemStack In itemStacks
                result.Add(New ItemType(itemStack.Key), itemStack.ToList)
            Next
            Return result
        End Get
    End Property
End Class
