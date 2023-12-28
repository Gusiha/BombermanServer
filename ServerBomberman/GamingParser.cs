namespace ServerBomberman
{
    public static class GamingParser
    {
        static int[] Parse(string input)
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
    }
}
