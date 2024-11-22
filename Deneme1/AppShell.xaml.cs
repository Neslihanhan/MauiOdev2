

           namespace Deneme1
    {
        public partial class AppShell : Shell
        {
            public AppShell()
            {
                InitializeComponent();

            
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(KrediHesaplama), typeof(KrediHesaplama));
            Routing.RegisterRoute(nameof(RenkSecici), typeof(RenkSecici));
            Routing.RegisterRoute(nameof(VucutKitleIndeksi), typeof(VucutKitleIndeksi));
            Routing.RegisterRoute(nameof(YapilacaklarListesi), typeof(YapilacaklarListesi));

        }
    }
    }

   

    


        
    

