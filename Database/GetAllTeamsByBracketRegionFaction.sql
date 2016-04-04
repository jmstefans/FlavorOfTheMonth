USE [fotm]
GO

/****** Object:  StoredProcedure [dbo].[GetAllTeamsByBracketRegionFaction]    Script Date: 4/2/2016 12:05:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<John Stefanski>
-- Create date: <2/19/2016>
-- Description:	<Gather all of the teams for the
-- past month and group them by class composition.
-- Then return that list sorted by most popular.>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllTeamsByBracketRegionFaction] 
	@Bracket nvarchar(50) = '_3v3',  -- Default 3v3
	@RegionID int = 0,				 -- Default US
	@FactionID int = -1				 -- Default Any faction
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (@FactionID = -1)
	begin
		Print 'Faction is any'
		SELECT t.TeamID, cl.Name, s.BlizzName, t.Bracket, t.ModifiedDate, tm.FactionID
				FROM dbo.Team t
				LEFT OUTER JOIN dbo.TeamMember tm on t.TeamID = tm.TeamID
				LEFT OUTER JOIN dbo.[Character] c on tm.CharacterID = c.CharacterID
				LEFT OUTER JOIN dbo.Class cl on c.ClassID = cl.ClassID
				LEFT OUTER JOIN dbo.Spec s on c.SpecID = s.SpecID
				LEFT OUTER JOIN dbo.Realm r on c.RealmID = r.RealmID
				WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
					AND t.Bracket = @Bracket
					AND r.RegionID = @RegionID
				ORDER BY t.TeamID, cl.Name, s.BlizzName
	end
	if (@FactionID = 0)
	begin
		Print 'Faction is 0'
		SELECT t.TeamID, cl.Name, s.BlizzName, t.Bracket, t.ModifiedDate
				FROM dbo.Team t
				LEFT OUTER JOIN dbo.TeamMember tm on t.TeamID = tm.TeamID
				LEFT OUTER JOIN dbo.[Character] c on tm.CharacterID = c.CharacterID
				LEFT OUTER JOIN dbo.Class cl on c.ClassID = cl.ClassID
				LEFT OUTER JOIN dbo.Spec s on c.SpecID = s.SpecID
				LEFT OUTER JOIN dbo.Realm r on c.RealmID = r.RealmID
				WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
					AND t.Bracket = @Bracket
					AND r.RegionID = @RegionID
					AND tm.FactionID = @FactionID
				ORDER BY t.TeamID, cl.Name, s.BlizzName
	end
	if (@FactionID = 1)
	begin
		Print 'Faction is 1'
		SELECT t.TeamID, cl.Name, s.BlizzName, t.Bracket, t.ModifiedDate
				FROM dbo.Team t
				LEFT OUTER JOIN dbo.TeamMember tm on t.TeamID = tm.TeamID
				LEFT OUTER JOIN dbo.[Character] c on tm.CharacterID = c.CharacterID
				LEFT OUTER JOIN dbo.Class cl on c.ClassID = cl.ClassID
				LEFT OUTER JOIN dbo.Spec s on c.SpecID = s.SpecID
				LEFT OUTER JOIN dbo.Realm r on c.RealmID = r.RealmID
				WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
					AND t.Bracket = @Bracket
					AND r.RegionID = @RegionID
					AND tm.FactionID = @FactionID
				ORDER BY t.TeamID, cl.Name, s.BlizzName
	end
END

GO

