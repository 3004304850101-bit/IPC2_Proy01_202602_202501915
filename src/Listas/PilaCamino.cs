namespace src.Modelo
{
    public class NodoPila
    {
        public Paso? pasoC { get; set; }
        public NodoPila? siguiente { get; set; }

        public NodoPila(Paso paso)
        {
            pasoC = paso;
        }
    }

    public class Pila
    {
        public NodoPila? cima { get; set; }
        public int contador { get; set;}

        public Pila()
        {
            contador=0;
        }

        public void Apilar(Paso paso)
        {
            NodoPila nodoPila=new NodoPila(paso);
            if (cima == null)
            {
                cima=nodoPila;
            }
            else
            {
                nodoPila.siguiente=cima;
                cima=nodoPila;
            }
            contador++;
        }

        public Paso Desapilar()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("PILA ESTA VACIA");
            }
            Paso? paso=cima!.pasoC;

            cima=cima.siguiente;
            contador--;

            return paso!;
        }

        public bool EstaVacia()
        {
            return contador == 0;
        }

        public Paso? Cima()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("PILA ESTA VACIA");
            }
            return cima!.pasoC;
        }
    }
}