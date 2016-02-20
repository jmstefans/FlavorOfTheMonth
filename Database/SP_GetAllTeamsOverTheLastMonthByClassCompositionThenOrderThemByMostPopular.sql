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
CREATE PROCEDURE SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.TeamID, cl.Name
		FROM fotm.dbo.Team t
		LEFT OUTER JOIN fotm.dbo.TeamMember tm on t.TeamID = tm.TeamID
		LEFT OUTER JOIN fotm.dbo.[Character] c on tm.CharacterID = c.CharacterID
		LEFT OUTER JOIN fotm.dbo.Class cl on c.ClassID = cl.ClassID
		WHERE tm.ModifiedDate >= DATEADD(DAY, -30, SYSDATETIME())
END
GO