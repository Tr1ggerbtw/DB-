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

```sql
INSERT INTO Category (Name, Description, Age_min)
VALUES 
    ('Puzzle', 'Games that emphasize puzzle-solving.', 3);
```

```sql
INSERT INTO GameCategory (game_id, category_id)
VALUES 
    (4, 5);
```

```sql
INSERT INTO Achievement (game_id, Name, Goal)
VALUES 
    (4, 'Time Paradox', 'Complete level 10');
```

```sql
INSERT INTO AppUser (username, password)
VALUES
    ('ViktorLeheza', '$2a$12$tfsmdmlBWXk22YnIkqOfT.0HbbnrMSxBm92StIlmc4PZNKNcv4cCa'),
    ('AlinaParadoxy', '$2a$12$Fo0Vcj0waLK0riGn07xW9uv/5V5tZFwpFrBamWKtoQTw8GbF0EmAC');
```

```sql
INSERT INTO UserInfo (appuser_id, PhoneNumber, Email, Birthday)
VALUES
    (4, '+380979734812', 'viktorleheza@example.com', '1964-03-25'),
    (5, '+380675197698', 'paradoxy11@example.com', '2003-11-19');
```

```sql
INSERT INTO UserLibrary (appuser_id)
VALUES 
    (4), (5);
```

```sql
INSERT INTO GameCollection (userlibrary_id, Name)
VALUES
    (4, 'Recently Added'),
    (5, 'Strategy');
```

```sql
INSERT INTO LibraryCollection (gamecollection_id, userlibrary_id, game_id)
VALUES
    (5, 4, 4);
```

```sql
INSERT INTO Progress (userlibrary_id, game_id, Hours_played)
VALUES
    (5, 4, 25);
```

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


