-- ORDERED AVERAGE RATING WITH NAMES
SELECT 
	AVG([main].[Rating]) AS [Average rating]
	,[spectacles].[Title]
FROM [dbo].[Ratings] AS [main]
INNER JOIN [dbo].[Spectacles] [spectacles] ON [spectacles].[SpectacleId] = [main].[SpectacleId]
GROUP BY 
	[spectacles].[Title]
ORDER BY [Average rating] DESC

-- MOST RETURNED TICKETS SPECTACLE
SELECT 
	[spectacles].Title
	,COUNT(*) AS [Total Returned Tickets]
FROM [dbo].[Tickets] [main]
INNER JOIN [dbo].[Events] [events] ON [events].EventId = [main].EventId
INNER JOIN [dbo].[Spectacles] [spectacles] ON [spectacles].[SpectacleId] = [events].SpectacleId
WHERE [main].IsReturned = 1
GROUP BY 
	[spectacles].Title
ORDER BY [Total Returned Tickets] DESC


-- LAST performance date for each actor
SELECT [main].[ActorId]
		,[actors].[FirstName]
		,MAX([lastSpectacle].[Last Spectacle]) AS [Last Event Date]
FROM [dbo].[ActorsInSpectacles] [main]
LEFT JOIN [dbo].[Actors] [actors] ON [main].ActorId = [actors].ActorId
LEFT JOIN (
	SELECT [events].[SpectacleId], MAX([events].[Date]) AS [Last Spectacle]
	FROM [dbo].[Events] [events]
	GROUP BY [events].[SpectacleId]
) [lastSpectacle] ON [lastSpectacle].[SpectacleId] = [main].[SpectacleId]
GROUP BY
	[main].[ActorId],
	[actors].[FirstName]
ORDER BY 
	[main].[ActorId]

-- IN WHAT GENRE ACTORS PLAY AT MOST