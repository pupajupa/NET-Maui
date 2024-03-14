using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using MauiAppAntikhovitch.Entities;

namespace MauiAppAntikhovitch.Services
{
    public interface IDbService
    {
        IEnumerable<HospitalRoom> GetAllHospitalRooms();
        IEnumerable<Patient> GetHospitalRoomPatients(int id);
        void Init();    
    }
}
