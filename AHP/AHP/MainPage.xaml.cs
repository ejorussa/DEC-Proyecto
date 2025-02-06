using System.Data;

namespace AHP
{
    public partial class MainPage : ContentPage
    {
        int cantidadFilas;
        int criteriosG;
        int alternativasG;
        int numeroDeTablaG;
        List<RadioButton> fila = new List<RadioButton>();
        List<List<RadioButton>> matriz = new List<List<RadioButton>>();
        List<AHP> tablasGlobal = new List<AHP>();
        public MainPage(List<AHP> tablas, int criterios, int alternativas, int numeroDeTabla)
        {
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
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.10, GridUnitType.Star) }); // Columna 1
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.20, GridUnitType.Star) }); // Columna 2
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.20, GridUnitType.Star) }); // Columna 3
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.10, GridUnitType.Star) }); // Columna 4
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.50, GridUnitType.Star) }); // Columna 5
            int aux = 1;
            int cont = 2;
            string c1 = "";
            string c2 = "";
            grid.Add(new Label { Text = "", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0); // Columna 1
            grid.Add(new Label { Text = "Cuál prefiere A o B?", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 1, 0); // Columna 2
            grid.Add(new Label { Text = "", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 0); // Columna 3
            grid.Add(new Label { Text = "Igual", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 3, 0); // Columna 4
            grid.Add(new Label { Text = "¿Cuánto más?", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 4, 0); // Columna 5
            // Agregar elementos a cada fila
            for (int i = 0; i < cantidadFilas; i++)
            {
                if (numeroDeTabla == 0)
                {
                    if (cont <= criterios)
                    {
                        c1 = "Criterio-" + aux;
                        c2 = "Criterio-" + cont;
                        cont = cont + 1;

                    }
                    else
                    {
                        aux = aux + 1;
                        cont = aux + 1;
                        c1 = "Criterio-" + aux;
                        c2 = "Criterio-" + cont;
                        cont++;
                    }
                }
                else
                {
                    if (cont <= alternativas)
                    {
                        c1 = "Criterio-" + aux;
                        c2 = "Criterio-" + cont;
                        cont = cont + 1;

                    }
                    else
                    {
                        aux = aux + 1;
                        cont = aux + 1;
                        c1 = "Criterio-" + aux;
                        c2 = "Criterio-" + cont;
                        cont++;
                    }
                }

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Agregar una fila
                fila = new List<RadioButton> { new RadioButton { Content = c1, GroupName = "fila" + (i + 1) }, new RadioButton { Content = c2, GroupName = "fila" + (i + 1) }, new RadioButton { Content = "1", GroupName = "comparaciones" + (i + 1) } };
                // Elementos de la fila

                
                grid.Add(new Label { Text = "1", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, i + 1); // Columna 1
                grid.Add(fila[0], 1, i + 1); // Columna 2
                grid.Add(fila[1], 2, i + 1); // Columna 3
                grid.Add(fila[2], 3, i + 1); // Columna 4
                grid.Add(new Frame { Content = CreateFrameContent(i, fila), HasShadow = false, BorderColor = Color.FromRgb(0, 0, 0) }, 4, i + 1); // Columna 5
            }

            // Agregar el Grid a tu página
            ScrollView sv = new ScrollView();
            sv.Orientation = ScrollOrientation.Vertical;
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

                    Navigation.PushAsync(new MainPage(tablasGlobal, criteriosG, alternativasG, numeroDeTablaG));
                }
                else
                {
                    DisplayAlert("Hola", "nononoonononononon", "Cancelar");

                }

            }
            else
            {
                DisplayAlert("Hola", "Hola", "Cancelar");
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
                Ponderacion_Lineal pl = new Ponderacion_Lineal(matriz, tablasGlobal[0].promedioFilas);
                pl.resolver();
                DisplayAlert("Hola", "Hola", "Cancelar");
            }
            }

            View CreateFrameContent(int numero, List<RadioButton> fila)
            {
                // Crear un HorizontalStackLayout
                var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

                // Agregar RadioButton al HorizontalStackLayout
                for (int i = 2; i <= 9; i++)
                {
                    RadioButton rb = new RadioButton { Content = i.ToString(), GroupName = "comparaciones" + (numero + 1) };
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

            private void Button_Clicked(object sender, EventArgs e)
            {
                //esto se puede automatizar a medida que creamos filas y columnas desde el codigo

                //List<RadioButton> fila2 = new List<RadioButton> { c3, c4, Igual1, Peso10, Peso11, Peso12, Peso13, Peso14, Peso15, Peso16, Peso17 };
                //List<RadioButton> fila3 = new List<RadioButton> { c5, c6, Igual2, Peso18, Peso19, Peso20, Peso21, Peso22, Peso23, Peso24, Peso25 };
                int filas = 2;
                int columnas = 2;
                float[,] tablaresultado = new float[filas, columnas]; ;

                //No es el numero de comparaciones, es el tamaño de la matriz
                int contador = 0;
                int contador2 = 0;
                int seleccionado = 0;
                int comparaciones = 2;
                int auxiliar = 0;
                foreach (List<RadioButton> lista in matriz)
                {
                    if (contador < comparaciones - 1)
                    {
                        auxiliar = 0;
                        foreach (RadioButton rb in lista)
                        {
                            if (rb.IsChecked && auxiliar == 0)
                            {
                                seleccionado = 1;
                            }
                            if (rb.IsChecked && auxiliar == 1)
                            {
                                seleccionado = 2;
                            }
                            if (rb.IsChecked && auxiliar != 0 && auxiliar != 1 && seleccionado == 1)
                            {
                                tablaresultado[contador2, contador + 1] = float.Parse(rb.Content.ToString());
                            }
                            else if (rb.IsChecked && auxiliar != 0 && auxiliar != 1 && seleccionado == 2)
                            {
                                tablaresultado[contador2, contador + 1] = 1 / float.Parse(rb.Content.ToString());
                            }
                            auxiliar++;

                        }
                        contador++;

                    }
                    else
                    {
                        auxiliar = 0;
                        contador--;
                        contador2++;
                        foreach (RadioButton rb in lista)
                        {
                            if (rb.IsChecked && auxiliar == 0)
                            {
                                seleccionado = 1;
                            }
                            if (rb.IsChecked && auxiliar == 1)
                            {
                                seleccionado = 2;
                            }
                            if (rb.IsChecked && auxiliar != 0 && auxiliar != 1 && seleccionado == 1)
                            {
                                tablaresultado[contador2, contador + 1] = float.Parse(rb.Content.ToString());
                            }
                            else if (rb.IsChecked && auxiliar != 0 && auxiliar != 1 && seleccionado == 2)
                            {
                                tablaresultado[contador2, contador + 1] = 1 / float.Parse(rb.Content.ToString());
                            }
                            auxiliar++;

                        }
                    }

                }
                contador = 0;
                // Agregar filas al DataTable y asignar valores
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        //Se hacen n(n-1)/2 comparaciones (3*2)/2 se hacen 3 comparaciones

                        if (j == i)
                        {
                            tablaresultado[i, j] = 1;
                            break;
                        }
                    }
                    if (i != 0)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            tablaresultado[i, k] = 1 / tablaresultado[k, i];
                        }
                    }

                }
                DisplayAlert("Hola", "Hola", "Cancelar");
            }
        

    }
}
// Acordarnos de sacar el ultimo elemento en la lista al volver a la pagina anterior
