# Magnum Opus Of SPLORR!!

## Action Items

- Location!
- Character!
- Direction!
- Route!
- WinningLocation
    - set
    - clear
    - game is over when player character is at a/the winning location
- Item
- EquipSlot
- CharacterType
- Generator
    - list generators
    - add entry
    - edit entry
    - remove entry
    - delete generator
    - create generator

## Deliberate Decisions

- UI is Spectre.Console
- Data Store is SQLite
- Language is VB, the one true King of Clown Languages
- All Presentation layer is strictly in MagnumOpusOfSPLORR.vbproj
- All "Business" layer is strictly in MOOS.Game.vbproj
- All Persistence layer is strictly in MOOS.Data.vbproj
- Characters MUST exist at a Location
- More than one Character MAY exist at a Location
- One way Routes connection Locations
- The Routes have unique combinations of from location and direction

## TODO Decisions

- How do we "win"?
    - Be at a/the "winning location"?

## DB Schema

### Directions

- DirectionId (PK AUTO)
- DirectionName (TEXT)

### Locations

- LocationId (PK AUTO)
- LocationName (TEXT)

### Characters

- CharacterId (PK AUTO)
- CharacterName (TEXT)
- LocationId (FK Locations)

### Players

- PlayerId (PK, CK = 1)
- CharacterId (FK Characters)

### Routes

- RouteId (PK AUTO)
- FromLocationId (FK Locations)
- DirectionId (FK Directions)
- ToLocationId (FK Locations)