# ЗВІТ З ЛАБОРАТОРНОЇ РОБОТИ №1

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
```

#### UserLibrary
```sql
CREATE TABLE UserLibrary
(
    library_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL,
    CONSTRAINT fk_userlibrary_user FOREIGN KEY (user_id) REFERENCES AppUser(user_id)
);
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
    Description TEXT,
    Release_date DATE
);
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
