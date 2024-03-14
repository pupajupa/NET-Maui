using System.Diagnostics;

namespace MauiAppAntikhovitch.View;

public partial class IntegralCalculationPage : ContentPage
{
    CancellationTokenSource? cancellationTokenSource;
    CancellationToken? token;

    public IntegralCalculationPage()
    {
        InitializeComponent();
    }

    private async void StartButton_Clicked(object sender, EventArgs e)
    {
        calculateLabel.Text = "0%";
        statusLabel.Text = "Вычисление";
        startButton.IsEnabled = false;
        cancelButton.IsEnabled = true;

        progressBar.Progress = 0;
        cancellationTokenSource = new CancellationTokenSource();
        token = cancellationTokenSource.Token;

        double result = await Task.Run(() => CalculateIntegration(cancellationTokenSource.Token));

        if (!cancellationTokenSource.Token.IsCancellationRequested)
            statusLabel.Text = $"Результат: {result}";
        else
            statusLabel.Text = "Задание отменено";

        startButton.IsEnabled = true;
        cancelButton.IsEnabled = false;
    }

    private async Task<double> CalculateIntegration(CancellationToken cancellationToken)
    {
        Debug.WriteLine($"main thread =====================>{Thread.CurrentThread.ManagedThreadId}");
        double step = 0.0001;
        double result = 0;
        double currentProgress = 0;

        for (double x = 0; x < 1; x += step)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Debug.WriteLine($"integral thread =====================>{Thread.CurrentThread.ManagedThreadId}");
                if (x > currentProgress)
                {
                    currentProgress += 0.01;
                    progressBar.ProgressTo(currentProgress, 1, Easing.Linear);
                    calculateLabel.Text = $"{(int)(currentProgress * 100)}%";
                }
            });

            result += Math.Sin(x) * step;
        }
        Debug.WriteLine($"main thread =====================>{Thread.CurrentThread.ManagedThreadId}");
        return result;
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        cancellationTokenSource?.Cancel();
    }
}