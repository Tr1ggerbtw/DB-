# ЗВІТ З ЛАБОРАТОРНОЇ РОБОТИ №2

## Тема: Перетворення вашої ER-діаграми на схему PostgreSQL

### Працювали над лабораторною роботою:
* **Легеза Данііл Павлович IM-41**
* **Бойко Данило Сергійович IM-41**

### SQL-скрипт(и)

#### AppUser
```sql
CREATE TABLE AppUser
(
    appuser_id BIGSERIAL PRIMARY KEY,
    username VARCHAR(32) NOT NULL UNIQUE,
    password VARCHAR(128) NOT NULL
);
-- insert example
INSERT INTO AppUser (username, password)
VALUES 
    ('SashaShlyapik', '$2a$12$Qn7uFgVs1HbxzLIMVKadJOo9wAhGzmp18LAe0wRYvM99YqWIh94lO'),
    ('RobertPolson', '$2a$12$wPSeNt5Ig4k/ctiEDQbN..w8cBSGxG7M6eHbqSe9cVAe7uCWg6ZFy'),
    ('Magnojezz', '$2a$12$l0b1cygLGtQB7D5.ziKJiuwNpryJH/Pjr6JCGvGPWaEZjGZCo6zSe');
```

#### UserInfo
```sql
CREATE TABLE UserInfo
(
    appuser_id BIGINT PRIMARY KEY,
    PhoneNumber VARCHAR(15),
    Email VARCHAR(254),
    Birthday DATE,
    CONSTRAINT fk_userinfo_user FOREIGN KEY (appuser_id) REFERENCES AppUser(appuser_id)
);
-- insert example
INSERT INTO UserInfo (appuser_id, PhoneNumber, Email, Birthday)
VALUES
    (1, '+380501234567', 'sasha.shlyapik@example.com', '1986-04-07'),
    (2, '+380679876543', 'robert.polson@example.com', '1889-01-06'),
    (3, NULL, 'magnojezz@example.com', '2006-09-11');
```

#### UserLibrary
```sql
CREATE TABLE UserLibrary
(
    userlibrary_id BIGSERIAL PRIMARY KEY,
    appuser_id BIGINT NOT NULL,
    CONSTRAINT fk_userlibrary_user FOREIGN KEY (appuser_id) REFERENCES AppUser(appuser_id)
);
-- insert example
INSERT INTO UserLibrary (appuser_id)
VALUES
    (1), 
    (2), 
    (3);
```

#### GameCollection
```sql
CREATE TABLE GameCollection
(
    gamecollection_id BIGSERIAL PRIMARY KEY,
    userlibrary_id BIGINT NOT NULL,
    Name VARCHAR(32) NOT NULL,
    CONSTRAINT fk_collection_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id)
);
-- insert example
INSERT INTO GameCollection (userlibrary_id, Name) 
VALUES 
	(1, 'Favourites'),
	(1, 'Legendary'),
	(2, 'Must Play'),
	(3, 'Cozy Games');
```

#### Category
```sql
CREATE TABLE Category
(
    category_id BIGSERIAL PRIMARY KEY,
    Name VARCHAR(32) NOT NULL,
    Description TEXT,
    Age_min INTEGER
);
-- insert example
INSERT INTO Category (Name, Description, Age_min)
VALUES 
	('Strategy', 'Games that require careful planning and skill to achieve victory.', 12), 
	('RPG', 'Role-playing games where playes assume the roles of characters in a fictional settings.', 16),
	('Simulation', 'Games that simulate real-world or fictional activities.', 6),
	('Adventure', 'Games with a focus on exploration and puzzle-solving.', 8);
```

#### Game
```sql
CREATE TABLE Game
(
    game_id BIGSERIAL PRIMARY KEY,
    Price DECIMAL(10, 2) CHECK (Price > 0),
    Name VARCHAR(64) NOT NULL,
    Description TEXT,
    Release_date DATE
);
-- insert example
INSERT INTO Game (Price, Name, Description, Release_date)
VALUES
    (25.99, 'Shadow Friend', 'Late king decrees the next monarch to be elected by the people. Become one.', '2021-09-02'),
    (64.28, 'Mars Colony Tycoon', 'Colonize Mars, but be aware of price of mistakes', '2010-03-10'),
    (88.14, 'Cats, Cats and Cats', 'Meow, meow, meow, meow', '2019-05-05');
```

#### Achievement
```sql
CREATE TABLE Achievement
(
    achievement_id BIGSERIAL PRIMARY KEY,
    game_id BIGINT NOT NULL,
    Name VARCHAR(64) NOT NULL,
    Goal TEXT,
    CONSTRAINT fk_achievement_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);
-- insert example
INSERT INTO Achievement (game_id, Name, Goal)
VALUES
    (1, 'Silent Influence', 'Win the election without publicly campaigning.'),
    (2, 'Self-Sufficient', 'Produce all necessary resources locally on Mars.'),
    (3, 'Meow-narch', 'Unlock the legendary "King Cat".');
```

#### LibraryCollection
```sql
CREATE TABLE LibraryCollection
(
    gamecollection_id BIGINT NOT NULL,
    userlibrary_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    PRIMARY KEY (gamecollection_id, userlibrary_id, game_id),
    CONSTRAINT fk_libcoll_collection FOREIGN KEY (gamecollection_id) REFERENCES GameCollection(gamecollection_id),
    CONSTRAINT fk_libcoll_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id),
    CONSTRAINT fk_libcoll_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);
```

#### Progress
```sql
CREATE TABLE Progress
(
    userlibrary_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    Hours_played INTEGER,
    PRIMARY KEY (userlibrary_id, game_id),
    CONSTRAINT fk_progress_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id),
    CONSTRAINT fk_progress_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);
```

#### GameCategory
```sql
CREATE TABLE GameCategory
(
    game_id BIGINT NOT NULL,
    category_id BIGINT NOT NULL,
    PRIMARY KEY (game_id, category_id),
    CONSTRAINT fk_gamecat_game FOREIGN KEY (game_id) REFERENCES Game(game_id),
    CONSTRAINT fk_gamecat_category FOREIGN KEY (category_id) REFERENCES Category(category_id)
);
-- insert example
INSERT INTO GameCategory (game_id, category_id) 
VALUES 
	(1, 2),
	(1, 4),
	(2, 1),
	(2, 3),
	(3, 3);
```

#### UnlockedAchievement
```sql
CREATE TABLE UnlockedAchievement
(
    userlibrary_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    achievement_id BIGINT NOT NULL,
    Data_complete DATE,
    PRIMARY KEY (userlibrary_id, game_id, achievement_id),
    CONSTRAINT fk_unlocked_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id),
    CONSTRAINT fk_unlocked_game FOREIGN KEY (game_id) REFERENCES Game(game_id),
    CONSTRAINT fk_unlocked_achievement FOREIGN KEY (achievement_id) REFERENCES Achievement(achievement_id)
);
```
