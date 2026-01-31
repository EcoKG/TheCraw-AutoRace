# TheCraw AutoRace

Legacy project (originally WinForms / .NET Framework 4.7.2) for an auto-race helper.

## Roadmap (planned)
- Migrate to **.NET (Core) / .NET 8**
- Rebuild UI in **WPF + MVVM**
- Split into `Core` (pure logic), `Infrastructure` (WinAPI/input/pixel), and `App` (WPF)

See: [Migration plan](docs/NET8_WPF_MVVM_MIGRATION_PLAN.md)

## Current solution
- `TheCraw AutoRace.sln`
- `AutoRace` (WinForms, .NET Framework 4.7.2)
- `Eco_Lib` (shared utilities: WinAPI, input, pixel helpers)

## Notes
- `.vs/` is a local Visual Studio folder and should not be committed.
