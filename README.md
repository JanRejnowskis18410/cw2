# cw2

## Funkcje
- Tworzenie pliku XML w wymaganym formacie dla danych z pliku dane.csv,
- Aplikacja częsciowo zabezpieczona, sprawdza puste kolumny, powtarzające się rekordy w pliku csv. Dodatkowo sprawdza, czy plik z danymi podany przez klienta istnieje
- Pobieranie argumentów od klienta, jeśli nie będą podane w kolejności jak w pliku APBD_cw.pdf lub ich liczba będzie inna niż 3, uruchomi aplikację z wartościami domyślnymi
- Tworzenie pliku log.txt

## Co trzeba dokończyć
- Nie ma sprawdzania poprawności ścieżki do plików, co opisane zostało w punkcie 1.2 w pliku APBD_cw.pdf
- Ogólny refactor kodu
- Stworzyć serializację dla jsonów

## Z jakich narzędzi korzystałem
- VisualStudioCode 2019
- Przy wywoływaniu programu z argumentami używałem opcji **Debug->Properties->Application arguments**
