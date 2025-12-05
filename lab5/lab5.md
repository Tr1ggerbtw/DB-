# ЗВІТ З ЛАБОРАТОРНОЇ РОБОТИ №5

## Тема: Нормалізація бази даних

---

### Працювали над лабораторною роботою:
* **Легеза Данііл Павлович IM-41**
* **Бойко Данило Сергійович IM-41**

---

## 1. Вступ

У цій лабораторній роботі було проведено аналіз схеми бази даних онлайн-бібліотеки комп'ютерних ігор на відповідність нормальним формам.

Хоча початкова схема була добре спроектована, проаналізувавши, ми виявили кілька транзитивних залежностей у таблицях зв'язків, які були виправлені в процесі нормалізації.

---

## 2. Функціональні Залежності Початкової Схеми

Аналіз показує, що більшість таблиць вже відповідають вимогам 3NF, оскільки в них відсутні часткові та транзитивні залежності. Кожен неключовий атрибут залежить виключно від усього первинного ключа.

### 2.1. Таблиці без порушень (відповідають 3NF)

**1. `AppUser(appuser_id, username, password)`**
* **ПК:** `appuser_id`
* **ФЗ:** `appuser_id → username, password`

**2. `UserInfo(appuser_id, PhoneNumber, Email, Birthday)`**
* **ПК:** `appuser_id`
* **ФЗ:** `appuser_id → PhoneNumber, Email, Birthday`

**3. `UserLibrary(userlibrary_id, appuser_id)`**
* **ПК:** `userlibrary_id`
* **ФЗ:** `userlibrary_id → appuser_id`

**4. `GameCollection(gamecollection_id, userlibrary_id, Name)`**
* **ПК:** `gamecollection_id`
* **ФЗ:** `gamecollection_id → userlibrary_id, Name`

**5. `Category(category_id, Name, Description, Age_min)`**
* **ПК:** `category_id`
* **ФЗ:** `category_id → Name, Description, Age_min`

**6. `Game(game_id, Price, Name, Description, Release_date)`**
* **ПК:** `game_id`
* **ФЗ:** `game_id → Price, Name, Description, Release_date`

**7. `Achievement(achievement_id, game_id, Name, Goal)`**
* **ПК:** `achievement_id`
* **ФЗ:** `achievement_id → game_id, Name, Goal`

**8. `Progress(userlibrary_id, game_id, Hours_played)`**
* **ПК:** `(userlibrary_id, game_id)`
* **ФЗ:** `(userlibrary_id, game_id) → Hours_played`

**9. `GameCategory(game_id, category_id)`**
* **ПК:** `(game_id, category_id)`
* **ФЗ:** Тривіальна (немає неключових атрибутів).

### 2.2. Таблиці з виявленими порушеннями (Кандидати на нормалізацію)

**1. `LibraryCollection(gamecollection_id, userlibrary_id, game_id)`**
* **ПК:** `(gamecollection_id, userlibrary_id, game_id)`
* **ФЗ:**
    * `gamecollection_id → userlibrary_id`
    * *Проблема:* Атрибут `userlibrary_id` залежить лише від частини складеного ключа (`gamecollection_id`).

**2. `UnlockedAchievement(userlibrary_id, game_id, achievement_id, Data_complete)`**
* **ПК:** `(userlibrary_id, game_id, achievement_id)`
* **ФЗ:**
    * `achievement_id → game_id`
    * `(userlibrary_id, achievement_id) → Data_complete`
    * *Проблема:* Атрибут `game_id` визначається іншим атрибутом ключа (`achievement_id`), а не всім ключем.

---

## 3. Аналіз нормальних форм та план нормалізації

### 3.1. Перша нормальна форма (1NF)

Відношення знаходиться в 1NF, якщо всі атрибути є атомарними, відсутні повторювані групи, і є первинний ключ.

**Висновок:** Усі таблиці початкової схеми мають атомарні атрибути та визначені ключі. Схема відповідає 1NF.

### 3.2. Друга нормальна форма (2NF)

**Вимога:** Жоден неключовий атрибут не повинен залежати від частини складеного ключа.

**Аналіз `LibraryCollection`:**
У цій таблиці часткова функціональну залежність: `gamecollection_id → userlibrary_id`. Атрибут `userlibrary_id` є зайвим у цій сутності, оскільки колекція вже прив'язана до бібліотеки у таблиці `GameCollection`.

