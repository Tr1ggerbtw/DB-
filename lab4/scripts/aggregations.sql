-- count amount of bought games
SELECT COUNT(*) as total_bought_games 
FROM progress

-- avg hours people have wasted on games 
SELECT AVG(Hours_played) as avg_hours
from Progress

-- count each category usage in games
SELECT
	category_id,
    COUNT(*) AS usage_count
FROM
    gamecategory
GROUP BY
    category_id;

-- select youngest user..... (min for oldest)
select MAX(Birthday) as youngest_user
from UserInfo
