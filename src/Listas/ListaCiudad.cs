namespace src.Modelo
{
    public class NodoCiudad
    {
        public Ciudad? Ciudad { get; set; }
        public NodoCiudad? SiguienteCiudad { get; set; }
        public NodoCiudad(Ciudad? ciudad)
        {
            Ciudad = ciudad;
        }
    }

    public class ListaCiudad
    {
        public NodoCiudad? PrimerCiudad { get; set; }
        public NodoCiudad? UltimaCiudad { get; set; }

        public void AgregarCiudad(Ciudad ciudad)
        {
            NodoCiudad nuevoNodo = new NodoCiudad(ciudad);
            if (PrimerCiudad == null)
            {
                PrimerCiudad = nuevoNodo;
            }
            else
            {
                UltimaCiudad!.SiguienteCiudad = nuevoNodo;
            }
            UltimaCiudad=nuevoNodo;
        }
    }
}