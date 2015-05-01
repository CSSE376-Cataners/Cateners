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
        Assert.AreEqual(gameLobby.Players, lobby.Players);
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
            Assert.AreEqual(gameLobby.Players[i], lobby.Players[i]);
        }
    }

    }

}
