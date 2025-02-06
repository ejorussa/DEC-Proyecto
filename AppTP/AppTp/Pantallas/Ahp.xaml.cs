namespace AppTp.Pantallas;

using Metodos;

public partial class Ahp : ContentPage
{
    int cantidadFilas;
    int criteriosG;
    int alternativasG;
    int numeroDeTablaG;
    List<bool> max;
    List<RadioButton> fila = new List<RadioButton>();
    List<List<RadioButton>> matriz = new List<List<RadioButton>>();
    List<AHP> tablasGlobal = new List<AHP>();
    public Ahp(List<AHP> tablas, int criterios, int alternativas, int numeroDeTabla, List<bool> max)
    {
        this.max = max;
        this.tablasGlobal = tablas;
        InitializeComponent();
        if (numeroDeTabla == 0)
        {
            cantidadFilas = (criterios * (criterios - 1)) / 2;
            numeroDeTablaG = numeroDeTabla + 1;
        }
        else
        {
            cantidadFilas = (alternativas * (alternativas - 1)) / 2;
            numeroDeTablaG = numeroDeTabla + 1;
        }
        criteriosG = criterios;
        alternativasG = alternativas;

        ToolbarItem nextToolbarItem = new ToolbarItem
        {
            Text = "Siguiente",
            Priority = 0, // Prioridad para la posición en la barra de herramientas
            Order = ToolbarItemOrder.Primary, // Orden primario en la barra de herramientas
        };
        nextToolbarItem.Clicked += OnNextClicked;

        ToolbarItems.Add(nextToolbarItem);


        // Crear un Grid
        Grid grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) }); // Columna 1
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.075, GridUnitType.Star) }); // Columna 2
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.10, GridUnitType.Star) }); // Columna 3
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.10, GridUnitType.Star) }); // Columna 4
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.75, GridUnitType.Star) }); // Columna 5
        int aux = 1;
        int cont = 2;
        string c1 = "";
        string c2 = "";
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.10, GridUnitType.Star) }); // Agregar una fila
        grid.Add(new Label { Text = "", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000") }, 0, 0); // Columna 1
        grid.Add(new Label { Text = "Cuál prefiere A o B?", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000") }, 1, 0); // Columna 2
        grid.Add(new Label { Text = "", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000") }, 2, 0); // Columna 3
        grid.Add(new Label { Text = "Igual", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000") }, 3, 0); // Columna 4
        grid.Add(new Label { Text = "¿Cuánto más?", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000") }, 4, 0); // Columna 5
                                                                                                                                                    // Agregar elementos a cada fila
        for (int i = 0; i < cantidadFilas; i++)
        {
            if (numeroDeTabla == 0)
            {
                if (cont <= criterios)
                {
                    c1 = "Cr-" + aux;
                    c2 = "Cr-" + cont;
                    cont = cont + 1;

                }
                else
                {
                    aux = aux + 1;
                    cont = aux + 1;
                    c1 = "Cr-" + aux;
                    c2 = "Cr-" + cont;
                    cont++;
                }
            }
            else
            {
                if (cont <= alternativas)
                {
                    c1 = "A-" + aux;
                    c2 = "A-" + cont;
                    cont = cont + 1;

                }
                else
                {
                    aux = aux + 1;
                    cont = aux + 1;
                    c1 = "A-" + aux;
                    c2 = "A-" + cont;
                    cont++;
                }
            }

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Agregar una fila
            fila = new List<RadioButton> { new RadioButton { Content = c1, GroupName = "fila" + (i + 1), TextColor = Color.FromHex("#000000") }, new RadioButton { Content = c2, GroupName = "fila" + (i + 1), TextColor = Color.FromHex("#000000") }, new RadioButton { Content = "1", GroupName = "comparaciones" + (i + 1), TextColor = Color.FromHex("#000000") } };
            // Elementos de la fila


            grid.Add(new Label { Text = (i+1).ToString(), VerticalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#000000"), HorizontalOptions = LayoutOptions.Center }, 0, i + 1); // Columna 1
            grid.Add(fila[0], 1, i + 1); // Columna 2
            grid.Add(fila[1], 2, i + 1); // Columna 3
            grid.Add(fila[2], 3, i + 1); // Columna 4
            grid.Add(new Frame { Content = CreateFrameContent(i, fila), BackgroundColor = Colors.Transparent, BorderColor = Colors.Transparent}, 4, i + 1); // Columna 5
        }

        // Agregar el Grid a tu página
        ScrollView sv = new ScrollView();
        sv.Orientation = ScrollOrientation.Vertical;
        sv.BackgroundColor = Color.FromHex("#E3FEF7");
        sv.Content = grid;
        Content = sv;


    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        AHP ahp;
        if (numeroDeTablaG == 1)
        {
            ahp = new AHP(criteriosG, matriz);
        }
        else
        {
            ahp = new AHP(alternativasG, matriz);
        }

        if (ahp.comprobador())
        {
            tablasGlobal.Add(ahp);
        }
        if (tablasGlobal.Count < (criteriosG + 1))
        {
            if (ahp.comprobador())
            {

                Navigation.PushAsync(new Ahp(tablasGlobal, criteriosG, alternativasG, numeroDeTablaG, max));
            }
            else
            {
                DisplayAlert("Error", "Las decisiones no tienen concistencia", "OK");

            }

        }
        else
        {
            float[,] matriz = new float[alternativasG, criteriosG];
            int fila = 0;
            for (int i = 0; i < criteriosG; i++)
            {
                fila = 0;
                float[] promactual = tablasGlobal[i + 1].promedioFilas;
                while (fila < alternativasG)
                {
                    matriz[fila, i] = promactual[fila];
                    fila++;
                }

            }
            List<float> a = new List<float>(tablasGlobal[0].promedioFilas);
            Metodos.MultiCriterio pl = new Metodos.MultiCriterio(matriz,a , max, 0);
            pl.resolver();
            Navigation.PushAsync(new Pantallas.Pasos.AHP_TabbedPage(tablasGlobal, pl));
        }
        
    }

    View CreateFrameContent(int numero, List<RadioButton> fila)
    {
        // Crear un HorizontalStackLayout
        var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

        // Agregar RadioButton al HorizontalStackLayout
        for (int i = 2; i <= 9; i++)
        {
            RadioButton rb = new RadioButton { Content = i.ToString(), GroupName = "comparaciones" + (numero + 1), TextColor = Color.FromHex("#000000") };
            stackLayout.Children.Add(rb);
            fila.Add(rb);
        }
        matriz.Add(fila);
        fila = new List<RadioButton>();
        return stackLayout;
    }
    private void Peso2_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {



    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
#endif
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
#endif
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {

    }
}

// Acordarnos de sacar el ultimo elemento en la lista al volver a la pagina anterior
