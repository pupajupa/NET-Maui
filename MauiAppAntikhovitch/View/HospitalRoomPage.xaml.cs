namespace MauiAppAntikhovitch.View;

public partial class HospitalRoomPage : ContentPage
{
	public HospitalRoomPage()
	{
		InitializeComponent();
	}
    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        header.Text = $"�� �������: {languagePicker.SelectedItem}";
        // ��� ��� ����� ������
        //header.Text = $"�� �������: {languagePicker.Items[languagePicker.SelectedIndex]}";
    }
}