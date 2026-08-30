Prüfe, ob folgendes Feature/folgende Komponente bereits im Framework (`../tprog-framework`)
existiert: $ARGUMENTS

- Falls ja: zeige, wie es aus dieser App heraus korrekt konsumiert wird (Namespace, Export/Import,
  Basis-Klasse/Interface), ohne es zu duplizieren.
- Falls nein, aber allgemein genug für Wiederverwendung: implementiere es im Framework unter
  `../tprog-framework`, unter Einhaltung von dessen Konventionen (siehe
  `../tprog-framework/CLAUDE.md`: Plattformtrennung Core/Mvvm vs. Wpf/Style, MEF-Export/Import,
  `.editorconfig`-Regeln, XML-Doku, Tests in `TProg.Framework.Test`). Binde es anschließend hier
  in der App ein.
- Falls nein und eindeutig app-spezifisch: implementiere es direkt in diesem Repo.

Nenne am Ende kurz, wofür du dich entschieden hast und warum.
