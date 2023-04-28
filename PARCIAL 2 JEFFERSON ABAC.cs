using System;

class Program
{

    static int[,] tablero;

    static void Paso1_CrearTablero()
    {
        for (int f = 0; f < tablero.GetLength(0); f++)
        {
            for (int c = 0; c < tablero.GetLength(1); c++)
            {
                tablero[f, c] = 0; // para rellenar con 0 
            }
        }
    }
    static void Paso2_ColocarBarcos()
    {
        
        Random rand = new Random();

        
        int num_barcos = 0;

        // para ver cuantos barcos colocar 
        if (tablero.GetLength(0) <= 5 && tablero.GetLength(1) <= 5)
        {
            num_barcos = 4; // Si la matriz es de tamaño 5x5 o menor, se colocan 4 barcos
        }
        else if (tablero.GetLength(0) <= 10 && tablero.GetLength(1) <= 10)
        {
            num_barcos = 7; // Si la matriz es de tamaño 10x10 o menor, se colocan 7 barcos
        }
        else
        {
            num_barcos = 13; // Si la matriz es de tamaño mayor a 10x10, se colocan 13 barcos
        }

        // poner barcos a lo loco
        for (int i = 0; i < num_barcos; i++)
        {
            int fila = rand.Next(0, tablero.GetLength(0));
            int columna = rand.Next(0, tablero.GetLength(1));

            // ver si la casilla esta vacilla antes de colocar un barco
            if (tablero[fila, columna] == 0)
            {
                tablero[fila, columna] = 1;
            }
            else
            {
                i--; // Si la casilla ya está ocupada, generar otra posición aleatoria
            }
        }
    }

    static void Paso3_ImprimirTablero()
    {
        string caracterImprimir = "";

        // Se recorre la matriz para imprimir cada casilla
        for (int f = 0; f < tablero.GetLength(0); f++)
        {
            for (int c = 0; c < tablero.GetLength(1); c++)
            {
                switch (tablero[f, c])
                {
                    case 0:
                        caracterImprimir = "            ~"; // Si la casilla no ha sido seleccionada, se imprime un tilde
                        break;
                    case 1:
                        caracterImprimir = "            ~"; // Si la no casilla ha sido seleccionada pero  hay barco, se imprime un tilde
                        break;
                    case -1:
                        caracterImprimir = "            *"; // Si la casilla tiene un barco y ha sido seleccionada, se imprime un asterisco
                        break;
                    case -2:
                        caracterImprimir = "            X"; // Si la casilla no tiene un barco y ha sido seleccionada, se imprime una equis
                        break;
                    default:
                        caracterImprimir = "            ~"; // En cualquier otro caso, se imprime un tilde
                        break;
                }

                Console.Write(caracterImprimir + " ");
            }

            // Se imprime un salto de línea para pasar a la siguiente fila
            Console.WriteLine();
        }
    }

    static void Paso4_IngresoCoordenadas()
    {
        // Declaramos algunas variables para el control del juego
        int fila, columna = 0;
        int intentos = 0;
        int barcosRestantes = tablero.GetLength(0) * tablero.GetLength(1) / 4; // número de barcos a encontrar, basado en el tamaño del tablero

        // Limpiamos la consola antes de empezar
        Console.Clear();

        //  bucle principal del juego
        do
        {
            try
            {
                
                Console.Write("Ingrese la Fila (0-{0}): ", tablero.GetLength(0) - 1);
                string entradaFila = Console.ReadLine();
                fila = int.Parse(entradaFila);

                if (fila < 0)
                {
                    // Si el usuario ingresa un número negativo
                   throw new Exception("El valor ingresado para la fila no es válido. Por favor ingrese un valor numérico positivo.");
                }

                Console.Write("Ingresa la Columna (0-{0}): ", tablero.GetLength(1) - 1);
                string entradaColumna = Console.ReadLine();
                columna = int.Parse(entradaColumna);

                if (columna < 0)
                {
                    
                    throw new Exception("El valor ingresado para la columna no es válido. Por favor ingrese un valor numérico positivo.");
                }

                if (tablero[fila, columna] == 1)
                {
                    // Si el usuario ha acertado, hacemos un sonido y marcamos la posición como atacada (*)
                    Console.Beep(1100, 800);
                    tablero[fila, columna] = -1;
                    barcosRestantes--;

                    if (barcosRestantes == 0)
                    {
                        // Si ya no quedan barcos por encontrar, el usuario ha ganado y terminamos el juego
                        Console.WriteLine("¡Felicitaciones! Has encontrado todos los barcos. Presiona cualquier tecla para salir.");
                        Console.ReadKey();
                        break;
                    }
                }
                else
                {
                    // Si el usuario ha fallado, marcamos la posición como atacada sin acierto (-2)
                    tablero[fila, columna] = -2;
                }

                // Limpiamos la consola y mostramos el tablero actualizado
                Console.Clear();
                Paso3_ImprimirTablero();

                // Incrementamos el contador de intentos
                intentos++;

                if (intentos >= tablero.GetLength(0) * tablero.GetLength(1))
                {
                    // Si el usuario ha alcanzado el máximo de intentos permitidos, el juego termina
                    Console.WriteLine("¡Lo siento! Has alcanzado el máximo de intentos. Presiona cualquier tecla para salir.");
                    Console.ReadKey();
                    break;
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, mostramos un mensaje de error y esperamos a que el usuario presione una tecla para continuar
                Console.WriteLine(ex.Message);
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                    Console.Clear();
                Paso3_ImprimirTablero();
            }

        } while (true);
    }


    static void Main(string[] args)
    {
        Console.WriteLine("      ------------------------------------------------------------------------------                                                                             ");
        Console.WriteLine("                             Bienvenido al juego battlesheep  ");
        Console.WriteLine("             sonido de beep y un asterisco  indica que le diste a un barco  ");
        Console.WriteLine("                      una X indica que fallaste el tiro karnal\n\n");
        Console.WriteLine("                           esta es la version beta 1.1 jaja\n\n");
        Console.WriteLine("      ------------------------------------------------------------------------------                                                                             ");
        // Definir variable para continuar jugando
        bool continuarJugando = true;

        // Iniciar bucle de juego
        do
        {
            int filas, columnas;

            // Pedir al usuario la cantidad de filas del tablero
            Console.Write("Ingrese la cantidad de filas del tablero: ");
            if (!int.TryParse(Console.ReadLine(), out filas))
            {
                Console.WriteLine("Error: debe ingresar un número entero.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                continue; //  al inicio del bucle
            }

            Console.Write("Ingrese la cantidad de columnas del tablero: ");
            if (!int.TryParse(Console.ReadLine(), out columnas))
            {
                Console.WriteLine("Error: debe ingresar un número entero.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                continue; 
            }

            // Crear el tablero con el tamaño que nos dio el usuario 
            tablero = new int[filas, columnas];

            // llamamos las funciones para teb y bar
            Paso1_CrearTablero();
            Paso2_ColocarBarcos();

            Console.Clear();

            Paso3_ImprimirTablero();

            Paso4_IngresoCoordenadas();

            // Preguntar al usuario si quiere seguir jugando
            Console.Write("¿Desea continuar jugando? (s/n): ");
            string respuesta = Console.ReadLine();
            if (respuesta.ToLower() == "n")
            {
                continuarJugando = false; // Si la respuesta es 'n', terminar el bucle de juego
            }

            Console.Clear(); 
        } while (continuarJugando);
    }
}