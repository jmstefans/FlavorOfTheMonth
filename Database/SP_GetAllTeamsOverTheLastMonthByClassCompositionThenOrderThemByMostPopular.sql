USE [fotm]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular]    Script Date: 2/29/2016 11:21:29 PM ******/
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
CREATE PROCEDURE [dbo].[SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular] 
	@Bracket nvarchar(50) = '_3v3',  -- Default 3v3
	@RegionID int = 0				 -- Default US
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.TeamID, cl.Name, s.BlizzName, t.Bracket, t.ModifiedDate
		FROM fotm.dbo.Team t
		LEFT OUTER JOIN fotm.dbo.TeamMember tm on t.TeamID = tm.TeamID
		LEFT OUTER JOIN fotm.dbo.[Character] c on tm.CharacterID = c.CharacterID
		LEFT OUTER JOIN fotm.dbo.Class cl on c.ClassID = cl.ClassID
		LEFT OUTER JOIN fotm.dbo.Spec s on c.SpecID = s.SpecID
		LEFT OUTER JOIN fotm.dbo.Realm r on c.RealmID = r.RealmID
		WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
			AND t.Bracket = @Bracket
			AND r.RegionID = @RegionID
		ORDER BY t.TeamID, cl.Name, s.BlizzName
END
GO

