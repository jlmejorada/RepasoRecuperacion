using APIMaui.VM.Util;
using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMaui.VM
{
    public class AgregarVM : clsVMBase
    {
        #region ATRIBUTOS
        private Personas persona = new Personas();
        private DelegateCommand btnVolverCmd;
        private DelegateCommand btnGuardarCmd;
        #endregion

        #region PROPIEDADES
        public Personas Persona { get { return persona; } set { persona = value; } }
        public DelegateCommand BtnVolverCmd { get { return btnVolverCmd; } }

        public DelegateCommand BtnGuardarCmd { get { return btnGuardarCmd; } }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor por defectos
        /// </summary>
        public AgregarVM()
        {
            btnVolverCmd = new DelegateCommand(btnVolverCmdExecute);
            btnGuardarCmd = new DelegateCommand(btnGuardarCmdExecute);
        }
        #endregion

        #region FUNCIONES
        private async void btnVolverCmdExecute()
        {
            await Shell.Current.GoToAsync("///listaPersona");
        }
        private async void btnGuardarCmdExecute()
        {
            ObservableCollection<Personas> lista = await ManejadoraAPI.ObtenerListado();
            int id = lista.Count+1;
            Personas personaNueva = new Personas(id, persona.Nombre, persona.Direccion);
            await ManejadoraAPI.InsertarObjMAUI(personaNueva);
            await Shell.Current.GoToAsync("///listaPersona");
        }
        #endregion
    }
}
