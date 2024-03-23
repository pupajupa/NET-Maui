using System.Collections.ObjectModel;
using System.Text;
using MauiAppAntikhovitch.Entities;
using MauiAppAntikhovitch.Services;
namespace MauiAppAntikhovitch.View;

public partial class CurrencyConverterPage : ContentPage
{
    private IRateService _service;
    private ObservableCollection<string> _currencies = new ObservableCollection<string>();
    public ObservableCollection<string> Currencies
    {
        get { return _currencies; }
        set { _currencies = value; }
    }
    public CurrencyConverterPage(IRateService rateService)
    {
        InitializeComponent();
        _service = rateService;
        datePicker.MaximumDate = DateTime.Now;
        getTodayRates(DateTime.Now);
        BindingContext = this;
    }
    private string _selectedCurrency;
    public string SelectedCurrency
    {
        get { return _selectedCurrency; }
        set
        {
            if (_selectedCurrency != value)
            {
                _selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }
    }
    public async Task getTodayRates(DateTime dt)
    {
        IEnumerable<Rate>? rates = await _service.GetRates(dt);
        if (rates != null)
        {
            StringBuilder ratesText = new StringBuilder();
            ratesText.Clear();
            foreach (Rate rate in rates)
            {
                if (rate.Cur_Abbreviation == "RUB")
                    ratesText.AppendLine($"100 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");
                if (rate.Cur_Abbreviation == "EUR")
                    ratesText.AppendLine($"1 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");
                if (rate.Cur_Abbreviation == "USD")
                    ratesText.AppendLine($"1 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");
                if (rate.Cur_Abbreviation == "CHF")
                    ratesText.AppendLine($"1 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");
                if (rate.Cur_Abbreviation == "CNY")
                    ratesText.AppendLine($"10 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");
                if (rate.Cur_Abbreviation == "GBP")
                    ratesText.AppendLine($"1 {rate.Cur_Abbreviation} = {rate.Cur_OfficialRate} BYN");

                if (!_currencies.Contains(rate.Cur_Abbreviation) && (rate.Cur_Abbreviation == "RUB" || rate.Cur_Abbreviation == "EUR"
                    || rate.Cur_Abbreviation == "USD" || rate.Cur_Abbreviation == "CHF" || rate.Cur_Abbreviation == "CNY" ||
                    rate.Cur_Abbreviation == "GBP"))
                {
                    _currencies.Add(rate.Cur_Abbreviation);
                }
            }
            EnterRate.Text = ratesText.ToString();
        }
        else
        {
            EnterRate.Text = "Failed to fetch rates from API";
        }
    }
    private async void ConvertLeftClicked(object sender, EventArgs e)
    {
        IEnumerable<Rate>? rates = await _service.GetRates(DateTime.Now);
        if (double.TryParse(EnterLeft.Text, out double amount))
        {
            Rate selectedRate = rates.FirstOrDefault(r => r.Cur_Abbreviation == SelectedCurrency);
            if (selectedRate != null)
            {
                if (selectedRate.Cur_Abbreviation == "RUB")
                {
                    double convertedAmount = Math.Round(amount / (double)selectedRate.Cur_OfficialRate * 100, 4);
                    EnterRight.Text = convertedAmount.ToString();
                }
                if (selectedRate.Cur_Abbreviation == "USD" || selectedRate.Cur_Abbreviation == "EUR" ||
                    selectedRate.Cur_Abbreviation == "GBP" || selectedRate.Cur_Abbreviation == "CHF")
                {
                    double convertedAmount = Math.Round(amount / (double)selectedRate.Cur_OfficialRate, 4);
                    EnterRight.Text = convertedAmount.ToString();
                }
                if (selectedRate.Cur_Abbreviation == "CNY")
                {
                    double convertedAmount = Math.Round(amount / (double)selectedRate.Cur_OfficialRate * 10, 4);
                    EnterRight.Text = convertedAmount.ToString();
                }
            }
        }
    }
    private async void ConvertRightClicked(object sender, EventArgs e)
    {
        IEnumerable<Rate>? rates = await _service.GetRates(DateTime.Now);
        if (double.TryParse(EnterRight.Text, out double amount))
        {
            Rate selectedRate = rates.FirstOrDefault(r => r.Cur_Abbreviation == SelectedCurrency);
            if (selectedRate != null)
            {
                if (selectedRate.Cur_Abbreviation == "RUB")
                {
                    double convertedAmount = Math.Round(amount * (double)selectedRate.Cur_OfficialRate / 100, 4);
                    EnterLeft.Text = convertedAmount.ToString();
                }
                if (selectedRate.Cur_Abbreviation == "USD" || selectedRate.Cur_Abbreviation == "EUR" || selectedRate.Cur_Abbreviation == "GBP"
                    || selectedRate.Cur_Abbreviation == "CHF")
                {
                    double convertedAmount = Math.Round(amount * (double)selectedRate.Cur_OfficialRate, 4);
                    EnterLeft.Text = convertedAmount.ToString();
                }
                if (selectedRate.Cur_Abbreviation == "CNY")
                {
                    double convertedAmount = Math.Round(amount * (double)selectedRate.Cur_OfficialRate * 10 / 100, 4);
                    EnterLeft.Text = convertedAmount.ToString();
                }
            }
        }
    }


    private async void DateChanged(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        await getTodayRates(selectedDate);
    }
}