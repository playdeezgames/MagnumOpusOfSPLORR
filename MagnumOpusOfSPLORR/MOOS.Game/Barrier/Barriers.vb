Public Module Barriers
    ReadOnly Property AllBarriers As List(Of Barrier)
        Get
            Return BarrierData.All.Select(Function(id) New Barrier(id)).ToList
        End Get
    End Property
    Function CreateBarrier(itemType As ItemType, destroysItem As Boolean, selfDestructs As Boolean) As Barrier
        Return New Barrier(BarrierData.Create(itemType.Id, destroysItem, selfDestructs))
    End Function
End Module
