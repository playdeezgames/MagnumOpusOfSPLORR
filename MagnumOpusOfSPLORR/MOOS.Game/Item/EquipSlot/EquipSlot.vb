Public Class EquipSlot
    ReadOnly Property Id As Long
    Sub New(equipSlotId As Long)
        Id = equipSlotId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name()}(#{Id})"
        End Get
    End Property
    Property Name As String
        Get
            Return EquipSlotData.ReadName(Id)
        End Get
        Set(value As String)
            EquipSlotData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return ItemTypeEquipSlotData.ReadCountForEquipSlot(Id) = 0 AndAlso CharacterTypeEquipSlotData.ReadCountForEquipSlot(Id) = 0
        End Get
    End Property
    Sub Destroy()
        EquipSlotData.Clear(Id)
    End Sub
    Public Shared Operator =(first As EquipSlot, second As EquipSlot) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As EquipSlot, second As EquipSlot) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
