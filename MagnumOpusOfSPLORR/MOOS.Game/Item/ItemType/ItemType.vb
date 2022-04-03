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
    ReadOnly Property CanHeal As Boolean
        Get
            Return Not String.IsNullOrEmpty(HealDice)
        End Get
    End Property
    Property HealDice As String
        Get
            Return ItemTypeData.ReadHealDice(Id)
        End Get
        Set(value As String)
            ItemTypeData.WriteHealDice(Id, value)
        End Set
    End Property
    Property Name As String
        Get
            Return ItemTypeData.ReadName(Id)
        End Get
        Set(value As String)
            ItemTypeData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property HasEquipSlot As Boolean
        Get
            Return EquipSlot IsNot Nothing
        End Get
    End Property
    Property EquipSlot As EquipSlot
        Get
            Dim equipSlotId = ItemTypeEquipSlotData.Read(Id)
            Return If(equipSlotId.HasValue, New EquipSlot(equipSlotId.Value), Nothing)
        End Get
        Set(value As EquipSlot)
            If value Is Nothing Then
                ItemTypeEquipSlotData.Clear(Id)
            Else
                ItemTypeEquipSlotData.Write(Id, value.Id)
            End If
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
    Public Shared Operator =(first As ItemType, second As ItemType) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As ItemType, second As ItemType) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
