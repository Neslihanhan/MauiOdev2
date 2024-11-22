namespace Deneme1;

public partial class VucutKitleIndeksi : ContentPage
{
	public VucutKitleIndeksi()
	{
		InitializeComponent();
        KiloSlider.ValueChanged += (s, e) => KiloEntry.Text = e.NewValue.ToString("0");
        BoySlider.ValueChanged += (s, e) => BoyEntry.Text = e.NewValue.ToString("0.00");
    }

    private void OnKiloSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        KiloEntry.Text = e.NewValue.ToString("0");
    }

    private void OnBoySliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        BoyEntry.Text = e.NewValue.ToString("0.00");
    }

    private void OnHesaplaClicked(object sender, EventArgs e)
    {
        // Giriþ deðerlerini kontrol et
        if (!double.TryParse(KiloEntry.Text, out double kilo) || !double.TryParse(BoyEntry.Text, out double boy))
        {
            DisplayAlert("Hata", "Lütfen geçerli bir deðer giriniz.", "Tamam");
            return;
        }

        if (boy <= 0)
        {
            DisplayAlert("Hata", "Boy sýfýrdan büyük olmalýdýr.", "Tamam");
            return;
        }

        // VKÝ Hesaplama
        double vki = kilo / (boy * boy);
        SonucLabel.Text = $"Vücut Kitle Ýndeksiniz: {vki:F2}";

        // Durum Belirleme
        string durum;
        if (vki < 16)
            durum = "Ýleri düzeyde zayýf";
        else if (vki < 17)
            durum = "Orta düzey zayýf";
        else if (vki < 18.5)
            durum = "Hafif düzeyde zayýf";
        else if (vki < 25)
            durum = "Normal kilo";
        else if (vki < 30)
            durum = "Hafif þiþman";
        else if (vki < 35)
            durum = "1. derece obez";
        else if (vki < 40)
            durum = "2. derece obez";
        else
            durum = "Morbid obez";

        DurumLabel.Text = durum;
    }
}

