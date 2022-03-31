Imports System.Runtime.CompilerServices

Public Enum CounterType
    Movement
End Enum
Public Module CounterTypeExtensions
    <Extension()>
    Function Name(counterType As CounterType) As String
        Select Case counterType
            Case CounterType.Movement
                Return "Movement"
            Case Else
                Throw New ArgumentOutOfRangeException
        End Select
    End Function
End Module
