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
    Property Value As Long
        Get
            Return CounterData.ReadCounterValue(Id).Value
        End Get
        Set(value As Long)
            CounterData.WriteCounterValue(Id, value)
        End Set
    End Property
    Public Shared Operator =(first As Counter, second As Counter) As Boolean
        Return first.Id = second.Id
    End Operator
    Public Shared Operator <>(first As Counter, second As Counter) As Boolean
        Return first.Id <> second.Id
    End Operator
End Class
