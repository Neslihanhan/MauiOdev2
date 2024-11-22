namespace Deneme1;


    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent();

            // Kredi Türlerini Tanýmla
            KrediTuruPicker.ItemsSource = new string[]
            {
                "Ýhtiyaç Kredisi",
                "Konut Kredisi",
                "Taþýt Kredisi",
                "Ticari Kredisi"
            };
        }

    private void Hesapla_Clicked(object sender, EventArgs e)
    {
        // Seçilen kredi türünü alýyoruz
        string secilenKrediTuru = KrediTuruPicker.SelectedItem?.ToString()?.Trim();

        // Giriþlerin geçerliliðini kontrol ediyoruz
        bool krediMiktariGecerli = double.TryParse(TutarEntry.Text, out double krediMiktari);
        bool faizOraniGecerli = double.TryParse(FaizOraniEntry.Text, out double faizOrani);
        bool vadeGecerli = int.TryParse(VadeEntry.Text, out int vade);

        if (string.IsNullOrEmpty(secilenKrediTuru) || !krediMiktariGecerli || !faizOraniGecerli || !vadeGecerli)
        {
            DisplayAlert("Hata", "Lütfen geçerli giriþler yapýnýz.", "Tamam");
            return;
        }

        // Faiz oranýný desimale çeviriyoruz (yüzde olarak verilmiþse)
        double brutFaizOrani = faizOrani / 100;

        // Kredi türüne göre faiz oranýný düzenliyoruz
        switch (secilenKrediTuru)
        {
            case "Ýhtiyaç Kredisi":
                brutFaizOrani += brutFaizOrani * 0.10 + brutFaizOrani * 0.15; // Ekstra faizler
                break;
            case "Taþýt Kredisi":
                brutFaizOrani += brutFaizOrani * 0.15 + brutFaizOrani * 0.05; // Ekstra faizler
                break;
            case "Konut Kredisi":
                // Faiz oraný olduðu gibi kalacak
                break;
            case "Ticari Kredisi":
                brutFaizOrani += brutFaizOrani * 0.50 + brutFaizOrani * 0.05; // Ekstra faizler
                break;
            default:
                DisplayAlert("Hata", "Geçerli bir kredi türü seçiniz.", "Tamam");
                return;
        }

        // Aylýk taksiti hesaplýyoruz
        double aylikTaksit = ((Math.Pow(1 + brutFaizOrani, vade) * brutFaizOrani) / (Math.Pow(1 + brutFaizOrani, vade) - 1)) * krediMiktari;

        // Toplam ödeme ve toplam faiz hesaplamalarýný yapýyoruz
        double toplamOdeme = aylikTaksit * vade;
        double toplamFaiz = toplamOdeme - krediMiktari;

        // Hesaplama sonuçlarýný ekrana yazdýrýyoruz
        AylikTaksitLabel.Text = $"Aylýk Taksit: {aylikTaksit:F2} TL";
        ToplamOdemeLabel.Text = $"Toplam Ödeme: {toplamOdeme:F2} TL";
        ToplamFaizLabel.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
    }

    // Vade slider'ý ile ilgili deðer deðiþimi
    private void OnVadeSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        double yeniDeger = e.NewValue;
        VadeEntry.Text = yeniDeger.ToString("0");
    }
}
    
