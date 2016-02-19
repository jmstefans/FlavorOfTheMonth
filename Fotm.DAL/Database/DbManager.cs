using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Fotm.DAL.Database.DataProvider;
using Fotm.DAL.Models;
using Fotm.DAL.Models.Base;
using WowDotNetAPI.Models;

namespace Fotm.DAL.Database
{
    /// <summary>
    /// Persistent DB class for reading and writing to the database. 
    /// </summary>
    public class DbManager : ManagerBase<DbManager>, IDisposable
    {
        #region Members

        private DataProviderBase _dataProvider;
        private IDbConnection _dbConnection;

        #endregion

        #region Properties

        private IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                    throw new ArgumentException("DB connection cannot be null.");

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                return _dbConnection;
            }
        }

        #endregion

        #region Data Provider / Connection

        /// <summary>
        /// Sets the active data provider.
        /// </summary>
        /// <param name="dataProviderType">The type of data provider <see cref="DataProviderFactory.DataProviderType"/> being set.</param>
        /// <param name="connectionProperties"></param>
        public void SetDataProvider(DataProviderFactory.DataProviderType dataProviderType, params string[] connectionProperties)
        {
            _dataProvider = DataProviderFactory.GetDataProvider(dataProviderType, connectionProperties);
            _dbConnection?.Dispose();
            _dbConnection = _dataProvider.GetDataProviderConnection();
        }

        #endregion

        #region CRUD

        #region Create

        #region Create Scripts

        private const string TEAM_INSERT_QUERY =
            "insert into [Team] (Bracket, MeanRatingChange, MeanRating, ModifiedDate, ModifiedStatus, ModifiedUserID) " +
            "values(@Bracket, @MeanRatingChange, @MeanRating, @ModifiedDate, @ModifiedStatus, @ModifiedUserID);" +
            "select scope_identity();";

        private const string TEAM_MEMBER_INSERT_QUERY =
            "insert into [TeamMember] " +
            "(TeamID, RatingChangeValue, CurrentRating, CharacterID, SpecID, RaceID, FactionID, GenderID,ModifiedDate, ModifiedStatus, ModifiedUserID) " +
            "values" +
            "(@TeamID, @RatingChangeValue, @CurrentRating, @CharacterID, @SpecID, @RaceID, @FactionID, @GenderID, @ModifiedDate, @ModifiedStatus, @ModifiedUserID);";

        #endregion

        /// <summary>
        /// Asynchronously inserts an enumerable collection of DbEntityBase objects into DB. 
        /// </summary>
        /// <typeparam name="T">The DbEntity to insert <see cref="DbEntityBase"/></typeparam>
        /// <param name="objects">DbEntityBase objects to insert into DB.</param>
        public async void InsertObjectsAsync<T>(IEnumerable<T> objects) where T : DbEntityBase, new()
        {
            await Task.Run(() => InsertObjects(objects));
        }

        /// <summary>
        /// Inserts an enumerable collection of DbEntityBase objects into DB. 
        /// Note - This method uses reflection for the object properties.
        ///        So if performance is an issue, consider a custom insert.
        /// TODO: possible refactor - dapper should handle the property mapping, reflection may not be needed.
        /// </summary>
        /// <typeparam name="T">The DbEntity to insert <see cref="DbEntityBase"/></typeparam>
        /// <param name="objects">DbEntityBase objects to insert into DB.</param>
        public void InsertObjects<T>(IEnumerable<T> objects) where T : DbEntityBase, new()
        {
            var type = typeof(T);

            using (var trans = DbConnection.BeginTransaction())
            {
                try
                {
                    var cols = GetColumnNames<T>();
                    var colPar = GetColumnParameters(cols);

                    foreach (var obj in objects)
                    {
                        var query =
                       $"insert into [{type.Name}] ({string.Join(",", cols)}, ModifiedDate, ModifiedStatus, ModifiedUserID) " +
                       $"values ({string.Join(",", colPar)}, '{new SqlDateTime(DateTime.Now)}', 'I', 0);";

                        DbConnection.Execute(query, obj, trans);
                    }

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Console.WriteLine("Failed: " + e);
                }
            }
        }
        
        public void InsertTeamsAndMembers(IEnumerable<Team> teams)
        {
            //using (var trans = DbConnection.BeginTransaction())
            //{
            //    try
            //    {
            foreach (var team in teams)
            {
                var teamId = DbConnection.ExecuteScalar<long>(TEAM_INSERT_QUERY, team);
                //var teamId = DbConnection.ExecuteScalar<long>(TEAM_INSERT_QUERY, team, trans);
                Console.WriteLine($"{DateTime.Now}: Inserting Team: " + teamId);

                foreach (var teamMember in team.TeamMembers)
                {
                    var dbTeam = GetTeamByTeamId(teamId);
                    teamMember.Team = dbTeam;

                    DbConnection.Execute(TEAM_MEMBER_INSERT_QUERY, teamMember);
                    //DbConnection.Execute(TEAM_MEMBER_INSERT_QUERY, teamMember, trans);
                    Console.WriteLine($"{DateTime.Now}: Inserting team member, character ID: " +
                                              teamMember.CharacterID);
                }
            }

            //    trans.Commit();
            //}
            //catch (Exception e)
            //{
            //    trans.Rollback();
            //    Console.WriteLine($"{DateTime.Now}: Insert teams and members failed: " + e);
            //}
            //}
        }
        
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="apiCharacter"></param>
        /// <param name="pvpStats"></param>
        public void InsertApiCharacterAsDbCharacter(WowDotNetAPI.Models.Character apiCharacter, PvpStats pvpStats)
        {
            var realm = GetRealmByName(apiCharacter.Realm);
            if (realm == null)
            {
                InsertNewRealm(apiCharacter.Realm);
                realm = GetRealmByName(apiCharacter.Realm);
            }

            var spec = GetSpecByName(pvpStats.Spec);
            if (spec == null)
            {
                InsertNewSpec(pvpStats.Spec);
                spec = GetSpecByName(pvpStats.Spec);
            }

            var raceId = pvpStats.RaceId;
            var classId = (int)apiCharacter.Class;
            var factionId = pvpStats.FactionId;
            var genderId = pvpStats.GenderId;

            var character = new Character
            {
                Name = apiCharacter.Name,
                RealmID = realm.RealmID,
                SpecID = spec.SpecID,
                RaceID = raceId,
                ClassID = classId,
                FactionID = Convert.ToBoolean(factionId),
                GenderID = Convert.ToBoolean(genderId),
                SeasonWins = pvpStats.SeasonWins,
                SeasonLosses = pvpStats.SeasonLosses,
                WeeklyWins = pvpStats.WeeklyWins,
                WeeklyLosses = pvpStats.WeeklyLosses,
                ModifiedDate = DateTime.Now,
                ModifiedStatus = "I",
                ModifiedUserID = 0
            };

            var columns = new[]
            {
                "Name", "RealmID", "SpecID", "RaceID", "ClassID", "FactionID", "GenderID", "SeasonWins", "SeasonLosses",
                "WeeklyWins", "WeeklyLosses", "ModifiedDate", "ModifiedStatus", "ModifiedUserID"
            };
            var colParams = GetColumnParameters(columns);
            var query =
                      $"insert into [Character] ({string.Join(",", columns)}) " +
                      $"values ({string.Join(",", colParams)});";

            DbConnection.Execute(query, character);
        }

        public void InsertNewRealm(string realmName)
        {
            var query = $"insert into [Realm] (Name, ModifiedDate, ModifiedStatus, ModifiedUserID) values (@Name, '{DateTime.Now}', 'I', 0);";
            DbConnection.Execute(query, new { Name = realmName });
        }

        public void InsertNewSpec(string blizzSpecName)
        {
            var query = $"insert into [Spec] (Name, BlizzName, ModifiedDate, ModifiedStatus, ModifiedUserID) " +
                        $"values (@Name, @BlizzName, '{DateTime.Now}', 'I', 0);";
            DbConnection.Execute(query, new { Name = blizzSpecName, BlizzName = blizzSpecName });
        }

        #endregion Create

        #region Read

        public Character GetCharacter(string name, int realmId)
        {
            var query = "select * from Character where Name = @Name and RealmID = @RealmID";
            return DbConnection.Query<Character>(query, new { Name = name, RealmID = realmId }).FirstOrDefault();
        }

        public Class GetClassByName(string name)
        {
            var query = "select * from Class where Name = @Name;";
            return DbConnection.Query<Class>(query, new { Name = name }).FirstOrDefault();
        }

        public Realm GetRealmByName(string name)
        {
            var query = "select * from Realm where Name = @Name;";
            return DbConnection.Query<Realm>(query, new { Name = name }).FirstOrDefault();
        }

        public Race GetRaceByName(string name)
        {
            var query = "select * from Race where Name = @Name;";
            return DbConnection.Query<Race>(query, new { Name = name }).FirstOrDefault();
        }

        public Spec GetSpecByName(string name)
        {
            var query = "select * from Spec where BlizzName = @BlizzName;";
            return DbConnection.Query<Spec>(query, new { BlizzName = name }).FirstOrDefault();
        }


        public Team GetTeamByTeamId(long teamId)
        {
            var query = "select * from Team where TeamID = @TeamID";
            return DbConnection.Query<Team>(query, new { TeamID = teamId }).FirstOrDefault();
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Helpers 

        private string[] GetColumnNames<T>() where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var columns = new List<string>();
            var txtInfo = new CultureInfo("en-US", false).TextInfo;

            for (var i = 0; i < properties.Length; i++)
            {
                var col = txtInfo.ToTitleCase(properties[i].Name);
                columns.Add(col);
            }

            return columns.ToArray();
        }

        private string[] GetColumnParameters(string[] columnNames)
        {
            return columnNames.Select(c => $"@{c}").ToArray();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }

        #endregion

    }
}
