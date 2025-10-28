# ЗВІТ З ЛАБОРАТОРНОЇ РОБОТИ №3

## Тема: Маніпулювання даними SQL (OLTP)

### Працювали над лабораторною роботою:
* **Легеза Данііл Павлович IM-41**
* **Бойко Данило Сергійович IM-41**

### SQL-скрипт(и)

#### INSERT
```sql
INSERT INTO Game (Price, Name, Description, Release_date)
VALUES 
    (19.99, 'Puzzle Game', 'A puzzle game about time manipulation.', '2025-10-28');
```
<img width="932" height="138" alt="game" src="https://github.com/user-attachments/assets/339db02a-19de-4d5a-8204-900aed7165c8" />

```sql
INSERT INTO Category (Name, Description, Age_min)
VALUES 
    ('Puzzle', 'Games that emphasize puzzle-solving.', 3);
```
<img width="840" height="163" alt="category" src="https://github.com/user-attachments/assets/b07cb95f-a5c5-461f-8e3e-d858607176b6" />

```sql
INSERT INTO GameCategory (game_id, category_id)
VALUES 
    (4, 5);
```
<img width="219" height="182" alt="gamecategory" src="https://github.com/user-attachments/assets/7922f099-5719-45dd-b072-cb7da67a449c" />

```sql
INSERT INTO Achievement (game_id, Name, Goal)
VALUES 
    (4, 'Time Paradox', 'Complete level 10');
```
<img width="670" height="136" alt="achievement" src="https://github.com/user-attachments/assets/3f7b1185-8d41-450c-b9da-a5c6e02e4819" />

```sql
INSERT INTO AppUser (username, password)
VALUES
    ('ViktorLeheza', '$2a$12$tfsmdmlBWXk22YnIkqOfT.0HbbnrMSxBm92StIlmc4PZNKNcv4cCa'),
    ('AlinaParadoxy', '$2a$12$Fo0Vcj0waLK0riGn07xW9uv/5V5tZFwpFrBamWKtoQTw8GbF0EmAC');
```
<img width="721" height="167" alt="appuser" src="https://github.com/user-attachments/assets/89372f23-e3f9-4ea4-8994-191db628544d" />

```sql
INSERT INTO UserInfo (appuser_id, PhoneNumber, Email, Birthday)
VALUES
    (4, '+380979734812', 'viktorleheza@example.com', '1964-03-25'),
    (5, '+380675197698', 'paradoxy11@example.com', '2003-11-19');
```
<img width="548" height="165" alt="userinfo" src="https://github.com/user-attachments/assets/7f945d89-6205-407a-a71f-12e68563640e" />

```sql
INSERT INTO UserLibrary (appuser_id)
VALUES 
    (4), (5);
```
<img width="245" height="173" alt="userlibrary" src="https://github.com/user-attachments/assets/8b091fcc-e706-4f20-a0de-036b76cc1b5a" />

```sql
INSERT INTO GameCollection (userlibrary_id, Name)
VALUES
    (4, 'Recently Added'),
    (5, 'Strategy');
```
<img width="431" height="192" alt="gamecollection" src="https://github.com/user-attachments/assets/71033d99-ef1c-4a41-a03d-7d87d300c525" />

```sql
INSERT INTO LibraryCollection (gamecollection_id, userlibrary_id, game_id)
VALUES
    (5, 4, 4);
```
<img width="368" height="165" alt="librarycollection" src="https://github.com/user-attachments/assets/11496d3d-877e-4585-a6fa-9980b5252149" />

```sql
INSERT INTO Progress (userlibrary_id, game_id, Hours_played)
VALUES
    (5, 4, 25);
```
<img width="342" height="167" alt="progress" src="https://github.com/user-attachments/assets/34c349ab-702a-42af-a9e5-30293d3f7196" />

#### SELECT
```sql
SELECT * FROM Category;
```

```sql
SELECT name, price
FROM Game
WHERE price > 50;
```

```sql
SELECT email, birthday
FROM UserInfo
WHERE birthday > '1990-01-01';
```

```sql
SELECT game.name AS GameName, category.name AS CategoryName
FROM Game
JOIN GameCategory ON game.game_id = gamecategory.game_id
JOIN Category ON gamecategory.category_id = category.category_id;
```

```sql
SELECT u.appuser_id, a.username, p.game_id, p.Hours_played
FROM Progress p
JOIN UserLibrary u ON p.userlibrary_id = u.userlibrary_id
JOIN AppUser a ON u.appuser_id = a.appuser_id
WHERE p.Hours_played > 10;
```

```sql
SELECT g.Name AS Game, ach.Name AS Achievement, ach.Goal as AchievementGoal
FROM Achievement ach
JOIN Game g ON ach.game_id = g.game_id
ORDER BY g.Name;
```

```sql
SELECT Name, Release_date
FROM Game
WHERE Release_date > '2020-01-02';
```

```sql
SELECT ui.Email, ui.PhoneNumber
FROM UserInfo ui
JOIN AppUser a ON ui.appuser_id = a.appuser_id
WHERE a.username = 'RobertPolson';
```

#### UPDATE
```sql
UPDATE UserInfo
SET
    Email = 'sashashlyapik11@example.com',
    PhoneNumber = '+380974938601'
WHERE appuser_id = 1;
```

```sql
UPDATE AppUser
SET
    username = 'daniilmagnojezz',
    password = '$2a$12$ytCpHKYDmvxEdtARudh3puu/8PF4.zhAID4CLDDdUA/r4fQitGlGS'
WHERE appuser_id = 3;
```

```sql
UPDATE GameCollection
SET
    Name = 'Top Strategy'
WHERE Name = 'Strategy';
```

```sql
UPDATE Game
SET
    Price = Price * 0.8
WHERE game_id = 2;
```

```sql
UPDATE Category
SET
	Description = 'Games that simulate real-world or fictional activities, like THE SIMS'
WHERE Name = 'Simulation';
```

```sql
UPDATE Progress
SET
	Hours_played = 63
WHERE userlibrary_id = 5 AND game_id = 4;
```

```sql
UPDATE UnlockedAchievement
SET 
	Data_complete = '2021-04-15'
WHERE userlibrary_id = 2;
```

```sql
UPDATE GameCategory
SET
	category_id = 2
WHERE game_id = 4;
```

#### DELETE
```sql
DELETE 
FROM UnlockedAchievement
WHERE userlibrary_id = 1 
  AND game_id = 3 
  AND achievement_id = 3;
```

```sql
DELETE
FROM LibraryCollection
WHERE userlibrary_id = 1
  AND game_id = 3
  AND gamecollection_id = 2;
```

```sql
DELETE 
FROM UnlockedAchievement
WHERE achievement_id = 1;
```

```sql
DELETE 
FROM Achievement
WHERE achievement_id = 1;
```

```sql
DELETE
FROM UserInfo
WHERE appuser_id = 3;
```

```sql
DELETE 
FROM Progress
WHERE userlibrary_id = 3
  AND game_id = 3;
```

```sql
DELETE
FROM GameCategory
WHERE game_id = 1
  AND category_id = 4
```


