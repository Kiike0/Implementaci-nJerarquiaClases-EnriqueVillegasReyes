using System.Reflection.Metadata.Ecma335;
using System.Transactions;

public class saleDePantalla : Exception
{
    public saleDePantalla(String mensaje) : base ("¡Error!: "+mensaje){
        
    }
}
public interface IGrafico
{
    public Boolean mover(int x, int y);
    public String dibujar();
}

public class Punto : IGrafico
{
    private int _x;
    public int x
    {
        get
        {
            return _x;
        }
        set
        {
            
            if(value < 0 || value > 800)
            {
                throw new saleDePantalla("Se sale de pantalla (Máx. 800x600)");
            }
            else
            {
                _x = value; 
            }
        }


    }

    private int _y;
    public int y {
        get
        {
            return _y;
        }
        set
        {

            if (value < 0 || value > 800)
            {
                throw new saleDePantalla("Se sale de pantalla (Máx. 800x600)");
            }
            else
            {
                _y = value;
            }
        }

    }

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

public class Rectangulo : Punto
{
    //Comprobamos que el tamaño del rectángulo no sale de pantalla
    private int _ancho;
    public int ancho
    {
        get
        {
            return _ancho;
        }
        set
        {

            if (value < 0 || value > 800)
            {
                throw new saleDePantalla("Se sale de pantalla (Máx. 800x600)");
            }
            else
            {
                _ancho = value;
            }
        }


    }

    private int _alto;
    public int alto
    {
        get
        {
            return _alto;
        }
        set
        {

            if (value < 0 || value > 600)
            {
                throw new saleDePantalla("Se sale de pantalla (Máx. 800x600)");
            }
            else
            {
                _alto = value;
            }
        }

    }

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

public class Circulo : Punto
{
    //Comprobamos que el tamaño del rectángulo no sale de pantalla
    private int _radio;
    public int radio
    {
        get
        {
            return _radio;
        }
        set
        {

            if (value < 0 || value > 300)
            {
                throw new saleDePantalla("Se sale de pantalla (Máx. 800x600)"); //Suponemos que el radio es la mitad del tamaño vertical de la pantalla
            }
            else
            {
                _radio = value;
            }
        }


    }

    public Circulo(int x, int y, int radio) : base(x, y)
    {
        this.radio = radio;
    }

    public override String dibujar()
    {
        return "Círculo: Radio: " + radio + " Valores x e y: " + x + ", " + y;
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
        Console.WriteLine("----------------------------BIENVENIDO AL EDITOR GRAFICO---------------------------------");

        Console.WriteLine("\nPASO 1:");
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
            try
                {
                Console.WriteLine("Inserta el valor x del Punto: ");
                if (int.TryParse(Console.ReadLine(), out puntox))
                {
                    puntoOk = true; //Comprueba si el número se ingresó correctamente
                }
                else
                {
                    Console.WriteLine("Valor x no válido.");
                    puntoOk = false;
                }
                Console.WriteLine("Inserta el valor y del Punto: ");
                if (int.TryParse(Console.ReadLine(), out puntoy))
                {
                    puntoOk = true;
                }
                else
                {
                    Console.WriteLine("Valor y no válido.");
                    puntoOk = false;
                }
                Punto punto = new Punto(puntox, puntoy);
                graficoCompuesto.AgregarElemento(punto);
            } catch (saleDePantalla ex)
            {
               Console.WriteLine(ex.Message);
               puntoOk=false;
            }

        }
            
       



        //Insertamos los valores del Rectangulo para crear un Objeto Rectangulo para agregarlo al grafico
        Console.WriteLine("Inserta los valores del Rectángulo: ");
      
        int ancho = 0;
        int alto = 0;
        Boolean rectanguloOk = false;

        while(!rectanguloOk) {
            try
            {
                Console.WriteLine("Inserta el ancho: ");
                if (int.TryParse(Console.ReadLine(), out ancho))
                {
                    rectanguloOk = true; //Comprueba si el número se ingresó correctamente
                }
                else
                {
                    Console.WriteLine("Valor del ancho no válido.");
                    rectanguloOk = false;
                }
                Console.WriteLine("Inserta el alto: ");
                if (int.TryParse(Console.ReadLine(), out alto))
                {
                    rectanguloOk = true;
                }
                else
                {
                    Console.WriteLine("Valor del alto no válido.");
                    rectanguloOk = false;
                }
                Rectangulo rectangulo = new Rectangulo(puntox, puntoy, ancho, alto);
                graficoCompuesto.AgregarElemento(rectangulo);
            } catch (saleDePantalla ex)
            {
                Console.WriteLine(ex.Message);
                rectanguloOk=false;
            }
        }

            
        


        //Insertamos los valores del Circulo para crear un Objeto Circulo para agregarlo al grafico
        Console.WriteLine("Inserta los valores del Círculo: ");

        int radio = 0;
        Boolean circuloOk = false;

        while (!circuloOk)
        {
            try
            {
                Console.WriteLine("Inserta el valor del radio: ");
                if (int.TryParse(Console.ReadLine(), out radio))
                {
                    circuloOk = true; //Comprueba si el número se ingresó correctamente
                }
                else
                {
                    Console.WriteLine("Valor del radio no válido.");
                    circuloOk = false;
                }
                Circulo circulo = new Circulo(puntox, puntoy, radio);
                graficoCompuesto.AgregarElemento(circulo);
        } catch (saleDePantalla ex)
            {
                Console.WriteLine (ex.Message);
                circuloOk = false;
            }
        }
            

        //Siguiente paso: dibujar el gráfico
        Console.WriteLine("\nPASO 2:");
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
                            try
                            {
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
                            } catch (saleDePantalla ex)
                            {
                                Console.WriteLine(ex.Message);
                                valoresOk |= false;
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