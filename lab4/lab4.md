# ЗВІТ З ЛАБОРАТОРНОЇ РОБОТИ №4

## Тема: Аналітичні SQL-запити(OLAP)

### Працювали над лабораторною роботою:
* **Легеза Данііл Павлович IM-41**
* **Бойко Данило Сергійович IM-41**

## Дані в таблицях(над якими будемо проводити операції)

### appuser

<img width="696" height="130" alt="image" src="https://github.com/user-attachments/assets/6aabd8f4-e952-4934-a922-c67649e78834" />

### userinfo

<img width="624" height="128" alt="image" src="https://github.com/user-attachments/assets/095022a4-d72b-4a77-b2d5-cbbd39be5933" />

### userlibrary

<img width="247" height="126" alt="image" src="https://github.com/user-attachments/assets/d161e51d-6688-46cc-ab09-10f57c7e851c" />

### librarycollection

<img width="369" height="152" alt="image" src="https://github.com/user-attachments/assets/236613c1-ee92-4254-8a09-77d5ccda567d" />

### gamecollection

<img width="435" height="152" alt="image" src="https://github.com/user-attachments/assets/8c501bab-7a1e-4513-9477-81e0638fe1f0" />

### game

<img width="922" height="125" alt="image" src="https://github.com/user-attachments/assets/63c569ac-7d39-49d3-8315-9c9371d633a9" />

### category

<img width="830" height="147" alt="image" src="https://github.com/user-attachments/assets/a22f5ea7-f21f-4cca-b4a7-85f05702e2be" />

### gamecategory

<img width="224" height="175" alt="image" src="https://github.com/user-attachments/assets/df32b241-fe7b-4395-a17d-6c025c8fdb19" />

### progress

<img width="343" height="150" alt="image" src="https://github.com/user-attachments/assets/969a94f3-ca8e-4354-8143-2d28fb50729d" />

### achievement

<img width="668" height="122" alt="image" src="https://github.com/user-attachments/assets/219bb84d-4ca1-4e21-98f3-97f6f8eef212" />

### unlockedachievement

<img width="467" height="146" alt="image" src="https://github.com/user-attachments/assets/6f0a583a-b34b-4585-89eb-a2dd1ec517c3" />

# Виконання скриптів

## aggregations.sql
```sql
SELECT COUNT(*) as total_bought_games 
FROM progress
```
<img width="194" height="76" alt="image" src="https://github.com/user-attachments/assets/b9789ecc-1eee-411f-9d40-86bc58b7b36b" /><br>
```sql
SELECT AVG(Hours_played) as avg_hours
from Progress
```
<img width="202" height="69" alt="image" src="https://github.com/user-attachments/assets/0f0fad05-d2bd-4bb2-9054-c4ddd78cf085" /><br>
```sql
SELECT
	category_id,
    COUNT(*) AS usage_count
FROM
    gamecategory
GROUP BY
    category_id;
```
<img width="245" height="147" alt="image" src="https://github.com/user-attachments/assets/e4a5e8ee-81a0-497d-a2f2-39a5cd4bd121" /><br>
```sql
select MAX(Birthday) as youngest_user
from UserInfo
```
<img width="162" height="76" alt="image" src="https://github.com/user-attachments/assets/6b1f72f3-f6ce-4357-aa02-659c19b9061e" /><br>

## join.sql
```sql
SELECT
au.username, ui.Email, ui.PhoneNumber
FROM
AppUser as au
INNER JOIN
UserInfo AS ui ON au.appuser_id = ui.appuser_id;
```
<img width="525" height="118" alt="image" src="https://github.com/user-attachments/assets/88edec6a-8e41-4e6f-91f5-8534f3f20f97" /><br>
```sql
SELECT
    G.Name AS GameName,
    C.Name AS CategoryName
FROM
    Game AS G
LEFT JOIN
    GameCategory AS GC ON G.game_id = GC.game_id
LEFT JOIN
    Category AS C ON GC.category_id = C.category_id 
ORDER BY
    G.Name;
```
<img width="342" height="168" alt="image" src="https://github.com/user-attachments/assets/c3264a79-8374-4b3a-9fd3-df32520e6c98" /><br>
```sql
SELECT
    G.Name AS GameName,
    P.Hours_played
FROM
    Progress AS P
RIGHT JOIN
    Game AS G ON P.game_id = G.game_id
WHERE
    P.Hours_played IS NULL;
```
<img width="344" height="117" alt="image" src="https://github.com/user-attachments/assets/a09b63c9-9cd3-423b-a907-e9343ab61124" /><br>

## subquery.sql
```sql
SELECT
    FilteredGames.name,
    FilteredGames.Total_Hours
FROM (
        SELECT
            G.name,
            SUM(P.hours_played) AS Total_Hours
        FROM
            Progress AS P
        INNER JOIN
            Game AS G ON G.game_id = P.game_id
        GROUP BY
            G.name
		HAVING SUM(P.hours_played) > 1000
    ) AS FilteredGames
ORDER BY 
	FilteredGames.Total_Hours DESC
LIMIT 1
```
<img width="291" height="66" alt="image" src="https://github.com/user-attachments/assets/bbbe1004-ace3-46ad-a6da-4b772ed8fa04" /><br> 
```sql
SELECT
    AU.username
FROM
    AppUser AS AU
INNER JOIN
    UserLibrary AS UL ON AU.appuser_id = UL.appuser_id
WHERE
    UL.userlibrary_id IN (
        SELECT
            UA.userlibrary_id
        FROM
            UnlockedAchievement AS UA
        INNER JOIN
            Achievement AS A ON UA.achievement_id = A.achievement_id
        WHERE
            A.Name = 'Meow-narch'
    );
```
<img width="198" height="90" alt="image" src="https://github.com/user-attachments/assets/026a81a2-1514-44c5-ad7f-285ef83554bb" /><br> 
```sql
SELECT
    AU.username,
    UI.Email,
    (
        SELECT
            SUM(P.Hours_played)
        FROM
            UserLibrary AS UL
        INNER JOIN
            Progress AS P ON UL.userlibrary_id = P.userlibrary_id
        WHERE
            UL.appuser_id = AU.appuser_id
    ) AS Total_Hours_Played
FROM
    AppUser AS AU
INNER JOIN
    UserInfo AS UI ON AU.appuser_id = UI.appuser_id
	ORDER BY
    Total_Hours_Played DESC;
```
<img width="508" height="116" alt="image" src="https://github.com/user-attachments/assets/df0f2fb9-237c-4836-a0cd-969d95072a7c" /><br> 


