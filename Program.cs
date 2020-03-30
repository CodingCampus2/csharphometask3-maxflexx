using System;
using CodingCampusCSharpHomework;

namespace HomeworkTemplate
{
    class Program
    {
        static float degreesToRadians(float degrees)
        {
            return (float)(Math.PI / 180f) * degrees;
        }

        static float distanceMySolution(float lon1, float lat1, float lon2, float lat2)
        {
            float R = 6371f;

            float dLat = degreesToRadians(lat2 - lat1);
            float dLon = degreesToRadians(lon2 - lon1);

            float a = (float)(Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(degreesToRadians(lat1)) * Math.Cos(degreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2));
            float c = 2f * (float)Math.Asin(Math.Min(1, Math.Sqrt(a)));
            float d = R * c;

            return d;
        }

        static float distanceCodingGameSoluion(float lon1, float lat1, float lon2, float lat2)
        {

            float x = (lon2 - lon1) * (float)Math.Cos(lat1 + lat2 / 2);
            float y = lat2 - lat1;
            float sq = (float)Math.Sqrt(Math.Pow(x, 2) + (float)Math.Pow(y, 2));
            return sq * 6371f;
        }

        static void ImplementationForCodingGame()
        {
            const int DEFIB_NAME = 1;
            const int DEFIB_ADDRESS = 2;
            const int DEFIB_LON = 4;
            const int DEFIB_LAT = 5;

            string LON = Console.ReadLine();
            string LAT = Console.ReadLine();
            float userLong = degreesToRadians(float.Parse(LON.Replace(',', '.')));
            float userLat = degreesToRadians(float.Parse(LAT.Replace(',', '.')));
            float minDistance = float.MaxValue;
            string resultName = "";
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                string DEFIB = Console.ReadLine();
                string[] words = DEFIB.Split(';');
                float lon = float.Parse(words[DEFIB_LON].Replace(',', '.'));
                float lat = float.Parse(words[DEFIB_LAT].Replace(',', '.'));
                float currentDistance = distanceCodingGameSoluion(degreesToRadians(lon), degreesToRadians(lat), userLong, userLat);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    resultName = words[DEFIB_NAME];
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            Console.WriteLine($"{resultName}");
        }

        static void Main(string[] args)
        {
            Func<Task3, string> TaskSolver = task =>
            {
                // Your solution goes here
                // You can get all needed inputs from task.[Property]
                // Good luck!


                // THIS SOLUTION DOES NOT PASS TESTS FOR SOME REASON
                // I HAVE CREATED "ImplementationForCodingGame()" AND TESTED IT ON CodingGame 
                string UserLongitude = task.UserLongitude;
                string UserLatitude = task.UserLatitude;
                float userLong = float.Parse(UserLongitude);
                float userLat = float.Parse(UserLatitude);

                int placesAmount = task.DefibliratorStorages.Length;
                
                float minDistance = float.MaxValue;
                int minIndex = 0;

                for (int i = 0; i < placesAmount; i++)
                {
                    string defibliratorStorage = task.DefibliratorStorages[i];
                    string[] words = defibliratorStorage.Split(';');

                    float lon = float.Parse(words[2]);
                    float lat = float.Parse(words[3]);
                    float currentDistance = distanceMySolution(lon, lat, userLong, userLat);
                    if (currentDistance < minDistance)
                    {
                        minIndex = i;
                        minDistance = currentDistance;
                    }
                }
                string[] resultWords = task.DefibliratorStorages[minIndex].Split(';');
                return $"Name:{resultWords[0]}; Address:{resultWords[1]}";
            };

            Task3.CheckSolver(TaskSolver);
        }
    }
}

