using System.Data;
using Dapper;
using first.data;
using first.model;
using Microsoft.Data.SqlClient;

namespace first
{


    public class Program
    {

        public static void Main(string[] args)


        {


            string sqlCommand = "SELECT GETDATE()";

            DataContextDapper dataContextDapper = new();

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
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            Console.WriteLine(sqlQuery);

            IEnumerable<Computer> computers = dataContextDapper.LoadData<Computer>(sqlQuery);

            foreach (var singleComputer in computers)
            {
                Console.WriteLine(singleComputer.Motherboard);
            }



            // Console.WriteLine(newComputer.Motherboard);
        }
    }
}