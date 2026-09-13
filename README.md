# Abweichungen von der Spezifikation

AK: Beim Klick auf eine Person wird ein modaler Dialog mit den folgenden Daten angezeigt.
-> Ich habe mich entschieden den Dialog über einen Doppelklick auf das Element anzuzeigen, da dies von der Bedienung intuitiver ist.

# Annahmen

- Mit der temporären Sicherung ist nur eine Sicherung gemeint, die nur für die Dauer des Programmlaufs besteht.

# Architekturhinweise

- Für den Austausch der API muss lediglich das Projekt TProg.StarWarsDan.Data.Swapi ausgetauscht werden gegen beispielsweise eine DB Implementierung.
- Für die Umsetzung des CLienten habe ich mein privates Framework verwendet. Bei Bedarf kann ich die Inhalte des Frameworks ebenfalls erläutern.

# Hinweise zum starten der Applikation ohne Visual Studio und installierten SDK

- .NET Desktop Runtime 10.0 herunterladen & installieren (https://dotnet.microsoft.com/en-us/download/dotnet/10.0)