using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CollegeTournament
{
    public class SportTournament
    {
        public static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4L01UE8;Initial Catalog=TournamentDatabase;Integrated Security=True;Encrypt=False");

        public static void AddPlayer(string studentName, string dept, int teamId)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select s.maxPlayers from SportsDetails s left join SportTeam t on t.sportId=s.sportId where t.sportTeamId={teamId};";
            int totalteamSize = (int)cmd.ExecuteScalar();
            cmd.CommandText = $"select count(s.studentId) from StudentPlayer s left join SportTeam t on t.sportTeamId={teamId}";
            int currentSize = (int)cmd.ExecuteScalar();
            if (currentSize < totalteamSize)
            {
                cmd.CommandText = $"INSERT INTO StudentPlayer VALUES('{studentName}','{dept}',{teamId})";
                cmd.ExecuteReader().Close();
            }
            else
            {
                Console.WriteLine("Cannot Add. Reached Maximum size");
            }
            conn.Close();
        }

        public static void AddSport(string sportName, int maxPlayers)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = $"INSERT INTO SportsDetails VALUES('{sportName}',{maxPlayers})";
                cmd.ExecuteReader().Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"{sportName} already exist!");
            }
            conn.Close();
        }

        public static void RemoveSport(string sportName)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM SportsDetails WHERE sportName='{sportName}'";
            cmd.ExecuteReader().Close();
            conn.Close();
        }

        public static void AddTournament(string tournamentName)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = $"INSERT INTO TournamentList VALUES('{tournamentName}')";
                cmd.ExecuteReader().Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"{tournamentName} already exist!");
            }
            conn.Close();
        }
        public static void RemoveTournament(string tournamentName)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM TournamentList WHERE tournamentName='{tournamentName}'";
            cmd.ExecuteReader().Close();
            conn.Close();
        }

        public static void AddScoreboard(int tournamentId, int SportId, int teamId, int studentId, int score)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO ScoreBoard VALUES({tournamentId},{SportId},{teamId},{studentId},{score})";
            cmd.ExecuteReader().Close();
            conn.Close();
        }
        public static void EditScoreboard(int tournamentId, int SportId, int teamId, int studentId, int score)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE TABLE ScoreBoard SET={score} WHERE tournamentId={tournamentId} AND SportId={SportId} AND teamId={teamId} AND studentId={studentId}";
            cmd.ExecuteReader().Close();
            conn.Close();
        }

        public static void AddSportTeam(string teamName, int tournamentId, int sportId)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO SportTeam Values('{teamName}',{tournamentId},{sportId})";
            cmd.ExecuteReader().Close();
            conn.Close();
        }


        public static void Main(string[] args)
        {
            AddTournament("RDTournament");
            AddSport("Cricket", 11);
            AddSport("Women Cricket", 11);
            AddSportTeam("teamName", 1, 1);
            AddPlayer("Yuvateja", "CSE", 1);
            AddScoreboard(1, 1, 1, 1, 90);
            AddPlayer("Harish", "ECE", 1);
            AddPlayer("Guru", "ECE", 1);
            AddPlayer("Faf", "CSE", 1);
            AddPlayer("ABD", "CSE", 1);
            AddPlayer("King Kohli", "CSE", 1);
            AddPlayer("R pant", "CSE", 1);
            AddPlayer("S Yadav", "CSE", 1);
            AddPlayer("R Sharma", "CSE", 1);
            AddPlayer("Markram", "CSE", 1);
            AddPlayer("Archer", "CSE", 1);
            RemoveSport("Women Cricket");
            RemoveTournament("RDTournament");
            AddSport("Cricket", 11);
            AddTournament("RDTournament");
            Console.WriteLine("Done");
        }

    }
}
