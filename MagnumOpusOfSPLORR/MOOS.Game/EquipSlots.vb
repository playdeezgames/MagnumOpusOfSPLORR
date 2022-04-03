Public Module EquipSlots
    ReadOnly Property AllEquipSlots As List(Of EquipSlot)
        Get
            Return EquipSlotData.All.Select(Function(x) New EquipSlot(x)).ToList
        End Get
    End Property
    Function FindEquipSlotByUniqueName(uniqueName As String) As EquipSlot
        Return AllEquipSlots.Single(Function(x) x.UniqueName = uniqueName)
    End Function
    Function CreateEquipSlot(equipSlotName As String) As EquipSlot
        Return New EquipSlot(EquipSlotData.Create(equipSlotName))
    End Function
End Module