**Рішення: Змінюємо структуру таблиці, видаляючи атрибут userlibrary_id. Тепер таблиця LibraryCollection слугує лише для зв'язку "багато-до-багатьох" між GameCollection та Game.**

### 3.3. Третя нормальна форма (3NF)

**Вимога:** Жоден неключовий атрибут не повинен залежати транзитивно від ключа.

**Аналіз `UnlockedAchievement`:**
У цій таблиці присутня транзитивна залежність. Атрибут `game_id` залежить від `achievement_id`. Зберігання ідентифікатора гри в цій таблиці створює надлишковість, оскільки кожна ачівка вже належить унікальній грі.

**Рішення: Видаляємо атрибут game_id з таблиці UnlockedAchievement.**

---

## 4. Фінальний SQL DDL (Нормалізована схема — 3NF)

```sql
CREATE TABLE AppUser
(
    appuser_id BIGSERIAL PRIMARY KEY,
    username VARCHAR(32) NOT NULL UNIQUE,
    password VARCHAR(128) NOT NULL
);

CREATE TABLE UserInfo
(
    appuser_id BIGINT PRIMARY KEY,
    PhoneNumber VARCHAR(15),
    Email VARCHAR(254),
    Birthday DATE,
    CONSTRAINT fk_userinfo_user FOREIGN KEY (appuser_id) REFERENCES AppUser(appuser_id)
);

CREATE TABLE UserLibrary
(
    userlibrary_id BIGSERIAL PRIMARY KEY,
    appuser_id BIGINT NOT NULL,
    CONSTRAINT fk_userlibrary_user FOREIGN KEY (appuser_id) REFERENCES AppUser(appuser_id)
);

CREATE TABLE GameCollection
(
    gamecollection_id BIGSERIAL PRIMARY KEY,
    userlibrary_id BIGINT NOT NULL,
    Name VARCHAR(32) NOT NULL,
    CONSTRAINT fk_collection_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id)
);

CREATE TABLE Category
(
    category_id BIGSERIAL PRIMARY KEY,
    Name VARCHAR(32) NOT NULL,
    Description TEXT,
    Age_min INTEGER
);

CREATE TABLE Game
(
    game_id BIGSERIAL PRIMARY KEY,
    Price DECIMAL(10, 2) CHECK (Price > 0),
    Name VARCHAR(64) UNIQUE NOT NULL,
    Description TEXT,
    Release_date DATE
);

CREATE TABLE Achievement
(
    achievement_id BIGSERIAL PRIMARY KEY,
    game_id BIGINT NOT NULL,
    Name VARCHAR(64) NOT NULL,
    Goal TEXT,
    CONSTRAINT fk_achievement_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);

CREATE TABLE LibraryCollection
(
    gamecollection_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    PRIMARY KEY (gamecollection_id, game_id),
    CONSTRAINT fk_libcoll_collection FOREIGN KEY (gamecollection_id) REFERENCES GameCollection(gamecollection_id),
    CONSTRAINT fk_libcoll_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);

CREATE TABLE Progress
(
    userlibrary_id BIGINT NOT NULL,
    game_id BIGINT NOT NULL,
    Hours_played INTEGER,
    PRIMARY KEY (userlibrary_id, game_id),
    CONSTRAINT fk_progress_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id),
    CONSTRAINT fk_progress_game FOREIGN KEY (game_id) REFERENCES Game(game_id)
);

CREATE TABLE GameCategory
(
    game_id BIGINT NOT NULL,
    category_id BIGINT NOT NULL,
    PRIMARY KEY (game_id, category_id),
    CONSTRAINT fk_gamecat_game FOREIGN KEY (game_id) REFERENCES Game(game_id),
    CONSTRAINT fk_gamecat_category FOREIGN KEY (category_id) REFERENCES Category(category_id)
);


CREATE TABLE UnlockedAchievement
(
    userlibrary_id BIGINT NOT NULL,
    achievement_id BIGINT NOT NULL,
    Data_complete DATE,
    PRIMARY KEY (userlibrary_id, achievement_id),
    CONSTRAINT fk_unlocked_library FOREIGN KEY (userlibrary_id) REFERENCES UserLibrary(userlibrary_id),
    CONSTRAINT fk_unlocked_achievement FOREIGN KEY (achievement_id) REFERENCES Achievement(achievement_id)
);
```
