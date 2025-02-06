

namespace GraficoApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private List<Nodo> nodos = new List<Nodo>();
        private List<Arista> aristas = new List<Arista>();

        public MainPage()
        {
            InitializeComponent();
            float ultimonodoparx = 0;
            float ultimonodopary = 0;
            float ultimonodoimparx = 0;
            float ultimonodoimpary = 0;

            int[,] matriz = new int[15, 15];

            // Crear una instancia de Random para generar números aleatorios
            Random random = new Random();

            // Generar valores aleatorios de 0 y 1 en la matriz
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    matriz[i, j] = random.Next(2); // Genera un número aleatorio entre 0 y 1
                }
            }

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                
                if(i == 0)
                {
                    Nodo nodo = new Nodo("Nodo " + (i + 1), 30, 30);
                    nodos.Add(nodo);
                    ultimonodoparx = nodo.X;
                    ultimonodopary = nodo.Y;

                }
                
                if(i % 2 == 0 && i > 1)
                {
                    Nodo nodo = new Nodo("Nodo " + (i + 1), ultimonodoparx + 100, ultimonodopary);
                    nodos.Add(nodo);
                    ultimonodoparx = nodo.X;
                    ultimonodopary = nodo.Y;
                }
                if(i % 2 != 0 && i > 1)
                {
                    Nodo nodo = new Nodo("Nodo " + (i + 1), ultimonodoimparx + 100, ultimonodoimpary);
                    nodos.Add(nodo);
                    ultimonodoimparx = nodo.X;
                    ultimonodoimpary = nodo.Y;
                }
                if (i == 1)
                {
                    Nodo nodo = new Nodo("Nodo " + (i + 1), ultimonodoparx, ultimonodopary + 200);
                    nodos.Add(nodo);
                    ultimonodoimparx = nodo.X;
                    ultimonodoimpary = nodo.Y;
                }
                
                
                
            }

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == 1)
                    {
                        Arista arista = new Arista(nodos[i], nodos[j]);
                        aristas.Add(arista);
                    }
                }
            }
            var drawable = new GrafoDrawable(nodos, aristas);
            graphView.Drawable = drawable;
            graphView.Invalidate();
        }
    }
}

