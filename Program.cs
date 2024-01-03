using System.Data;
using System.Text.Json;
using AutoMapper;
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
            // string computersJson = File.ReadAllText("ComputersSnake.json");

            // Console.WriteLine(computersJson);

            // JsonSerializerOptions options = new()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? jsoncomputers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            // IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            // if (computers != null)
            // {
            //     foreach (Computer computer in computers)
            //     {
            //         string sqlString = @" 
            //     INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     CPUCores,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            //     ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            //    + "', '" + computer.HasWifi
            //    + "', '" + computer.HasLTE
            //    + "', '" + computer.CPUCores
            //    + "', '" + computer.ReleaseDate
            //    + "', '" + computer.Price
            //    + "', '" + EscapeSingleQuote(computer.VideoCard)
            //    + "')";

            //         bool result = dataContextDapper.ExecuteSql(sqlString);

            //         Console.WriteLine(result);
            //     }

            // }

            // JsonSerializerSettings settings = new()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()

            // };

            // string computerCopyNewtonSoft = JsonConvert.SerializeObject(computers, settings);
            // File.WriteAllText("computerCopyNewtonSoft.txt", computerCopyNewtonSoft);

            // string computerCopySystem = System.Text.Json.JsonSerializer.Serialize(computers, options: options);

            // File.WriteAllText("computerCopySystem.txt", computerCopySystem);



            string computersJson = File.ReadAllText("ComputersSnake.json");


            // Mapper mapper = new(new MapperConfiguration((cfg =>
            // {
            //     cfg.CreateMap<ComputerSnake, Computer>()
            //     .ForMember(destination => destination.ComputerId, options => options.MapFrom(source => source.computer_id))
            //     .ForMember(destination => destination.Motherboard, options => options.MapFrom(source => source.motherboard))
            //     .ForMember(destination => destination.VideoCard, options => options.MapFrom(source => source.video_card))
            //     .ForMember(destination => destination.HasLTE, options => options.MapFrom(source => source.has_lte))
            //     .ForMember(destination => destination.HasWifi, options => options.MapFrom(source => source.has_wifi))
            //     .ForMember(destination => destination.Price, options => options.MapFrom(source => source.price))
            //     .ForMember(destination => destination.ReleaseDate, options => options.MapFrom(source => source.release_date));
            // })));

            IEnumerable<Computer>? jsoncomputersSnakes = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);


            if (jsoncomputersSnakes != null)
            {
                //  IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(jsoncomputersSnakes);
                foreach (Computer computerItem in jsoncomputersSnakes)
                {
                    Console.WriteLine(computerItem.Motherboard);
                }
            }





        }

        // public static string EscapeSingleQuote(string inpute)
        // {
        //     string output = inpute.Replace("'", "''");

        //     return output;
        // }



    }
}