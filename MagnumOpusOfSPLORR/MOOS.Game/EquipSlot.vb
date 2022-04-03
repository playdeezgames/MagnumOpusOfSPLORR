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
            Return True
        End Get
    End Property
    Sub Destroy()
        EquipSlotData.Clear(Id)
    End Sub
End Class
