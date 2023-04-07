create database TournamentDatabase;
use TournamentDatabase;
create Table SportsDetails(sportId int primary key identity, sportName nchar(20) unique, maxPlayers int);
create Table StudentPlayer(studentId int primary key identity, studentName nchar(50), deptName nchar(10), teamId int);
create table TournamentList(tournamentId int primary key identity, tournamentName nchar(50));
create table ScoreBoard(scoreId int primary key identity, tournamentId int, sportId int, teamId int, studentId int, score int);
create table SportTeam(sportTeamId int primary key identity,teamName nchar(20), tournamentId int, sportId int);
