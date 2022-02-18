using MigrationTest2.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationTest2.Execution
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           
            while (true)
            {

                Console.Clear();
                Console.WriteLine(".....Welcome To Data Migration Application....");

                Console.WriteLine("Select One Option...");
                Console.WriteLine("                      Press Any Key to Perform Migration");
                Console.WriteLine("                      Press Esacpe to Exit");
                
                ConsoleKeyInfo c=Console.ReadKey();
                if(c.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Thank you...");
                    break;
                }
                else
                {
                    Console.WriteLine("You have Entered choice to perform Migration...");
                }

                CancellationTokenSource cts = new CancellationTokenSource();
                var token = cts.Token;
                int num1, num2;

                bool check = Inputnum(out num1, out num2);


                if (check==true)
                {

                    MigrationModel ms = await MigrationOperation.CreateMigration(num1, num2);
                    int migrateid = ms.Id;
                    Task MigrateTask = ProcessMigration.StartMigration(num1, num2, token);
                    Console.WriteLine("Performing Migration..");


                    Task cancelTask = Task.Run(async () =>
                    {
                        while (true)
                        {
                            Console.WriteLine("Enter status to Show Status");
                            Console.WriteLine("Enter cancel to cancel task");
                            
                            string choice=Console.ReadLine();
                            if (choice.Equals("cancel"))
                            {
                                cts.Cancel();
                                //Console.WriteLine("TaskCancelled");
                                break;
                            }
                            else if(choice.Equals("status"))
                            {
                                 MigrationOperation.ShowMigrationStatus();
                            }
                            
                        }
                    });

                    Task MergerdTask = await Task.WhenAny(new[] { MigrateTask, cancelTask });

                    if (MergerdTask.IsCompleted && cts.IsCancellationRequested)
                    {
                        Console.WriteLine("Migration Cancelled....");
                        MigrationOperation.UpdateMigration(migrateid, "Cancelled");
                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("Migration Completed....");
                        MigrationOperation.UpdateMigration(migrateid, "Completed");
                        Console.ReadLine();

                    }

                }
                else
                {
                    Console.WriteLine("Please Enter Correct Values");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                    
                }
            }
        }
        private static bool Inputnum(out int num1, out int num2)
        {
            try
            {
                Console.WriteLine("\nEnter the Start Number from where you want to start Migration");
                num1 = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nEnter the End Number till where you want to end Migration");
                num2 = Int32.Parse(Console.ReadLine());
                if (num1>0 && num2>0 && num2>num1)
                {
                    return true;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Please Enter Correct Input");
                num1=num2=0;
            }
            return false;
        }    
    }
}
