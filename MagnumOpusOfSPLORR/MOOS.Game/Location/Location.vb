﻿Imports MOOS.Data

Public Class Location
    ReadOnly Property Id As Long
    Property Name As String
        Get
            Return LocationData.ReadName(Id)
        End Get
        Set(value As String)
            LocationData.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property Characters As List(Of Character)
        Get
            Return CharacterData.ReadForLocation(Id).Select(Function(x) New Character(x)).ToList
        End Get
    End Property
    ReadOnly Property CanDestroy As Boolean
        Get
            Return Not Characters.Any
        End Get
    End Property
    Sub Destroy()
        If CanDestroy Then
            LocationData.Clear(Id)
        End If
    End Sub
End Class