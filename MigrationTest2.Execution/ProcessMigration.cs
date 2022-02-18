using Microsoft.EntityFrameworkCore;
using MigrationTest2.Data;
using MigrationTest2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationTest2.Execution
{
    public class ProcessMigration
    {

        public static List<Task> taskList = new List<Task>();
        public static async Task StartMigration(int num1, int num2, CancellationToken cts)
        {

            if (!cts.IsCancellationRequested)
            {
                Console.WriteLine("\n");

                Console.WriteLine("Migration of data Started.");
                Console.WriteLine($"Performing migration from {num1} to {num2} ");

                var databaseContext = new MigrationDbContext();

                int totalbatch = num2 - num1;
                int counter = num1;

                int batchCounter = 0;
                int batchdiff = totalbatch;

                while (batchCounter<=totalbatch)
                {

                    var SourceTableList = new List<SourceModel>();
                    
                    int countend = ((totalbatch-batchCounter)>=100)?counter+100:counter+(totalbatch-batchCounter);
                    //Console.WriteLine("Batchcounter: "+batchCounter);
                    //Console.WriteLine("Counter"+counter+" End:"+countend);
                    SourceTableList = databaseContext.sourceModels
                                .Where(x => (x.ID >= counter && x.ID < countend)).ToList();

                    counter+=100;
                    batchCounter+=100;

                    taskList.Add(PerformMigration(SourceTableList, cts));
                        //t.Wait();
                }
                Task T = Task.WhenAll(taskList);
                await T;
            }
        }

        public static async Task PerformMigration(List<SourceModel> SourceTableList, CancellationToken cts)
        {
            //foreach (var source in SourceTableList)
            //    Console.WriteLine(source.ID);
            //var databaseContext = new MigrationDbContext();
            var DestinationData = new List<DestinationModel>();

            if (!cts.IsCancellationRequested)
            {
                foreach (var item in SourceTableList)
                {
                        //Console.WriteLine(item.ID);
                        DestinationData.Add(new DestinationModel()
                        {
                            SourceId = item.ID,
                            Total = await Addition(item.FirstNumner, item.SecondNumber)
                        });
                    }
                    //foreach(var item in DestinationData)
                    //    Console.WriteLine(item.Total);

                    // Console.WriteLine("Length is: ,,,,,"+DestinationData.Count);

                    using (var databaseContext = new MigrationDbContext())
                    {
                        //Console.WriteLine($"Saving ... From {start} to {end} in database");
                        await databaseContext.destinationModels.AddRangeAsync(DestinationData, cts);
                        databaseContext.SaveChanges();
                    }
            }
        }
        public static async Task<int> Addition(int firstNumner,int secondNumber)
        {
            await Task.Delay(50);
            return firstNumner+secondNumber;
        }
    }
}
