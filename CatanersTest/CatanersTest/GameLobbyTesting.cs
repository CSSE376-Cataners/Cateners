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

    

}
