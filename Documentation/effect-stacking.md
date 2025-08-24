# Effect Stacking Rules

`EffectData` now defines how item effects behave when multiple instances of the same effect are applied.

## Properties

- `IsPassive`: only effects marked as passive are applied during stat construction.
- `Stacking`:
  - `Stack` – duplicate effects add their percentages together.
  - `Renew` – a duplicate replaces the existing effect, renewing its duration.
  - `Ignore` – duplicates are discarded.

Active effects (`IsPassive` set to `false`) are ignored when building equipment stats and must be handled by runtime systems.
