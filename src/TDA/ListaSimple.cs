namespace src.TDA
{
    public class Nodo<T>{
        public T dato { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T valor)
        {
            dato= valor;
        }
    }
    public class ListaSimple<T>
    {
        private Nodo<T>? cabeza;
        
        public void Agregar(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(valor);
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                Nodo<T>? actual = cabeza;
                while (actual?.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual!.Siguiente = nuevoNodo;
            }
        }

        public void Mostrar()
        {
            Nodo<T>? actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine(actual.dato);
                actual = actual.Siguiente;
            }
        }
    }

    
}