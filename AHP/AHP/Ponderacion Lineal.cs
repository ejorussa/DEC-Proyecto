using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP
{
    class Ponderacion_Lineal
    {
        public float[,] matriz {  get; set; }
        public float[,] matrizNormalizada { get; set; }
        float[] pesos {  get; set; }
        float[] resultado { get; set; }

        public Ponderacion_Lineal(float[,] matriz, float[] pesos) 
        {
            this.matriz = matriz;
            this.pesos = pesos;
        }

        public void resolver()
        {
            int columnas = matriz.GetLength(1); // Obtener el número de columnas
            int filas = matriz.GetLength(0); // Obtener el número de filas
            this.matrizNormalizada = new float[filas, columnas];
            float[] sumaColumnas = new float[columnas]; // Array para almacenar la suma de cada columna
            resultado = new float[filas];
            // Iterar sobre cada columna
            for (int j = 0; j < columnas; j++)
            {
                float suma = 0;

                // Sumar los elementos de la columna actual
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    suma += matriz[i, j];
                }

                // Almacenar la suma en el array
                sumaColumnas[j] = suma;
            }

            //normalizar
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizNormalizada[i, j] = (matriz[i, j] / sumaColumnas[j]);
                }
            }
            //Agregacion
            float acu = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    acu = acu + (matrizNormalizada[i, j] * pesos[j]);
                }
                resultado[i] = acu;
                acu = 0;
            }
        }

    }
}
