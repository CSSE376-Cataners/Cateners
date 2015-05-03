using CatanersShared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersTest
{
    [TestFixture]
    class GameLobbyTesting
    {
        
        
    [Test]
    public void TestConstructor(){
        Lobby lobby = new Lobby("lobby", 100, new Player("LichKing"), 33);
        lobby.addPlayer(new Player("Arthus"));

        GameLobby gameLobby = new GameLobby(lobby);
        Assert.AreEqual(gameLobby.GameName, lobby.GameName);
        Assert.AreEqual(gameLobby.MaxTimePerTurn, lobby.MaxTimePerTurn);
        Assert.AreEqual(gameLobby.Owner, lobby.Owner);
        Assert.AreEqual(gameLobby.lobbyID, lobby.lobbyID);
        Assert.AreEqual(gameLobby.gamePlayers.Count, lobby.Players.Count);
    }

    [Test]
    public void TestThatAllPlayersGetAddedToGameLobby()
    {
        Lobby lobby = new Lobby("You Are Not Prepared", 100, new Player("Arthus"), 33);
        lobby.addPlayer(new Player("LadySylvanas"));
        lobby.addPlayer(new Player("Illidan"));

        GameLobby gameLobby = new GameLobby(lobby);

        for (int i = 0; i < lobby.PlayerCount; i++)
        {
            Assert.AreEqual(gameLobby.gamePlayers[i], lobby.Players[i]);
        }
    }

    [Test]
    public void testGameLobbyToJson()
    {
        Lobby newLobby = new Lobby("Test", 3, new Player("Gandalf"), 2);
        GameLobby gamelobby = new GameLobby(newLobby);
        Console.WriteLine(gamelobby.toJson());
        Assert.AreEqual("{\"gamePlayers\":[{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Gandalf\"}],\"lobbyID\":2,\"allReady\":false,\"Players\":[{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Gandalf\"}],\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Gandalf\"},\"PlayerCount\":1}", gamelobby.toJson());
    }

    [Test]
    public void testGameLobbyFromJson()
    {
        Lobby testLobby = new Lobby("Test", 3, new Player("Saruman"), 2);
        GameLobby gamelobby = new GameLobby(testLobby);
        Console.WriteLine(gamelobby);
        Lobby newLobby = Lobby.fromJson("{\"Players\":[{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Saruman\"}],\"lobbyID\":2,\"allReady\":false,\"GameName\":\"Test\",\"MaxTimePerTurn\":3,\"Owner\":{\"resources\":{\"Brick\":0,\"Ore\":0,\"Sheep\":0,\"Wheat\":0,\"Wood\":0},\"Ready\":false,\"Username\":\"Saruman\"},\"PlayerCount\":1}");
        Console.WriteLine(newLobby);
        Assert.True(testLobby.Equals(newLobby));
    }

    }

}
