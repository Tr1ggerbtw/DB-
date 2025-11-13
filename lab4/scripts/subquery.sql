-- inner join name with hours, group_by, count hours for each game, filter it with having, afterwards sort by descending order, limit to show limited amount

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

-- look for anyone with at least 1 achievement, after with where choose users with achievement that we need, and select them

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

-- join AppUser with UserInfo, calculate total hours played using subquery, order by hours descending

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

