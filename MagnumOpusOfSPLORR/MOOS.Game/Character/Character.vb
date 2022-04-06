Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Counters As List(Of Counter)
        Get
            Return CounterData.ReadForCharacter(Id).Select(Function(x) New Counter(x)).ToList
        End Get
    End Property
    Property CharacterType As CharacterType
        Get
            Return New CharacterType(CharacterData.ReadCharacterType(Id).Value)
        End Get
        Set(value As CharacterType)
            CharacterData.WriteCharacterType(Id, value.Id)
        End Set
    End Property
    ReadOnly Property HasAvailableEquipSlot As Boolean
        Get
            Return CharacterType.EquipSlots.Any(Function(x) Not EquippedItems.Keys.Contains(x))
        End Get
    End Property
    ReadOnly Property EquippedItems As Dictionary(Of EquipSlot, ItemType)
        Get
            Dim equipSlotIds = EquippedItemData.ReadEquippedSlotsForCharacter(Id)
            Dim result As New Dictionary(Of EquipSlot, ItemType)
            For Each equipSlotId In equipSlotIds
                result(New EquipSlot(equipSlotId)) = New ItemType(EquippedItemData.ReadItemTypeForCharacterEquipSlot(Id, equipSlotId).Value)
            Next
            Return result
        End Get
    End Property
    Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            CharacterData.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property IsPlayerCharacter As Boolean
        Get
            Return Id = PlayerData.Read.Value
        End Get
    End Property
    Public Function RollAttack() As Integer
        Dim weapons = EquippedItems.Values.Where(Function(x) x.CanAttack)
        If weapons.Any Then
            Dim tally = 0
            For Each weapon In weapons
                tally += RNG.RollDice(weapon.AttackDice)
            Next
            Return tally
        Else
            Return RNG.RollDice(CharacterType.AttackDice)
        End If
    End Function
    Public Function RollDefense() As Integer
        Return RNG.RollDice(CharacterType.DefendDice)
    End Function
    ReadOnly Property UniqueName As String
        Get
            Dim result = $"{Name}(#{Id})"
            If IsPlayerCharacter Then
                result &= "(PC)"
            End If
            Return result
        End Get
    End Property
    Public Sub Kill()
        For Each item In Inventory.Items
            Location.Inventory.Add(item)
        Next
        Destroy()
    End Sub
    Public Sub SetCounter(counterType As CounterType, counterValue As Long)
        Dim counter = Counters.SingleOrDefault(Function(x) x.CounterType = counterType)
        If counter Is Nothing Then
            CounterData.Create(Id, counterType, counterValue)
        Else
            CounterData.WriteCounterValue(counter.Id, counterValue)
        End If
    End Sub
    Property Name As String
        Get
            Return CharacterData.ReadName(Id)
        End Get
        Set(value As String)
            CharacterData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property DisplayName As String
        Get
            If IsPlayerCharacter Then
                Return "You"
            Else
                Return Name
            End If
        End Get
    End Property
    ReadOnly Property AttackVerb As String
        Get
            If IsPlayerCharacter Then
                Return "attack"
            Else
                Return "attacks"
            End If
        End Get
    End Property
    ReadOnly Property RollVerb As String
        Get
            If IsPlayerCharacter Then
                Return "roll"
            Else
                Return "rolls"
            End If
        End Get
    End Property
    ReadOnly Property HitVerb As String
        Get
            If IsPlayerCharacter Then
                Return "hit"
            Else
                Return "hits"
            End If
        End Get
    End Property
    ReadOnly Property KillVerb As String
        Get
            If IsPlayerCharacter Then
                Return "kill"
            Else
                Return "kills"
            End If
        End Get
    End Property
    ReadOnly Property MissVerb As String
        Get
            If IsPlayerCharacter Then
                Return "miss"
            Else
                Return "misses"
            End If
        End Get
    End Property
    Public Sub SetAsPlayerCharacter()
        PlayerData.Write(Id)
    End Sub
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not IsPlayerCharacter
        End Get
    End Property
    Sub Destroy()
        If CanDestroy Then
            CharacterData.Clear(Id)
        End If
    End Sub

    Public Sub Unequip(equipSlot As EquipSlot)
        EquippedItemData.Clear(Id, equipSlot.Id)
    End Sub

    Public Sub EquipItemType(equipSlot As EquipSlot, itemType As ItemType)
        EquippedItemData.Write(Id, equipSlot.Id, itemType.Id)
    End Sub
    ReadOnly Property AvailableEquipSlots As List(Of EquipSlot)
        Get
            Return CharacterType.EquipSlots.Where(Function(x) Not EquippedItems.Keys.Contains(x)).ToList
        End Get
    End Property
    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = CharacterInventoryData.Read(Id)
            If inventoryId.HasValue Then
                Return New Inventory(inventoryId.Value)
            End If
            Dim result = Inventories.Create()
            CharacterInventoryData.Write(Id, result.Id)
            Return result
        End Get
    End Property
    Function CanPass(route As Route) As Boolean
        Dim requirements = route.PassRequirements
        Dim itemStacks = Inventory.StackedItems
        For Each requirement In requirements
            If Not itemStacks.Any(Function(entry) entry.Key = requirement.Key) Then
                Return False
            End If
            Dim itemStack = itemStacks.Single(Function(entry) entry.Key = requirement.Key)
            If itemStack.Value.LongCount < requirement.Value Then
                Return False
            End If
        Next
        Return True
    End Function
    Public Sub Pass(route As Route)
        If CanPass(route) Then
            Dim passCosts = route.PassCosts
            For Each passCost In passCosts
                Dim counter = passCost.Value
                While counter > 0
                    Inventory.Items.First(Function(i) i.ItemType = passCost.Key).Destroy()
                    counter -= 1
                End While
            Next
            route.SelfDestructBarriers()
            AddMoveToCounter()
            Location = route.ToLocation
        End If
    End Sub
    Private Sub AddMoveToCounter()
        Dim moveCounter = Counters.SingleOrDefault(Function(x) x.CounterType = CounterType.Movement)
        If moveCounter IsNot Nothing Then
            moveCounter.Value += 1
        Else
            CounterData.Create(Id, CounterType.Movement, 1)
        End If
    End Sub
    Property Wounds As Long
        Get
            Return CharacterData.ReadWounds(Id).Value
        End Get
        Set(value As Long)
            CharacterData.WriteWounds(Id, value)
        End Set
    End Property

    Public ReadOnly Property IsDead As Boolean
        Get
            Return Wounds >= CharacterType.Health
        End Get
    End Property
    ReadOnly Property CanHeal As Boolean
        Get
            Return Wounds > 0 AndAlso Inventory.Items.Any(Function(x) x.CanHeal)
        End Get
    End Property
    Public Shared Operator =(first As Character, second As Character) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Character, second As Character) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
