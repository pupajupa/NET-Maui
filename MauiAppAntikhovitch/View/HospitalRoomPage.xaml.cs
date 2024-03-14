namespace MauiAppAntikhovitch.View;

public partial class HospitalRoomPage : ContentPage
{
	public HospitalRoomPage()
	{
		InitializeComponent();
	}
    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        header.Text = $"Вы выбрали: {languagePicker.SelectedItem}";
        // или так через индекс
        //header.Text = $"Вы выбрали: {languagePicker.Items[languagePicker.SelectedIndex]}";
    }
}