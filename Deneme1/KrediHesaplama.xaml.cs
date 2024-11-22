namespace Deneme1;


    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent();

            // Kredi T�rlerini Tan�mla
            KrediTuruPicker.ItemsSource = new string[]
            {
                "�htiya� Kredisi",
                "Konut Kredisi",
                "Ta��t Kredisi",
                "Ticari Kredisi"
            };
        }

    private void Hesapla_Clicked(object sender, EventArgs e)
    {
        // Se�ilen kredi t�r�n� al�yoruz
        string secilenKrediTuru = KrediTuruPicker.SelectedItem?.ToString()?.Trim();

        // Giri�lerin ge�erlili�ini kontrol ediyoruz
        bool krediMiktariGecerli = double.TryParse(TutarEntry.Text, out double krediMiktari);
        bool faizOraniGecerli = double.TryParse(FaizOraniEntry.Text, out double faizOrani);
        bool vadeGecerli = int.TryParse(VadeEntry.Text, out int vade);

        if (string.IsNullOrEmpty(secilenKrediTuru) || !krediMiktariGecerli || !faizOraniGecerli || !vadeGecerli)
        {
            DisplayAlert("Hata", "L�tfen ge�erli giri�ler yap�n�z.", "Tamam");
            return;
        }

        // Faiz oran�n� desimale �eviriyoruz (y�zde olarak verilmi�se)
        double brutFaizOrani = faizOrani / 100;

        // Kredi t�r�ne g�re faiz oran�n� d�zenliyoruz
        switch (secilenKrediTuru)
        {
            case "�htiya� Kredisi":
                brutFaizOrani += brutFaizOrani * 0.10 + brutFaizOrani * 0.15; // Ekstra faizler
                break;
            case "Ta��t Kredisi":
                brutFaizOrani += brutFaizOrani * 0.15 + brutFaizOrani * 0.05; // Ekstra faizler
                break;
            case "Konut Kredisi":
                // Faiz oran� oldu�u gibi kalacak
                break;
            case "Ticari Kredisi":
                brutFaizOrani += brutFaizOrani * 0.50 + brutFaizOrani * 0.05; // Ekstra faizler
                break;
            default:
                DisplayAlert("Hata", "Ge�erli bir kredi t�r� se�iniz.", "Tamam");
                return;
        }

        // Ayl�k taksiti hesapl�yoruz
        double aylikTaksit = ((Math.Pow(1 + brutFaizOrani, vade) * brutFaizOrani) / (Math.Pow(1 + brutFaizOrani, vade) - 1)) * krediMiktari;

        // Toplam �deme ve toplam faiz hesaplamalar�n� yap�yoruz
        double toplamOdeme = aylikTaksit * vade;
        double toplamFaiz = toplamOdeme - krediMiktari;

        // Hesaplama sonu�lar�n� ekrana yazd�r�yoruz
        AylikTaksitLabel.Text = $"Ayl�k Taksit: {aylikTaksit:F2} TL";
        ToplamOdemeLabel.Text = $"Toplam �deme: {toplamOdeme:F2} TL";
        ToplamFaizLabel.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
    }

    // Vade slider'� ile ilgili de�er de�i�imi
    private void OnVadeSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        double yeniDeger = e.NewValue;
        VadeEntry.Text = yeniDeger.ToString("0");
    }
}
    
