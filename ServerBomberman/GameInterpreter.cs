using Bomberman.Classes;

namespace ServerBomberman
{
    /// <summary>
    /// Class for message parsing and interpretation
    /// </summary>
    public static class GameInterpreter
    {
        /// <summary>
        /// Initial message string parse
        /// </summary>
        /// <param name="input">Message received from client.</param>
        /// <returns>Int array of three elements, where [0] and [1] are X and Y coords, and [2] for either action or error code.</returns>
        public static int[] Parse(string input)
        {
            string[] strings = input.Split(' ');

            int[] response = new int[3];
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = 0;
            }

            if (strings.Length != 2)
            {
                response[2] = 400;
                return response;
            }

            switch (strings[1])
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
                case "connect":
                    {
                        response[2] = 1;
                        break;
                    }
                case "disconnect":
                    {
                        response[2] = 2;
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
        public static bool ConnectPlayer(Session session)
        {
            return session.Connect(new Player(0, 0));
        }

        /// <summary>
        /// Processes given session and player according to the response
        /// </summary>
        /// <param name="response">Contains XY coords. and code for invoking certain action</param>
        /// <param name="player">Player entity that performs the action</param>
        /// <param name="session">Active session the player belongs to</param>
        /// <returns>Int array of two elements, where [0]: Action code; [1]: 0 for successful operation and -1 in case of a failure</returns>
        public static int[] DoAction(int[] response, Player player, Session session)
        {
            bool success = false;

            int[] finalResponse = new int[2];
            finalResponse[0] = response[2];

            switch (response[2])
            {
                case 0:
                    {
                        success = session.Move(player, response[0], response[1]);
                        break;
                    }
                case 1:
                    {
                        //connect method
                        break;
                    }
                case 2:
                    {
                        //disconnect method
                        break;
                    }
                case 3:
                    {
                        //place method
                        break;
                    }
            }

            finalResponse[1] = success ? 1 : 0;
            return finalResponse;
        }
    }
}
