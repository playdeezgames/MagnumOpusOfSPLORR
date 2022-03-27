Public Class Barrier
    ReadOnly Property Id As Long
    Sub New(barrierId As Long)
        Id = barrierId
    End Sub
    ReadOnly Property UniqueName As String
        Get
            Return $"{If(DestroysItem, "destroys", "requires")} {ItemType.UniqueName}{If(SelfDestructs, " and self destructs", "")}(#{Id})"
        End Get
    End Property
    Property ItemType As ItemType
        Get
            Return New ItemType(BarrierData.ReadItemType(Id).Value)
        End Get
        Set(value As ItemType)
            BarrierData.WriteItemType(Id, value.Id)
        End Set
    End Property
    Property DestroysItem As Boolean
        Get
            Return BarrierData.ReadDestroysItem(Id).Value
        End Get
        Set(value As Boolean)
            If value Then
                BarrierData.WriteDestroysItem(Id)
            Else
                BarrierData.ClearDestroysItem(Id)
            End If
        End Set
    End Property
    Property SelfDestructs As Boolean
        Get
            Return BarrierData.ReadSelfDestructs(Id).Value
        End Get
        Set(value As Boolean)
            If value Then
                BarrierData.WriteSelfDestructs(Id)
            Else
                BarrierData.ClearSelfDestructs(Id)
            End If
        End Set
    End Property
End Class
