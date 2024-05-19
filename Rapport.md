## Rapport för Inlämningsuppgift #1

### **Beskrivning av mina endpoints**

PostHighscore:
Metoden tar emot en HTTP POST-begäran med en highscore som data.
Om spelarens namn inte är null, läggs highscoren till i databasen och en Created 201-status returneras med platsen för den skapade resursen.
Vid fel returneras en 500-status med ett felmeddelande.

DeleteAllHighscores:
Metoden tar emot en HTTP DELETE-begäran för att radera alla highscores från databasen.
Alla highscores tas bort från databasen och en OK 200-status returneras om operationen lyckas.
Vid fel returneras en 500-status med ett felmeddelande.

GetAllHighscores:
Metoden tar emot en HTTP GET-begäran för att hämta alla highscores från databasen.
Highscores returneras i fallande ordning efter poäng och en OK 200-status returneras.
Vid fel returneras en 500-status med ett felmeddelande.

GetTopTenHighscores:
Metoden tar emot en HTTP GET-begäran för att hämta de tio bästa highscores från databasen.
De tio bästa highscores returneras och en OK 200-status returneras.
Vid fel returneras en 500-status med ett felmeddelande.

GetHighscoreById:
Metoden tar emot en HTTP GET-begäran med en highscore-ID för att hämta en specifik highscore från databasen.
Om highscoren hittas returneras den med en OK 200-status, annars returneras en NotFound 404-status.
Vid fel returneras en 500-status med ett felmeddelande.

GetHighscoresByPlayerName:
Metoden tar emot en HTTP GET-begäran med en spelares namn för att hämta alla highscores för den spelaren från databasen.
Om highscores hittas returneras de med en OK 200-status, annars returneras en NotFound 404-status.
Vid fel returneras en 500-status med ett felmeddelande.

GetHighscoreStatistics:
Metoden tar emot en HTTP GET-begäran för att hämta statistik över highscores från databasen.
Statistiken inkluderar totalt antal highscores, populäraste skapelsedatum och populäraste spel.
En JSON med statistiken returneras och en OK 200-status returneras.
Vid fel returneras en 500-status med ett felmeddelande.

### **Egen bedömning**

Kortfattat! Några meningar per fråga räcker:

- Hur bedömer du själv att du klarat uppgiften?
  Jag tycker att jag har klarat uppgiften bra och har uppfyllt alla krav för att få väl godkännt.

- Hur motiverar du valet av antingen ett minimal api eller ett api med controllers?
  Jag har använt WebAPI med controllers eftersom denna WebAPI har en bättre struktur för att kunna utveckla programmet, och det blir inte för komplicerat om man vill lägga till nya endpoints.

- Hur bedömer du strukturen på din kod?
  Jag anser att strukturen i min kod är mycket bra eftersom den är lättläst och välorganiserad.
