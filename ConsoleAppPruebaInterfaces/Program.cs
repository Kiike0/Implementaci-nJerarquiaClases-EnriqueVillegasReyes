using System.Reflection.Metadata.Ecma335;

public interface IGrafico
{
    public Boolean mover(int x, int y);
    public String dibujar();
}

public class Punto : IGrafico
{
    public int x { get; protected set; }
    public int y { get; protected set; }

    public Punto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Boolean mover(int x, int y)
    {
        if (x > 800 || y > 600) //Para cumplir la condición planteada
            return false;
        else
        {
            this.x = x;
            this.y = y;
            return true;
        }
    }
    public virtual String dibujar()
    {
        return "Punto: Valores x e y: " + x + ", " + y;
    }

}

public class Circulo : Punto
{
    public int radio { get; protected set; }

    public Circulo(int x, int y, int radio) : base(x, y)
    {
        this.radio = radio;
    }

    public override String dibujar()
    {
        return "Círculo: Radio: " + radio + " Valores x e y: " + x + ", " + y;
    }

}

public class Rectangulo : Punto
{
    public int ancho { get; protected set; }
    public int alto { get; protected set; }

    public Rectangulo(int x, int y, int ancho, int alto) : base(x, y)
    {
        this.ancho = ancho;
        this.alto = alto;
    }

    public override String dibujar()
    {
        return "Rectángulo: Ancho: " + ancho + " Alto: " + alto + " Valores x e y: " + x + ", " + y;
    }

}

public class GraficoCompuesto : IGrafico
{
    private List<IGrafico> elementos = new List<IGrafico>();

    public GraficoCompuesto()
    {

    }

    public Boolean mover(int x, int y)
    {
        if (x > 800 || y > 600)
            return false;

        foreach (var elemento in elementos)
        {
            if (!elemento.mover(x, y))
                return false;
        }

        return true;
    }
    public String dibujar()
    {
        string _result = "";
        foreach (var elemento in elementos)
        {
            _result += elemento.dibujar() +"\n";
        }
        return _result;

    }

    public void AgregarElemento(IGrafico elemento)
    {
        elementos.Add(elemento);
    }


}

public class EditorGrafico
{

