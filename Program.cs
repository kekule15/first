using System.Data;
using Dapper;
using first.data;
using first.model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace first
{


    public class Program
    {

        public static void Main(string[] args)


        {


            string sqlCommand = "SELECT GETDATE()";

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            DataContextDapper dataContextDapper = new(config);

            DataContextEF dataContextEF = new(config);

            DateTime rightNow = dataContextDapper.LoadSingleDataa<DateTime>(sqlCommand);

            // Console.WriteLine(rightNow.ToString());

            Computer newComputer = new()
            {
                Motherboard = "Linux DXF Board",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 100.19m,
                VideoCard = "RTX 2060"
            };

            string sqlString = @" 
                INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
                ) VALUES ('" + newComputer.Motherboard
                + "', '" + newComputer.HasWifi
                + "', '" + newComputer.HasLTE
                + "', '" + newComputer.ReleaseDate
                + "', '" + newComputer.Price
                + "', '" + newComputer.VideoCard
                + "')";

            // Console.WriteLine(sqlString);

            //  bool result = dataContextDapper.ExecuteSql(sqlString);

            //Console.WriteLine(result);


            string sqlQuery = @" SELECT 
                Computer.ComputerId, 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.CPUCores,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            Console.WriteLine(sqlQuery);

            IEnumerable<Computer>? computers = dataContextEF.Computer?.ToList<Computer>();

            if (computers != null)
            {
                foreach (var singleComputer in computers)
                {
                    Console.WriteLine(singleComputer.Motherboard);
                }
            }

            // Console.WriteLine(newComputer.Motherboard);
        }
    }
}