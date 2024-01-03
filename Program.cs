using System.Data;
using System.Text.Json;
using Dapper;
using first.data;
using first.model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            // string sqlString = @" 
            //     INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            //     ) VALUES ('" + newComputer.Motherboard
            //     + "', '" + newComputer.HasWifi
            //     + "', '" + newComputer.HasLTE
            //     + "', '" + newComputer.ReleaseDate
            //     + "', '" + newComputer.Price
            //     + "', '" + newComputer.VideoCard
            //     + "')";

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

            //Console.WriteLine(sqlQuery);

            // IEnumerable<Computer>? computers = dataContextEF.Computer?.ToList<Computer>();

            // if (computers != null)
            // {
            //     foreach (var singleComputer in computers)
            //     {
            //         Console.WriteLine(singleComputer.Motherboard);
            //     }
            // }

            // File.WriteAllText("log.txt", sqlQuery);

            //using StreamWriter streamWriter = new("log.txt", append: true);

            // streamWriter.WriteLine(sqlQuery + "\n");

            // streamWriter.Close();
            string computersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(computersJson);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? jsoncomputers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computers != null)
            {
                foreach (Computer computer in computers)
                {
                    string sqlString = @" 
                INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                CPUCores,
                ReleaseDate,
                Price,
                VideoCard
                ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
               + "', '" + computer.HasWifi
               + "', '" + computer.HasLTE
               + "', '" + computer.CPUCores
               + "', '" + computer.ReleaseDate
               + "', '" + computer.Price
               + "', '" + EscapeSingleQuote(computer.VideoCard)
               + "')";

                    bool result = dataContextDapper.ExecuteSql(sqlString);

                    Console.WriteLine(result);
                }

            }

            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()

            };

            string computerCopyNewtonSoft = JsonConvert.SerializeObject(computers, settings);
            File.WriteAllText("computerCopyNewtonSoft.txt", computerCopyNewtonSoft);

            string computerCopySystem = System.Text.Json.JsonSerializer.Serialize(computers, options: options);

            File.WriteAllText("computerCopySystem.txt", computerCopySystem);



        }

        public static string EscapeSingleQuote(string inpute)
        {
            string output = inpute.Replace("'", "''");

            return output;
        }
    }
}