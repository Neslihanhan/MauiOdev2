namespace Deneme1;

public partial class RenkSecici : ContentPage
{
	public RenkSecici()
	{
		InitializeComponent();
        UpdateColorPreview();

    }
    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
      
        int red = (int)RedSlider.Value;
        int green = (int)GreenSlider.Value;
        int blue = (int)BlueSlider.Value;

       
        var selectedColor = Color.FromRgb(red, green, blue);

        
        ColorBox.BackgroundColor = selectedColor;

       
        string hexCode = $"#{red:X2}{green:X2}{blue:X2}";

      
        HexColorLabel.Text = $"Hex Kodu: {hexCode}";
    }

    private void UpdateColorPreview()
    {
        int red = (int)RedSlider.Value;
        int green = (int)GreenSlider.Value;
        int blue = (int)BlueSlider.Value;

        // Oluþan rengi güncelle
        ColorBox.BackgroundColor = Color.FromRgb(red, green, blue);

        // Hex kodunu güncelle
        CopyHexButton.Text = $"Hex Kodunu Kopyala: #{red:X2}{green:X2}{blue:X2}";
    }

    private void OnRandomColorButtonClicked(object sender, EventArgs e)
    {
        var random = new Random();
        RedSlider.Value = random.Next(0, 256);
        GreenSlider.Value = random.Next(0, 256);
        BlueSlider.Value = random.Next(0, 256);

        UpdateColorPreview();
    }


    private void OnCopyHexButtonClicked(object sender, EventArgs e)
    {
        
        var hexCode = HexColorLabel.Text.Replace("Hex Kodu: ", "");
        Clipboard.SetTextAsync(hexCode);

        
        DisplayAlert("Bilgi", "Hex kodu kopyalandý: " + hexCode, "Tamam");
    }
}

    


