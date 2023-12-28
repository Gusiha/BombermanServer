using Bomberman.Classes;

namespace ServerBomberman
{
    public static class GamingParser
    {
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
