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

        static float calculateDistance(float lon1, float lat1, float lon2, float lat2)
        {

            float x = (lon2 - lon1) * (float)Math.Cos(lat1 + lat2 / 2);
            float y = lat2 - lat1;
            float sq = (float)Math.Sqrt(Math.Pow(x, 2) + (float)Math.Pow(y, 2));
            return sq * 6371f;
        }

        static void Main(string[] args)
        {
            Func<Task3, string> TaskSolver = task =>
            {
                // Your solution goes here
                // You can get all needed inputs from task.[Property]
                // Good luck!
                string UserLongitude = task.UserLongitude;
                string UserLatitude = task.UserLatitude;
                float userLong = degreesToRadians(float.Parse(UserLongitude));
                float userLat = degreesToRadians(float.Parse(UserLatitude));

                int placesAmount = task.DefibliratorStorages.Length;
                
                float minDistance = float.MaxValue;
                int minIndex = 0;

                for (int i = 0; i < placesAmount; i++)
                {
                    string defibliratorStorage = task.DefibliratorStorages[i];
                    string[] words = defibliratorStorage.Split(';');

                    float lon = float.Parse(words[2]);
                    float lat = float.Parse(words[3]);
                    float currentDistance = calculateDistance(degreesToRadians(lon), degreesToRadians(lat), userLong, userLat);
                    if (currentDistance < minDistance)
                    {
                        minIndex = i;
                        minDistance = currentDistance;
                    }
                }
                string[] resultWords = task.DefibliratorStorages[minIndex].Split(';');
                return $"Name: {resultWords[0]}; Address: {resultWords[1]}";
            };

            Task3.CheckSolver(TaskSolver);
        }
    }
}

