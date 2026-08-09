using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
namespace src.Modelo
{
    public class LectorXML
    {
        private readonly XDocument _doc;

         public LectorXML(string ruta)
        {
            _doc = XDocument.Load(ruta);
        }

        public ListaCiudad CrearCiudades()
        {
            ListaCiudad ciudades= new ListaCiudad();
            foreach(XElement e in _doc.Root!.Element("listaCiudades")!.Elements("ciudad"))
            {
                    XElement elemento= e.Element("nombre")!;
                    string Nombre=elemento.Value.Trim();
                    int CantidadFilas=(int)elemento.Attribute("filas")!;
                    int CantidadColumnas=(int)elemento.Attribute("columnas")!;

                    Ciudad ciudad= new Ciudad(Nombre,CantidadFilas,CantidadColumnas);
                    ciudad.AgregarFilas(CantidadFilas);
                    ciudad.AgregarColumnas(CantidadColumnas);
                    
                    //LECTURA DE FILAS
                foreach (XElement elementof in e.Elements("fila"))
                {
                    int fila=(int)elementof.Attribute("numero")!;
                    //Cadena limpia
                    string cadena=elementof.Value.Trim();
                    //longitud cadena
                    int longer=cadena.Length-2;
                    //Cadena
                    cadena=cadena.Substring(1,longer);

                    for (int i = 0; i < longer; i++)
                    {
                        string caracter=cadena[i].ToString();
                        ciudad.AgregarCelda(fila,i+1,caracter);
                    }

                }

                    //LECTURA UNIDAD MILITAR
                foreach(XElement elementom in e.Elements("unidadMilitar"))
                {
                    int filam=(int) elementom.Attribute("fila")!;
                    int columnam=(int) elementom.Attribute("columna")!;
                    int combate=int.Parse(elementom.Value);
                    ciudad.AsignarUnidadMilitar(filam,columnam,combate);
                }

                ciudades.AgregarCiudad(ciudad);
            }

            return ciudades;
        }

        //Lista Robots
        public ListaRobot CrearRobots()
        {
            ListaRobot robots = new ListaRobot();
            foreach(XElement elementor in _doc.Root!.Element("robots")!.Elements("robot"))
            {
                XElement elmr=elementor.Element("nombre")!;
                string nombrer=elmr.Value.Trim();
                string tipor=elmr.Attribute("tipo")!.Value;

                if (tipor == "ChapinFighter")
                {
                    int combater=(int)elmr.Attribute("capacidad")!;
                    Fighter fighter= new Fighter(nombrer,combater);
                    robots.AgregarRobot(fighter);
                }
                else
                {
                    Rescue rescue= new Rescue(nombrer);
                    robots.AgregarRobot(rescue);
                }

                
            }
            return robots;
        }
    }
}