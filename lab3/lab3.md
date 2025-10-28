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
<img width="841" height="172" alt="category" src="https://github.com/user-attachments/assets/116e1e7b-6bff-4bce-be2e-11a19a1106ea" />

```sql
SELECT name, price
FROM Game
WHERE price > 50;
```
<img width="304" height="91" alt="game" src="https://github.com/user-attachments/assets/d0b5a3bd-aa7d-4696-a2b8-6ff2679673c6" />

```sql
SELECT email, birthday
FROM UserInfo
WHERE birthday > '1990-01-01';
```
<img width="295" height="90" alt="userinfo" src="https://github.com/user-attachments/assets/cffe323e-9252-4efc-90e7-7438600a8247" />

```sql
SELECT game.name AS GameName, category.name AS CategoryName
FROM Game
JOIN GameCategory ON game.game_id = gamecategory.game_id
JOIN Category ON gamecategory.category_id = category.category_id;
```
<img width="347" height="191" alt="gamejoin" src="https://github.com/user-attachments/assets/a2d7d67e-2a77-4845-8c5c-ae3f50dea388" />

```sql
SELECT u.appuser_id, a.username, p.game_id, p.Hours_played
FROM Progress p
JOIN UserLibrary u ON p.userlibrary_id = u.userlibrary_id
JOIN AppUser a ON u.appuser_id = a.appuser_id
WHERE p.Hours_played > 10;
```
<img width="469" height="142" alt="progress" src="https://github.com/user-attachments/assets/079ebc59-3d23-47ab-bf0a-a283bfb280cf" />

```sql
SELECT g.Name AS Game, ach.Name AS Achievement, ach.Goal as AchievementGoal
FROM Achievement ach
JOIN Game g ON ach.game_id = g.game_id
ORDER BY g.Name;
```
<img width="634" height="144" alt="achievemnt" src="https://github.com/user-attachments/assets/8c102afe-2660-47b7-bb1a-cab38e51a5dd" />

```sql
SELECT Name, Release_date
FROM Game
WHERE Release_date > '2020-01-02';
```
<img width="297" height="97" alt="gameez" src="https://github.com/user-attachments/assets/b072568b-ef3b-47ee-ac9b-0c25d799b343" />

```sql
SELECT ui.Email, ui.PhoneNumber
FROM UserInfo ui
JOIN AppUser a ON ui.appuser_id = a.appuser_id
WHERE a.username = 'RobertPolson';
```
<img width="374" height="68" alt="userinfojoin" src="https://github.com/user-attachments/assets/e594d59f-51eb-43ba-8bb2-535eed91c38a" />

#### UPDATE
```sql
UPDATE UserInfo
SET
    Email = 'sashashlyapik11@example.com',
    PhoneNumber = '+380974938601'
WHERE appuser_id = 1;
```
<img width="552" height="167" alt="userinfo" src="https://github.com/user-attachments/assets/527931c6-c333-4216-b13e-fcd128700a5b" />

```sql
UPDATE AppUser
SET
    username = 'daniilmagnojezz',
    password = '$2a$12$ytCpHKYDmvxEdtARudh3puu/8PF4.zhAID4CLDDdUA/r4fQitGlGS'
WHERE appuser_id = 3;
```
<img width="702" height="171" alt="appuser" src="https://github.com/user-attachments/assets/71d783a4-2755-47c0-89ff-0f38778fe347" />

```sql
UPDATE GameCollection
SET
    Name = 'Top Strategy'
WHERE Name = 'Strategy';
```
<img width="434" height="195" alt="gamecollection" src="https://github.com/user-attachments/assets/f150ef20-cb3b-4eed-b03f-470a76205f72" />

```sql
UPDATE Game
SET
    Price = Price * 0.8
WHERE game_id = 2;
```
<img width="933" height="145" alt="game" src="https://github.com/user-attachments/assets/d1500100-4d1f-4664-9e93-4c80557896f6" />

```sql
UPDATE Category
SET
	Description = 'Games that simulate real-world or fictional activities, like THE SIMS'
WHERE Name = 'Simulation';
```
<img width="847" height="166" alt="category" src="https://github.com/user-attachments/assets/2fdf26c7-abfb-499c-be48-0baf7142cb17" />

```sql
UPDATE Progress
SET
	Hours_played = 63
WHERE userlibrary_id = 5 AND game_id = 4;
```
<img width="337" height="166" alt="progress" src="https://github.com/user-attachments/assets/7b3d49a0-dbae-4bcf-a68c-1f14874ebddf" />

```sql
UPDATE UnlockedAchievement
SET 
	Data_complete = '2021-04-15'
WHERE userlibrary_id = 2;
```
<img width="466" height="143" alt="unlockedachievement" src="https://github.com/user-attachments/assets/14580366-33b5-45d9-8f6d-e49f7d9f885d" />

```sql
UPDATE GameCategory
SET
	category_id = 2
WHERE game_id = 4;
```
<img width="222" height="192" alt="gamecategory" src="https://github.com/user-attachments/assets/0245fdc2-76c9-47ed-acc4-6397cbba0f81" />

#### DELETE
```sql
DELETE 
FROM UnlockedAchievement
WHERE userlibrary_id = 1 
  AND game_id = 3 
  AND achievement_id = 3;
```
<img width="471" height="113" alt="unlockedachievement" src="https://github.com/user-attachments/assets/85c8629e-6217-431b-9f5d-e56adfa15c39" />

```sql
DELETE
FROM LibraryCollection
WHERE userlibrary_id = 1
  AND game_id = 3
  AND gamecollection_id = 2;
```
<img width="368" height="148" alt="librarycollection" src="https://github.com/user-attachments/assets/f9f090e1-15e9-47ee-9e6b-ee5151c69d00" />

```sql
DELETE 
FROM UnlockedAchievement
WHERE achievement_id = 1;
```
<img width="466" height="94" alt="unlockedachievement2" src="https://github.com/user-attachments/assets/fe68d16d-dd79-4d11-941c-e25eb157868f" />

```sql
DELETE 
FROM Achievement
WHERE achievement_id = 1;
```
<img width="680" height="121" alt="achievement" src="https://github.com/user-attachments/assets/9a6e9235-828a-477a-991c-6cd18049a8f5" />

```sql
DELETE
FROM UserInfo
WHERE appuser_id = 3;
```
<img width="559" height="144" alt="userinfo" src="https://github.com/user-attachments/assets/31985f86-b08d-4f77-9534-da327c014db5" />

```sql
DELETE 
FROM Progress
WHERE userlibrary_id = 3
  AND game_id = 3;
```
<img width="344" height="155" alt="progress" src="https://github.com/user-attachments/assets/6d9be583-2aa0-4a4e-8f24-e2f51778825b" />

```sql
DELETE
FROM GameCategory
WHERE game_id = 1
  AND category_id = 4
```
<img width="224" height="169" alt="gamecategory" src="https://github.com/user-attachments/assets/a3c9ceb4-2e6a-4875-9f07-61cb962afb74" />

