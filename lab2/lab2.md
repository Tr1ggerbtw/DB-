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
    user_id BIGSERIAL PRIMARY KEY,
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
    user_id BIGINT PRIMARY KEY,
    PhoneNumber VARCHAR(15),
    Email VARCHAR(254),
    Birthday DATE,
    CONSTRAINT fk_userinfo_user FOREIGN KEY (user_id) REFERENCES AppUser(user_id)
);
-- insert example
INSERT INTO UserInfo (user_id, PhoneNumber, Email, Birthday)
VALUES
    (1, '+380501234567', 'sasha.shlyapik@example.com', '1986-04-07'),
    (2, '+380679876543', 'robert.polson@example.com', '1889-01-06'),
    (3, NULL, 'magnojezz@example.com', '2006-09-11');
```

#### UserLibrary
```sql
CREATE TABLE UserLibrary
(
    library_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL,
    CONSTRAINT fk_userlibrary_user FOREIGN KEY (user_id) REFERENCES AppUser(user_id)
);
-- insert example
INSERT INTO UserLibrary (user_id)
VALUES
    (1), 
    (2), 
    (3);

```

#### GameCollection
```sql
CREATE TABLE GameCollection
(
    collection_id BIGSERIAL PRIMARY KEY,
    library_id BIGINT NOT NULL,
    Name VARCHAR(32) NOT NULL,
    CONSTRAINT fk_collection_library FOREIGN KEY (library_id) REFERENCES UserLibrary(library_id)
);
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
```

#### Game
```sql

CREATE TABLE Game
(
    game_id BIGSERIAL PRIMARY KEY,
    Price DECIMAL(10, 2),
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
    collection_id BIGINT NOT NULL,
    library_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    PRIMARY KEY (collection_id, library_id, game_id),
    CONSTRAINT fk_libcoll_collection FOREIGN KEY (collection_id) REFERENCES GameCollection(collection_id),
    CONSTRAINT fk_libcoll_library FOREIGN KEY (library_id) REFERENCES UserLibrary(library_id),
    CONSTRAINT fk_libcoll_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);
```

#### Progress
```sql
CREATE TABLE Progress
(
    library_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    Hours_played INTEGER,
    PRIMARY KEY (library_id, game_id),
    CONSTRAINT fk_progress_library FOREIGN KEY (library_id) REFERENCES UserLibrary(library_id),
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
```

#### UnlockedAchievement
```sql
CREATE TABLE UnlockedAchievement
(
    library_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    achievement_id BIGINT NOT NULL,
    Data_complete DATE,
    PRIMARY KEY (library_id, game_id, achievement_id),
    CONSTRAINT fk_unlocked_library FOREIGN KEY (library_id) REFERENCES UserLibrary(library_id),
    CONSTRAINT fk_unlocked_game FOREIGN KEY (game_id) REFERENCES Game(game_id),
    CONSTRAINT fk_unlocked_achievement FOREIGN KEY (achievement_id) REFERENCES Achievement(achievement_id)
);
```
