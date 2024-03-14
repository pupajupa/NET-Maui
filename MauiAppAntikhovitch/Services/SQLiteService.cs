using System.Text;
using MauiAppAntikhovitch.Entities;
using SQLite;

namespace MauiAppAntikhovitch.Services
{
    class SQLiteService:IDbService
    {
        List<string> m_names = ["Ivan", "Bob", "David", "Frank", "Alex", "Dima", "Petr", "Egor", "Jack", "Mainson"];
        List<string> f_names = ["Alice", "Charlie", "Emma", "Anna", "Rina", "Sonya", "Kris", "Dasha", "Clara"];
        private const string _dbName = "SQLiteDb.db3";
        public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;
        private static string _dbPath => Path.Combine(FileSystem.AppDataDirectory,_dbName);
        SQLiteConnection Database;
        public void Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteConnection(_dbPath, Flags);
            Database.CreateTable<HospitalRoom>();
            Database.CreateTable<Patient>();
            int NumHospitalRoom = new Random().Next(2, 4);
            for (int i = 0; i < NumHospitalRoom; i++)
            {
                var room = new HospitalRoom
                {
                    RoomNumber = new Random().Next(1, 100),
                    BedCount = new Random().Next(1, 5)
                };
                Database.Insert(room);
                int PatientCount = new Random().Next(5, 10);
                for (int j = 0; j < PatientCount; j++)
                {
                    string male, name;
                    Random random = new Random();
                    if (i % 2 == 0)
                    {
                        name = m_names[new Random().Next(0, m_names.Count)];
                        male = "M";
                    }
                    else
                    {
                        male = "F";
                        name = f_names[new Random().Next(0, f_names.Count)];
                    }
                    var patient = new Patient
                    {
                        Name = name,
                        Gender = male,
                        Age = new Random().Next(20, 50),
                        HospitalRoomId = i + 1,
                    };
                    Database.Insert(patient);
                }
            }
        }
        public IEnumerable<HospitalRoom> GetAllHospitalRooms()
        {
            Init();
            return Database.Table<HospitalRoom>().ToList();
        }

        public IEnumerable<Patient> GetHospitalRoomPatients(int id)
        {
            Init();
            return Database.Table<Patient>().Where(i => i.HospitalRoomId == id).ToList();
        }
        public SQLiteService() { }
    }
}
