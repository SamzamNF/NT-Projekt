# NT Projekt – Prototype for Tur- og Effektivitets-Logging  
*Udviklet for Nordjyllands Trafikselskab (NT) som del af et studieprojekt*

---

## Om projektet

Dette program er udviklet som en **prototypeløsning** til Nordjyllands Trafikselskab (NT) i forbindelse med en projektopgave. Formålet er at give NT et værktøj til at **logge og analysere busture** med fokus på **brændstof-/energiforbrug, forsinkelser og ruteeffektivitet**.

### Problemformulering

NT ønsker et system, der kan:
- Registrere og gemme data om busture (tid, rute, bus, fører, energiforbrug, varighed, kommentarer mv.)
- Analysere og præsentere data for at identificere mønstre i forsinkelser og energiforbrug
- Muliggøre sortering og filtrering af ture på forskellige parametre (fx bruger, rute, periode, effektivitet)

---

## Funktionalitet

- **CRUD på busture:** Opret, vis, slet og filtrér ture
- **Bruger-, bus- og ruteadministration:** Håndtering af brugere, busser og ruter
- **Effektivitetsanalyse:** Sortering og visning af ture efter energiforbrug og forsinkelse
- **Filbaseret datalagring:** Alle data gemmes i tekstfiler i mappen `/Data`
- **Konsolbaseret brugergrænseflade:** Simpelt tekstbaseret UI til interaktion

---

## Teknologi og krav

- **Udviklet i:** C# 13 (.NET 9)
- **Krav:**  
  - .NET 9 SDK  
  - Visual Studio 2022 eller nyere  
  - Windows (testet på Windows 10/11)
- **Ingen eksterne databaser** – alt data lagres i tekstfiler

---

## Sådan fungerer programmet

1. **Start programmet**  
   Programmet initialiserer repositories for brugere, busser, ruter og ture, og sikrer at nødvendige datafiler findes.

2. **Brugerinteraktion**  
   Brugeren navigerer via menuer til at oprette, vise, slette eller analysere ture, ruter, busser og brugere.

3. **Datahåndtering**  
   Alle ændringer gemmes straks i tekstfiler. Data kan hentes, filtreres og vises i konsollen.

4. **Analyse**  
   Brugeren kan sortere ture efter energiforbrug, forsinkelse, periode, bruger eller rute.

---

## Kendte begrænsninger og fremtidige forbedringer

- **UI/visning:**  
  Nogle print-funktioner i `TripViewModel` mangler pæn og brugervenlig formatering. Dette kan forbedres i en fremtidig version, fx med bedre tabeller, farver eller evt. grafisk interface.
- **Fejlhåndtering:**  
  Programmet håndterer de fleste fejl, men der kan stadig opstå utilsigtede situationer ved forkert input.
- **Dataformat:**  
  Ændringer i dataformatet kræver, at gamle datafiler opdateres manuelt.

---

## Installation og brug

1. **Klon eller download projektet**
2. **Åbn løsningen i Visual Studio 2022**
3. **Sørg for at .NET 9 SDK er installeret**
4. **Kør projektet (F5 eller Ctrl+F5)**
5. **Datafiler oprettes automatisk i `/Data`-mappen ved første kørsel**

---

## Udviklet af

Projektet er udviklet som en del af en studieopgave for NT, Nordjyllands Trafikselskab, med fokus på at demonstrere mulighederne for digital logging og analyse af busture.

---
