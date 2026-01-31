# .NET 8 + WPF/MVVM Migration Plan

This repository currently contains a legacy **WinForms / .NET Framework 4.7.2** application (`AutoRace`) plus shared utilities (`Eco_Lib`).

Goal: migrate to a maintainable architecture targeting **.NET 8** with a **WPF + MVVM** UI, while preserving behavior and enabling gradual, low-risk refactors.

## Guiding principles

- **Small, mergeable steps**: keep changes incremental; avoid “big bang” rewrites.
- **Separate pure logic from Windows-specific code**: a `Core` layer should be platform-agnostic.
- **Safety via baselines**: capture current behavior (manual checklist + eventual automated tests) before changing internals.
- **Parallel run when possible**: introduce new projects alongside the legacy app, then migrate features.

## Target architecture

- `AutoRace.Core`
  - Domain models + services
  - No UI dependencies
  - Minimal dependencies, testable
- `AutoRace.Infrastructure.Windows`
  - WinAPI interop, input simulation, screen/pixel capture, window targeting
  - Wrap native calls behind interfaces used by `Core`
- `AutoRace.App.Wpf`
  - WPF UI
  - MVVM (ViewModels bind to `Core` services)
  - Composition root (DI) + configuration

## Phased plan

### Phase 0 — Planning + repo hygiene (this issue)

Deliverables:
- A written plan (this document)
- A rough module map (what moves where)
- A list of risks/unknowns

### Phase 1 — Establish build + CI foundations

Deliverables:
- A working Windows CI pipeline (build at minimum)
- Consistent formatting/style rules
- Optional: add test project scaffolding (even if initially empty)

Notes:
- Prefer building the solution on a GitHub Actions **windows-latest** runner.

### Phase 2 — Create new .NET 8 projects alongside legacy

Deliverables:
- New .NET 8 projects added without breaking the existing solution
- `AutoRace.Core` contains the first slices of pure domain logic
- `AutoRace.Infrastructure.Windows` contains the first wrappers for WinAPI/pixel/input

Approach:
- Start by moving **types** (DTOs/models/enums) that have no WinForms dependencies.
- Add interfaces in `Core`, implementations in `Infrastructure.Windows`.

### Phase 3 — WPF shell + MVVM scaffolding

Deliverables:
- WPF app boots
- Navigation shell exists (even if minimal)
- Basic settings/profile view that loads/saves a profile model

### Phase 4 — Incremental feature migration

Deliverables (repeat per feature slice):
- Move one feature at a time from WinForms into `Core` services
- Wire into WPF UI
- Keep legacy app working until parity is reached

Recommended order:
1. Profile/config persistence
2. “Run state” orchestration
3. Automation steps (pixel detection → decision → input)

### Phase 5 — Decommission legacy WinForms app

Deliverables:
- Feature parity checklist complete
- Remove/archival plan for WinForms project

## Module map (suggested)

- `AutoRace` (legacy)
  - Eventually replaced by `AutoRace.App.Wpf`
- `Eco_Lib`
  - Split into:
    - `AutoRace.Infrastructure.Windows` (WinAPI/input/pixel)
    - `AutoRace.App.Wpf` (UI helpers if any)
    - Optional: `AutoRace.Common` for cross-cutting utilities

## Risks / unknowns

- WinAPI behavior differences between .NET Framework and .NET 8 (P/Invoke signatures, DPI scaling, threading).
- Pixel sampling/screen capture performance and reliability.
- Input simulation permissions / anti-cheat / OS security constraints.
- Threading model differences: WinForms vs WPF dispatcher.

## Acceptance criteria for “migration complete”

- WPF app reproduces the critical user workflows of the legacy app.
- Core automation logic is testable (unit tests for decision logic; integration tests optional).
- CI builds on Windows and produces artifacts.
- Legacy WinForms app is removed or clearly marked archived.
