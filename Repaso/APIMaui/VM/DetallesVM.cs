using APIMaui.VM.Util;
using ENT;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APIMaui.VM
{
    [QueryProperty(nameof(IdPersona), "idPersona")]
    public class DetallesVM : clsVMBase
    {
        #region ATRIBUTOS
        private Personas persona;
        private DelegateCommand btnVolverCmd;
        private int idPersona;
        #endregion

        #region PROPIEDADES
        public Personas Persona
        {
            get => persona;
            set { persona = value; OnPropertyChanged(nameof(Persona)); }
        }

        public DelegateCommand BtnVolverCmd => btnVolverCmd;

        public int IdPersona
        {
            get => idPersona;
            set
            {
                idPersona = value;
                OnPropertyChanged(nameof(IdPersona));
                obtienePersona();  // Se ejecuta cuando se recibe el ID
            }
        }
        #endregion

        #region Constructor
        public DetallesVM()
        {
            btnVolverCmd = new DelegateCommand(btnVolverCmdExecute);
        }
        #endregion

        #region FUNCIONES
        private async void obtienePersona()
        {
            if (idPersona > 0) // Evita llamadas innecesarias con ID 0
            {
                Persona = await DAL.ManejadoraAPI.ObtenerObjMAUI(idPersona);
            }
        }

        private async void btnVolverCmdExecute()
        {
            await Shell.Current.GoToAsync("///listaPersona");
        }
        #endregion
    }
}
