using Bomberman.Classes;
using System.Net;

namespace ServerBomberman
{
    /// <summary>
    /// Class for message parsing and interpretation
    /// </summary>
    public class GameInterpreter
    {
        /// <summary>
        /// Guid received from a client
        /// </summary>
        public Guid PlayerID { get; private set; }

        public GameInterpreter()
        {

        }

        /// <summary>
        /// Initial message string parse
        /// </summary>
        /// <param name="input">Message received from client.</param>
        /// <returns>Int array of three elements, where [0] and [1] are X and Y coords, and [2] for either action or error code.</returns>
        public int[] Parse(string input)
        {
            string[] strings = input.Split(' ');

            int[] response = new int[3];

            for (int i = 0; i < response.Length; i++)
            {
                response[i] = 0;
            }

            if (strings.Length != 2)
            {
                if (strings.Length == 1 && strings[0] == "connect")
                {
                    response[2] = 1;
                    return response;
                }

                response[2] = 400;
                return response;
            }


            try
            {
                PlayerID = new(strings[1]);
            }
            catch
            {
                response[2] = 402;
                return response;
            }

            switch (strings[0])
            {
                case "left":
                    {
                        --response[0];
                        break;
                    }
                case "right":
                    {
                        ++response[0];
                        break;
                    }
                case "up":
                    {
                        --response[1];
                        break;
                    }
                case "down":
                    {
                        ++response[1];
                        break;
                    }
                case "place":
                    {
                        response[2] = 4;
                        break;
                    }
                case "disconnect":
                    {
                        response[2] = 2;
                        break;
                    }


                default:
                    {
                        response[2] = 400;
                        break;
                    }
            }


            return response;
        }

        /// <summary>
        /// Creates a new player entity and invokes the Connection method of the session object
        /// </summary>
        /// <param name="session">session to connect a new player to</param>
        /// <returns>True if successful, False if not</returns>
        public bool ConnectPlayer(Session session, EndPoint endPoint)
        {
            Player player = new Player(0, 0, endPoint);
            bool success = session.Connect(player);
            if(success)
            {
                PlayerID = player.ID;
                return true;
            }

            return false;
        }

        public bool DisconnectPlayer(Session session, Player player)
        {
            return session.Disconnect(player);
        }

        /// <summary>
        /// Processes given session and player according to the response
        /// </summary>
        /// <param name="response">Contains XY coords. and code for invoking certain action</param>
        /// <param name="player">Player entity that performs the action</param>
        /// <param name="session">Active session the player belongs to</param>
        /// <returns>Integer value 200 for successful operation, -1 in case of a failure, or a corresponding error code</returns>
        public int DoAction(int[] response, Session session)
        {
            bool success = false;

            Player? player = session.FindPlayerById(PlayerID);

            int finalResponse;

            if (player == null)
            {
                finalResponse = 402;
                return finalResponse;
            }

            switch (response[2])
            {
                // 0 for move
                case 0:
                    {
                        success = session.Move(player, response[0], response[1]);
                        break;
                    }
                // 1 for connect
                case 1:
                    {
                        //connect method
                        break;
                    }
                // 2 for disconnect
                case 2:
                    {
                        success = DisconnectPlayer(session, player);
                        break;
                    }
                // 3 for placing
                case 3:
                    {
                        //place method
                        break;
                    }
            }

            finalResponse = success ? 200 : 201;
            return finalResponse;
        }
    }
}
