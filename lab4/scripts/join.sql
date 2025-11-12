-- connect appuser with userinfo(if there is no extended information for user in userinfo, we return nothing(not even null), since it's inner join)

SELECT
au.username, ui.Email, ui.PhoneNumber
FROM
AppUser as au
INNER JOIN
UserInfo AS ui ON au.appuser_id = ui.appuser_id;

-- for each game choose their categories, if there is no category == null, thereafter sort and select

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

-- find games without progress

SELECT
    G.Name AS GameName,
    P.Hours_played
FROM
    Progress AS P
RIGHT JOIN
    Game AS G ON P.game_id = G.game_id
WHERE
    P.Hours_played IS NULL;


