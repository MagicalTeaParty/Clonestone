using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klassenplanung
{
    class GameWorld
    {
        //Fields
        Camera mainCamera;
        MenuButton menuButton;
        Menu gameMenu;
        GameBoard gameBoard;
        NetworkManager nM;
        Player firstPlayer;
        Player secondPlayer;
        List<Player> spectators;
    }
}