Public Class Counter
    ReadOnly Property Id As Long
    Sub New(counterId As Long)
        Id = counterId
    End Sub
    ReadOnly Property CounterType As CounterType
        Get
            Return CType(CounterData.ReadCounterType(Id).Value, CounterType)
        End Get
    End Property
    ReadOnly Property Value As Long
        Get
            Return CounterData.ReadCounterValue(Id).Value
        End Get
    End Property
End Class
