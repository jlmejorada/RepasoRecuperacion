namespace ENT
{
    public class Personas
    {
        // Propiedades

        private int id;
        private string nombre;
        private string direccion;

        public int Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }

        // Constructor sin parámetros
        public Personas()
        {
        }

        // Constructor con parámetros
        public Personas(int id, string nombre, string direccion)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
        }

        public Personas(Personas persona)
        {
            Id = persona.id;
            Nombre = persona.nombre;
            Direccion = persona.direccion;
        }
    }
}
