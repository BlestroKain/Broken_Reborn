# Elemental Affinity Settings

Elemental damage multipliers can be adjusted without recompiling the server. Edit the values under the `Combat` section of `resources/config.json`:

```json
{
  "Combat": {
    "ElementalAdvantage": 1.5,
    "ElementalNeutral": 1.0,
    "ElementalDisadvantage": 0.5
  }
}
```

* **ElementalAdvantage** – applied when the attacker's element is strong against the defender.
* **ElementalNeutral** – used when neither element has an advantage.
* **ElementalDisadvantage** – applied when the attacker is weak against the defender.

Adjust these numbers to quickly balance elemental combat.
