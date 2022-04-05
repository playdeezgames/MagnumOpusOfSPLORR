Public Class CharacterType
    ReadOnly Property Id As Long
    Sub New(characterTypeId As Long)
        Id = characterTypeId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    ReadOnly Property EquipSlots As List(Of EquipSlot)
        Get
            Return CharacterTypeEquipSlotData.ReadForCharacterType(Id).Select(Function(x) New EquipSlot(x)).ToList
        End Get
    End Property
    ReadOnly Property AvailableEquipSlots As List(Of EquipSlot)
        Get
            Dim current As New HashSet(Of Long)(CharacterTypeEquipSlotData.ReadForCharacterType(Id))
            Return EquipSlotData.All.Where(Function(x) Not current.Contains(x)).Select(Function(x) New EquipSlot(x)).ToList
        End Get
    End Property
    ReadOnly Property HasEquipSlots As Boolean
        Get
            Return EquipSlots.Any
        End Get
    End Property
    Property Name As String
        Get
            Return CharacterTypeData.ReadName(Id)
        End Get
        Set(value As String)
            CharacterTypeData.WriteName(Id, value)
        End Set
    End Property
    Property Health As Long
        Get
            Return CharacterTypeData.ReadHealth(Id).Value
        End Get
        Set(value As Long)
            CharacterTypeData.WriteHealth(Id, value)
        End Set
    End Property
    Property AttackDice As String
        Get
            Return CharacterTypeData.ReadAttackDice(Id)
        End Get
        Set(value As String)
            CharacterTypeData.WriteAttackDice(Id, value)
        End Set
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not CharacterData.ReadForCharacterType(Id).Any
        End Get
    End Property

    Public Property DefendDice As String
        Get
            Return CharacterTypeData.ReadDefendDice(Id)
        End Get
        Set(value As String)
            CharacterTypeData.WriteDefendDice(Id, value)
        End Set
    End Property

    Public Sub Destroy()
        If CanDestroy Then
            CharacterTypeData.Clear(Id)
        End If
    End Sub
    Public Shared Operator =(first As CharacterType, second As CharacterType) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As CharacterType, second As CharacterType) As Boolean
        Return first.Id <> second.Id
    End Operator
    Public Sub AddEquipSlot(equipSlot As EquipSlot)
        CharacterTypeEquipSlotData.Write(Id, equipSlot.Id)
    End Sub
End Class
