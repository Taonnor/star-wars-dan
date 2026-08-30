# CLAUDE.md

Diese Datei gibt Claude Code Hinweise für die Arbeit in diesem Repository.

## Projektüberblick

`star-wars-dan` ist eine Beispiel-/Workshop-App (Star-Wars-Personendaten über die SWAPI), gebaut
auf `tprog-framework`. Sie dient primär zur Demonstration/zum Workshop, nicht als Produktivsystem.

Aus dem README: Personen-Klick öffnet einen modalen Dialog per Doppelklick (bewusste Abweichung von
der ursprünglichen Spezifikation, aus Usability-Gründen). Der Datenzugriff ist austauschbar
gekapselt in `TProg.StarWarsDan.Data.Swapi` (z.B. gegen eine DB-Implementierung).

## Lösungsstruktur

```
TProg.StarWarsDan.App          Composition Root / Startpunkt, nutzt tprog-framework.
TProg.StarWarsDan.Domain       Domänenmodelle.
TProg.StarWarsDan.Data.Api     Abstraktion des Datenzugriffs.
TProg.StarWarsDan.Data.Swapi   Konkrete SWAPI-Implementierung des Datenzugriffs.
TProg.StarWarsDan.Ui           UI/ViewModels, nutzt tprog-framework (MVVM, Dialoge).
TProg.StarWarsDan.Test         Tests.
```

## Framework-Nutzung

Dieses Repo referenziert `tprog-framework` (`TProg.Framework.*`). Der Workspace-Ordner (eine Ebene
höher, `../`) enthält `tprog-framework/` als Geschwister-Repo - siehe die übergreifende
`../CLAUDE.md` und `../tprog-framework/CLAUDE.md` für die Framework-Konventionen.

**Bevor du ein neues Feature (ViewModel, Dialog, Service, Control) implementierst**: prüfe zuerst,
ob eine passende Basis dafür bereits in `../tprog-framework` existiert (siehe `/check-framework`).
Falls nicht und das Feature allgemein genug ist, ergänze es dort statt es hier zu duplizieren -
beachte dabei aber, dass dies eine Workshop-/Beispiel-App ist: nicht jedes hier benötigte Feature
muss zwingend generisch genug fürs Framework sein.

Falls dieses Repo **nicht** im gemeinsamen Workspace-Ordner geöffnet ist, sondern einzeln, sorgt
`.claude/settings.json` (`additionalDirectories: ["../tprog-framework"]`) dafür, dass das Framework
trotzdem lesbar/schreibbar bleibt - vorausgesetzt, `tprog-framework` liegt als Geschwister-Ordner
neben diesem Repo auf der Festplatte.

## Code-Stil

Übernimmt die Konventionen aus `tprog-framework` (eigene `.editorconfig` vorhanden).

## Ausführen ohne Visual Studio

.NET Desktop Runtime 9.0.2 installieren, siehe README.md für Details.
