using MigrationTest2.Data;
using MigrationTest2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTest2.Execution
{
    public class MigrationOperation
    {

        public static async Task<MigrationModel> CreateMigration(int num1,int num2)
        {

            var databaseContext = new MigrationDbContext();

            MigrationModel migrateob = new MigrationModel();
            migrateob.StartTime = DateTime.Now;
            migrateob.Start = num1;
            migrateob.End = num2;
            migrateob.Status = "Ongoing";

            await databaseContext.migrationModels.AddAsync(migrateob);
            databaseContext.SaveChanges();

            return migrateob;
        }

        public static void UpdateMigration(int mId,string status)
        {
            var databaseContext = new MigrationDbContext();

            var MigrationData = databaseContext.migrationModels.Find(mId);
            MigrationData.Status = status;
            databaseContext.migrationModels.Attach(MigrationData);
            databaseContext.SaveChanges();
        }

        public static void ShowMigrationStatus()
        {
            var databaseContext = new MigrationDbContext();

            var MigrationStatus = databaseContext.migrationModels.ToList();

            Console.WriteLine("ID\tStart Value\tEnd Value\tStatus");
            foreach (var migration in MigrationStatus)
            {
                Console.WriteLine(migration.Id+"\t"+migration.Start+"\t"+migration.End+"\t"+migration.Status);
            }

        }
    }
}
