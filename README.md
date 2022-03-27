# Magnum Opus Of SPLORR!!

## Action Items

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
- Win condition: Player must be at a WinningLocation

## TODO Decisions

- How do we "lose"?
    - ????