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
    ReadOnly Property UniqueName As String
        Get
            Dim result = $"{Name}(#{Id})"
            If IsPlayerCharacter Then
                result &= "(PC)"
            End If
            Return result
        End Get
    End Property
    Property Name As String
        Get
            Return CharacterData.ReadName(Id)
        End Get
        Set(value As String)
            CharacterData.WriteName(Id, value)
        End Set
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
    Public Shared Operator =(first As Character, second As Character) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Character, second As Character) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
