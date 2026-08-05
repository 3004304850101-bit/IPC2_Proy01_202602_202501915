namespace src.Modelo
{
    public class Filas
    {
        public int NumeroFila { get; set; }
        public Celda? PrimerCelda { get; set; }
        public Filas? SiguienteFila { get; set; }
        public Filas(int numeroFila)
        {
            NumeroFila = numeroFila;
        }
    }
}