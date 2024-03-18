namespace MauiAppAntikhovitch.View;
using MauiAppAntikhovitch.Entities;
using MauiAppAntikhovitch.Services;
using System.Collections.ObjectModel;

public partial class HospitalRoomPage : ContentPage
{
    private IDbService _databaseService;
    public ObservableCollection<HospitalRoom> HospitalRooms { get; set; }
	public HospitalRoomPage(IDbService databaseService)
	{
		InitializeComponent();
        _databaseService = databaseService;
        _databaseService.Init();
	}

    private void LoadedHospitalRooms(object sender, EventArgs e)
    {
        HospitalRooms = new ObservableCollection<HospitalRoom>(_databaseService.GetAllHospitalRooms());
        HospitalRoomsPicker.ItemsSource = HospitalRooms;
    }
    private void SelectedHospitalRoom(object sender, EventArgs e)
    {
        if (HospitalRoomsPicker.SelectedItem is HospitalRoom selectedRoom)
        {
            var patients = _databaseService.GetHospitalRoomPatients(selectedRoom.Id);
            PatientsCollection.ItemsSource = patients;
        }
    }
}