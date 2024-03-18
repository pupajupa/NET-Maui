using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppAntikhovitch.Entities
{
    [Table("HospitalRooms")]
    public class HospitalRoom
    {
        [PrimaryKey,AutoIncrement,Indexed]
        public int Id { get; set; }
        public string RoomNumber { get; set; }
    }
}
