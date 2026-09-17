# Star Wars Dan

[![.NET Desktop](https://github.com/Taonnor/star-wars-dan/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Taonnor/star-wars-dan/actions/workflows/dotnet-desktop.yml)

Eine WPF-Anwendung, die Personendaten aus dem Star-Wars-Universum über die öffentliche SWAPI
abruft, in einer Liste anzeigt und zum Bearbeiten in einem Dialog öffnet. Sie ist als
Workshop-Beispiel entstanden und dient der Demonstration, nicht dem Produktivbetrieb.

Der Datenzugriff ist hinter einer Abstraktion gekapselt: Die konkrete SWAPI-Anbindung lässt sich
gegen eine andere Quelle austauschen, ohne die übrigen Projekte anzufassen. Die Anwendung baut auf
`tprog-framework` auf, das die MVVM-Grundlage, die Fenster-Shell und die Dialoge beisteuert.

## Projekte

| Projekt | Inhalt |
| --- | --- |
| `TProg.StarWarsDan.App` | Startpunkt und Composition Root |
| `TProg.StarWarsDan.Ui` | Ansichten und ViewModels |
| `TProg.StarWarsDan.Domain` | Domänenmodelle |
| `TProg.StarWarsDan.Data.Api` | Abstraktion des Datenzugriffs — die Naht, an der die Quelle getauscht wird |
| `TProg.StarWarsDan.Data.Swapi` | Konkrete Anbindung an die SWAPI |
| `TProg.StarWarsDan.Test` | Tests zu ViewModels und Datenzugriff |

## Bauen und Testen

```
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build
```

Eine Windows-Maschine ist erforderlich, weil die Anwendung auf WPF aufsetzt. Dieselben Schritte
laufen in der CI, je einmal in `Debug` und in `Release`.

Ein zusätzlicher Schritt für die Framework-Pakete ist nicht nötig: Sie liegen im Ordner `nuget/`
dieses Repos, und die `NuGet.config` im Wurzelverzeichnis zeigt relativ dorthin. Das Repo lässt sich
damit auf jeder Maschine wiederherstellen, ohne dass dort etwas eingerichtet sein muss.

## Lizenz

MIT — siehe [`LICENSE`](LICENSE).

---

# Hinweise zur Abgabe

*Der folgende Teil ist der ursprüngliche Inhalt dieser README aus dem Workshop-Kontext. Er
dokumentiert Entscheidungen, die sich nirgendwo sonst nachlesen lassen, und bleibt deshalb
unverändert erhalten.*

## Abweichungen von der Spezifikation

AK: Beim Klick auf eine Person wird ein modaler Dialog mit den folgenden Daten angezeigt.
-> Ich habe mich entschieden den Dialog über einen Doppelklick auf das Element anzuzeigen, da dies von der Bedienung intuitiver ist.

## Annahmen

- Mit der temporären Sicherung ist nur eine Sicherung gemeint, die nur für die Dauer des Programmlaufs besteht.

## Architekturhinweise

- Für den Austausch der API muss lediglich das Projekt TProg.StarWarsDan.Data.Swapi ausgetauscht werden gegen beispielsweise eine DB Implementierung.
- Für die Umsetzung des CLienten habe ich mein privates Framework verwendet. Bei Bedarf kann ich die Inhalte des Frameworks ebenfalls erläutern.

## Hinweise zum starten der Applikation ohne Visual Studio und installierten SDK

- .NET Desktop Runtime 10.0 herunterladen & installieren (https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
