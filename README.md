# Brave-Lamb-Test-Wojciech-Farej

## Assety:
TextMeshPro 

## Pozostałę informacje
Czas wykonania: ok. 6 godzin z przerwami
Plugin wykorzystany zosatał do stworzenia UI

Decyzje: 
- wykorzystanie puli obiektów, aby nie instancjonować ponownie elementów (ale ta z Unity jest przeciętna :/ )
- desperacka (raczej udana) próba przerobienia GameManagera na wzorzec Singleton
- zastosowanie prostego shadera na tło 😊

Co nie działa:
- Wrogowie czasamia spawnią się jeden na drugim (wiem jak to naprawić, ale już brak czasu 🙃)

Mimo iż po 3-ech godzinach miałem core rozgrywki, to wykańczanie zeszło dłużej niż podejrzewałem, głównie przez unitowską pulę obiektów.
