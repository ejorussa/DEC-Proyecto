using AppTp.Entidades;
using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class AHP_TabbedPage : TabbedPage
{
    AHP ahp;
	public AHP_TabbedPage(List<AHP> ahp, MultiCriterio pl)
	{
		InitializeComponent();
        
		List<string[,]> matricesComparacion = new List<string[,]>();
        List<string[,]> matricesNormalizadas = new List<string[,]>();
        int cont = 0;
        List<string> listaLetras = new List<string>
            {
                "Pesos Relativos"
            };
        List<float[]> pesosRela = new List<float[]>();
        int max = 0;
        int colmax = 0;
        foreach (AHP elemento in ahp)
		{
            if (cont == 0)
            {
                string[,] comparacion = Entidades.formatoAhp.formatoExcel(elemento.matriz, false, "C" + cont.ToString());
                pesosRela.Add(elemento.promedioFilas);
                var grid = new Grid();
                
                Metodos.formatoTabla.CreateTable(comparacion, "C" , grid);
                PantallaPaso1.Children.Add(grid);
                var grid2 = new Grid();
                string[,] normalizado = formatoAhp.AgregarColumna(Entidades.formatoAhp.formatoExcel(elemento.matrizNormalizada, false, "C" + cont.ToString()), pesosRela, listaLetras);
                Metodos.formatoTabla.CreateTable(normalizado, "C", grid2);
                PantallaPaso2.Children.Add(grid2);
                if (max < elemento.matriz.GetLength(0) + 1)
                {
                    max = elemento.matriz.GetLength(0) + 1;
                }
                if (colmax < elemento.matriz.GetLength(1) + 2)
                {
                    colmax = elemento.matriz.GetLength(1) + 2;
                }
                grid.Margin = 10;
                grid2.Margin = 10;
                pesosRela = new List<float[]>();

            }
            else
            {
                var grid = new Grid();
                var grid2 = new Grid();
                grid.Margin = 10;
                grid2.Margin = 10;
                string[,] comparacion = Entidades.formatoAhp.formatoExcel(elemento.matriz, true, "C" + cont.ToString());
                pesosRela.Add(elemento.promedioFilas);
                string[,] normalizado = formatoAhp.AgregarColumna(Entidades.formatoAhp.formatoExcel(elemento.matrizNormalizada, true, "C" + cont.ToString()), pesosRela, listaLetras);
                if (max < elemento.matriz.GetLength(0) + 1)
                {
                    max = elemento.matriz.GetLength(0) + 1;
                }
                if (colmax < elemento.matriz.GetLength(1) + 2)
                {
                    colmax = elemento.matriz.GetLength(1) + 2;
                }
                pesosRela = new List<float[]>();
                Metodos.formatoTabla.CreateTable(comparacion, "C", grid);
                Metodos.formatoTabla.CreateTable(normalizado, "C", grid2);
                PantallaPaso1.Children.Add(grid);
                PantallaPaso2.Children.Add(grid2);
            }
            cont++;
		}
        Metodos.formatoTabla.CreateTable(pl.agregacionExcel(), "Paso 3", GridPL);
        resultado.mostrarResultados(pl.ordenarResultado());

    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        string nombreArchivo = await DisplayPromptAsync("Nombre del archivo", "Ingrese el nombre del archivo donde desea guardar la resolucion del problema");

        if(nombreArchivo != "" || nombreArchivo != null)
        {
            
        }
    }
}