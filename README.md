# Plan Projektu - Mines Game

## Intefejs graficzny:
- stworzenie głównego menu aplikacji,
- dodanie zakładek przy użyciu TabbedPage,
- przygotowanie okna gry,
- przygotowanie okna historii,
- przygotowanie ustawień użytkownika,
- dodanie customowych fontów,
- dodanie obrazów w formacie PNG
---

# Mines Game

## Opis projektu

Głównym założeniem aplikacji jest stworzenie mobilnej gry typu **Mines**, inspirowanej grami dostępnymi w kasynach online, np. Stake.

Gra opiera się na planszy **5x5**, na której losowo rozmieszczane są miny.  
Gracz wybiera pola, próbując unikać min. Każde poprawnie odkryte pole zwiększa mnożnik potencjalnej wygranej.

W dowolnym momencie gracz może użyć opcji **Cash Out**, aby odebrać aktualną wygraną.  
Jeżeli gracz odkryje pole z miną, przegrywa całą aktualną stawkę.

---

# Główne funkcje aplikacji

- system gry Mines,
- możliwość ustawienia ilości min,
- możliwość ustawienia wysokości stawki,
- system mnożników wygranej,
- system Cash Out,
- balans gracza,
- historia rozegranych gier,
- zapisywanie danych przy użyciu SQLite,
- zapamiętywanie ustawień użytkownika,
- nowoczesny interfejs inspirowany aplikacjami kasyn online.

---

# Zastosowane technologie

## .NET MAUI
Framework wykorzystywany do stworzenia aplikacji mobilnej.

## SQLite
Lokalna baza danych wykorzystywana do:
- przechowywania historii gier,
- zapisywania danych użytkownika.

## CollectionView
Wykorzystywany do wyświetlania:
- planszy gry,
- historii rozegranych gier.

## SharedPreferences
Wykorzystywane do zapisywania:
- nazwy użytkownika,
- balansu gracza.

---

# Wygląd aplikacji

Aplikacja została zaprojektowana w ciemnym motywie kolorystycznym z zielonymi elementami interfejsu, inspirowanymi nowoczesnymi aplikacjami gamingowymi.

W projekcie zastosowano:
- customowe fonty,
- obrazy PNG,
- kontrolki Border,
- stylizowane przyciski i pola.

---

# Algorytm gry

Algorytm gry oblicza mnożnik na podstawie:
- ilości min,
- liczby odkrytych pól,
- aktualnego ryzyka przegranej.

Im więcej min znajduje się na planszy, tym większy jest potencjalny mnożnik wygranej.

W algorytmie został zastosowany również niewielki **house edge** (~1–4%), dzięki czemu działanie gry przypomina system używany w rzeczywistych grach typu Mines.

---

# Struktura projektu

Projekt został podzielony na foldery:

```text
Models
Services
Views
Resources
```

## Models
Klasy danych aplikacji.

## Services
Obsługa bazy danych oraz logiki aplikacji.

## Views
Widoki aplikacji.

## Resources
Obrazy, fonty oraz inne zasoby projektu.

---

# Inspiracja

Projekt inspirowany jest grą „Mines” dostępną na platformach kasyn online, takich jak Stake.

Algorytm działania został opracowany na podstawie materiału opublikowanego przez:

**Hemant Singh Parmar**

---

# Źródła

- https://medium.com/@hemantsp/know-backend-of-stakes-mine-game-073536e201c9
