Imports MOOS.Data

Public Module Locations
    ReadOnly Property AllLocations As List(Of Location)
        Get
            Return LocationData.All().Select(Function(id) New Location(id)).ToList
        End Get
    End Property
    Function CreateLocation(locationName As String) As Location
        Return New Location(LocationData.Create(locationName))
    End Function

    Function FindLocationByName(locationName As String) As List(Of Location)
        Return LocationData.ReadForName(locationName).Select(Function(id) New Location(id)).ToList
    End Function
    Function FindLocationByUniqueName(locationUniqueName As String) As Location
        Return AllLocations.SingleOrDefault(Function(x) x.UniqueName = locationUniqueName)
    End Function
End Module