    static void Main(string[] args)
    {
        Console.WriteLine("--------------------------BIENVENIDO AL EDITOR GRAFICO-------------------------------");

        Console.WriteLine("PASO 1:");
        GraficoCompuesto graficoCompuesto = new GraficoCompuesto();
        Console.WriteLine("Inserta los datos para crear un Gráfico Compuesto de un Punto, un Rectángulo y un Círculo");

        //Insertamos los valores del punto para crear un Objeto Punto para agregarlo al grafico
        Console.WriteLine("Inserta los valores del Punto: ");

        int puntox = 0;
        int puntoy = 0;
        Boolean puntoOk = false;

        //Creamos un while para controlar que siga preguntando hasta que ingrese los valores correctamente
        while (!puntoOk)
        {
            Console.WriteLine("Inserta el valor x del Punto: ");
            if (int.TryParse(Console.ReadLine(), out puntox))
            {
                Console.WriteLine("Ingresaste el número correctamente ");
                puntoOk = true;
            }
            else
            {
                Console.WriteLine("No ingresaste un número válido");
                puntoOk = false;
            }
            Console.WriteLine("Inserta el valor y del Punto: ");
            if (int.TryParse(Console.ReadLine(), out puntoy))
            {
                Console.WriteLine("Ingresaste el número correctamente ");
                puntoOk = true;
            }
            else
            {
                Console.WriteLine("No ingresaste un número válido");
                puntoOk = false;
            }
            if(puntoOk)
            {
                Punto puntoComprobacion = new Punto(puntox, puntoy);
                if (!puntoComprobacion.mover(puntox, puntoy))
                {
                    puntoOk = false;
                    Console.WriteLine("Error: El punto se sale de la pantalla (Máx:800x600)");
                }
            }
            
        }
        Punto punto = new Punto(puntox, puntoy);
        graficoCompuesto.AgregarElemento(punto);



        //Insertamos los valores del Rectangulo para crear un Objeto Rectangulo para agregarlo al grafico
        Console.WriteLine("Inserta los valores del Rectángulo: ");
      
        int ancho = 0;
        int alto = 0;
        Boolean rectanguloOk = false;

        while(!rectanguloOk) {
            Console.WriteLine("Inserta el ancho: ");
            if (int.TryParse(Console.ReadLine(), out ancho))
            {
                Console.WriteLine("Ingresaste el número correctamente ");
                rectanguloOk = true;
            }
            else
            {
                Console.WriteLine("No ingresaste un número válido");
                rectanguloOk = false;
            }
            Console.WriteLine("Inserta el alto: ");
            if (int.TryParse(Console.ReadLine(), out alto))
            {
                Console.WriteLine("Ingresaste el número correctamente ");
                rectanguloOk = true;
            }
            else
            {
                Console.WriteLine("No ingresaste un número válido");
                rectanguloOk = false;
            }
            if (ancho>800 || alto>600)
            {
                rectanguloOk = false;
                Console.WriteLine("Error: El tamaño del rectángulo se sale de la pantalla (Máx:800x600)");
            }
        }
        Rectangulo rectangulo = new Rectangulo(puntox, puntoy, ancho, alto);
        graficoCompuesto.AgregarElemento(rectangulo);


        //Insertamos los valores del Circulo para crear un Objeto Circulo para agregarlo al grafico
        Console.WriteLine("Inserta los valores del Círculo: ");

        int radio = 0;
        Boolean circuloOk = false;

        while(!circuloOk)
        {
            Console.WriteLine("Inserta el valor del radio: ");
            if (int.TryParse(Console.ReadLine(), out radio))
            {
                Console.WriteLine("Ingresaste el número correctamente ");
                circuloOk = true;
            }
            else
            {
                Console.WriteLine("No ingresaste un número válido");
                circuloOk = false;
            }
            if (radio> 400)
            {
                circuloOk = false;
                Console.WriteLine("Error: El tamaño del círculo se sale de la pantalla (Máx:800x600)");
            }
        }
        Circulo circulo = new Circulo(puntox, puntoy, radio);
        graficoCompuesto.AgregarElemento(circulo);

        //Siguiente paso: dibujar el gráfico
        Console.WriteLine("PASO 2:");
        Console.WriteLine("Aquí va el grafico");
        Console.WriteLine(graficoCompuesto.dibujar());

        //Ultimo paso: Elegir si moverlo o salir
        Console.WriteLine("PASO 3:");
        
       
        int opcion = 0;
        Boolean salirOk = false;
        while(!salirOk) {
            Console.WriteLine("Elige entre las siguientes dos opciones");
            Console.WriteLine("1. Mover el gráfico");
            Console.WriteLine("2. Salir");
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Inserta las nuevas coordenadas (x, y) para mover el gráfico:");
                        Boolean valoresOk = false;
                        while (!valoresOk)
                        {
                            Console.WriteLine("Inserta el valor x:");
                            if (int.TryParse(Console.ReadLine(), out int newX))
                            {
                                Console.WriteLine("Inserta el valor y:");
                                if (int.TryParse(Console.ReadLine(), out int newY))
                                {
                                    if (graficoCompuesto.mover(newX, newY))
                                    {
                                        Console.WriteLine("Gráfico movido exitosamente.");
                                        Console.WriteLine(graficoCompuesto.dibujar());
                                        valoresOk = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El gráfico no puede moverse fuera de la pantalla (Máximo: 800x600)");
                                        valoresOk = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Valor y no válido.");
                                    valoresOk = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Valor x no válido.");
                                valoresOk = false;
                            }
                            
                        }
                        break;
                    case 2:
                        Console.WriteLine("Saliendo del programa.");
                        salirOk = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
        
    }
}